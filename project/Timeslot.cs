namespace assignment_3;


public class Timeslot
{
    private static List<Timeslot> _timeslots = new List<Timeslot>();

    private int ScheduleId;
    private DateTime Date;
    private TimeSpan StartTime;
    private TimeSpan EndTime;
    private static int maxNumberOfCredits = 100;
    
    public Timeslot(int ScheduleId, DateTime Date, TimeSpan StartTime, TimeSpan EndTime)
    { 
        if (ScheduleId<=0) throw new ArgumentException("Schedule ID cannot be empty");
        
        this.ScheduleId = ScheduleId;
        this.Date = Date;
        this.StartTime = StartTime;
        this.EndTime = EndTime;
        _timeslots.Add(this);
    }

    public static List<Timeslot> GetTimeslots() => new List<Timeslot>(_timeslots);

    public void UpdateTime(TimeSpan newStartTime, TimeSpan newEndTime)
    {
        this.StartTime = newStartTime;
        this.EndTime = newEndTime;
    }



}