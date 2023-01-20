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
    public partial class Sly1Form : Form
    {
        public Sly1 game;
        public Vector3 savedPos;

        public Sly1Form(Sly1 game)
        {
            this.game = game;

            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.savedPos = this.game.GetPosition();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.game.SetPosition(this.savedPos);
        }
    }
}
