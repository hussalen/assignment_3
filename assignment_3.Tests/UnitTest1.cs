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
            var individualProject = new IndividualProject(topic, dueDate, ["test"]);

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
    }

    public class ExamTests
    {
        [Test]
        public void PastDateTryOfScheduling()
        {
            var pastDate = new DateTime(2024, 11, 11); 
            
            Assert.Throws<ArgumentException>(() => new Exam(pastDate));
        }

        [Test]
        public void TryOfSchedulingOnTheTakenDate()
        {
            var exam = new Exam(new DateTime(2025, 03, 15));
            var initialDate = DateTime.Today.AddDays(1); // Use DateTime.Today for consistency
    
            exam.ScheduleExam(initialDate); // Schedule for the next day
    
            Assert.AreEqual(initialDate, exam.ExamDate); // No .Date needed since ExamDate is already adjusted
        }



        [Test]
        public void Exam_Creation_WithFutureDate_ShouldNotThrowException()
        {
            DateTime futureDate = DateTime.Now.AddDays(1); // Valid future date
            var exam = new Exam(futureDate);
            Assert.AreEqual(futureDate, exam.ExamDate);
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
        [Test]
        public void ScheduleExam_WithFarFutureDate_ShouldUpdateExamDate()
        {
            var exam = new Exam(DateTime.Now.AddDays(2));
            DateTime farFutureDate = DateTime.Now.AddYears(1); // Schedule exam for 1 year later
            exam.ScheduleExam(farFutureDate);
            Assert.AreEqual(farFutureDate, exam.ExamDate);
        }
        [Test]
        public void Exam_Creation_WithInvalidDate_ShouldThrowException()
        {
            // Arrange
            DateTime invalidDate = DateTime.Now.AddDays(-5); // Invalid past date
        
            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Exam(invalidDate), "Exam date cannot be in the past.");
        }
        [Test]
        public void ScheduleExam_WithSameExamDate_ShouldNotThrowException()
        {
            // Arrange
            var exam = new Exam(DateTime.Now.AddDays(1));
            DateTime sameDate = DateTime.Now.AddDays(1);

            // Act & Assert: Scheduling the same date should not throw an exception
            Assert.DoesNotThrow(() => exam.ScheduleExam(sameDate));
        }
        
        [TestFixture]
        public class ExamTimeslotManagementTests
        {
            [Test]
            public void Exam_ShouldAllowAssigningTimeslot()
            {
                // Arrange
                var exam = new Exam(DateTime.Now.AddDays(1)); // Exam scheduled for tomorrow
                var timeslot = new Timeslot(
                    101,
                    DateTime.Now.AddDays(1),
                    TimeSpan.FromHours(9),
                    TimeSpan.FromHours(10)
                );

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
                var timeslot1 = new Timeslot(
                    101,
                    DateTime.Now.AddDays(1),
                    TimeSpan.FromHours(9),
                    TimeSpan.FromHours(10)
                );
                var timeslot2 = new Timeslot(
                    102,
                    DateTime.Now.AddDays(1),
                    TimeSpan.FromHours(10),
                    TimeSpan.FromHours(11)
                );

                // Act
                exam.AddTimeslot(timeslot1);

                // Assert
                Assert.Throws<InvalidOperationException>(
                    () => exam.AddTimeslot(timeslot2),
                    "An exam can only have one timeslot."
                );
            }
            [Test]
            public void Exam_ShouldAllowTimeslotRemoval()
            {
                // Arrange
                var exam = new Exam(DateTime.Now.AddDays(1));
                var timeslot = new Timeslot(101, DateTime.Now.AddDays(1), TimeSpan.FromHours(9), TimeSpan.FromHours(10));

                // Act
                exam.AddTimeslot(timeslot);
                exam.RemoveTimeslot();  // Remove the timeslot

                // Assert: The exam should no longer have a timeslot
                Assert.IsNull(exam.Timeslot, "The exam should not have an associated timeslot after removal.");
                Assert.IsNull(timeslot.Exam, "The timeslot should not have an associated exam after removal.");
            }
            [Test]
            public void Exam_ShouldThrowException_WhenSchedulingInPast()
            {
                // Arrange
                var pastDate = DateTime.Now.AddDays(-1);  // Setting a past date
    
                // Act & Assert: Creating an exam with a past date should throw an ArgumentException
                Assert.Throws<ArgumentException>(() => new Exam(pastDate), "Exam date cannot be in the past.");
            }

            [Test]
            public void Exam_ShouldNotAllowAddingTimeslot_WhenTimeslotIsAssignedToAnotherExam()
            {
                // Arrange
                var exam1 = new Exam(DateTime.Now.AddDays(1));
                var exam2 = new Exam(DateTime.Now.AddDays(2));
                var timeslot = new Timeslot(101, DateTime.Now.AddDays(1), TimeSpan.FromHours(9), TimeSpan.FromHours(10));

                // Act
                exam1.AddTimeslot(timeslot);  // First exam gets the timeslot

                // Assert: Trying to assign the same timeslot to a second exam should throw an exception
                Assert.Throws<InvalidOperationException>(() => exam2.AddTimeslot(timeslot), "The timeslot is already assigned to another exam.");
            }
            [Test]
            public void Exam_ShouldAllowReassigningTimeslot_AfterRemoval()
            {
                // Arrange
                var exam1 = new Exam(DateTime.Now.AddDays(1));  // Exam scheduled for tomorrow
                var exam2 = new Exam(DateTime.Now.AddDays(2));  // Another exam scheduled for the next day
                var timeslot = new Timeslot(101, DateTime.Now.AddDays(1), TimeSpan.FromHours(9), TimeSpan.FromHours(10));

                // Act
                exam1.AddTimeslot(timeslot);  // Assigning timeslot to the first exam
                exam1.RemoveTimeslot();  // Removing timeslot from the first exam
                exam2.AddTimeslot(timeslot);  // Assigning the same timeslot to the second exam

                // Assert
                Assert.AreEqual(timeslot, exam2.Timeslot, "The timeslot should now be assigned to the second exam.");
            }
            [Test]
            public void Exam_ShouldThrowException_WhenAddingNullTimeslot()
            {
                // Arrange
                var exam = new Exam(DateTime.Now.AddDays(1));  // Exam scheduled for tomorrow

                // Act & Assert
                Assert.Throws<ArgumentNullException>(() => exam.AddTimeslot(null), "Timeslot cannot be null.");
            }

            [Test]
            public void Exam_ShouldThrowException_WhenTimeslotAssignedWithPastDate()
            {
                // Arrange
                var exam = new Exam(DateTime.Now.AddDays(1));  // Exam scheduled for tomorrow

                // Act & Assert
                Assert.Throws<ArgumentException>(() => new Timeslot(101, DateTime.Now.AddDays(-1), TimeSpan.FromHours(9), TimeSpan.FromHours(10)), "Date cannot be in the past.");
            }
            
            [Test]
            public void Timeslot_ShouldThrowException_WhenStartTimeGreaterThanEndTime()
            {
                // Arrange
                var exam = new Exam(DateTime.Now.AddDays(1));  // Exam scheduled for tomorrow

                // Act & Assert
                Assert.Throws<ArgumentException>(() => new Timeslot(101, DateTime.Now.AddDays(1), TimeSpan.FromHours(10), TimeSpan.FromHours(9)), "Start time must be earlier than the end time.");
            }
            [Test]
            public void Exam_ShouldThrowException_WhenStartTimeIsEqualToEndTime()
            {
                // Arrange
                var exam = new Exam(DateTime.Now.AddDays(1));  // Exam scheduled for tomorrow

                // Act & Assert: Start time equals end time should throw an exception
                Assert.Throws<ArgumentException>(() => new Timeslot(101, DateTime.Now.AddDays(1), TimeSpan.FromHours(9), TimeSpan.FromHours(9)),
                    "Start time cannot be equal to end time.");
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

            [Test]
            public void Timeslot_UpdateTime_WithValidTimes_ShouldNotThrowException()
            {
                // Arrange
                int scheduleId = 1;
                DateTime date = DateTime.Now.AddDays(1);
                TimeSpan startTime = TimeSpan.FromHours(10); // 10:00 AM
                TimeSpan endTime = TimeSpan.FromHours(12);  // 12:00 PM
                var timeslot = new Timeslot(scheduleId, date, startTime, endTime);

                // Act & Assert: Updating time with valid inputs should not throw any exception
                Assert.DoesNotThrow(() => timeslot.UpdateTime(TimeSpan.FromHours(11), TimeSpan.FromHours(13)));  // Corrected end time
            }
            
        }
        
        [TestFixture]
        public class ClassroomTests
        {


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
                var timeslot1 = new Timeslot(
                    101,
                    DateTime.Now.AddDays(1),
                    TimeSpan.FromHours(9),
                    TimeSpan.FromHours(10)
                );
                var timeslot2 = new Timeslot(
                    102,
                    DateTime.Now.AddDays(1),
                    TimeSpan.FromHours(11),
                    TimeSpan.FromHours(12)
                );

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
                var timeslot = new Timeslot(
                    101,
                    DateTime.Now.AddDays(1),
                    TimeSpan.FromHours(9),
                    TimeSpan.FromHours(10)
                );
                classroom.AssignTimeslot(timeslot);

                // Act & Assert
                Assert.Throws<InvalidOperationException>(() => classroom.AssignTimeslot(timeslot));
            }

            [Test]
            public void AssignTimeslot_ShouldUpdateClassroomInTimeslot()
            {
                // Arrange
                var classroom = new Classroom(1, 30);
                var timeslot = new Timeslot(
                    101,
                    DateTime.Now.AddDays(1),
                    TimeSpan.FromHours(9),
                    TimeSpan.FromHours(10)
                );

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
                var timeslot = new Timeslot(
                    101,
                    DateTime.Now.AddDays(1),
                    TimeSpan.FromHours(9),
                    TimeSpan.FromHours(10)
                );

                // Act & Assert
                Assert.Throws<InvalidOperationException>(
                    () => classroom1.ChangeClassroom(timeslot, classroom2)
                );
            }

            [Test]
            public void AssignTimeslot_ShouldFailIfTimeslotDatesOverlap()
            {
                // Arrange
                var classroom = new Classroom(1, 30);
                var date = DateTime.Today.AddDays(1); // Use DateTime.Today for consistent dates
                var timeslot1 = new Timeslot(
                    101,
                    date,
                    TimeSpan.FromHours(9),
                    TimeSpan.FromHours(10)
                );
                var timeslot2 = new Timeslot(
                    102,
                    date,
                    TimeSpan.FromHours(9).Add(TimeSpan.FromMinutes(30)),
                    TimeSpan.FromHours(10).Add(TimeSpan.FromMinutes(30))
                );

                classroom.AssignTimeslot(timeslot1);

                // Act & Assert
                Assert.Throws<InvalidOperationException>(() => classroom.AssignTimeslot(timeslot2));
            }
            [Test]
            public void ChangeClassroom_ShouldTransferTimeslotProperly()
            {
                // Arrange
                var classroom1 = new Classroom(1, 30);
                var classroom2 = new Classroom(2, 40);
                var timeslot = new Timeslot(101, DateTime.Today.AddDays(1), TimeSpan.FromHours(9), TimeSpan.FromHours(10));

                classroom1.AssignTimeslot(timeslot);

                // Act
                classroom1.ChangeClassroom(timeslot, classroom2);

                // Assert
                Assert.IsTrue(classroom1.GetAssignedTimeslots().Count == 0);
                Assert.IsTrue(classroom2.GetAssignedTimeslots().Contains(timeslot));
                Assert.AreEqual(classroom2, timeslot.Classroom);
            }
            [Test]
            public void RemoveTimeslot_ShouldUnassignFromClassroomAndTimeslot()
            {
                // Arrange
                var classroom = new Classroom(1, 30);
                var timeslot = new Timeslot(101, DateTime.Today.AddDays(1), TimeSpan.FromHours(9), TimeSpan.FromHours(10));
                classroom.AssignTimeslot(timeslot);

                // Act
                classroom.RemoveTimeslot(timeslot);

                // Assert
                Assert.IsFalse(classroom.GetAssignedTimeslots().Contains(timeslot));
                Assert.IsNull(timeslot.Classroom);
            }
        }
    }
}
