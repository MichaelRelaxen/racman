using System;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace racman
{
    public partial class DiskGameSelector : Form
    {
        public DiskGameSelector()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        public int GetSelectedVersion()
        {
            return this.comboBox1.SelectedIndex;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Select.Enabled = true;
        }
    }
}