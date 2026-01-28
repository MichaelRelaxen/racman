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
        public JankpotForm(rac1 game)
        {
            this.game = game;
            InitializeComponent();
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

            if (jank_active)
            {
                if (fk_bolts > 596524)
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
                            jank_mult = (190 - bpm) / 20.0;
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
                            jank_mult = (360 - bpm) / 20.0;
                        }
                        else
                        {
                            jank_mult = 0.1;
                        }
                    }
                }
            }


            // Colors
            Color j_color = Color.Black;
            if (jank_active)
            {
                if (jank_mult >= 5.0)
                    j_color = Color.FromArgb(0, 170, 0); // #00AA00
                else if (jank_mult > 1.0)
                    j_color = Color.FromArgb(221, 221, 0); // #DDDD00
                else if (jank_mult < 1.0)
                    j_color = Color.Red;
                else
                    j_color = Color.Black; // 1.0x
            }

            // Update UI State
            string state_txt = "OFF";
            if (jank_active)
            {
                if (fk_bolts > 596524)
                    state_txt = "JANKPOT OVERFLOW";
                else
                    state_txt = "JANKPOT ON";
            }
            
            Color state_col = jank_active ? j_color : Color.Gray;

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

        private void btnResetState_Click(object sender, EventArgs e)
        {
             game.api.WriteMemory(game.pid, rac1.addr.jankpotState, 4, new byte[] { 0, 0, 0, 0 });
        }

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
    }
}
