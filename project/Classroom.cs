namespace assignment_3;

public class Classroom
{

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
    }


}