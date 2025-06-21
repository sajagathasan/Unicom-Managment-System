using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicom_Tic_Management_System
{
    internal class Marks
    {
        public int MarkID { get; set; }
        public int StudentID { get; set; }
        public int ExamID { get; set; }
        public int Score { get; set; }
        public string StudentName { get; set; } // For display purposes
        public string ExamName { get; set; } // For display purposes
        public string SubjectName { get; set; } // For display purposes
    }
}
