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
    public partial class RoomstudentForm : Form
    {
        private Form previousForm;

        public RoomstudentForm()
        {
            InitializeComponent();
            this.Load += RoomstudentForm_Load;
        }

        public RoomstudentForm(Form callingForm)
        {
            InitializeComponent();
            previousForm = callingForm;
            this.Load += RoomstudentForm_Load;
        }

        private void LoadRoomstudentData()
        {
            var db = new Unicom_Tic_Management_System.Repositories.DatabaseManager();
           // dataGridView1.DataSource = db.GetAllRoomstudent();
        }

        private void RoomstudentForm_Load(object sender, EventArgs e)
        {
            LoadRoomstudentData();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Dashboadstudent dashboard = new Dashboadstudent();
            dashboard.Show();
        }
    }
}
