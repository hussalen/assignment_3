using System;
using System.Collections.Generic;
using System.Linq;
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
                    value >= DateTime.Now
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

        public string description;
        public List<string> notes;

        public IndividualProject(string topic, DateTime dueDate)
        {
            AssignmentID = Interlocked.Increment(ref nextId);
            Topic = topic;
            DueDate = dueDate;
            SubmissionDate = null;
            AddIndividualProject(this);
            SaveManager.SaveToJson(_individualProject_List, nameof(_individualProject_List));
        }

        private static readonly List<IndividualProject> _individualProject_List = new();

        private static void AddIndividualProject(IndividualProject individualProject)
        {
            if (individualProject is null)
                throw new ArgumentException($"{nameof(individualProject)} cannot be null.");

            _individualProject_List.Add(individualProject);
        }

        public static List<IndividualProject> GetIndividualProjectExtent() =>
            new(_individualProject_List);

        public Student SubmittingStudent { get; set; }

        public void Submit()
        {
            SubmissionDate=DateTime.Now;
            Console.Write("Individual Project submitted");
            
        }
    }
}
