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

      
        public List<Timeslot> AssignedTimeslots { get; private set; } = new();

       
        public List<Subject> Subjects { get; private set; } = new();

       
        public List<Grade> AssignedGrades { get; private set; } = new();

        
        public List<Assignment> Assignments { get; private set; } = new();

        public Availability AvailabilityStatus { get; set; }

        public Teacher(List<Subject> subjects, string name)
        {
            Name = name;
            TeacherID = Interlocked.Increment(ref nextId);
            Subject = subjects;
            AvailabilityStatus = Availability.Available;
        }

        public void ViewSchedule()
        {
            Console.WriteLine($"Schedule for Teacher {Name}:");
            foreach (var timeslot in AssignedTimeslots)
            {
                Console.WriteLine($"- Schedule ID: {timeslot.ScheduleId}, Date: {timeslot.Date}");
            }
        }

        public void RequestScheduleChange(Timeslot timeslot, DateTime newDate, TimeSpan newStartTime, TimeSpan newEndTime)
        {
            if (!AssignedTimeslots.Contains(timeslot))
            {
                throw new ArgumentException("The timeslot is not assigned to this teacher.");
            }

            timeslot.Date = newDate;
            timeslot.UpdateTime(newStartTime, newEndTime);
            Console.WriteLine($"Teacher {Name} requested a change for Timeslot {timeslot.ScheduleId}.");
        }

        public void AssignTimeslot(Timeslot timeslot)
        {
            if (timeslot == null)
                throw new ArgumentException("Timeslot cannot be null.");

            if (!AssignedTimeslots.Contains(timeslot))
            {
                AssignedTimeslots.Add(timeslot);
                timeslot.SetTeacher(this); 
            }
        }

        public void AddSubject(Subject subject)
        {
            if (subject == null)
                throw new ArgumentException("Subject cannot be null.");

            if (!Subjects.Contains(subject))
            {
                Subjects.Add(subject);
                subject.SetTeacher(this); 
            }
        }

        public void AssignGrade(Grade grade)
        {
            if (grade == null)
                throw new ArgumentException("Grade cannot be null.");

            if (!AssignedGrades.Contains(grade))
            {
                AssignedGrades.Add(grade);
                grade.SetTeacher(this); 
            }
        }

        public void AddAssignment(Assignment assignment)
        {
            if (assignment == null)
                throw new ArgumentException("Assignment cannot be null.");

            if (!Assignments.Contains(assignment))
            {
                Assignments.Add(assignment);
                assignment.SetTeacher(this); 
            }
        }

        public void AssignRole()
        {
            
        }
    }
}
