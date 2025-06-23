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
    public partial class TimetableForm : Form
    {
        public TimetableForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var subject = comboBox1.SelectedItem as ComboBoxItem;
            var room = comboBox2.SelectedItem as ComboBoxItem;
            var timeSlot = textBox1.Text.Trim();

            if (subject == null || room == null || string.IsNullOrEmpty(timeSlot))
            {
                MessageBox.Show("Please fill all fields.");
                return;
            }

            using (var conn = new SQLiteConnection("Data Source=unicomtic.db;Version=3;"))
            {
                conn.Open();
                var cmd = new SQLiteCommand(@"INSERT INTO Timetables (SubjectID, TimeSlot, RoomID)
                                              VALUES (@sub, @time, @room)", conn);
                cmd.Parameters.AddWithValue("@sub", subject.Value);
                cmd.Parameters.AddWithValue("@time", timeSlot);
                cmd.Parameters.AddWithValue("@room", room.Value);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Timetable added!");
                LoadTimetables();
            }
        }

        private void TimetableForm_Load(object sender, EventArgs e)
        {
            LoadSubjects();
            LoadRooms();
            LoadTimetables();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                int timetableID = Convert.ToInt32(dataGridView1.CurrentRow.Cells["TimetableID"].Value);

                using (var conn = new SQLiteConnection("Data Source=unicomtic.db;Version=3;"))
                {
                    conn.Open();
                    var cmd = new SQLiteCommand("DELETE FROM Timetables WHERE TimetableID = @id", conn);
                    cmd.Parameters.AddWithValue("@id", timetableID);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Timetable deleted.");
                    LoadTimetables();
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void LoadSubjects()
        {
            comboBox1.Items.Clear();
            using (var conn = new SQLiteConnection("Data Source=unicomtic.db;Version=3;"))
            {
                conn.Open();
                var cmd = new SQLiteCommand("SELECT SubjectID, SubjectName FROM Subjects", conn);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    comboBox1.Items.Add(new ComboBoxItem(
                        reader["SubjectName"].ToString(),
                        Convert.ToInt32(reader["SubjectID"])
                    ));
                }
            }
        }

        private void LoadTimetables()
        {
            using (var conn = new SQLiteConnection("Data Source=unicomtic.db;Version=3;"))
            {
                conn.Open();
                var adapter = new SQLiteDataAdapter(@"
                    SELECT TimetableID, s.SubjectName, t.TimeSlot, r.RoomName 
                    FROM Timetables t
                    JOIN Subjects s ON t.SubjectID = s.SubjectID
                    JOIN Rooms r ON t.RoomID = r.RoomID", conn);
                var dt = new DataTable();
                adapter.Fill(dt);
                dataGridView1.DataSource = dt;

                if (dataGridView1.Columns.Count > 0)
                {
                    dataGridView1.Columns["TimetableID"].HeaderText = "ID";
                    dataGridView1.Columns["SubjectName"].HeaderText = "Subject";
                    dataGridView1.Columns["TimeSlot"].HeaderText = "Time Slot";
                    dataGridView1.Columns["RoomName"].HeaderText = "Room";
                }
            }
        }

        private void LoadRooms()
        {
            comboBox2.Items.Clear();
            using (var conn = new SQLiteConnection("Data Source=unicomtic.db;Version=3;"))
            {
                conn.Open();
                var cmd = new SQLiteCommand("SELECT RoomID, RoomName FROM Rooms", conn);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    comboBox2.Items.Add(new ComboBoxItem(
                        reader["RoomName"].ToString(),
                        Convert.ToInt32(reader["RoomID"])
                    ));
                }
            }
        }

        public class ComboBoxItem
        {
            public string Text { get; set; }
            public int Value { get; set; }

            public ComboBoxItem(string text, int value)
            {
                Text = text;
                Value = value;
            }

            public override string ToString()
            {
                return Text;
            }
        }
    }
}
