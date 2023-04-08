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
        }

        public int GetSelectedVersion()
        {
            return this.comboBox1.SelectedIndex;
        }
    }
}