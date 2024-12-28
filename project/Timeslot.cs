namespace assignment_3;

public class Timeslot
{
    public int ScheduleId { get; private set; }
    public Classroom Classroom { get; private set; }
    public Teacher AssignedTeacher { get; private set; }
    private Subject _subject;
    private DateTime _date;
    private TimeSpan _startTime;
    private TimeSpan _endTime;
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
        //SaveManager.SaveToJson(_timeslotList, nameof(_timeslotList));
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

    internal void SetClassroom(Classroom classroom)
    {
        Classroom = classroom;
    }

    internal void ClearClassroom()
    {
        Classroom = null;
    }

    private Exam _exam;
    public Exam Exam
    {
        get => _exam;
        set
        {
            if (value != null && value.Timeslot != null && value.Timeslot != this)
                throw new InvalidOperationException(
                    "The exam is already assigned to another timeslot."
                );
            _exam = value;
            if (value != null && value.Timeslot != this)
                value.Timeslot = this;
        }
    }

    public void AddExam(Exam exam)
    {
        if (_exam != null)
            throw new InvalidOperationException("This timeslot already has an associated exam.");

        Exam = exam;
    }

    public void EditExam(Exam newExam)
    {
        RemoveExam();
        AddExam(newExam);
    }

    public void RemoveExam()
    {
        if (_exam != null)
        {
            var temp = _exam;
            _exam = null;
            temp.Timeslot = null;
        }
    }

    public void SetSubject(Subject subject)
    {
        if (_subject != null)
            throw new InvalidOperationException(
                "This timeslot is already assigned to another subject."
            );

        _subject = subject; //cannot be assigned to second subject before clearing the ref
    }

    public void ClearSubject()
    {
        _subject = null;
    }

    public Subject GetSubject() => _subject;

    public void AssignTeacher(Teacher teacher)
    {
        if (teacher == null)
            throw new ArgumentNullException(nameof(teacher), "Teacher cannot be null.");

        if (AssignedTeacher != null)
            throw new InvalidOperationException("A teacher is already assigned to this timeslot.");

        AssignedTeacher = teacher;
        teacher.AssignTimeslot(this);
    }

    public void RemoveTeacher()
    {
        if (AssignedTeacher == null)
            throw new InvalidOperationException("No teacher is assigned to this timeslot.");

        var temp = AssignedTeacher;
        AssignedTeacher = null;
        temp.RemoveTimeslot(this);
    }
}
