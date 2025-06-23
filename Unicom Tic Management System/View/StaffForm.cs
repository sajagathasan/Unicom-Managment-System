using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Unicom_Tic_Management_System.View
{
    public partial class StaffForm : Form
    {
        public StaffForm()
        {
            InitializeComponent();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
            StaffForm StaffForm = new StaffForm();
            StaffForm.Show();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string firstName = textBox1.Text;
            string lastName = textBox2.Text;
            string address = textBox3.Text;
            string gender = comboBox1.Text;
            string email = textBox4.Text;
            string mobileNumber = textBox5.Text;
            string nic = textBox6.Text;
            string dob = dateTimePicker1.Value.ToString("yyyy-MM-dd"); // Format must match DB
            string qualification = textBox8.Text;
            string position = textBox7.Text;
            string userName = textBox9.Text;
            string password = textBox10.Text;

            var db = new Unicom_Tic_Management_System.Repositories.DatabaseManager();

            db.AddStaff(firstName, lastName, email, address, mobileNumber, gender, nic, dob, qualification, position, userName, password);

            MessageBox.Show("Create successfully!");
            ClearFields();
        }

        private void ClearFields()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox6.Clear();
            textBox7.Clear();
            textBox8.Clear();
            textBox9.Clear();
            textBox10.Clear();
            comboBox1.SelectedIndex = -1;
            dateTimePicker1.Value = DateTime.Today.AddYears(-18);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Enabled = true;
            textBox2.Enabled = true;
            textBox3.Enabled = true;
            comboBox1.Enabled = true;
            textBox4.Enabled = true;
            textBox9.Enabled = true;
            textBox6.Enabled = true;
            dateTimePicker1.Enabled = true;
            textBox7.Enabled = true;
            textBox10.Enabled = true;
            textBox8.Enabled = true;
            textBox9.Enabled = true;
        }
    }
}
