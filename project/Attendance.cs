using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace assignment_3
{
    public class Attendance
    {
        public int AttendanceID { get; private set; }
        public bool IsPresent { get; set; }

        static int nextId;

        public Attendance(bool isPresent = false)
        {
            AttendanceID = Interlocked.Increment(ref nextId);
            this.IsPresent = isPresent;
            addAttendance(this);
            SaveManager.SaveToJson(_attendance_List, nameof(_attendance_List));
        }

        public static List<Attendance> GetAttendanceExtent() =>
            new List<Attendance>(_attendance_List);

        private static List<Attendance> _attendance_List = new();

        private static void addAttendance(Attendance attendance)
        {
            if (attendance is null)
            {
                throw new ArgumentException($"{nameof(attendance)} cannot be null.");
            }
            _attendance_List.Add(attendance);
        }

        public void MarkAttendance() { // If student is present, then mark attendance
        }
    }
}
