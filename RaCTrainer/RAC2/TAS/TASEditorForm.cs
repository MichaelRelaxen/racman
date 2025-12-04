// written using claude and gemini llms

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using ScintillaNET;
using System.Runtime.InteropServices;

namespace racman
{
    public partial class TASEditorForm : Form
    {
        private Scintilla scintilla;
        private int frameTimerSubID = -1;
        private int currentFrame = 0;
        private int lastProcessedFrameCount = 0;
        private int currentHighlightedLine = -1;
        private const uint FRAME_TIMER_OFFSET = 0x20;

        private bool liveHighlightEnabled = true;
        private string currentFilePath = "";

        // Style Constants
        private const int StyleDefault = 0;
        private const int StyleComment = 1;
        private const int StyleMacro = 2;
        private const int StyleCommand = 3;
        private const int StyleNumber = 4;
        private const int StyleButton = 5;
        private const int StyleLinehide = 6;
        private const int StyleBreakpoint = 7;
        private const int StyleVisibility = 8;

        // Margins
        private const int MarginFrameCounts = 0;
        private const int MarginFold = 1;

        // Margin Text Style
        private const int StyleMarginText = 33;

        // Indicator for live frame highlight
        private const int IndicatorFrameHighlight = 8;

        // Colors
        private readonly Color colorComment = Color.FromArgb(87, 166, 74);
        private readonly Color colorMacro = Color.FromArgb(220, 220, 170);
        private readonly Color colorCommand = Color.FromArgb(86, 156, 214);
        private readonly Color colorNumber = Color.FromArgb(181, 206, 168);
        private readonly Color colorButton = Color.FromArgb(156, 220, 254);
        private readonly Color colorLinehide = Color.FromArgb(255, 165, 0); // Orange
        private readonly Color colorBreakpoint = Color.FromArgb(255, 80, 80); // Red
        private readonly Color colorVisibility = Color.FromArgb(255, 255, 0); // Yellow
        private readonly Color colorBackground = Color.FromArgb(30, 30, 30);
        private readonly Color colorForeground = Color.FromArgb(212, 212, 212);
        private readonly Color colorLineHighlight = Color.FromArgb(60, 60, 60);
        private readonly Color colorMarginBack = Color.FromArgb(37, 37, 38);
        private readonly Color colorMarginFore = Color.FromArgb(133, 133, 133);
        private readonly Color colorFrameHighlight = Color.FromArgb(100, 150, 100); // green tint for live frame

        // Store frame counts per line and frame info
        private Dictionary<int, int> lineFrameCounts = new Dictionary<int, int>();
        private Dictionary<int, LineFrameInfo> lineFrameInfo = new Dictionary<int, LineFrameInfo>();
        private Dictionary<string, (List<string> args, List<string> body)> macros = new Dictionary<string, (List<string>, List<string>)>();

        private string currentProgressText = "";
        private Label statusLabel;

        private class LineFrameInfo
        {
            public int StartFrame;
            public int Duration;
            public string CommandType; // "wait", "rep", or "single"
            public int RepCount; // For rep commands and macro calls
            public int MacroFramesPerRep; // For rep commands that call macros
            public string MacroName; // Name of the macro being executed
        }

        public TASEditorForm()
        {
            InitializeComponents();
            SetupScintilla();
            LoadSampleContent();
            SubscribeToFrameTimer();
        }

