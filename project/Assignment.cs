using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace assignment_3
{
    public interface IAssignment
    {
        public int AssignmentID { get; }
        public static int nextId; 
        public string Topic { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? SubmissionDate { get; set; }
        public Student SubmittingStudent { get; set; }
    }

    public class Assignment : IAssignment
    {
        public int AssignmentID { get; private set; }
        public string Topic { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? SubmissionDate { get; set; }
        public Student SubmittingStudent { get; set; }

        public Teacher AssignedTeacher { get; private set; }

        private static int nextId = 1;

        public Assignment(string topic, DateTime dueDate)
        {
            if (string.IsNullOrWhiteSpace(topic))
                throw new ArgumentException("Assignment topic cannot be null or empty.");

            AssignmentID = nextId++;
            Topic = topic;
            DueDate = dueDate;
        }

        public void SubmitAssignment(DateTime submissionDate, Student student)
        {
            if (submissionDate > DueDate)
                throw new ArgumentException("Submission date cannot be after the due date.");

            SubmissionDate = submissionDate;
            SubmittingStudent = student;
            Console.WriteLine($"Assignment {AssignmentID} submitted by {student.Name} on {submissionDate}.");
        }

        public void UpdateAssignment(string newTopic, DateTime newDueDate)
        {
            if (string.IsNullOrWhiteSpace(newTopic))
                throw new ArgumentException("Assignment topic cannot be null or empty.");

            if (newDueDate < DateTime.Now)
                throw new ArgumentException("Due date cannot be in the past.");

            Topic = newTopic;
            DueDate = newDueDate;
            Console.WriteLine($"Assignment {AssignmentID} updated to new topic: {newTopic} with new due date: {newDueDate}");
        }
        public void SetTeacher(Teacher teacher)
        {
            if (teacher == null)
                throw new ArgumentException("Teacher cannot be null.");

            if (AssignedTeacher != teacher)
            {
                AssignedTeacher = teacher;
                if (!teacher.Assignments.Contains(this))
                {
                    teacher.AddAssignment(this); 
                }
            }
        }
    }
}
