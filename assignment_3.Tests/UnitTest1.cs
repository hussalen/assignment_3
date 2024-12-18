using System;
using System.Collections.Generic;
using System.Text.Json.Nodes;
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
            Assert.AreEqual(topic, individualProject.Topic);
            Assert.AreEqual(dueDate, individualProject.DueDate);
            Assert.IsNull(individualProject.SubmissionDate);
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
            Assert.AreEqual(topic, groupProject.Topic);
            Assert.AreEqual(dueDate, groupProject.DueDate);
            Assert.AreEqual(noOfPeople, groupProject.NoOfPeople);
            Assert.AreEqual(documentation, groupProject.Documentation);
            Assert.AreEqual(roles, groupProject.Roles);
            Assert.IsNull(groupProject.SubmissionDate);
        }

        [Test]
        public void PastDateTryOfScheduling()
        {
            var exam = new Exam(new DateTime(2024, 11, 11));
            DateTime pastDate = DateTime.Now.AddDays(-1);

            Assert.Throws<ArgumentException>(() => exam.ScheduleExam(pastDate));
        }

        [Test]
        public void TryOfSchedulingOnTheTakenDate()
        {
            var exam = new Exam(new DateTime(2024, 11, 11));
            var initialDate = DateTime.Now.AddDays(1);
            exam.ScheduleExam(initialDate);
            Assert.AreEqual(initialDate, exam.ExamDate);
        }

        [Test]
        public void Report_Content_SetAndGet_ShouldWorkCorrectly()
        {
            // Arrange
            var report = new Report([]);
            JsonArray content = new JsonArray();
            content.Add("Test content");

            // Act
            report.Content = content;

            // Assert
            Assert.IsNotNull(report.Content);
            Assert.AreEqual("Test content", report.Content[0].ToString());
        }
    }

    public class ExamTests
    {
        [Test]
        public void Exam_Creation_WithPastDate_ShouldThrowException()
        {
            DateTime pastDate = DateTime.Now.AddDays(-1);
            Assert.Throws<ArgumentException>(() => new Exam(pastDate));
        }

        [Test]
        public void ScheduleExam_WithFutureDate_ShouldUpdateExamDate()
        {
            var exam = new Exam(DateTime.Now.AddDays(2));
            DateTime newDate = DateTime.Now.AddDays(10);
            exam.ScheduleExam(newDate);
            Assert.AreEqual(newDate, exam.ExamDate);
        }

        [Test]
        public void ScheduleExam_WithPastDate_ShouldThrowException()
        {
            var exam = new Exam(DateTime.Now.AddDays(2));
            DateTime pastDate = DateTime.Now.AddDays(-1);
            Assert.Throws<ArgumentException>(() => exam.ScheduleExam(pastDate));
        }

        [Test]
        public void ScheduleExam_WithSameDate_ShouldUpdateExamDate()
        {
            var exam = new Exam(DateTime.Now.AddDays(2));
            DateTime sameDate = DateTime.Now.AddDays(2);
            exam.ScheduleExam(sameDate);
            Assert.AreEqual(sameDate, exam.ExamDate);
        }

        [TestFixture]
        public class ExamTimeslotManagementTests
        {
            [Test]
            public void Exam_ShouldAllowAssigningTimeslot()
            {
                // Arrange
                var exam = new Exam(DateTime.Now.AddDays(1)); // Exam scheduled for tomorrow
                var timeslot = new Timeslot(101, DateTime.Now.AddDays(1), TimeSpan.FromHours(9),
                    TimeSpan.FromHours(10));

                // Act
                exam.AddTimeslot(timeslot);

                // Assert
                Assert.AreEqual(timeslot, exam.Timeslot);
            }
            [Test]
            public void Exam_ShouldNotAllowAssigningMultipleTimeslots()
            {
                // Arrange
                var exam = new Exam(DateTime.Now.AddDays(1));
                var timeslot1 = new Timeslot(101, DateTime.Now.AddDays(1), TimeSpan.FromHours(9), TimeSpan.FromHours(10));
                var timeslot2 = new Timeslot(102, DateTime.Now.AddDays(1), TimeSpan.FromHours(10), TimeSpan.FromHours(11));

                // Act
                exam.AddTimeslot(timeslot1);

                // Assert
                Assert.Throws<InvalidOperationException>(() => exam.AddTimeslot(timeslot2), "An exam can only have one timeslot.");
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
                Assert.Throws<ArgumentException>(
                    () => new Timeslot(invalidScheduleId, date, startTime, endTime),
                    "Schedule ID cannot be empty"
                );
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
                Assert.Throws<ArgumentException>(
                    () => new Timeslot(scheduleId, date, startTime, endTime),
                    "End time cannot be earlier than start time"
                );
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
                Assert.Throws<ArgumentException>(
                    () => timeslot.UpdateTime(newStartTime, newEndTime),
                    "End time cannot be earlier than start time"
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

            [Test]
            public void Classroom_ShouldAllowAddingTimeslots()
            {
                // Arrange
                var classroom = new Classroom(1, 30);
                var timeslot1 = new Timeslot(101, DateTime.Now.AddDays(1), TimeSpan.FromHours(9),
                    TimeSpan.FromHours(10));
                var timeslot2 = new Timeslot(102, DateTime.Now.AddDays(1), TimeSpan.FromHours(11),
                    TimeSpan.FromHours(12));

                // Act
                classroom.AssignTimeslot(timeslot1);
                classroom.AssignTimeslot(timeslot2);

                // Assert
                var assignedTimeslots = classroom.GetAssignedTimeslots();
                CollectionAssert.Contains(assignedTimeslots, timeslot1);
                CollectionAssert.Contains(assignedTimeslots, timeslot2);
                Assert.AreEqual(2, assignedTimeslots.Count);
            }

            [Test]
            public void AssignTimeslot_ShouldNotAllowDuplicateAssignments()
            {
                // Arrange
                var classroom = new Classroom(1, 30);
                var timeslot = new Timeslot(101, DateTime.Now.AddDays(1), TimeSpan.FromHours(9),
                    TimeSpan.FromHours(10));
                classroom.AssignTimeslot(timeslot);

                // Act & Assert
                Assert.Throws<InvalidOperationException>(() => classroom.AssignTimeslot(timeslot));
            }

            [Test]
            public void AssignTimeslot_ShouldUpdateClassroomInTimeslot()
            {
                // Arrange
                var classroom = new Classroom(1, 30);
                var timeslot = new Timeslot(101, DateTime.Now.AddDays(1), TimeSpan.FromHours(9),
                    TimeSpan.FromHours(10));

                // Act
                classroom.AssignTimeslot(timeslot);

                // Assert
                Assert.AreEqual(classroom, timeslot.Classroom);
            }

            [Test]
            public void ChangeClassroom_ShouldThrowExceptionIfTimeslotIsNotAssignedToOldClassroom()
            {
                // Arrange
                var classroom1 = new Classroom(1, 30);
                var classroom2 = new Classroom(2, 40);
                var timeslot = new Timeslot(101, DateTime.Now.AddDays(1), TimeSpan.FromHours(9),
                    TimeSpan.FromHours(10));

                // Act & Assert
                Assert.Throws<InvalidOperationException>(() => classroom1.ChangeClassroom(timeslot, classroom2));
            }

            [Test]
            public void AssignTimeslot_ShouldFailIfTimeslotDatesOverlap()
            {
                // Arrange
                var classroom = new Classroom(1, 30);
                var date = DateTime.Today.AddDays(1); // Use DateTime.Today for consistent dates
                var timeslot1 = new Timeslot(101, date, TimeSpan.FromHours(9), TimeSpan.FromHours(10));
                var timeslot2 = new Timeslot(102, date, TimeSpan.FromHours(9).Add(TimeSpan.FromMinutes(30)),
                    TimeSpan.FromHours(10).Add(TimeSpan.FromMinutes(30)));

                classroom.AssignTimeslot(timeslot1);

                // Act & Assert
                Assert.Throws<InvalidOperationException>(() => classroom.AssignTimeslot(timeslot2));
            }


        }
    }
}
