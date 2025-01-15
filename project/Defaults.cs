using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace assignment_3
{
    public static class Defaults
    {
        public static readonly Student DEFAULT_STUDENT = new(ClassLevel.Freshman);
        public static readonly Coding DEFAULT_CODING =
            new("DEFAULT", new DateTime(2026, 01, 01), "Python", "http://def.com");
        public static readonly Essay DEFAULT_ESSAY =
            new("DEFAULT_ESSAY", new DateTime(2026, 01, 01), 500, 1000);
        public static readonly Grade DEFAULT_GRADE = new(3);
        public static readonly Timeslot DEFAULT_TIMESLOT = new Timeslot(
            1,                                       
            new DateTime(2026, 01, 01),                  
            TimeSpan.FromHours(9),                       
            TimeSpan.FromHours(10)                  
        );
        public static readonly Exam DEFAULT_EXAM = new(new DateTime(2026, 01, 01), DEFAULT_TIMESLOT);
        public static readonly Classroom DEFAULT_CLASSROOM = new Classroom(1, 0); 
        
    }
    }

