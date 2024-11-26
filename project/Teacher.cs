using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace assignment_3
{
    public enum Availability
    {
        Available,
        Unavailable,
        OnLeave
    }

    public class Teacher
    {
        public string Name { get; set; }
        private static int nextId = 1;
        public int TeacherID { get; private set; }
        public List<Subject> Subject { get; set; }
        public Availability AvailabilityStatus { get; set; }

        public Teacher(List<Subject> subjects, string name)
        {
            Name = name;
            TeacherID = Interlocked.Increment(ref nextId);
            Subject = subjects;
            AvailabilityStatus = Availability.Available;
        }

        public void ViewSchedule() { }

        public void RequestScheduleChange() { }

        public void AssignRole() { }
    }
}
