namespace assignment_3;

public class Classroom
{
    public int RoomId { get; set; }
    public int RoomNumber { get; set; }
    public int Capacity { get; set; }

    public bool CheckAvailability()
    {
        return Capacity > 0;
        
    }
}