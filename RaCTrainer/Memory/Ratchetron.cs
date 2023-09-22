using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace racman
{
    public class Ratchetron : IPS3API
    {
        string ip
        {
            get;
            set;
        }

        private int port = 9671;

        private TcpClient client;
        private UdpClient udpClient;
        private NetworkStream stream;
        private bool connected = false;

        private IPEndPoint remoteEndpoint;

        private Dictionary<int, Action<byte[]>> memSubCallbacks = new Dictionary<int, Action<byte[]>>();
        private Dictionary<int, uint> memSubTickUpdates = new Dictionary<int, uint>();
        private Dictionary<int, UInt32> frozenAddresses = new Dictionary<int, uint>();


        public Ratchetron(string ip) : base(ip)
        {
            this.ip = ip;
        }

        public override bool Connect()
        {
            try
            {
                this.client = new TcpClient(this.ip, this.port);
                this.client.NoDelay = true;

                this.stream = client.GetStream();

                byte[] connMsg = new byte[6];
                stream.Read(connMsg, 0, 6);

                uint apiRev = BitConverter.ToUInt32(connMsg.Skip(2).Take(4).Reverse().ToArray(), 0);

                if (apiRev < 2)
                {
                    MessageBox.Show("The Ratchetron module loaded on your PS3 is too old, you need to restart your PS3 to load the new version.");
                    return false;
                }

                if (connMsg[0] == 0x01)
                {
                    this.remoteEndpoint = new IPEndPoint(IPAddress.Parse(this.ip), 0);

                    this.connected = true;

#if DEBUG
                    this.EnableDebugMessages();
#endif

                    return true;
                }
            } catch (SocketException)
            {
                return false;
            } catch (Exception)
            {
                // who cares about error handling anyway?
                return false;
            }

            return false;
        }

        public override bool Disconnect()
        {
            this.connected = false;
            this.udpClient.Close();
            this.client.Close();

            return true;
        }

        public override string getGameTitleID()
        {
            if (!connected)
            {
                throw new Exception("I ain't connected");
            }

            byte[] cmd = { 0x06 };

            WriteStream(cmd, 0, 1);

            byte[] titleIdBuf = new byte[16];
            stream.Read(titleIdBuf, 0, 16);

            return System.Text.Encoding.Default.GetString(titleIdBuf).Replace("\0", string.Empty);
        }

        public int[] GetPIDList()
        {
            if (!connected)
            {
                throw new Exception("I ain't connected");
            }

            byte[] cmd = { 0x03 };

            WriteStream(cmd, 0, 1);

            byte[] pidListBuf = new byte[64];

            int n_bytes = 0;
            while (n_bytes < 64) {
                n_bytes += stream.Read(pidListBuf, 0, 64);
            }

            int[] pids = new int[16];

            for (int i = 0; i < 64; i += 4)
            {
                byte[] bytes = pidListBuf.Skip(i).Take(4).ToArray();

                if (BitConverter.IsLittleEndian)
                {
                    Array.Reverse(bytes);
                }

                pids[i / 4] = BitConverter.ToInt32(bytes, 0);
            }

            return pids;
        }

        public void EnableDebugMessages()
        {
            byte[] cmd = { 0x0d };

            WriteStream(cmd, 0, 1);
        }

        public override int getCurrentPID()
        {
            return this.GetPIDList()[2];
        }

        private static Mutex writeLock = new Mutex();
        private void WriteStream(byte[] array, int offset, int count)
        {
            writeLock.WaitOne();

            if (this.stream.CanWrite)
            {
                this.stream.Write(array, offset, count);
            }

            writeLock.ReleaseMutex();
        }
        
        public override void WriteMemory(int pid, uint address, uint size, byte[] memory)
        {
            var cmdBuf = new List<byte>();
            cmdBuf.Add(0x05);
            cmdBuf.AddRange(BitConverter.GetBytes((UInt32)pid).Reverse());
            cmdBuf.AddRange(BitConverter.GetBytes((UInt32)address).Reverse());
            cmdBuf.AddRange(BitConverter.GetBytes((UInt32)size).Reverse());
            cmdBuf.AddRange(memory);


            this.WriteStream(cmdBuf.ToArray(), 0, cmdBuf.Count);
        }

        public override byte[] ReadMemory(int pid, uint address, uint size)
        {
            var cmdBuf = new List<byte>();
            cmdBuf.Add(0x04);
            cmdBuf.AddRange(BitConverter.GetBytes((UInt32)pid).Reverse());
            cmdBuf.AddRange(BitConverter.GetBytes((UInt32)address).Reverse());
            cmdBuf.AddRange(BitConverter.GetBytes((UInt32)size).Reverse());

#if DEBUG
            var watch = new System.Diagnostics.Stopwatch();

            watch.Start();
#endif

            this.WriteStream(cmdBuf.ToArray(), 0, cmdBuf.Count);

            byte[] memory = new byte[size];

            int n_bytes = 0;
            while (n_bytes < size)
            {
                n_bytes += stream.Read(memory, 0, (int)size);
            }

#if DEBUG
            watch.Stop();

            //Console.WriteLine($"Request for {size} bytes memory at {address.ToString("X")} took: {watch.ElapsedMilliseconds} ms");
#endif 
            return memory.Take((int)size).ToArray();
        }

        public override void Notify(string message)
        {
            var cmdBuf = new List<byte>();
            cmdBuf.Add(0x02);
            cmdBuf.AddRange(BitConverter.GetBytes((UInt32)message.Length).Reverse());
            cmdBuf.AddRange(Encoding.ASCII.GetBytes(message));

            this.WriteStream(cmdBuf.ToArray(), 0, cmdBuf.Count);
        }

        private void DataChannelReceive()
        {
            IPEndPoint end = new IPEndPoint(IPAddress.Any, 0);

            while (this.connected)
            {
                try
                {
                    byte[] cmdBuf = this.udpClient.Receive(ref end);
                    byte command = cmdBuf.Take(1).ToArray()[0];

                    switch (command)
                    {
                        case 0x06:
                            {
                                UInt32 memSubID = BitConverter.ToUInt32(cmdBuf.Skip(1).Take(4).Reverse().ToArray(), 0);
                                UInt32 size = BitConverter.ToUInt32(cmdBuf.Skip(5).Take(4).Reverse().ToArray(), 0);
                                uint tickUpdated = BitConverter.ToUInt32(cmdBuf.Skip(9).Take(4).Reverse().ToArray(), 0);
                                var value = cmdBuf.Skip(13).Take((int)size).Reverse().ToArray();

                                if (this.memSubTickUpdates.ContainsKey((int)memSubID) && this.memSubTickUpdates[(int)memSubID] != tickUpdated)
                                {
                                    this.memSubTickUpdates[(int)memSubID] = tickUpdated;
                                    this.memSubCallbacks[(int)memSubID](value);
                                }

                                break;
                            }
                    }
                } catch (SocketException)
                {
                    // Who gives a shit
                }
            }
        }

        public void OpenDataChannel()
        {
            byte[] data = new byte[1024];
            int port = 4000;
            bool udpStarted = false;
            while (!udpStarted)
            {
                try
                {
                    IPEndPoint ipep = new IPEndPoint(IPAddress.Any, port);
                    this.udpClient = new UdpClient(ipep);
                    udpStarted = true;
                }
                catch (SocketException)
                {
                    if (port++ > 5000)
                    {
                        MessageBox.Show("Tried to open data connection on all ports between 4000 and 5000, but that failed. Did you deny RaCMAN firewall access?");
                        return;
                    }
                }
            }

            var assignedPort = ((IPEndPoint)this.udpClient.Client.LocalEndPoint).Port;
            
            var cmdBuf = new List<byte>();
            cmdBuf.Add(0x09);
            cmdBuf.AddRange(BitConverter.GetBytes((UInt32)assignedPort).Reverse());

            this.WriteStream(cmdBuf.ToArray(), 0, cmdBuf.Count);

            byte[] returnValue = new byte[1];

            int n_bytes = 0;
            while (n_bytes < 1)
            {
                n_bytes += stream.Read(returnValue, 0, 1);
            }

            if (returnValue[0] == 128) { 
                Console.WriteLine("Waiting for connection on port " + assignedPort);

                //this.udpClient.Send(new byte[] { 0x01 }, 1, remoteEndpoint);

                Thread dataThread = new Thread(this.DataChannelReceive);
                dataThread.Start();
            } else if (returnValue[0] == 2)
            {
                Console.WriteLine("Tried to open data channel, but server says we already have one open.");
                udpClient.Close();
            } else
            {
                Console.WriteLine("Server error trying to open data channel.");
                udpClient.Close();
            }
        }

        public override int SubMemory(int pid, uint address, uint size, MemoryCondition condition, byte[] memory, Action<byte[]> callback)
        {
            var cmdBuf = new List<byte>();
            cmdBuf.Add(0x0a);
            cmdBuf.AddRange(BitConverter.GetBytes((UInt32)pid).Reverse());
            cmdBuf.AddRange(BitConverter.GetBytes((UInt32)address).Reverse());
            cmdBuf.AddRange(BitConverter.GetBytes((UInt32)size).Reverse());
            cmdBuf.AddRange(new byte[] { (byte)condition });
            cmdBuf.AddRange(memory);

            this.WriteStream(cmdBuf.ToArray(), 0, cmdBuf.Count);

            byte[] memSubIDBuf = new byte[4];

            int n_bytes = 0;
            while (n_bytes < 4)
            {
                n_bytes += stream.Read(memSubIDBuf, 0, 4);
            }

            var memSubID = (int)BitConverter.ToInt32(memSubIDBuf.Take(4).Reverse().ToArray(), 0);

            this.memSubCallbacks[memSubID] = callback;
            this.memSubTickUpdates[memSubID] = 0;

            Console.WriteLine($"Subscribed to address {address.ToString("X")} with subscription ID {memSubID}");

            return memSubID;
        }

        public override int FreezeMemory(int pid, uint address, uint size, MemoryCondition condition, byte[] memory)
        {
            var cmdBuf = new List<byte>();
            cmdBuf.Add(0x0b);
            cmdBuf.AddRange(BitConverter.GetBytes((UInt32)pid).Reverse());
            cmdBuf.AddRange(BitConverter.GetBytes((UInt32)address).Reverse());
            cmdBuf.AddRange(BitConverter.GetBytes((UInt32)size).Reverse());
            cmdBuf.AddRange(new byte[] { (byte)condition });
            cmdBuf.AddRange(memory);

            this.WriteStream(cmdBuf.ToArray(), 0, cmdBuf.Count);

            byte[] memSubIDBuf = new byte[4];

            int n_bytes = 0;
            while (n_bytes < 4)
            {
                n_bytes += stream.Read(memSubIDBuf, 0, 4);
            }

            var memSubID = (int)BitConverter.ToInt32(memSubIDBuf.Take(4).Reverse().ToArray(), 0);

            Console.WriteLine($"Froze address {address.ToString("X")} with subscription ID {memSubID}");

            frozenAddresses[memSubID] = address;

            return memSubID;
        }

        public override void ReleaseSubID(int memSubID)
        {   
            var cmdBuf = new List<byte>();
            cmdBuf.Add(0x0c);
            cmdBuf.AddRange(BitConverter.GetBytes((UInt32)memSubID).Reverse());

            this.WriteStream(cmdBuf.ToArray(), 0, cmdBuf.Count);

            byte[] resultBuf = new byte[1];

            int n_bytes = 0;
            while (n_bytes < 1)
            {
                n_bytes += stream.Read(resultBuf, 0, 1);
            }

            this.memSubCallbacks.Remove(memSubID);
            this.memSubTickUpdates.Remove(memSubID);
            this.frozenAddresses.Remove(memSubID);

            Console.WriteLine($"Released memory subscription ID {memSubID}");

            // we're ignoring the results because yolo
        }

        public override int MemSubIDForAddress(uint address)
        {
            foreach(KeyValuePair<int, uint> entry in frozenAddresses)
            {
                if (address == entry.Value)
                {
                    return entry.Key;
                }
            }
            return -1;
        }

        // Doesn't work, sorry.
        public uint AllocatePage(int pid, uint size, uint flags, bool is_executable)
        {
            var cmdBuf = new List<byte>();
            cmdBuf.Add(0x0e);
            cmdBuf.AddRange(BitConverter.GetBytes((UInt32)pid).Reverse());
            cmdBuf.AddRange(BitConverter.GetBytes((UInt32)size).Reverse());
            cmdBuf.AddRange(BitConverter.GetBytes((UInt32)flags).Reverse());
            cmdBuf.AddRange(BitConverter.GetBytes((UInt32)(is_executable ? 1 : 0)).Reverse());

            this.WriteStream(cmdBuf.ToArray(), 0, cmdBuf.Count);

            byte[] address = new byte[8];

            int n_bytes = 0;
            while (n_bytes < 8)
            {
                n_bytes += stream.Read(address, 0, 8);
            }

            return (uint)BitConverter.ToUInt32(address.Take(4).Reverse().ToArray(), 0); ;
        }
    }
}
