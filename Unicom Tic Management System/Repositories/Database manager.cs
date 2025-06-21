using System;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;

namespace Unicom_Tic_Management_System.Repositories
{
    public class DatabaseManager
    {
        private SQLiteConnection conn;
        private readonly string connectionString = "Data Source=unicomtic.db;Version=3;";
        public DatabaseManager()
        {
            conn = new SQLiteConnection("Data Source=unicomtic.db;Version=3;");
            InitializeDatabase();
        }

        private void InitializeDatabase()
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();

                using (var command = conn.CreateCommand())
                {

                    command.CommandText = @"
                     CREATE TABLE IF NOT EXISTS Users (
                        UserID INTEGER PRIMARY KEY,
                        Username TEXT,
                        Password TEXT,
                        Role TEXT
                     );

                    INSERT OR IGNORE INTO Users (UserID, Username, Password, Role) VALUES
                        (1, 'admin@', 'admin123', 'Admin');

                    CREATE TABLE IF NOT EXISTS Courses (
                        CourseID INTEGER PRIMARY KEY,
                        CourseName TEXT
                     );

                    CREATE TABLE IF NOT EXISTS Subjects (
                        SubjectID INTEGER PRIMARY KEY,
                        SubjectName TEXT,
                        CourseID INTEGER,
                        FOREIGN KEY(CourseID) REFERENCES Courses
                     );

                    CREATE TABLE IF NOT EXISTS Students (
                        StudentID INTEGER PRIMARY KEY,
                        FirstName TEXT NOT NULL,
                        LastName TEXT NOT NULL,
                        EmailID TEXT NOT NULL,
                        Address TEXT NOT NULL,
                        MobileNumber INTEGER NOT NULL,
                        Gender TEXT NOT NULL,
                        NICNumber INTEGER NOT NULL,
                        DateOfBirth TEXT NOT NULL,
                        CourseID INTEGER NOT NULL,
                        CourseName TEXT NOT NULL,
                        UserName TEXT NOT NULL,
                        PassWord TEXT NOT NULL,
                        FOREIGN KEY(CourseID) REFERENCES Courses
                     );

                    CREATE TABLE IF NOT EXISTS Teachers (
                        TeacherID INTEGER PRIMARY KEY,
                        FirstName TEXT NOT NULL,
                        LastName TEXT NOT NULL,
                        EmailID TEXT NOT NULL,
                        Address TEXT NOT NULL,
                        MobileNumber INTEGER NOT NULL,
                        Gender TEXT NOT NULL,
                        NICNumber INTEGER NOT NULL,
                        DateOfBirth TEXT NOT NULL,
                        Qualification TEXT NOT NULL,
                        Position TEXT NOT NULL,
                        SubjectID INTEGER NOT NULL,
                        Subject TEXT NOT NULL,
                        UserName TEXT NOT NULL,
                        PassWord TEXT NOT NULL,
                        FOREIGN KEY(SubjectID) REFERENCES Subjects(SubjectID)
                     );

                    CREATE TABLE IF NOT EXISTS Exams (
                        ExamID INTEGER PRIMARY KEY,
                        ExamName TEXT,
                        SubjectID INTEGER,
                        FOREIGN KEY(SubjectID) REFERENCES Subjects
                     );

                    CREATE TABLE IF NOT EXISTS Marks (
                        MarkID INTEGER PRIMARY KEY,
                        StudentID INTEGER,
                        ExamID INTEGER,
                        Score INTEGER,
                        FOREIGN KEY(StudentID) REFERENCES Students,
                        FOREIGN KEY(ExamID) REFERENCES Exams
                     );

                    CREATE TABLE IF NOT EXISTS Rooms (
                        RoomID INTEGER PRIMARY KEY,
                        RoomName TEXT,
                        RoomType TEXT
                     );

                    INSERT OR IGNORE INTO Rooms (RoomID, RoomName, RoomType) VALUES
                        (1, 'Lab 1', 'Lab'),
                        (2, 'Lab 2', 'Lab'),
                        (3, 'Hall A', 'Hall'),
                        (4, 'Hall B', 'Hall');

                    CREATE TABLE IF NOT EXISTS Timetables (
                        TimetableID INTEGER PRIMARY KEY,
                        SubjectID INTEGER,
                        TimeSlot TEXT,
                        RoomID INTEGER,
                        FOREIGN KEY(SubjectID) REFERENCES Subjects,
                        FOREIGN KEY(RoomID) REFERENCES Rooms
                     );
                     ";

                    command.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
        public DataTable GetAllCourses()
        {
            conn.Open();
            var adapter = new SQLiteDataAdapter("SELECT * FROM Courses", conn);
            var dt = new DataTable();
            adapter.Fill(dt);
            conn.Close();
            return dt;
        }

        public void AddStudent(string firstName, string lastName, string email, string address, string mobile, string gender, string nic, string dob, int courseId, string courseName, string userName, string passWord)
        {
            conn.Open();
            var cmd = new SQLiteCommand(@"
            INSERT INTO Students (
                FirstName, LastName, EmailID, Address, MobileNumber, Gender, NICNumber, DateOfBirth, CourseID ,CourseName, UserName, PassWord
            ) VALUES (
                @f, @l, @e, @a, @m, @g, @n, @d, @c, @o, @u, @p
            )", conn);

            cmd.Parameters.AddWithValue("@f", firstName);
            cmd.Parameters.AddWithValue("@l", lastName);
            cmd.Parameters.AddWithValue("@e", email);
            cmd.Parameters.AddWithValue("@a", address);
            cmd.Parameters.AddWithValue("@m", mobile);
            cmd.Parameters.AddWithValue("@g", gender);
            cmd.Parameters.AddWithValue("@n", nic);
            cmd.Parameters.AddWithValue("@d", dob);
            cmd.Parameters.AddWithValue("@c", courseId);
            cmd.Parameters.AddWithValue("@o", courseName);
            cmd.Parameters.AddWithValue("@u", userName);
            cmd.Parameters.AddWithValue("@p", passWord);    

            cmd.ExecuteNonQuery();
            MessageBox.Show("Student added successfully!");
        }

        public void AddTeacher(string firstName, string lastName, string email, string address, string mobile, string gender, string nic, string dob, int subjectId, string subject, string qualification, string position, string userName, string passWord)
        {
            conn.Open();
            var cmd = new SQLiteCommand(@"
            INSERT INTO Teachers (
                FirstName, LastName, EmailID, Address, MobileNumber, Gender, NICNumber, DateOfBirth, SubjectID, Subject, Qualification, Position, UserName, PassWord
            ) VALUES (
                @f, @l, @e, @a, @m, @g, @n, @d, @c, @s, @q, @o, @u, @p
            )", conn);

            cmd.Parameters.AddWithValue("@f", firstName);
            cmd.Parameters.AddWithValue("@l", lastName);
            cmd.Parameters.AddWithValue("@e", email);
            cmd.Parameters.AddWithValue("@a", address);
            cmd.Parameters.AddWithValue("@m", mobile);                 
            cmd.Parameters.AddWithValue("@g", gender);
            cmd.Parameters.AddWithValue("@n", nic);
            cmd.Parameters.AddWithValue("@d", dob);
            cmd.Parameters.AddWithValue("@c", subjectId);
            cmd.Parameters.AddWithValue("@s", subject);
            cmd.Parameters.AddWithValue("@q", qualification);
            cmd.Parameters.AddWithValue("@o", position);
            cmd.Parameters.AddWithValue("@u", userName);
            cmd.Parameters.AddWithValue("@p", passWord);


            cmd.ExecuteNonQuery();
            MessageBox.Show("Teacher added successfully!");
            conn.Close();

        }

    }
}
