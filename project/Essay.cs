using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace assignment_3
{
    public class Essay : Assignment
    {
        public int AssignmentID => throw new NotImplementedException();
        public string topic { get; set; }
        public DateTime dueDate { get; set; }
        public DateTime? submissionDate { get; set; }

        public int wordCount;
        public int minWordCount = 500;
        public int maxWordCount;

        public Essay() { }
    }
}
