using System;
using System.Collections.Generic;
using System.ComponentModel;
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

    public class Student : Person
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
            set => _gpa = value == 0.0f || value == 0 ? 0.0f : ValidateGPA(value);
        }

        private static int nextId = 1;

        private static Dictionary<Int32, List<string>> classSchedules = new();

        private List<IAssignment> _assignments = [Defaults.DEFAULT_CODING];
        public List<IAssignment> Assignments
        {
            get => new(_assignments);
            private set => _assignments = value;
        }

        private List<Grade> _grades = [Defaults.DEFAULT_GRADE];

        public List<Grade> Grades
        {
            get => new(_grades);
            private set => _grades = value;
        }
        private static int DF_TTABLE_KEY = 9999;

        private static Dictionary<int, TimeTable> TimeTables =
            new() { [DF_TTABLE_KEY] = Defaults.DEFAULT_TIMETABLE, };

        private static List<Student> _studentList = new();
        private int? TimeTableKey { get; set; }

        public Student(
            ClassLevel classLevel,
            string name,
            string email,
            string[] addressLines,
            string password
        )
            : base(name, email, addressLines, password)
        {
            StudentID = Interlocked.Increment(ref nextId);
            ClassLevel = classLevel;
            GPA = 0.0f;
            AddStudent(this);
            //SaveManager.SaveToJson(_studentList, nameof(_studentList));
        }

        public Student(Person person, ClassLevel classLevel)
            : base(
                person.Name,
                person.Email.Address.ToString(),
                person.AddressLines,
                person.Password
            )
        {
            StudentID = Interlocked.Increment(ref nextId);
            ClassLevel = classLevel;
            GPA = 0.0f;
            AddStudent(this);
        }

        private void AddStudent(Student student)
        {
            if (student == null)
            {
                throw new ArgumentException($"{nameof(student)} cannot be null.");
            }

            _studentList ??= new();

            if (_studentList.Contains(student))
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

            if (gpa is < 3.0f or > 5.0f)
            {
                throw new ValidationException("GPA must be between 3.0 and 5.0.");
            }

            return gpa;
        }

        private float CalculateGPA(List<Grade> grades)
        {
            uint sum = 0;
            uint count = 0;
            foreach (var grade in grades)
            {
                if (grade == Defaults.DEFAULT_GRADE)
                    continue;
                sum += grade.GradeValue;
                count += 1;
            }
            return sum / count;
        }

        public void AddGrade(Grade grade)
        {
            if (grade.Student == this)
            {
                throw new ArgumentException("Grade is already assigned.");
            }
            if (_grades.Contains(grade))
                throw new ArgumentException("Grade already exists.");
            _grades.Add(grade);
            grade.Student = this;

            GPA = CalculateGPA(_grades);
        }

        public void RemoveGrade(Grade grade)
        {
            if (grade.Student != this)
            {
                throw new ArgumentException(
                    $"Grade is not assigned to this student's grade, but assigned to {nameof(grade.Student)}."
                );
            }
            if (!_grades.Contains(grade))
                throw new ArgumentException("Grade does not exist.");
            _grades.Remove(grade);
            grade.ClearStudent();
            GPA = CalculateGPA(_grades);
        }

        public void UpdateGrade(Grade grade)
        {
            RemoveGrade(grade);
            AddGrade(grade);
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
        private static Dictionary<int, Dictionary<int, DateTime?>> studentAssignmentsSubmission = new();
        public void SubmitAssignment(IAssignment assignment)
        {
            ArgumentNullException.ThrowIfNull(assignment);
            if (assignment.SubmissionDate.HasValue)
            {
                throw new InvalidOperationException("This assignment has already been submitted.");
            }
            if (DateTime.Now > assignment.DueDate)
            {
                throw new InvalidOperationException(
                    "Cannot submit the assignment after the due date"
                );
            }
            assignment.Submit(this); 
            _assignments.Add(assignment);
            if (!studentAssignmentsSubmission.ContainsKey(StudentID))
            {
                studentAssignmentsSubmission[StudentID] = new Dictionary<int, DateTime?>();
            }
            studentAssignmentsSubmission[StudentID][assignment.AssignmentID] = DateTime.Now;
        }

        public void RemoveAssignmentSubmission(IAssignment assignment)
        {
            ArgumentNullException.ThrowIfNull(assignment);
            ArgumentNullException.ThrowIfNull(assignment.SubmissionDate);
            if (!_assignments.Contains(assignment))
                throw new ArgumentException(
                    $"The assignment {nameof(assignment)} is not submitted"
                );
            if (assignment.SubmittingStudent != this)
                throw new InvalidOperationException(
                    "This assignment does not belong to the current student."
                );
            assignment.SubmissionDate = null;
            _assignments.Remove(assignment);
        }

        public void EditAssignmentSubmission(IAssignment assignment)
        {
            ArgumentNullException.ThrowIfNull(assignment);
            ArgumentNullException.ThrowIfNull(assignment.SubmissionDate);

            if (!_assignments.Contains(assignment))
                throw new ArgumentException(
                    $"The assignment {nameof(assignment)} is not submitted"
                );

            if (assignment.SubmittingStudent != this)
                throw new InvalidOperationException(
                    "This assignment does not belong to the current student."
                );

            assignment.SubmissionDate = DateTime.UtcNow;
            _assignments.Remove(assignment);
            _assignments.Add(assignment);
        }

        public void AddTimeTable(TimeTable timeTable)
        {
            ArgumentNullException.ThrowIfNull(timeTable);

            if (timeTable.Student != null || TimeTables.ContainsKey(timeTable.Id))
            {
                throw new InvalidOperationException(
                    $"TimeTable {timeTable.Id} is already assigned to Student."
                );
            }
            TimeTableKey = timeTable.Id;
            TimeTables[timeTable.Id] = timeTable;
            timeTable.AssignStudent(this);
            TimeTables.Remove(DF_TTABLE_KEY);
        }

        public void RemoveTimeTable()
        {
            if (TimeTableKey == null)
                throw new InvalidOperationException($"No timetable is assigned to Student.");

            TimeTable timeTable = TimeTables[TimeTableKey.Value];
            timeTable.RemoveStudent();
            TimeTables.Remove(TimeTableKey.Value);
            TimeTableKey = null;
            TimeTables[DF_TTABLE_KEY] = Defaults.DEFAULT_TIMETABLE;
        }

        public void ResetStudent()
        {
            _assignments.Clear();
            _grades.Clear();
            RemoveTimeTable();
            _studentList.Remove(this);
        }
    }
}
