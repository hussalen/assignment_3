using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace assignment_3
{
    public enum ClassLevel
    {
        Freshman,
        Sophomore,
        Junior,
        Senior
    }

    public class Student
    {
        public int StudentID { get; private set; }
        public ClassLevel ClassLevel { get; set; }
        static int nextId;

        public Student(ClassLevel classLevel, float GPA)
        {
            StudentID = Interlocked.Increment(ref nextId);
            ClassLevel = classLevel;
            this.GPA = GPA;
        }

        private float _gpa;

        public float GPA
        {
            get { return _gpa; }
            set { _gpa = GpaValidation(value); }
        }

        private float GpaValidation(float gpa)
        {
            if (Math.Round(gpa * 100) != gpa * 100)
            {
                throw new ArgumentException(
                    $"Invalid GPA value '{gpa}', GPA can only have up to 2 decimal points."
                );
            }
            if (gpa is >= 3.0f and <= 5.0f)
                return gpa;
            throw new ValidationException();
        }

        public void viewClassSchedule() { }

        public void submitAssignment() { }
    }
}
