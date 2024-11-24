using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace assignment_3
{
    public class Attendance
    {
        public int AttendanceID { get; private set; }

        private bool _isPresent;
        public bool IsPresent
        {
            get => _isPresent;
            private set => _isPresent = value; // Restrict direct external modification
        }

        private static int nextId;
        private static readonly List<Attendance> _attendanceList = new();

        public Attendance()
        {
            AttendanceID = Interlocked.Increment(ref nextId);
            IsPresent = false;
            AddAttendance(this);
        }

        private static void AddAttendance(Attendance attendance)
        {
            if (attendance == null)
            {
                throw new ArgumentException($"{nameof(attendance)} cannot be null.");
            }
            _attendanceList.Add(attendance);
        }

        public static List<Attendance> GetAttendanceExtent() => new(_attendanceList);

        public void MarkAttendance(bool isPresent)
        {
            if (isPresent != true)
            {
                Console.WriteLine(
                    $"Attendance not marked for ID {AttendanceID} because the student is absent."
                );
                return;
            }

            IsPresent = true;
            Console.WriteLine($"Attendance marked as present for ID {AttendanceID}.");
        }
    }
}
