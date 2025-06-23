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
    public partial class Dashboadstudent : Form
    {
        public Dashboadstudent()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            this.Hide();
            StudentstudentForm StudentstudentForm = new StudentstudentForm();
            StudentstudentForm.FormClosed += (s, args) => this.Show();
            StudentstudentForm.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ExamstudentForm examForm = new ExamstudentForm(this); // pass current form
            examForm.Show();
            this.Hide();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            TimetablestudentForm timetablestudentForm = new TimetablestudentForm();
            timetablestudentForm.Show();   
            this.Hide();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            MarksstudentForm marksForm = new MarksstudentForm();
            marksForm.Show();
            this.Hide();
        }
    }
}
