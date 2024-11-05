using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace project
{
    public class Essay : Assignment
    {
        public int AssignmentID => throw new NotImplementedException();

        public string topic
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }
        public DateTime dueDate
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }
        public DateTime? submissionDate
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        public int wordCount;
        public int minWordCount = 500;
        public int maxWordCount;
    }
}
