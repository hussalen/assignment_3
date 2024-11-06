namespace assignment_3;

public class Subject
{ private static List<Subject> subjects = new List<Subject>();
    private string SubjectId;
    private string SubjectName;
    private int GradingScale;

    public Subject(string SubjectId, string SubjectName, int GradingScale)
    {
        if (string.IsNullOrEmpty(SubjectId)) throw new ArgumentException("Subject ID cannot be empty");
        if (string.IsNullOrEmpty(SubjectName)) throw new ArgumentException("Subject name cannot be empty");
        if (GradingScale < 1 || GradingScale > 5) throw new ArgumentException("Grading scale must be between 1 and 5");

        this.SubjectId = SubjectId;
        this.SubjectName = SubjectName;
        this.GradingScale = GradingScale;
        subjects.Add(this);
    }

    public void UpdateSubjectName(string newSubjectName)
    { if (string.IsNullOrEmpty(newSubjectName)) throw new ArgumentException("Subject name cannot be empty");
        SubjectName = newSubjectName;
    }
    
}

