using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace assignment_3
{
    public class IndividualProject : Assignment
    {
        public int AssignmentID { get; private set; }
        public static int nextId = 1;

        int Assignment.AssignmentID => throw new NotImplementedException();

        public string Topic { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? SubmissionDate { get; set; }
        public string description;
        public List<string> notes;

        public IndividualProject(string topic, DateTime dueDate)
        {
            AssignmentID = nextId++;
            this.Topic = topic;
            this.DueDate = dueDate;
            SubmissionDate = null;
            addIndividualProject(this);
            SaveManager.SaveToJson(_individualProject_List, nameof(_individualProject_List));
        }

        private static List<IndividualProject> _individualProject_List = new();

        private static void addIndividualProject(IndividualProject individualProject)
        {
            if (individualProject is null)
            {
                throw new ArgumentException($"{nameof(individualProject)} cannot be null.");
            }
            _individualProject_List.Add(individualProject);
        }

        public static List<IndividualProject> GetIndividualProjectExtent() =>
            new List<IndividualProject>(_individualProject_List);
    }
}
