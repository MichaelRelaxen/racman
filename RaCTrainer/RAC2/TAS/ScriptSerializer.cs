using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using racman;
using System.Globalization;

namespace racman
{
    public static class ScriptSerializer
    {
        private static readonly Dictionary<string, ushort> buttonMap = new Dictionary<string, ushort>()
        {
            ["l2"] = 0x1,
            ["r2"] = 0x2,
            ["l1"] = 0x4,
            ["r1"] = 0x8,
            ["triangle"] = 0x10,
            ["circle"] = 0x20,
            ["cross"] = 0x40,
            ["square"] = 0x80,
            ["select"] = 0x100,
            ["l3"] = 0x200,
            ["r3"] = 0x400,
            ["start"] = 0x800,
            ["up"] = 0x1000,
            ["right"] = 0x2000,
            ["down"] = 0x4000,
            ["left"] = 0x8000,
            ["D"] = 0x4000,
            ["U"] = 0x1000,
            ["L"] = 0x8000,
            ["R"] = 0x2000,
            ["tri"] = 0x10,
            ["x"] = 0x40,
            ["cir"] = 0x20,
            ["sq"] = 0x80,
            ["sel"] = 0x100,
            ["st"] = 0x800
        };

        private abstract class Action
        {
            public abstract void Run(BinaryWriter writer);
        }

        private class ButtonPress : Action
        {
            public ushort ButtonMask;
            public byte Load_Pos_Flag = 0;
            public byte Breakpoint = 0;
            public byte Render = 0;
            public byte Length = 0x18;
            public uint Randomness = 0;

            public byte LX = 128; // default neutral
            public byte LY = 128;
            public byte RX = 128;
            public byte RY = 128;

            public override void Run(BinaryWriter writer)
            {
                writer.Write(BitConverter.GetBytes(ButtonMask).Reverse().ToArray());

                // Write stick values
                writer.Write(new byte[] { RX, RY, LX, LY });

                writer.Write(Load_Pos_Flag);
                writer.Write(Breakpoint);
                writer.Write(Render);
                writer.Write(Length);
                writer.Write(BitConverter.GetBytes(Randomness).Reverse().ToArray());


                writer.Write(BitConverter.GetBytes((ushort)0x7C).Reverse().ToArray());
            }
        }


        private class Wait : Action
        {
            public int Frames;
            public Wait(int frames) { Frames = frames; }

            public override void Run(BinaryWriter writer)
            {
                for (int i = 0; i < Frames; i++)
                    new ButtonPress().Run(writer);
            }
        }

        private class StickDirection : Action
        {
            public int Frames;
            public string Stick; // "left" or "right"
            public double Degrees;
            public double Magnitude = 1.0;

            public StickDirection(string stick, double degrees, double magnitude = 1.0)
            {
                Stick = stick;
                Frames = 1;
                Degrees = degrees;
                Magnitude = magnitude;
            }


            public override void Run(BinaryWriter writer)
            {
                double radians = Degrees * Math.PI / 180.0;

                // Calculate X/Y components
                double x = Math.Cos(radians); // horizontal
                double y = Math.Sin(radians); // vertical

                // Normalize to make diagonals hit full strength
                double maxComponent = Math.Max(Math.Abs(x), Math.Abs(y));
                if (maxComponent > 0)
                {
                    x /= maxComponent;
                    y /= maxComponent;
                }

                // Map x: -1 (left) to +1 (right) → 0 to 255
                int horizontal = (int)((x + 1) / 2 * 255);

                // Map y: +1 (up) to -1 (down) → 0 to 255
                int vertical = (int)((1 - y) / 2 * 255);

                byte lx = 128, ly = 128, rx = 128, ry = 128;

                if (Stick == "left")
                {
                    lx = ClampToByte(horizontal);
                    ly = ClampToByte(vertical);
                }
                else if (Stick == "right")
                {
                    rx = ClampToByte(horizontal);
                    ry = ClampToByte(vertical);
                }

                for (int i = 0; i < Frames; i++)
                {
                    new ButtonPress { LX = lx, LY = ly, RX = rx, RY = ry }.Run(writer);
                }
            }




        }

