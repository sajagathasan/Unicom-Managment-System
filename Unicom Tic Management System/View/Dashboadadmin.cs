using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unicom_Tic_Management_System.View;

namespace Unicom_Tic_Management_System
{
    public partial class Dashboadadmin : Form
    {
        public Dashboadadmin()
        {
            InitializeComponent();
        }

        private void Dashboadadmin_Load(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            StudentForm StudentForm = new StudentForm(); 
            StudentForm.Show();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           // dataGridView2.Columns.Clear();

            //dataGridView2.Columns.Add("Announcement", "Announcement");

        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            TeacherForm teacherForm = new TeacherForm(this);
            this.Hide();
            teacherForm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            StaffForm staffForm = new StaffForm();
            staffForm.Show();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            this.Hide();
            SubjectForm subjectForm = new SubjectForm();
            subjectForm.FormClosed += (s, args) => this.Show();
            subjectForm.Show();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            TimetableForm timetableForm = new TimetableForm();  
            timetableForm.Show();
            this.Hide();

            TimetablestudentForm timetablestudentForm = new TimetablestudentForm();
            timetablestudentForm.Show();
            this.Hide();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            MarksteacherForm marksForm = new MarksteacherForm();
            marksForm.Show();
            this.Hide();
        }
    }
}
