using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace UnicomTICManagementSystem
{
    // Model classes
    public class User
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }

    public class Course
    {
        public int CourseID { get; set; }
        public string CourseName { get; set; }
    }

    public class Subject
    {
        public int SubjectID { get; set; }
        public string SubjectName { get; set; }
        public int CourseID { get; set; }
        public string CourseName { get; set; } // For display purposes
    }

    public class Student
    {
        public int StudentID { get; set; }
        public string Name { get; set; }
        public int CourseID { get; set; }
        public string CourseName { get; set; } // For display purposes
    }

    public class Exam
    {
        public int ExamID { get; set; }
        public string ExamName { get; set; }
        public int SubjectID { get; set; }
        public string SubjectName { get; set; } // For display purposes
    }

    public class Mark
    {
        public int MarkID { get; set; }
        public int StudentID { get; set; }
        public int ExamID { get; set; }
        public int Score { get; set; }
        public string StudentName { get; set; } // For display purposes
        public string ExamName { get; set; } // For display purposes
        public string SubjectName { get; set; } // For display purposes
    }

    public class Room
    {
        public int RoomID { get; set; }
        public string RoomName { get; set; }
        public string RoomType { get; set; }
    }

    public class Timetable
    {
        public int TimetableID { get; set; }
        public int SubjectID { get; set; }
        public string TimeSlot { get; set; }
        public int RoomID { get; set; }
        public string SubjectName { get; set; } // For display purposes
        public string RoomName { get; set; } // For display purposes
        public string RoomType { get; set; } // For display purposes
    }

    public class DatabaseManager
    {
        private static DatabaseManager _instance;
        private readonly string _connectionString;

        private DatabaseManager()
        {
            _connectionString = "Data Source=unicomtic.db;Version=3;";
        }

        public static DatabaseManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new DatabaseManager();
                return _instance;
            }
        }

        public void InitializeDatabase()
        {
            try
            {
                using (var connection = new SQLiteConnection(_connectionString))
                {
                    connection.Open();
                    CreateTables(connection);
                    InsertDefaultData(connection);
                }
            }
            catch (Exception ex)
            {
                LogError($"Database initialization failed: {ex.Message}");
                throw;
            }
        }

        private void CreateTables(SQLiteConnection connection)
        {
            string[] createTableCommands = {
                @"CREATE TABLE IF NOT EXISTS Users (
                    UserID INTEGER PRIMARY KEY AUTOINCREMENT,
                    Username TEXT UNIQUE NOT NULL,
                    Password TEXT NOT NULL,
                    Role TEXT NOT NULL
                );",
                @"CREATE TABLE IF NOT EXISTS Courses (
                    CourseID INTEGER PRIMARY KEY AUTOINCREMENT,
                    CourseName TEXT NOT NULL
                );",
                @"CREATE TABLE IF NOT EXISTS Subjects (
                    SubjectID INTEGER PRIMARY KEY AUTOINCREMENT,
                    SubjectName TEXT NOT NULL,
                    CourseID INTEGER,
                    FOREIGN KEY(CourseID) REFERENCES Courses(CourseID)
                );",
                @"CREATE TABLE IF NOT EXISTS Students (
                    StudentID INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name TEXT NOT NULL,
                    CourseID INTEGER,
                    FOREIGN KEY(CourseID) REFERENCES Courses(CourseID)
                );",
                @"CREATE TABLE IF NOT EXISTS Exams (
                    ExamID INTEGER PRIMARY KEY AUTOINCREMENT,
                    ExamName TEXT NOT NULL,
                    SubjectID INTEGER,
                    FOREIGN KEY(SubjectID) REFERENCES Subjects(SubjectID)
                );",
                @"CREATE TABLE IF NOT EXISTS Marks (
                    MarkID INTEGER PRIMARY KEY AUTOINCREMENT,
                    StudentID INTEGER,
                    ExamID INTEGER,
                    Score INTEGER,
                    FOREIGN KEY(StudentID) REFERENCES Students(StudentID),
                    FOREIGN KEY(ExamID) REFERENCES Exams(ExamID)
                );",
                @"CREATE TABLE IF NOT EXISTS Rooms (
                    RoomID INTEGER PRIMARY KEY AUTOINCREMENT,
                    RoomName TEXT NOT NULL,
                    RoomType TEXT NOT NULL
                );",
                @"CREATE TABLE IF NOT EXISTS Timetables (
                    TimetableID INTEGER PRIMARY KEY AUTOINCREMENT,
                    SubjectID INTEGER,
                    TimeSlot TEXT NOT NULL,
                    RoomID INTEGER,
                    FOREIGN KEY(SubjectID) REFERENCES Subjects(SubjectID),
                    FOREIGN KEY(RoomID) REFERENCES Rooms(RoomID)
                );"
            };

            foreach (string command in createTableCommands)
            {
                using (var cmd = new SQLiteCommand(command, connection))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void InsertDefaultData(SQLiteConnection connection)
        {
            // Check if default data already exists
            using (var cmd = new SQLiteCommand("SELECT COUNT(*) FROM Users", connection))
            {
                if (Convert.ToInt64(cmd.ExecuteScalar()) > 0)
                    return; // Data already exists
            }

            // Insert default users
            string[] defaultUsers = {
                "INSERT INTO Users (Username, Password, Role) VALUES ('admin', 'admin123', 'Admin');",
                "INSERT INTO Users (Username, Password, Role) VALUES ('staff1', 'staff123', 'Staff');",
                "INSERT INTO Users (Username, Password, Role) VALUES ('student1', 'student123', 'Student');",
                "INSERT INTO Users (Username, Password, Role) VALUES ('lecturer1', 'lecturer123', 'Lecturer');"
            };

            // Insert default rooms
            string[] defaultRooms = {
                "INSERT INTO Rooms (RoomName, RoomType) VALUES ('Lab 1', 'Lab');",
                "INSERT INTO Rooms (RoomName, RoomType) VALUES ('Lab 2', 'Lab');",
                "INSERT INTO Rooms (RoomName, RoomType) VALUES ('Lab 3', 'Lab');",
                "INSERT INTO Rooms (RoomName, RoomType) VALUES ('Hall A', 'Hall');",
                "INSERT INTO Rooms (RoomName, RoomType) VALUES ('Hall B', 'Hall');",
                "INSERT INTO Rooms (RoomName, RoomType) VALUES ('Hall C', 'Hall');"
            };

            // Insert sample courses
            string[] defaultCourses = {
                "INSERT INTO Courses (CourseName) VALUES ('BSc Computer Science');",
                "INSERT INTO Courses (CourseName) VALUES ('BSc Information Technology');",
                "INSERT INTO Courses (CourseName) VALUES ('BSc Software Engineering');"
            };

            foreach (string command in defaultUsers.Concat(defaultRooms).Concat(defaultCourses))
            {
                using (var cmd = new SQLiteCommand(command, connection))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // User methods
        public async Task<User> AuthenticateUserAsync(string username, string password)
        {
            return await Task.Run(() =>
            {
                using (var connection = new SQLiteConnection(_connectionString))
                {
                    connection.Open();
                    string query = "SELECT * FROM Users WHERE Username = @username AND Password = @password";
                    using (var cmd = new SQLiteCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@password", password);
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new User
                                {
                                    UserID = reader.GetInt32(reader.GetOrdinal("UserID")),
                                    Username = reader.GetString(reader.GetOrdinal("Username")),
                                    Password = reader.GetString(reader.GetOrdinal("Password")),
                                    Role = reader.GetString(reader.GetOrdinal("Role"))
                                };
                            }
                        }
                    }
                }
                return null;
            });
        }

        // Course methods
        public async Task<List<Course>> GetCoursesAsync()
        {
            return await Task.Run(() =>
            {
                var courses = new List<Course>();
                using (var connection = new SQLiteConnection(_connectionString))
                {
                    connection.Open();
                    string query = "SELECT * FROM Courses ORDER BY CourseName";
                    using (var cmd = new SQLiteCommand(query, connection))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            courses.Add(new Course
                            {
                                CourseID = reader.GetInt32(reader.GetOrdinal("CourseID")),
                                CourseName = reader.GetString(reader.GetOrdinal("CourseName"))
                            });
                        }
                    }
                }
                return courses;
            });
        }

        public async Task<bool> AddCourseAsync(Course course)
        {
            if (course == null || string.IsNullOrWhiteSpace(course.CourseName))
                return false;

            return await Task.Run(() =>
            {
                try
                {
                    using (var connection = new SQLiteConnection(_connectionString))
                    {
                        connection.Open();
                        string query = "INSERT INTO Courses (CourseName) VALUES (@courseName)";
                        using (var cmd = new SQLiteCommand(query, connection))
                        {
                            cmd.Parameters.AddWithValue("@courseName", course.CourseName);
                            return cmd.ExecuteNonQuery() > 0;
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogError($"AddCourseAsync failed: {ex.Message}");
                    return false;
                }
            });
        }

        public async Task<bool> UpdateCourseAsync(Course course)
        {
            if (course == null || string.IsNullOrWhiteSpace(course.CourseName))
                return false;

            return await Task.Run(() =>
            {
                try
                {
                    using (var connection = new SQLiteConnection(_connectionString))
                    {
                        connection.Open();
                        string query = "UPDATE Courses SET CourseName = @courseName WHERE CourseID = @courseId";
                        using (var cmd = new SQLiteCommand(query, connection))
                        {
                            cmd.Parameters.AddWithValue("@courseName", course.CourseName);
                            cmd.Parameters.AddWithValue("@courseId", course.CourseID);
                            return cmd.ExecuteNonQuery() > 0;
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogError($"UpdateCourseAsync failed: {ex.Message}");
                    return false;
                }
            });
        }

        public async Task<bool> DeleteCourseAsync(int courseId)
        {
            return await Task.Run(() =>
            {
                try
                {
                    using (var connection = new SQLiteConnection(_connectionString))
                    {
                        connection.Open();
                        string query = "DELETE FROM Courses WHERE CourseID = @courseId";
                        using (var cmd = new SQLiteCommand(query, connection))
                        {
                            cmd.Parameters.AddWithValue("@courseId", courseId);
                            return cmd.ExecuteNonQuery() > 0;
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogError($"DeleteCourseAsync failed: {ex.Message}");
                    return false;
                }
            });
        }

        // Subject methods
        public async Task<List<Subject>> GetSubjectsAsync()
        {
            return await Task.Run(() =>
            {
                var subjects = new List<Subject>();
                using (var connection = new SQLiteConnection(_connectionString))
                {
                    connection.Open();
                    string query = @"SELECT s.*, c.CourseName
                                   FROM Subjects s
                                   LEFT JOIN Courses c ON s.CourseID = c.CourseID
                                   ORDER BY s.SubjectName";
                    using (var cmd = new SQLiteCommand(query, connection))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            subjects.Add(new Subject
                            {
                                SubjectID = reader.GetInt32(reader.GetOrdinal("SubjectID")),
                                SubjectName = reader.GetString(reader.GetOrdinal("SubjectName")),
                                CourseID = reader.IsDBNull(reader.GetOrdinal("CourseID")) ? 0 : reader.GetInt32(reader.GetOrdinal("CourseID")),
                                CourseName = reader.IsDBNull(reader.GetOrdinal("CourseName")) ? string.Empty : reader.GetString(reader.GetOrdinal("CourseName"))
                            });
                        }
                    }
                }
                return subjects;
            });
        }

        public async Task<bool> AddSubjectAsync(Subject subject)
        {
            if (subject == null || string.IsNullOrWhiteSpace(subject.SubjectName))
                return false;

            return await Task.Run(() =>
            {
                try
                {
                    using (var connection = new SQLiteConnection(_connectionString))
                    {
                        connection.Open();
                        string query = "INSERT INTO Subjects (SubjectName, CourseID) VALUES (@subjectName, @courseId)";
                        using (var cmd = new SQLiteCommand(query, connection))
                        {
                            cmd.Parameters.AddWithValue("@subjectName", subject.SubjectName);
                            cmd.Parameters.AddWithValue("@courseId", subject.CourseID == 0 ? (object)DBNull.Value : subject.CourseID);
                            return cmd.ExecuteNonQuery() > 0;
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogError($"AddSubjectAsync failed: {ex.Message}");
                    return false;
                }
            });
        }

        public async Task<bool> UpdateSubjectAsync(Subject subject)
        {
            if (subject == null || string.IsNullOrWhiteSpace(subject.SubjectName))
                return false;

            return await Task.Run(() =>
            {
                try
                {
                    using (var connection = new SQLiteConnection(_connectionString))
                    {
                        connection.Open();
                        string query = "UPDATE Subjects SET SubjectName = @subjectName, CourseID = @courseId WHERE SubjectID = @subjectId";
                        using (var cmd = new SQLiteCommand(query, connection))
                        {
                            cmd.Parameters.AddWithValue("@subjectName", subject.SubjectName);
                            cmd.Parameters.AddWithValue("@courseId", subject.CourseID == 0 ? (object)DBNull.Value : subject.CourseID);
                            cmd.Parameters.AddWithValue("@subjectId", subject.SubjectID);
                            return cmd.ExecuteNonQuery() > 0;
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogError($"UpdateSubjectAsync failed: {ex.Message}");
                    return false;
                }
            });
        }

        public async Task<bool> DeleteSubjectAsync(int subjectId)
        {
            return await Task.Run(() =>
            {
                try
                {
                    using (var connection = new SQLiteConnection(_connectionString))
                    {
                        connection.Open();
                        string query = "DELETE FROM Subjects WHERE SubjectID = @subjectId";
                        using (var cmd = new SQLiteCommand(query, connection))
                        {
                            cmd.Parameters.AddWithValue("@subjectId", subjectId);
                            return cmd.ExecuteNonQuery() > 0;
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogError($"DeleteSubjectAsync failed: {ex.Message}");
                    return false;
                }
            });
        }

        // Student methods
        public async Task<List<Student>> GetStudentsAsync()
        {
            return await Task.Run(() =>
            {
                var students = new List<Student>();
                using (var connection = new SQLiteConnection(_connectionString))
                {
                    connection.Open();
                    string query = @"SELECT s.*, c.CourseName
                                   FROM Students s
                                   LEFT JOIN Courses c ON s.CourseID = c.CourseID
                                   ORDER BY s.Name";
                    using (var cmd = new SQLiteCommand(query, connection))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            students.Add(new Student
                            {
                                StudentID = reader.GetInt32(reader.GetOrdinal("StudentID")),
                                Name = reader.GetString(reader.GetOrdinal("Name")),
                                CourseID = reader.IsDBNull(reader.GetOrdinal("CourseID")) ? 0 : reader.GetInt32(reader.GetOrdinal("CourseID")),
                                CourseName = reader.IsDBNull(reader.GetOrdinal("CourseName")) ? string.Empty : reader.GetString(reader.GetOrdinal("CourseName"))
                            });
                        }
                    }
                }
                return students;
            });
        }

        public async Task<bool> AddStudentAsync(Student student)
        {
            if (student == null || string.IsNullOrWhiteSpace(student.Name))
                return false;

            return await Task.Run(() =>
            {
                try
                {
                    using (var connection = new SQLiteConnection(_connectionString))
                    {
                        connection.Open();
                        string query = "INSERT INTO Students (Name, CourseID) VALUES (@name, @courseId)";
                        using (var cmd = new SQLiteCommand(query, connection))
                        {
                            cmd.Parameters.AddWithValue("@name", student.Name);
                            cmd.Parameters.AddWithValue("@courseId", student.CourseID == 0 ? (object)DBNull.Value : student.CourseID);
                            return cmd.ExecuteNonQuery() > 0;
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogError($"AddStudentAsync failed: {ex.Message}");
                    return false;
                }
            });
        }

        public async Task<bool> UpdateStudentAsync(Student student)
        {
            if (student == null || string.IsNullOrWhiteSpace(student.Name))
                return false;

            return await Task.Run(() =>
            {
                try
                {
                    using (var connection = new SQLiteConnection(_connectionString))
                    {
                        connection.Open();
                        string query = "UPDATE Students SET Name = @name, CourseID = @courseId WHERE StudentID = @studentId";
                        using (var cmd = new SQLiteCommand(query, connection))
                        {
                            cmd.Parameters.AddWithValue("@name", student.Name);
                            cmd.Parameters.AddWithValue("@courseId", student.CourseID == 0 ? (object)DBNull.Value : student.CourseID);
                            cmd.Parameters.AddWithValue("@studentId", student.StudentID);
                            return cmd.ExecuteNonQuery() > 0;
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogError($"UpdateStudentAsync failed: {ex.Message}");
                    return false;
                }
            });
        }

        public async Task<bool> DeleteStudentAsync(int studentId)
        {
            return await Task.Run(() =>
            {
                try
                {
                    using (var connection = new SQLiteConnection(_connectionString))
                    {
                        connection.Open();
                        string query = "DELETE FROM Students WHERE StudentID = @studentId";
                        using (var cmd = new SQLiteCommand(query, connection))
                        {
                            cmd.Parameters.AddWithValue("@studentId", studentId);
                            return cmd.ExecuteNonQuery() > 0;
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogError($"DeleteStudentAsync failed: {ex.Message}");
                    return false;
                }
            });
        }

        // Room methods
        public async Task<List<Room>> GetRoomsAsync()
        {
            return await Task.Run(() =>
            {
                var rooms = new List<Room>();
                using (var connection = new SQLiteConnection(_connectionString))
                {
                    connection.Open();
                    string query = "SELECT * FROM Rooms ORDER BY RoomType, RoomName";
                    using (var cmd = new SQLiteCommand(query, connection))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            rooms.Add(new Room
                            {
                                RoomID = reader.GetInt32(reader.GetOrdinal("RoomID")),
                                RoomName = reader.GetString(reader.GetOrdinal("RoomName")),
                                RoomType = reader.GetString(reader.GetOrdinal("RoomType"))
                            });
                        }
                    }
                }
                return rooms;
            });
        }

        public async Task<bool> AddRoomAsync(Room room)
        {
            if (room == null || string.IsNullOrWhiteSpace(room.RoomName) || string.IsNullOrWhiteSpace(room.RoomType))
                return false;

            return await Task.Run(() =>
            {
                try
                {
                    using (var connection = new SQLiteConnection(_connectionString))
                    {
                        connection.Open();
                        string query = "INSERT INTO Rooms (RoomName, RoomType) VALUES (@roomName, @roomType)";
                        using (var cmd = new SQLiteCommand(query, connection))
                        {
                            cmd.Parameters.AddWithValue("@roomName", room.RoomName);
                            cmd.Parameters.AddWithValue("@roomType", room.RoomType);
                            return cmd.ExecuteNonQuery() > 0;
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogError($"AddRoomAsync failed: {ex.Message}");
                    return false;
                }
            });
        }

        public async Task<bool> UpdateRoomAsync(Room room)
        {
            if (room == null || string.IsNullOrWhiteSpace(room.RoomName) || string.IsNullOrWhiteSpace(room.RoomType))
                return false;

            return await Task.Run(() =>
            {
                try
                {
                    using (var connection = new SQLiteConnection(_connectionString))
                    {
                        connection.Open();
                        string query = "UPDATE Rooms SET RoomName = @roomName, RoomType = @roomType WHERE RoomID = @roomId";
                        using (var cmd = new SQLiteCommand(query, connection))
                        {
                            cmd.Parameters.AddWithValue("@roomName", room.RoomName);
                            cmd.Parameters.AddWithValue("@roomType", room.RoomType);
                            cmd.Parameters.AddWithValue("@roomId", room.RoomID);
                            return cmd.ExecuteNonQuery() > 0;
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogError($"UpdateRoomAsync failed: {ex.Message}");
                    return false;
                }
            });
        }

        public async Task<bool> DeleteRoomAsync(int roomId)
        {
            return await Task.Run(() =>
            {
                try
                {
                    using (var connection = new SQLiteConnection(_connectionString))
                    {
                        connection.Open();
                        string query = "DELETE FROM Rooms WHERE RoomID = @roomId";
                        using (var cmd = new SQLiteCommand(query, connection))
                        {
                            cmd.Parameters.AddWithValue("@roomId", roomId);
                            return cmd.ExecuteNonQuery() > 0;
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogError($"DeleteRoomAsync failed: {ex.Message}");
                    return false;
                }
            });
        }

        // Exam methods
        public async Task<List<Exam>> GetExamsAsync()
        {
            return await Task.Run(() =>
            {
                var exams = new List<Exam>();
                using (var connection = new SQLiteConnection(_connectionString))
                {
                    connection.Open();
                    string query = @"SELECT e.*, s.SubjectName
                                   FROM Exams e
                                   LEFT JOIN Subjects s ON e.SubjectID = s.SubjectID
                                   ORDER BY e.ExamName";
                    using (var cmd = new SQLiteCommand(query, connection))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            exams.Add(new Exam
                            {
                                ExamID = reader.GetInt32(reader.GetOrdinal("ExamID")),
                                ExamName = reader.GetString(reader.GetOrdinal("ExamName")),
                                SubjectID = reader.IsDBNull(reader.GetOrdinal("SubjectID")) ? 0 : reader.GetInt32(reader.GetOrdinal("SubjectID")),
                                SubjectName = reader.IsDBNull(reader.GetOrdinal("SubjectName")) ? string.Empty : reader.GetString(reader.GetOrdinal("SubjectName"))
                            });
                        }
                    }
                }
                return exams;
            });
        }

        public async Task<bool> AddExamAsync(Exam exam)
        {
            if (exam == null || string.IsNullOrWhiteSpace(exam.ExamName))
                return false;

            return await Task.Run(() =>
            {
                try
                {
                    using (var connection = new SQLiteConnection(_connectionString))
                    {
                        connection.Open();
                        string query = "INSERT INTO Exams (ExamName, SubjectID) VALUES (@examName, @subjectId)";
                        using (var cmd = new SQLiteCommand(query, connection))
                        {
                            cmd.Parameters.AddWithValue("@examName", exam.ExamName);
                            cmd.Parameters.AddWithValue("@subjectId", exam.SubjectID == 0 ? (object)DBNull.Value : exam.SubjectID);
                            return cmd.ExecuteNonQuery() > 0;
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogError($"AddExamAsync failed: {ex.Message}");
                    return false;
                }
            });
        }

        public async Task<bool> UpdateExamAsync(Exam exam)
        {
            if (exam == null || string.IsNullOrWhiteSpace(exam.ExamName))
                return false;

            return await Task.Run(() =>
            {
                try
                {
                    using (var connection = new SQLiteConnection(_connectionString))
                    {
                        connection.Open();
                        string query = "UPDATE Exams SET ExamName = @examName, SubjectID = @subjectId WHERE ExamID = @examId";
                        using (var cmd = new SQLiteCommand(query, connection))
                        {
                            cmd.Parameters.AddWithValue("@examName", exam.ExamName);
                            cmd.Parameters.AddWithValue("@subjectId", exam.SubjectID == 0 ? (object)DBNull.Value : exam.SubjectID);
                            cmd.Parameters.AddWithValue("@examId", exam.ExamID);
                            return cmd.ExecuteNonQuery() > 0;
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogError($"UpdateExamAsync failed: {ex.Message}");
                    return false;
                }
            });
        }

        public async Task<bool> DeleteExamAsync(int examId)
        {
            return await Task.Run(() =>
            {
                try
                {
                    using (var connection = new SQLiteConnection(_connectionString))
                    {
                        connection.Open();
                        string query = "DELETE FROM Exams WHERE ExamID = @examId";
                        using (var cmd = new SQLiteCommand(query, connection))
                        {
                            cmd.Parameters.AddWithValue("@examId", examId);
                            return cmd.ExecuteNonQuery() > 0;
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogError($"DeleteExamAsync failed: {ex.Message}");
                    return false;
                }
            });
        }

        // Mark methods
        public async Task<List<Mark>> GetMarksAsync()
        {
            return await Task.Run(() =>
            {
                var marks = new List<Mark>();
                using (var connection = new SQLiteConnection(_connectionString))
                {
                    connection.Open();
                    string query = @"SELECT m.*, s.Name as StudentName, e.ExamName, sub.SubjectName
                                   FROM Marks m
                                   LEFT JOIN Students s ON m.StudentID = s.StudentID
                                   LEFT JOIN Exams e ON m.ExamID = e.ExamID
                                   LEFT JOIN Subjects sub ON e.SubjectID = sub.SubjectID
                                   ORDER BY s.Name, e.ExamName";
                    using (var cmd = new SQLiteCommand(query, connection))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            marks.Add(new Mark
                            {
                                MarkID = reader.GetInt32(reader.GetOrdinal("MarkID")),
                                StudentID = reader.GetInt32(reader.GetOrdinal("StudentID")),
                                ExamID = reader.GetInt32(reader.GetOrdinal("ExamID")),
                                Score = reader.GetInt32(reader.GetOrdinal("Score")),
                                StudentName = reader.IsDBNull(reader.GetOrdinal("StudentName")) ? string.Empty : reader.GetString(reader.GetOrdinal("StudentName")),
                                ExamName = reader.IsDBNull(reader.GetOrdinal("ExamName")) ? string.Empty : reader.GetString(reader.GetOrdinal("ExamName")),
                                SubjectName = reader.IsDBNull(reader.GetOrdinal("SubjectName")) ? string.Empty : reader.GetString(reader.GetOrdinal("SubjectName"))
                            });
                        }
                    }
                }
                return marks;
            });
        }

        public async Task<bool> AddMarkAsync(Mark mark)
        {
            if (mark == null || mark.StudentID == 0 || mark.ExamID == 0)
                return false;

            return await Task.Run(() =>
            {
                try
                {
                    using (var connection = new SQLiteConnection(_connectionString))
                    {
                        connection.Open();
                        string query = "INSERT INTO Marks (StudentID, ExamID, Score) VALUES (@studentId, @examId, @score)";
                        using (var cmd = new SQLiteCommand(query, connection))
                        {
                            cmd.Parameters.AddWithValue("@studentId", mark.StudentID);
                            cmd.Parameters.AddWithValue("@examId", mark.ExamID);
                            cmd.Parameters.AddWithValue("@score", mark.Score);
                            return cmd.ExecuteNonQuery() > 0;
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogError($"AddMarkAsync failed: {ex.Message}");
                    return false;
                }
            });
        }

        public async Task<bool> UpdateMarkAsync(Mark mark)
        {
            if (mark == null || mark.StudentID == 0 || mark.ExamID == 0)
                return false;

            return await Task.Run(() =>
            {
                try
                {
                    using (var connection = new SQLiteConnection(_connectionString))
                    {
                        connection.Open();
                        string query = "UPDATE Marks SET StudentID = @studentId, ExamID = @examId, Score = @score WHERE MarkID = @markId";
                        using (var cmd = new SQLiteCommand(query, connection))
                        {
                            cmd.Parameters.AddWithValue("@studentId", mark.StudentID);
                            cmd.Parameters.AddWithValue("@examId", mark.ExamID);
                            cmd.Parameters.AddWithValue("@score", mark.Score);
                            cmd.Parameters.AddWithValue("@markId", mark.MarkID);
                            return cmd.ExecuteNonQuery() > 0;
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogError($"UpdateMarkAsync failed: {ex.Message}");
                    return false;
                }
            });
        }

        public async Task<bool> DeleteMarkAsync(int markId)
        {
            return await Task.Run(() =>
            {
                try
                {
                    using (var connection = new SQLiteConnection(_connectionString))
                    {
                        connection.Open();
                        string query = "DELETE FROM Marks WHERE MarkID = @markId";
                        using (var cmd = new SQLiteCommand(query, connection))
                        {
                            cmd.Parameters.AddWithValue("@markId", markId);
                            return cmd.ExecuteNonQuery() > 0;
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogError($"DeleteMarkAsync failed: {ex.Message}");
                    return false;
                }
            });
        }

        // Timetable methods
        public async Task<List<Timetable>> GetTimetablesAsync()
        {
            return await Task.Run(() =>
            {
                var timetables = new List<Timetable>();
                using (var connection = new SQLiteConnection(_connectionString))
                {
                    connection.Open();
                    string query = @"SELECT t.*, s.SubjectName, r.RoomName, r.RoomType
                                   FROM Timetables t
                                   LEFT JOIN Subjects s ON t.SubjectID = s.SubjectID
                                   LEFT JOIN Rooms r ON t.RoomID = r.RoomID
                                   ORDER BY t.TimeSlot, s.SubjectName";
                    using (var cmd = new SQLiteCommand(query, connection))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            timetables.Add(new Timetable
                            {
                                TimetableID = reader.GetInt32(reader.GetOrdinal("TimetableID")),
                                SubjectID = reader.IsDBNull(reader.GetOrdinal("SubjectID")) ? 0 : reader.GetInt32(reader.GetOrdinal("SubjectID")),
                                TimeSlot = reader.GetString(reader.GetOrdinal("TimeSlot")),
                                RoomID = reader.IsDBNull(reader.GetOrdinal("RoomID")) ? 0 : reader.GetInt32(reader.GetOrdinal("RoomID")),
                                SubjectName = reader.IsDBNull(reader.GetOrdinal("SubjectName")) ? string.Empty : reader.GetString(reader.GetOrdinal("SubjectName")),
                                RoomName = reader.IsDBNull(reader.GetOrdinal("RoomName")) ? string.Empty : reader.GetString(reader.GetOrdinal("RoomName")),
                                RoomType = reader.IsDBNull(reader.GetOrdinal("RoomType")) ? string.Empty : reader.GetString(reader.GetOrdinal("RoomType"))
                            });
                        }
                    }
                }
                return timetables;
            });
        }

        public async Task<bool> AddTimetableAsync(Timetable timetable)
        {
            if (timetable == null || string.IsNullOrWhiteSpace(timetable.TimeSlot) || timetable.SubjectID == 0 || timetable.RoomID == 0)
                return false;

            return await Task.Run(() =>
            {
                try
                {
                    using (var connection = new SQLiteConnection(_connectionString))
                    {
                        connection.Open();
                        string query = "INSERT INTO Timetables (SubjectID, TimeSlot, RoomID) VALUES (@subjectId, @timeSlot, @roomId)";
                        using (var cmd = new SQLiteCommand(query, connection))
                        {
                            cmd.Parameters.AddWithValue("@subjectId", timetable.SubjectID);
                            cmd.Parameters.AddWithValue("@timeSlot", timetable.TimeSlot);
                            cmd.Parameters.AddWithValue("@roomId", timetable.RoomID);
                            return cmd.ExecuteNonQuery() > 0;
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogError($"AddTimetableAsync failed: {ex.Message}");
                    return false;
                }
            });
        }

        public async Task<bool> UpdateTimetableAsync(Timetable timetable)
        {
            if (timetable == null || string.IsNullOrWhiteSpace(timetable.TimeSlot) || timetable.SubjectID == 0 || timetable.RoomID == 0)
                return false;

            return await Task.Run(() =>
            {
                try
                {
                    using (var connection = new SQLiteConnection(_connectionString))
                    {
                        connection.Open();
                        string query = "UPDATE Timetables SET SubjectID = @subjectId, TimeSlot = @timeSlot, RoomID = @roomId WHERE TimetableID = @timetableId";
                        using (var cmd = new SQLiteCommand(query, connection))
                        {
                            cmd.Parameters.AddWithValue("@subjectId", timetable.SubjectID);
                            cmd.Parameters.AddWithValue("@timeSlot", timetable.TimeSlot);
                            cmd.Parameters.AddWithValue("@roomId", timetable.RoomID);
                            cmd.Parameters.AddWithValue("@timetableId", timetable.TimetableID);
                            return cmd.ExecuteNonQuery() > 0;
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogError($"UpdateTimetableAsync failed: {ex.Message}");
                    return false;
                }
            });
        }

        public async Task<bool> DeleteTimetableAsync(int timetableId)
        {
            return await Task.Run(() =>
            {
                try
                {
                    using (var connection = new SQLiteConnection(_connectionString))
                    {
                        connection.Open();
                        string query = "DELETE FROM Timetables WHERE TimetableID = @timetableId";
                        using (var cmd = new SQLiteCommand(query, connection))
                        {
                            cmd.Parameters.AddWithValue("@timetableId", timetableId);
                            return cmd.ExecuteNonQuery() > 0;
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogError($"DeleteTimetableAsync failed: {ex.Message}");
                    return false;
                }
            });
        }

        // Bonus: Get top 3 students by average marks
        public async Task<List<dynamic>> GetTopStudentsAsync()
        {
            return await Task.Run(() =>
            {
                var topStudents = new List<dynamic>();
                using (var connection = new SQLiteConnection(_connectionString))
                {
                    connection.Open();
                    string query = @"SELECT s.Name, AVG(m.Score) as AverageScore
                                   FROM Students s
                                   LEFT JOIN Marks m ON s.StudentID = m.StudentID
                                   GROUP BY s.StudentID, s.Name
                                   ORDER BY AverageScore DESC
                                   LIMIT 3";
                    using (var cmd = new SQLiteCommand(query, connection))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            topStudents.Add(new
                            {
                                Name = reader.GetString(reader.GetOrdinal("Name")),
                                AverageScore = reader.IsDBNull(reader.GetOrdinal("AverageScore"))
                                    ? 0.0
                                    : Math.Round(reader.GetDouble(reader.GetOrdinal("AverageScore")), 2)
                            });
                        }
                    }
                }
                return topStudents;
            });
        }

        // Error logging method
        public void LogError(string error)
        {
            try
            {
                string logPath = "error_log.txt";
                string logEntry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {error}\n";
                System.IO.File.AppendAllText(logPath, logEntry);
            }
            catch
            {
                // Ignore logging errors
            }
        }
    }
}
/*

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicom_Tic_Management_System;

namespace Unicom_Tic_Management_System
{
    internal class Timetable
    {
        public int TimetableID { get; set; }
        public int SubjectID { get; set; }
        public string TimeSlot { get; set; }
        public int RoomID { get; set; }
        public string SubjectName { get; set; } // For display purposes
        public string RoomName { get; set; } // For display purposes
        public string RoomType { get; set; } // For display purposes
    }
}
    public class DatabaseManager
    {
        private static DatabaseManager _instance;
        private readonly string _connectionString;

        private DatabaseManager()
        {
            _connectionString = "Data Source=unicomtic.db;Version=3;";
        }

        public static DatabaseManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new DatabaseManager();
                return _instance;
            }
        }

        public void InitializeDatabase()
        {
            try
            {
                using (var connection = new SQLiteConnection(_connectionString))
                {
                    connection.Open();
                    CreateTables(connection);
                    InsertDefaultData(connection);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Database initialization failed: {ex.Message}");
            }
        }

        private void CreateTables(SQLiteConnection connection)
        {
            string[] createTableCommands = {
                @"CREATE TABLE IF NOT EXISTS Users (
                    UserID INTEGER PRIMARY KEY AUTOINCREMENT,
                    Username TEXT UNIQUE NOT NULL,
                    Password TEXT NOT NULL,
                    Role TEXT NOT NULL
                );",
                @"CREATE TABLE IF NOT EXISTS Courses (
                    CourseID INTEGER PRIMARY KEY AUTOINCREMENT,
                    CourseName TEXT NOT NULL
                );",
                @"CREATE TABLE IF NOT EXISTS Subjects (
                    SubjectID INTEGER PRIMARY KEY AUTOINCREMENT,
                    SubjectName TEXT NOT NULL,
                    CourseID INTEGER,
                    FOREIGN KEY(CourseID) REFERENCES Courses(CourseID)
                );",
                @"CREATE TABLE IF NOT EXISTS Students (
                    StudentID INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name TEXT NOT NULL,
                    CourseID INTEGER,
                    FOREIGN KEY(CourseID) REFERENCES Courses(CourseID)
                );",
                @"CREATE TABLE IF NOT EXISTS Exams (
                    ExamID INTEGER PRIMARY KEY AUTOINCREMENT,
                    ExamName TEXT NOT NULL,
                    SubjectID INTEGER,
                    FOREIGN KEY(SubjectID) REFERENCES Subjects(SubjectID)
                );",
                @"CREATE TABLE IF NOT EXISTS Marks (
                    MarkID INTEGER PRIMARY KEY AUTOINCREMENT,
                    StudentID INTEGER,
                    ExamID INTEGER,
                    Score INTEGER,
                    FOREIGN KEY(StudentID) REFERENCES Students(StudentID),
                    FOREIGN KEY(ExamID) REFERENCES Exams(ExamID)
                );",
                @"CREATE TABLE IF NOT EXISTS Rooms (
                    RoomID INTEGER PRIMARY KEY AUTOINCREMENT,
                    RoomName TEXT NOT NULL,
                    RoomType TEXT NOT NULL
                );",
                @"CREATE TABLE IF NOT EXISTS Timetables (
                    TimetableID INTEGER PRIMARY KEY AUTOINCREMENT,
                    SubjectID INTEGER,
                    TimeSlot TEXT NOT NULL,
                    RoomID INTEGER,
                    FOREIGN KEY(SubjectID) REFERENCES Subjects(SubjectID),
                    FOREIGN KEY(RoomID) REFERENCES Rooms(RoomID)
                );"
            };

            foreach (string command in createTableCommands)
            {
                using (var cmd = new SQLiteCommand(command, connection))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void InsertDefaultData(SQLiteConnection connection)
        {
            // Check if default data already exists
            using (var cmd = new SQLiteCommand("SELECT COUNT(*) FROM Users", connection))
            {
                if (Convert.ToInt32(cmd.ExecuteScalar()) > 0)
                    return; // Data already exists
            }

            // Insert default users
            string[] defaultUsers = {
                "INSERT INTO Users (Username, Password, Role) VALUES ('admin', 'admin123', 'Admin');",
                "INSERT INTO Users (Username, Password, Role) VALUES ('staff1', 'staff123', 'Staff');",
                "INSERT INTO Users (Username, Password, Role) VALUES ('student1', 'student123', 'Student');",
                "INSERT INTO Users (Username, Password, Role) VALUES ('lecturer1', 'lecturer123', 'Lecturer');"
            };

            // Insert default rooms
            string[] defaultRooms = {
                "INSERT INTO Rooms (RoomName, RoomType) VALUES ('Lab 1', 'Lab');",
                "INSERT INTO Rooms (RoomName, RoomType) VALUES ('Lab 2', 'Lab');",
                "INSERT INTO Rooms (RoomName, RoomType) VALUES ('Lab 3', 'Lab');",
                "INSERT INTO Rooms (RoomName, RoomType) VALUES ('Hall A', 'Hall');",
                "INSERT INTO Rooms (RoomName, RoomType) VALUES ('Hall B', 'Hall');",
                "INSERT INTO Rooms (RoomName, RoomType) VALUES ('Hall C', 'Hall');"
            };

            // Insert sample courses
            string[] defaultCourses = {
                "INSERT INTO Courses (CourseName) VALUES ('BSc Computer Science');",
                "INSERT INTO Courses (CourseName) VALUES ('BSc Information Technology');",
                "INSERT INTO Courses (CourseName) VALUES ('BSc Software Engineering');"
            };

            foreach (string command in defaultUsers)
            {
                using (var cmd = new SQLiteCommand(command, connection))
                {
                    cmd.ExecuteNonQuery();
                }
            }

            foreach (string command in defaultRooms)
            {
                using (var cmd = new SQLiteCommand(command, connection))
                {
                    cmd.ExecuteNonQuery();
                }
            }

            foreach (string command in defaultCourses)
            {
                using (var cmd = new SQLiteCommand(command, connection))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // User methods
        public async Task<User> AuthenticateUserAsync(string username, string password)
        {
            return await Task.Run(() =>
            {
                using (var connection = new SQLiteConnection(_connectionString))
                {
                    connection.Open();
                    string query = "SELECT * FROM Users WHERE Username = @username AND Password = @password";
                    using (var cmd = new SQLiteCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@password", password);
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new User
                                {
                                    UserID = reader.GetInt32("UserID"),
                                    Username = reader.GetString("Username"),
                                    Password = reader.GetString("Password"),
                                    Role = reader.GetString("Role")
                                };
                            }
                        }
                    }
                }
                return null;
            });
        }

        // Course methods
        public async Task<List<Course>> GetCoursesAsync()
        {
            return await Task.Run(() =>
            {
                var courses = new List<Course>();
                using (var connection = new SQLiteConnection(_connectionString))
                {
                    connection.Open();
                    string query = "SELECT * FROM Courses ORDER BY CourseName";
                    using (var cmd = new SQLiteCommand(query, connection))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            courses.Add(new Course
                            {
                                CourseID = reader.GetInt32("CourseID"),
                                CourseName = reader.GetString("CourseName")
                            });
                        }
                    }
                }
                return courses;
            });
        }

        public async Task<bool> AddCourseAsync(Course course)
        {
            return await Task.Run(() =>
            {
                try
                {
                    using (var connection = new SQLiteConnection(_connectionString))
                    {
                        connection.Open();
                        string query = "INSERT INTO Courses (CourseName) VALUES (@courseName)";
                        using (var cmd = new SQLiteCommand(query, connection))
                        {
                            cmd.Parameters.AddWithValue("@courseName", course.CourseName);
                            return cmd.ExecuteNonQuery() > 0;
                        }
                    }
                }
                catch
                {
                    return false;
                }
            });
        }

        public async Task<bool> UpdateCourseAsync(Course course)
        {
            return await Task.Run(() =>
            {
                try
                {
                    using (var connection = new SQLiteConnection(_connectionString))
                    {
                        connection.Open();
                        string query = "UPDATE Courses SET CourseName = @courseName WHERE CourseID = @courseId";
                        using (var cmd = new SQLiteCommand(query, connection))
                        {
                            cmd.Parameters.AddWithValue("@courseName", course.CourseName);
                            cmd.Parameters.AddWithValue("@courseId", course.CourseID);
                            return cmd.ExecuteNonQuery() > 0;
                        }
                    }
                }
                catch
                {
                    return false;
                }
            });
        }

        public async Task<bool> DeleteCourseAsync(int courseId)
        {
            return await Task.Run(() =>
            {
                try
                {
                    using (var connection = new SQLiteConnection(_connectionString))
                    {
                        connection.Open();
                        string query = "DELETE FROM Courses WHERE CourseID = @courseId";
                        using (var cmd = new SQLiteCommand(query, connection))
                        {
                            cmd.Parameters.AddWithValue("@courseId", courseId);
                            return cmd.ExecuteNonQuery() > 0;
                        }
                    }
                }
                catch
                {
                    return false;
                }
            });
        }

        // Subject methods
        public async Task<List<Subject>> GetSubjectsAsync()
        {
            return await Task.Run(() =>
            {
                var subjects = new List<Subject>();
                using (var connection = new SQLiteConnection(_connectionString))
                {
                    connection.Open();
                    string query = @"SELECT s.*, c.CourseName
                                   FROM Subjects s
                                   JOIN Courses c ON s.CourseID = c.CourseID
                                   ORDER BY s.SubjectName";
                    using (var cmd = new SQLiteCommand(query, connection))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            subjects.Add(new Subject
                            {
                                SubjectID = reader.GetInt32("SubjectID"),
                                SubjectName = reader.GetString("SubjectName"),
                                CourseID = reader.GetInt32("CourseID"),
                                CourseName = reader.GetString("CourseName")
                            });
                        }
                    }
                }
                return subjects;
            });
        }

        public async Task<bool> AddSubjectAsync(Subject subject)
        {
            return await Task.Run(() =>
            {
                try
                {
                    using (var connection = new SQLiteConnection(_connectionString))
                    {
                        connection.Open();
                        string query = "INSERT INTO Subjects (SubjectName, CourseID) VALUES (@subjectName, @courseId)";
                        using (var cmd = new SQLiteCommand(query, connection))
                        {
                            cmd.Parameters.AddWithValue("@subjectName", subject.SubjectName);
                            cmd.Parameters.AddWithValue("@courseId", subject.CourseID);
                            return cmd.ExecuteNonQuery() > 0;
                        }
                    }
                }
                catch
                {
                    return false;
                }
            });
        }

        public async Task<bool> UpdateSubjectAsync(Subject subject)
        {
            return await Task.Run(() =>
            {
                try
                {
                    using (var connection = new SQLiteConnection(_connectionString))
                    {
                        connection.Open();
                        string query = "UPDATE Subjects SET SubjectName = @subjectName, CourseID = @courseId WHERE SubjectID = @subjectId";
                        using (var cmd = new SQLiteCommand(query, connection))
                        {
                            cmd.Parameters.AddWithValue("@subjectName", subject.SubjectName);
                            cmd.Parameters.AddWithValue("@courseId", subject.CourseID);
                            cmd.Parameters.AddWithValue("@subjectId", subject.SubjectID);
                            return cmd.ExecuteNonQuery() > 0;
                        }
                    }
                }
                catch
                {
                    return false;
                }
            });
        }

        public async Task<bool> DeleteSubjectAsync(int subjectId)
        {
            return await Task.Run(() =>
            {
                try
                {
                    using (var connection = new SQLiteConnection(_connectionString))
                    {
                        connection.Open();
                        string query = "DELETE FROM Subjects WHERE SubjectID = @subjectId";
                        using (var cmd = new SQLiteCommand(query, connection))
                        {
                            cmd.Parameters.AddWithValue("@subjectId", subjectId);
                            return cmd.ExecuteNonQuery() > 0;
                        }
                    }
                }
                catch
                {
                    return false;
                }
            });
        }

        // Student methods
        public async Task<List<Student>> GetStudentsAsync()
        {
            return await Task.Run(() =>
            {
                var students = new List<Student>();
                using (var connection = new SQLiteConnection(_connectionString))
                {
                    connection.Open();
                    string query = @"SELECT s.*, c.CourseName
                                   FROM Students s
                                   JOIN Courses c ON s.CourseID = c.CourseID
                                   ORDER BY s.Name";
                    using (var cmd = new SQLiteCommand(query, connection))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            students.Add(new Student
                            {
                                StudentID = reader.GetInt32("StudentID"),
                                Name = reader.GetString("Name"),
                                CourseID = reader.GetInt32("CourseID"),
                                CourseName = reader.GetString("CourseName")
                            });
                        }
                    }
                }
                return students;
            });
        }

        public async Task<bool> AddStudentAsync(Student student)
        {
            return await Task.Run(() =>
            {
                try
                {
                    using (var connection = new SQLiteConnection(_connectionString))
                    {
                        connection.Open();
                        string query = "INSERT INTO Students (Name, CourseID) VALUES (@name, @courseId)";
                        using (var cmd = new SQLiteCommand(query, connection))
                        {
                            cmd.Parameters.AddWithValue("@name", student.Name);
                            cmd.Parameters.AddWithValue("@courseId", student.CourseID);
                            return cmd.ExecuteNonQuery() > 0;
                        }
                    }
                }
                catch
                {
                    return false;
                }
            });
        }

        public async Task<bool> UpdateStudentAsync(Student student)
        {
            return await Task.Run(() =>
            {
                try
                {
                    using (var connection = new SQLiteConnection(_connectionString))
                    {
                        connection.Open();
                        string query = "UPDATE Students SET Name = @name, CourseID = @courseId WHERE StudentID = @studentId";
                        using (var cmd = new SQLiteCommand(query, connection))
                        {
                            cmd.Parameters.AddWithValue("@name", student.Name);
                            cmd.Parameters.AddWithValue("@courseId", student.CourseID);
                            cmd.Parameters.AddWithValue("@studentId", student.StudentID);
                            return cmd.ExecuteNonQuery() > 0;
                        }
                    }
                }
                catch
                {
                    return false;
                }
            });
        }

        public async Task<bool> DeleteStudentAsync(int studentId)
        {
            return await Task.Run(() =>
            {
                try
                {
                    using (var connection = new SQLiteConnection(_connectionString))
                    {
                        connection.Open();
                        string query = "DELETE FROM Students WHERE StudentID = @studentId";
                        using (var cmd = new SQLiteCommand(query, connection))
                        {
                            cmd.Parameters.AddWithValue("@studentId", studentId);
                            return cmd.ExecuteNonQuery() > 0;
                        }
                    }
                }
                catch
                {
                    return false;
                }
            });
        }

        // Room methods
        public async Task<List<Room>> GetRoomsAsync()
        {
            return await Task.Run(() =>
            {
                var rooms = new List<Room>();
                using (var connection = new SQLiteConnection(_connectionString))
                {
                    connection.Open();
                    string query = "SELECT * FROM Rooms ORDER BY RoomType, RoomName";
                    using (var cmd = new SQLiteCommand(query, connection))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            rooms.Add(new Room
                            {
                                RoomID = reader.GetInt32("RoomID"),
                                RoomName = reader.GetString("RoomName"),
                                RoomType = reader.GetString("RoomType")
                            });
                        }
                    }
                }
                return rooms;
            });
        }

        // Exam methods
        public async Task<List<Exam>> GetExamsAsync()
        {
            return await Task.Run(() =>
            {
                var exams = new List<Exam>();
                using (var connection = new SQLiteConnection(_connectionString))
                {
                    connection.Open();
                    string query = @"SELECT e.*, s.SubjectName
                                   FROM Exams e
                                   JOIN Subjects s ON e.SubjectID = s.SubjectID
                                   ORDER BY e.ExamName";
                    using (var cmd = new SQLiteCommand(query, connection))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            exams.Add(new Exam
                            {
                                ExamID = reader.GetInt32("ExamID"),
                                ExamName = reader.GetString("ExamName"),
                                SubjectID = reader.GetInt32("SubjectID"),
                                SubjectName = reader.GetString("SubjectName")
                            });
                        }
                    }
                }
                return exams;
            });
        }

        public async Task<bool> AddExamAsync(Exam exam)
        {
            return await Task.Run(() =>
            {
                try
                {
                    using (var connection = new SQLiteConnection(_connectionString))
                    {
                        connection.Open();
                        string query = "INSERT INTO Exams (ExamName, SubjectID) VALUES (@examName, @subjectId)";
                        using (var cmd = new SQLiteCommand(query, connection))
                        {
                            cmd.Parameters.AddWithValue("@examName", exam.ExamName);
                            cmd.Parameters.AddWithValue("@subjectId", exam.SubjectID);
                            return cmd.ExecuteNonQuery() > 0;
                        }
                    }
                }
                catch
                {
                    return false;
                }
            });
        }

        public async Task<bool> UpdateExamAsync(Exam exam)
        {
            return await Task.Run(() =>
            {
                try
                {
                    using (var connection = new SQLiteConnection(_connectionString))
                    {
                        connection.Open();
                        string query = "UPDATE Exams SET ExamName = @examName, SubjectID = @subjectId WHERE ExamID = @examId";
                        using (var cmd = new SQLiteCommand(query, connection))
                        {
                            cmd.Parameters.AddWithValue("@examName", exam.ExamName);
                            cmd.Parameters.AddWithValue("@subjectId", exam.SubjectID);
                            cmd.Parameters.AddWithValue("@examId", exam.ExamID);
                            return cmd.ExecuteNonQuery() > 0;
                        }
                    }
                }
                catch
                {
                    return false;
                }
            });
        }

        public async Task<bool> DeleteExamAsync(int examId)
        {
            return await Task.Run(() =>
            {
                try
                {
                    using (var connection = new SQLiteConnection(_connectionString))
                    {
                        connection.Open();
                        string query = "DELETE FROM Exams WHERE ExamID = @examId";
                        using (var cmd = new SQLiteCommand(query, connection))
                        {
                            cmd.Parameters.AddWithValue("@examId", examId);
                            return cmd.ExecuteNonQuery() > 0;
                        }
                    }
                }
                catch
                {
                    return false;
                }
            });
        }

        // Mark methods
        public async Task<List<Mark>> GetMarksAsync()
        {
            return await Task.Run(() =>
            {
                var marks = new List<Mark>();
                using (var connection = new SQLiteConnection(_connectionString))
                {
                    connection.Open();
                    string query = @"SELECT m.*, s.Name as StudentName, e.ExamName, sub.SubjectName
                                   FROM Marks m
                                   JOIN Students s ON m.StudentID = s.StudentID
                                   JOIN Exams e ON m.ExamID = e.ExamID
                                   JOIN Subjects sub ON e.SubjectID = sub.SubjectID
                                   ORDER BY s.Name, e.ExamName";
                    using (var cmd = new SQLiteCommand(query, connection))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            marks.Add(new Mark
                            {
                                MarkID = reader.GetInt32("MarkID"),
                                StudentID = reader.GetInt32("StudentID"),
                                ExamID = reader.GetInt32("ExamID"),
                                Score = reader.GetInt32("Score"),
                                StudentName = reader.GetString("StudentName"),
                                ExamName = reader.GetString("ExamName"),
                                SubjectName = reader.GetString("SubjectName")
                            });
                        }
                    }
                }
                return marks;
            });
        }

        public async Task<bool> AddMarkAsync(Mark mark)
        {
            return await Task.Run(() =>
            {
                try
                {
                    using (var connection = new SQLiteConnection(_connectionString))
                    {
                        connection.Open();
                        string query = "INSERT INTO Marks (StudentID, ExamID, Score) VALUES (@studentId, @examId, @score)";
                        using (var cmd = new SQLiteCommand(query, connection))
                        {
                            cmd.Parameters.AddWithValue("@studentId", mark.StudentID);
                            cmd.Parameters.AddWithValue("@examId", mark.ExamID);
                            cmd.Parameters.AddWithValue("@score", mark.Score);
                            return cmd.ExecuteNonQuery() > 0;
                        }
                    }
                }
                catch
                {
                    return false;
                }
            });
        }

        public async Task<bool> UpdateMarkAsync(Mark mark)
        {
            return await Task.Run(() =>
            {
                try
                {
                    using (var connection = new SQLiteConnection(_connectionString))
                    {
                        connection.Open();
                        string query = "UPDATE Marks SET StudentID = @studentId, ExamID = @examId, Score = @score WHERE MarkID = @markId";
                        using (var cmd = new SQLiteCommand(query, connection))
                        {
                            cmd.Parameters.AddWithValue("@studentId", mark.StudentID);
                            cmd.Parameters.AddWithValue("@examId", mark.ExamID);
                            cmd.Parameters.AddWithValue("@score", mark.Score);
                            cmd.Parameters.AddWithValue("@markId", mark.MarkID);
                            return cmd.ExecuteNonQuery() > 0;
                        }
                    }
                }
                catch
                {
                    return false;
                }
            });
        }

        public async Task<bool> DeleteMarkAsync(int markId)
        {
            return await Task.Run(() =>
            {
                try
                {
                    using (var connection = new SQLiteConnection(_connectionString))
                    {
                        connection.Open();
                        string query = "DELETE FROM Marks WHERE MarkID = @markId";
                        using (var cmd = new SQLiteCommand(query, connection))
                        {
                            cmd.Parameters.AddWithValue("@markId", markId);
                            return cmd.ExecuteNonQuery() > 0;
                        }
                    }
                }
                catch
                {
                    return false;
                }
            });
        }

        // Timetable methods
        public async Task<List<Timetable>> GetTimetablesAsync()
        {
            return await Task.Run(() =>
            {
                var timetables = new List<Timetable>();
                using (var connection = new SQLiteConnection(_connectionString))
                {
                    connection.Open();
                    string query = @"SELECT t.*, s.SubjectName, r.RoomName, r.RoomType
                                   FROM Timetables t
                                   JOIN Subjects s ON t.SubjectID = s.SubjectID
                                   JOIN Rooms r ON t.RoomID = r.RoomID
                                   ORDER BY t.TimeSlot, s.SubjectName";
                    using (var cmd = new SQLiteCommand(query, connection))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            timetables.Add(new Timetable
                            {
                                TimetableID = reader.GetInt32("TimetableID"),
                                SubjectID = reader.GetInt32("SubjectID"),
                                TimeSlot = reader.GetString("TimeSlot"),
                                RoomID = reader.GetInt32("RoomID"),
                                SubjectName = reader.GetString("SubjectName"),
                                RoomName = reader.GetString("RoomName"),
                                RoomType = reader.GetString("RoomType")
                            });
                        }
                    }
                }
                return timetables;
            });
        }

        public async Task<bool> AddTimetableAsync(Timetable timetable)
        {
            return await Task.Run(() =>
            {
                try
                {
                    using (var connection = new SQLiteConnection(_connectionString))
                    {
                        connection.Open();
                        string query = "INSERT INTO Timetables (SubjectID, TimeSlot, RoomID) VALUES (@subjectId, @timeSlot, @roomId)";
                        using (var cmd = new SQLiteCommand(query, connection))
                        {
                            cmd.Parameters.AddWithValue("@subjectId", timetable.SubjectID);
                            cmd.Parameters.AddWithValue("@timeSlot", timetable.TimeSlot);
                            cmd.Parameters.AddWithValue("@roomId", timetable.RoomID);
                            return cmd.ExecuteNonQuery() > 0;
                        }
                    }
                }
                catch
                {
                    return false;
                }
            });
        }

        public async Task<bool> UpdateTimetableAsync(Timetable timetable)
        {
            return await Task.Run(() =>
            {
                try
                {
                    using (var connection = new SQLiteConnection(_connectionString))
                    {
                        connection.Open();
                        string query = "UPDATE Timetables SET SubjectID = @subjectId, TimeSlot = @timeSlot, RoomID = @roomId WHERE TimetableID = @timetableId";
                        using (var cmd = new SQLiteCommand(query, connection))
                        {
                            cmd.Parameters.AddWithValue("@subjectId", timetable.SubjectID);
                            cmd.Parameters.AddWithValue("@timeSlot", timetable.TimeSlot);
                            cmd.Parameters.AddWithValue("@roomId", timetable.RoomID);
                            cmd.Parameters.AddWithValue("@timetableId", timetable.TimetableID);
                            return cmd.ExecuteNonQuery() > 0;
                        }
                    }
                }
                catch
                {
                    return false;
                }
            });
        }

        public async Task<bool> DeleteTimetableAsync(int timetableId)
        {
            return await Task.Run(() =>
            {
                try
                {
                    using (var connection = new SQLiteConnection(_connectionString))
                    {
                        connection.Open();
                        string query = "DELETE FROM Timetables WHERE TimetableID = @timetableId";
                        using (var cmd = new SQLiteCommand(query, connection))
                        {
                            cmd.Parameters.AddWithValue("@timetableId", timetableId);
                            return cmd.ExecuteNonQuery() > 0;
                        }
                    }
                }
                catch
                {
                    return false;
                }
            });
        }

        // Bonus: Get top 3 students by average marks
        public async Task<List<dynamic>> GetTopStudentsAsync()
        {
            return await Task.Run(() =>
            {
                var topStudents = new List<dynamic>();
                using (var connection = new SQLiteConnection(_connectionString))
                {
                    connection.Open();
                    string query = @"SELECT s.Name, AVG(m.Score) as AverageScore
                                   FROM Students s
                                   JOIN Marks m ON s.StudentID = m.StudentID
                                   GROUP BY s.StudentID, s.Name
                                   ORDER BY AverageScore DESC
                                   LIMIT 3";
                    using (var cmd = new SQLiteCommand(query, connection))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            topStudents.Add(new
                            {
                                Name = reader.GetString("Name"),
                                AverageScore = Math.Round(reader.GetDouble("AverageScore"), 2)
                            });
                        }
                    }
                }
                return topStudents;
            });
        }

        // Error logging method
        public void LogError(string error)
        {
            try
            {
                string logPath = "error_log.txt";
                string logEntry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {error}\n";
                File.AppendAllText(logPath, logEntry);
            }
            catch
            {
                // Ignore logging errors
            }
        }
    }
}*/