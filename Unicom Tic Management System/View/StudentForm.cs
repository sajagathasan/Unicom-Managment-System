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
    public partial class StudentForm : Form
    {
        private void StudentForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.OpenForms["DashboardForm"].Show();
        }
        public StudentForm()
        {
            InitializeComponent();
            dateTimePicker1.Enabled = true;
            this.FormClosed += StudentForm_FormClosed;
            //this.Load += StudentForm_Load;
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.Controls.Add(this.dateTimePicker1);
            dateTimePicker1.Value = DateTime.Today.AddYears(-18);
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string firstName = textBox1.Text;
            string lastName = textBox2.Text;
            string address = textBox3.Text;
            string gender = comboBox1.Text;
            string email = textBox5.Text;
            string mobileNumber = textBox9.Text;
            string nic = textBox4.Text;
            string dob = dateTimePicker1.Value.ToString("yyyy-MM-dd"); // Format must match DB
            int courseId = Convert.ToInt32(comboBox2.SelectedValue);
            string courseName = comboBox3.Text;
            string userName = textBox10.Text;
            string password = textBox6.Text;

            var db = new Unicom_Tic_Management_System.Repositories.DatabaseManager();

            db.AddStudent(firstName, lastName, email, address, mobileNumber, gender, nic, dob, courseId, courseName, userName, password);

            MessageBox.Show("Create successfully!");
            ClearFields();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void ClearFields()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            comboBox1.SelectedIndex = -1;
            textBox5.Text = "";
            textBox9.Text = "";
            textBox4.Text = "";
            dateTimePicker1.Value = DateTime.Now;
            textBox6.Text = "";
            comboBox2.SelectedIndex = -1;
            comboBox3.Text = "";
            textBox10.Text = "";
            textBox6.Text = "";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Enabled = true;
            textBox2.Enabled = true;
            textBox3.Enabled = true;
            comboBox1.Enabled = true;
            textBox4.Enabled = true;
            textBox9.Enabled = true;
            textBox5.Enabled = true;
            textBox6.Enabled = true;
            dateTimePicker1.Enabled = true;
            comboBox2.Enabled = true;
            comboBox3.Enabled = true;
            textBox10.Enabled = true;
            textBox9.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            StudentForm StudentForm = new StudentForm();
            StudentForm.Show();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
