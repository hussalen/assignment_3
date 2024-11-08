namespace assignment_3;

public static class ClassExtents
{
    // Initialize lists for each class
    private static List<Classroom> _classrooms_List = new();
    private static List<Assignment> _assignments_List = new();
    private static List<Attendance> _attendance_List = new();
    private static List<Coding> _coding_List = new();
    private static List<Essay> _essay_List = new();
    private static List<Exam> _exam_List = new();
    private static List<Grade> _grades_List = new();
    private static List<GroupProject> _groupProjects_List = new();
    private static List<IndividualProject> _individualProjects_List = new();
    private static List<Parent> _parents_List = new();
    private static List<Report> _reports_List = new();
    private static List<Timeslot> _schedule_List = new();
    private static List<Student> _students_List = new();
    private static List<Subject> _subjects_List = new();
    private static List<TimeTable> _timeTables_List = new();
	private static List<Timeslot> _timeslots_List = new(); 

    // Classroom
    public static List<Classroom> GetClassroomExtent() => new List<Classroom>(_classrooms_List);

    public static void AddClassroom(Classroom classroom)
    {
        if (classroom != null)
        {
            _classrooms_List.Add(classroom);
        }
        else
        {
            throw new ArgumentException("Classroom cannot be null.");
        }
    }

    // Assignment
    public static List<Assignment> GetAssignmentExtent() => new List<Assignment>(_assignments_List);

    public static void AddAssignment(Assignment assignment)
    {
        if (assignment != null)
        {
            _assignments_List.Add(assignment);
        }
        else
        {
            throw new ArgumentException("Assignment cannot be null.");
        }
    }

    // Attendance
    public static List<Attendance> GetAttendanceExtent() => new List<Attendance>(_attendance_List);

    public static void AddAttendance(Attendance attendance)
    {
        if (attendance != null)
        {
            _attendance_List.Add(attendance);
        }
        else
        {
            throw new ArgumentException("Attendance cannot be null.");
        }
    }

    // Coding
    public static List<Coding> GetCodingExtent() => new List<Coding>(_coding_List);

    public static void AddCoding(Coding coding)
    {
        if (coding != null)
        {
            _coding_List.Add(coding);
        }
        else
        {
            throw new ArgumentException("Coding cannot be null.");
        }
    }

    // Essay
    public static List<Essay> GetEssayExtent() => new List<Essay>(_essay_List);

    public static void AddEssay(Essay essay)
    {
        if (essay != null)
        {
            _essay_List.Add(essay);
        }
        else
        {
            throw new ArgumentException("Essay cannot be null.");
        }
    }

    // Exam
    public static List<Exam> GetExamExtent() => new List<Exam>(_exam_List);

    public static void AddExam(Exam exam)
    {
        if (exam != null)
        {
            _exam_List.Add(exam);
        }
        else
        {
            throw new ArgumentException("Exam cannot be null.");
        }
    }

    // Grade
    public static List<Grade> GetGradeExtent() => new List<Grade>(_grades_List);

    public static void AddGrade(Grade grade)
    {
        if (grade != null)
        {
            _grades_List.Add(grade);
        }
        else
        {
            throw new ArgumentException("Grade cannot be null.");
        }
    }

    // GroupProject
    public static List<GroupProject> GetGroupProjectExtent() =>
        new List<GroupProject>(_groupProjects_List);

    public static void AddGroupProject(GroupProject groupProject)
    {
        if (groupProject != null)
        {
            _groupProjects_List.Add(groupProject);
        }
        else
        {
            throw new ArgumentException("GroupProject cannot be null.");
        }
    }

    // IndividualProject
    public static List<IndividualProject> GetIndividualProjectExtent() =>
        new List<IndividualProject>(_individualProjects_List);

    public static void AddIndividualProject(IndividualProject individualProject)
    {
        if (individualProject != null)
        {
            _individualProjects_List.Add(individualProject);
        }
        else
        {
            throw new ArgumentException("IndividualProject cannot be null.");
        }
    }

    // Parent
    public static List<Parent> GetParentExtent() => new List<Parent>(_parents_List);

    public static void AddParent(Parent parent)
    {
        if (parent != null)
        {
            _parents_List.Add(parent);
        }
        else
        {
            throw new ArgumentException("Parent cannot be null.");
        }
    }

    // Report
    public static List<Report> GetReportExtent() => new List<Report>(_reports_List);

    public static void AddReport(Report report)
    {
        if (report != null)
        {
            _reports_List.Add(report);
        }
        else
        {
            throw new ArgumentException("Report cannot be null.");
        }
    }

    // Student
    public static List<Student> GetStudentExtent() => new List<Student>(_students_List);

    public static void AddStudent(Student student)
    {
        if (student != null)
        {
            _students_List.Add(student);
        }
        else
        {
            throw new ArgumentException("Student cannot be null.");
        }
    }

    // Subject
    public static List<Subject> GetSubjectExtent() => new List<Subject>(_subjects_List);

    public static void AddSubject(Subject subject)
    {
        if (subject != null)
        {
            _subjects_List.Add(subject);
        }
        else
        {
            throw new ArgumentException("Subject cannot be null.");
        }
    }

    // TimeTable
    public static List<TimeTable> GetTimeTableExtent() => new List<TimeTable>(_timeTables_List);

    public static void AddTimeTable(TimeTable timeTable)
    {
        if (timeTable != null)
        {
            _timeTables_List.Add(timeTable);
        }
        else
        {
            throw new ArgumentException("TimeTable cannot be null.");
        }
    }
	//Timeslot
	public static List<Timeslot> GetTimeslotExtent() => new List<Timeslot>(_timeslots_List);
	public static void AddTimeslot(Timeslot timeslot)
    {
        if (timeslot != null)
        {
            _timeslots_List.Add(timeslot);
        }
        else
        {
            throw new ArgumentException("Timeslot cannot be null.");
        }
    }
}
