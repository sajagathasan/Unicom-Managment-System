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
    public partial class MarksteacherForm : Form
    {
        public MarksteacherForm()
        {
            InitializeComponent();
            LoadSubjects();
            LoadMarksData();
        }

        private void LoadSubjects()
        {
            // You can fetch from DB; for now, hardcoded
            comboBox1.Items.Clear();
            comboBox1.Items.Add("Python");
            comboBox1.Items.Add("Csharp");
            comboBox1.Items.Add("HTML,CSS,JS");
        }

        private void LoadMarksData()
        {
            // For example purpose, create a dummy table
            DataTable dt = new DataTable();
            dt.Columns.Add("Student ID");
            dt.Columns.Add("Subject");
            dt.Columns.Add("Marks");

            dataGridView1.DataSource = dt;
        }

        private void MarksteacherForm_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string studentId = textBox1.Text;
            string subject = comboBox1.Text;
            string marks = textBox2.Text;

            if (string.IsNullOrEmpty(studentId) || string.IsNullOrEmpty(subject) || string.IsNullOrEmpty(marks))
            {
                MessageBox.Show("Please fill all fields.");
                return;
            }

            // Add to DataGridView (or save to database)
            DataTable dt = (DataTable)dataGridView1.DataSource;
            dt.Rows.Add(studentId, subject, marks);

            MessageBox.Show("Marks saved successfully!");
            ClearForm();
        }

        private void ClearForm()
        {
            textBox1.Text = "";
            comboBox1.SelectedIndex = -1;
            textBox2.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Dashboadadmin dashboard = new Dashboadadmin();
            dashboard.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}
