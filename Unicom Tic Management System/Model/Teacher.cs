using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicom_Tic_Management_System.Model
{
    public class Teacher
    {
        public int TeacherID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailID { get; set; }
        public string Address { get; set; }
        public string MobileNumber { get; set; }
        public string Gender { get; set; }
        public string DateOfBirth { get; set; }
        public int SubjectID { get; set; }
        public string Subject { get; set; }
        public string Qualifiaction { get; set; }
        public string Position { get; set; }
        public object NICNumber { get; internal set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }
    }
}
