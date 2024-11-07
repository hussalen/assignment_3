using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using assignment_3;

namespace assignment_3
{
    public interface Assignment
    {
        public int AssignmentID { get; }
        public static int nextId;
        public string topic { get; set; }
        public DateTime dueDate { get; set; }
        public DateTime? submissionDate { get; set; }
    }
}
