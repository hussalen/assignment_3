using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace assignment_3
{
    public static class Defaults
    {
        public static readonly Student DEFAULT_STUDENT =
            new(
                ClassLevel.Freshman,
                "Jeremy Willis",
                "jeremy@gmail.com",
                ["4095 Patterson Lake Rd", "Pinckney", "MI 48169"],
                "test123"
            );
        public static readonly Coding DEFAULT_CODING =
            new("DEFAULT", new DateTime(2026, 01, 01), "Python", "http://def.com");
        public static readonly Grade DEFAULT_GRADE = new(3);

        public static readonly TimeTable DEFAULT_TIMETABLE = new(Day.WEDNESDAY);

        public static readonly Timeslot DEFAULT_TIMESLOT = new Timeslot(
            1,
            new DateTime(2026, 01, 01),
            TimeSpan.FromHours(9),
            TimeSpan.FromHours(10)
        );
        public static readonly Exam DEFAULT_EXAM =
            new(new DateTime(2026, 01, 01), DEFAULT_TIMESLOT);

        public static readonly Teacher DEFAULT_TEACHER =
            new(
                "Jeremy Willis",
                "jeremy@gmail.com",
                ["4095 Patterson Lake Rd", "Pinckney", "MI 48169"],
                "test123"
            );
        public static readonly Subject DEFAULT_SUBJECT = new("default subject", 4);
        public static readonly Admin DEFAULT_ADMIN =
            new(
                "Jeremy Willis",
                "jeremy@gmail.com",
                ["4095 Patterson Lake Rd", "Pinckney", "MI 48169"],
                "test123"
            );
        public static readonly Classroom DEFAULT_CLASSROOM = new Classroom(1, 0); 
        public static readonly Essay DEFAULT_ESSAY = new("Default", new DateTime(2027, 06, 06),20,200);
    }
}
