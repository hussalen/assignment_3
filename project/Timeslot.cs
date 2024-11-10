namespace assignment_3;


public class Timeslot
{
    public int ScheduleId { get; private set; }
    public DateTime Date { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    
    public Timeslot(int ScheduleId, DateTime Date, TimeSpan StartTime, TimeSpan EndTime)
    { 
        if (ScheduleId<=0) throw new ArgumentException("Schedule ID cannot be empty");
        
        this.ScheduleId = ScheduleId;
        this.Date = Date;
        this.StartTime = StartTime;
        this.EndTime = EndTime;
    }


    public void UpdateTime(TimeSpan newStartTime, TimeSpan newEndTime)
    {
        this.StartTime = newStartTime;
        this.EndTime = newEndTime;
    }



}