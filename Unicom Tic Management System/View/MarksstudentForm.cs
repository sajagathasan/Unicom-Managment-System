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
    public partial class MarksstudentForm : Form
    {
        public MarksstudentForm()
        {
            InitializeComponent();
            LoadSubjects();
            LoadMarksData();
        }

        private void LoadSubjects()
        {
            comboBox1.Items.Clear(); 
            comboBox1.Items.Add("Python");
            comboBox1.Items.Add("Csharp");
            comboBox1.Items.Add("HTML, CSS, JS");
        }

        private void LoadMarksData()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Student ID");
            dt.Columns.Add("Subject");
            dt.Columns.Add("Marks");

            dataGridView1.DataSource = dt;
        }

        private void MarksstudentForm_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Dashboadstudent dashboard = new Dashboadstudent();
            dashboard.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }
    }
}
