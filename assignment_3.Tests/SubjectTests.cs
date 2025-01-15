using System;
using assignment_3;
using NUnit.Framework;

namespace assignment_3.Tests
{
    [TestFixture]
    public class SubjectTests
    {
        [Test]
        public void Subject_Creation_ShouldInitializeCorrectly()
        {
            // Arrange
            string subjectName = "Mathematics";
            int gradingScale = 3;

            // Act
            var subject = new Subject(subjectName, gradingScale);

            // Assert
            Assert.AreEqual(subjectName, subject.SubjectName);
            Assert.AreEqual(gradingScale, subject.GradingScale);
        }

        [Test]
        public void Subject_Creation_WithEmptySubjectId_ShouldThrowException()
        {
            // Arrange
            string subjectName = "Mathematics";
            int gradingScale = 3;

            // Act & Assert
            Assert.Throws<ArgumentException>(
                () => new Subject(subjectName, gradingScale),
                "Subject ID cannot be empty"
            );
        }

        [Test]
        public void Subject_Creation_WithNullSubjectId_ShouldThrowException()
        {
            // Arrange
            string subjectName = "Mathematics";
            int gradingScale = 3;

            // Act & Assert
            Assert.Throws<ArgumentException>(
                () => new Subject(subjectName, gradingScale),
                "Subject ID cannot be empty"
            );
        }

        [Test]
        public void Subject_Creation_WithEmptySubjectName_ShouldThrowException()
        {
            // Arrange
            string subjectName = "";
            int gradingScale = 3;

            // Act & Assert
            Assert.Throws<ArgumentException>(
                () => new Subject(subjectName, gradingScale),
                "Subject name cannot be empty"
            );
        }

        [Test]
        public void Subject_Creation_WithNullSubjectName_ShouldThrowException()
        {
            // Arrange
            string subjectName = null;
            int gradingScale = 3;

            // Act & Assert
            Assert.Throws<ArgumentException>(
                () => new Subject(subjectName, gradingScale),
                "Subject name cannot be empty"
            );
        }

        [Test]
        public void Subject_Creation_WithInvalidGradingScale_ShouldThrowException()
        {
            // Arrange
            string subjectName = "Mathematics";
            int invalidGradingScale = 6;

            // Act & Assert
            Assert.Throws<ArgumentException>(
                () => new Subject(subjectName, invalidGradingScale),
                "Grading scale must be between 1 and 5"
            );
        }

        [Test]
        public void UpdateSubjectName_WithValidName_ShouldUpdateCorrectly()
        {
            // Arrange
            var subject = new Subject("Mathematics", 3);
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
            var subject = new Subject("Mathematics", 3);
            string newSubjectName = "";

            // Act & Assert
            Assert.Throws<ArgumentException>(
                () => subject.UpdateSubjectName(newSubjectName),
                "Subject name cannot be empty"
            );
        }

        [Test]
        public void UpdateSubjectName_ShouldUpdateNameCorrectly()
        {
            // Arrange
            var math = new Subject("Mathematics", 5);

            // Act
            math.UpdateSubjectName("Advanced Mathematics");

            // Assert
            Assert.AreEqual("Advanced Mathematics", math.SubjectName); // Check if name was updated
        }

        [Test]
        public void AddSubSubject_ShouldThrowException_WhenAddingItself()
        {
            // Arrange
            var math = new Subject("Mathematics", 5);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => math.AddSubSubject(math)); // Should throw exception when adding itself as a sub-subject
        }

        [Test]
        public void AddSubSubject_ShouldThrowException_WhenSubjectAlreadyHasParent()
        {
            // Arrange
            var math = new Subject("Mathematics", 5);
            var algebra = new Subject("Algebra", 4);
            math.AddSubSubject(algebra); // Add Algebra to Math

            // Act & Assert
            var calculus = new Subject("Calculus", 5);
            Assert.Throws<InvalidOperationException>(() => calculus.AddSubSubject(algebra)); // Algebra cannot have two parents
        }

