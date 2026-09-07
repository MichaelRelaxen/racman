using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Net;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace racman {
    public class Ratchetron : IPS3API {
        public static int ReceiveTimeoutMs = 8000;
        public static int SendTimeoutMs = 8000;

        string ip {
            get;
            set;
        }

        private int port = 9671;

        private TcpClient client;
        private UdpClient udpClient;
        private NetworkStream stream;
        private volatile bool connected = false;
        private uint apiRevision = 0;
        private Thread dataThread;

        private readonly object syncRoot = new object();

        private List<int> memorySubs = new List<int>();
        private Dictionary<int, Action<byte[]>> memSubCallbacks = new Dictionary<int, Action<byte[]>>();
        private Dictionary<int, uint> memSubTickUpdates = new Dictionary<int, uint>();
        private Dictionary<int, UInt32> frozenAddresses = new Dictionary<int, uint>();
        private readonly object memorySubsLock = new object();

        private int connectionLostRaised;


        private volatile int currentPid = 0;
        public int CurrentPid => currentPid;
        private volatile bool lastInGameState = false;

        public Ratchetron(string ip) : base(ip) {
            this.ip = ip;
        }

        public override uint ServerRevision => apiRevision;

        public override bool IsConnected => connected;

        public bool SupportsModernCommands => apiRevision >= 5;

        public override bool Connect() {
            try {
                this.client = new TcpClient(this.ip, this.port);
                this.client.NoDelay = true;
                this.client.ReceiveTimeout = ReceiveTimeoutMs;
                this.client.SendTimeout = SendTimeoutMs;

                this.stream = client.GetStream();
                this.stream.ReadTimeout = ReceiveTimeoutMs;
                this.stream.WriteTimeout = SendTimeoutMs;

                byte[] connMsg = new byte[6];
                ReadExact(connMsg, 0, 6);

                uint apiRev = BitConverter.ToUInt32(connMsg.Skip(2).Take(4).Reverse().ToArray(), 0);

                if (apiRev < 4) {
                    MessageBox.Show("The Ratchetron module loaded on your PS3 is too old, you need to restart your PS3 to load the new version.");
                    CloseSockets();
                    return false;
                }

                if (connMsg[0] == 0x01) {
                    this.apiRevision = apiRev;
                    this.connected = true;
                    this.connectionLostRaised = 0;

#if DEBUG
                    this.EnableDebugMessages();
#endif

                    try {
                        this.currentPid = getCurrentPID();
                    }
                    catch (Exception ex) {
                        Console.WriteLine($"Could not fetch initial PID on connect: {ex.Message}");
                    }

                    return true;
                }
            }
            catch (SocketException) {
                CloseSockets();
                return false;
            }
            catch (Exception) {
                // who cares about error handling anyway?
                CloseSockets();
                return false;
            }

            return false;
        }

        public override bool Disconnect() {
            if (connected) {
                this.ReleaseAllSubs();
            }

            this.connected = false;
            CloseSockets();

            Thread thread = this.dataThread;
            if (thread != null && thread != Thread.CurrentThread && thread.IsAlive) {
                thread.Join(500);
            }
            this.dataThread = null;

            return true;
        }

        private void CloseSockets() {
            this.udpClient?.Close();
            this.stream?.Close();
            this.client?.Close();

            this.udpClient = null;
            this.stream = null;
            this.client = null;
        }

        // Reads exactly `count` bytes, looping until the buffer is full instead of trusting a
        // single Read() call to return everything.
        private void ReadExact(byte[] buffer, int offset, int count) {
            NetworkStream s = this.stream;
            if (s == null) {
                throw new IOException("Not connected to Ratchetron.");
            }

            int read = 0;
            while (read < count) {
                int n = s.Read(buffer, offset + read, count - read);
                if (n <= 0) {
                    throw new IOException("Ratchetron closed the connection.");
                }
                read += n;
            }
        }

        private byte[] ReadExact(int count) {
            byte[] buffer = new byte[count];
            ReadExact(buffer, 0, count);
            return buffer;
        }

        private void WriteRaw(byte[] array, int offset, int count) {
            NetworkStream s = this.stream;
            if (s == null || !s.CanWrite) {
                throw new IOException("Not connected to Ratchetron.");
            }

            s.Write(array, offset, count);
        }

        private void WriteStream(byte[] array, int offset, int count) {
            lock (syncRoot) {
                try {
                    WriteRaw(array, offset, count);
                }
                catch (Exception ex) {
                    HandleTransportFailure(ex);
                    throw;
                }
            }
        }

        private void HandleTransportFailure(Exception ex) {
            if (ex is IOException || ex is SocketException || ex is ObjectDisposedException) {
                if (connected && Interlocked.Exchange(ref connectionLostRaised, 1) == 0) {
                    connected = false;
                    RaiseConnectionLost();
                }
            }
        }

        private void EnsureConnected() {
            if (!connected) {
                throw new Exception("I ain't connected");
            }
        }

        public override string getGameTitleID() {
            EnsureConnected();

            byte[] cmd = { 0x06 };

            lock (syncRoot) {
                try {
                    WriteRaw(cmd, 0, 1);
                    byte[] titleIdBuf = ReadExact(16);
                    return System.Text.Encoding.Default.GetString(titleIdBuf).Replace("\0", string.Empty);
                }
                catch (Exception ex) {
                    HandleTransportFailure(ex);
                    throw;
                }
            }
        }

        public int[] GetPIDList() {
            EnsureConnected();

            byte[] cmd = { 0x03 };

            lock (syncRoot) {
                try {
                    WriteRaw(cmd, 0, 1);

                    byte[] pidListBuf = ReadExact(64);

                    int[] pids = new int[16];

                    for (int i = 0; i < 64; i += 4) {
                        byte[] bytes = pidListBuf.Skip(i).Take(4).ToArray();

                        if (BitConverter.IsLittleEndian) {
                            Array.Reverse(bytes);
                        }

                        pids[i / 4] = BitConverter.ToInt32(bytes, 0);
                    }

                    return pids;
                }
                catch (Exception ex) {
                    HandleTransportFailure(ex);
                    throw;
                }
            }
        }

        public void EnableDebugMessages() {
            byte[] cmd = { 0x0d };

            WriteStream(cmd, 0, 1);
        }

        public override int getCurrentPID() {
            if (!SupportsModernCommands) {
                return this.GetPIDList()[2];
            }

            EnsureConnected();

            byte[] cmd = { 0x14 };

            lock (syncRoot) {
                try {
                    WriteRaw(cmd, 0, 1);
                    byte[] pidBuf = ReadExact(4);

                    if (BitConverter.IsLittleEndian) {
                        Array.Reverse(pidBuf);
                    }

                    int pid = BitConverter.ToInt32(pidBuf, 0);
                    if (pid != 0) {
                        this.currentPid = pid;
                    }
                    return pid;
                }
                catch (Exception ex) {
                    HandleTransportFailure(ex);
                    throw;
                }
            }
        }

        public override void WriteMemory(int pid, uint address, uint size, byte[] memory) {
            var cmdBuf = new List<byte>();
            cmdBuf.Add(0x05);
            cmdBuf.AddRange(BitConverter.GetBytes((UInt32)currentPid).Reverse());
            cmdBuf.AddRange(BitConverter.GetBytes((UInt32)address).Reverse());
            cmdBuf.AddRange(BitConverter.GetBytes((UInt32)size).Reverse());
            cmdBuf.AddRange(memory);

            this.WriteStream(cmdBuf.ToArray(), 0, cmdBuf.Count);
        }

        public override byte[] ReadMemory(int pid, uint address, uint size) {
            EnsureConnected();

            var cmdBuf = new List<byte>();
            cmdBuf.Add(0x04);
            cmdBuf.AddRange(BitConverter.GetBytes((UInt32)currentPid).Reverse());
            cmdBuf.AddRange(BitConverter.GetBytes((UInt32)address).Reverse());
            cmdBuf.AddRange(BitConverter.GetBytes((UInt32)size).Reverse());

#if DEBUG
            var watch = new System.Diagnostics.Stopwatch();

            watch.Start();
#endif

            lock (syncRoot) {
                try {
                    WriteRaw(cmdBuf.ToArray(), 0, cmdBuf.Count);

                    byte[] memory = ReadExact((int)size);

#if DEBUG
                    watch.Stop();

                    //Console.WriteLine($"Request for {size} bytes memory at {address.ToString("X")} took: {watch.ElapsedMilliseconds} ms");
#endif 
                    return memory;
                }
                catch (Exception ex) {
                    HandleTransportFailure(ex);
                    throw;
                }
            }
        }

        public override void Notify(string message) {
            var cmdBuf = new List<byte>();
            cmdBuf.Add(0x02);
            var payload = Encoding.ASCII.GetBytes(message);
            uint length = (uint)(payload.Length + 1);
            cmdBuf.AddRange(BitConverter.GetBytes(length).Reverse());
            cmdBuf.AddRange(payload);
            cmdBuf.Add(0x00); // null terminating character to avoid strings looking messed up

            this.WriteStream(cmdBuf.ToArray(), 0, cmdBuf.Count);

        }

        private void DataChannelReceive() {
            IPEndPoint end = new IPEndPoint(IPAddress.Any, 0);

            while (this.connected) {
                UdpClient udp = this.udpClient;
                if (udp == null) break;

                try {
                    byte[] cmdBuf = udp.Receive(ref end);
                    byte command = cmdBuf.Take(1).ToArray()[0];

                    switch (command) {
                        case 0x06: {
                                UInt32 memSubID = BitConverter.ToUInt32(cmdBuf.Skip(1).Take(4).Reverse().ToArray(), 0);
                                UInt32 size = BitConverter.ToUInt32(cmdBuf.Skip(5).Take(4).Reverse().ToArray(), 0);
                                uint tickUpdated = BitConverter.ToUInt32(cmdBuf.Skip(9).Take(4).Reverse().ToArray(), 0);
                                var value = cmdBuf.Skip(13).Take((int)size).Reverse().ToArray();
                                Action<byte[]> callback = null;

                                lock (memorySubsLock) {
                                    if (this.memSubTickUpdates.TryGetValue((int)memSubID, out uint previousTick) &&
                                        previousTick != tickUpdated &&
                                        this.memSubCallbacks.TryGetValue((int)memSubID, out callback)) {
                                        this.memSubTickUpdates[(int)memSubID] = tickUpdated;
                                    }
                                    else {
                                        callback = null;
                                    }
                                }

                                callback?.Invoke(value);
                                break;
                            }
                        // for opening/closing: 1 extra byte for coming in/out
                        case 0x08: {
                                byte enteringOrLeaving = cmdBuf.Skip(1).Take(1).ToArray()[0];
                                bool inGame = enteringOrLeaving != 0;
                                // debounce
                                if (inGame == lastInGameState) break;
                                lastInGameState = inGame;

                                Console.WriteLine($"Got new IS_INGAME: {enteringOrLeaving}");

                                if (inGame) {
                                    // refresh pid on separate thread.
                                    new Thread(RefreshCurrentPid) { IsBackground = true }.Start();
                                }
                                else {
                                    this.currentPid = 0;
                                }

                                RaiseInGameChanged(inGame);
                                break;
                            }

                    }
                }
                catch (SocketException) {
                    // Who gives a shit
                }
                catch (ObjectDisposedException) {
                    break;
                }
            }
        }


        private void RefreshCurrentPid() {
            Thread.Sleep(3000); // this sucks.
            for (int attempt = 0; attempt < 20; attempt++) {
                try {
                    int pid = getCurrentPID();
                    Console.WriteLine($"PID refresh attempt {attempt}: got {pid}");
                    if (pid != 0) {
                        this.currentPid = pid;
                        Console.WriteLine($"Refreshed current PID: {pid}");
                        return;
                    }
                }
                catch (Exception ex) {
                    Console.WriteLine($"PID refresh attempt {attempt} threw: {ex.GetType().Name}: {ex.Message}");
                }

                Thread.Sleep(100);
            }

            Console.WriteLine("I give up.");
        }

        public void OpenDataChannel() {
            byte[] data = new byte[1024];
            int port = 4000;
            bool udpStarted = false;
            while (!udpStarted) {
                try {
                    IPEndPoint ipep = new IPEndPoint(IPAddress.Any, port);
                    this.udpClient = new UdpClient(ipep);
                    udpStarted = true;
                }
                catch (SocketException) {
                    if (port++ > 5000) {
                        MessageBox.Show("Tried to open data connection on all ports between 4000 and 5000, but that failed. Did you deny RaCMAN firewall access?");
                        return;
                    }
                }
            }

            var assignedPort = ((IPEndPoint)this.udpClient.Client.LocalEndPoint).Port;

            var cmdBuf = new List<byte>();
            cmdBuf.Add(0x09);
            cmdBuf.AddRange(BitConverter.GetBytes((UInt32)assignedPort).Reverse());

            byte[] returnValue;
            lock (syncRoot) {
                try {
                    WriteRaw(cmdBuf.ToArray(), 0, cmdBuf.Count);
                    returnValue = ReadExact(1);
                }
                catch (Exception ex) {
                    HandleTransportFailure(ex);
                    udpClient?.Close();
                    udpClient = null;
                    throw;
                }
            }

            if (returnValue[0] == 128 || returnValue[0] == 0x01) {
                Console.WriteLine("Waiting for connection on port " + assignedPort);

                //this.udpClient.Send(new byte[] { 0x01 }, 1, remoteEndpoint);

                dataThread = new Thread(this.DataChannelReceive);
                dataThread.IsBackground = true;
                dataThread.Start();
            }
            else if (returnValue[0] == 2) {
                Console.WriteLine("Tried to open data channel, but server says we already have one open.");
                udpClient.Close();
                udpClient = null;
            }
            else {
                Console.WriteLine("Server error trying to open data channel.");
                udpClient.Close();
                udpClient = null;
            }
        }

        public override int SubMemory(int pid, uint address, uint size, MemoryCondition condition, byte[] memory, Action<byte[]> callback) {
            EnsureConnected();

            var cmdBuf = new List<byte>();
            cmdBuf.Add(0x0a);
            cmdBuf.AddRange(BitConverter.GetBytes((UInt32)currentPid).Reverse());
            cmdBuf.AddRange(BitConverter.GetBytes((UInt32)address).Reverse());
            cmdBuf.AddRange(BitConverter.GetBytes((UInt32)size).Reverse());
            cmdBuf.AddRange(new byte[] { (byte)condition });
            cmdBuf.AddRange(memory);

            int memSubID;
            lock (syncRoot) {
                try {
                    WriteRaw(cmdBuf.ToArray(), 0, cmdBuf.Count);

                    byte[] memSubIDBuf = ReadExact(4);
                    memSubID = (int)BitConverter.ToInt32(memSubIDBuf.Take(4).Reverse().ToArray(), 0);
                }
                catch (Exception ex) {
                    HandleTransportFailure(ex);
                    throw;
                }
            }

            lock (memorySubsLock) {
                if (!this.memorySubs.Contains(memSubID)) this.memorySubs.Add(memSubID);
                this.memSubCallbacks[memSubID] = callback;
                this.memSubTickUpdates[memSubID] = 0;
            }

            Console.WriteLine($"Subscribed to address {address.ToString("X")} with subscription ID {memSubID}");

            return memSubID;
        }

        public override int FreezeMemory(int pid, uint address, uint size, MemoryCondition condition, byte[] memory) {
            EnsureConnected();

            var cmdBuf = new List<byte>();
            cmdBuf.Add(0x0b);
            cmdBuf.AddRange(BitConverter.GetBytes((UInt32)currentPid).Reverse());
            cmdBuf.AddRange(BitConverter.GetBytes((UInt32)address).Reverse());
            cmdBuf.AddRange(BitConverter.GetBytes((UInt32)size).Reverse());
            cmdBuf.AddRange(new byte[] { (byte)condition });
            cmdBuf.AddRange(memory);

            int memSubID;
            lock (syncRoot) {
                try {
                    WriteRaw(cmdBuf.ToArray(), 0, cmdBuf.Count);

                    byte[] memSubIDBuf = ReadExact(4);
                    memSubID = (int)BitConverter.ToInt32(memSubIDBuf.Take(4).Reverse().ToArray(), 0);
                }
                catch (Exception ex) {
                    HandleTransportFailure(ex);
                    throw;
                }
            }

            Console.WriteLine($"Froze address {address.ToString("X")} with subscription ID {memSubID}");

            lock (memorySubsLock) {
                if (!this.memorySubs.Contains(memSubID)) this.memorySubs.Add(memSubID);
                frozenAddresses[memSubID] = address;
            }

            return memSubID;
        }

        public override void ReleaseAllSubs() {
            int[] allSubsCopy;
            lock (memorySubsLock) {
                allSubsCopy = this.memorySubs.ToArray();
            }

            if (allSubsCopy.Length == 0) {
                return;
            }

            if (SupportsModernCommands && connected) {
                try {
                    byte[] cmd = { 0x15 };

                    lock (syncRoot) {
                        WriteRaw(cmd, 0, 1);
                        ReadExact(1);
                    }

                    lock (memorySubsLock) {
                        this.memorySubs.Clear();
                        this.memSubCallbacks.Clear();
                        this.memSubTickUpdates.Clear();
                        this.frozenAddresses.Clear();
                    }

                    Console.WriteLine($"Released all {allSubsCopy.Length} memory subscriptions.");
                    return;
                }
                catch (Exception ex) {
                    HandleTransportFailure(ex);
                }
            }

            foreach (var sub in allSubsCopy) {
                try {
                    this.ReleaseSubID(sub);
                }
                catch (Exception) {
                    // who cares about error handling anyway?
                }
            }

            lock (memorySubsLock) {
                this.memorySubs.Clear();
                this.memSubCallbacks.Clear();
                this.memSubTickUpdates.Clear();
                this.frozenAddresses.Clear();
            }
        }

        public override void ReleaseSubID(int memSubID) {
            lock (memorySubsLock) {
                this.memSubCallbacks.Remove(memSubID);
                this.memSubTickUpdates.Remove(memSubID);
                this.frozenAddresses.Remove(memSubID);
                this.memorySubs.Remove(memSubID);
            }

            if (!connected) {
                return;
            }

            var cmdBuf = new List<byte>();
            cmdBuf.Add(0x0c);
            cmdBuf.AddRange(BitConverter.GetBytes((UInt32)memSubID).Reverse());

            lock (syncRoot) {
                try {
                    WriteRaw(cmdBuf.ToArray(), 0, cmdBuf.Count);
                    ReadExact(1);
                }
                catch (Exception ex) {
                    HandleTransportFailure(ex);
                    throw;
                }
            }

            Console.WriteLine($"Released memory subscription ID {memSubID}");

            // we're ignoring the results because yolo
        }


        public int OpenFile(string remotePath) {
            EnsureConnected();

            var cmdBuf = new List<byte> {
                0x10,  // Open file command
                0, 0, 0, 0     // Flags (unused)
            };

            cmdBuf.AddRange(BitConverter.GetBytes((UInt32)remotePath.Length + 1).Reverse());
            cmdBuf.AddRange(Encoding.ASCII.GetBytes(remotePath));
            cmdBuf.Add(0x0);

            lock (syncRoot) {
                try {
                    WriteRaw(cmdBuf.ToArray(), 0, cmdBuf.Count);

                    byte[] fileHandleBuf = ReadExact(4);
                    return BitConverter.ToInt32(fileHandleBuf, 0);
                }
                catch (Exception ex) {
                    HandleTransportFailure(ex);
                    throw;
                }
            }
        }

        public override void WriteFile(string remotePath, byte[] buffer) {
            // Open file
            int fileHandle = OpenFile(remotePath);

            var cmdBuf = new List<byte> {
                    0x11,  // Write file command
                };

            cmdBuf.AddRange(BitConverter.GetBytes(fileHandle));
            cmdBuf.AddRange(BitConverter.GetBytes(buffer.Length).Reverse());

            // Close file by sending a write command with 0 file size
            var closeCmdBuf = new List<byte> {
                0x11,  // Write file command
            };

            closeCmdBuf.AddRange(BitConverter.GetBytes(fileHandle));
            closeCmdBuf.AddRange(BitConverter.GetBytes(0)); // 0 size to indicate end of file

            lock (syncRoot) {
                try {
                    WriteRaw(cmdBuf.ToArray(), 0, cmdBuf.Count);

                    // Split up into 2048 byte chunks
                    for (int i = 0; i < buffer.Length; i += 2048) {
                        int chunkSize = Math.Min(2048, buffer.Length - i);

                        WriteRaw(buffer, i, chunkSize);
                    }

                    WriteRaw(closeCmdBuf.ToArray(), 0, closeCmdBuf.Count);
                }
                catch (Exception ex) {
                    HandleTransportFailure(ex);
                    throw;
                }
            }
        }

        public override void WriteFile(string remotePath, string filePath) {
            if (!File.Exists(filePath)) {
                throw new FileNotFoundException("The specified file does not exist.", filePath);
            }

            var file = File.OpenRead(filePath);

            var buffer = new byte[file.Length];
            file.Read(buffer, 0, (int)file.Length);
            file.Close();

            WriteFile(remotePath, buffer);
        }


        // Doesn't work, sorry.
        public uint AllocatePage(int pid, uint size, uint flags, bool is_executable) {
            EnsureConnected();

            var cmdBuf = new List<byte>();
            cmdBuf.Add(0x0e);
            cmdBuf.AddRange(BitConverter.GetBytes((UInt32)pid).Reverse());
            cmdBuf.AddRange(BitConverter.GetBytes((UInt32)size).Reverse());
            cmdBuf.AddRange(BitConverter.GetBytes((UInt32)flags).Reverse());
            cmdBuf.AddRange(BitConverter.GetBytes((UInt32)(is_executable ? 1 : 0)).Reverse());

            lock (syncRoot) {
                try {
                    WriteRaw(cmdBuf.ToArray(), 0, cmdBuf.Count);

                    byte[] address = ReadExact(8);
                    return (uint)BitConverter.ToUInt32(address.Take(4).Reverse().ToArray(), 0);
                }
                catch (Exception ex) {
                    HandleTransportFailure(ex);
                    throw;
                }
            }
        }

        public override uint GetUserID() {
            EnsureConnected();

            byte[] cmd = { 0x12 };

            lock (syncRoot) {
                try {
                    WriteRaw(cmd, 0, 1);

                    byte[] userIdBuf = ReadExact(4);
                    return BitConverter.ToUInt32(userIdBuf.Reverse().ToArray(), 0);
                }
                catch (Exception ex) {
                    HandleTransportFailure(ex);
                    throw;
                }
            }
        }
        public override int DeleteDirectory(string remotePath) {
            EnsureConnected();

            var cmdBuf = new List<byte> { 0x13 };
            cmdBuf.AddRange(BitConverter.GetBytes((UInt32)remotePath.Length + 1).Reverse());
            cmdBuf.AddRange(Encoding.ASCII.GetBytes(remotePath));
            cmdBuf.Add(0x0);

            lock (syncRoot) {
                try {
                    WriteRaw(cmdBuf.ToArray(), 0, cmdBuf.Count);

                    byte[] resultBuf = ReadExact(4);
                    return BitConverter.ToInt32(resultBuf.Reverse().ToArray(), 0);
                }
                catch (Exception ex) {
                    HandleTransportFailure(ex);
                    throw;
                }
            }
        }
    }
}