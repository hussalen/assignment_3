namespace assignment_3;

public class Timeslot
{
    public int ScheduleId { get; private set; }

    private DateTime _date;
    public DateTime Date
    {
        get => _date;
        set
        {
            if (value < DateTime.Today)
                throw new ArgumentException("Date cannot be in the past.");
            _date = value;
        }
    }

    private TimeSpan _startTime;
    public TimeSpan StartTime
    {
        get => _startTime;
        set
        {
            if (value < TimeSpan.Zero || value > TimeSpan.FromHours(24))
                throw new ArgumentException("Start time must be a valid time of the day.");
            if (_endTime != default && value >= _endTime)
                throw new ArgumentException("Start time must be earlier than the end time.");
            _startTime = value;
        }
    }

    private TimeSpan _endTime;
    public TimeSpan EndTime
    {
        get => _endTime;
        set
        {
            if (value <= _startTime)
                throw new ArgumentException("End time must be later than the start time.");
            if (value > TimeSpan.FromHours(24))
                throw new ArgumentException("End time must be within a valid time of the day.");
            _endTime = value;
        }
    }

    public Timeslot(int scheduleId, DateTime date, TimeSpan startTime, TimeSpan endTime)
    {
        if (scheduleId <= 0)
            throw new ArgumentException("Schedule ID must be a positive number.");

        ScheduleId = scheduleId;
        Date = date;
        StartTime = startTime;
        EndTime = endTime;
        AddTimeslot(this);
        SaveManager.SaveToJson(_timeslotList, nameof(_timeslotList));
    }

    private static List<Timeslot> _timeslotList = new();

    private static void AddTimeslot(Timeslot timeslot)
    {
        if (timeslot is null)
        {
            throw new ArgumentException($"{nameof(timeslot)} cannot be null.");
        }

        if (_timeslotList.Any(t => t.ScheduleId == timeslot.ScheduleId))
        {
            throw new ArgumentException(
                $"A timeslot with Schedule ID {timeslot.ScheduleId} already exists."
            );
        }

        _timeslotList.Add(timeslot);
    }

    public static List<Timeslot> GetTimeslotExtent() => new(_timeslotList);

    public void UpdateTime(TimeSpan newStartTime, TimeSpan newEndTime)
    {
        if (newStartTime >= newEndTime)
            throw new ArgumentException("New start time must be earlier than the new end time.");
        if (newStartTime < TimeSpan.Zero || newEndTime > TimeSpan.FromHours(24))
            throw new ArgumentException("Time must be within a valid range (00:00 to 24:00).");

        StartTime = newStartTime;
        EndTime = newEndTime;
    }
}
