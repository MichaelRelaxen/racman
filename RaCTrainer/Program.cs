using System;
using System.Windows.Forms;
using System.Reflection;

using AutoUpdaterDotNET;

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
#if !DEBUG
            AutoUpdater.Start("https://MichaelRelaxen.github.io/racman/update.xml");
#endif

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
