using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace assignment_3
{
    public class GroupProject : Assignment
    {
        int Assignment.AssignmentID => throw new NotImplementedException();

        public string topic { get; set; }
        public DateTime dueDate { get; set; }
        public DateTime? submissionDate { get; set; }

        public int noOfPeople;
        public string documentation;
        public Student[] roles;

        public GroupProject(int noOfPeople, string documentation)
        {
            this.noOfPeople = noOfPeople;
            this.documentation = documentation;
        }
    }
}
