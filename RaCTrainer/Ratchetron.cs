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

namespace racman
{
    class Ratchetron: IPS3API
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
        private Dictionary<int, UInt32> frozenAddresses = new Dictionary<int, uint>();


        public Ratchetron(string ip): base(ip)
        {
            this.ip = ip;
        }

        public override bool Connect()
        {
            try
            {
                this.client = new TcpClient(this.ip, this.port);

                this.stream = client.GetStream();

                byte[] connMsg = new byte[6];
                stream.Read(connMsg, 0, 6);

                if (connMsg[0] == 0x01)
                {
                    this.remoteEndpoint = new IPEndPoint(IPAddress.Parse(this.ip), 0);
                    //this.remoteEndpoint = new IPEndPoint(IPAddress.Parse("10.9.0.8"), 0);

                    this.connected = true;
                    return true;
                }
            } catch (SocketException e)
            {
                return false;
            } catch (Exception e)
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

            stream.Write(cmd, 0, 1);

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

            stream.Write(cmd, 0, 1);

            byte[] pidListBuf = new byte[64];

            int n_bytes = 0;
            while (n_bytes < 64) {
                n_bytes += stream.Read(pidListBuf, 0, 64);
            }

            int[] pids = new int[16];

            for (int i = 0; i < 64; i+=4)
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

        public override int getCurrentPID()
        {
            return this.GetPIDList()[2];
        }

        public override void WriteMemory(int pid, uint address, uint size, byte[] memory)
        {
            var cmdBuf = new List<byte>();
            cmdBuf.Add(0x05);
            cmdBuf.AddRange(BitConverter.GetBytes((UInt32)pid).Reverse());
            cmdBuf.AddRange(BitConverter.GetBytes((UInt32)address).Reverse());
            cmdBuf.AddRange(BitConverter.GetBytes((UInt32)size).Reverse());
            cmdBuf.AddRange(memory);

            this.stream.Write(cmdBuf.ToArray(), 0, cmdBuf.Count);
        }

        public override byte[] ReadMemory(int pid, uint address, uint size)
        {
            var cmdBuf = new List<byte>();
            cmdBuf.Add(0x04);
            cmdBuf.AddRange(BitConverter.GetBytes((UInt32)pid).Reverse());
            cmdBuf.AddRange(BitConverter.GetBytes((UInt32)address).Reverse());
            cmdBuf.AddRange(BitConverter.GetBytes((UInt32)size).Reverse());

            this.stream.Write(cmdBuf.ToArray(), 0, cmdBuf.Count);

            byte[] memory = new byte[2048];

            int n_bytes = 0;
            while (n_bytes < size)
            {
                n_bytes += stream.Read(memory, 0, (int)size);
            }

            return memory.Take((int)size).ToArray();
        }

        public override void Notify(string message)
        {
            var cmdBuf = new List<byte>();
            cmdBuf.Add(0x02);
            cmdBuf.AddRange(BitConverter.GetBytes((UInt32)message.Length).Reverse());
            cmdBuf.AddRange(Encoding.ASCII.GetBytes(message));

            this.stream.Write(cmdBuf.ToArray(), 0, cmdBuf.Count);
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
                                var value = cmdBuf.Skip(9).Take((int)size).Reverse().ToArray();

                                this.memSubCallbacks[(int)memSubID](value);

                                break;
                            }
                    }
                } catch (SocketException e)
                {
                    // Who gives a shit
                }
            }
        }

        public void OpenDataChannel()
        {
            byte[] data = new byte[1024];
            IPEndPoint ipep = new IPEndPoint(IPAddress.Any, 0);
            this.udpClient = new UdpClient(ipep);

            var assignedPort = ((IPEndPoint)this.udpClient.Client.LocalEndPoint).Port;
            
            var cmdBuf = new List<byte>();
            cmdBuf.Add(0x09);
            cmdBuf.AddRange(BitConverter.GetBytes((UInt32)assignedPort).Reverse());

            this.stream.Write(cmdBuf.ToArray(), 0, cmdBuf.Count);

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

        public int SubMemory(int pid, uint address, uint size, Action<byte[]> callback)
        {
            return SubMemory(pid, address, size, new byte[size], callback);
        }

        public int SubMemory(int pid, uint address, uint size, byte[] memory, Action<byte[]> callback)
        {
            var cmdBuf = new List<byte>();
            cmdBuf.Add(0x0a);
            cmdBuf.AddRange(BitConverter.GetBytes((UInt32)pid).Reverse());
            cmdBuf.AddRange(BitConverter.GetBytes((UInt32)address).Reverse());
            cmdBuf.AddRange(BitConverter.GetBytes((UInt32)size).Reverse());
            cmdBuf.AddRange(new byte[] { 0x01 });
            cmdBuf.AddRange(memory);

            this.stream.Write(cmdBuf.ToArray(), 0, cmdBuf.Count);

            byte[] memSubIDBuf = new byte[4];

            int n_bytes = 0;
            while (n_bytes < 4)
            {
                n_bytes += stream.Read(memSubIDBuf, 0, 4);
            }

            var memSubID = (int)BitConverter.ToInt32(memSubIDBuf.Take(4).Reverse().ToArray(), 0);

            this.memSubCallbacks[memSubID] = callback;

            return memSubID;
        }

        public int FreezeMemory(int pid, uint address, uint size, byte[] memory)
        {
            var cmdBuf = new List<byte>();
            cmdBuf.Add(0x0b);
            cmdBuf.AddRange(BitConverter.GetBytes((UInt32)pid).Reverse());
            cmdBuf.AddRange(BitConverter.GetBytes((UInt32)address).Reverse());
            cmdBuf.AddRange(BitConverter.GetBytes((UInt32)size).Reverse());
            cmdBuf.AddRange(new byte[] { 0x01 });
            cmdBuf.AddRange(memory);

            this.stream.Write(cmdBuf.ToArray(), 0, cmdBuf.Count);

            byte[] memSubIDBuf = new byte[4];

            int n_bytes = 0;
            while (n_bytes < 4)
            {
                n_bytes += stream.Read(memSubIDBuf, 0, 4);
            }

            var memSubID = (int)BitConverter.ToInt32(memSubIDBuf.Take(4).Reverse().ToArray(), 0);

            frozenAddresses[memSubID] = address;

            return memSubID;
        }

        public virtual void FreezeMemory(int pid, uint address, UInt32 intValue)
        {
            this.FreezeMemory(pid, address, 4, BitConverter.GetBytes((UInt32)intValue).Reverse().ToArray());
        }

        public void ReleaseSubID(int memSubID)
        {
            var cmdBuf = new List<byte>();
            cmdBuf.Add(0x0c);
            cmdBuf.AddRange(BitConverter.GetBytes((UInt32)memSubID).Reverse());

            this.stream.Write(cmdBuf.ToArray(), 0, cmdBuf.Count);

            byte[] resultBuf = new byte[1];

            int n_bytes = 0;
            while (n_bytes < 1)
            {
                n_bytes += stream.Read(resultBuf, 0, 1);
            }

            // we're ignoring the results because yolo
        }

        public int MemSubIDForAddress(uint address)
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
    }
}
