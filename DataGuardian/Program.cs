using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataGuardian.Plugins.Core;
using DataGuardian.Windows;

namespace DataGuardian
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            _ = new Core();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (true)
                Application.Run(new WndMain());
            else
                Application.Run(new MyApplicationContext());
        }
    }
}