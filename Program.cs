using Marzersoft;
using Marzersoft.Themes;
using System;

namespace RSTK
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            App.MainFormType = typeof(RSTKForm);
            App.Name = "RSTK";
            App.Description = "Config editor and launcher for Rocksmith and Rocksmith 2014";
            App.Mutex = true;
            App.AutoCheckForUpdates = false;
            App.TrayIcon = true;
            App.SplashForm = false;
            //App.Theme = App.Themes["dark"];
            App.Run(args);
        }
    }
}
