using System;
using System.ComponentModel.DataAnnotations;
using assignment_3;
using NUnit.Framework;

namespace assignment_3.Tests
{
    [TestFixture]
    public class StudentTests
    {
        private List<Student> students = new();
        private Student currentStudent;

        [SetUp]
        public void Setup()
        {
            currentStudent = new Student(ClassLevel.Freshman, 4.0f);
            students.Add(currentStudent);
        }

        [Test]
        public void UniqueStudentID()
        {
            for (int i = 0; i < 5; i++)
            {
                students.Add(new Student(ClassLevel.Freshman, 4.0f));
            }

            var distinctStudentIds = new HashSet<int>();
            foreach (var student in students)
            {
                Assert.That(distinctStudentIds.Add(student.StudentID), Is.True);
            }
        }

        [Test]
        public void GPAIsValidWhenInRange()
        {
            var validGpa = new Student(ClassLevel.Freshman, 4.0f);
            Assert.That(validGpa.GPA, Is.EqualTo(4.0f), "Valid GPA should be accepted.");
        }

        [Test]
        public void GPAThrowsValidationExceptionWhenOutOfRange()
        {
            Assert.Throws<ValidationException>(() => new Student(ClassLevel.Sophomore, 2.55f)); // Below range
            Assert.Throws<ValidationException>(() => new Student(ClassLevel.Junior, 5.1f)); // Above range
        }

        [Test]
        public void TooManyDecimalPointsForGpa()
        {
            Assert.Throws<ArgumentException>(() => new Student(ClassLevel.Sophomore, 3.333f));
        }

        [Test]
        public void EditAssignmentSubmissionUpdatesSubmissionDate()
        {
            var assignment = new Assignment("Test Assignment") { SubmissionDate = DateTime.Now };
            currentStudent.EditAssignmentSubmission(assignment);

            Assert.That(assignment.SubmissionDate, Is.EqualTo(DateTime.UtcNow));
        }

        [Test]
        public void EditAssignmentSubmissionThrowsExceptionIfAssignmentDoesNotExist()
        {
            var nonExistentAssignment = new Assignment("Non-existent Assignment");
            Assert.Throws<ArgumentException>(() => currentStudent.EditAssignmentSubmission(nonExistentAssignment));
        }

        [Test]
        public void EditAssignmentSubmissionThrowsExceptionIfNotCurrentStudent()
        {
            var otherStudent = new Student(ClassLevel.Sophomore, 4.0f);
            var assignment = new Assignment("Test Assignment") { SubmittingStudent = otherStudent };
            Assert.Throws<InvalidOperationException>(() => currentStudent.EditAssignmentSubmission(assignment));
        }
    }
}
