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
    public partial class TODForm : Form
    {
        AutosplitterHelper autosplitter;
        static ModLoaderForm modLoaderForm;
        public tod game;

        private int disconnectSubId = -1;
        private bool useAutosplitter = false;
        private bool hasSmuggled = false;
        private ASSRoute? autosplitterASSroute;
        private byte lastPlanet;
        private byte lastGoodPlanet;

        public TODForm(tod game)
        {
            this.game = game;
            InitializeComponent();

            if (game.api is Ratchetron r)
            {
                // r.setDisconnectCallback(() =>
                // {
                //     game.api.Disconnect();
                // });

                r.setReconnectCallback(() =>
                {
                    

                    if (useAutosplitter)
                    {
                        System.Threading.Thread.Sleep(8000);
                        autosplitter.Reconnect();
                        setupDisconnectSubs();
                        
                        game.api.Notify("Autosplitter reconnected!");
                    }
                });
            }
        }

        private bool hasEnteredAutoscroller(byte last, byte curr)
        {
            if (last == curr) return false;
            return curr == 4 || curr == 8 || curr == 13;
        }

        private bool hasLeftAutoscroller(byte last, byte curr)
        {
            if (last == curr) return false;

            var leftAutoscroller = false;
            leftAutoscroller |= last == 4 && curr != 5;
            leftAutoscroller |= last == 8 && curr != 9;
            leftAutoscroller |= last == 13 && curr != 14;
            return leftAutoscroller;
        }

        private void setupDisconnectSubs()
        {
            var api = game.api;
            var pid = api.getCurrentPID();

            disconnectSubId = api.SubMemory(pid, tod.addr.currentPlanet, 1, (val) =>
            {
                if (autosplitterASSroute == ASSRoute.None) return;

                var currPlanet = val[0];
                var currGoodPlanet = lastGoodPlanet;

                if (lastPlanet == 0 && currPlanet != 0)
                    currGoodPlanet = currPlanet;

                if (autosplitterASSroute == ASSRoute.ASS && hasEnteredAutoscroller(lastGoodPlanet, currGoodPlanet))
                {
                    handleDisconnect();
                }
                // Disconnect if we just left the autoscroller
                else if (hasLeftAutoscroller(lastGoodPlanet, currGoodPlanet))
                {
                    if (autosplitterASSroute == ASSRoute.SmugglingGASS && !hasSmuggled)
                        hasSmuggled = true;
                    else
                        handleDisconnect();
                }


                lastPlanet = currPlanet;
                lastGoodPlanet = currGoodPlanet;
            });
        }

        private void handleDisconnect()
        {
            // Void memory subs
            game.api.ReleaseSubID(disconnectSubId);

            // Reset planet state
            hasSmuggled = false;
            lastPlanet = 0;
            lastGoodPlanet = 0;

            // Disconnect autosplitter subs
            autosplitter?.Stop();
            game.api.Notify("Autosplitter disconnected");
        }

        private void patchLoaderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((Application.OpenForms["ModLoaderForm"] as ModLoaderForm) != null)
            {
                modLoaderForm.Activate();
            }
            else
            {
                modLoaderForm = new ModLoaderForm();
                modLoaderForm.Show();
            }
        }

        private void memoryUtilitiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MemoryForm memoryForm = new MemoryForm();
            memoryForm.Show();
        }

        private void planets_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var api = game.api;
            var pid = api.getCurrentPID();

            api.WriteMemory(pid, tod.addr.savePlanetId, new byte[] { (byte)planets_comboBox.SelectedIndex });
        }

        private void TODForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (autosplitter != null && autosplitter.IsRunning)
                autosplitter?.Stop();
            autosplitter = null;

            game.api.Disconnect();
            Application.Exit();
        }

        private void setAutosplitterLabel()
        {
            labelSplitterRoute.Visible = true;
            switch (autosplitterASSroute)
            {
                case ASSRoute.None:
                    labelSplitterRoute.Text = "No ASS/GASS";
                    break;
                case ASSRoute.ASS:
                    labelSplitterRoute.Text = "Old ASS";
                    break;
                case ASSRoute.GASS:
                    labelSplitterRoute.Text = "GASS";
                    break;
                case ASSRoute.SmugglingGASS:
                    labelSplitterRoute.Text = "GASS with smuggling";
                    break;
            }
        }

        private void buttonStartAutosplitter_Click(object sender, EventArgs e)
        {
            var choiceForm = new CategoryChoiceForm();
            choiceForm.ShowDialog();

            if (choiceForm.route is ASSRoute route)
            {
                useAutosplitter = true;
                autosplitterASSroute = route;
                setupDisconnectSubs();

                Console.WriteLine("Autosplitter starting!");
                autosplitter = new AutosplitterHelper();
                autosplitter.StartAutosplitterForGame(this.game);

                labelAutosplitterStatus.Text = "Autosplitter enabled!";
                labelAutosplitterStatus.ForeColor = Color.Green;

                setAutosplitterLabel();
                buttonStartAutosplitter.Text = "Configure Autosplitter";
                buttonStartAutosplitter.Click -= buttonStartAutosplitter_Click;
                buttonStartAutosplitter.Click += buttonReselectAutosplitter_Click;
            }
        }

        private void buttonReselectAutosplitter_Click(object sender, EventArgs e) 
        {
            var choiceForm = new CategoryChoiceForm();
            choiceForm.ShowDialog();

            if (choiceForm.route is ASSRoute route)
            {
                autosplitterASSroute = route;
                setAutosplitterLabel();
            }
        }

        private void DieButtonClick(object sender, EventArgs e)
        {
            game.DeathAbuse();
        }

        private void ChallegeModeButtonClick(object sender, EventArgs e)
        {
            game.SetChallengeMode();
        }

        private void ResetAllGoldBoltsClick(object sender, EventArgs e)
        {
            game.ResetAllGoldBolts();
        }

        private void GodRatchetClick(object sender, EventArgs e)
        {
            game.SetGodRatchet();
        }

        private void ResetAllRYNOPlans(object sender, EventArgs e)
        {
            game.ResetRYNOPlans();
        }

        private void PlayerValuesFormClick(object sender, EventArgs e)
        {
            PlayerValues form = new PlayerValues(game);
            form.Show();
        }

        private void ArmorSkinsFormClick(object sender, EventArgs e)
        {
            ArmorSkinsForm form = new ArmorSkinsForm();
            form.Show();
        }

        private void WeaponsFormClick(object sender, EventArgs e)
        {
            WeaponForm form = new WeaponForm(game); 
            form.Show();
        }

        private void GadgetsFormClick(object sender, EventArgs e)
        {
            GadgetForm form = new GadgetForm(game);
            form.Show();
        }

        private void ResetGroovitronStorageClick(object sender, EventArgs e)
        {
            game.ResetGoldenGrovitronStorage();
        }
    }
}
