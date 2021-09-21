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


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Start();

        }

        public static Form AttachPS3Form;
        public static void Start()
        {
            AttachPS3Form = new AttachPS3Form();
            Application.Run(AttachPS3Form);
        }
    }
}
