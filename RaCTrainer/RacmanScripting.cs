using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace racman
{
    public partial class RacmanScripting : Form
    {
        public RacmanScripting()
        {
            InitializeComponent();
        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = $"{Directory.GetCurrentDirectory()}\\mods";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    var lines = File.ReadAllLines(openFileDialog.FileName);
                    codeBox.Lines = lines;
                }
            }
        }

        public bool RunCurrentCode()
        {
            if (codeBox.Text == null || codeBox.Text == "") return true;
            var res = new LuaAutomation(codeBox.Text);
            return !res.failed;
        }

        private void buttonRun_Click(object sender, EventArgs e)
        {
            RunCurrentCode();
        }
    }
}