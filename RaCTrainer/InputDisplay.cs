using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;

namespace racman
{
    public partial class InputDisplay : Form
    {
        enum buttons
        {
            l2 = 0x1,
            r2 = 0x2,
            l1 = 0x4,
            r1 = 0x8,
            triangle = 0x10,
            circle = 0x20,
            cross = 0x40,
            square = 0x80,
            select = 0x100,
            l3 = 0x200,
            r3 = 0x400,
            start = 0x800,
            up = 0x1000,
            right = 0x2000,
            down = 0x4000,
            left = 0x8000,
        }

        uint mask_offset;
        uint analog_offset;

        string gameID = AttachPS3Form.game;

        List<buttons> mask = new List<buttons>();

        float rx = 0.0f;
        float ry = 0.0f;
        float lx = 0.0f;
        float ly = 0.0f;

        public System.Windows.Forms.Timer timer;
        public InputDisplay()
        {
            InitializeComponent();
        }
        private void InputDisplay_Load(object sender, EventArgs e)
        {
            if(gameID == "NPEA00385")
            {
                mask_offset = 0x964AF0;
                analog_offset = 0x964A40;
            }
            if(gameID == "NPEA00387")
            {
                mask_offset = 0xD99370;
                analog_offset = 0xD9954C;
            }

            skinComboBox.SelectedIndex = 0;

            this.UpdateButtons();

            timer = new System.Windows.Forms.Timer();
            timer.Interval = (int)16.66667;
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
        }

        public void timer_Tick(object sender, EventArgs e)
        {
            this.UpdateButtons();
            this.Refresh();
        }
        private static List<buttons> DecodeMask(int mask)  
        {
            var buttons = (buttons)mask;
            var list = new List<buttons>();

            foreach (buttons button in Enum.GetValues(typeof(buttons))) 
            {
                if (buttons.HasFlag(button))
                    list.Add(button);
            }
            return list;
        }
        private void UpdateButtons()
        {
            mask = DecodeMask(Convert.ToInt32(func.ReadMemory(AttachPS3Form.ip, AttachPS3Form.pid, mask_offset, 4), 16));

            string analogs = func.ReadMemory(AttachPS3Form.ip, AttachPS3Form.pid, analog_offset, 16);

            rx = func.HexToFloat(analogs.Substring(0, 8));
            ry = func.HexToFloat(analogs.Substring(8, 8));
            lx = func.HexToFloat(analogs.Substring(16, 8));
            ly = func.HexToFloat(analogs.Substring(24, 8));
        }

        private Image sprite = Properties.Resources.ds3b;
        private GraphicsUnit units = GraphicsUnit.Pixel;
        private void InputDisplay_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(sprite, 0, 0, new Rectangle(0, 0, 800, 558), units);

            if (mask.Contains(buttons.r3))
            {
                e.Graphics.DrawImage(sprite, 469 + (rx * 32), 328 + (ry * 32), new Rectangle(106, 627, 105, 105), units); // Right Stick + R3 Press
            }
            else
            {
                e.Graphics.DrawImage(sprite, 469 + (rx * 32), 328 + (ry * 32), new Rectangle(0, 627, 105, 105), units); // Right Stick
            }
            if (mask.Contains(buttons.l3))
            {
                e.Graphics.DrawImage(sprite, 210 + (lx * 32), 328 + (ly * 32), new Rectangle(106, 627, 105, 105), units); // Left Stick + L3 Press
            }
            else
            {
                e.Graphics.DrawImage(sprite, 210 + (lx * 32), 328 + (ly * 32), new Rectangle(0, 627, 105, 105), units); // Left Stick
            }

            if (mask.Contains(buttons.left)) e.Graphics.DrawImage(sprite, 74, 244, new Rectangle(0, 560, 52, 38), units);
            if (mask.Contains(buttons.right)) e.Graphics.DrawImage(sprite, 162, 244, new Rectangle(130, 560, 52, 38), units);
            if (mask.Contains(buttons.down)) e.Graphics.DrawImage(sprite, 124, 276, new Rectangle(53, 560, 38, 52), units);
            if (mask.Contains(buttons.up)) e.Graphics.DrawImage(sprite, 124, 198, new Rectangle(92, 560, 38, 52), units);

            if (mask.Contains(buttons.cross)) e.Graphics.DrawImage(sprite, 609, 303, new Rectangle(389, 560, 62, 62), units);
            if (mask.Contains(buttons.circle)) e.Graphics.DrawImage(sprite, 680, 232, new Rectangle(326, 560, 62, 62), units);
            if (mask.Contains(buttons.triangle)) e.Graphics.DrawImage(sprite, 609, 161, new Rectangle(263, 560, 62, 62), units);
            if (mask.Contains(buttons.square)) e.Graphics.DrawImage(sprite, 538, 232, new Rectangle(200, 560, 62, 62), units);

            if (mask.Contains(buttons.select)) e.Graphics.DrawImage(sprite, 291, 252, new Rectangle(460, 561, 38, 20), units);
            if (mask.Contains(buttons.start)) e.Graphics.DrawImage(sprite, 459, 252, new Rectangle(499, 561, 37, 20), units);

            if (mask.Contains(buttons.r1)) e.Graphics.DrawImage(sprite, 596, 73, new Rectangle(458, 654, 89, 27), units);
            if (mask.Contains(buttons.l1)) e.Graphics.DrawImage(sprite, 99, 73, new Rectangle(458, 654, 89, 27), units);
            if (mask.Contains(buttons.l2)) e.Graphics.DrawImage(sprite, 99, 0, new Rectangle(460, 586, 86, 65), units);
            if (mask.Contains(buttons.r2)) e.Graphics.DrawImage(sprite, 599, 0, new Rectangle(460, 586, 86, 65), units);
        }

        private void skinComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(skinComboBox.SelectedIndex == 0) sprite = Properties.Resources.ds3b;
            if(skinComboBox.SelectedIndex == 1) sprite = Properties.Resources.ds3w;
        }

        private void InputDisplay_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
