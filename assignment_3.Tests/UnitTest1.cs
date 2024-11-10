using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace assignment_3.Tests
{
    [TestFixture]
    public class AssignmentTests
    {
        [Test]
        public void IndividualProject_Creation_ShouldInitializeCorrectly()
        {
            // Arrange
            string topic = "Math Project";
            DateTime dueDate = new DateTime(2024, 12, 25);

            // Act
            var individualProject = new IndividualProject(topic, dueDate);

            // Assert
            Assert.AreEqual(1, individualProject.AssignmentID);
            Assert.AreEqual(topic, individualProject.topic);
            Assert.AreEqual(dueDate, individualProject.dueDate);
            Assert.IsNull(individualProject.submissionDate);
        }

        [Test]
        public void GroupProject_Creation_ShouldInitializeCorrectly()
        {
            // Arrange
            string topic = "Science Project";
            DateTime dueDate = new DateTime(2024, 11, 15);
            int noOfPeople = 3;
            string documentation = "Group project documentation";
            Student[] roles = new Student[3];

            // Act
            var groupProject = new GroupProject(topic, dueDate, noOfPeople, documentation, roles);

            // Assert
            Assert.AreEqual(1, groupProject.AssignmentID);
            Assert.AreEqual(topic, groupProject.topic);
            Assert.AreEqual(dueDate, groupProject.dueDate);
            Assert.AreEqual(noOfPeople, groupProject.noOfPeople);
            Assert.AreEqual(documentation, groupProject.documentation);
            Assert.AreEqual(roles, groupProject.roles);
            Assert.IsNull(groupProject.submissionDate);
        }

        [Test]
        public void PastDateTryOfScheduling()
        {
            var exam = new Exam();
            DateTime pastDate = DateTime.Now.AddDays(-1);

            Assert.Throws<ArgumentException>(() => exam.ScheduleExam(pastDate));
        }

        [Test]
        public void TryOfSchedulingOnTheTakenDate()
        {
            var exam = new Exam();
            var initialDate = DateTime.Now.AddDays(1);
            exam.ScheduleExam(initialDate);
            Assert.AreEqual(initialDate, exam.ExamDate);
        }

        [Test]
        public void NullInput()
        {
            var exam = new Exam();
            Assert.Throws<FormatException>(() => exam.ScheduleExam(null));
        }
        // Report class tests
        [Test]
        public void Report_Creation_ShouldInitializeCorrectly()
        {
            // Act
            var report = new Report();

            // Assert
            Assert.IsNotNull(report);
            Assert.AreEqual(1, report.ReportId);
            Assert.IsNull(report.content);
        }

        [Test]
        public void Report_Content_SetAndGet_ShouldWorkCorrectly()
        {
            // Arrange
            var report = new Report();
            JsonArray content = new JsonArray();
            content.Add("Test content");

            // Act
            report.content = content;

            // Assert
            Assert.IsNotNull(report.content);
            Assert.AreEqual("Test content", report.content[0].ToString());

    public class ExamTests
    {
        [Test]
        public void Exam_Creation_ShouldInitializeCorrectly()
        {
            int examId = 1;
            DateTime examDate = DateTime.Now.AddDays(5);
            var exam = new Exam(examId, examDate);
            Assert.AreEqual(examId, exam.ExamId);
            Assert.AreEqual(examDate, exam.ExamDate);
        }

        [Test]
        public void Exam_Creation_WithPastDate_ShouldThrowException()
        {
            int examId = 1;
            DateTime pastDate = DateTime.Now.AddDays(-1);
            Assert.Throws<ArgumentException>(() => new Exam(examId, pastDate));
        }

        [Test]
        public void Exam_Creation_WithInvalidExamId_ShouldThrowException()
        {
            int invalidExamId = -1;
            DateTime examDate = DateTime.Now.AddDays(5);
            Assert.Throws<ArgumentException>(() => new Exam(invalidExamId, examDate));
        }

        [Test]
        public void ScheduleExam_WithFutureDate_ShouldUpdateExamDate()
        {
            var exam = new Exam(1, DateTime.Now.AddDays(2));
            DateTime newDate = DateTime.Now.AddDays(10);
            exam.ScheduleExam(newDate);
            Assert.AreEqual(newDate, exam.ExamDate);
        }

        [Test]
        public void ScheduleExam_WithPastDate_ShouldThrowException()
        {
            var exam = new Exam(1, DateTime.Now.AddDays(2));
            DateTime pastDate = DateTime.Now.AddDays(-1);
            Assert.Throws<ArgumentException>(() => exam.ScheduleExam(pastDate));
        }

        [Test]
        public void ScheduleExam_WithSameDate_ShouldUpdateExamDate()
        {
            var exam = new Exam(1, DateTime.Now.AddDays(2));
            DateTime sameDate = DateTime.Now.AddDays(2);
            exam.ScheduleExam(sameDate);
            Assert.AreEqual(sameDate, exam.ExamDate);
        }
    }


        [TestFixture]
    public class TimeslotTests
    {
        [Test]
        public void Timeslot_Creation_ShouldInitializeCorrectly()
        {
            // Arrange
            int scheduleId = 1;
            DateTime date = DateTime.Now.AddDays(1);
            TimeSpan startTime = new TimeSpan(10, 0, 0); // 10:00 AM
            TimeSpan endTime = new TimeSpan(12, 0, 0); // 12:00 PM

            // Act
            var timeslot = new Timeslot(scheduleId, date, startTime, endTime);

            // Assert
            Assert.AreEqual(scheduleId, timeslot.ScheduleId);
            Assert.AreEqual(date, timeslot.Date);
            Assert.AreEqual(startTime, timeslot.StartTime);
            Assert.AreEqual(endTime, timeslot.EndTime);
        }

        [Test]
        public void Timeslot_Creation_WithInvalidScheduleId_ShouldThrowException()
        {
            // Arrange
            int invalidScheduleId = -1;
            DateTime date = DateTime.Now.AddDays(1);
            TimeSpan startTime = new TimeSpan(10, 0, 0); // 10:00 AM
            TimeSpan endTime = new TimeSpan(12, 0, 0); // 12:00 PM

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Timeslot(invalidScheduleId, date, startTime, endTime), 
                "Schedule ID cannot be empty");
        }

        [Test]
        public void Timeslot_Creation_WithEndTimeBeforeStartTime_ShouldThrowException()
        {
            // Arrange
            int scheduleId = 1;
            DateTime date = DateTime.Now.AddDays(1);
            TimeSpan startTime = new TimeSpan(14, 0, 0); // 2:00 PM
            TimeSpan endTime = new TimeSpan(12, 0, 0); // 12:00 PM (end time before start time)

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Timeslot(scheduleId, date, startTime, endTime), 
                "End time cannot be earlier than start time");
        }

        [Test]
        public void Timeslot_UpdateTime_ShouldUpdateStartTimeAndEndTimeCorrectly()
        {
            // Arrange
            int scheduleId = 1;
            DateTime date = DateTime.Now.AddDays(1);
            TimeSpan startTime = new TimeSpan(10, 0, 0); // 10:00 AM
            TimeSpan endTime = new TimeSpan(12, 0, 0); // 12:00 PM
            var timeslot = new Timeslot(scheduleId, date, startTime, endTime);

            TimeSpan newStartTime = new TimeSpan(11, 0, 0); // 11:00 AM
            TimeSpan newEndTime = new TimeSpan(13, 0, 0); // 1:00 PM

            // Act
            timeslot.UpdateTime(newStartTime, newEndTime);

            // Assert
            Assert.AreEqual(newStartTime, timeslot.StartTime);
            Assert.AreEqual(newEndTime, timeslot.EndTime);
        }

        [Test]
        public void Timeslot_UpdateTime_WithInvalidEndTime_ShouldThrowException()
        {
            // Arrange
            int scheduleId = 1;
            DateTime date = DateTime.Now.AddDays(1);
            TimeSpan startTime = new TimeSpan(10, 0, 0); // 10:00 AM
            TimeSpan endTime = new TimeSpan(12, 0, 0); // 12:00 PM
            var timeslot = new Timeslot(scheduleId, date, startTime, endTime);

            TimeSpan newStartTime = new TimeSpan(11, 0, 0); // 11:00 AM
            TimeSpan newEndTime = new TimeSpan(9, 0, 0); // 9:00 AM (end time before start time)

            // Act & Assert
            Assert.Throws<ArgumentException>(() => timeslot.UpdateTime(newStartTime, newEndTime), 
                "End time cannot be earlier than start time");
        }
    }

    [TestFixture]
    public class SubjectTests
    {
        [Test]
        public void Subject_Creation_ShouldInitializeCorrectly()
        {
            // Arrange
            string subjectId = "MATH101";
            string subjectName = "Mathematics";
            int gradingScale = 3;

            // Act
            var subject = new Subject(subjectId, subjectName, gradingScale);

            // Assert
            Assert.AreEqual(subjectId, subject.SubjectId);
            Assert.AreEqual(subjectName, subject.SubjectName);
            Assert.AreEqual(gradingScale, subject.GradingScale);
        }

        [Test]
        public void Subject_Creation_WithEmptySubjectId_ShouldThrowException()
        {
            // Arrange
            string subjectId = "";
            string subjectName = "Mathematics";
            int gradingScale = 3;

            // Act & Assert
            Assert.Throws<ArgumentException>(
                () => new Subject(subjectId, subjectName, gradingScale),
                "Subject ID cannot be empty"
            );
        }

        [Test]
        public void Subject_Creation_WithNullSubjectId_ShouldThrowException()
        {
            // Arrange
            string subjectId = null;
            string subjectName = "Mathematics";
            int gradingScale = 3;

            // Act & Assert
            Assert.Throws<ArgumentException>(
                () => new Subject(subjectId, subjectName, gradingScale),
                "Subject ID cannot be empty"
            );
        }

        [Test]
        public void Subject_Creation_WithEmptySubjectName_ShouldThrowException()
        {
            // Arrange
            string subjectId = "MATH101";
            string subjectName = "";
            int gradingScale = 3;

            // Act & Assert
            Assert.Throws<ArgumentException>(
                () => new Subject(subjectId, subjectName, gradingScale),
                "Subject name cannot be empty"
            );
        }

        [Test]
        public void Subject_Creation_WithNullSubjectName_ShouldThrowException()
        {
            // Arrange
            string subjectId = "MATH101";
            string subjectName = null;
            int gradingScale = 3;

            // Act & Assert
            Assert.Throws<ArgumentException>(
                () => new Subject(subjectId, subjectName, gradingScale),
                "Subject name cannot be empty"
            );
        }

        [Test]
        public void Subject_Creation_WithInvalidGradingScale_ShouldThrowException()
        {
            // Arrange
            string subjectId = "MATH101";
            string subjectName = "Mathematics";
            int invalidGradingScale = 6;

            // Act & Assert
            Assert.Throws<ArgumentException>(
                () => new Subject(subjectId, subjectName, invalidGradingScale),
                "Grading scale must be between 1 and 5"
            );
        }

        [Test]
        public void UpdateSubjectName_WithValidName_ShouldUpdateCorrectly()
        {
            // Arrange
            var subject = new Subject("MATH101", "Mathematics", 3);
            string newSubjectName = "Advanced Mathematics";

            // Act
            subject.UpdateSubjectName(newSubjectName);

            // Assert
            Assert.AreEqual(newSubjectName, subject.SubjectName);
        }

        [Test]
        public void UpdateSubjectName_WithEmptyName_ShouldThrowException()
        {
            // Arrange
            var subject = new Subject("MATH101", "Mathematics", 3);
            string newSubjectName = "";

            // Act & Assert
            Assert.Throws<ArgumentException>(
                () => subject.UpdateSubjectName(newSubjectName),
                "Subject name cannot be empty"
            );
        }

        [Test]
        public void UpdateSubjectName_WithNullName_ShouldThrowException()
        {
            // Arrange
            var subject = new Subject("MATH101", "Mathematics", 3);
            string newSubjectName = null;

            // Act & Assert
            Assert.Throws<ArgumentException>(
                () => subject.UpdateSubjectName(newSubjectName),
                "Subject name cannot be empty"
            );
        }
    }

    [TestFixture]
    public class ClassroomTests
    {
        [Test]
        public void Classroom_Creation_ShouldInitializeCorrectly()
        {
            // Arrange
            int roomId = 101;
            int capacity = 30;

            // Act
            var classroom = new Classroom(roomId, capacity);

            // Assert
            Assert.AreEqual(roomId, classroom.RoomId);
            Assert.AreEqual(capacity, classroom.Capacity);
        }

        [Test]
        public void Classroom_Creation_WithInvalidRoomId_ShouldThrowException()
        {
            // Arrange
            int invalidRoomId = -1;
            int capacity = 30;

            // Act & Assert
            Assert.Throws<ArgumentException>(
                () => new Classroom(invalidRoomId, capacity),
                "Room id cannot be less than 0"
            );
        }

        [Test]
        public void Classroom_Creation_WithInvalidCapacity_ShouldThrowException()
        {
            // Arrange
            int roomId = 101;
            int invalidCapacity = -10;

            // Act & Assert
            Assert.Throws<ArgumentException>(
                () => new Classroom(roomId, invalidCapacity),
                "Capacity cannot be negative."
            );
        }

        [Test]
        public void CheckAvailability_ShouldReturnTrue_WhenCapacityIsGreaterThanZero()
        {
            // Arrange
            var classroom = new Classroom(101, 25);

            // Act
            bool isAvailable = classroom.CheckAvailability();

            // Assert
            Assert.IsTrue(isAvailable);
        }

        [Test]
        public void CheckAvailability_ShouldReturnFalse_WhenCapacityIsZero()
        {
            // Arrange
            var classroom = new Classroom(101, 0);

            // Act
            bool isAvailable = classroom.CheckAvailability();

            // Assert
            Assert.IsFalse(isAvailable);
        }

        [Test]
        public void Capacity_SetWithValidValue_ShouldUpdateCapacity()
        {
            // Arrange
            var classroom = new Classroom(101, 20);
            int newCapacity = 35;

            // Act
            classroom.Capacity = newCapacity;

            // Assert
            Assert.AreEqual(newCapacity, classroom.Capacity);
        }

        [Test]
        public void Capacity_SetWithNegativeValue_ShouldThrowException()
        {
            // Arrange
            var classroom = new Classroom(101, 20);
            int invalidCapacity = -5;

            // Act & Assert
            Assert.Throws<ArgumentException>(
                () => classroom.Capacity = invalidCapacity,
                "Capacity cannot be negative."
            );
        }
    }
}
