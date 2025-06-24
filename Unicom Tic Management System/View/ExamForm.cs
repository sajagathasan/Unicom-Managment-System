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
        private Form previousForm;

        public ExamForm()
        {
            InitializeComponent();
        }


        public ExamForm(Form callingForm)
        {
            InitializeComponent();
            previousForm = callingForm;
            this.Load += ExamForm_Load;
        }

        private void ExamForm_Load(object sender, EventArgs e)
        {
            LoadExamData();

            if (userRole == "Student")
            {
                button1.Visible = false;
                button2.Visible = false;
                button3.Visible = false;
            }
        }
        /*public void LoadExamData()
        {
            var db = new Unicom_Tic_Management_System.Repositories.DatabaseManager();
            dataGridView1.DataSource = db.GetAllExam();
        }*/


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

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            DateTime selectedDate = dateTimePicker1.Value;
            if (selectedDate > DateTime.Today)
            {
                MessageBox.Show("Date of birth cannot be in the future.");
                dateTimePicker1.Value = DateTime.Today.AddYears(-18);
            }
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
