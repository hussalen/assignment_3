namespace assignment_3;

public class Timeslot
{
    public int ScheduleId { get; private set; }
    public Classroom Classroom { get; private set; }
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
        if (date.Date < DateTime.Today)
            throw new ArgumentException("Date cannot be in the past.");
        
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
        if (timeslot == null)
        {
            throw new ArgumentNullException($"{nameof(timeslot)} cannot be null.");
        }

        if (_timeslotList.Any(t => t.ScheduleId == timeslot.ScheduleId) && timeslot.ScheduleId != Defaults.DEFAULT_TIMESLOT.ScheduleId)
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
        if (newEndTime <= newStartTime)
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
        Classroom = Defaults.DEFAULT_CLASSROOM;
    }

    private Exam _exam;
    public Exam Exam
    {
        get => _exam; 
        set
        {
            if (_exam == value) return;
            if (_exam == Defaults.DEFAULT_EXAM && value != Defaults.DEFAULT_EXAM)
            {
                _exam = value;
                value.Timeslot = this;
                return;
            }
            if (_exam != Defaults.DEFAULT_EXAM && value != Defaults.DEFAULT_EXAM)
            {
                throw new InvalidOperationException("Cannot overwrite an existing valid exam.");
            }
            
            if (value == Defaults.DEFAULT_EXAM)
            {
                _exam = Defaults.DEFAULT_EXAM;
            }
        }
    }
    public void AddExam(Exam exam)
    {
        if (exam.Timeslot == Defaults.DEFAULT_TIMESLOT)
        {
            exam.Timeslot = this;
        }
        if (_exam != Defaults.DEFAULT_EXAM && _exam != exam)
            throw new InvalidOperationException("This timeslot already has an associated exam.");
        if (exam.Timeslot != Defaults.DEFAULT_TIMESLOT)
        {
           exam.RemoveTimeslot();
        }
        Exam = exam;
        exam.Timeslot = this;
    }

    public void EditExam(Exam newExam)
    {
        ArgumentNullException.ThrowIfNull(newExam);
        if (newExam == _exam) return;
        RemoveExam();
        newExam.Timeslot = this; 
        Exam = newExam;
    }

    public void RemoveExam() {
        _exam = Defaults.DEFAULT_EXAM;
    }

    public void SetSubject(Subject subject)
    {
        if (_subject != null && _subject != subject)
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
    
    
}