        public static byte ClampToByte(int value) => (byte)Math.Max(0, Math.Min(255, value));
        private class Repeat : Action
        {
            public int Count;

            public Action SubAction;

            public string MacroName;

            public List<string> MacroCallArgs;

            public Repeat(int count, Action act)
            {
                Count = count;
                SubAction = act;
            }

            public Repeat(int count, string macroName, List<string> macroArgs)
            {
                Count = count;
                MacroName = macroName;
                MacroCallArgs = macroArgs;
            }

            public override void Run(BinaryWriter writer)
            {
                if (MacroName != null && MacroCallArgs != null && MacroCallArgs.Count >= 1)
                {
                    for (int i = 0; i < Count; i++)
                    {
                        string item = MacroCallArgs[i % MacroCallArgs.Count];
                        foreach (Action action in (from line in ExpandMacro(MacroName, new List<string> { item })
                                                   select ParseLine(line) into a
                                                   where a != null
                                                   select a).ToList())
                        {
                            action.Run(writer);
                        }
                    }
                }
                else
                {
                    if (SubAction == null)
                        throw new Exception("Repeat SubAction is null");

                    for (int j = 0; j < Count; j++)
                    {
                        SubAction.Run(writer);
                    }
                }
            }
        }

        private class Sequence : Action
        {
            public List<Action> Actions = new List<Action>();
            public override void Run(BinaryWriter writer)
            {
                foreach (var a in Actions) a.Run(writer);
            }
        }

        private static Dictionary<string, (List<string> args, List<string> body)> macros;

        public static void Compile(string inputPath, string outputPath)
        {
            macros = new Dictionary<string, (List<string>, List<string>)>();
            var lines = File.ReadAllLines(inputPath).ToList();
            var actions = ParseScript(lines);

            var ms = new MemoryStream();
            var writer = new BinaryWriter(ms);

            actions.Run(writer);

            int totalFrames = (int)(ms.Length / 16) - 1;
            for (int i = 0; i < 3; i++)
                writer.Write(BitConverter.GetBytes(0xDEADDEAD).Reverse().ToArray());
            writer.Write(BitConverter.GetBytes(totalFrames).Reverse().ToArray());

            writer.Close();
            File.WriteAllBytes(outputPath, ms.ToArray());
        }

