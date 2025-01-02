namespace assignment_3;

public class Exam
{
    public int ExamId { get; private set; }
    private static int nextId = 1;

    private DateTime _examDate;
    public DateTime ExamDate
    {
        get => _examDate;
        private set
        {
            if (value < DateTime.Now.Date)
                throw new ArgumentException("Exam date cannot be in the past.");
            _examDate = value;
        }
    }

    public Exam(DateTime examDate, Timeslot timeslot)
    {
        ExamId = Interlocked.Increment(ref nextId);
        ExamDate = examDate;
        Timeslot = timeslot ?? Defaults.DEFAULT_TIMESLOT;
        //SaveManager.SaveToJson(_examList, nameof(_examList));
    }

    public void ScheduleExam(DateTime newDate, Timeslot timeslot)
    {
        if (newDate < DateTime.Now)
            throw new ArgumentException("Exam date cannot be in the past.");
        if (timeslot == null)
            throw new ArgumentNullException(nameof(timeslot), "Timeslot cannot be null.");
        if (timeslot.Exam != null && timeslot.Exam != this)
            throw new InvalidOperationException("This timeslot is already occupied by another exam.");
        
        if (_timeslot != Defaults.DEFAULT_TIMESLOT)
        {
            throw new InvalidOperationException(
                "Exam already has a valid timeslot. Use EditTimeslot to make changes."
            );
        }
        ExamDate = newDate;
        Timeslot = timeslot;
        timeslot.Exam = this; // Set reverse relationship
    }


    private static void AddExam(Exam exam)
    {
        if (exam == null)
            throw new ArgumentNullException($"{nameof(exam)} cannot be null.");

        if (_examList.Any(e => e.ExamId == exam.ExamId))
            throw new ArgumentException($"An exam with ID {exam.ExamId} already exists.");

        _examList.Add(exam);
    }

    public void AddTimeslot(Timeslot timeslot)
    {   
        if (timeslot == null) throw new ArgumentNullException(nameof(timeslot), "Timeslot cannot be null.");
        
        if (_timeslot != null && _timeslot != Defaults.DEFAULT_TIMESLOT && _timeslot != timeslot)
        {
            throw new InvalidOperationException("This exam already has an associated timeslot.");
        }
        _timeslot = timeslot;
        if (timeslot.Exam != null && timeslot.Exam != this)
        {
            timeslot.Exam = this; 
        }
        Timeslot = timeslot;
    }

    public void EditTimeslot(Timeslot newTimeslot)
    {
        RemoveTimeslot();
        AddTimeslot(newTimeslot);
    }

    public void RemoveTimeslot()
    {
        if (_timeslot != null)
        {
            var temp = _timeslot;
            _timeslot = null;
            temp.Exam = null; // Clear reverse connection
        }
    }

    public static List<Exam> GetExamExtent() => new(_examList);

    private Timeslot _timeslot;
    public Timeslot Timeslot
    {
        get => _timeslot;
        set
        {
            if (_timeslot == value) return;
            _timeslot?.RemoveExam();
            _timeslot = value;
            if (value != null && value.Exam != this)
            {
                value.Exam = this;
            }
        }
    }

    private static readonly List<Exam> _examList = new();
}
