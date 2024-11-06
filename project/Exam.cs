namespace assignment_3;

public class Exam
{
    private static List<Exam> exams = new List<Exam>();
        
    // Encapsulation
    public int ExamId { get; private set; }
    public DateTime ExamDate { get; private set; }

    public Exam(int examId, DateTime examDate)
    {
        if (examId <= 0) throw new ArgumentException("Exam ID must be a positive integer.");
        if (examDate < DateTime.Now) throw new ArgumentException("Exam date cannot be in the past.");
        ExamId = examId;
        ExamDate = examDate;

        exams.Add(this);
    }
    public void ScheduleExam(DateTime date)
    {
        if (date < DateTime.Now)
            throw new ArgumentException("Exam date cannot be in the past.");
        ExamDate = date;
    }
}
