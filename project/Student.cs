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

        private ClassLevel _classLevel;
        public ClassLevel ClassLevel
        {
            get => _classLevel;
            set => _classLevel = ValidateClassLevel(value);
        }

        private float _gpa;
        public float GPA
        {
            get => _gpa;
            set => _gpa = ValidateGPA(value);
        }

        private static int nextId;

        private static readonly Dictionary<int, List<string>> classSchedules = new();
        private static readonly List<string> assignments = new();

        public Student(ClassLevel classLevel, float gpa)
        {
            StudentID = Interlocked.Increment(ref nextId);
            ClassLevel = classLevel;
            GPA = gpa;
            AddStudent(this);
            SaveManager.SaveToJson(_studentList, nameof(_studentList));
        }

        private static readonly List<Student> _studentList = new();

        private static void AddStudent(Student student)
        {
            if (student == null)
            {
                throw new ArgumentException($"{nameof(student)} cannot be null.");
            }

            if (_studentList.Any(s => s.StudentID == student.StudentID))
            {
                throw new ArgumentException(
                    $"A student with ID {student.StudentID} already exists."
                );
            }

            _studentList.Add(student);
        }

        public static List<Student> GetStudentExtent() => new List<Student>(_studentList);

        private float ValidateGPA(float gpa)
        {
            if (Math.Round(gpa, 2) != gpa)
            {
                throw new ArgumentException(
                    $"Invalid GPA value '{gpa}', GPA can only have up to 2 decimal points."
                );
            }

            if (gpa >= 3.0f && gpa <= 5.0f)
            {
                return gpa;
            }

            throw new ValidationException("GPA must be between 3.0 and 5.0.");
        }

        private ClassLevel ValidateClassLevel(ClassLevel classLevel)
        {
            if (!Enum.IsDefined(typeof(ClassLevel), classLevel))
            {
                throw new ArgumentException($"Invalid ClassLevel value: {classLevel}.");
            }
            return classLevel;
        }

        public void ViewClassSchedule()
        {
            if (!classSchedules.ContainsKey(StudentID))
            {
                Console.WriteLine($"No class schedule found for student ID {StudentID}.");
                return;
            }

            var schedule = classSchedules[StudentID];
            Console.WriteLine($"Class Schedule for Student ID {StudentID}:");
            foreach (var course in schedule)
            {
                Console.WriteLine($"- {course}");
            }
        }

        public void SubmitAssignment(string assignment)
        {
            if (string.IsNullOrWhiteSpace(assignment))
            {
                throw new ArgumentException("Assignment cannot be null or empty.");
            }

            assignments.Add(assignment);
            Console.WriteLine($"Assignment submitted successfully for Student ID {StudentID}: {assignment}");
        }

        public static void AddClassToSchedule(int studentId, string course)
        {
            if (string.IsNullOrWhiteSpace(course))
            {
                throw new ArgumentException("Course cannot be null or empty.");
            }

            if (!classSchedules.ContainsKey(studentId))
            {
                classSchedules[studentId] = new List<string>();
            }

            classSchedules[studentId].Add(course);
        }
    }
}