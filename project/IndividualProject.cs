using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace assignment_3
{
    public class IndividualProject : Assignment
    {
        int Assignment.AssignmentID => throw new NotImplementedException();

        public string topic { get; set; }
        public DateTime dueDate { get; set; }
        public DateTime? submissionDate { get; set; }

        public string description;
        public List<string> notes;
    }
}
