using System;
using System.Windows.Forms;
using Unicom_Tic_Management_System.Repositories;
using Unicom_Tic_Management_System.View;

namespace Unicom_Tic_Management_System
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            DatabaseManager db = new DatabaseManager();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Start the correct form here
            Application.Run(new FrontForm()); // Or MainForm, LoginForm, etc.
           // Application.Run(new Dashboadadmin());
        }
    }
}

