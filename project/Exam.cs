namespace assignment_3;

public class Exam
{
    // Encapsulation
    public int ExamId { get; private set; }
    static int nextId;
    public DateTime ExamDate { get; private set; }

    public Exam(DateTime examDate)
    {
        if (examDate < DateTime.Now)
            throw new ArgumentException("Exam date cannot be in the past.");
        ExamId = Interlocked.Increment(ref nextId);
        ExamDate = examDate;
    }

    public void ScheduleExam(DateTime date)
    {
        if (date < DateTime.Now)
            throw new ArgumentException("Exam date cannot be in the past.");
        ExamDate = date;
        addExam(this);
        SaveManager.SaveToJson(_exam_List, nameof(_exam_List));
    }

    private static List<Exam> _exam_List = new();

    public static List<Exam> GetExamExtent() => new List<Exam>(_exam_List);

    private static void addExam(Exam exam)
    {
        if (exam is null)
        {
            throw new ArgumentException($"{nameof(exam)} cannot be null.");
        }
        _exam_List.Add(exam);
    }
}
