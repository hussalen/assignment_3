using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace assignment_3
{
    public class Attendance
    {
        public int AttendanceID { get; private set; }
        public bool isPresent { get; private set; }

        static int nextId;

        public Attendance()
        {
            AttendanceID = Interlocked.Increment(ref nextId);
            this.isPresent = false;
        }

        public void MarkAttendance() { // If student is present, then mark attendance
        }
    }
}
