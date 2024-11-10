using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace assignment_3.Tests
{
    [TestFixture]
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
