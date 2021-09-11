using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

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
        private NetworkStream stream;
        private bool connected = false;


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
    }
}
