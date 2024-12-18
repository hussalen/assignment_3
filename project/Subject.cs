namespace assignment_3
{
    public class Subject
    {
        public string SubjectId { get; private set; }

        private string _subjectName;
        public string SubjectName
        {
            get => _subjectName;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Subject name cannot be null or empty.");
                _subjectName = value;
            }
        }

        private int _gradingScale;
        public int GradingScale
        {
            get => _gradingScale;
            private set
            {
                if (value < 1 || value > 5)
                    throw new ArgumentException("Grading scale must be between 1 and 5.");
                _gradingScale = value;
            }
        }

        private List<Teacher> _teachers = new();
        public List<Teacher> Teachers
        {
            get => new(_teachers);
            set
            {
                if (value == null || value.Count == 0)
                    throw new InvalidOperationException("A subject must be assigned to at least one teacher.");
                _teachers = value;
            }
        }

        private List<Student> _students = new();
        public List<Student> Students
        {
            get => new(_students);
            set
            {
                if (value == null)
                    throw new ArgumentException("Student list cannot be null.");
                _students = value;
            }
        }

        public Subject(string subjectId, string subjectName, int gradingScale)
        {
            if (string.IsNullOrWhiteSpace(subjectId))
                throw new ArgumentException("Subject ID cannot be null or empty.");

            SubjectId = subjectId;
            SubjectName = subjectName;
            GradingScale = gradingScale;
        }

        public void UpdateSubjectName(string newSubjectName)
        {
            if (string.IsNullOrWhiteSpace(newSubjectName))
                throw new ArgumentException("Subject name cannot be null or empty.");
            SubjectName = newSubjectName;
        }
    }
}
