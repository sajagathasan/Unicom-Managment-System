using System;
using System.Collections.Generic;
using System.Data.SQLite;
using Unicom_Tic_Management_System;
using Unicom_Tic_Management_System.Model;

namespace Unicom_Tic_Management_System.Contreller
{
    public class TeacherRepository
    {
        private string connectionString = "Data Source=your_database.db;Version=3;";

        public void AddTeacher(Teacher Teacher)
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                string query = @"INSERT INTO Teachers 
                             (FirstName, LastName, EmailID, Address, MobileNumber, Gender, NICNumber, DateOfBirth, CourseID, Subject, Qualification, Position, Username,PassWord) 
                             VALUES 
                             (@FirstName, @LastName, @EmailID, @Address, @MobileNumber, @Gender, @NICNumber, @DateOfBirth, @SubjectID, @Subject, @Qualification, @Position, @UserName, @PassWord)";

                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@FirstName", Teacher.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", Teacher.LastName);
                    cmd.Parameters.AddWithValue("@EmailID", Teacher.EmailID);
                    cmd.Parameters.AddWithValue("@Address", Teacher.Address);
                    cmd.Parameters.AddWithValue("@MobileNumber", Teacher.MobileNumber);
                    cmd.Parameters.AddWithValue("@Gender", Teacher.Gender);
                    cmd.Parameters.AddWithValue("@NICNumber", Teacher.NICNumber);
                    cmd.Parameters.AddWithValue("@DateOfBirth", Teacher.DateOfBirth);
                    cmd.Parameters.AddWithValue("@CourseID", Teacher.SubjectID);
                    cmd.Parameters.AddWithValue("@Subject", Teacher.Subject);
                    cmd.Parameters.AddWithValue("@UserName", Teacher.UserName);
                    cmd.Parameters.AddWithValue("@PassWord", Teacher.PassWord);


                    cmd.ExecuteNonQuery();
                }

            }
        }
    }
}