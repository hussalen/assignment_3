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
            //FIX: subject.Teacher = this;
        }

        public void RemoveSubject(Subject subject)
        {
            ArgumentNullException.ThrowIfNull(subject);
            if (!_subjects.Contains(subject))
                throw new ArgumentException("Subject is not assigned to this teacher.");

            _subjects.Remove(subject);
            //FIX: subject.Teacher = null;
        }

        public void ViewSchedule()
        {
            Console.WriteLine($"Schedule for Teacher {Name}:");
            foreach (var subject in _subjects)
            {
                Console.WriteLine($"- {subject.SubjectName} (ID: {subject.SubjectId})");
            }
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

