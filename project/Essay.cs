using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace assignment_3
{
    public class Essay : Assignment
    {
        public int AssignmentID => throw new NotImplementedException();
        public string? topic { get; set; }
        public DateTime dueDate { get; set; }
        public DateTime? submissionDate { get; set; }

        public int wordCount { get; private set; }
        public uint minWordCount
        {
            get => minWordCount;
            init
            {
                minWordCount = ValidateEssayWordCount(value, maxWordCount, value);
                ;
            }
        }
        public uint maxWordCount
        {
            get => maxWordCount;
            private set
            {
                maxWordCount = ValidateEssayWordCount(minWordCount, value, value);
                ;
            }
        }

        public Essay(uint minWordCount, uint maxWordCount)
        {
            this.minWordCount = minWordCount;
            this.maxWordCount = maxWordCount;
        }

        private uint ValidateEssayWordCount(uint minWC, uint maxWC, uint returnValue)
        {
            if (minWC > maxWC)
                throw new ValidationException("Minimum word count is greater than max word count.");
            return returnValue;
        }
    }
}
