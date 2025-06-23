using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Unicom_Tic_Management_System.View
{
    public partial class ExamForm : Form
    {
        private string userRole;

        public ExamForm()
        {
            InitializeComponent();
        }

        private Form previousForm;

        public ExamForm(Form callingForm)
        {
            InitializeComponent();
            previousForm = callingForm;
        }

        private void ExamForm_Load(object sender, EventArgs e)
        {
            LoadExamData();

            if (userRole == "Student")
            {
                // Disable buttons or hide editing features
                button1.Visible = false;
                button2.Visible = false;
                button3.Visible = false;
            }
        }

        private void LoadExamData()
        {
            using (var conn = new SQLiteConnection("Data Source=unicomtic.db;Version=3;"))
            {
                conn.Open();
                var adapter = new SQLiteDataAdapter("SELECT * FROM Exams", conn);
                var dt = new DataTable();
                adapter.Fill(dt);
                dataGridView1.DataSource = dt;
                conn.Close();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {


        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            previousForm.Show(); 
            this.Close();
        }
    }
}
