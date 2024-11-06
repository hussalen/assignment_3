namespace assignment_3;

public class Classroom
{
    private static List<Classroom> _classrooms = new List<Classroom>();

    private int _roomId;
    private int _capacity;

    public int RoomId
    {
        get => _roomId;
        private set
        {
            if(value<=0) throw new ArgumentException("Room id cannot be less than 0");
            _roomId = value;
        }
    }
    public int Capacity
    {
        get => _capacity; 
        set
        {
            if (value < 0) throw new ArgumentException("Capacity cannot be negative.");
            _capacity = value;
        }
    }
    public bool CheckAvailability()
    {
        return Capacity > 0;

    }
    public Classroom(int roomId, int capacity)
    {
        RoomId = roomId;  // invoke the setter with validation
        Capacity = capacity; 
        AddClassroom(this);  //instance add
        
    }

    public static List<Classroom> GetClassrooms()
    {
        return _classrooms;
    }
    private static void AddClassroom(Classroom classroom)
    {
        if (classroom == null) 
            throw new ArgumentException("Classroom cannot be null");
        _classrooms.Add(classroom);
    }
}