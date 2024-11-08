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
        public string topic { get; set; }
        public DateTime dueDate { get; set; }
        public DateTime? submissionDate { get; set; }

        public IndividualProject(string topic, DateTime dueDate)
        {
            AssignmentID = nextId++;
            this.topic = topic;
            this.dueDate = dueDate;
            submissionDate = null;
        }
    }
}