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
        public string topic { get; set; }
        public DateTime dueDate { get; set; }
        public DateTime? submissionDate { get; set; }

        public int noOfPeople;
        public string documentation;
        public Student[] roles;


        public GroupProject(string topic, DateTime dueDate, int noOfPeople, string documentation, Student[] roles)
        {
            AssignmentID = nextId++;
            this.topic = topic;
            this.dueDate = dueDate;
            this.noOfPeople = noOfPeople;
            this.documentation = documentation;
            this.roles = roles;
            submissionDate = null;

        public GroupProject(int noOfPeople, string documentation)
        {
            this.noOfPeople = noOfPeople;
            this.documentation = documentation;
        }
    }

}