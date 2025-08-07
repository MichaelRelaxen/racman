using System;
using racman;
using System.Collections.Generic;
using System.IO;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Threading;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;
using System.Text.RegularExpressions;

namespace racman
{
    static class Rackets2API
    {
        private static uint tasState = 0x1BE0270;
        private static uint tasStop = 0x1BE0274;
        private static uint frameStepMode = 0x1BE0028;
        private static uint frameAdvance = 0x1BE002C;
        private static uint tasRenderMode = 0x1BE0038;
        private static uint tasGCMFlip = 0x1BE003C;

        private static uint savefileLoadAside = 0x1BE0040;
        private static uint savefileSetAside = 0x1BE0044;

        private static uint tasHideHud = 0x1BE0048;

        private static uint saveMode = 0x1BE004C;
        private static uint positionToLoad = 0x1BE0400;
        private static uint playerPosition = 0x147F260;
        private static uint currentPlanet = 0x1329A3C;

        private static string savedPosition;

        private static IPS3API api = func.api;


        public static string UploadInputsFile(string scriptFilePath)
        {
            if (!File.Exists(scriptFilePath))
            {
                return $"Script file not found: {scriptFilePath}";
            }

            string scriptDir = Path.GetDirectoryName(scriptFilePath);
            string compiledFileName = Path.GetFileNameWithoutExtension(scriptFilePath) + ".rtas";

            string compiledDir = Path.Combine(scriptDir, "compiled");
            if (!Directory.Exists(compiledDir))
            {
                Directory.CreateDirectory(compiledDir);
            }
            string compiledFilePath = Path.Combine(compiledDir, compiledFileName);


            try
            {
                ScriptSerializer.Compile(scriptFilePath, compiledFilePath);
            }
            catch (Exception e)
            {
                return $"compilation failed: {e.Message}\n";
            }

            Console.Write("Uploading... ");
            try
            {
                api.WriteFile("/dev_hdd0/game/NPEA00386/USRDIR/recording.rtas", compiledFilePath);
                Console.WriteLine("done.");

                ReloadSetState(4);
                // Thread.Sleep(200);
            }
            catch (Exception e)
            {
                return $"Upload failed: {e.Message}";
            }

            return null;
        }


        private static void WriteUint(uint addr, uint state)
        {
            api.WriteMemory(api.getCurrentPID(), addr, state);
        }

        public static void PauseUnpauseRackets(bool pause)
        {
            WriteUint(frameStepMode, (uint)(pause ? 1 : 0));
        }

        public static void Framestep()
        {
            WriteUint(frameAdvance, 1);
        }

        public static void SetPositionToLoadMethod()
        {
            savedPosition = api.ReadMemoryStr(api.getCurrentPID(), playerPosition, 34);
            api.WriteMemory(api.getCurrentPID(), positionToLoad, 34, savedPosition);
        }

        public static void CopyPositionToClipBoardMethod()
        {
            Clipboard.SetText(savedPosition);
        }
        public static void PastePositionFromClipboard()
        {
            savedPosition = Clipboard.GetText();
            api.WriteMemory(api.getCurrentPID(), positionToLoad, 34, savedPosition);
        }

        public static void SetCurrentLevelMethod(uint level)
        {
            api.WriteMemory(api.getCurrentPID(), currentPlanet, level);
        }

        public static void SetSaveModeMethod(uint mode)
        {
            api.WriteMemory(api.getCurrentPID(), saveMode, mode);
        }

        public static void SetAsideMethod()
        {
            api.WriteMemory(api.getCurrentPID(), savefileSetAside, new byte[1] { 1 });
        }

        public static void LoadSetAsideMethod()
        {
            api.WriteMemory(api.getCurrentPID(), savefileLoadAside, new byte[1] { 1 });
        }

        private static void ReloadSetState(uint state)
        {
            LoadSetAsideMethod();
            WriteUint(tasState, state);
        }

        public static void CancelPlayback()
        {
            WriteUint(tasStop, 1);
        }

        public static void RestartPlayback()
        {
            ReloadSetState(4);
        }

        public static void StartRecording()
        {
            ReloadSetState(1);
        }

        public static void SetRenderingMode(bool skipFrames, bool skipRender)
        {
            WriteUint(tasRenderMode, (uint)(skipRender ? 1 : 0));
            WriteUint(tasGCMFlip, (uint)(skipFrames ? 1 : 0));
        }

        public static void SetHudStatus(bool hide)
        {
            WriteUint(tasHideHud, (uint)(hide ? 1 : 0));
        }

        public static void SetConfigData(string keyword, string contents)
        {
            ChangeFileLines("config.txt", contents, keyword);
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
                File.WriteAllLines("tas-config.txt", new_data);
            }
            else
            {
                File.WriteAllLines("tas-config.txt", data);
            }
        }

        public static string GetConfigData(string keyword)
        {
            string[] data = File.ReadAllLines("tas-config.txt");

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
    }
}
