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
    public partial class TeacherForm : Form
    {
        private void TeacherForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.OpenForms["DashboardForm"].Show();
        }
        public TeacherForm(Dashboadadmin dashboadadmin)
        {
            InitializeComponent();
            dateTimePicker1.Enabled = true;
            this.FormClosed += TeacherForm_FormClosed;
            //this.Load += StudentForm_Load;
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.Controls.Add(this.dateTimePicker1);
            dateTimePicker1.Value = DateTime.Today.AddYears(-18);
            this.Load += TeacherForm_Load;
        }

        private void LoadTeacherData()
        {
            var db = new Unicom_Tic_Management_System.Repositories.DatabaseManager();
            dataGridView1.DataSource = db.GetAllTeacher();
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string firstName = textBox1.Text;
            string lastName = textBox2.Text;
            string address = textBox3.Text;
            string gender = comboBox1.Text;
            string email = textBox4.Text;
            string mobileNumber = textBox9.Text;
            string nic = textBox6.Text;
            string dob = dateTimePicker1.Value.ToString("yyyy-MM-dd"); // Format must match DB
            int subjectId = Convert.ToInt32(comboBox2.SelectedValue);
            string subjectName = comboBox3.SelectedItem?.ToString();
            string qualification = textBox7.Text;
            string position = textBox10.Text;
            string userName = textBox8.Text;
            string password = textBox9.Text;

            var db = new Unicom_Tic_Management_System.Repositories.DatabaseManager();

            db.AddTeacher(firstName, lastName, email, address, mobileNumber, gender, nic, dob, subjectId, subjectName, qualification, position, userName, password);

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
            comboBox2.SelectedIndex = -1;
            comboBox3.SelectedIndex = -1;
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
            comboBox2.Enabled = true;
            comboBox3.Enabled = true;
            textBox7.Enabled = true;
            textBox10.Enabled = true;
            textBox8.Enabled = true;
            textBox9.Enabled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void TeacherForm_Load(object sender, EventArgs e)
        {
            LoadTeacherData();
        }

        private void dateTimePicker1_ValueChanged_1(object sender, EventArgs e)
        {
            DateTime selectedDate = dateTimePicker1.Value;
            if (selectedDate > DateTime.Today)
            {
                MessageBox.Show("Date of birth cannot be in the future.");
                dateTimePicker1.Value = DateTime.Today.AddYears(-18);
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
    
}
