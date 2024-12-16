namespace assignment_3;

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
    //Reflex subject having subsubjects
    private Subject _parentSubject; //Parent ref
    private readonly List<Subject> _subSubjects = new();

    public Subject(string subjectId, string subjectName, int gradingScale)
    {
        if (string.IsNullOrWhiteSpace(subjectId))
            throw new ArgumentException("Subject ID cannot be null or empty.");

        SubjectId = subjectId;
        SubjectName = subjectName;
        GradingScale = gradingScale;
        AddSubject(this);
        SaveManager.SaveToJson(_subjectList, nameof(_subjectList));
    }

    private static readonly List<Subject> _subjectList = new();

    private static void AddSubject(Subject subject)
    {
        if (subject is null)
        {
            throw new ArgumentException($"{nameof(subject)} cannot be null.");
        }

        if (_subjectList.Any(s => s.SubjectId == subject.SubjectId))
        {
            throw new ArgumentException($"A subject with ID {subject.SubjectId} already exists.");
        }

        _subjectList.Add(subject);
    }

    public static List<Subject> GetSubjectExtent() => new List<Subject>(_subjectList);

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

}
