using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace assignment_3
{
    public class IndividualProject : IAssignment
    {
        public int AssignmentID { get; private set; }
        public static int nextId = 1;

        private string _topic;
        public string Topic
        {
            get => _topic;
            set =>
                _topic =
                    !string.IsNullOrWhiteSpace(value) && value.Length is >= 3 and <= 100
                        ? value
                        : throw new ArgumentException(
                            "Topic must be between 3 and 100 characters and cannot be empty."
                        );
        }

        private DateTime _dueDate;
        public DateTime DueDate
        {
            get => _dueDate;
            set =>
                _dueDate =
                    value >= DateTime.UtcNow
                        ? value
                        : throw new ArgumentException("DueDate must be in the future.");
        }

        private DateTime? _submissionDate;
        public DateTime? SubmissionDate
        {
            get => _submissionDate;
            set =>
                _submissionDate =
                    value == null || value <= DueDate
                        ? value
                        : throw new ArgumentException(
                            "SubmissionDate cannot be after the DueDate."
                        );
        }

        public string Description { get; set; }

        private List<string> _notes;
        public List<string> Notes
        {
            get => _notes;
            set =>
                _notes =
                    value.Count != 0 && !value.Any(string.IsNullOrWhiteSpace)
                        ? value
                        : throw new ArgumentNullException(
                            $"Cannot have empty notes, source: {nameof(value)}"
                        );
        }

        public IndividualProject(string topic, DateTime dueDate, List<string> notes, string description)
        {
            AssignmentID = Interlocked.Increment(ref nextId);
            Topic = topic;
            DueDate = dueDate;
            SubmissionDate = null;
            Notes = notes;
            Description = description;
            AddIndividualProject(this);
        }

        private static List<IndividualProject> _individualProject_List = new();

        private static void AddIndividualProject(IndividualProject individualProject)
        {
            if (individualProject is null)
                throw new ArgumentException($"{nameof(individualProject)} cannot be null.");

            _individualProject_List.Add(individualProject);
        }

        public static List<IndividualProject> GetIndividualProjectExtent() =>
            new(_individualProject_List);

        public Student SubmittingStudent { get; set; }


        public void Submit(DateTime submissionDate, Student student)
        {
            SubmissionDate = submissionDate;
            SubmittingStudent = student;
            Console.WriteLine($"IndividualProject '{Topic}' submitted by {student.Name}");
        }

        public string GetDetails()
        {
            return $"IndividualProject: {Topic}, Description: {Description}, Notes: {string.Join(", ", Notes)}";

        
        public void Submit(Student student)
        {
            throw new NotImplementedException();

        }
    }
}
