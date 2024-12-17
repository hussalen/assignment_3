using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace assignment_3
{
    
    public interface IAssignment
    {
        int AssignmentID { get; }
        string Topic { get; set; }
        DateTime DueDate { get; set; }
        DateTime? SubmissionDate { get; set; }
        Student SubmittingStudent { get; set; }
    }

   
    public class IndividualProject : IAssignment
    {
        public int AssignmentID { get; private set; }
        public string Topic { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? SubmissionDate { get; set; }
        public Student SubmittingStudent { get; set; }

        private static int nextId = 1;

        public IndividualProject(string topic, DateTime dueDate)
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
            Console.WriteLine($"Individual Project {AssignmentID} submitted by {student.Name} on {submissionDate}.");
        }

        public void UpdateAssignment(string newTopic, DateTime newDueDate)
        {
            if (string.IsNullOrWhiteSpace(newTopic))
                throw new ArgumentException("Assignment topic cannot be null or empty.");

            if (newDueDate < DateTime.Now)
                throw new ArgumentException("Due date cannot be in the past.");

            Topic = newTopic;
            DueDate = newDueDate;
            Console.WriteLine($"Individual Project {AssignmentID} updated to new topic: {newTopic} with new due date: {newDueDate}");
        }
    }

    
    public class GroupProject : IAssignment
    {
        public int AssignmentID { get; private set; }
        public string Topic { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? SubmissionDate { get; set; }
        public Student SubmittingStudent { get; set; }

        private static int nextId = 1;

        public GroupProject(string topic, DateTime dueDate)
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
            Console.WriteLine($"Group Project {AssignmentID} submitted by {student.Name} on {submissionDate}.");
        }

        public void UpdateAssignment(string newTopic, DateTime newDueDate)
        {
            if (string.IsNullOrWhiteSpace(newTopic))
                throw new ArgumentException("Assignment topic cannot be null or empty.");

            if (newDueDate < DateTime.Now)
                throw new ArgumentException("Due date cannot be in the past.");

            Topic = newTopic;
            DueDate = newDueDate;
            Console.WriteLine($"Group Project {AssignmentID} updated to new topic: {newTopic} with new due date: {newDueDate}");
        }
    }

    
    public class Essay : IAssignment
    {
        public int AssignmentID { get; private set; }
        public string Topic { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? SubmissionDate { get; set; }
        public Student SubmittingStudent { get; set; }

        private static int nextId = 1;

        public Essay(string topic, DateTime dueDate)
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
            Console.WriteLine($"Essay {AssignmentID} submitted by {student.Name} on {submissionDate}.");
        }

        public void UpdateAssignment(string newTopic, DateTime newDueDate)
        {
            if (string.IsNullOrWhiteSpace(newTopic))
                throw new ArgumentException("Assignment topic cannot be null or empty.");

            if (newDueDate < DateTime.Now)
                throw new ArgumentException("Due date cannot be in the past.");

            Topic = newTopic;
            DueDate = newDueDate;
            Console.WriteLine($"Essay {AssignmentID} updated to new topic: {newTopic} with new due date: {newDueDate}");
        }
    }

    
    public class Coding : IAssignment
    {
        public int AssignmentID { get; private set; }
        public string Topic { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? SubmissionDate { get; set; }
        public Student SubmittingStudent { get; set; }

        private static int nextId = 1;

        public Coding(string topic, DateTime dueDate)
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
            Console.WriteLine($"Coding Project {AssignmentID} submitted by {student.Name} on {submissionDate}.");
        }

        public void UpdateAssignment(string newTopic, DateTime newDueDate)
        {
            if (string.IsNullOrWhiteSpace(newTopic))
                throw new ArgumentException("Assignment topic cannot be null or empty.");

            if (newDueDate < DateTime.Now)
                throw new ArgumentException("Due date cannot be in the past.");

            Topic = newTopic;
            DueDate = newDueDate;
            Console.WriteLine($"Coding Project {AssignmentID} updated to new topic: {newTopic} with new due date: {newDueDate}");
        }
    }
}
