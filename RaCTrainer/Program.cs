using System;
using System.Windows.Forms;
using AutoUpdate;

namespace racman
{
    static class Program
    {

        


        //public static string currentVersion = "a.0.3"; // change this for each release
        //public static string latestVersion;

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

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new AttachPS3Form());

        }
    }
}
