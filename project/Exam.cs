namespace assignment_3;

public class Exam
{
    public int ExamId { get; private set; }
    private static int nextId;

    private DateTime _examDate;
    public DateTime ExamDate
    {
        get => _examDate;
        private set
        {
            if (value < DateTime.Now)
                throw new ArgumentException("Exam date cannot be in the past.");
            _examDate = value;
        }
    }

    private static readonly List<Exam> _examList = new();

    public Exam(DateTime examDate)
    {
        ExamId = Interlocked.Increment(ref nextId);
        ExamDate = examDate; // Validates via setter
        AddExam(this);
    }

    public void ScheduleExam(DateTime newDate)
    {
        if (newDate < DateTime.Now)
            throw new ArgumentException("Exam date cannot be in the past.");

        ExamDate = newDate; // Validates via setter
    }

    private static void AddExam(Exam exam)
    {
        if (exam == null)
            throw new ArgumentException($"{nameof(exam)} cannot be null.");

        if (_examList.Any(e => e.ExamId == exam.ExamId))
            throw new ArgumentException($"An exam with ID {exam.ExamId} already exists.");

        _examList.Add(exam);
    }

    public static List<Exam> GetExamExtent() => new(_examList);
}
