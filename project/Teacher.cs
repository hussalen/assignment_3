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
            get => new(_subjects);
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
                throw new InvalidOperationException(
                    "Subject is already assigned to another teacher."
                );

            _subjects.Add(subject);
            if (subject.Teacher != this)
            {
                subject.Teacher = this; 
            }
        }

        public void RemoveSubject(Subject subject)
        {
            ArgumentNullException.ThrowIfNull(subject);
            if (!_subjects.Contains(subject))
                throw new ArgumentException("Subject is not assigned to this teacher.");

            _subjects.Remove(subject);
            if (subject.Teacher == this)
            {
                subject.Teacher = null; 
            }
        }

        public void ViewSchedule()
        {
            Console.WriteLine($"Schedule for Teacher {Name}:");
            foreach (var subject in _subjects)
            {
                Console.WriteLine($"- {subject.SubjectName} (ID: {subject.SubjectId})");
            }
        }

        private readonly List<Timeslot> _assignedTimeslots = new();

        public List<Timeslot> AssignedTimeslots => new List<Timeslot>(_assignedTimeslots);

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
        private readonly List<Grade> _grades = new();

        public List<Grade> Grades => new List<Grade>(_grades);

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
                grade.Teacher = null; 
            }
        }
    }
}