        private void InitializeComponents()
        {
            this.Text = "Rackets2 TAS Editor";
            this.Size = new Size(1200, 800);
            this.BackColor = colorBackground;
            this.ShowIcon = false; 
            this.FormClosing += TASEditorForm_FormClosing;

            MenuStrip menuStrip = new MenuStrip();
            menuStrip.BackColor = Color.FromArgb(45, 45, 48);
            menuStrip.ForeColor = Color.White;
            menuStrip.Dock = DockStyle.Top;

            ToolStripMenuItem fileMenu = new ToolStripMenuItem("File");
            fileMenu.DropDownItems.Add("Open", null, OnOpenFile);
            fileMenu.DropDownItems.Add("Save", null, OnSaveFile);
            fileMenu.DropDownItems.Add("Save As...", null, OnSaveFileAs);
            menuStrip.Items.Add(fileMenu);

            ToolStripMenuItem viewMenu = new ToolStripMenuItem("View");
            ToolStripMenuItem toggleLineHighlight = new ToolStripMenuItem("Live Frame Highlighting");
            toggleLineHighlight.CheckOnClick = true;
            toggleLineHighlight.Checked = true;
            toggleLineHighlight.Click += (s, e) =>
            {
                liveHighlightEnabled = toggleLineHighlight.Checked;
                if (!liveHighlightEnabled)
                {
                    ClearLineHighlight();
                }
            };
            viewMenu.DropDownItems.Add(toggleLineHighlight);
            menuStrip.Items.Add(viewMenu);

            this.MainMenuStrip = menuStrip;
            this.Controls.Add(menuStrip);

            int statusLabelMagic = 360;

            statusLabel = new Label();
            statusLabel.AutoSize = true; // itll resize if it needs more height
            statusLabel.MinimumSize = new Size(statusLabelMagic, 150);
            statusLabel.Location = new Point(this.ClientSize.Width - (statusLabelMagic + 20), 30);
            statusLabel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            statusLabel.BackColor = Color.FromArgb(45, 45, 48);
            statusLabel.ForeColor = Color.White;
            statusLabel.Font = new Font("Consolas", 10, FontStyle.Regular);
            statusLabel.TextAlign = ContentAlignment.TopLeft;
            statusLabel.Padding = new Padding(10);
            statusLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            statusLabel.Text = "No live data";
            this.Controls.Add(statusLabel);
            statusLabel.BringToFront();
        }

