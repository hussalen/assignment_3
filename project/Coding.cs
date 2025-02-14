using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace assignment_3
{
    public class Coding : IAssignment
    {
        private static int nextId = 1;
        public int AssignmentID { get; private set; }

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

        public DateTime? SubmissionDate { get; set; }

        private string _language;
        public string Language
        {
            get => _language;
            set =>
                _language = !string.IsNullOrWhiteSpace(value)
                    ? value
                    : throw new ArgumentException("Language cannot be null or empty.");
        }

        private string _repositoryUrl;
        public string RepositoryUrl
        {
            get => _repositoryUrl;
            set =>
                _repositoryUrl = Uri.IsWellFormedUriString(value, UriKind.Absolute)
                    ? value
                    : throw new ArgumentException("RepositoryUrl must be a valid URL.");
        }

        private static List<Coding> _codingList = new();

        public Student SubmittingStudent { get; set; }

        public Coding(string topic, DateTime dueDate, string language, string repositoryUrl)
        {
            AssignmentID = Interlocked.Increment(ref nextId);
            Topic = topic;
            DueDate = dueDate;
            Language = language;
            RepositoryUrl = repositoryUrl;
            SubmissionDate = null;
            AddAssignment(this);
            //SaveManager.SaveToJson(_codingList, nameof(_codingList));
        }

        private static void AddAssignment(Coding coding)
        {
            if (coding is null)
            {
                throw new ArgumentException($"{nameof(coding)} cannot be null.");
            }
            _codingList.Add(coding);
        }
        public void EditAssignment(int assignmentId, string newTopic, DateTime newDueDate, string newLanguage, string newRepositoryUrl)
        {
            Coding coding = _codingList.FirstOrDefault(c => c.AssignmentID == assignmentId);
            if (ReferenceEquals(coding, Defaults.DEFAULT_CODING))
            {
                throw new ArgumentException("Default Assignment is only modifiable by Admin");
            }
            ArgumentNullException.ThrowIfNull(coding);
            if (coding.SubmissionDate.HasValue)
            {
                throw new InvalidOperationException("Cannot modify a submitted assignment.");
            }
            coding.Topic = newTopic;
            coding.DueDate = newDueDate;
            coding.Language = newLanguage;
            coding.RepositoryUrl = newRepositoryUrl;
        }
        
        public static void RemoveAssignment(int assignmentId)
        {
            Coding coding = _codingList.FirstOrDefault(c => c.AssignmentID == assignmentId);
            ArgumentNullException.ThrowIfNull(coding);
            if (coding == Defaults.DEFAULT_CODING)
            {
                throw new ArgumentException("Coding assignment is default assignment, nothing to remove");
            }
            _codingList.Remove(coding);
        }

        public static List<Coding> GetCodingExtent() => new(_codingList);

        public void Submit(Student student)
        {
            if (string.IsNullOrWhiteSpace(RepositoryUrl))
            {
                throw new ArgumentException("Repository URL must be provided before submission.");
            }
            SubmissionDate = DateTime.UtcNow;
            SubmittingStudent = student;
            Console.WriteLine("Coding assignment submitted successfully!");
        }

    }
}
