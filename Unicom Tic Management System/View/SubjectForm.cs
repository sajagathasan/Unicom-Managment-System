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
    public partial class SubjectForm : Form
    {
        private Unicom_Tic_Management_System.Repositories.DatabaseManager db = new Unicom_Tic_Management_System.Repositories.DatabaseManager();

        public SubjectForm()
        {
            InitializeComponent();
        }

        private void SubjectForm_Load(object sender, EventArgs e)
        {
            LoadCourses();
            LoadSubjects();
        }

        private void LoadCourses()
        {
            comboBox1.DisplayMember = "CourseName";
            comboBox1.ValueMember = "CourseID";
            comboBox1.DataSource = db.GetCourses();
        }

        private void LoadSubjects()
        {
            dataGridView1.DataSource = db.GetSubjects();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string subjectName = textBox1.Text;
            string courseName = comboBox1.Text;

            if (string.IsNullOrEmpty(subjectName))
            {
                MessageBox.Show("Subject name cannot be empty.");
                return;
            }

            if (comboBox1.SelectedValue == null)
            {
                MessageBox.Show("Please select a course.");
                return;
            }
            int courseId = Convert.ToInt32(comboBox1.SelectedValue);
            int subjectId = int.TryParse(textBox2.Text, out int id) ? id : 0;

            db.AddSubject(subjectName, courseName, courseId, subjectId);
            LoadSubjects();
            ClearInputs();
            MessageBox.Show("Subject added successfully!");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                int subjectId = Convert.ToInt32(textBox2.Text);
                string subjectName = textBox1.Text;
                string courseName = comboBox1.Text;
                int courseId = Convert.ToInt32(comboBox1.SelectedValue);

                db.UpdateSubject(subjectId, subjectName, courseName, courseId);
                LoadSubjects();
                MessageBox.Show("Subject updated successfully!");
                ClearInputs();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                int subjectId = Convert.ToInt32(textBox2.Text);
                string subjectName = textBox1.Text;
                string courseName = comboBox1.Text;
                int courseId = Convert.ToInt32(comboBox1.SelectedValue);

                db.DeleteSubject(courseId, subjectId, subjectName, courseName);
                LoadSubjects();
                MessageBox.Show("Subject deleted successfully!");
                ClearInputs();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                textBox2.Text = row.Cells["SubjectID"].Value.ToString();
                textBox1.Text = row.Cells["SubjectName"].Value.ToString();
                comboBox1.Text = row.Cells["CourseName"].Value.ToString();
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ClearInputs()
        {
            textBox1.Clear();
            textBox2.Clear();
            comboBox1.SelectedIndex = -1;
        }

        private void SubjectForm_Load_1(object sender, EventArgs e)
        {

        }
    }
}
