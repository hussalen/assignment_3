namespace assignment_3;

public class ClassroomNotification
{
    public Classroom Classroom { get; private set; }
    public Timeslot Timeslot { get; private set; }
    public string NotifMessage { get; set; }
    public bool? Availability { get; set; }

    public ClassroomNotification(Classroom classroom, Timeslot timeslot)
    {
        Classroom = classroom;
        Timeslot = timeslot;
    }

    public void SendNotification()
    {
        if (Availability == true)
        {
            Console.WriteLine(
                $"Notification for Room {Classroom.RoomId} at Timeslot {Timeslot?.ScheduleId ?? 0}: {NotifMessage}"
            );
        }
        else
        {
            Console.WriteLine(
                $"Room {Classroom.RoomId} at Timeslot {Timeslot?.ScheduleId ?? 0} is unavailable or empty. Notification not sent."
            );
        }
    }
}
