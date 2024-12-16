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
            currentStudent = new Student(ClassLevel.Freshman);
            students.Add(currentStudent);
        }

        [Test]
        public void UniqueStudentID()
        {
            for (int i = 0; i < 5; i++)
            {
                students.Add(new Student(ClassLevel.Freshman));
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
            var validGpa = new Student(ClassLevel.Freshman);
            Assert.That(validGpa.GPA, Is.EqualTo(4.0f), "Valid GPA should be accepted.");
        }
    }
}
