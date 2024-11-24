namespace assignment_3;

public class Timeslot
{
    public int ScheduleId { get; private set; }
    public DateTime Date { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }

    public Timeslot(int ScheduleId, DateTime Date, TimeSpan StartTime, TimeSpan EndTime)
    {
        if (ScheduleId <= 0)
            throw new ArgumentException("Schedule ID cannot be empty");

        this.ScheduleId = ScheduleId;
        this.Date = Date;
        this.StartTime = StartTime;
        this.EndTime = EndTime;

        addTimeslot(this);
        SaveManager.SaveToJson(_timeslot_List, nameof(_timeslot_List));
    }

    private static List<Timeslot> _timeslot_List = new();

    private static void addTimeslot(Timeslot timeslot)
    {
        if (timeslot is null)
        {
            throw new ArgumentException($"{nameof(timeslot)} cannot be null.");
        }
        _timeslot_List.Add(timeslot);
    }

    public static List<Timeslot> GetTimeslotExtent() => new List<Timeslot>(_timeslot_List);

    public void UpdateTime(TimeSpan newStartTime, TimeSpan newEndTime)
    {
        this.StartTime = newStartTime;
        this.EndTime = newEndTime;
    }
}
