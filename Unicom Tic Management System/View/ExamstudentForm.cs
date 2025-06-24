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
    public partial class ExamstudentForm : Form
    {
        private Form previousForm;
        public ExamstudentForm()
        {
            InitializeComponent();
            this.Load += ExamstudentForm_Load;
        }

        public ExamstudentForm(Form callingForm)
        {
            InitializeComponent();
            previousForm = callingForm;
            this.Load += ExamstudentForm_Load;
        }

        private void ExamstudentForm_Load(object sender, EventArgs e)
        {
            LoadRoomstudentData();
        }


        private void LoadRoomstudentData()
        {
            var db = new Unicom_Tic_Management_System.Repositories.DatabaseManager();
            //dataGridView1.DataSource = db.GetAllRoomstudent();
        }
              
        private void button4_Click(object sender, EventArgs e)
        {
            previousForm.Show();  
            this.Close();
        }


    }
}
