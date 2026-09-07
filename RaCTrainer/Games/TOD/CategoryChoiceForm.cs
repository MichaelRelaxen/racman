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
    public enum ASSRoute
    {
        None,
        ASS,
        GASS,
        SmugglingGASS
    }

    public partial class CategoryChoiceForm : Form
    {
        public ASSRoute? route = null;

        public CategoryChoiceForm()
        {
            InitializeComponent();
        }

        private void buttonNormal_Click(object sender, EventArgs e)
        {
            route = ASSRoute.GASS;
            this.Close();
        }

        private void buttonAGB_Click(object sender, EventArgs e)
        {
            route = ASSRoute.SmugglingGASS;
            this.Close();

        }

        private void buttonNone_Click(object sender, EventArgs e)
        {
            route = ASSRoute.None;
            this.Close();
        }

        private void buttonEvil_Click(object sender, EventArgs e)
        {
            route = ASSRoute.ASS;
            this.Close();
        }
    }
}
