using System;
using System.ComponentModel.DataAnnotations;
using assignment_3;
using NUnit.Framework;

namespace people_tests
{
    [TestFixture]
    public class StudentTests
    {
        private List<Student> students = new();

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
    }
}
