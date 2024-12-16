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
        public void UpdateSubjectName_ShouldUpdateNameCorrectly()
        {
            // Arrange
            var math = new Subject("S1", "Mathematics", 5);

            // Act
            math.UpdateSubjectName("Advanced Mathematics");

            // Assert
            Assert.AreEqual("Advanced Mathematics", math.SubjectName);  // Check if name was updated
        }

        [Test]
        public void AddSubSubject_ShouldThrowException_WhenAddingItself()
        {
            // Arrange
            var math = new Subject("S1", "Mathematics", 5);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => math.AddSubSubject(math));  // Should throw exception when adding itself as a sub-subject
        }

        [Test]
        public void AddSubSubject_ShouldThrowException_WhenSubjectAlreadyHasParent()
        {
            // Arrange
            var math = new Subject("S1", "Mathematics", 5);
            var algebra = new Subject("S2", "Algebra", 4);
            math.AddSubSubject(algebra);  // Add Algebra to Math

            // Act & Assert
            var calculus = new Subject("S3", "Calculus", 5);
            Assert.Throws<InvalidOperationException>(() => algebra.AddSubSubject(calculus));  // Algebra cannot have two parents
        }
    }
}
