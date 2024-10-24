namespace assignment_3;

public class Schedule
{
    public int ScheduleId { get; set; }
    public DateTime Date { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public Subject SubjectName { get; set; }
    public Classroom RoomNumber { get; set; }

    public void UpdateTime(TimeSpan newStartTime, TimeSpan newEndTime)
    {
        this.StartTime = newStartTime;
        this.EndTime = newEndTime;
    }



}