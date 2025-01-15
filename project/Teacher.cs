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
        public int TeacherID { get; private set; }
        private List<Subject> _subjects = new();
        public List<Subject> Subjects
        {
            get => new(_subjects);
            private set => _subjects = value;
        }

        public Availability AvailabilityStatus { get; set; }

        private static List<Teacher> _teachersList = new();

        public static List<Teacher> GetTeachersExtent() => new List<Teacher>(_teachersList);

        public Teacher(string name, string email, string[] addressLines, string password)
            : base(name, email, addressLines, password)
        {
            TeacherID = Interlocked.Increment(ref nextId);
            AvailabilityStatus = Availability.Available;
            AddTeacher(this);
        }

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
            ArgumentNullException.ThrowIfNull(subject);
            if (_subjects.Contains(subject))
                throw new ArgumentException("Subject is already assigned to this teacher.");

            if (subject.Teacher != null && subject.Teacher != this)
                throw new InvalidOperationException(
                    "Subject is already assigned to another teacher."
                );

            _subjects.Add(subject);
            //FIX: subject.Teacher = this;
        }

        public void RemoveSubject(Subject subject)
        {
            ArgumentNullException.ThrowIfNull(subject);
            if (!_subjects.Contains(subject))
                return;
            _subjects.Remove(subject);
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

            //We need to clear timessots, assignments and students, but there's no reverse connection established for any of them.
        }
    }
}