        [Test]
        public void AddSubSubject_ShouldAddMultipleSubSubjects()
        {
            // Arrange
            var math = new Subject("Mathematics", 5);
            var algebra = new Subject("Algebra", 4);
            var geometry = new Subject("Geometry", 4);

            // Act
            math.AddSubSubject(algebra);
            math.AddSubSubject(geometry);

            // Assert
            Assert.Contains(algebra, math.GetSubSubjects());
            Assert.Contains(geometry, math.GetSubSubjects());
        }

        [Test]
        public void AddSubSubject_ShouldThrowException_WhenCreatingCircularReference()
        {
            // Arrange
            var math = new Subject("Mathematics", 5);
            var algebra = new Subject("Algebra", 4);
            math.AddSubSubject(algebra);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => algebra.AddSubSubject(math));
        }

        [Test]
        public void Subject_ShouldAllowMultipleTimeslots()
        {
            // Arrange
            var subject = new Subject("Mathematics", 4);
            var timeslot1 = new Timeslot(
                3,
                DateTime.Now.AddDays(1),
                TimeSpan.FromHours(9),
                TimeSpan.FromHours(11)
            );
            var timeslot2 = new Timeslot(
                2,
                DateTime.Now.AddDays(1),
                TimeSpan.FromHours(12),
                TimeSpan.FromHours(13)
            );

            // Act
            subject.AddTimeslot(timeslot1);
            subject.AddTimeslot(timeslot2);

            // Assert
            Assert.AreEqual(2, subject.GetTimeslots().Count, "Subject should have two timeslots.");
            Assert.Contains(
                timeslot1,
                subject.GetTimeslots(),
                "Timeslot 1 should be assigned to the subject."
            );
            Assert.Contains(
                timeslot2,
                subject.GetTimeslots(),
                "Timeslot 2 should be assigned to the subject."
            );
        }

        [Test]
        public void Timeslot_ShouldOnlyBelongToOneSubject()
        {
            // Arrange
            var subject1 = new Subject("Mathematics", 4);
            var subject2 = new Subject("Science", 4);
            var timeslot = new Timeslot(
                4,
                DateTime.Now.AddDays(1),
                TimeSpan.FromHours(9),
                TimeSpan.FromHours(11)
            );

            // Act
            subject1.AddTimeslot(timeslot);

            // Assert
            Assert.Throws<ArgumentException>(
                () => subject2.AddTimeslot(timeslot),
                "A timeslot should not be able to belong to two subjects."
            );
        }

        [Test]
        public void Subject_ShouldRemoveTimeslotCorrectly()
        {
            // Arrange
            var subject = new Subject("History", 5);
            var timeslot = new Timeslot(
                5,
                DateTime.Now.AddDays(1),
                TimeSpan.FromHours(9),
                TimeSpan.FromHours(11)
            );

            // Act
            subject.AddTimeslot(timeslot);
            subject.RemoveTimeslot(timeslot);

            // Assert
            Assert.AreEqual(
                0,
                subject.GetTimeslots().Count,
                "The subject should not have any timeslots after removal."
            );
        }

        [Test]
        public void Subject_ShouldAddTimeslotCorrectly()
        {
            // Arrange
            var subject = new Subject("English", 3);
            var timeslot = new Timeslot(
                1,
                DateTime.Now.AddDays(1),
                TimeSpan.FromHours(9),
                TimeSpan.FromHours(11)
            );

            // Act
            subject.AddTimeslot(timeslot);

            // Assert
            Assert.AreEqual(
                1,
                subject.GetTimeslots().Count,
                "The subject should have one timeslot."
            );
            Assert.AreEqual(
                timeslot,
                subject.GetTimeslots()[0],
                "The added timeslot should be the first in the list."
            );
        }
    }
}
