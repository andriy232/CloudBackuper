using System;
using System.Windows.Forms;
using DataGuardian.Impl;
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

            if (false)
                Application.Run(new WndMain());
            else
                Application.Run(new MyApplicationContext());
        }
    }
}