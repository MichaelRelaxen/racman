using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using NLua;

namespace racman
{
    class LuaAutomation
    {
        private LuaAutomationTimer luaAutomationTimer = new LuaAutomationTimer();
        private LuaFunctions functions;
        private Mod mod;

        public bool failed = false;

        private class InputsClass
        {
            public bool CrossPressed() => Inputs.Mask.Contains(Inputs.Buttons.cross);
            public bool TrianglePressed() => Inputs.Mask.Contains(Inputs.Buttons.triangle);
            public bool SquarePressed() => Inputs.Mask.Contains(Inputs.Buttons.square);
            public bool CirclePressed() => Inputs.Mask.Contains(Inputs.Buttons.circle);

            public bool R1Pressed() => Inputs.Mask.Contains(Inputs.Buttons.r1);
            public bool R2Pressed() => Inputs.Mask.Contains(Inputs.Buttons.r2);
            public bool R3Pressed() => Inputs.Mask.Contains(Inputs.Buttons.r3);
            public bool L1Pressed() => Inputs.Mask.Contains(Inputs.Buttons.l1);
            public bool L2Pressed() => Inputs.Mask.Contains(Inputs.Buttons.l2);
            public bool L3Pressed() => Inputs.Mask.Contains(Inputs.Buttons.l3);

            public bool StartPressed() => Inputs.Mask.Contains(Inputs.Buttons.start);
            public bool SelectPressed() => Inputs.Mask.Contains(Inputs.Buttons.select);
        }

        public LuaAutomation(string filename, string gameID, Mod mod)
        {
            this.mod = mod;

            Lua state = this.InitLuaState(gameID, Path.GetDirectoryName(filename));

            Console.WriteLine($"Loading Lua automation from file {filename}...");
            // Load automation file
            var automationFile = File.OpenRead(filename);
            StreamReader reader = new StreamReader(automationFile);
            var automation = reader.ReadToEnd();

            try
            {
                state.DoString(automation, filename.Replace($"{Directory.GetCurrentDirectory()}\\mods\\{gameID}\\", ""));
            } catch (NLua.Exceptions.LuaScriptException ex)
            {
                Console.Error.WriteLine(ex.Message);
                Console.Error.WriteLine(ex.StackTrace);

                failed = true;

                automationFile.Close();

                return;
            }

            automationFile.Close();

            try
            {
                // Call OnLoad Lua function
                var onLoadFunc = state["OnLoad"] as LuaFunction;
                onLoadFunc.Call();
            } catch (NLua.Exceptions.LuaScriptException ex)
            {
                Console.Error.WriteLine(ex.Message);
                Console.Error.WriteLine(ex.StackTrace);

                failed = true;

                return;
            }

            
            // Set up OnTick function
            luaAutomationTimer = new LuaAutomationTimer();
            luaAutomationTimer.Interval = (int)16.66667;
            luaAutomationTimer.Elapsed += LuaAutomationTick;
            luaAutomationTimer.SynchronizingObject = null;

            luaAutomationTimer.State = state;
            luaAutomationTimer.TickFunction = state["OnTick"] as LuaFunction;
            luaAutomationTimer.OnUnloadFunction = state["OnUnload"] as LuaFunction;

            luaAutomationTimer.Start();

            Console.WriteLine($"Loaded Lua automation for file {filename}!");
        }

        /// This constructor only runs a given function once I guess
        public LuaAutomation(string code)
        {
            Lua state = this.InitLuaState(AttachPS3Form.game, $"{Directory.GetCurrentDirectory()}\\mods");

            try
            {
                state.DoString(code);
            }
            catch (NLua.Exceptions.LuaScriptException ex)
            {
                Console.Error.WriteLine(ex.Message);
                Console.Error.WriteLine(ex.StackTrace);

                failed = true;

                return;
            }
            
        }

