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
            if (value < DateTime.Now)
                throw new ArgumentException("Exam date cannot be in the past.");
            _examDate = value;
        }
    }

    public Exam(DateTime examDate)
    {
        ExamId = Interlocked.Increment(ref nextId);
        ExamDate = examDate;
        AddExam(this);
        SaveManager.SaveToJson(_examList, nameof(_examList));
    }

    public void ScheduleExam(DateTime newDate)
    {
        if (newDate < DateTime.Now)
            throw new ArgumentException("Exam date cannot be in the past.");
        ExamDate = newDate;
    }

    private static void AddExam(Exam exam)
    {
        if (exam == null)
            throw new ArgumentException($"{nameof(exam)} cannot be null.");

        if (_examList.Any(e => e.ExamId == exam.ExamId))
            throw new ArgumentException($"An exam with ID {exam.ExamId} already exists.");

        _examList.Add(exam);
    }
    
    public void AddTimeslot(Timeslot timeslot)
    {
        if (_timeslot != null)
            throw new InvalidOperationException("This exam already has an associated timeslot.");
        
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
            if (value != null && value.Exam != null && value.Exam != this)
                throw new InvalidOperationException("The timeslot is already assigned to another exam.");
            _timeslot = value;
            if (value != null && value.Exam != this)
                value.Exam = this; 
        }
    }

    private static readonly List<Exam> _examList = new();
}