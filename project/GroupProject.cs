using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace assignment_3
{
    public class GroupProject : IAssignment
    {
        public int AssignmentID { get; private set; }
        private static int nextId = 1;

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

        public DateTime? SubmissionDate { get; set; }

        private int _noOfPeople;
        public int NoOfPeople
        {
            get => _noOfPeople;
            set =>
                _noOfPeople =
                    value > 0
                        ? value
                        : throw new ArgumentException("NoOfPeople must be greater than zero.");
        }

        public string Documentation { get; set; } = string.Empty;

        private Student[] _roles;
        public Student[] Roles
        {
            get => _roles;
            set =>
                _roles =
                    value != null && value.Length == NoOfPeople
                        ? value
                        : throw new ArgumentException(
                            $"Roles array must have exactly {NoOfPeople} entries."
                        );
        }

        private static readonly List<GroupProject> _groupProjectList = new();

        public GroupProject(
            string topic,
            DateTime dueDate,
            int noOfPeople,
            string documentation,
            Student[] roles
        )
        {
            AssignmentID = Interlocked.Increment(ref nextId);
            Topic = topic;
            DueDate = dueDate;
            NoOfPeople = noOfPeople;
            Documentation =
                documentation ?? throw new ArgumentException("Documentation cannot be null.");
            Roles = roles;
            SubmissionDate = null;
            AddGroupProject(this);
            SaveManager.SaveToJson(_groupProjectList, nameof(_groupProjectList));
        }

        private static void AddGroupProject(GroupProject groupProject)
        {
            if (groupProject == null)
            {
                throw new ArgumentException($"{nameof(groupProject)} cannot be null.");
            }
            _groupProjectList.Add(groupProject);
        }

        public static List<GroupProject> GetGroupProjectExtent() => new(_groupProjectList);
    }
}
