using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace racman
{
    static class Program
    {
        public static string currentVersion = "a.0.3"; // change this for each release
        public static string latestVersion;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            string checkUpdate = func.get_data("https://github.com/MichaelRelaxen/racman/releases");
            latestVersion = checkUpdate.Substring(checkUpdate.IndexOf("<a href=\"/MichaelRelaxen/racman/releases/tag/")).Split('>')[1].Split('<')[0];

            if (currentVersion != latestVersion)
            {
                DialogResult dialogResult = MessageBox.Show($"New version {latestVersion} is available (current ver: {currentVersion})\nDo you want to update?", "New update available.", MessageBoxButtons.YesNo);

                if (dialogResult == DialogResult.Yes)
                {
                    System.Diagnostics.Process.Start($"https://github.com/MichaelRelaxen/racman/releases/download/{latestVersion}/racman.exe");
                    Application.Exit();
                }
                else
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new AttachPS3Form());
                }
            }
            else
            {
                //lmfao im sure theres a better way to do this
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new AttachPS3Form());
            }


        }
    }
}
