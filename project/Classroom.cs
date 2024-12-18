namespace assignment_3;

public class Classroom
{
    private int _roomId;
    private int _capacity;
    private static List<Classroom> _classrooms_List = new();

    public static List<Classroom> GetClassroomExtent() => new List<Classroom>(_classrooms_List);
    private readonly List<Timeslot> _assignedTimeslots = new();

    public int RoomId
    {
        get => _roomId;
        private set
        {
            if (value <= 0)
                throw new ArgumentException("Room id cannot be less than 0");
            _roomId = value;
        }
    }
    public int Capacity
    {
        get => _capacity;
        set
        {
            if (value < 0)
                throw new ArgumentException("Capacity cannot be negative.");
            _capacity = value;
        }
    }

    public bool CheckAvailability()
    {
        return Capacity > 0;
    }
    public Classroom(int roomId, int capacity)
    {
        RoomId = roomId;
        Capacity = capacity;
        _assignedTimeslots = new List<Timeslot>
        {
            new Timeslot(0, DateTime.MinValue, TimeSpan.Zero, TimeSpan.Zero) // Dummy or placeholder Timeslot
        };

        addClassroom(this);
        SaveManager.SaveToJson(_classrooms_List, nameof(_classrooms_List));
    }
    private static void addClassroom(Classroom classroom)
    {
        if (classroom is null)
        {
            throw new ArgumentException($"{nameof(classroom)} cannot be null.");
        }
        _classrooms_List.Add(classroom);
    }

    public void AssignTimeslot(Timeslot timeslot)
    {
        if (timeslot == null)
            throw new ArgumentNullException(nameof(timeslot));

        if (_assignedTimeslots.Contains(timeslot))
            throw new InvalidOperationException("This timeslot is already assigned to the classroom");

        if (_assignedTimeslots.Any(t => t.Date == timeslot.Date &&
                                        t.StartTime < timeslot.EndTime &&
                                        timeslot.StartTime < t.EndTime))
        {
            throw new InvalidOperationException("Overlapping timeslots, cannot assign new timeslot to this classroom");
        }
        if (_assignedTimeslots.Count == 1 && _assignedTimeslots[0].ScheduleId == 0)
        {
            _assignedTimeslots.RemoveAt(0); // Remove the dummy
        }

        _assignedTimeslots.Add(timeslot);
        timeslot.SetClassroom(this);
    }

        
    public void RemoveTimeslot(Timeslot timeslot)
    {
        if (timeslot == null)
            throw new ArgumentNullException(nameof(timeslot));
        if (_assignedTimeslots.Remove(timeslot))
        {
            timeslot.ClearClassroom(); 
        }
    }

    public void ChangeClassroom(Timeslot timeslot, Classroom newClassroom)
    {   if (timeslot == null)
            throw new ArgumentNullException(nameof(timeslot), "Timeslot cannot be null."); 
        if (newClassroom == null)
            throw new ArgumentNullException(nameof(newClassroom), "New classroom cannot be null.");
        if (!_assignedTimeslots.Contains(timeslot))
            throw new InvalidOperationException("Timeslot is not assigned");
        RemoveTimeslot(timeslot);
        newClassroom.AssignTimeslot(timeslot);
        
    }
    public bool IsEmpty()
    {
        return _assignedTimeslots.Count == 1 && _assignedTimeslots[0].ScheduleId == 0;
    }
    public void CheckAndSendEmptyClassroomNotification()
    {
        if (IsEmpty())
        {
            var notification = new ClassroomNotification(this, null)
            {
                NotifMessage = "This classroom is empty and not scheduled for any timeslot.",
                Availability = false
            };
            notification.SendNotification();
        }
    }
    public List<Timeslot> GetAssignedTimeslots() => new List<Timeslot>(_assignedTimeslots);

}
