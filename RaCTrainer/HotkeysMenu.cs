using System;
using System.Linq;
using System.Windows.Forms;
namespace racman
{

    public partial class HotkeysMenu : Form
    {
        string LoadHotkey, SaveHotkey, Coord1Hotkey, Coord2Hotkey, Coord3Hotkey, DieHotkey;

        public HotkeysMenu()
        {
            InitializeComponent();

            LoadHotkey = func.GetConfigData("config.exe", "LoadHotkey");
            SaveHotkey = func.GetConfigData("config.exe", "SaveHotkey");
            DieHotkey = func.GetConfigData("config.exe", "DieHotkey");
            Coord1Hotkey = func.GetConfigData("config.exe", "Coord1Hotkey");
            Coord2Hotkey = func.GetConfigData("config.exe", "Coord2Hotkey");
            Coord3Hotkey = func.GetConfigData("config.exe", "Coord3Hotkey");


            loadPositionHotkeyTextBox.Text = LoadHotkey;
            loadPositionHotkeyTextBox.SelectionStart = 0;
            loadPositionHotkeyTextBox.SelectionLength = 0; //Otherwise it highlights it for some reason

            savePositionHotkeyTextBox.Text = SaveHotkey;
            savePositionHotkeyTextBox.SelectionStart = 0;
            savePositionHotkeyTextBox.SelectionLength = 0; //Otherwise it highlights it for some reason

            killYourselfHotkeyTextBox.Text = DieHotkey;
            killYourselfHotkeyTextBox.SelectionStart = 0;
            killYourselfHotkeyTextBox.SelectionLength = 0; //Otherwise it highlights it for some reason

            coords1HotkeyTextBox.Text = Coord1Hotkey;
            coords1HotkeyTextBox.SelectionStart = 0;
            coords1HotkeyTextBox.SelectionLength = 0; //Otherwise it highlights it for some reason

            coords2HotkeyTextBox.Text = Coord2Hotkey;
            coords2HotkeyTextBox.SelectionStart = 0;
            coords2HotkeyTextBox.SelectionLength = 0; //Otherwise it highlights it for some reason

        }

        //
        //Save Position Hotkey
        //
        private void SavePositionHotkeyTextBox_Click(object sender, EventArgs e)
        {
            savePositionHotkeyTextBox.Text = "Enter Hotkey...";
        }

        private void SavePositionHotkeyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            SaveHotkey = Convert.ToString(e.KeyCode);
            savePositionHotkeyTextBox.Text = SaveHotkey;
            SendKeys.Send("{TAB}");

        }

        //
        //Load Position Hotkey
        //
        private void LoadPositionHotkeyTextBox_Click(object sender, EventArgs e)
        {
            loadPositionHotkeyTextBox.Text = "Enter Hotkey...";
        }

        private void LoadPositionHotkeyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            LoadHotkey = Convert.ToString(e.KeyCode);
            loadPositionHotkeyTextBox.Text = LoadHotkey;
            SendKeys.Send("{TAB}");
        }

        //
        //Kill Yourself Hotkey
        //
        private void KillYourselfHotkeyTextBox_Click(object sender, EventArgs e)
        {
            killYourselfHotkeyTextBox.Text = "Enter Hotkey...";
        }

        private void KillYourselfHotkeyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            DieHotkey = Convert.ToString(e.KeyCode);
            killYourselfHotkeyTextBox.Text = DieHotkey;
            SendKeys.Send("{TAB}");
        }

        //
        //Coordinate 1 Hotkey
        //
        private void Coords1HotkeyTextBox_Click(object sender, EventArgs e)
        {
            coords1HotkeyTextBox.Text = "Enter Hotkey...";

        }

        private void Coords1HotkeyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            Coord1Hotkey = Convert.ToString(e.KeyCode);
            coords1HotkeyTextBox.Text = Coord1Hotkey;
            SendKeys.Send("{TAB}");
        }

        private void HotkeysMenu_Load(object sender, EventArgs e)
        {

        }

        //
        //Coordinate 2 Hotkey
        //
        private void Coords2HotkeyTextBox_Click(object sender, EventArgs e)
        {
            coords2HotkeyTextBox.Text = "Enter Hotkey...";
        }

        private void Coords2HotkeyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            Coord2Hotkey = Convert.ToString(e.KeyCode);
            coords2HotkeyTextBox.Text = Coord2Hotkey;
            SendKeys.Send("{TAB}");
        }



        //Save Hotkeys
        private void SaveHotkeysButton_Click(object sender, EventArgs e)
        {

            func.ChangeFileLines("config.exe", LoadHotkey, "LoadHotkey");
            RAC1Form.LoadHotkey = (Keys)System.Enum.Parse(typeof(Keys), func.GetConfigData("config.exe", "LoadHotkey"));

            func.ChangeFileLines("config.exe", SaveHotkey, "SaveHotkey");
            RAC1Form.SaveHotkey = (Keys)System.Enum.Parse(typeof(Keys), func.GetConfigData("config.exe", "SaveHotkey"));

            func.ChangeFileLines("config.exe", DieHotkey, "DieHotkey");
            RAC1Form.DieHotkey = (Keys)System.Enum.Parse(typeof(Keys), func.GetConfigData("config.exe", "DieHotkey"));

            func.ChangeFileLines("config.exe", Coord1Hotkey, "Coord1Hotkey");
            RAC1Form.Coord1Hotkey = (Keys)System.Enum.Parse(typeof(Keys), func.GetConfigData("config.exe", "Coord1Hotkey"));

            func.ChangeFileLines("config.exe", Coord2Hotkey, "Coord2Hotkey");
            RAC1Form.Coord2Hotkey = (Keys)System.Enum.Parse(typeof(Keys), func.GetConfigData("config.exe", "Coord2Hotkey"));

            func.ChangeFileLines("config.exe", Coord3Hotkey, "Coord3Hotkey");
            RAC1Form.Coord3Hotkey = (Keys)System.Enum.Parse(typeof(Keys), func.GetConfigData("config.exe", "Coord3Hotkey"));


        }

    }
}