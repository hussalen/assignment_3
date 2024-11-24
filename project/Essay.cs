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
        public string Topic { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? SubmissionDate { get; set; }

        public int wordCount { get; private set; }

        private uint _minWordCount;
        public uint MinWordCount
        {
            get => _minWordCount;
            init { _minWordCount = ValidateEssayWordCount(value, MaxWordCount, value); }
        }
        private uint _maxWordCount;
        public uint MaxWordCount
        {
            get => _maxWordCount;
            private set { _maxWordCount = ValidateEssayWordCount(MinWordCount, value, value); }
        }

        private static List<Essay> _essay_List = new();

        public static List<Essay> GetEssayExtent() => new List<Essay>(_essay_List);

        private static void addEssay(Essay essay)
        {
            if (essay != null)
            {
                _essay_List.Add(essay);
            }
            else
            {
                throw new ArgumentException($"{nameof(essay)} cannot be null.");
            }
        }

        public Essay(uint minWordCount, uint maxWordCount)
        {
            this.MinWordCount = minWordCount;
            this.MaxWordCount = maxWordCount;
            addEssay(this);
            SaveManager.SaveToJson(_essay_List, nameof(_essay_List));
        }

        private uint ValidateEssayWordCount(uint minWC, uint maxWC, uint returnValue)
        {
            if (minWC > maxWC)
                throw new ValidationException("Minimum word count is greater than max word count.");
            return returnValue;
        }
    }
}
