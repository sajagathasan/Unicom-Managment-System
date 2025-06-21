using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicom_Tic_Management_System
{
    internal class Room
    {
        public int RoomID { get; set; }
        public string RoomName { get; set; }
        public string RoomType { get; set; } // "Lab" or "Hall"
    }
}
