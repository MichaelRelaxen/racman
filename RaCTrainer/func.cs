using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace racman
{
    class func
    {
        public static WebClient client = new WebClient();
        public static int pid = AttachPS3Form.pid;
        public static IPS3API api;
        public static string sprxPath = Environment.CurrentDirectory + @"\";
        public static byte[] StringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }
        public static byte[] FromHex(string hex)
        {
            byte[] raw = new byte[hex.Length / 2];
            for (int i = 0; i < raw.Length; i++)
            {
                raw[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
            }
            return raw;
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
            return api.getCurrentPID();
        }
        public static string current_game(string ip)
        {
            return api.getGameTitleID();
        }
        public static void WriteMemory(string ip, int pid, uint offset, string val/*byte[] memory*/)
        {
            api.WriteMemory(pid, offset, (uint)val.Length / 2, val);
        }
        public static string ReadMemory(string ip, int pid, uint offset, uint length)
        {
            return api.ReadMemoryStr(pid, offset, length);
        }

        public static bool PrepareRatchetron(string ip)
        {
            // Check if Ratchetron is already loaded
            string slot6sprx = get_data($"http://{ip}/home.ps3mapi");

            bool ratchetronLoaded = slot6sprx.Contains("ratchetron_server.sprx");

            if (ratchetronLoaded)
            {
                return true;
            }
            
            // Always upload because fuck it
            client.UploadFile($"ftp://{ip}:21/dev_hdd0/tmp/ratchetron_server.sprx", sprxPath + @"\ratchetron_server.sprx");
            get_data($"http://{ip}/vshplugin.ps3mapi?prx=%2Fdev_hdd0%2Ftmp%2Fratchetron_server.sprx&load_slot=6");

            return true;
        }

        public static void WriteMemory_SingleByte(string ip, int pid, uint offset, string val/*byte[] memory*/)
        {
            api.WriteMemory(pid, offset, 1, val);
        }

        public static void ChangeFileLines(string filename, string contents, string keyword)
        {
            string[] data = File.ReadAllLines("config.txt");
            bool found = false;

            for (int i = 0; i < data.Length; i++)
            {
                if (Regex.Match(data[i], @"^([\w\-]+)").Value == keyword)
                {
                    data[i] = keyword + " = " + contents;
                    found = true;
                }
            }


            if (!found)
            {
                string[] new_data;
                new_data = new string[data.Length + 1];


                for (int i = 0; i < data.Length; i++)
                {
                    new_data[i] = data[i];
                }
                new_data[data.Length] = keyword + " = " + contents;
                File.WriteAllLines("config.txt", new_data);
            }
            else
            {
                File.WriteAllLines("config.txt", data);
            }


        }

        public static string GetConfigData(string filename, string keyword)
        {
            string[] data = File.ReadAllLines("config.txt");

            for (int i = 0; i < data.Length; i++)
            {
                if (Regex.Match(data[i], @"^([\w\-]+)").Value == keyword)
                {
                    int startPos = data[i].IndexOf("=") + 2;
                    return data[i].Substring(startPos, data[i].Length - startPos);
                }
            }

            return "";
        }
        public static IEnumerable<string> SplitByN(string str, int n)
        {
            while (str.Length > 0)
            {
                yield return new string(str.Take(n).ToArray());
                str = new string(str.Skip(n).ToArray());
            }
        }
        public static string repeatstringidfkfuckthisshit(string swag, int length) // i dont know what to name it
        {
            return string.Concat(Enumerable.Repeat(swag, length));
        }

    }
}
