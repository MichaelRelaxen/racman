using System;
using System.Linq;
using System.Net;
using System.IO;
using System.Threading;

namespace racman
{
    class func
    {
        public static WebClient client = new WebClient();
        public static int pid = AttachPS3Form.pid;
        public static string ebootPath = Environment.CurrentDirectory + @"\EBOOTs";
        public static byte[] StringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }
        public static string ByteArrayToString(byte[] array)
        {
            return BitConverter.ToString(array).Replace("-", string.Empty);
        }
        public static float HexToFloat(string s)
        {
            uint x = Convert.ToUInt32(s, 16);
            return BitConverter.ToSingle(BitConverter.GetBytes(x), 0);
        }
        public static string get_data(string url)
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
        public static int current_pid(string ip)
        {
            string Output = get_data($"http://{ip}/getmem.ps3mapi?");
            int ebootPos = Output.IndexOf("_main_EBOOT.BIN");
            if (ebootPos != -1)
            {
                string processHex = Output.Substring(ebootPos - 8, 8);
                int processDec = Convert.ToInt32(processHex, 16);
                return processDec;
            }
            else return 0;
        }
        public static string current_game(string ip)
        {
            string Output = get_data($"http://{ip}/cpursx.ps3?/sman.ps3");
            int gamePos = Output.IndexOf("target=\"_blank\">")+16;
            return Output.Substring(gamePos, 9);
        }
        public static void WriteMemory(string ip, int pid, uint offset, string val/*byte[] memory*/)
        {
            string addr = Convert.ToString(offset, 16);
            //string val = BitConverter.ToString(memory).Replace("-", string.Empty);
            get_data($"http://{ip}/setmem.ps3mapi?proc={pid}$addr={addr}&val={val}");
        }
        public static string ReadMemory(string ip, int pid, uint offset, uint length)
        {
            string addr = Convert.ToString(offset, 16);
            string Output = get_data($"http://{ip}/getmem.ps3mapi?proc={pid}$addr={addr}&len={length}");
            int resPos = Output.IndexOf("</textarea>");

            string resultstring = Output.Substring(resPos - (int)length * 2, (int)length * 2);
            return resultstring;
        }

        public static void UploadFile(string ip, string file)
        {
            try
            {
                client.UploadFile($"ftp://{ip}:21/dev_hdd0/game/NPEA00387/USRDIR/EBOOT.BIN", file);
            }
            catch
            {
            }
        }
    }
}