        public Lua InitLuaState(string game, string fpath)
        {
            Lua state = new Lua();
            state.UseTraceback = true;

            functions = new LuaFunctions();
            functions.ModName = mod?.name;

            state.LoadCLRPackage();

            state.RegisterFunction("print", functions, typeof(LuaFunctions).GetMethod("Print"));
            state.RegisterFunction("sleep", typeof(LuaFunctions).GetMethod("Sleep"));
            state.RegisterFunction("bytestoint", typeof(LuaFunctions).GetMethod("ByteArrayToInt"));
            state.RegisterFunction("inttobytes", typeof(LuaFunctions).GetMethod("IntToByteArray"));
            state.RegisterFunction("bytestofloat", typeof(LuaFunctions).GetMethod("ByteArrayToFloat"));
            state.RegisterFunction("floattobytes", typeof(LuaFunctions).GetMethod("FloatToByteArray"));
            state.RegisterFunction("memset", typeof(LuaFunctions).GetMethod("Memset"));
            state.RegisterFunction("ba", typeof(LuaFunctions).GetMethod("LuaTableToByteArray"));
            state.RegisterFunction("dumpbytes", typeof(LuaFunctions).GetMethod("DumpByteArray"));
            state.RegisterFunction("read_large", typeof(LuaFunctions).GetMethod("ReadLarge"));
            state.RegisterFunction("get_ba_range", typeof(LuaFunctions).GetMethod("GetByteArrayRange"));
            state.RegisterFunction("large_lookup", typeof(LuaFunctions).GetMethod("LargeLookup"));
            state.RegisterFunction("subscribe_memory", functions, typeof(LuaFunctions).GetMethod("SubscribeMemory"));
            state.RegisterFunction("udp_sock", typeof(LuaFunctions).GetMethod("UdpSock"));
            state.RegisterFunction("writememory", typeof(LuaFunctions).GetMethod("WriteMemoryAsync"));
            state.RegisterFunction("net_receive", typeof(LuaFunctions).GetMethod("ReceiveSockData"));
            state.RegisterFunction("read_byte", typeof(LuaFunctions).GetMethod("ReadOneByte"));
            state.RegisterFunction("set_lap_flag_addr", typeof(LuaFunctions).GetMethod("SetLapFlagAddress"));

            state["Ratchetron"] = func.api;
            state["GAME_PID"] = func.api.getCurrentPID();
            state["Inputs"] = new InputsClass();

            // Load racman standard library
            string standardLibsFolder = $"{Directory.GetCurrentDirectory()}\\mods\\libs\\standard\\";

            if (Directory.Exists(standardLibsFolder))
            {
                var libraryFiles = Directory.EnumerateFiles(standardLibsFolder);

                foreach (var libraryFile in libraryFiles)
                {
                    if (!libraryFile.EndsWith(".lua"))
                    {
                        continue;
                    }

                    var libReader = new StreamReader(libraryFile);
                    state.DoString(libReader.ReadToEnd(), libraryFile.Replace($"{Directory.GetCurrentDirectory()}\\mods\\", ""));
                }
            }


            // Load "standard" library for current game
            string gameLibsFolder = $"{Directory.GetCurrentDirectory()}\\mods\\libs\\{game}\\";
            state.DoString($"package.path = package.path .. \";{fpath.Replace("\\", "\\\\")}\\\\?.lua\"", "set package path chunk");

            if (Directory.Exists(gameLibsFolder))
            {
                var libraryFiles = Directory.EnumerateFiles(gameLibsFolder);

                foreach (var libraryFile in libraryFiles)
                {
                    var libReader = new StreamReader(libraryFile);
                    try
                    {
                        state.DoString(libReader.ReadToEnd(), libraryFile.Replace($"{Directory.GetCurrentDirectory()}\\mods\\", ""));
                    }
                    catch (NLua.Exceptions.LuaScriptException ex)
                    {
                        Console.Error.WriteLine(ex.Message);
                        Console.Error.WriteLine(ex.StackTrace);

                        failed = true;

                        libReader.Close();

                        return null;
                    }

                    libReader.Close();
                }
            }

            return state;
        }
    
