using project;

namespace assignment_3;

public static class ClassExtents
{
    private static List<Classroom> _classrooms_List = new();
    private static List<Exam> _exam_List = new();
    private static List<Grade> _grades_List = new();
    private static List<Subject> _subjects_List = new();
    private static List<Assignment> _assignments_List = new();

    public static List<Classroom> GetClassroomExtent()
    {
        return new List<Classroom>(_classrooms_List);
    }

    public static void AddClassroom(Classroom classroom)
    {
        if (classroom != null)
        {
            _classrooms_List.Add(classroom);
        }
        throw new ArgumentException("Classroom cannot be null.");
    }
}
