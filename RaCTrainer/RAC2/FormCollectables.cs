using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace racman.RAC2
{
    public partial class FormCollectables : Form
    {
        private rac2 game;
        public FormCollectables(rac2 game)
        {
            this.game = game;
            InitializeComponent();
        }



        private void buttonResetPlatBolts_Click(object sender, EventArgs e)
        {
            game.resetPlatBolts();
        }

        private void buttonUnlockAllPlat_Click(object sender, EventArgs e)
        {
            var api = game.api;
            var pid = api.getCurrentPID();
            byte[] unlocked = Enumerable.Repeat((byte)0xFF, 0x70).ToArray();
            api.WriteMemory(pid, rac2.addr.platinumBoltArray, unlocked);
        }

        private void ResetNANObutton_Click(object sender, EventArgs e)
        {
            var api = game.api;
            var pid = api.getCurrentPID();
            byte[] locked = Enumerable.Repeat((byte)0x00, 10).ToArray();
            api.WriteMemory(pid, rac2.addr.nanotechBoostArray, locked);
        }

        private void UnlockNANOButton_Click(object sender, EventArgs e)
        {
            var api = game.api;
            var pid = api.getCurrentPID();
            byte[] locked = Enumerable.Repeat((byte)0x01, 10).ToArray();
            api.WriteMemory(pid, rac2.addr.nanotechBoostArray, locked);
        }

        private void ResetSPButton_Click(object sender, EventArgs e)
        {
            var api = game.api;
            var pid = api.getCurrentPID();
            byte[] locked = Enumerable.Repeat((byte)0x00, 30).ToArray();
            api.WriteMemory(pid, rac2.addr.skillPointArray, locked);
        }

        private void UnlockSPButton_Click(object sender, EventArgs e)
        {
            var api = game.api;
            var pid = api.getCurrentPID();
            byte[] locked = Enumerable.Repeat((byte)0x01, 30).ToArray();
            api.WriteMemory(pid, rac2.addr.skillPointArray, locked);
        }
    }
}
