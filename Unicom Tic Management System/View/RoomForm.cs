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
    public partial class RoomForm : Form
    {
        public RoomForm()
        {
            InitializeComponent();
            InitializeGrid();
            this.Load += RoomForm_Load;
        }

        private void LoadRoomData()
        {
            var db = new Unicom_Tic_Management_System.Repositories.DatabaseManager();
            //dataGridView1.DataSource = db.GetAllRoom();
        }

        private void InitializeGrid()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Room");
            dt.Columns.Add("CourseName");
            dataGridView1.DataSource = dt;
        }


        private void RoomForm_Load(object sender, EventArgs e)
        {
            LoadRoomData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string CourseName = comboBox1.Text.Trim();
            string Room = comboBox2.Text.Trim();

            if (string.IsNullOrEmpty(Room) || string.IsNullOrEmpty(CourseName))
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }

            DataTable dt = (DataTable)dataGridView1.DataSource;
            dt.Rows.Add(Room, CourseName);

            MessageBox.Show("Classroom added successfully.");
            ClearForm();
        }

        private void ClearForm()
        {
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Dashboadteacher dashboard = new Dashboadteacher();
            dashboard.Show();
        }
    }
}