        private void LuaAutomationTick(object sender, EventArgs e)
        {
            var timer = (LuaAutomationTimer)sender;

            if (failed)
            {
                timer.Stop();
                return;
            }

            if (timer.Mutex > 0)
            {
                timer.Ticks += 1;
                timer.MissedTicks += 1;

                Console.WriteLine($"{{{mod.name}}} Skipping a tick, missed {timer.MissedTicks} ticks.");

                return;
            }

            timer.Mutex = 1;

            var watch = new System.Diagnostics.Stopwatch();

            watch.Start();

            try
            {
                timer.CallMutex.WaitOne();
                timer.TickFunction.Call(timer.Ticks);
                timer.CallMutex.ReleaseMutex();
            } catch (NLua.Exceptions.LuaScriptException ex)
            {
                Console.Error.WriteLine(ex.Message);
                Console.Error.WriteLine(ex.StackTrace);

                mod.Unload();

                failed = true;

                return;
            }

            watch.Stop();

            if (watch.ElapsedMilliseconds > 16)
            {
                Console.WriteLine($"{{{functions.ModName}}} Tick function exceeded 16 ms, took: {watch.ElapsedMilliseconds} ms");
            }

            timer.Ticks += 1;
            timer.MissedTicks = 0;
            timer.Mutex = 0;
        }

        public void Unload()
        {
            if (failed)
            {
                return;
            }

            this.luaAutomationTimer.Stop();
            try
            {
                this.luaAutomationTimer.OnUnloadFunction.Call();
            } catch (NLua.Exceptions.LuaScriptException ex)
            {
                Console.Error.WriteLine(ex.Message);
                Console.Error.WriteLine(ex.StackTrace);

                failed = true;

                return;
            }

            this.luaAutomationTimer.State.Close();
        }
    }

    // Functions injected into the Lua state
    class LuaFunctions
    {
        public string ModName;
        public LuaAutomationTimer timer;

        public void Print(string text)
        {
            Console.WriteLine($"[{ModName}] {text}");
        }

        public static void Sleep(int milliseconds)
        {
            Thread.Sleep(milliseconds);
        }

        public static uint ByteArrayToInt(byte[] bytes)
        {
            if (bytes.Length == 1)
            {
                return bytes[0];
            }

            if (bytes.Length == 2)
            {
                return BitConverter.ToUInt16(bytes.Reverse().ToArray(), 0);
            }

            return BitConverter.ToUInt32(bytes.Reverse().ToArray(), 0);
        }

        public static byte[] IntToByteArray(int num, int size)
        {
            return BitConverter.GetBytes(num).Take(size).Reverse().ToArray();
        }

        public static float ByteArrayToFloat(byte[] bytes)
        {
            return BitConverter.ToSingle(bytes.Reverse().ToArray(), 0);
        }

       public static byte[] FloatToByteArray(float number)
       {
            return BitConverter.GetBytes(number).Reverse().ToArray();
       }

        public static void Memset(uint addr, byte num, uint size)
        {
            IPS3API api = func.api;

            api.WriteMemory(AttachPS3Form.pid, addr, size, Enumerable.Repeat<byte>(num, (int)size).ToArray());
        }

        public static byte[] LuaTableToByteArray(object table)
        {
            List<byte> bytes = new List<byte>();

            foreach (var value in ((NLua.LuaTable)table).Values)
            {
                bytes.Add(((byte)((Int64)value)));
            }

            return bytes.ToArray();
        }

        public static void DumpByteArray(byte[] bytes)
        {
            foreach (byte val in bytes)
            {
                Console.Write($"{val.ToString("X2")} ");
            }
            Console.WriteLine("");
        }

        public static byte[] ReadLarge(uint address, uint size)
        {
            List<byte> buffer = new List<byte>();

            int pid = AttachPS3Form.pid;
            Ratchetron api = (Ratchetron)func.api;

            for (uint i = 0; i <= size; i+=0x8000)
            {
                buffer.AddRange(api.ReadMemory(pid, address + i, 0x8000));
            }

            return buffer.ToArray();
        }

        public static uint[] LargeLookup(byte[] bytes, int offset, int objectSize, byte[] lookup)
        {
            List<uint> result = new List<uint>();

            for (int i = 0; i < bytes.Length; i += objectSize)
            {
                if (bytes.Skip(i + offset).Take(lookup.Length).ToArray() == lookup)
                {
                    result.Add((uint)i);
                }
            }

            return result.ToArray();
        }

