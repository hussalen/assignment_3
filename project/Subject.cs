using System.ComponentModel;

namespace assignment_3
{
    public class Subject
    {
        public int SubjectId { get; private set; }

        private string _subjectName;
        public Subject ParentSubject { get; private set; }
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
                    throw new InvalidOperationException(
                        "A subject must be assigned to at least one teacher."
                    );
                _teachers = value;
            }
        }

        private Subject _subSubject;
        private List<Subject> _subSubjects = new List<Subject>();

        private List<Timeslot> _timeslots = new List<Timeslot>();

        public Subject SubSubject
        {
            get => _subSubject;
            private set { _subSubject = value; }
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

        public Teacher Teacher => throw new NotImplementedException();
        private static int nextId = 1;

        public Subject(string subjectName, int gradingScale)
        {
            SubjectId = Interlocked.Increment(ref nextId);
            SubjectName = subjectName;
            GradingScale = gradingScale;
        }

        public void UpdateSubjectName(string newSubjectName)
        {
            if (string.IsNullOrWhiteSpace(newSubjectName))
                throw new ArgumentException("Subject name cannot be null or empty.");
            SubjectName = newSubjectName;
        }

        public void AddSubSubject(Subject subSubject)
        {
            if (subSubject == null)
                throw new ArgumentNullException(nameof(subSubject));
            if (subSubject == this)
                throw new InvalidOperationException("A subject cannot be a sub-subject of itself.");
            if (subSubject.ParentSubject != null && subSubject.ParentSubject != this)
                throw new InvalidOperationException(
                    "A sub-subject cannot have more than one parent."
                );
            if (_subSubjects.Contains(subSubject))
                throw new InvalidOperationException(
                    "This sub-subject is already added to the parent subject."
                );
            Subject currentParent = this;
            while (currentParent.ParentSubject != null)
            {
                if (currentParent.ParentSubject == subSubject)
                {
                    throw new InvalidOperationException("A circular reference cannot be created.");
                }
                currentParent = currentParent.ParentSubject;
            }
            _subSubjects.Add(subSubject);
            subSubject.SetParentSubject(this);
        }

        private void SetParentSubject(Subject parentSubject)
        {
            ParentSubject = parentSubject;
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
            ParentSubject = null;
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

        public List<Subject> GetSubSubjects()
        {
            return new List<Subject>(_subSubjects);
        }

        public List<Timeslot> GetTimeslots() => new List<Timeslot>(_timeslots);

        public void RemoveTeacher(Teacher teacher)
        {
            if (!_teachers.Contains(teacher) || teacher == Defaults.DEFAULT_TEACHER)
                return;
            teacher.RemoveSubject(this);
        }
    }
}
