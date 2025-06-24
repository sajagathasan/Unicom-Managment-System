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
                        CourseName TEXT,
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

                    CREATE TABLE IF NOT EXISTS Staff (
                        StaffID INTEGER PRIMARY KEY,
                        FirstName TEXT NOT NULL,
                        LastName TEXT NOT NULL,
                        EmailID TEXT NOT NULL,
                        Address TEXT NOT NULL,
                        MobileNumber TEXT NOT NULL,
                        Gender TEXT NOT NULL,
                        NICNumber TEXT NOT NULL,
                        DateOfBirth TEXT NOT NULL,
                        Qualification TEXT NOT NULL,
                        Position TEXT NOT NULL,
                        UserName TEXT NOT NULL,
                        PassWord TEXT NOT NULL
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

        public DataTable GetAllTeacher()
        {
            conn.Open();
            var dt = new DataTable();
            var adapter = new SQLiteDataAdapter("SELECT * FROM Teacher", conn);
            adapter.Fill(dt);
            conn.Close();
            return dt;
        }

        public DataTable GetAllStudent()
        {
            conn.Open();
            var dt = new DataTable();
            var adapter = new SQLiteDataAdapter("SELECT * FROM Student", conn);
            adapter.Fill(dt);
            conn.Close();
            return dt;
        }

        public DataTable GetAllStaff()
        {
            conn.Open();
            var dt = new DataTable();
            var adapter = new SQLiteDataAdapter("SELECT * FROM Staff", conn);
            adapter.Fill(dt);
            conn.Close();
            return dt;
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

        public DataTable GetAllSujects()
        {
            conn.Open();
            var dt = new DataTable();
            var adapter = new SQLiteDataAdapter("SELECT * FROM Sujects", conn);
            adapter.Fill(dt);
            conn.Close();
            return dt;
        }

        public DataTable GetAllExams()
        {
            conn.Open();
            var dt = new DataTable();
            var adapter = new SQLiteDataAdapter("SELECT * FROM Staff", conn);
            adapter.Fill(dt);
            conn.Close();
            return dt;
        }

        public DataTable GetAllMarks()
        {
            conn.Open();
            var dt = new DataTable();
            var adapter = new SQLiteDataAdapter("SELECT * FROM Staff", conn);
            adapter.Fill(dt);
            conn.Close();
            return dt;
        }

        public DataTable GetAllRooms()
        {
            conn.Open();
            var dt = new DataTable();
            var adapter = new SQLiteDataAdapter("SELECT * FROM Staff", conn);
            adapter.Fill(dt);
            conn.Close();
            return dt;
        }

        public DataTable GetAllTimetable()
        {
            conn.Open();
            var dt = new DataTable();
            var adapter = new SQLiteDataAdapter("SELECT * FROM Staff", conn);
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

        public void AddStaff(string firstName, string lastName, string email, string address, string mobile, string gender, string nic, string dob, string qualification, string position, string userName, string passWord)
        {
            conn.Open();
            var cmd = new SQLiteCommand(@"
        INSERT INTO Staff (
            FirstName, LastName, EmailID, Address, MobileNumber, Gender, NICNumber, DateOfBirth, Qualification, Position, UserName, PassWord
        ) VALUES (
            @f, @l, @e, @a, @m, @g, @n, @d, @q, @o, @u, @p
        )", conn);

            cmd.Parameters.AddWithValue("@f", firstName);
            cmd.Parameters.AddWithValue("@l", lastName);
            cmd.Parameters.AddWithValue("@e", email);
            cmd.Parameters.AddWithValue("@a", address);
            cmd.Parameters.AddWithValue("@m", mobile);
            cmd.Parameters.AddWithValue("@g", gender);
            cmd.Parameters.AddWithValue("@n", nic);
            cmd.Parameters.AddWithValue("@d", dob);
            cmd.Parameters.AddWithValue("@q", qualification);
            cmd.Parameters.AddWithValue("@o", position);
            cmd.Parameters.AddWithValue("@u", userName);
            cmd.Parameters.AddWithValue("@p", passWord);

            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public void AddSubject(string subjectName, string courseName, int courseId, int subjectId)
        {
            conn.Open();
            var cmd = new SQLiteCommand("INSERT INTO Subjects (SubjectName, CourseID, CourseName, SubjectID) VALUES (@n, @c, @o, @u)", conn);
            cmd.Parameters.AddWithValue("@n", subjectName);
            cmd.Parameters.AddWithValue("@o", courseName);
            cmd.Parameters.AddWithValue("@c", courseId);
            cmd.Parameters.AddWithValue("@u", subjectId);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public DataTable GetSubjects()
        {
            conn.Open();
            var dt = new DataTable();
            var adapter = new SQLiteDataAdapter(@"
                SELECT Subjects.SubjectID, Subjects.SubjectName, Courses.CourseName, Course.CourseID 
                FROM Subjects 
                JOIN Courses ON Subjects.CourseID = Courses.CourseID", conn);
            adapter.Fill(dt);
            conn.Close();
            return dt;
        }

        public void UpdateSubject(int subjectId, string subjectName, string courseName, int courseId)
        {
            conn.Open();
            var cmd = new SQLiteCommand("UPDATE Subjects SET SubjectName = @n, CourseName = @o, SubjectID = @c WHERE SubjectID = @u", conn);
            cmd.Parameters.AddWithValue("@n", subjectName);
            cmd.Parameters.AddWithValue("@o", courseName);
            cmd.Parameters.AddWithValue("@u", subjectId);
            cmd.Parameters.AddWithValue("@c", courseId);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public void DeleteSubject(int courseId, int subjectId, string subjectName, string courseName)
        {
            conn.Open();
            var cmd = new SQLiteCommand("DELETE FROM Subjects SET SubjectName = @o, CourseName = @c Subjects WHERE SubjectID = @u", conn);
            cmd.Parameters.AddWithValue("@n", subjectName);
            cmd.Parameters.AddWithValue("@o", courseName);
            cmd.Parameters.AddWithValue("@u", subjectId);
            cmd.Parameters.AddWithValue("@c", courseId);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public DataTable GetCourses()
        {
            conn.Open();
            var dt = new DataTable();
            var adapter = new SQLiteDataAdapter("SELECT * FROM Courses", conn);
            adapter.Fill(dt);
            conn.Close();
            return dt;
        }

        public bool UpdatePassword(string username, string newPassword)
        {
            conn.Open();
            var cmd = new SQLiteCommand("UPDATE Users SET Password = @pwd WHERE Username = @user", conn);
            cmd.Parameters.AddWithValue("@pwd", newPassword);
            cmd.Parameters.AddWithValue("@user", username);

            int rows = cmd.ExecuteNonQuery();
            conn.Close();

            return rows > 0;
        }



        public (string Role, string Name) GetUserRole(string username, string password)
        {
            conn.Open();

            // Admins
            var cmd = new SQLiteCommand("SELECT * FROM Users WHERE Username = @u AND Password = @p", conn);
            cmd.Parameters.AddWithValue("@u", username);
            cmd.Parameters.AddWithValue("@p", password);
            var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                string role = reader["Role"].ToString();
                reader.Close();
                conn.Close();
                return (role, username);
            }
            reader.Close();

            // Students
            cmd = new SQLiteCommand("SELECT * FROM Students WHERE UserName = @u AND PassWord = @p", conn);
            cmd.Parameters.AddWithValue("@u", username);
            cmd.Parameters.AddWithValue("@p", password);
            reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                string name = reader["FirstName"].ToString();
                reader.Close();
                conn.Close();
                return ("Student", name);
            }
            reader.Close();

            // Teachers
            cmd = new SQLiteCommand("SELECT * FROM Teachers WHERE UserName = @u AND PassWord = @p", conn);
            cmd.Parameters.AddWithValue("@u", username);
            cmd.Parameters.AddWithValue("@p", password);
            reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                string name = reader["FirstName"].ToString();
                reader.Close();
                conn.Close();
                return ("Teacher", name);
            }
            reader.Close();

            // Staff
            cmd = new SQLiteCommand("SELECT * FROM Staff WHERE UserName = @u AND PassWord = @p", conn);
            cmd.Parameters.AddWithValue("@u", username);
            cmd.Parameters.AddWithValue("@p", password);
            reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                string name = reader["FirstName"].ToString();
                reader.Close();
                conn.Close();
                return ("Staff", name);
            }
            reader.Close();


            conn.Close();
            return (null, null); // Not found
        }



    }
}
