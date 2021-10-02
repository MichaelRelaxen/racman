using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace racman
{
    class RPCS3: IPS3API
    {
        [DllImport("kernel32.dll")]
        private static extern ulong OpenProcess(int desiredAccess, bool inheritHandle, int processId);
        [DllImport("kernel32.dll")]
        private static extern bool CloseHandle(ulong handle);
        [DllImport("kernel32.dll")]
        private static extern bool WriteProcessMemory(ulong process, ulong baseAddress, byte[] buffer, int size, ref int numberOfBytesRead);
        [DllImport("kernel32.dll")]
        private static extern bool ReadProcessMemory(ulong process, ulong baseAddress, byte[] buffer, int size, ref int numberOfBytesRead);

        public RPCS3(string ip) : base(ip)
        {

        }


        public override string getGameTitleID()
        {
            // ReadMemory(0, 0x4740700, 16);

            return "NPEA00385";
        }

        public override int getCurrentPID()
        {
            return 0; //lmao
        }

        public override void WriteMemory(int pid, uint address, uint size, byte[] memory)
        {
            Process rpcs3 = Process.GetProcessesByName("rpcs3")[0];
            if (rpcs3 != null)
            {
                int readAmount = 0;
                ulong handle = OpenProcess(0x0038, false, rpcs3.Id);
                WriteProcessMemory(handle, 0x300000000ul + address, memory, memory.Length, ref readAmount);
                CloseHandle(handle);
            }
        }

        public override byte[] ReadMemory(int pid, uint address, uint size)
        {
            byte[] result = new byte[size];

            Process rpcs3 = Process.GetProcessesByName("rpcs3")[0];
            if (rpcs3 != null)
            {
                int readAmount = 0;
                ulong handle = OpenProcess(0x0038, false, rpcs3.Id);
                ReadProcessMemory(handle, 0x300000000ul + address, result, (int)size, ref readAmount);
                CloseHandle(handle);
            }
            return result;
        }

        public override bool Connect()
        {
            return true;  // fuck you I won't do what you tell me
        }

        public override void Notify(string message)
        {
            // Can't be bothered to implement this for webman
        }

        public override bool Disconnect()
        {
            return true;
        }
    }
}
