using System;
using System.Linq;
using System.Windows.Forms;
namespace racman
{

    public partial class HotkeysMenuRac3 : Form
    {
        string LoadHotkey, SaveHotkey, Coord1Hotkey, Coord2Hotkey, Coord3Hotkey, DieHotkey;

        public HotkeysMenuRac3()
        {
            InitializeComponent();

            LoadHotkey = func.GetConfigData("config.exe", "LoadHotkeyRac3");
            SaveHotkey = func.GetConfigData("config.exe", "SaveHotkeyRac3");
            DieHotkey = func.GetConfigData("config.exe", "DieHotkeyRac3");
            Coord1Hotkey = func.GetConfigData("config.exe", "Coord1HotkeyRac3");
            Coord2Hotkey = func.GetConfigData("config.exe", "Coord2HotkeyRac3");
            Coord3Hotkey = func.GetConfigData("config.exe", "Coord3HotkeyRac3");


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

            coords3HotkeyTextBox.Text = Coord3Hotkey;
            coords3HotkeyTextBox.SelectionStart = 0;
            coords3HotkeyTextBox.SelectionLength = 0; //Otherwise it highlights it for some reason


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

        //
        //Coordinate 3 Hotkey
        //
        private void Coords3HotkeyTextBox_Click(object sender, EventArgs e)
        {
            coords3HotkeyTextBox.Text = "Enter Hotkey...";
        }

        private void Coords3HotkeyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            Coord3Hotkey = Convert.ToString(e.KeyCode);
            coords3HotkeyTextBox.Text = Coord3Hotkey;
            SendKeys.Send("{TAB}");
        }



        //Save Hotkeys
        private void SaveHotkeysButton_Click(object sender, EventArgs e)
        {

            func.ChangeFileLines("config.exe", LoadHotkey, "LoadHotkeyRac3");
            RAC3Form.LoadHotkey = (Keys)System.Enum.Parse(typeof(Keys), func.GetConfigData("config.exe", "LoadHotkeyRac3"));

            func.ChangeFileLines("config.exe", SaveHotkey, "SaveHotkeyRac3");
            RAC3Form.SaveHotkey = (Keys)System.Enum.Parse(typeof(Keys), func.GetConfigData("config.exe", "SaveHotkeyRac3"));

            func.ChangeFileLines("config.exe", DieHotkey, "DieHotkeyRac3");
            RAC3Form.DieHotkey = (Keys)System.Enum.Parse(typeof(Keys), func.GetConfigData("config.exe", "DieHotkeyRac3"));

            func.ChangeFileLines("config.exe", Coord1Hotkey, "Coord1HotkeyRac3");
            RAC3Form.Coord1Hotkey = (Keys)System.Enum.Parse(typeof(Keys), func.GetConfigData("config.exe", "Coord1HotkeyRac3"));

            func.ChangeFileLines("config.exe", Coord2Hotkey, "Coord2HotkeyRac3");
            RAC3Form.Coord2Hotkey = (Keys)System.Enum.Parse(typeof(Keys), func.GetConfigData("config.exe", "Coord2HotkeyRac3"));

            func.ChangeFileLines("config.exe", Coord3Hotkey, "Coord3HotkeyRac3");
            RAC3Form.Coord3Hotkey = (Keys)System.Enum.Parse(typeof(Keys), func.GetConfigData("config.exe", "Coord3HotkeyRac3"));


        }

    }
}