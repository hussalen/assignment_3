using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace assignment_3
{
    public class Essay : IAssignment
    {
        private static int nextId = 1;
        public int AssignmentID { get; private set; }

        private string _topic;
        public string Topic
        {
            get => _topic;
            set =>
                _topic =
                    !string.IsNullOrWhiteSpace(value) && value.Length is >= 3 and <= 100
                        ? value
                        : throw new ArgumentException(
                            "Topic must be between 3 and 100 characters and cannot be empty."
                        );
        }

        private DateTime _dueDate;
        public DateTime DueDate
        {
            get => _dueDate;
            set =>
                _dueDate =
                    value >= DateTime.Now
                        ? value
                        : throw new ArgumentException("DueDate must be in the future.");
        }

        public DateTime? SubmissionDate { get; set; }

        private uint _minWordCount;
        public uint MinWordCount
        {
            get => _minWordCount;
            init => _minWordCount = ValidateEssayWordCount(value, _maxWordCount);
        }

        private uint _maxWordCount;
        public uint MaxWordCount
        {
            get => _maxWordCount;
            init => _maxWordCount = ValidateEssayWordCount(_minWordCount, value);
        }
        private int _wordCount;
        public int WordCount
        {
            get => _wordCount;
            set
            {
                if (value < MinWordCount || value > MaxWordCount)
                {
                    throw new ValidationException(
                        $"Word count must be between {MinWordCount} and {MaxWordCount}.");
                }
                _wordCount = value;
            }
        }

        private static readonly List<Essay> _essayList = new();
        public Student SubmittingStudent { get; set; }

        public Essay(string topic, DateTime dueDate, uint minWordCount, uint maxWordCount)
        {
            AssignmentID = Interlocked.Increment(ref nextId);
            Topic = topic;
            DueDate = dueDate;
            MinWordCount = minWordCount;
            MaxWordCount = maxWordCount;
            AddEssay(this);
            SaveManager.SaveToJson(_essayList, nameof(_essayList));
        }

        private static void AddEssay(Essay essay)
        {
            if (essay == null)
            {
                throw new ArgumentException($"{nameof(essay)} cannot be null.");
            }
            _essayList.Add(essay);
        }

        public static List<Essay> GetEssayExtent() => new(_essayList);

        private static uint ValidateEssayWordCount(uint minWordCount, uint maxWordCount)
        {
            if (minWordCount > maxWordCount)
            {
                throw new ValidationException(
                    "Minimum word count cannot be greater than the maximum word count."
                );
            }
            return maxWordCount;
        }
        
        public void Submit(int wordCount)
        {
            WordCount = wordCount;  // using the setter for WordCount validation
            SubmissionDate = DateTime.Now;
            Console.WriteLine($"Essay submitted successfully with {wordCount} words.");
        }

        //from IAssignment
        public void Submit()
        {
            Console.WriteLine("Essay submitted successfully!");
        }
    }
}
