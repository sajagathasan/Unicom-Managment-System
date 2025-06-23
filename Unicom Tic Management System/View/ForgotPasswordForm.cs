using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Unicom_Tic_Management_System.View
{
    public partial class ForgotPasswordForm : Form
    {
        private Unicom_Tic_Management_System.Repositories.DatabaseManager db = new Unicom_Tic_Management_System.Repositories.DatabaseManager();

        public ForgotPasswordForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text;
            string newPassword = textBox2.Text;

            var db = new Unicom_Tic_Management_System.Repositories.DatabaseManager();  // your existing DB class

            if (db.UpdatePassword(username, newPassword))
            {
                MessageBox.Show("Password reset successfully!");
                this.Close();
            }
            else
            {
                MessageBox.Show("Username not found!");
            }
        }
    }
}