        private void SetupScintilla()
        {
            scintilla = new Scintilla();
            scintilla.Dock = DockStyle.Fill;
            scintilla.BorderStyle = ScintillaNET.BorderStyle.None;

            // Basic settings
            scintilla.WrapMode = WrapMode.None;
            scintilla.IndentationGuides = IndentView.LookBoth;
            scintilla.CaretForeColor = Color.White;
            scintilla.SetSelectionBackColor(true, Color.FromArgb(38, 79, 120));

            // Enable multiple selection and typing
            scintilla.MultipleSelection = true;
            scintilla.AdditionalSelectionTyping = true;
            scintilla.MouseSelectionRectangularSwitch = true;
            scintilla.VirtualSpaceOptions = VirtualSpace.RectangularSelection;

            scintilla.LexerLanguage = "container";

            // Styling
            scintilla.StyleResetDefault();
            scintilla.Styles[Style.Default].Font = "Consolas";
            scintilla.Styles[Style.Default].Size = 12;
            scintilla.Styles[Style.Default].BackColor = colorBackground;
            scintilla.Styles[Style.Default].ForeColor = colorForeground;
            scintilla.StyleClearAll();

            // Custom styles
            scintilla.Styles[StyleComment].ForeColor = colorComment;
            scintilla.Styles[StyleMacro].ForeColor = colorMacro;
            scintilla.Styles[StyleCommand].ForeColor = colorCommand;
            scintilla.Styles[StyleCommand].Bold = true;
            scintilla.Styles[StyleNumber].ForeColor = colorNumber;
            scintilla.Styles[StyleButton].ForeColor = colorButton;
            scintilla.Styles[StyleLinehide].ForeColor = colorLinehide;
            scintilla.Styles[StyleLinehide].Bold = true;
            scintilla.Styles[StyleBreakpoint].ForeColor = colorBreakpoint;
            scintilla.Styles[StyleBreakpoint].Bold = true;
            scintilla.Styles[StyleVisibility].ForeColor = colorVisibility;
            scintilla.Styles[StyleVisibility].Bold = true;

            // Margin text style
            scintilla.Styles[StyleMarginText].BackColor = colorMarginBack;
            scintilla.Styles[StyleMarginText].ForeColor = colorMarginFore;

            // Line highlighting
            scintilla.CaretLineVisible = true;
            scintilla.CaretLineBackColor = colorLineHighlight;

            // Setup indicator for live frame highlighting
            scintilla.Indicators[IndicatorFrameHighlight].Style = IndicatorStyle.StraightBox;
            scintilla.Indicators[IndicatorFrameHighlight].ForeColor = colorFrameHighlight;
            scintilla.Indicators[IndicatorFrameHighlight].Alpha = 80;
            scintilla.Indicators[IndicatorFrameHighlight].OutlineAlpha = 120;

            // Margins
            scintilla.Margins[MarginFrameCounts].Width = 80;
            scintilla.Margins[MarginFrameCounts].Type = MarginType.Text;

            scintilla.Margins[MarginFold].Width = 20;
            scintilla.Margins[MarginFold].Type = MarginType.Symbol;
            scintilla.Margins[MarginFold].Mask = Marker.MaskFolders;
            scintilla.Margins[MarginFold].Sensitive = true;
            scintilla.Margins[MarginFold].BackColor = colorBackground;

            // Folding
            scintilla.SetProperty("fold", "1");
            scintilla.SetProperty("fold.compact", "1");
            scintilla.AutomaticFold = AutomaticFold.Show | AutomaticFold.Click | AutomaticFold.Change;
            scintilla.SetFoldFlags(FoldFlags.LineAfterContracted);

            // Fold markers
            scintilla.Markers[Marker.Folder].Symbol = MarkerSymbol.BoxPlus;
            scintilla.Markers[Marker.FolderOpen].Symbol = MarkerSymbol.BoxMinus;
            scintilla.Markers[Marker.FolderEnd].Symbol = MarkerSymbol.BoxPlusConnected;
            scintilla.Markers[Marker.FolderMidTail].Symbol = MarkerSymbol.TCorner;
            scintilla.Markers[Marker.FolderOpenMid].Symbol = MarkerSymbol.BoxMinusConnected;
            scintilla.Markers[Marker.FolderSub].Symbol = MarkerSymbol.VLine;
            scintilla.Markers[Marker.FolderTail].Symbol = MarkerSymbol.LCorner;

            for (int i = 25; i <= 31; i++)
            {
                scintilla.Markers[i].SetForeColor(colorMarginBack);
                scintilla.Markers[i].SetBackColor(Color.Gray);
            }

            scintilla.StyleNeeded += Scintilla_StyleNeeded;
            scintilla.TextChanged += Scintilla_TextChanged;
            scintilla.KeyDown += Scintilla_KeyDown;
            scintilla.MarginClick += Scintilla_MarginClick;

            this.Controls.Add(scintilla);
            this.MainMenuStrip.SendToBack();
        }

        private void Scintilla_StyleNeeded(object sender, StyleNeededEventArgs e)
        {
            int startPos = scintilla.GetEndStyled();
            int startLine = scintilla.LineFromPosition(startPos);
            int endLine = scintilla.LineFromPosition(e.Position);

            for (int lineNum = startLine; lineNum <= endLine; lineNum++)
            {
                if (lineNum >= scintilla.Lines.Count) break;

                var line = scintilla.Lines[lineNum];
                string text = line.Text;
                int lineStart = line.Position;

                scintilla.StartStyling(lineStart);
                scintilla.SetStyling(text.Length, StyleDefault);

                int commentIdx = text.IndexOf("//");
                string codeText = commentIdx >= 0 ? text.Substring(0, commentIdx) : text;

                StyleWithPattern(codeText, lineStart, @"\b(linehide|end)\b", StyleLinehide);
                StyleWithPattern(codeText, lineStart, @"\bbreakpoint\b", StyleBreakpoint);
                StyleWithPattern(codeText, lineStart, @"\b(hide|show|skip|noskip)\b", StyleVisibility);
                StyleWithPattern(codeText, lineStart, @"\bmacro\s+\w+", StyleMacro);
                StyleWithPattern(codeText, lineStart, @"\b(wait|rep|teleport|rng|left_stick|right_stick)\b", StyleCommand);
                StyleWithPattern(codeText, lineStart, @"\b(l1|l2|l3|r1|r2|r3|triangle|circle|cross|square|select|up|down|left|right|tri|cir|x|sq|sel|st|start|U|D|L|R)\b", StyleButton);
                StyleWithPattern(codeText, lineStart, @"\b\d+\.?\d*\b", StyleNumber);

                // style comment last so it overrides color
                if (commentIdx >= 0)
                {
                    scintilla.StartStyling(lineStart + commentIdx);
                    scintilla.SetStyling(text.Length - commentIdx, StyleComment);
                }
            }
        }

