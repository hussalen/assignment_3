namespace assignment_3;

public class Classroom
{
    private int _roomId;
    private int _capacity;
    private static List<Classroom> _classrooms_List = new();

    public static List<Classroom> GetClassroomExtent() => new List<Classroom>(_classrooms_List);

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

    private static void addClassroom(Classroom classroom)
    {
        if (classroom is null)
        {
            throw new ArgumentException($"{nameof(classroom)} cannot be null.");
        }
        _classrooms_List.Add(classroom);
    }

    public Classroom(int roomId, int capacity)
    {
        RoomId = roomId; 
        Capacity = capacity;
        addClassroom(this);
    }
}
