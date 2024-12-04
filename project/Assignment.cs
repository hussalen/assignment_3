using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using assignment_3;

namespace assignment_3
{
    public interface IAssignment
    {
        public int AssignmentID { get; }
        public static int nextId; //probably we should seperate it for each class
        public string Topic { get; set; }

        public DateTime DueDate { get; set; }
        public DateTime? SubmissionDate { get; set; }

        public Student SubmittingStudent { get; set; }
    }
    
    
}

