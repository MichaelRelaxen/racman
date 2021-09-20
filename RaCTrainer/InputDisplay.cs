using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Ratchetron
{
    public partial class InputDisplay : Form
    {
        public System.Windows.Forms.Timer timer;
        public InputDisplay()
        {
            InitializeComponent();
        }
        private void InputDisplay_Load(object sender, EventArgs e)
        {
            skinComboBox.SelectedIndex = 0;

            if (Directory.Exists("skins"))
            {
                foreach(var file in Directory.EnumerateFiles("skins"))
                {
                    skinComboBox.Items.Add(file.Replace("skins\\", ""));
                }
            }

            timer = new System.Windows.Forms.Timer();
            timer.Interval = (int)16.66667;
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
        }

        public void timer_Tick(object sender, EventArgs e)
        {
            this.Refresh();
        }

        private Image sprite = Properties.Resources.ds3b;
        private GraphicsUnit units = GraphicsUnit.Pixel;
        private void InputDisplay_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(sprite, 0, 0, new Rectangle(0, 0, 800, 558), units);

            if (Inputs.Mask.Contains(Inputs.Buttons.r3)) e.Graphics.DrawImage(sprite, 469 + (Inputs.rx * 32), 328 + (Inputs.ry * 32), new Rectangle(106, 627, 105, 105), units); 
            else e.Graphics.DrawImage(sprite, 469 + (Inputs.rx * 32), 328 + (Inputs.ry * 32), new Rectangle(0, 627, 105, 105), units); 
            if (Inputs.Mask.Contains(Inputs.Buttons.l3)) e.Graphics.DrawImage(sprite, 210 + (Inputs.lx * 32), 328 + (Inputs.ly * 32), new Rectangle(106, 627, 105, 105), units); 
            else e.Graphics.DrawImage(sprite, 210 + (Inputs.lx * 32), 328 + (Inputs.ly * 32), new Rectangle(0, 627, 105, 105), units); 

            if (Inputs.Mask.Contains(Inputs.Buttons.left)) e.Graphics.DrawImage(sprite, 74, 244, new Rectangle(0, 560, 52, 38), units);
            if (Inputs.Mask.Contains(Inputs.Buttons.right)) e.Graphics.DrawImage(sprite, 162, 244, new Rectangle(130, 560, 52, 38), units);
            if (Inputs.Mask.Contains(Inputs.Buttons.down)) e.Graphics.DrawImage(sprite, 124, 276, new Rectangle(53, 560, 38, 52), units);
            if (Inputs.Mask.Contains(Inputs.Buttons.up)) e.Graphics.DrawImage(sprite, 124, 198, new Rectangle(92, 560, 38, 52), units);

            if (Inputs.Mask.Contains(Inputs.Buttons.cross)) e.Graphics.DrawImage(sprite, 609, 303, new Rectangle(389, 560, 62, 62), units);
            if (Inputs.Mask.Contains(Inputs.Buttons.circle)) e.Graphics.DrawImage(sprite, 680, 232, new Rectangle(326, 560, 62, 62), units);
            if (Inputs.Mask.Contains(Inputs.Buttons.triangle)) e.Graphics.DrawImage(sprite, 609, 161, new Rectangle(263, 560, 62, 62), units);
            if (Inputs.Mask.Contains(Inputs.Buttons.square)) e.Graphics.DrawImage(sprite, 538, 232, new Rectangle(200, 560, 62, 62), units);

            if (Inputs.Mask.Contains(Inputs.Buttons.select)) e.Graphics.DrawImage(sprite, 291, 252, new Rectangle(460, 561, 38, 20), units);
            if (Inputs.Mask.Contains(Inputs.Buttons.start)) e.Graphics.DrawImage(sprite, 459, 252, new Rectangle(499, 561, 37, 20), units);

            if (Inputs.Mask.Contains(Inputs.Buttons.r1)) e.Graphics.DrawImage(sprite, 596, 73, new Rectangle(458, 654, 89, 27), units);
            if (Inputs.Mask.Contains(Inputs.Buttons.l1)) e.Graphics.DrawImage(sprite, 99, 73, new Rectangle(458, 654, 89, 27), units);
            if (Inputs.Mask.Contains(Inputs.Buttons.l2)) e.Graphics.DrawImage(sprite, 99, 0, new Rectangle(460, 586, 86, 65), units);
            if (Inputs.Mask.Contains(Inputs.Buttons.r2)) e.Graphics.DrawImage(sprite, 599, 0, new Rectangle(460, 586, 86, 65), units);
        }

        private void skinComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(skinComboBox.SelectedIndex == 0) sprite = Properties.Resources.ds3b;
            if(skinComboBox.SelectedIndex == 1) sprite = Properties.Resources.ds3w;
            
            if (skinComboBox.SelectedIndex > 1)
            {
                var skinName = skinComboBox.Items[skinComboBox.SelectedIndex].ToString();
                sprite = Image.FromFile($"skins\\{skinName}");
            }
        }

        private void InputDisplay_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer.Enabled = false;
        }
    }
}
