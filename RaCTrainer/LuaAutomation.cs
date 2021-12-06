using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLua;

namespace racman
{
    class LuaAutomation
    {
        private LuaAutomationTimer luaAutomationTimer = new LuaAutomationTimer();
        private LuaFunctions functions;
        private Mod mod;

        public bool failed = false;

        public LuaAutomation(string filename, string gameID, Mod mod)
        {
            this.mod = mod;

            Console.WriteLine($"Loading Lua automation from file {filename}...");

            Lua state = new Lua();

            functions = new LuaFunctions();
            functions.ModName = mod.name;

            state.LoadCLRPackage();

            state.RegisterFunction("print", functions, typeof(LuaFunctions).GetMethod("LuaPrint"));
            state.RegisterFunction("bytestoint", typeof(LuaFunctions).GetMethod("LuaByteArrayToInt"));

            state["Ratchetron"] = func.api;
            state["GAME_PID"] = func.api.getCurrentPID();

            // Load racman standard library
            string standardLibsFolder = $"{Directory.GetCurrentDirectory()}\\mods\\libs\\standard\\";

            if (Directory.Exists(standardLibsFolder))
            {
                var libraryFiles = Directory.EnumerateFiles(standardLibsFolder);

                foreach (var libraryFile in libraryFiles)
                {
                    var libReader = new StreamReader(libraryFile);
                    state.DoString(libReader.ReadToEnd());
                }
            }


            // Load "standard" library for current game

            string gameLibsFolder = $"{Directory.GetCurrentDirectory()}\\mods\\libs\\{gameID}\\";

            if (Directory.Exists(gameLibsFolder))
            {
                var libraryFiles = Directory.EnumerateFiles(gameLibsFolder);

                foreach (var libraryFile in libraryFiles)
                {
                    var libReader = new StreamReader(libraryFile);
                    state.DoString(libReader.ReadToEnd());
                }
            }

            // Load automation file
            var automationFile = File.OpenRead(filename);
            StreamReader reader = new StreamReader(automationFile);
            var automation = reader.ReadToEnd();

            try
            {
                state.DoString(automation);
            } catch (NLua.Exceptions.LuaScriptException ex)
            {
                Console.Error.WriteLine(ex.Message);
                Console.Error.WriteLine(ex.StackTrace);

                failed = true;

                return;
            }

            // Call OnLoad Lua function
            var onLoadFunc = state["OnLoad"] as LuaFunction;
            onLoadFunc.Call();

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
                timer.TickFunction.Call(timer.Ticks);
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

    class LuaFunctions
    {
        public string ModName;

        public void LuaPrint(string text)
        {
            Console.WriteLine($"[{ModName}] {text}");
        }

        public static int LuaByteArrayToInt(byte[] bytes)
        {
            return (int)BitConverter.ToInt32(bytes.Take(4).Reverse().ToArray(), 0);
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
    }
}
