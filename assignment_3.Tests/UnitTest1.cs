namespace assignment_3.Tests;

public class Tests
{
    public void PastDateTryOfScheduling()
    {
        var exam = new Exam();
        DateTime pastDate = DateTime.Now.AddDays(-1);

        Assert.Throws<ArgumentException>(() => exam.ScheduleExam(pastDate));
    }

    public void TryOfSchedulingOnTheTakenDate()
        {   var exam = new Exam(); 
            var initialDate = DateTime.Now.AddDays(1);
              exam.ScheduleExam(initialDate);
            Assert.Equal(initialDate, exam.ExamDate);   
            
}   public void CorrectFormat()
    {
        var exam = new Exam();
        exam.ScheduleExam("2024-10-24");
        Assert.Equal(new DateTime(2024, 10, 24), exam.ExamDate);
    }
    public void NullInput()
    {
        var exam = new Exam();
        Assert.Throws<FormatException>(() => exam.ScheduleExam(null)); 
    }