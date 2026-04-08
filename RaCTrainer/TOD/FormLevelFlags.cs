using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace racman.TOD
{
    public partial class FormLevelFlags : Form
    {
        private IGame game;
        private uint baseAddr;
        private Dictionary<string, int> flags;
        private Dictionary<string, (int order, string displayName)> planetOrder;

        // Map from byte address to the list of (checkbox, bitPosition) in that byte
        private Dictionary<uint, List<(CheckBox box, int bit)>> byteCheckBoxes = new Dictionary<uint, List<(CheckBox, int)>>();
        private List<int> subIds = new List<int>();
        private bool updatingFromMemory = false;

        // Progressive loading state
        private List<IGrouping<string, KeyValuePair<string, int>>> planetGroups;
        private int nextGroupIndex = 0;
        private int nextY = 5;
        private Timer loadTimer;

        public FormLevelFlags(IGame game, uint baseAddr, Dictionary<string, int> flags)
            : this(game, baseAddr, flags, null)
        {
        }

        public FormLevelFlags(IGame game, uint baseAddr, Dictionary<string, int> flags, Dictionary<string, (int order, string displayName)> planetOrder)
        {
            this.game = game;
            this.baseAddr = baseAddr;
            this.flags = flags;
            this.planetOrder = planetOrder;
            InitializeComponent();
        }

        private string GetBestPlanetMatch(string flagName)
        {
            if (planetOrder == null) return null;

            string key = flagName.StartsWith("LVL_") ? flagName.Substring(4) : flagName;

            // Match longest prefix first to distinguish e.g. FASTOON_RETURN from FAST
            string bestMatch = null;
            foreach (var planet in planetOrder.Keys)
            {
                if (key.StartsWith(planet + "_") || key == planet)
                {
                    if (bestMatch == null || planet.Length > bestMatch.Length)
                        bestMatch = planet;
                }
            }

            return bestMatch;
        }

        private int GetPlanetOrder(string flagName)
        {
            var match = GetBestPlanetMatch(flagName);
            return match != null ? planetOrder[match].order : int.MaxValue;
        }

        private string GetPlanetDisplayName(string flagName)
        {
            var match = GetBestPlanetMatch(flagName);
            return match != null ? planetOrder[match].displayName : null;
        }

        private static void GetFlagLocation(uint baseAddr, int flagId, out uint byteAddr, out int bitInByte)
        {
            int wordIndex = flagId / 64;
            int bitInWord = flagId % 64;
            // Big-endian 64-bit: bit b is in byte (7 - b/8) within the word
            int byteInWord = 7 - (bitInWord / 8);
            byteAddr = baseAddr + (uint)(wordIndex * 8) + (uint)byteInWord;
            bitInByte = bitInWord % 8;
        }

        private void FormLevelFlags_Load(object sender, EventArgs e)
        {
            // Pre-sort and group by planet for progressive loading
            var sorted = flags
                .OrderBy(f => GetPlanetOrder(f.Key))
                .ThenBy(f => f.Value);

            planetGroups = sorted
                .GroupBy(f => GetPlanetDisplayName(f.Key) ?? "Other")
                .ToList();

            flagPanel.AutoScroll = false;

            loadTimer = new Timer();
            loadTimer.Interval = 1;
            loadTimer.Tick += LoadNextGroup;
            loadTimer.Start();
        }

        private void LoadNextGroup(object sender, EventArgs e)
        {
            if (nextGroupIndex >= planetGroups.Count)
            {
                loadTimer.Stop();
                loadTimer.Dispose();
                loadTimer = null;
                flagPanel.AutoScroll = true;
                return;
            }

            var group = planetGroups[nextGroupIndex++];

            flagPanel.SuspendLayout();

            if (planetOrder != null)
            {
                var header = new Label();
                header.Text = group.Key;
                header.Font = new Font(header.Font, FontStyle.Bold);
                header.AutoSize = true;
                header.Left = 5;
                header.Top = nextY;
                nextY += 20;
                flagPanel.Controls.Add(header);
            }

            // Track which byte addresses are new in this group so we subscribe after
            var newByteAddrs = new HashSet<uint>();

            foreach (var flag in group)
            {
                GetFlagLocation(baseAddr, flag.Value, out uint byteAddr, out int bitInByte);

                var box = new CheckBox();
                box.Text = flag.Key;
                box.AutoSize = true;
                box.Left = 20;
                box.Top = nextY;
                nextY += 22;

                uint capturedAddr = byteAddr;
                byte capturedMask = (byte)(1 << bitInByte);

                box.CheckStateChanged += (sn, ev) =>
                {
                    if (updatingFromMemory) return;

                    var data = game.api.ReadMemory(game.pid, capturedAddr, 1);
                    if (box.Checked)
                        data[0] = (byte)(data[0] | capturedMask);
                    else
                        data[0] = (byte)(data[0] & ~capturedMask);
                    game.api.WriteMemory(game.pid, capturedAddr, new byte[] { data[0] });
                };

                if (!byteCheckBoxes.ContainsKey(byteAddr))
                {
                    byteCheckBoxes[byteAddr] = new List<(CheckBox, int)>();
                    newByteAddrs.Add(byteAddr);
                }
                byteCheckBoxes[byteAddr].Add((box, bitInByte));

                flagPanel.Controls.Add(box);
            }

            flagPanel.ResumeLayout();

            // Subscribe to any new byte addresses from this group
            foreach (uint addr in newByteAddrs)
            {
                var boxes = byteCheckBoxes[addr];

                var subId = game.api.SubMemory(game.pid, addr, 1, (data) =>
                {
                    this.Invoke(new Action(() =>
                    {
                        updatingFromMemory = true;
                        foreach (var (box, bit) in boxes)
                        {
                            box.Checked = (data[0] & (1 << bit)) != 0;
                        }
                        updatingFromMemory = false;
                    }));
                });
                subIds.Add(subId);
            }
        }

        private void FormLevelFlags_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (loadTimer != null)
            {
                loadTimer.Stop();
                loadTimer.Dispose();
            }
            foreach (var subId in subIds) game.api.ReleaseSubID(subId);
        }
    }
}
