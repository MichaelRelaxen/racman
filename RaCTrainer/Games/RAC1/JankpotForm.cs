using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace racman
{
    public partial class JankpotForm : Form
    {
        private rac1 game;
        private int currentBpm = 0;
        private double currentMult = 1.0;
        private bool updatingFromMemory = false;

        public JankpotForm(rac1 game)
        {
            this.game = game;
            InitializeComponent();
            this.pbGraph.Paint += PbGraph_Paint;
        }

        private void JankpotForm_Load(object sender, EventArgs e)
        {

        }

        private void updateTimer_Tick(object sender, EventArgs e)
        {
            // Read Values
            // Using user-provided and existing addresses
            // jankpotState => 0x00a15f2c
            // jankpotBolts => 0x00a0fd18
            // jankpotTimer => 0x00a0fd14
            // ngPlusGoodies => 0x00969cd0
            // ngPlusState => 0x0096c9fc
            // boltCount => 0x969CA0 (existing)

            byte[] stateBytes = game.api.ReadMemory(game.pid, rac1.addr.jankpotState, 4);
            byte[] boltsBytes = game.api.ReadMemory(game.pid, rac1.addr.jankpotBolts, 4);
            byte[] timerBytes = game.api.ReadMemory(game.pid, rac1.addr.jankpotTimer, 4);
            byte[] goodiesBytes = game.api.ReadMemory(game.pid, rac1.addr.ngPlusGoodies, 4);
            byte[] ngPlusBytes = game.api.ReadMemory(game.pid, rac1.addr.ngPlusState, 4);
            byte[] playerBoltsBytes = game.api.ReadMemory(game.pid, rac1.addr.boltCount, 4);

            if (stateBytes == null || boltsBytes == null) return;

            uint fk_state = BitConverter.ToUInt32(stateBytes.Reverse().ToArray(), 0);
            uint fk_bolts = BitConverter.ToUInt32(boltsBytes.Reverse().ToArray(), 0);
            uint fk_timer = BitConverter.ToUInt32(timerBytes.Reverse().ToArray(), 0);
            uint goodies = BitConverter.ToUInt32(goodiesBytes.Reverse().ToArray(), 0);
            uint ngPlus = BitConverter.ToUInt32(ngPlusBytes.Reverse().ToArray(), 0);
            uint curr_bolts = BitConverter.ToUInt32(playerBoltsBytes.Reverse().ToArray(), 0);


            // Logic from jankpot.py
            /*
            # Logic
            ng_mult = 1.0
            if (goodies != 0) or (ngplus != 0):
                ng_mult = 2.0
            
            jank_mult = 1.0
            jank_active = (jk_state != 0)
            
            if jank_active:
                if jk_bolts > 596524:
                    jank_mult = 5.0
                elif jk_timer > 0:
                    bpm = int((3600 * jk_bolts) / jk_timer)
                    if ng_mult > 1.5:
                            bpm = int(bpm / 2)
                    
                    if bpm < 150:
                        if bpm < 91:
                            jank_mult = 5.0
                        else:
                            jank_mult = (190 - bpm) / 20.0
                    else:
                            if bpm < 301:
                                jank_mult = 1.0
                            elif bpm < 352:
                                jank_mult = (360 - bpm) / 20.0
                            else:
                                jank_mult = 0.1
            */

            double ng_mult = 1.0;
            if (goodies != 0 || ngPlus != 0)
            {
                ng_mult = 2.0;
            }

            double jank_mult = 1.0;
            bool jank_active = fk_state != 0;

            updatingFromMemory = true;
            cbJankpotEnabled.Checked = jank_active;
            updatingFromMemory = false;

            if (jank_active)
            {
                if (fk_bolts >= 596524)
                {
                    jank_mult = 5.0;
                }
                else if (fk_timer > 0)
                {
                    // Python: bpm = int((3600 * jk_bolts) / jk_timer)
                    // Note: Python division / is float division usually, but int() cast makes it floor. 
                    // In C#, integer division is floor by default. 
                    // However, let's use double to be safe and match logic 
                    double bpm_calc = (3600.0 * fk_bolts) / fk_timer;
                    int bpm = (int)bpm_calc;

                    if (ng_mult > 1.5)
                    {
                        bpm = bpm / 2;
                    }

                    if (bpm < 150)
                    {
                        if (bpm < 91)
                        {
                            jank_mult = 5.0;
                        }
                        else
                        {
                            jank_mult = (int)((190 - bpm) / 20.0);
                        }
                    }
                    else
                    {
                        if (bpm < 301)
                        {
                            jank_mult = 1.0;
                        }
                        else if (bpm < 352)
                        {
                            jank_mult = (int)((360 - bpm) / 80.0);
                        }
                        else
                        {
                            jank_mult = 0.1;
                        }
                    }

                    // Update fields for graph
                    currentBpm = bpm;
                    currentMult = jank_mult;
                }
            }
            else
            {
               // Reset graph pos: BPM 0 -> Mult 5.0
               currentBpm = 0;
               currentMult = 5.0;
            }
            
            pbGraph.Invalidate();


            // Colors
            Color j_color = Color.Black;
            if (jank_active)
            {
                if (jank_mult >= 5.0)
                    j_color = Color.FromArgb(0, 170, 0); // #00AA00
                else if (jank_mult > 1.0)
                    j_color = Color.Purple; // Replaced Yellow with Purple
                else if (jank_mult < 1.0)
                    j_color = Color.Red;
                else
                    j_color = Color.Black; // 1.0x
            }

            // Update UI State
            string state_txt = "OFF";
            Color state_col = Color.Gray;

            if (jank_active)
            {
                if (fk_bolts >= 596524)
                {
                    state_txt = "INFINITJANK";
                    // Cycle Rainbow
                    state_col = rainbowColors[colorIndex];
                    colorIndex = (colorIndex + 1) % rainbowColors.Length;
                }
                else
                {
                    state_txt = "JANKPOT ON";
                    state_col = j_color;
                }
            }
            else
            {
                 state_txt = "OFF";
                 state_col = Color.Gray;
            }

            lblState.Text = state_txt;
            lblState.ForeColor = state_col;

            lblMultiplier.Text = string.Format("{0:0.00}x", jank_mult);
            lblMultiplier.ForeColor = j_color;

            lblJankBolts.Text = fk_bolts.ToString();
            lblJankBolts.ForeColor = j_color;

            TimeSpan t = TimeSpan.FromSeconds(fk_timer / 60.0);
            string timeStr = "";
            if (t.Hours > 0)
                timeStr = string.Format("{0:D2}:{1:D2}:{2:D2}", t.Hours, t.Minutes, t.Seconds);
            else
                timeStr = string.Format("{0:D2}:{1:D2}", t.Minutes, t.Seconds);
            
            lblJankTimer.Text = timeStr;
            lblJankTimer.ForeColor = j_color;

            lblPlayerBolts.Text = curr_bolts.ToString();

            lblNGPlus.Text = string.Format("NG+ Mult: {0:0.0}x", ng_mult);
        }

        private void cbJankpotEnabled_CheckedChanged(object sender, EventArgs e)
        {
             if (updatingFromMemory) return;

             // If Checked -> Write 1 (Enabled)
             // If Unchecked -> Write 0 (Disabled / Reset)
             byte[] val = cbJankpotEnabled.Checked ? new byte[] { 1, 0, 0, 0 } : new byte[] { 0, 0, 0, 0 };
             game.api.WriteMemory(game.pid, rac1.addr.jankpotState, 4, val);
        }

        private void btnInfJank_Click(object sender, EventArgs e)
        {
            // Write 596524 to Jankpot Bolts
            // 596524 = 0x00091A2C -> Little Endian: 2C 1A 09 00
            uint val = 596524;
            game.api.WriteMemory(game.pid, rac1.addr.jankpotBolts, 4, BitConverter.GetBytes(val).Reverse().ToArray());

            // Turn ON Jankpot
            game.api.WriteMemory(game.pid, rac1.addr.jankpotState, 4, new byte[] { 1, 0, 0, 0 });
        }

        private void lblHelp_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Jankpot is an intended mechanic called \"bolt mining\"\n\n" +
                            "It turns on if you kill an enemy in a segment already completed and reloaded\n" +
                            "It turns off if you kill an enemy in other conditions\n\n" +
                            "It will multiply the bolts spawned by crates and enemies\n" +
                            "depending on the bolts you earned and the time spent in this mode\n\n" +
                            "Infinite Jankpot overflows the bolt counter and guarantees permanent x5",
                            "Jankpot Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private int colorIndex = 0;
        private readonly Color[] rainbowColors = new Color[] 
        { 
            Color.Red, Color.Orange, Color.Yellow, Color.Green, Color.Blue, Color.Indigo, Color.Violet 
        };

        private void txtJankBolts_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (uint.TryParse(txtJankBolts.Text, out uint val))
                {
                    game.api.WriteMemory(game.pid, rac1.addr.jankpotBolts, 4, BitConverter.GetBytes(val).Reverse().ToArray());
                }
            }
        }

        private void txtJankTimer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (float.TryParse(txtJankTimer.Text, out float seconds))
                {
                    uint frames = (uint)(seconds * 60.0f);
                    game.api.WriteMemory(game.pid, rac1.addr.jankpotTimer, 4, BitConverter.GetBytes(frames).Reverse().ToArray());
                }
            }
        }

        private void txtPlayerBolts_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (uint.TryParse(txtPlayerBolts.Text, out uint val))
                {
                    game.api.WriteMemory(game.pid, rac1.addr.boltCount, 4, BitConverter.GetBytes(val).Reverse().ToArray());
                }
            }
        }

        private void PbGraph_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            int w = pbGraph.Width;
            int h = pbGraph.Height;

            // Clear
            g.Clear(Color.White);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // X Axis: 0 to 400 BPM
            // Y Axis: 0.0 to 5.0 Mult
            float maxBpm = 400f;
            float maxMult = 5.0f;

            Func<float, float> GetX = (bpm) => (bpm / maxBpm) * w;
            Func<float, float> GetY = (mult) => h - ((mult / maxMult) * h);

            // 1. Background Zones
            // Green: < 91
            float x91 = GetX(91);
            g.FillRectangle(new SolidBrush(Color.FromArgb(50, 0, 255, 0)), 0, 0, x91, h);

            // Normal: 91 - 150 (implied transparent/white or yellow?)
            // Logic said:
            // if (bpm < 150) ...
            // else { if (bpm < 301) ... else if (bpm < 352) ... }
            
            // Red: 150 - 300
            float x150 = GetX(150);
            float x301 = GetX(301);
            g.FillRectangle(new SolidBrush(Color.FromArgb(50, 255, 0, 0)), x150, 0, x301 - x150, h);

            // Violet: 301 - 351 (Logic says 352 check, text says 351, logic says <352 so 351 is included)
            float x352 = GetX(352); 
            g.FillRectangle(new SolidBrush(Color.FromArgb(50, 148, 0, 211)), x301, 0, x352 - x301, h);

            // 2. Draw Curve
            List<PointF> points = new List<PointF>();
            for (int b = 0; b <= 400; b++)
            {
                float m = 0f;
                if (b < 150)
                {
                    if (b < 91) m = 5.0f;
                    else m = (int)((190 - b) / 20.0f);
                }
                else
                {
                    if (b < 301) m = 1.0f;
                    else if (b < 352) m = (int)((360 - b) / 80.0f);
                    else m = 0.1f;
                }
                points.Add(new PointF(GetX(b), GetY(m)));
            }
            if (points.Count > 1)
            {
                g.DrawLines(new Pen(Color.Black, 2.0f), points.ToArray());
            }

            // 3. Draw Current Point
            float px;
            bool isOverflow = false;

            if (currentBpm > maxBpm)
            {
                px = w - 10; // Clamp to right edge
                isOverflow = true;
            }
            else
            {
                px = GetX(currentBpm);
            }

            float py = GetY((float)currentMult);
            float r = 5;

            // Draw Point
            g.FillEllipse(Brushes.Blue, px - r, py - r, r * 2, r * 2);
            g.DrawEllipse(Pens.Black, px - r, py - r, r * 2, r * 2);

            if (isOverflow)
            {
                 // Draw > above
                 using (Font f = new Font("Segoe UI", 10, FontStyle.Bold))
                 {
                     g.DrawString(">", f, Brushes.Red, px - 6, py - 25);
                 }
            }

            // Draw Current BPM Text (High Middle)
            using (Font f = new Font("Segoe UI", 12, FontStyle.Bold))
            {
                string bpmText = $"BPM: {currentBpm}";
                SizeF size = g.MeasureString(bpmText, f);
                float tx = (w - size.Width) / 2;
                float ty = 10; // Top margin
                g.DrawString(bpmText, f, Brushes.Black, tx, ty);
            }
        }
    }
}
