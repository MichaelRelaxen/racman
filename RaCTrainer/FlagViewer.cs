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
    public partial class FlagViewer : Form
    {
        private int NumRows;
        private uint BaseAddr;
        private IGame Game;
        private List<List<CheckBox>> CheckBoxes = new List<List<CheckBox>>();
        private List<int> subIds = new List<int>();

        public FlagViewer(IGame game, uint baseAddr, int Rows)
        {
            Game = game; 
            BaseAddr = baseAddr;
            NumRows = Rows;
            InitializeComponent();
        }

        private void FlagViewer_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < NumRows; i++)
            {
                var label = new Label();
                label.Text = "0x" + (BaseAddr + i).ToString("X");
                label.Top = 25 * (i + 1);
                label.Left = 15;
                label.Width = 60;
                this.Controls.Add(label);

                var boxes = new List<CheckBox>();
                for (int j = 0; j < 8; j++)
                {
                    // Ensure capture. :(
                    int i2 = i, j2 = j;

                    var box = new CheckBox();
                    // box.Text = j.ToString();
                    box.Width = 15;
                    box.Left = label.Left + label.Width + 15 * (j + 1);
                    box.Top = label.Top;
                    box.CheckStateChanged += (sn, ev) =>
                    {
                        var data = Game.api.ReadMemory(Game.pid, BaseAddr + (uint)i2, 1);
                        byte toData = data[0];
                        byte flag = (byte)(1 << j2);
                        if (box.Checked)
                            toData = (byte)(toData | flag);
                        else
                            toData = (byte)(toData & ~flag);

                        Game.api.WriteMemory(Game.pid, BaseAddr + (uint)i2, new byte[] { toData });
                    };

                    boxes.Add(box);

                    this.Controls.Add(box);
                }
                CheckBoxes.Add(boxes);
            }

            // Second loop to avoid timing issues
            for (int i = 0; i < NumRows; i++) {
                var boxes = CheckBoxes[i];

                var subId = Game.api.SubMemory(Game.pid, BaseAddr + (uint)i, 1, (data) =>
                {
                    byte mask = 1;
                    for (int j = 0; j < 8; j++)
                    {
                        var currBox = boxes[j];
                        currBox.Invoke(new Action(() => currBox.Checked = (data[0] & mask) != 0));
                        mask *= 2;
                    }
                });
                subIds.Add(subId);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Game.api.WriteMemory(Game.pid, BaseAddr, Enumerable.Repeat((byte)0xFF, NumRows).ToArray());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Game.api.WriteMemory(Game.pid, BaseAddr, Enumerable.Repeat((byte)0x00, NumRows).ToArray());
        }

        private void FlagViewer_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (var subId in subIds) Game.api.ReleaseSubID(subId);
        }
    }
}
