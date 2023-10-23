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
            this.WriteMemory(pid, address, 4, BitConverter.GetBytes((UInt32)intValue).Reverse().ToArray());
        }
        public virtual void WriteMemory(int pid, uint address, uint size, string memory)
        {
            byte[] mem = Enumerable.Range(0, memory.Length)
                     .Where(x => x % 2 == 0)
                     .Select(x => Convert.ToByte(memory.Substring(x, 2), 16))
                     .ToArray();

            WriteMemory(pid, address, size, mem);
        }

        public void WriteMemory(int pid, uint address, byte[] memory)
        {
            this.WriteMemory(pid, address, (uint)memory.Length, memory);
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

        /// <summary>
        /// Any blasts the data channel with values all the time
        /// Changed only sends data when the value changes
        /// The other things do other things thanks for reading my Ted talk
        /// </summary>
        public enum MemoryCondition : byte
        {
            Any = 1,
            Changed = 2,
            Above = 3,
            Below = 4,
            Equal = 5,  // equal and not equal are not really useful for freezing
            NotEqual = 6
        }

        public abstract int SubMemory(int pid, uint address, uint size, MemoryCondition condition, byte[] memory, Action<byte[]> callback);

        // Defaults to changed because why blast yourself with data?
        public int SubMemory(int pid, uint address, uint size, Action<byte[]> callback)
        {
            return SubMemory(pid, address, size, MemoryCondition.Changed, new byte[size], callback);
        }

        public int SubMemory(int pid, uint address, uint size, MemoryCondition condition, Action<byte[]> callback)
        {
            return SubMemory(pid, address, size, condition, new byte[size], callback);
        }

        public abstract int FreezeMemory(int pid, uint address, uint size, MemoryCondition condition, byte[] memory);

        public virtual int FreezeMemory(int pid, uint address, MemoryCondition condition, UInt32 intValue)
        {
            return this.FreezeMemory(pid, address, 4, condition, BitConverter.GetBytes((UInt32)intValue).Reverse().ToArray());
        }

        public virtual int FreezeMemory(int pid, uint address, UInt32 intValue)
        {
            return this.FreezeMemory(pid, address, 4, MemoryCondition.Any, BitConverter.GetBytes((UInt32)intValue).Reverse().ToArray());
        }

        public abstract void ReleaseSubID(int memSubID);

        public abstract int MemSubIDForAddress(uint address);
    }
}
