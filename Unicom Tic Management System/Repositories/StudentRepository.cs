using System;
using System.Collections.Generic;
using System.Data.SQLite;
using Unicom_Tic_Management_System;
using Unicom_Tic_Management_System.Model;

public class StudentsRepository
{
    private string connectionString = "Data Source=your_database.db;Version=3;";

    public void AddStudent(Student student)
    {
        using (var conn = new SQLiteConnection(connectionString))
        {
            conn.Open();
            string query = @"INSERT INTO Students 
                             (FirstName, LastName, EmailID, Address, MobileNumber, , Gender, NICNumber, DateOfBirth, CourseID, CourseName, UserName, PassWord) 
                             VALUES 
                             (@FirstName, @LastName, @EmailID, @Address, @MobileNumber, @Gender, @NICNumber, @DateOfBirth, @CourseID, @CourseName, @UserName, @PassWord)";

            using (var cmd = new SQLiteCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@FirstName", student.FirstName);
                cmd.Parameters.AddWithValue("@LastName", student.LastName);
                cmd.Parameters.AddWithValue("@EmailID", student.EmailID);
                cmd.Parameters.AddWithValue("@Address", student.Address);
                cmd.Parameters.AddWithValue("@MobileNumber", student.MobileNumber);
                cmd.Parameters.AddWithValue("@Gender", student.Gender);
                cmd.Parameters.AddWithValue("@NICNumber", student.NICNumber);
                cmd.Parameters.AddWithValue("@DateOfBirth", student.DateOfBirth);
                cmd.Parameters.AddWithValue("@CourseID", student.CourseID);
                cmd.Parameters.AddWithValue("@CourseName", student.CourseName);
                cmd.Parameters.AddWithValue("@UserName", student.UserName);
                cmd.Parameters.AddWithValue("@PassWord", student.PassWord);

                cmd.ExecuteNonQuery();
            }

        }
    }

    internal void AddTeacher(Teacher teacher)
    {
        throw new NotImplementedException();
    }
}