        public static byte[] GetByteArrayRange(byte[] bytes, int start, int count)
        {
            List<byte> result = new List<byte>();
            
            for (int i = 0; i < count; i++)
            {
                result.Add(bytes[start + i]);
            }

            return result.ToArray();
        }

        public int SubscribeMemory(int address, int size, LuaFunction callback)
        {
            int pid = AttachPS3Form.pid;
            Ratchetron api = (Ratchetron)func.api;

            var subID = -1;
            subID = api.SubMemory(pid, (uint)address, (uint)size, (value) =>
            {
                if (timer == null)
                {
                    api.ReleaseSubID(subID);
                    return;
                }
                timer.CallMutex.WaitOne();
                callback.Call(value.Reverse().ToArray());
                timer.CallMutex.ReleaseMutex();
            });

            return subID;
        }

        public static UdpClient UdpSock(string endPoint)
        {
            string[] ep = endPoint.Split(':');
            if (ep.Length != 2) throw new FormatException("Invalid endpoint format");
            IPAddress ip;
            if (!IPAddress.TryParse(ep[0], out ip))
            {
                throw new FormatException("Invalid ip-adress");
            }
            int port;
            if (!int.TryParse(ep[1], NumberStyles.None, NumberFormatInfo.CurrentInfo, out port))
            {
                throw new FormatException("Invalid port");
            }

            var client = new UdpClient(ip.ToString(), port);

            return client;
        }

        public static void WriteMemoryAsync(uint address, byte[] memory)
        {
            int pid = AttachPS3Form.pid;
            Ratchetron api = (Ratchetron)func.api;

            new Thread(() =>
            {
                api.WriteMemory(pid, address, memory);
            }).Start();
        }

        public static byte[] ReceiveSockData(UdpClient client)
        {
            IPEndPoint ep = (IPEndPoint)client.Client.RemoteEndPoint;

            client.Client.ReceiveTimeout = 1;
            if (client.Available <= 0)
            {
                return new byte[] { };
            }

            try
            {
                return client.Receive(ref ep);
            }
            catch (System.Net.Sockets.SocketException ex)
            {
                return new byte[] { };
            }

            return new byte[] { };
        }

        public static int ReadOneByte(int address)
        {
            int pid = AttachPS3Form.pid;
            Ratchetron api = (Ratchetron)func.api;
            var res = api.ReadMemory(pid, (uint)address, 1);
            return (int)res[0];
        }

        private static void SetLapFlagAddressInForm(RAC2Form rac2form, int value)
        {
            if (rac2form.InvokeRequired)
            {
                Action safeWrite = delegate { rac2form?.UpdateLapFlag(value); };
                rac2form?.Invoke(safeWrite);
            }
            else
            {
                rac2form?.UpdateLapFlag(value);
            }
        }

        // Special hook for use with the lap skip trainer UI
        public static void SetLapFlagAddress(int address)
        {
            RAC2Form rac2form = null;
            for (var i = 0; i < Application.OpenForms.Count; i++)
            {
                // lmao 
                var form = Application.OpenForms[i];
                if (form is RAC2Form)
                    rac2form = (RAC2Form)form;
            }

            if (rac2form == null)
                throw new Exception("Called SetLapFlagAddress from non-RC2 game!");
            SetLapFlagAddressInForm(rac2form, 0);

            int pid = AttachPS3Form.pid;
            Ratchetron api = (Ratchetron)func.api;
            api.SubMemory(pid, (uint)address, 1, (flag) =>
            {
                SetLapFlagAddressInForm(rac2form, flag[0]);
            });
        }
    }

    class LuaAutomationTimer : System.Timers.Timer
    {
        public Lua State;
        public LuaFunction TickFunction;
        public LuaFunction OnUnloadFunction;

        public int Ticks = 0;
        public int MissedTicks = 0;

        public int Mutex = 0;
        public Mutex CallMutex = new Mutex();
    }
}
