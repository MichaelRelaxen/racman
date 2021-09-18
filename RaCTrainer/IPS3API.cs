using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace racman
{
    public abstract class IPS3API
    {
        string ip
        {
            get;
            set;
        }

        protected IPS3API(string ip)
        {
            this.ip = ip;
        }

        public abstract bool Connect();
        public abstract bool Disconnect();

        public abstract string getGameTitleID();
        public abstract int getCurrentPID();
        public abstract void WriteMemory(int pid, uint address, uint size, byte[] memory);
        public virtual void WriteMemory(int pid, uint address, UInt32 intValue)
        {
            this.WriteMemory(pid, address, 4, BitConverter.GetBytes((UInt32)pid).Reverse().ToArray());
        }
        public virtual void WriteMemory(int pid, uint address, uint size, string memory)
        {
            byte[] mem = Enumerable.Range(0, memory.Length)
                     .Where(x => x % 2 == 0)
                     .Select(x => Convert.ToByte(memory.Substring(x, 2), 16))
                     .ToArray();

            WriteMemory(pid, address, size, mem);
        }

        public abstract byte[] ReadMemory(int pid, uint address, uint size);
        public virtual string ReadMemoryStr(int pid, uint address, uint size)
        {
            byte[] memory = ReadMemory(pid, address, size);
            
            StringBuilder hex = new StringBuilder(memory.Length * 2);
            foreach (byte b in memory)
                hex.AppendFormat("{0:x2}", b);

            return hex.ToString();
        }

        public abstract void Notify(string message);

        public virtual string GetIP()
        {
            return this.ip;
        }
    }
}
