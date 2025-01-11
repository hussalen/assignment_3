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
            private set
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
            private set
            {
                if (value == null)
                    throw new ArgumentException("Student list cannot be null.");
                _students = value;
            }
        }

        private List<Subject> _subSubjects = new();
        private List<Timeslot> _timeslots = new();

        private Teacher _teacher;
        public Teacher Teacher
        {
            get => _teacher;
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(value), "Teacher cannot be null.");

                if (_teacher != null && _teacher != value)
                    throw new InvalidOperationException("This subject is already assigned to a different teacher.");

                _teacher = value;

                if (!_teacher.Subjects.Contains(this))
                {
                    _teacher.AssignSubject(this);
                }
            }
        }

        public Subject SubSubject { get; private set; }

        public Subject(string subjectId, string subjectName, int gradingScale)
        {
            if (string.IsNullOrWhiteSpace(subjectId))
                throw new ArgumentException("Subject ID cannot be null or empty.");

            SubjectId = subjectId;
            SubjectName = subjectName;
            GradingScale = gradingScale;
        }

        public void AddSubSubject(Subject subSubject)
        {
            if (subSubject == null)
                throw new ArgumentNullException(nameof(subSubject));

            if (subSubject == this)
                throw new InvalidOperationException("A subject cannot be a sub-subject of itself.");

            if (subSubject.SubSubject != null)
                throw new InvalidOperationException("A subject cannot have two parents.");

            if (!_subSubjects.Contains(subSubject))
            {
                _subSubjects.Add(subSubject);
                subSubject.SetParentSubject(this);
            }
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

        public void AddTimeslot(Timeslot timeslot)
        {
            if (timeslot == null)
                throw new ArgumentNullException(nameof(timeslot));

            if (timeslot.GetSubject() != null)
                throw new ArgumentException("A timeslot can only belong to one subject.");
            _timeslots.Add(timeslot);
            timeslot.SetSubject(this);
        }

        public void RemoveTimeslot(Timeslot timeslot)
        {
            if (timeslot == null)
                throw new ArgumentNullException(nameof(timeslot));

            if (_timeslots.Contains(timeslot))
            {
                _timeslots.Remove(timeslot);
                timeslot.ClearSubject();
            }
        }

        public List<Timeslot> GetTimeslots() => new List<Timeslot>(_timeslots);

        private void SetParentSubject(Subject parentSubject)
        {
            SubSubject = parentSubject;
        }

        private void RemoveParentSubject()
        {
            SubSubject = null;
        }
    }
}