        private static Action ParseScript(List<string> lines)
        {
            var actions = new List<Action>();
            int i = 0;
            int currentFrameCount = 1; 

            while (i < lines.Count)
            {
                var raw = lines[i];
                var line = raw.Contains("//") ? raw.Substring(0, raw.IndexOf("//")).Trim() : raw.Trim();

                if (string.IsNullOrWhiteSpace(line)) { i++; continue; }
                if (line.StartsWith("linehide")) { i++; continue; }

                if (line.StartsWith("macro "))
                {
                    i = ParseMacro(lines, i);
                }
                else if (line.StartsWith("lock ")) 
                {
                    // Syntax: lock <frame> <command...>
                    Match lockMatch = Regex.Match(line, @"lock\s+(\d+)\s+(.+)");
                    if (lockMatch.Success)
                    {
                        if (!int.TryParse(lockMatch.Groups[1].Value, out int targetFrame))
                            throw new Exception($"Invalid lock frame number at line {i + 1}: {line}");

                        string command = lockMatch.Groups[2].Value.Trim();

                        if (targetFrame > currentFrameCount)
                        {
                            int waitFrames = targetFrame - currentFrameCount;
                            actions.Add(new Wait(waitFrames));
                            currentFrameCount += waitFrames;
                        }

                        try
                        {
                            Action commandAction = ParseLine(command);
                            if (commandAction != null)
                            {
                                actions.Add(commandAction);
                                currentFrameCount += GetActionFrameCount(commandAction);
                            }
                        }
                        catch (Exception ex)
                        {
                            throw new Exception($"Failed to parse lock command at line {i + 1}: {line}\nReason: {ex.Message}");
                        }
                    }
                    else
                    {
                        throw new Exception($"Invalid lock syntax at line {i + 1}. Expected: lock <frame> <command>");
                    }
                    i++;
                }
                else 
                {
                    try
                    {
                        Action actionsObj = ParseLine(line);
                        if (actionsObj != null)
                        {
                            actions.Add(actionsObj);
                            currentFrameCount += GetActionFrameCount(actionsObj);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception($"Failed to parse line {i + 1}: {lines[i].Trim()}\nReason: {ex.Message}");
                    }
                    i++;
                }
            }

            return new Sequence { Actions = actions };
        }
        private static int GetActionFrameCount(Action action)
        {
            if (action is Wait wait) return wait.Frames;
            if (action is ButtonPress) return 1;

            if (action is Sequence seq)
            {
                return seq.Actions.Sum(a => GetActionFrameCount(a));
            }

            if (action is Repeat repeat)
            {
                if (repeat.MacroName != null && macros.ContainsKey(repeat.MacroName))
                {
                    var (_, body) = macros[repeat.MacroName];
                    int macroFrames = 0;

                    foreach (var line in body)
                    {
                        string cleaned = line.Trim();
                        if (string.IsNullOrWhiteSpace(cleaned) || cleaned.StartsWith("//")) continue;

                        try
                        {
                            Action subAction = ParseLine(cleaned);
                            if (subAction != null)
                            {
                                macroFrames += GetActionFrameCount(subAction);
                            }
                        }
                        catch
                        {
                            macroFrames += 1;
                        }
                    }
                    return repeat.Count * macroFrames;
                }

                if (repeat.SubAction != null)
                {
                    return repeat.Count * GetActionFrameCount(repeat.SubAction);
                }
                return repeat.Count;
            }

            return 1; 
        }

        private static int ParseMacro(List<string> lines, int start)
        {
            var raw = lines[start];
            var line = raw.Contains("//") ? raw.Substring(0, raw.IndexOf("//")).Trim() : raw.Trim();
            var header = line.Trim().Trim(':');
            var parts = header.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length < 2 || parts[0] != "macro")
            {
                throw new Exception($"Invalid macro definition syntax at line {start + 1}: {lines[start]}");
            }

            var name = parts[1];
            var args = parts.Skip(2).ToList();
            var body = new List<string>();

            int i = start + 1;
            while (i < lines.Count && !string.IsNullOrWhiteSpace(lines[i]))
                body.Add(lines[i++]);

            macros[name] = (args, body);
            return i;
        }

        private static List<string> ExpandMacro(string name, List<string> callArgs)
        {
            if (!macros.ContainsKey(name))
                throw new Exception($"Unknown macro '{name}'");

            var (args, body) = macros[name];

            if (args.Count == 0)
            {
                return new List<string>(body);
            }


            if (args.Count == 1 && callArgs.Count >= 1)
            {
                var argName = args[0];
                var values = callArgs;

                var expandedLines = new List<string>();

                foreach (var line in body)
                {
                    int cycleIndex = 0;
                    string replacedLine = Regex.Replace(line, $@"\b{Regex.Escape(argName)}\b", m =>
                    {
                        var val = values[cycleIndex % values.Count];
                        cycleIndex++;
                        return val;
                    });
                    expandedLines.Add(replacedLine);
                }
                return expandedLines;
            }
            else if (args.Count == callArgs.Count)
            {
                var expanded = new List<string>();
                foreach (var line in body)
                {
                    string modified = line;
                    for (int j = 0; j < args.Count; j++)
                        modified = Regex.Replace(modified, $@"\b{Regex.Escape(args[j])}\b", callArgs[j]);
                    expanded.Add(modified);
                }
                return expanded;
            }
            else
            {
                throw new Exception($"Macro '{name}' expects {args.Count} args but got {callArgs.Count}");
            }
        }


