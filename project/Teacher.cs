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

    public class Teacher
    {
        public string Name { get; set; }
        private static int nextId = 1;
        public int TeacherID { get; private set; }
        private List<Subject> _subjects = new();
        public List<Subject> Subjects
        {
            get => new(_subjects); // Encapsulation: Read-only access
            private set => _subjects = value;
        }

        public Availability AvailabilityStatus { get; set; }

        public Teacher(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be null or empty.");

            Name = name;
            TeacherID = Interlocked.Increment(ref nextId);
            AvailabilityStatus = Availability.Available;
        }

        public void AssignSubject(Subject subject)
        {
            ArgumentNullException.ThrowIfNull(subject);
            if (_subjects.Contains(subject))
                throw new ArgumentException("Subject is already assigned to this teacher.");

            if (subject.Teacher != null && subject.Teacher != this)
                throw new InvalidOperationException("Subject is already assigned to another teacher.");

            _subjects.Add(subject);
            subject.Teacher = this;
        }

        public void RemoveSubject(Subject subject)
        {
            ArgumentNullException.ThrowIfNull(subject);
            if (!_subjects.Contains(subject))
                throw new ArgumentException("Subject is not assigned to this teacher.");

            _subjects.Remove(subject);
            subject.Teacher = null;
        }

        public void ViewSchedule()
        {
            Console.WriteLine($"Schedule for Teacher {Name}:");
            foreach (var subject in _subjects)
            {
                Console.WriteLine($"- {subject.SubjectName} (ID: {subject.SubjectId})");
            }
        }
    }
}
