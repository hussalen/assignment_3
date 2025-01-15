using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace assignment_3
{
    public enum Availability
    {
        Available,
        Unavailable,
        OnLeave
    }

    public class Teacher : Person
    {
        private static int nextId = 1;
        private readonly List<Subject> _subjects;
        private readonly List<Timeslot> _assignedTimeslots;
        private readonly List<Grade> _grades;

        public string Name { get; set; }
        public int TeacherID { get; private set; }

        public Availability AvailabilityStatus { get; set; }

        private List<Subject> _subjects = new();


        public List<Subject> Subjects
        {
            get => new(_subjects);
            private set => _subjects = value;
        }

        public List<Timeslot> AssignedTimeslots => new List<Timeslot>(_assignedTimeslots);

        public List<Grade> Grades => new List<Grade>(_grades);

        private static List<Teacher> _teachersList = new();

        public static List<Teacher> GetTeachersExtent() => new List<Teacher>(_teachersList);

        public Teacher(string name, string email, string[] addressLines, string password)
            : base(name, email, addressLines, password)
        {
            TeacherID = Interlocked.Increment(ref nextId);
            AvailabilityStatus = Availability.Available;
            AddTeacher(this);
        }


            
            _subjects = new List<Subject>();
            _assignedTimeslots = new List<Timeslot>();
            _grades = new List<Grade>();

            Name = name;

        // INITIATE_COPY
        // TODO: remove association(s) here.
        public Teacher(Person person)
            : base(
                person.Name,
                person.Email.Address.ToString(),
                person.AddressLines,
                person.Password
            )
        {

            TeacherID = Interlocked.Increment(ref nextId);
            AvailabilityStatus = Availability.Available;
            AddTeacher(this);
        }

        private void AddTeacher(Teacher teacher)
        {
            if (teacher == null)
            {
                throw new ArgumentException($"{nameof(teacher)} cannot be null.");
            }

            _teachersList ??= new();

            if (_teachersList.Contains(teacher))
            {
                throw new ArgumentException(
                    $"A teacher with ID {teacher.TeacherID} already exists."
                );
            }

            _teachersList.Add(teacher);
        }

        private void RemoveTeacher(Teacher teacher)
        {
            if (teacher == null)
            {
                throw new ArgumentException($"{nameof(teacher)} cannot be null.");
            }

            ArgumentNullException.ThrowIfNull(_teachersList);

            if (!_teachersList.Contains(teacher))
            {
                throw new ArgumentException(
                    $"A teacher with ID {teacher.TeacherID} does not exist."
                );
            }

            _teachersList.Remove(teacher);
        }

        public void AssignSubject(Subject subject)
        {
            if (subject == null)
                throw new ArgumentNullException(nameof(subject));

            
            if (_subjects.Contains(subject))
                throw new ArgumentException("Subject is already assigned to this teacher.");

            
            if (subject.Teacher != this && subject.Teacher != null)
                throw new InvalidOperationException("Subject is already assigned to another teacher.");

            _subjects.Add(subject);

            
            if (subject.Teacher != this)
            {
                subject.Teacher = this;
            }
        }

        public void RemoveSubject(Subject subject)
        {
            if (subject == null)
                throw new ArgumentNullException(nameof(subject));

            if (!_subjects.Contains(subject))
                return;
            _subjects.Remove(subject);


                        if (subject.Teacher != this)
                throw new InvalidOperationException(
                    "Subject's teacher does not match this teacher�cannot unassign."
                );

            subject.Teacher = Defaults.DEFAULT_TEACHER;

            subject.RemoveTeacher(this);

        }

        public void ViewSchedule()
        {
            Console.WriteLine($"Schedule for Teacher {Name}:");
            foreach (var subject in _subjects)
            {
                Console.WriteLine($"- {subject.SubjectName} (ID: {subject.SubjectId})");
            }
        }


        public void AssignTimeslot(Timeslot timeslot)
        {
            if (timeslot == null)
                throw new ArgumentNullException(nameof(timeslot), "Timeslot cannot be null.");

            if (_assignedTimeslots.Contains(timeslot))
                throw new InvalidOperationException("This timeslot is already assigned to the teacher.");

            _assignedTimeslots.Add(timeslot);
        }

        public void RemoveTimeslot(Timeslot timeslot)
        {
            if (timeslot == null)
                throw new ArgumentNullException(nameof(timeslot), "Timeslot cannot be null.");

            if (!_assignedTimeslots.Remove(timeslot))
                throw new InvalidOperationException("This timeslot is not assigned to the teacher.");
        }

        public void AssignGrade(Grade grade)
        {
            if (grade == null)
                throw new ArgumentNullException(nameof(grade), "Grade cannot be null.");

            if (_grades.Contains(grade))
                throw new InvalidOperationException("This grade is already assigned to this teacher.");

            _grades.Add(grade);

            if (grade.Teacher != this)
            {
                grade.Teacher = this;
            }
        }

        public void RemoveGrade(Grade grade)
        {
            if (grade == null)
                throw new ArgumentNullException(nameof(grade), "Grade cannot be null.");

            if (!_grades.Remove(grade))
                throw new InvalidOperationException("This grade is not assigned to this teacher.");

            if (grade.Teacher == this)
            {
                grade.Teacher = Defaults.DEFAULT_TEACHER;
            }

        public void ResetTeacher()
        {
            RemoveTeacher(this);

            ArgumentNullException.ThrowIfNull(_subjects);
            foreach (var subject in _subjects)
            {
                subject.RemoveTeacher(this);
            }

            _subjects.Clear();
            _subjects.Add(Defaults.DEFAULT_SUBJECT);

        }

        private List<Student> _students = Student.GetStudentExtent();

        public void AddAssignmentToClass(IAssignment assignment, List<Student> students)
        {
            ArgumentNullException.ThrowIfNull(assignment);
            if (students == null || students.Count == 0)
                throw new ArgumentException("Student list cannot be empty.");

            if (_subjects.Any(subject => subject.Assignments.Contains(assignment)))
                throw new ArgumentException("Assignment already exists in the class.");

            foreach (var student in students)
            {
                if (!student.Assignments.Contains(assignment))
                {
                    student.Assignments.Add(assignment);
                }
            }

            Console.WriteLine($"Assignment '{assignment.Topic}' added to all students.");
            //TO DO: merge this with AddAssignment
        }

        public void EditAssignment(IAssignment assignment, string newTopic, DateTime? newDueDate)
        {
            ArgumentNullException.ThrowIfNull(assignment);

            assignment.Topic = newTopic ?? assignment.Topic;
            assignment.DueDate = newDueDate ?? assignment.DueDate;

            Console.WriteLine($"Assignment '{assignment.Topic}' edited.");
        }

        public void RemoveAssignment(IAssignment assignment, List<Student> students)
        {
            ArgumentNullException.ThrowIfNull(assignment);
            if (students == null || students.Count == 0)
                throw new ArgumentException("Student list cannot be empty.");

            foreach (var student in students)
            {
                student.Assignments.Remove(assignment);
            }

            Console.WriteLine($"Assignment '{assignment.Topic}' removed from all students.");
        }
    }
}