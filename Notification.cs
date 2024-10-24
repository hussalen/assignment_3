namespace assignment_3;

public class Notification
{
    public int NotificationId { get; set; }
    public string Message { get; set; }

    public void SendNotification(Classroom classroom)
    {
        if (classroom.Capacity == 0)
        {
            Console.WriteLine($"Notification sent: {Message}");
        }
    }
    
}