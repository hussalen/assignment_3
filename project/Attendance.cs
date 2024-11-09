using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace assignment_3
{
    public class Attendance
    {
        public int AttendanceID { get; private set; }
        public bool isPresent { get; set; }

        static int nextId;

        public Attendance(bool isPresent = false)
        {
            AttendanceID = Interlocked.Increment(ref nextId);
            this.isPresent = isPresent;
        }

        public void MarkAttendance() { // If student is present, then mark attendance
        }
    }
}
