using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace racman
{
    public partial class InputDisplay : Form
    {
        public struct InputPlot
        {
            public int drawX { get; set; }
            public int drawY { get; set; }
            public int spriteX { get; set; }
            public int spriteY { get; set; }
            public int spriteWidth { get; set; }
            public int spriteHeight { get; set; }
        }

        class ControllerSkin
        {

            public Image image;
            public Dictionary<string, InputPlot> buttons;
            public int analogPitch = 32;


            public static ControllerSkin Load(string skinPath)
            {
                var skin = new ControllerSkin();
                skin.buttons = new Dictionary<string, InputPlot>();

                var config = File.ReadAllText(skinPath + "\\skin.txt");

                foreach (var line in config.Split('\n'))
                {
                    if (line.Length < 2 || line[0] == '#')
                    {
                        continue;
                    }

                    var components = line.Split(':');
                    if (components.Length < 2) 
                    {
                        continue;
                    }

                    string buttonName = components[0];

                    if (buttonName == "imageName")
                    {
                        skin.image = Image.FromFile(skinPath + "\\" + components[1].Trim());
                        continue;
                    }

                    if (buttonName == "analogPitch")
                    {
                        skin.analogPitch = int.Parse(components[1].Trim());
                        continue;
                    }

                    var plot = components[1].Split(',').Select(thing => int.Parse(thing.Trim())).ToArray();
                    
                    if (plot.Length < 6)
                    {
                        continue;
                    }


                    var inputPlot = new InputPlot();
                    inputPlot.drawX         = plot[0];
                    inputPlot.drawY         = plot[1];
                    inputPlot.spriteX       = plot[2];
                    inputPlot.spriteY       = plot[3];
                    inputPlot.spriteWidth   = plot[4];
                    inputPlot.spriteHeight  = plot[5];

                    skin.buttons[buttonName] = inputPlot;
                }

                return skin;
            }
        }

        public System.Windows.Forms.Timer timer;
        ControllerSkin controllerSkin;

        public InputDisplay()
        {
            InitializeComponent();
        }
        private void InputDisplay_Load(object sender, EventArgs e)
        {
            if (Directory.Exists("controllerskins"))
            {
                foreach(var skinName in Directory.EnumerateDirectories("controllerskins"))
                {
                    skinComboBox.Items.Add(skinName.Replace("controllerskins\\", ""));
                }
            }
            // controllerSkin = ControllerSkin.Load(Directory.EnumerateDirectories("controllerskins").First());
            try
            {
                skinComboBox.SelectedIndex = Convert.ToInt32(func.GetConfigData("config.txt", "InputDisplaySkin"));
            }
            catch
            {
                skinComboBox.SelectedIndex = 0;
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

        private GraphicsUnit units = GraphicsUnit.Pixel;
        private void InputDisplay_Paint(object sender, PaintEventArgs e)
        {
            Image sprite = controllerSkin.image;

            InputPlot basePlot = controllerSkin.buttons["base"];
            InputPlot r3 = controllerSkin.buttons["r3"];
            InputPlot r3Press = controllerSkin.buttons["r3Press"];
            InputPlot l3 = controllerSkin.buttons["l3"];
            InputPlot l3Press = controllerSkin.buttons["l3Press"];

            InputPlot dpadLeft = controllerSkin.buttons["dpadLeft"];
            InputPlot dpadRight = controllerSkin.buttons["dpadRight"];
            InputPlot dpadDown = controllerSkin.buttons["dpadDown"];
            InputPlot dpadUp = controllerSkin.buttons["dpadUp"];

            InputPlot cross = controllerSkin.buttons["cross"];
            InputPlot circle = controllerSkin.buttons["circle"];
            InputPlot triangle = controllerSkin.buttons["triangle"];
            InputPlot square = controllerSkin.buttons["square"];

            InputPlot select = controllerSkin.buttons["select"];
            InputPlot start = controllerSkin.buttons["start"];

            InputPlot r1 = controllerSkin.buttons["r1"];
            InputPlot l1 = controllerSkin.buttons["l1"];
            InputPlot l2 = controllerSkin.buttons["l2"];
            InputPlot r2 = controllerSkin.buttons["r2"];

            e.Graphics.DrawImage(sprite, basePlot.drawX, basePlot.drawY, new Rectangle(basePlot.spriteX, basePlot.spriteY, basePlot.spriteWidth, basePlot.spriteHeight), units);

            if (Inputs.Mask.Contains(Inputs.Buttons.r3)) e.Graphics.DrawImage(sprite, r3.drawX + (Inputs.rx * controllerSkin.analogPitch), r3.drawY + (Inputs.ry * controllerSkin.analogPitch), new Rectangle(r3.spriteX, r3.spriteY, r3.spriteWidth, r3.spriteHeight), units); 
            else e.Graphics.DrawImage(sprite, r3Press.drawX + (Inputs.rx * controllerSkin.analogPitch), r3Press.drawY + (Inputs.ry * controllerSkin.analogPitch), new Rectangle(r3Press.spriteX, r3Press.spriteY, r3Press.spriteWidth, r3Press.spriteHeight), units); 
            if (Inputs.Mask.Contains(Inputs.Buttons.l3)) e.Graphics.DrawImage(sprite, l3.drawX + (Inputs.lx * controllerSkin.analogPitch), l3.drawY + (Inputs.ly * controllerSkin.analogPitch), new Rectangle(l3.spriteX, l3.spriteY, l3.spriteWidth, l3.spriteHeight), units); 
            else e.Graphics.DrawImage(sprite, l3Press.drawX + (Inputs.lx * controllerSkin.analogPitch), l3Press.drawY + (Inputs.ly * controllerSkin.analogPitch), new Rectangle(l3Press.spriteX, l3Press.spriteY, l3Press.spriteWidth, l3Press.spriteHeight), units); 

            if (Inputs.Mask.Contains(Inputs.Buttons.left)) e.Graphics.DrawImage(sprite, dpadLeft.drawX, dpadLeft.drawY, new Rectangle(dpadLeft.spriteX, dpadLeft.spriteY, dpadLeft.spriteWidth, dpadLeft.spriteHeight), units);
            if (Inputs.Mask.Contains(Inputs.Buttons.right)) e.Graphics.DrawImage(sprite, dpadRight.drawX, dpadRight.drawY, new Rectangle(dpadRight.spriteX, dpadRight.spriteY, dpadRight.spriteWidth, dpadRight.spriteHeight), units);
            if (Inputs.Mask.Contains(Inputs.Buttons.down)) e.Graphics.DrawImage(sprite, dpadDown.drawX, dpadDown.drawY, new Rectangle(dpadDown.spriteX, dpadDown.spriteY, dpadDown.spriteWidth, dpadDown.spriteHeight), units);
            if (Inputs.Mask.Contains(Inputs.Buttons.up)) e.Graphics.DrawImage(sprite, dpadUp.drawX, dpadUp.drawY, new Rectangle(dpadUp.spriteX, dpadUp.spriteY, dpadUp.spriteWidth, dpadUp.spriteHeight), units);

            if (Inputs.Mask.Contains(Inputs.Buttons.cross)) e.Graphics.DrawImage(sprite, cross.drawX, cross.drawY, new Rectangle(cross.spriteX, cross.spriteY, cross.spriteWidth, cross.spriteHeight), units);
            if (Inputs.Mask.Contains(Inputs.Buttons.circle)) e.Graphics.DrawImage(sprite, circle.drawX, circle.drawY, new Rectangle(circle.spriteX, circle.spriteY, circle.spriteWidth, circle.spriteHeight), units);
            if (Inputs.Mask.Contains(Inputs.Buttons.triangle)) e.Graphics.DrawImage(sprite, triangle.drawX, triangle.drawY, new Rectangle(triangle.spriteX, triangle.spriteY, triangle.spriteWidth, triangle.spriteHeight), units);
            if (Inputs.Mask.Contains(Inputs.Buttons.square)) e.Graphics.DrawImage(sprite, square.drawX, square.drawY, new Rectangle(square.spriteX, square.spriteY, triangle.spriteWidth, triangle.spriteHeight), units);

            if (Inputs.Mask.Contains(Inputs.Buttons.select)) e.Graphics.DrawImage(sprite, select.drawX, select.drawY, new Rectangle(select.spriteX, select.spriteY, select.spriteWidth, select.spriteHeight), units);
            if (Inputs.Mask.Contains(Inputs.Buttons.start)) e.Graphics.DrawImage(sprite, start.drawX, start.drawY, new Rectangle(start.spriteX, start.spriteY, start.spriteWidth, start.spriteHeight), units);

            if (Inputs.Mask.Contains(Inputs.Buttons.r1)) e.Graphics.DrawImage(sprite, r1.drawX, r1.drawY, new Rectangle(r1.spriteX, r1.spriteY, r1.spriteWidth, r1.spriteHeight), units);
            if (Inputs.Mask.Contains(Inputs.Buttons.l1)) e.Graphics.DrawImage(sprite, l1.drawX, l1.drawY, new Rectangle(l1.spriteX, l1.spriteY, l1.spriteWidth, l1.spriteHeight), units);
            if (Inputs.Mask.Contains(Inputs.Buttons.l2)) e.Graphics.DrawImage(sprite, l2.drawX, l2.drawY, new Rectangle(l2.spriteX, l2.spriteY, l2.spriteWidth, l2.spriteHeight), units);
            if (Inputs.Mask.Contains(Inputs.Buttons.r2)) e.Graphics.DrawImage(sprite, r2.drawX, r2.drawY, new Rectangle(r2.spriteX, r2.spriteY, r2.spriteWidth, r2.spriteHeight), units);
        }

        private void skinComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var skinName = skinComboBox.Items[skinComboBox.SelectedIndex].ToString();

            controllerSkin = ControllerSkin.Load($"controllerskins\\{skinName}");

            func.ChangeFileLines("config.txt", skinComboBox.SelectedIndex.ToString(), "InputDisplaySkin");

            this.Width = Math.Max(controllerSkin.buttons["base"].spriteWidth + 50, this.Width);
            this.Height = Math.Max(controllerSkin.buttons["base"].spriteHeight + 50, this.Height);
        }

        private void InputDisplay_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer.Enabled = false;
        }
    }
}
