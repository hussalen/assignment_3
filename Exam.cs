namespace assignment_3;

public class Exam
{
    public int ExamId { get; set;}
    public DateTime ExamDate { get; set;}
    public Subject Subject { get; set;}

    public void ScheduleExam(DateTime date)
    {
        this.ExamDate = date;
    }
}