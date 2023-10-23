using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace racman
{
    class WebMAN: IPS3API
    {
        private WebClient client = new WebClient();

        string ip
        {
            get;
            set;
        }

        public WebMAN(string ip) : base(ip)
        {
            this.ip = ip;
        }

        private string get_data(string url)
        {
            string x = null;
            try
            {
                x = client.DownloadString(url);
            }
            catch
            {
            }
            return x;
        }



        public override string getGameTitleID()
        {
            string Output = get_data($"http://{ip}/cpursx.ps3?/sman.ps3");
            int gamePos = Output.IndexOf("target=\"_blank\">") + 16;
            return Output.Substring(gamePos, 9);
        }

        public override int getCurrentPID()
        {
            string Output = get_data($"http://{ip}/getmem.ps3mapi?");
            int ebootPos = Output.IndexOf("_main_EBOOT.BIN");
            if (ebootPos != -1)
            {
                string processHex = Output.Substring(ebootPos - 8, 8);
                int processDec = Convert.ToInt32(processHex, 16);
                return processDec;
            }
            else
            {
                return 0;
            }
        }

        public override void WriteMemory(int pid, uint address, uint size, byte[] memory)
        {
            string addr = Convert.ToString(address, 16);

            StringBuilder hex = new StringBuilder(memory.Length * 2);
            foreach (byte b in memory)
                hex.AppendFormat("{0:x2}", b);
          
            //string val = BitConverter.ToString(memory).Replace("-", string.Empty);
            get_data($"http://{ip}/setmem.ps3mapi?proc={pid}$addr={addr}&val={hex.ToString()}");
        }

        public override void WriteMemory(int pid, uint address, uint size, string memory)
        {
            string addr = Convert.ToString(address, 16);

            get_data($"http://{ip}/setmem.ps3mapi?proc={pid}$addr={addr}&val={memory}");
        }

        public override byte[] ReadMemory(int pid, uint address, uint size)
        {
            string addr = Convert.ToString(address, 16);
            string Output = get_data($"http://{ip}/getmem.ps3mapi?proc={pid}$addr={addr}&len={size}");
            int resPos = Output.IndexOf("</textarea>");

            return Enumerable.Range(0, Output.Length)
                     .Where(x => x % 2 == 0)
                     .Select(x => Convert.ToByte(Output.Substring(x, 2), 16))
                     .ToArray();
        }

        public override string ReadMemoryStr(int pid, uint address, uint size)
        {
            string addr = Convert.ToString(address, 16);
            string Output = get_data($"http://{ip}/getmem.ps3mapi?proc={pid}$addr={addr}&len={size}");
            int resPos = Output.IndexOf("</textarea>");

            string resultstring = Output.Substring(resPos - (int)size * 2, (int)size * 2);
            return resultstring;
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

        public void PauseRSX()
        {
            get_data($"http://{ip}/xmb.ps3$rsx_pause");
        }

        public void ContinueRSX()
        {
            get_data($"http://{ip}/xmb.ps3$rsx_continue");
        }

        public override int SubMemory(int pid, uint address, uint size, MemoryCondition condition, byte[] memory, Action<byte[]> callback)
        {
            throw new NotImplementedException();
        }

        public override int FreezeMemory(int pid, uint address, uint size, MemoryCondition condition, byte[] memory)
        {
            throw new NotImplementedException();
        }

        public override void ReleaseSubID(int memSubID)
        {
            throw new NotImplementedException();
        }

        public override int MemSubIDForAddress(uint address)
        {
            throw new NotImplementedException();
        }
    }
}
