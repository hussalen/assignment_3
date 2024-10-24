namespace assignment_3;

public class Subject
{
    public int SubjectId { get; set; }
    public string SubjectName { get; set; }
    public int[] GradingScale { get; set; } = new int[] {1,2,3,4,5};

    public void UpdateSubjectName(string newSubjectName)
    {
        SubjectName = newSubjectName;
    }
    
}