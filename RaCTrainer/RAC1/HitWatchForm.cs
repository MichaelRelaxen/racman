using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace racman
{
    public partial class HitWatchForm : Form
    {
        private rac1 game;
        private uint hitTableAddr = 0x00A14C00;
        private const int SLOT_SIZE = 0x40;
        private const int SLOT_COUNT = 64;

        // Dynamic offsets for Moby struct
        private int off_oClass;
        private int off_state;
        private int off_mClass;
        private int off_pClass;
        private int off_pUpdate;
        private int off_pVars;
        private int off_position;
        // EcoID is field74_0xb1
        private int off_ecoID;
        // BaseVal is field76_0xb4
        private int off_baseVal;
        // MaxPool is field78_0xb6
        private int off_maxPool;

        private void InitOffsets()
        {
             off_oClass = (int)Marshal.OffsetOf(typeof(rac1.Moby), nameof(rac1.Moby.oClass));
             off_state = (int)Marshal.OffsetOf(typeof(rac1.Moby), nameof(rac1.Moby.state));
             off_mClass = (int)Marshal.OffsetOf(typeof(rac1.Moby), nameof(rac1.Moby.m_class));
             off_pClass = (int)Marshal.OffsetOf(typeof(rac1.Moby), nameof(rac1.Moby.p_class));
             off_pUpdate = (int)Marshal.OffsetOf(typeof(rac1.Moby), nameof(rac1.Moby.p_update));
             off_pVars = (int)Marshal.OffsetOf(typeof(rac1.Moby), nameof(rac1.Moby.vars));
             off_position = (int)Marshal.OffsetOf(typeof(rac1.Moby), nameof(rac1.Moby.position));
             
             // Check field names for economy (based on observed 0xB1, 0xB4, 0xB6 in previous code)
             // In rac1.cs:
             // field74_0xb1 is at 0xB1
             // field76_0xb4 is at 0xB4
             // field78_0xb6 is at 0xB6
             
             off_ecoID = (int)Marshal.OffsetOf(typeof(rac1.Moby), nameof(rac1.Moby.field74_0xb1));
             off_baseVal = (int)Marshal.OffsetOf(typeof(rac1.Moby), nameof(rac1.Moby.field76_0xb4));
             off_maxPool = (int)Marshal.OffsetOf(typeof(rac1.Moby), nameof(rac1.Moby.field78_0xb6));
        }

        private Label[] slotLabels;
        private Panel[] slotPanels;

        private int currentNextIndex = -1;
        private int selectedSlotResult = -1;

        public HitWatchForm(rac1 game)
        {
            this.game = game;
            InitializeComponent();
            InitOffsets();
            SetupGrid();
        }

        private void SetupGrid()
        {
            slotLabels = new Label[SLOT_COUNT];
            slotPanels = new Panel[SLOT_COUNT];

            for (int i = 0; i < SLOT_COUNT; i++)
            {
                Panel p = new Panel();
                p.Dock = DockStyle.Fill;
                p.Margin = new Padding(1);
                p.BackColor = Color.Black;
                p.Tag = i;
                p.Click += Slot_Click;
                p.Paint += SlotPanel_Paint;

                Label l = new Label();
                l.Text = i.ToString("X2");
                l.ForeColor = Color.White;
                l.Dock = DockStyle.Fill;
                l.TextAlign = ContentAlignment.MiddleCenter;
                l.Click += Slot_Click; // Bubble click
                l.Tag = i;
                l.AutoSize = false;
                l.BackColor = Color.Transparent;

                p.Controls.Add(l);

                int row = i / 8;
                int col = i % 8;

                gridPanel.Controls.Add(p, col, row);

                slotLabels[i] = l;
                slotPanels[i] = p;
            }
        }

        private void SlotPanel_Paint(object sender, PaintEventArgs e)
        {
            Panel p = sender as Panel;
            if (p != null && p.Tag is int idx && idx == currentNextIndex)
            {
                // Draw a border to highlight the next writing slot
                using (Pen pen = new Pen(Color.Cyan, 3))
                {
                    pen.Alignment = System.Drawing.Drawing2D.PenAlignment.Inset;
                    e.Graphics.DrawRectangle(pen, 0, 0, p.Width - 1, p.Height - 1);
                }
            }
        }

        private void Slot_Click(object sender, EventArgs e)
        {
            if (sender is Control c && c.Tag is int idx)
            {
                selectedSlotResult = idx;
                UpdateDetails(idx);
            }
        }

        private void HitWatchForm_Load(object sender, EventArgs e)
        {

        }

        private void HitWatchForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            pollTimer.Enabled = false;
        }

        private void pollTimer_Tick(object sender, EventArgs e)
        {
            PollMemory();
        }

        private byte[] lastBuffer = null;

        private void PollMemory()
        {
            if (game == null || game.api == null) return;
            
            // Read next index
            byte[] idxBytes = game.api.ReadMemory(game.pid, 0x00A10954, 4);
            int newNextIndex = -1;
            if (idxBytes != null)
            {
                Array.Reverse(idxBytes);
                uint val = BitConverter.ToUInt32(idxBytes, 0);
                newNextIndex = (int)(val & 0x3F);
            }

            if (currentNextIndex != newNextIndex)
            {
                int old = currentNextIndex;
                currentNextIndex = newNextIndex;
                if (old >= 0 && old < SLOT_COUNT) slotPanels[old].Invalidate();
                if (currentNextIndex >= 0 && currentNextIndex < SLOT_COUNT) slotPanels[currentNextIndex].Invalidate();
            }

            // Read entire table
            byte[] buf = game.api.ReadMemory(game.pid, hitTableAddr, SLOT_SIZE * SLOT_COUNT);
            if (buf == null || buf.Length != SLOT_SIZE * SLOT_COUNT) return;

            // Only update if changed? No, visual update might be needed anyway, 
            // but we can optimize later.
            
            for (int i = 0; i < SLOT_COUNT; i++)
            {
                int offset = i * SLOT_SIZE;
                byte[] slotBytes = new byte[SLOT_SIZE];
                Array.Copy(buf, offset, slotBytes, 0, SLOT_SIZE);
                ProcessSlot(i, slotBytes);
            }

            if (selectedSlotResult != -1)
            {
                UpdateDetails(selectedSlotResult); // Refresh details if data changed
            }
            
            lastBuffer = buf;
        }

        private void ProcessSlot(int idx, byte[] data)
        {
            // Offsets (from visu.py plan/user request):
            // Victim: 0x34 (u32)
            // Src: 0x20 (u32)
            // Flags: 0x24 (u32)
            // Type: 0x2A (u16)
            
            uint victim = ReadU32(data, 0x34);
            uint cur_src = ReadU32(data, 0x20);
            uint flags = ReadU32(data, 0x24);
            ushort type = ReadU16(data, 0x2A);

            bool isEmpty = IsAllZero(data);

            ushort oClass = 0;
            bool hasOClass = false;

            ushort Parent_oClass = 0;
            bool Parent_hasOClass = false;

            if (victim != 0)
            {
                byte[] oBytes = game.api.ReadMemory(game.pid, victim + (uint)off_oClass, 2);
                if (oBytes != null && oBytes.Length == 2)
                {
                    Array.Reverse(oBytes); // Big Endian for PS3
                    oClass = BitConverter.ToUInt16(oBytes, 0);
                    hasOClass = true;
                }
            }

            if (cur_src != 0)
            {
                byte[] _oBytes = game.api.ReadMemory(game.pid, cur_src + (uint)off_oClass, 2);
                if (_oBytes != null && _oBytes.Length == 2)
                {
                    Array.Reverse(_oBytes); // Big Endian for PS3
                    Parent_oClass = BitConverter.ToUInt16(_oBytes, 0);
                    Parent_hasOClass = true;
                }
            }

            Color c = Color.Black;

            if (isEmpty)
            {
                c = Color.Black;
            }
            else if (victim == 0)
            {
                c = Color.Gray;
            }
            else
            {
                if (type == 0x04E9)
                {
                    c = Color.Orange;
                }
                else if (type == 0x05F3)
                {
                    bool isGreen = false;
                    if (flags == 0x008B0000)
                    {
                        // Check oClass using cached value
                        if (hasOClass && oClass >= 0x120 && oClass <= 0x2B0)
                        {
                            isGreen = true;
                        }
                    }
                    c = isGreen ? Color.Green : Color.Red;
                }
                else
                {
                    if (oClass == 419)
                    {
                        c = Color.Blue;
                    }
                    else if(oClass == 1451)
                    {
                        c = Color.Purple;
                    }
                    else
                    {
                        c = Color.Black;
                    }
                }
            }

            // Text update
            string cellText;
            if (isEmpty)
            {
                cellText = idx.ToString("X2");
            }
            else
            {
                cellText = $"{type:X4}";
                if (hasOClass)
                {
                    cellText += $"\r\n{oClass:X4}";
                }
            }

            if (slotLabels[idx].Text != cellText)
            {
                slotLabels[idx].Text = cellText;
            }
            
            // ForeColor update for contrast
            Color textColor = (c == Color.Yellow || c == Color.Orange) ? Color.Black : Color.White;
            if (slotLabels[idx].ForeColor != textColor)
            {
                slotLabels[idx].ForeColor = textColor;
            }

            // Update UI
            if (slotPanels[idx].BackColor != c)
            {
                slotPanels[idx].BackColor = c;
            }
            // Ensure we repaint for the border if this is the next index
            if (idx == currentNextIndex)
            {
                // Force repaint to draw border on top of new BackColor
                slotPanels[idx].Invalidate();
            }
        }

        private void UpdateDetails(int idx)
        {
            if (lastBuffer == null) return;
            int offset = idx * SLOT_SIZE;
            if (offset + SLOT_SIZE > lastBuffer.Length) return;

            byte[] data = new byte[SLOT_SIZE];
            Array.Copy(lastBuffer, offset, data, 0, SLOT_SIZE);

            uint victim = ReadU32(data, 0x34);
            uint src = ReadU32(data, 0x20);
            uint flags = ReadU32(data, 0x24);
            ushort type = ReadU16(data, 0x2A);
            float impact = ReadFloat(data, 0x10);
            float damage = ReadFloat(data, 0x2C);
            // Robot: check if src matches robot ptr? We don't have robot ptr here easily yet, skipping.

            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Slot {idx:02} ({(idx == selectedSlotResult ? "Selected" : "")})");
            sb.AppendLine($"Victim: 0x{victim:X8}");
            sb.AppendLine($"Source: 0x{src:X8}");
            sb.AppendLine($"Flags : 0x{flags:X8}");
            sb.AppendLine($"Type  : 0x{type:X4}");
            sb.AppendLine($"Impact: {impact:F3}   Dmg: {damage:F3}");

            if (victim != 0 && game != null && game.api != null)
            {
                try
                {
                    // Read detailed moby info
                    
                    // 1. Basic Moby Info
                    byte[] bState = game.api.ReadMemory(game.pid, victim + (uint)off_state, 1);
                    byte state = (bState != null && bState.Length > 0) ? bState[0] : (byte)0;
                    
                    byte[] bMClass = game.api.ReadMemory(game.pid, victim + (uint)off_mClass, 1);
                    byte mClass = (bMClass != null && bMClass.Length > 0) ? bMClass[0] : (byte)0;

                    byte[] bPClass = game.api.ReadMemory(game.pid, victim + (uint)off_pClass, 4);
                    uint pClass = 0; 
                    if (bPClass != null) { Array.Reverse(bPClass); pClass = BitConverter.ToUInt32(bPClass, 0); }

                    byte[] bOClass = game.api.ReadMemory(game.pid, victim + (uint)off_oClass, 2);
                    ushort oClass = 0;
                    if (bOClass != null) { Array.Reverse(bOClass); oClass = BitConverter.ToUInt16(bOClass, 0); }

                    byte[] bUpFunc = game.api.ReadMemory(game.pid, victim + (uint)off_pUpdate, 4);
                    uint upFunc = 0;
                    if (bUpFunc != null) { Array.Reverse(bUpFunc); upFunc = BitConverter.ToUInt32(bUpFunc, 0); }
                    
                    sb.AppendLine();
                    sb.AppendLine("[Moby Details]");
                    sb.AppendLine($" oClass=0x{oClass:X4} ({oClass})  State=0x{state:X2}");
                    sb.AppendLine($" mClass=0x{mClass:X2}        pClass=0x{pClass:X8}");
                    sb.AppendLine($" upFunc=0x{upFunc:X8}");

                    // 2. pVars
                    byte[] bPVars = game.api.ReadMemory(game.pid, victim + (uint)off_pVars, 4);
                    uint pVars = 0;
                    if (bPVars != null) { Array.Reverse(bPVars); pVars = BitConverter.ToUInt32(bPVars, 0); }

                    string hpStr = "N/A";
                    if (pVars != 0)
                    {
                         byte[] bHP = game.api.ReadMemory(game.pid, pVars + 0x20, 4);
                         if (bHP != null)
                         {
                             byte[] bHPFloat = new byte[4]; Array.Copy(bHP, bHPFloat, 4); Array.Reverse(bHPFloat);
                             float fHP = BitConverter.ToSingle(bHPFloat, 0);
                             
                             byte[] bHPUint = new byte[4]; Array.Copy(bHP, bHPUint, 4); Array.Reverse(bHPUint);
                             uint uHP = BitConverter.ToUInt32(bHPUint, 0);
                             
                             hpStr = $"{fHP:F1} (0x{uHP:X8})";
                         }
                    }

                    sb.AppendLine();
                    sb.AppendLine("[pVars]");
                    sb.AppendLine($" @78: 0x{pVars:X8} -> HP: {hpStr}");

                    // 3. Position
                    byte[] bPos = game.api.ReadMemory(game.pid, victim + (uint)off_position, 12);
                    if (bPos != null && bPos.Length == 12)
                    {
                        float x = ReadFloat(bPos, 0); // Helper handles reverse
                        float y = ReadFloat(bPos, 4);
                        float z = ReadFloat(bPos, 8);
                        sb.AppendLine();
                        sb.AppendLine("[Position]");
                        sb.AppendLine($" ({x:F4}, {y:F4}, {z:F4})");
                    }

                    // 4. Economy
                    byte[] bEcoID = game.api.ReadMemory(game.pid, victim + (uint)off_ecoID, 1);
                    byte ecoID = (bEcoID != null && bEcoID.Length > 0) ? bEcoID[0] : (byte)0;

                    byte[] bBase = game.api.ReadMemory(game.pid, victim + (uint)off_baseVal, 2);
                    ushort baseVal = 0;
                    if (bBase != null) { Array.Reverse(bBase); baseVal = BitConverter.ToUInt16(bBase, 0); }

                    byte[] bMax = game.api.ReadMemory(game.pid, victim + (uint)off_maxPool, 2);
                    ushort maxPool = 0;
                    if (bMax != null) { Array.Reverse(bMax); maxPool = BitConverter.ToUInt16(bMax, 0); }

                    string afStr = (ecoID == 0xFF || ecoID == 0xFE) ? "OFF" : "ON";
                    
                    sb.AppendLine();
                    sb.AppendLine("[Economy]");
                    sb.AppendLine($" EcoID=0x{ecoID:X2} ({afStr})");
                    sb.AppendLine($" Base ={baseVal,-6}      Max ={maxPool}");

                }
                catch (Exception ex)
                {
                    sb.AppendLine($"\n[Error reading Moby]: {ex.Message}");
                }
            }

            if (src != 0 && game != null && game.api != null)
            {
                try 
                {
                     byte[] bSOClass = game.api.ReadMemory(game.pid, src + (uint)off_oClass, 2);
                     ushort sOClass = 0;
                     if (bSOClass != null) { Array.Reverse(bSOClass); sOClass = BitConverter.ToUInt16(bSOClass, 0); }

                     sb.AppendLine();
                     sb.AppendLine("[Src/Parent]");
                     sb.AppendLine($" oClass=0x{sOClass:X4} ({sOClass})");
                }
                catch {}
            }

            detailsLabel.Text = sb.ToString();
        }


        // Helpers for Big Endian reading (PS3)
        private uint ReadU32(byte[] buf, int offset)
        {
            if (offset + 4 > buf.Length) return 0;
            byte[] temp = new byte[4];
            Array.Copy(buf, offset, temp, 0, 4);
            Array.Reverse(temp);
            return BitConverter.ToUInt32(temp, 0);
        }

        private ushort ReadU16(byte[] buf, int offset)
        {
            if (offset + 2 > buf.Length) return 0;
            byte[] temp = new byte[2];
            Array.Copy(buf, offset, temp, 0, 2);
            Array.Reverse(temp); // BE
            return BitConverter.ToUInt16(temp, 0);
        }

        private float ReadFloat(byte[] buf, int offset)
        {
            if (offset + 4 > buf.Length) return 0;
            byte[] temp = new byte[4];
            Array.Copy(buf, offset, temp, 0, 4);
            Array.Reverse(temp); // BE
            return BitConverter.ToSingle(temp, 0);
        }

        private bool IsAllZero(byte[] buf)
        {
            foreach (byte b in buf) if (b != 0) return false;
            return true;
        }

    }
}
