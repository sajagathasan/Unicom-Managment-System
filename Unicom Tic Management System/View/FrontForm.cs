using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UnicomTICManagementSystem;
using Unicom_Tic_Management_System.Repositories;

namespace Unicom_Tic_Management_System.View
{
    public partial class FrontForm : Form
    {
       // private List<User> users;

      
        public FrontForm()
        {
            InitializeComponent();
/*
            users = new List<User>
            {
                new User { Username = "admin@", Password = "admin123", Role = "Admin" },
                new User { Username = "teacher", Password = "teach123", Role = "Teacher" },
                new User { Username = "student", Password = "stud123", Role = "Student" }
            };*/
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text.Trim();
            string password = textBox2.Text.Trim();

            Unicom_Tic_Management_System.Repositories.DatabaseManager db = new Unicom_Tic_Management_System.Repositories.DatabaseManager();

            var result = db.GetUserRole(username, password);


            // var loggrdInUser = users.FirstOrDefault(u => u.Username == username && u.Password == password);

            /* DatabaseManager db = new DatabaseManager();
             var result = db.GetUserRole(username, password);*/

            if (result.Role != null)
            {
                MessageBox.Show($"Login successful as {result.Role}");

                this.Hide();
                Form nextForm = null;

                if (result.Role == "Admin")
                    nextForm = new Dashboadadmin();
                else if (result.Role == "Teacher")
                    nextForm = new Dashboadteacher();
                else if (result.Role == "Student")
                    nextForm = new Dashboadstudent();

                if (nextForm != null)
                    nextForm.Show();
            }
            else
            {
                MessageBox.Show("Invalid credentials.");
            }
        

    }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ForgotPasswordForm forgotForm = new ForgotPasswordForm();
            forgotForm.ShowDialog();
        }
    }
}
