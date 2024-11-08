using NUnit.Framework;
using System;

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

        /* to be fixed

        [Test]
        public void CorrectFormat()
        {
            var exam = new Exam();
            exam.ScheduleExam("2024-10-24");
            Assert.AreEqual(new DateTime(2024, 10, 24), exam.ExamDate);
        }
        */

        [Test]
        public void NullInput()
        {
            var exam = new Exam();
            Assert.Throws<FormatException>(() => exam.ScheduleExam(null));
        }
    }
}
