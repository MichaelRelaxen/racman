using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace racman.TOD
{
    public partial class CategoryChoiceForm : Form
    {
        public bool? usingBoltSmugging = null;

        public CategoryChoiceForm()
        {
            InitializeComponent();
        }

        private void buttonWithout_Click(object sender, EventArgs e)
        {
            usingBoltSmugging = false;
            this.Close();
        }

        private void buttonWith_Click(object sender, EventArgs e)
        {
            usingBoltSmugging = true;
            this.Close();
        }
    }
}
