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
    public partial class DiagnosticsForm : Form
    {
        IGame game;
        Timer timer;

        interface IDiagnosticTest
        {
            void Start(IGame game);
            void Tick();
        }

        class NetworkDiagnosticTest : IDiagnosticTest
        {
            IGame game;
            int ticks = 0;

            public void Start(IGame game)
            {
                this.game = game;
            }

            public void Tick()
            {
                ticks++;

                throw new NotImplementedException();
            }
        }

        public DiagnosticsForm(IGame game)
        {
            InitializeComponent();

            this.game = game;

            gameCheckBox.Checked = true;
            networkCheckBox.Checked = true;
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            timer = new Timer();
            timer.Interval = 16;
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
        }

        public void timer_Tick(object sender, EventArgs e)
        {
            
        }
    }
}
