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
    public void AddSubSubject(Subject subSubject)
    {
        if (subSubject == null)
            throw new ArgumentNullException(nameof(subSubject));

        if (subSubject == this)
            throw new InvalidOperationException("A subject cannot be a sub-subject of itself.");
        
        if (subSubject._parentSubject != null)
            throw new InvalidOperationException("A subject cannot have two parents.");

        if (!_subSubjects.Contains(subSubject))
        {
            _subSubjects.Add(subSubject);
            subSubject.SetParentSubject(this); 
        }
    }

    private void SetParentSubject(Subject parentSubject)
    {
        _parentSubject = parentSubject;
    }
    public void RemoveSubSubject(Subject subSubject)
    {
        if (subSubject == null)
            throw new ArgumentNullException(nameof(subSubject));

        if (_subSubjects.Contains(subSubject))
        {
            _subSubjects.Remove(subSubject);
            subSubject.RemoveParentSubject(); 
        }
    }

    private void RemoveParentSubject()
    {
        _parentSubject = null;
    }
    public void AddTimeslot(Timeslot timeslot)
    {
        if (timeslot == null)
            throw new ArgumentNullException(nameof(timeslot));
        
        if (timeslot.GetSubject() != null)
            throw new ArgumentException("A timeslot can only belong to one subject.");
        _timeslots.Add(timeslot);
        timeslot.SetSubject(this); 
    }

    // Method to remove a timeslot from this subject
    public void RemoveTimeslot(Timeslot timeslot)
    {
        if (timeslot == null)
            throw new ArgumentNullException(nameof(timeslot));

        if (_timeslots.Contains(timeslot))
        {
            _timeslots.Remove(timeslot);
            timeslot.ClearSubject();  // Remove the Subject from the Timeslot
        }
    }

    // Get all timeslots for this subject
    public List<Timeslot> GetTimeslots() => new List<Timeslot>(_timeslots);
}

