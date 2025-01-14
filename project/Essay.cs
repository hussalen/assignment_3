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
                    value >= DateTime.UtcNow
                        ? value
                        : throw new ArgumentException("DueDate must be in the future.");
        }

        public DateTime? SubmissionDate { get; set; }

        private uint _minWordCount;
        public uint MinWordCount
        {
            get => _minWordCount;
            set => _minWordCount = value;
        }

        private uint _maxWordCount;
        public uint MaxWordCount
        {
            get => _maxWordCount;
            set =>
                _maxWordCount =
                    value > _minWordCount
                        ? value
                        : throw new ValidationException(
                            "Minimum word count cannot be greater than/equal to the maximum word count."
                        );
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
                        $"Word count must be between {MinWordCount} and {MaxWordCount}."
                    );
                }
                _wordCount = value;
            }
        }

        private static List<Essay> _essayList = new();
        public Student SubmittingStudent { get; set; }

        public Essay(string topic, DateTime dueDate, uint minWordCount, uint maxWordCount)
        {
            AssignmentID = Interlocked.Increment(ref nextId);
            Topic = topic;
            DueDate = dueDate;
            MinWordCount = minWordCount;
            MaxWordCount = maxWordCount;
            AddAssignment(this);
            //SaveManager.SaveToJson(_essayList, nameof(_essayList));
        }

        private static void AddAssignment(Essay essay)
        {
            if (essay == null)
            {
                throw new ArgumentException($"{nameof(essay)} cannot be null.");
            }
            _essayList.Add(essay);
        }
        public static void EditAssignment(int assignmentId, string newTopic, DateTime newDueDate, uint newMinWordCount, uint newMaxWordCount)
        {
            Essay essay = _essayList.FirstOrDefault(e => e.AssignmentID == assignmentId);
    
            if (ReferenceEquals(essay, Defaults.DEFAULT_ESSAY))
            {
                throw new ArgumentException("Default Assignment is only modifiable by Admin");
            }
            essay.Topic = newTopic;
            essay.DueDate = newDueDate;
            essay.MinWordCount = newMinWordCount;
            essay.MaxWordCount = newMaxWordCount;
        }
        public static void RemoveAssignment(int assignmentId)
        {
            Essay essay = _essayList.FirstOrDefault(e => e.AssignmentID == assignmentId);
            if (ReferenceEquals(essay, Defaults.DEFAULT_ESSAY))
            {
                throw new ArgumentException("Essay assignment is default assignment, nothing to remove");
            }
            _essayList.Remove(essay);
        }

        public static List<Essay> GetEssayExtent() => new(_essayList);
        
        public void Submit(Student student)
        {
            if (WordCount < MinWordCount || WordCount > MaxWordCount)
            {
                throw new ValidationException(
                    $"Word count must be between {MinWordCount} and {MaxWordCount}."
                );
            }
            SubmissionDate = DateTime.Now;
            SubmittingStudent = student;
            Console.WriteLine("Essay submitted successfully!");
        }
    }
}