        private void StyleWithPattern(string text, int lineStart, string pattern, int style)
        {
            foreach (Match m in Regex.Matches(text, pattern))
            {
                scintilla.StartStyling(lineStart + m.Index);
                scintilla.SetStyling(m.Length, style);
            }
        }

        private void Scintilla_TextChanged(object sender, EventArgs e)
        {
            CalculateFramesAndFolds();
        }

        private bool IsFrameActionLine(string text)
        {
            text = text.Trim();


            // filter out non action lines
            if (string.IsNullOrWhiteSpace(text)) return false;
            if (text.StartsWith("//")) return false;
            if (text.StartsWith("macro ")) return false;
            if (text.StartsWith("linehide")) return false;

            return true;
        }

        private void ParseMacros()
        {
            macros.Clear();
            int i = 0;
            while (i < scintilla.Lines.Count)
            {
                var line = scintilla.Lines[i];
                string text = line.Text.Trim();

                if (text.StartsWith("macro "))
                {
                    int commentIdx = text.IndexOf("//");
                    string definitionLine = commentIdx >= 0 ? text.Substring(0, commentIdx).Trim() : text.Trim();

                    definitionLine = definitionLine.TrimEnd(':');

                    var parts = definitionLine.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                    var name = parts[1];

                    var args = parts.Skip(2).Select(arg =>
                        Regex.Replace(arg, @"[^a-zA-Z0-9_]", "")
                    ).ToList();
                    var body = new List<string>();

                    int j = i + 1;
                    while (j < scintilla.Lines.Count && !string.IsNullOrWhiteSpace(scintilla.Lines[j].Text))
                    {
                        body.Add(scintilla.Lines[j].Text);
                        j++;
                    }

                    macros[name] = (args, body);
                    i = j;
                }
                else
                {
                    i++;
                }
            }
        }

        private int CalculateLineFrameCount(string text)
        {
            text = text.Trim();

            int commentIdx = text.IndexOf("//");
            if (commentIdx >= 0)
                text = text.Substring(0, commentIdx).Trim();

            if (string.IsNullOrWhiteSpace(text) || !IsFrameActionLine(text))
                return 0;

            var tokens = text.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            if (tokens.Length == 0) return 0;

            string cmd = tokens[0];

            if (macros.ContainsKey(cmd))
            {
                var (args, body) = macros[cmd];
                int macroFrames = 0;

                foreach (var macroLine in body)
                {
                    macroFrames += CalculateLineFrameCount(macroLine);
                }

                return macroFrames;
            }
            if (cmd == "wait" && tokens.Length >= 2)
            {
                Match m = Regex.Match(text, @"wait (\d+)");
                if (m.Success)
                    return int.Parse(m.Groups[1].Value);
            }

            if (cmd == "rep" && tokens.Length >= 3)
            {
                Match m = Regex.Match(text, @"rep (\d+)");
                if (m.Success)
                {
                    int count = int.Parse(m.Groups[1].Value);
                    string rest = string.Join(" ", tokens.Skip(2));

                    string repCmd = tokens[2];
                    if (macros.ContainsKey(repCmd))
                    {
                        int macroFrames = CalculateLineFrameCount(rest);
                        return count * macroFrames;
                    }

                    return count;
                }
            }
            return 1;
        }

