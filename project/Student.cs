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

        private float _gpa;
        static int nextId;

        public Student(ClassLevel classLevel, float gpa, float gPA)
        {
            StudentID = Interlocked.Increment(ref nextId);
            ClassLevel = classLevel;
            _gpa = gpa;
            GPA = gPA;
        }

        public float GPA
        {
            get { return _gpa; }
            set { _gpa = GpaValidation(value); }
        }

        private float GpaValidation(float gpa)
        {
            if (gpa is > 3.0f and < 5.0f)
                return gpa;
            throw new ValidationException();
        }

        public void viewClassSchedule() { }

        public void submitAssignment() { }
    }
}
