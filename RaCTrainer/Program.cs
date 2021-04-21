using System;
using System.Windows.Forms;
using AutoUpdate;
using System.Reflection;

namespace racman
{
    static class Program
    {


        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        
        [STAThread]
        static void Main(string[] args)
        {
            Updater.GitHubRepo = "/MichaelRelaxen/racman";
            if (Updater.AutoUpdate(args))
            {
                return;
            }


            if(func.GetConfigData("config.exe","FirstLoad") == "" && Convert.ToString(Assembly.GetEntryAssembly().GetName().Version) == "1.0.0.5")
            {
                MessageBox.Show("i hope u fuckin appreciate this sneep");
                func.ChangeFileLines("config.exe", "No", "FirstLoad");
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new AttachPS3Form());

        }
    }
}
