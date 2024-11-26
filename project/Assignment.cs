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
        public static int nextId;
        public string Topic { get; set; }

        public DateTime DueDate { get; set; }
        public DateTime? SubmissionDate { get; set; }
    }
}
