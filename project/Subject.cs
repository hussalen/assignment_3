namespace assignment_3;

public class Subject
{
    public string SubjectId { get; private set; }
    public string SubjectName { get; private set; }
    public int GradingScale { get; private set; }

    public Subject(string SubjectId, string SubjectName, int GradingScale)
    {
        if (string.IsNullOrEmpty(SubjectId))
            throw new ArgumentException("Subject ID cannot be empty");
        if (string.IsNullOrEmpty(SubjectName))
            throw new ArgumentException("Subject name cannot be empty");
        if (GradingScale < 1 || GradingScale > 5)
            throw new ArgumentException("Grading scale must be between 1 and 5");

        this.SubjectId = SubjectId;
        this.SubjectName = SubjectName;
        this.GradingScale = GradingScale;

        addSubject(this);
    }

    private static List<Subject> _subject_List = new();

    private static void addSubject(Subject subject)
    {
        if (subject is null)
        {
            throw new ArgumentException($"{nameof(subject)} cannot be null.");
        }
        _subject_List.Add(subject);
    }

    public static List<Subject> GetSubjectExtent() => new List<Subject>(_subject_List);

    public void UpdateSubjectName(string newSubjectName)
    {
        if (string.IsNullOrEmpty(newSubjectName))
            throw new ArgumentException("Subject name cannot be empty");
        SubjectName = newSubjectName;
    }
}