        private void CalculateFramesAndFolds()
        {
            ParseMacros();

            int currentFrameCount = 1;
            int foldLevel = 1024;
            bool inMacro = false;
            Stack<int> foldStack = new Stack<int>();
            lineFrameCounts.Clear();
            lineFrameInfo.Clear();

            for (int i = 0; i < scintilla.Lines.Count; i++)
            {
                var line = scintilla.Lines[i];
                string text = line.Text.Trim();

                if (text.StartsWith("macro "))
                {
                    inMacro = true;
                }
                else if (inMacro && string.IsNullOrWhiteSpace(text))
                {
                    inMacro = false;
                }

                // collapse.
                if (text.StartsWith("linehide") && !text.Contains("end"))
                {
                    line.FoldLevel = foldLevel | (int)FoldLevelFlags.Header;
                    foldStack.Push(foldLevel);
                    foldLevel++;
                }
                else if (text.StartsWith("linehide") && text.Contains("end"))
                {
                    if (foldStack.Count > 0)
                        foldLevel = foldStack.Pop();
                    else if (foldLevel > 1024)
                        foldLevel--;
                    line.FoldLevel = foldLevel;
                }
                else line.FoldLevel = foldLevel;

                line.MarginStyle = StyleMarginText;

                if (!inMacro)
                {
                    lineFrameCounts[i] = currentFrameCount;
                    line.MarginText = $"  {currentFrameCount.ToString().PadLeft(5)}";

                    if (IsFrameActionLine(line.Text))
                    {
                        int duration = CalculateLineFrameCount(line.Text);

                        if (duration > 0)
                        {
                            string commandType = "single";
                            int repCount = 0;
                            int macroFramesPerRep = 0;
                            string macroName = "";

                            if (text.StartsWith("wait "))
                            {
                                commandType = "wait";
                            }
                            else if (text.StartsWith("rep "))
                            {
                                commandType = "rep";
                                Match m = Regex.Match(text, @"rep (\d+)");
                                if (m.Success)
                                {
                                    repCount = int.Parse(m.Groups[1].Value);

                                    // Check if it's repeating a macro
                                    var tokens = text.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                                    if (tokens.Length >= 3)
                                    {
                                        string repCmd = tokens[2];
                                        if (macros.ContainsKey(repCmd))
                                        {
                                            macroName = repCmd;
                                            string rest = string.Join(" ", tokens.Skip(2));
                                            macroFramesPerRep = CalculateLineFrameCount(rest);
                                        }
                                        else
                                        {
                                            macroFramesPerRep = 1; // Single frame action
                                        }
                                    }
                                }
                            }
                            else
                            {
                                // Check if it's a macro call
                                var tokens = text.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                                if (tokens.Length > 0 && macros.ContainsKey(tokens[0]))
                                {
                                    commandType = "macro";
                                    macroName = tokens[0];
                                    repCount = 1;
                                    macroFramesPerRep = duration;
                                }
                            }

                            lineFrameInfo[i] = new LineFrameInfo
                            {
                                StartFrame = currentFrameCount,
                                Duration = duration,
                                CommandType = commandType,
                                RepCount = repCount,
                                MacroFramesPerRep = macroFramesPerRep,
                                MacroName = macroName
                            };

                            currentFrameCount += duration;
                        }
                    }
                }
                else
                {
                    line.MarginText = "";
                }
            }

            lastProcessedFrameCount = currentFrameCount - 1;
            UpdateTitle();
        }

        private void Scintilla_MarginClick(object sender, MarginClickEventArgs e)
        {
            if (e.Margin == MarginFold)
            {
                var line = scintilla.Lines[scintilla.LineFromPosition(e.Position)];
                if ((line.FoldLevel & (int)FoldLevelFlags.Header) != 0)
                {
                    line.ToggleFold();
                }
            }
        }

        private void HighlightLine(int lineIndex)
        {
            if (lineIndex < 0 || lineIndex >= scintilla.Lines.Count) return;

            if (currentHighlightedLine >= 0 && currentHighlightedLine < scintilla.Lines.Count)
            {
                var prevLine = scintilla.Lines[currentHighlightedLine];
                scintilla.IndicatorClearRange(prevLine.Position, prevLine.Length);
            }

            currentHighlightedLine = lineIndex;

            var line = scintilla.Lines[lineIndex];
            scintilla.IndicatorCurrent = IndicatorFrameHighlight;
            scintilla.IndicatorFillRange(line.Position, line.Length);
        }

