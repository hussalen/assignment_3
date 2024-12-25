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
            Assert.AreEqual("Advanced Mathematics", math.SubjectName); // Check if name was updated
        }

        [Test]
        public void AddSubSubject_ShouldThrowException_WhenAddingItself()
        {
            // Arrange
            var math = new Subject("S1", "Mathematics", 5);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => math.AddSubSubject(math)); // Should throw exception when adding itself as a sub-subject
        }

        [Test]
        public void AddSubSubject_ShouldThrowException_WhenSubjectAlreadyHasParent()
        {
            // Arrange
            var math = new Subject("S1", "Mathematics", 5);
            var algebra = new Subject("S2", "Algebra", 4);
            var physics = new Subject("S3", "Physics", 5);

            // Add Algebra as a sub-subject of Math
            math.AddSubSubject(algebra);

            // Act & Assert: Try adding Algebra as a sub-subject of another parent (Physics)
            Assert.Throws<InvalidOperationException>(() => physics.AddSubSubject(algebra),
                "A sub-subject cannot have more than one parent.");
        }

    
        [Test]
        public void Subject_ShouldAllowMultipleTimeslots()
        {
            // Arrange
            var subject = new Subject("MATH101", "Mathematics", 4);
            var timeslot1 = new Timeslot(1, DateTime.Now.AddDays(1), TimeSpan.FromHours(9), TimeSpan.FromHours(11));
            var timeslot2 = new Timeslot(2, DateTime.Now.AddDays(1), TimeSpan.FromHours(12), TimeSpan.FromHours(13));

            // Act
            subject.AddTimeslot(timeslot1);
            subject.AddTimeslot(timeslot2);

            // Assert
            Assert.AreEqual(2, subject.GetTimeslots().Count, "Subject should have two timeslots.");
            Assert.Contains(timeslot1, subject.GetTimeslots(), "Timeslot 1 should be assigned to the subject.");
            Assert.Contains(timeslot2, subject.GetTimeslots(), "Timeslot 2 should be assigned to the subject.");
        }

        [Test]
        public void Timeslot_ShouldOnlyBelongToOneSubject()
        {
            // Arrange
            var subject1 = new Subject("MATH101", "Mathematics", 4);
            var subject2 = new Subject("SCI101", "Science", 4);
            var timeslot = new Timeslot(1, DateTime.Now.AddDays(1), TimeSpan.FromHours(9), TimeSpan.FromHours(11));

            // Act
            subject1.AddTimeslot(timeslot);

            // Assert
            Assert.Throws<ArgumentException>(() => subject2.AddTimeslot(timeslot), "A timeslot should not be able to belong to two subjects.");
        }
        [Test]
        public void Subject_ShouldRemoveTimeslotCorrectly()
        {
            // Arrange
            var subject = new Subject("HIST101", "History", 5);
            var timeslot = new Timeslot(1, DateTime.Now.AddDays(1), TimeSpan.FromHours(9), TimeSpan.FromHours(11));

            // Act
            subject.AddTimeslot(timeslot);
            subject.RemoveTimeslot(timeslot);

            // Assert
            Assert.AreEqual(0, subject.GetTimeslots().Count, "The subject should not have any timeslots after removal.");
        }
        [Test]
        public void Subject_ShouldNotAllowAddingNullTimeslot()
        {
            // Arrange
            var subject = new Subject("ENG101", "English", 3);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => subject.AddTimeslot(null), "Timeslot cannot be null.");
        }
        [Test]
        public void Subject_ShouldNotAllowRemovingNullTimeslot()
        {
            // Arrange
            var subject = new Subject("ENG101", "English", 3);
            var timeslot = new Timeslot(1, DateTime.Now.AddDays(1), TimeSpan.FromHours(9), TimeSpan.FromHours(11));

            subject.AddTimeslot(timeslot);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => subject.RemoveTimeslot(null), "Timeslot cannot be null.");
        }


        [Test]
        public void Subject_ShouldAddTimeslotCorrectly()
        {
            // Arrange
            var subject = new Subject("ENG101", "English", 3);
            var timeslot = new Timeslot(1, DateTime.Now.AddDays(1), TimeSpan.FromHours(9), TimeSpan.FromHours(11));

            // Act
            subject.AddTimeslot(timeslot);

            // Assert
            Assert.AreEqual(1, subject.GetTimeslots().Count, "The subject should have one timeslot.");
            Assert.AreEqual(timeslot, subject.GetTimeslots()[0], "The added timeslot should be the first in the list.");
        }
        [Test]
        public void AddSubSubject_ShouldThrowException_WhenAddingDuplicateSubSubject()
        {
            // Arrange
            var parentSubject = new Subject("S1", "ParentSubject", 5);
            var childSubject = new Subject("S2", "ChildSubject", 4);

            // Act
            parentSubject.AddSubSubject(childSubject);

            // Assert
            Assert.Throws<InvalidOperationException>(() => parentSubject.AddSubSubject(childSubject),
                "Adding the same sub-subject twice should throw an exception.");
        }
        [Test]
        public void ParentSubject_ShouldBeSetCorrectly_WhenAddingSubSubject()
        {
            // Arrange
            var parentSubject = new Subject("S1", "ParentSubject", 5);
            var childSubject = new Subject("S2", "ChildSubject", 4);

            // Act
            parentSubject.AddSubSubject(childSubject);

            // Assert
            Assert.AreEqual(parentSubject, childSubject.ParentSubject, "ParentSubject should be set correctly when adding a sub-subject.");
        }
        [Test]
        public void ParentSubject_ShouldBeNull_WhenRemovingSubSubject()
        {
            // Arrange
            var parentSubject = new Subject("S1", "ParentSubject", 5);
            var childSubject = new Subject("S2", "ChildSubject", 4);

            // Act
            parentSubject.AddSubSubject(childSubject);
            parentSubject.RemoveSubSubject(childSubject);

            // Assert
            Assert.IsNull(childSubject.ParentSubject, "ParentSubject should be null after removing the sub-subject.");
        }
        [Test]
        public void AddSubSubject_ShouldThrowException_WhenAddingSameSubSubjectMultipleTimes()
        {
            // Arrange
            var parentSubject = new Subject("S1", "ParentSubject", 5);
            var childSubject = new Subject("S2", "ChildSubject", 4);

            // Act
            parentSubject.AddSubSubject(childSubject);

            // Assert
            Assert.Throws<InvalidOperationException>(() => parentSubject.AddSubSubject(childSubject),
                "Adding the same sub-subject twice should throw an exception.");
        }
    }
}
