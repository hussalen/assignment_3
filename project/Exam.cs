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
    }
}