        private static Action ParseLine(string line)
        {
            var tokens = line.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            if (tokens.Length == 0) return null;

            string cmd = tokens[0];

            if (macros.ContainsKey(cmd))
            {
                var expanded = ExpandMacro(cmd, tokens.Length > 1 ? tokens.Skip(1).ToList() : new List<string>());
                // var expanded = ExpandMacro(cmd, tokens.Skip(1).ToList());
                var subActions = expanded.Select(ParseLine).Where(a => a != null).ToList();
                return new Sequence { Actions = subActions };
            }

            if (cmd == "wait")
            {
                if (tokens.Length < 2 || !int.TryParse(tokens[1], out int frames))
                    throw new Exception("Invalid wait command syntax. Usage: wait <frames>");
                return new Wait(frames);
            }

            if (cmd == "rep")
            {
                if (tokens.Length < 3 || !int.TryParse(tokens[1], out int count))
                    throw new Exception("Invalid rep command syntax. Usage: rep <count> <action>");

                var subTokens = tokens.Skip(2).ToList();
                string rest = string.Join(" ", subTokens);
                return new Repeat(count, ParseLine(rest));
            }

            // Begin compound handling
            ushort btnMask = 0;

            byte load_pos_flag = 0;
            byte breakpoint = 0;
            byte render = 0;
            uint rng = 0;
            byte lx = 128, ly = 128, rx = 128, ry = 128;

            int i = 0;
            while (i < tokens.Length)
            {
                string token = tokens[i];

                if (buttonMap.TryGetValue(token, out ushort mask))
                {
                    btnMask |= mask;
                    i++;
                }
                else if (token == "teleport")
                {
                    load_pos_flag = 1;
                    i++;
                }
                else if (token == "breakpoint")
                {
                    breakpoint = 1;
                    i++;
                }
                else if (token == "show")
                {
                    render |= 0b0100;
                    i++;
                }
                else if (token == "hide")
                {
                    render |= 0b1100;
                    i++;
                }
                else if (token == "skip")
                {
                    render |= 0b0011;
                    i++;
                }
                else if (token == "noskip")
                {
                    render |= 0b0001;
                    i++;
                }

                else if (token == "rng")
                {
                    i++;
                    if (i >= tokens.Length)
                        throw new Exception("Expected number after rng");

                    if (!uint.TryParse(tokens[i++], out uint parsedRng))
                        throw new Exception("Invalid rng value, must be unsigned integer");

                    rng = parsedRng;
                }

                else if (token == "left_stick" || token == "right_stick")
                {
                    string stick = token == "left_stick" ? "left" : "right";
                    i++;
                    if (i >= tokens.Length)
                        throw new Exception($"Expected angle after {token}");

                    string degStr = tokens[i++];
                    string numberPart = degStr.Replace("deg", "");

                    if (!degStr.EndsWith("deg") || !double.TryParse(numberPart, NumberStyles.Any, CultureInfo.InvariantCulture, out double degrees))
                        throw new Exception("Invalid stick angle syntax (must end in 'deg') or invalid number format.");

                    double magnitude = 1.0;
                    if (i < tokens.Length && double.TryParse(tokens[i], NumberStyles.Any, CultureInfo.InvariantCulture, out double parsedMagnitude))
                    {
                        magnitude = Math.Max(0.0, Math.Min(1.0, parsedMagnitude));
                        i++;
                    }

                    double radians = degrees * Math.PI / 180.0;
                    double x = Math.Cos(radians);
                    double y = Math.Sin(radians);

                    // Normalize direction
                    double maxComponent = Math.Max(Math.Abs(x), Math.Abs(y));
                    if (maxComponent > 0)
                    {
                        x /= maxComponent;
                        y /= maxComponent;
                    }

                    // Apply magnitude
                    x *= magnitude;
                    y *= magnitude;

                    int horizontal = (int)((x + 1) / 2 * 255);
                    int vertical = (int)((1 - y) / 2 * 255);

                    if (stick == "left")
                    {
                        lx = ClampToByte(horizontal);
                        ly = ClampToByte(vertical);
                    }
                    else
                    {
                        rx = ClampToByte(horizontal);
                        ry = ClampToByte(vertical);
                    }
                }

                else
                {
                    throw new Exception($"Unknown token: {token}");
                }

                if (render != 0)
                {
                    Console.WriteLine($"saw {token}: {render}");
                }
            }

            return new ButtonPress
            {
                ButtonMask = btnMask,
                Load_Pos_Flag = load_pos_flag,
                Breakpoint = breakpoint,
                Render = render,
                Randomness = rng,
                LX = lx,
                LY = ly,
                RX = rx,
                RY = ry,
            };
        }

    }
}