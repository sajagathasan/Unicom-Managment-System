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
    public partial class Dashboadteacher : Form
    {
        public Dashboadteacher()
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
            SubjectForm subjectForm = new SubjectForm();
            subjectForm.FormClosed += (s, args) => this.Show();
            subjectForm.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            ExamForm examForm = new ExamForm(this); // pass current form
            examForm.Show();
            this.Hide();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            TimetableForm timetableForm = new TimetableForm();
            timetableForm.Show();
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
