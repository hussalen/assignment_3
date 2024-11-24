using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace assignment_3
{
    public class GroupProject : Assignment
    {
        public int AssignmentID { get; private set; }
        public static int nextId = 1;

        int Assignment.AssignmentID => throw new NotImplementedException();
        public string Topic { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? SubmissionDate { get; set; }

        public int noOfPeople;
        public string documentation;
        public Student[] roles;

        public GroupProject(
            string topic,
            DateTime dueDate,
            int noOfPeople,
            string documentation,
            Student[] roles
        )
        {
            AssignmentID = nextId++;
            this.Topic = topic;
            this.DueDate = dueDate;
            this.noOfPeople = noOfPeople;
            this.documentation = documentation;
            this.roles = roles;
            SubmissionDate = null;
            addGroupProject(this);
            SaveManager.SaveToJson(_groupProject_List, nameof(_groupProject_List));
        }

        private static List<GroupProject> _groupProject_List = new();

        private static void addGroupProject(GroupProject groupProject)
        {
            if (groupProject is null)
            {
                throw new ArgumentException($"{nameof(groupProject)} cannot be null.");
            }
            _groupProject_List.Add(groupProject);
        }

        public static List<GroupProject> GetGroupProjectExtent() =>
            new List<GroupProject>(_groupProject_List);
    }
}