        private void ClearLineHighlight()
        {
            if (currentHighlightedLine >= 0 && currentHighlightedLine < scintilla.Lines.Count)
            {
                var line = scintilla.Lines[currentHighlightedLine];
                scintilla.IndicatorCurrent = IndicatorFrameHighlight;
                scintilla.IndicatorClearRange(line.Position, line.Length);
            }
            currentHighlightedLine = -1;
            currentProgressText = "";
        }

        private void SubscribeToFrameTimer()
        {
            if (Rackets2API.defaultOffset == 0) return;
            try
            {
                var api = func.api;
                var pid = api.getCurrentPID();
                frameTimerSubID = api.SubMemory(pid, Rackets2API.defaultOffset + FRAME_TIMER_OFFSET, 4, IPS3API.MemoryCondition.Changed, OnFrameTimerChanged);
            }
            catch { }
        }

        private void OnFrameTimerChanged(byte[] value)
        {
            if (!liveHighlightEnabled) return;
            uint newFrame = BitConverter.ToUInt32(value.Take(4).ToArray(), 0);

            // memory says 2 when screen shows frame: 1
            int displayFrame = (int)newFrame - 1;

            if (scintilla.InvokeRequired)
                scintilla.BeginInvoke(new Action(() => HighlightCurrentFrame(displayFrame)));
            else
                HighlightCurrentFrame(displayFrame);
        }
        private (int frameInMacro, int lineIndexInBody, string actionText) GetMacroProgress(string macroName, int frameInRep, int macroFramesPerRep, List<string> callArgs)
        {
            if (!macros.ContainsKey(macroName) || macroFramesPerRep == 0)
                return (-1, -1, "");

            var (macroArgs, macroBody) = macros[macroName];
            Dictionary<string, string> substitutionMap = new Dictionary<string, string>();
            for (int i = 0; i < macroArgs.Count && i < callArgs.Count; i++)
            {
                substitutionMap[macroArgs[i]] = callArgs[i];
            }

            int currentFrameInMacro = 1;

            for (int i = 0; i < macroBody.Count; i++)
            {
                string macroLine = macroBody[i];
                string substitutedLine = macroLine;

                foreach (var sub in substitutionMap)
                {
                    substitutedLine = Regex.Replace(substitutedLine, $@"\b{Regex.Escape(sub.Key)}\b", sub.Value);
                }
                int lineDuration = CalculateLineFrameCount(macroLine);

                if (lineDuration > 0)
                {
                    int lineFrameEnd = currentFrameInMacro + lineDuration;

                    if (frameInRep >= currentFrameInMacro && frameInRep < lineFrameEnd)
                    {
                        int frameInAction = frameInRep - currentFrameInMacro + 1;
                        string actionText = substitutedLine.Trim();

                        if (lineDuration > 1)
                        {
                            actionText += $" ({frameInAction}/{lineDuration})";
                        }

                        return (currentFrameInMacro, i, actionText);
                    }
                    currentFrameInMacro += lineDuration;
                }
            }

            return (-1, -1, "");
        }
        private void HighlightCurrentFrame(int frame)
        {
            currentFrame = frame;
            currentProgressText = "";

            // only check lines that are actual frame actions
            int foundLine = -1;
            string statusText = "No live data";

            foreach (var kvp in lineFrameInfo.OrderBy(x => x.Key))
            {
                int lineIndex = kvp.Key;
                var info = kvp.Value;

                int lineFrameEnd = info.StartFrame + info.Duration;

                if (frame >= info.StartFrame && frame < lineFrameEnd)
                {
                    foundLine = lineIndex;

                    if (info.Duration > 1)
                    {
                        int progress = frame - info.StartFrame + 1;

                        // for rep commands and macros, show which repetition were on
                        if ((info.CommandType == "rep" || info.CommandType == "macro") && info.RepCount > 0 && info.MacroFramesPerRep > 0)
                        {
                            int currentRep = (progress - 1) / info.MacroFramesPerRep + 1;
                            int frameInRep = ((progress - 1) % info.MacroFramesPerRep) + 1;

                            if (!string.IsNullOrEmpty(info.MacroName))
                            {
                                string lineText = scintilla.Lines[foundLine].Text;
                                var tokens = lineText.Trim().Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                                int tokenStartIndex = (info.CommandType == "rep") ? 3 : 1;
                                var callArgs = tokens.Skip(tokenStartIndex).ToList();

                                var (frameInMacro, lineIndexInBody, actionText) = GetMacroProgress(info.MacroName, frameInRep, info.MacroFramesPerRep, callArgs);

                                statusText = $"Frame: {frame} / {lastProcessedFrameCount}\n";

                                string macroCallDisplay = lineText.Trim();
                                if (info.CommandType == "rep")
                                {
                                    if (tokens.Length >= 3)
                                    {
                                        macroCallDisplay = string.Join(" ", tokens.Skip(2));
                                    }
                                }
                                // statusText += $"Call: {macroCallDisplay}\n";

                                statusText += $"Macro: {info.MacroName}\n";
                                statusText += $"Rep: {currentRep} / {info.RepCount}\n";
                                statusText += $"Macro Frame: {frameInRep} / {info.MacroFramesPerRep}\n";
                                statusText += $"--------------------\n";

                                if (macros.ContainsKey(info.MacroName))
                                {
                                    var (args, body) = macros[info.MacroName];

                                    Dictionary<string, string> substitutionMap = new Dictionary<string, string>();
                                    for (int i = 0; i < args.Count && i < callArgs.Count; i++)
                                    {
                                        substitutionMap[args[i]] = callArgs[i];
                                    }

                                    for (int i = 0; i < body.Count; i++)
                                    {
                                        string linePrefix = (i == lineIndexInBody) ? "-> " : "   ";
                                        string lineDisplay = body[i].Trim();

                                        string substitutedDisplay = lineDisplay;
                                        foreach (var sub in substitutionMap)
                                        {
                                            substitutedDisplay = Regex.Replace(substitutedDisplay, $@"\b{Regex.Escape(sub.Key)}\b", sub.Value);
                                        }

                                        if (i == lineIndexInBody && actionText.Contains('('))
                                        {
                                            substitutedDisplay = actionText;
                                        }

                                        statusText += $"{linePrefix}{substitutedDisplay}\n";
                                    }
                                }
                            }
                            else
                            {
                                if (info.MacroFramesPerRep > 1)
                                {
                                    currentProgressText = $" (Rep {currentRep}/{info.RepCount}, Frame {frameInRep}/{info.MacroFramesPerRep})";

                                    statusText = $"Frame: {frame} / {lastProcessedFrameCount}\n";
                                    statusText += $"Line: {foundLine + 1} / {scintilla.Lines.Count}\n";
                                    statusText += $"Rep: {currentRep} / {info.RepCount}\n";
                                    statusText += $"Frame in Rep: {frameInRep} / {info.MacroFramesPerRep}";
                                }
                                else
                                {
                                    currentProgressText = $" (Rep {currentRep}/{info.RepCount})";

                                    statusText = $"Frame: {frame} / {lastProcessedFrameCount}\n";
                                    statusText += $"Line: {foundLine + 1} / {scintilla.Lines.Count}\n";
                                    statusText += $"Rep: {currentRep} / {info.RepCount}";
                                }
                            }
                        }
                        else 
                        {
                            currentProgressText = $" ({progress}/{info.Duration})";

                            statusText = $"Frame: {frame} / {lastProcessedFrameCount}\n";
                            statusText += $"Line: {foundLine + 1} / {scintilla.Lines.Count}\n";
                            statusText += $"Wait: {progress} / {info.Duration}";
                        }
                    }
                    else
                    {
                        statusText = $"Frame: {frame} / {lastProcessedFrameCount}\n";
                        statusText += $"Line: {foundLine + 1} / {scintilla.Lines.Count}";
                        if (!string.IsNullOrEmpty(info.MacroName))
                        {
                            statusText += $"\nMacro: {info.MacroName}";
                        }
                    }

                    break;
                }
            }

            if (foundLine >= 0 && foundLine != currentHighlightedLine)
            {
                HighlightLine(foundLine);
            }

            if (statusLabel.InvokeRequired)
                statusLabel.BeginInvoke(new Action(() => statusLabel.Text = statusText));
            else
                statusLabel.Text = statusText;

            UpdateTitle();
        }

