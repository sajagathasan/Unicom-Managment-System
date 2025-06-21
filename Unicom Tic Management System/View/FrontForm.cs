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

namespace Unicom_Tic_Management_System.View
{
    public partial class FrontForm : Form
    {
        private List<User> users;

      
        public FrontForm()
        {
            InitializeComponent();

            users = new List<User>
            {
                new User { Username = "admin@", Password = "admin123", Role = "Admin" },
                new User { Username = "teacher", Password = "teach123", Role = "Teacher" },
                new User { Username = "student", Password = "stud123", Role = "Student" }
            };
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text.Trim();
            string password = textBox2.Text.Trim();
           
            var loggrdInUser = users.FirstOrDefault(u => u.Username == username && u.Password == password);

            if (loggrdInUser != null)
            {
                MessageBox.Show($"Login successful as {loggrdInUser.Role}");

                this.Hide();

                Form nextForm = null;
                if(loggrdInUser.Role == "Admin")
                {
                    nextForm = new Dashboadadmin();
                }

                else if (loggrdInUser.Role == "Teacher")
                {
                    nextForm= new Dashboadteacher();
                }

                else if(loggrdInUser.Role == "Stduent")
                {
                    nextForm=(new Dashboadstudent());
                }
                
                if (nextForm != null)
                {
                    nextForm.Show(); 
                }
            }

            else
            {
                MessageBox.Show("Invalid credentials. Try again.");
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
    }
}
