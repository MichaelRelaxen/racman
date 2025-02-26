﻿using System;
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
    public partial class ChargebootColorPicker : Form
    {
        IPS3API api;
        uint primaryFront;
        uint primaryBack;
        uint tintFront;
        uint tintBack;

        public ChargebootColorPicker(IPS3API ps3api, uint pf, uint pb, uint tf, uint tb)
        {
            api = ps3api;
            primaryFront = pf;
            primaryBack = pb;
            tintFront = tf;
            tintBack = tb;

            InitializeComponent();
        }

        private void RAC2Cosmetics_Load(object sender, EventArgs e)
        {
            DefaultColors();
            comboBoxSlots.SelectedIndex = 0;
        }

        private void DefaultColors()
        {
            colorDialog1.Color = Color.FromArgb(0xFF, 0x20, 0x80, 0xFF);
            colorDialog2.Color = Color.FromArgb(0xFF, 0x00, 0x80, 0xFF);
            colorDialog3.Color = Color.FromArgb(0xFF, 0x00, 0x60, 0xCF);
            pictureBox1.BackColor = colorDialog1.Color;
            pictureBox2.BackColor = colorDialog2.Color;
            pictureBox3.BackColor = colorDialog3.Color;
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            DefaultColors();

            var pid = api.getCurrentPID();
            api.WriteMemory(pid, primaryFront, 4, "40FF8020");
            api.WriteMemory(pid, primaryBack, 4, "00CF6000");
            api.WriteMemory(pid, tintFront, 4, "20FFFF00");
            api.WriteMemory(pid, tintBack, 4, "00008000");
        }

        private void buttonColor_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.BackColor = colorDialog1.Color;
            };
            ApplyChanges();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            buttonColor_Click(sender, e);
        }

        private void buttonColor2_Click(object sender, EventArgs e)
        {
            if (colorDialog2.ShowDialog() == DialogResult.OK)
            {
                pictureBox2.BackColor = colorDialog2.Color;
            }
            ApplyChanges();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            buttonColor2_Click(sender, e);
        }

        private void buttonColor3_Click(object sender, EventArgs e)
        {
            if (colorDialog3.ShowDialog() == DialogResult.OK)
            {
                pictureBox3.BackColor = colorDialog3.Color;
            }
            ApplyChanges();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            buttonColor3_Click(sender, e);
        }

        private void ApplyChanges()
        {
            var pid = api.getCurrentPID();

            // FRONT: primary front, tint front
            api.WriteMemory(pid, primaryFront, new byte[]
            {
                0x40, colorDialog1.Color.B, colorDialog1.Color.G, colorDialog1.Color.R
            });
            api.WriteMemory(pid, tintFront, new byte[]
            {
                0x40, colorDialog1.Color.B, colorDialog1.Color.G, colorDialog1.Color.R
            });

            // MID: tint back
            api.WriteMemory(pid, tintBack, new byte[]
            {
                0x40, colorDialog2.Color.B, colorDialog2.Color.G, colorDialog2.Color.R
            });

            // BACK: primary back
            api.WriteMemory(pid, primaryBack, new byte[]
            {
                0x25, colorDialog3.Color.B, colorDialog3.Color.G, colorDialog3.Color.R
            });
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            func.ChangeFileLines("config.txt", colorDialog1.Color.ToArgb().ToString(), "SavedColor1No" + comboBoxSlots.SelectedIndex);
            func.ChangeFileLines("config.txt", colorDialog2.Color.ToArgb().ToString(), "SavedColor2No" + comboBoxSlots.SelectedIndex);
            func.ChangeFileLines("config.txt", colorDialog3.Color.ToArgb().ToString(), "SavedColor3No" + comboBoxSlots.SelectedIndex);
        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            var c1 = func.GetConfigData("config.txt", "SavedColor1No" + comboBoxSlots.SelectedIndex);
            var c2 = func.GetConfigData("config.txt", "SavedColor2No" + comboBoxSlots.SelectedIndex);
            var c3 = func.GetConfigData("config.txt", "SavedColor3No" + comboBoxSlots.SelectedIndex);

            colorDialog1.Color = Color.FromArgb(Convert.ToInt32(c1));
            colorDialog2.Color = Color.FromArgb(Convert.ToInt32(c2));
            colorDialog3.Color = Color.FromArgb(Convert.ToInt32(c3));

            pictureBox1.BackColor = colorDialog1.Color;
            pictureBox2.BackColor = colorDialog2.Color;
            pictureBox3.BackColor = colorDialog3.Color;

            ApplyChanges();
        }
    }
}