        private void Scintilla_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.S)
            {
                OnSaveFile(sender, e);
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.OemQuestion)
            {
                ToggleComment();
                e.SuppressKeyPress = true;
            }
        }

        private void ToggleComment()
        {
            int lineIndex = scintilla.CurrentLine;
            var line = scintilla.Lines[lineIndex];
            string text = line.Text.TrimStart();

            if (text.StartsWith("//"))
            {
                int pos = line.Text.IndexOf("//");
                scintilla.DeleteRange(line.Position + pos, 3);
            }
            else
            {
                int indentPos = line.Position + (line.Text.Length - text.Length);
                scintilla.InsertText(indentPos, "// ");
            }
        }

        private void UpdateTitle()
        {
            string filename = string.IsNullOrEmpty(currentFilePath) ? "Untitled" : Path.GetFileName(currentFilePath);
            int lineNum = scintilla.CurrentLine + 1;
            int totalLines = scintilla.Lines.Count;

            if (liveHighlightEnabled && currentHighlightedLine >= 0)
            {
                this.Text = $"Rackets2 TAS Editor - {filename} | Frame: {currentFrame}{currentProgressText} / {lastProcessedFrameCount} | Line: {currentHighlightedLine + 1} / {totalLines}";
            }
            else
            {
                this.Text = $"Rackets2 TAS Editor - {filename} | Frame: 1 / {lastProcessedFrameCount} | Line: {lineNum} / {totalLines}";
            }
        }

        private void LoadSampleContent()
        {
            string sample = @"// i'm a comment
// this is a sample macro with args
macro sample_macro arg1 argument dir1 d:  // live status display --->
    r1 x arg1 argument
    x dir1 
    x d

// argless macro
macro charge:
    r1
    wait 1
    r1

// linehide command lets you collapse specific sections.
linehide Hello!

// scripting starts when you start writing raw inputs/commands.
// <--- the number over to the left is the current frame count for this line.

rep 10 up
rep 5 left


rep 10 sample_macro left right up square
rep 5 sample_macro right down left up
breakpoint // pauses the game on this frame.


hide skip // hide turns off rendering, skip turns off framelimiter (on PS3, dont use this on RPCS3).
show noskip // disables the previous hide/skip commands.
teleport // teleports to coordinates, if set via Rackets2 position options.
rng 877304734 // nudges the rng by this number

linehide end

linehide Buttons
// different ways of writing button names.
up U
down D
left L
right R
circle cir
square sq
cross x
triangle tri
l1
l2 
l3
r1 
r2 
r3
select sel
start st

linehide end

charge 

right_stick 90deg // if no magnitude has been set, it'll default to 1.0. (max)
left_stick 360deg 0.5
right_stick 90deg 0.8


rep 50 r1 right_stick 270deg l2

charge 
";

            scintilla.Text = sample;
            scintilla.EmptyUndoBuffer();
        }

        private void OnOpenFile(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = "Rackets2 TAS Script|*.rtas2s|All Files|*.*";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    currentFilePath = dialog.FileName;
                    scintilla.Text = File.ReadAllText(currentFilePath);
                    scintilla.EmptyUndoBuffer();
                    UpdateTitle();
                }
            }
        }

        private void OnSaveFile(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(currentFilePath))
                OnSaveFileAs(sender, e);
            else
                File.WriteAllText(currentFilePath, scintilla.Text);
        }

        private void OnSaveFileAs(object sender, EventArgs e)
        {
            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.Filter = "Rackets2 TAS Script|*.rtas2s|All Files|*.*";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    currentFilePath = dialog.FileName;
                    File.WriteAllText(currentFilePath, scintilla.Text);
                    UpdateTitle();
                }
            }
        }

        private void TASEditorForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (frameTimerSubID != -1)
            {
                try { func.api.ReleaseSubID(frameTimerSubID); } catch { }
            }
        }
    }
}