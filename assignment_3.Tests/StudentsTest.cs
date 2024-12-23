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
        private TimeTable _timeTable;

        [SetUp]
        public void Setup()
        {
            currentStudent = new Student(ClassLevel.Freshman);
            students.Add(currentStudent);
            _timeTable = new TimeTable(Day.MONDAY);
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

        // [Test]
        // AddGrade
        [Test]
        public void GradeIsNotAssignedToStudent()
        {
            var student1 = new Student(ClassLevel.Freshman);
        }

        [Test]
        public void GradeIsAlreadyAssigned()
        {
            var grade = new Grade(3);
            currentStudent.AddGrade(grade);

            var ex = Assert.Throws<ArgumentException>(() => currentStudent.AddGrade(grade));
            Assert.That(ex.Message, Is.EqualTo("Grade is already assigned."));
        }

        [Test]
        public void GradeAlreadyExists()
        {
            var grade = new Grade(3);
            currentStudent.AddGrade(grade);

            var ex = Assert.Throws<ArgumentException>(() => currentStudent.AddGrade(grade));
            Assert.That(ex.Message, Is.EqualTo("Grade already exists."));
        }

        [Test]
        public void GradeDoesNotExist()
        {
            var grade = new Grade(3);

            var ex = Assert.Throws<ArgumentException>(() => currentStudent.RemoveGrade(grade));
            Assert.That(ex.Message, Is.EqualTo("Grade does not exist."));
        }

        [Test]
        public void GradeIsNotAssigned()
        {
            var grade = new Grade(3);

            var ex = Assert.Throws<ArgumentException>(() => currentStudent.UpdateGrade(grade));
            Assert.That(
                ex.Message,
                Is.EqualTo(
                    $"Grade is not assigned to this student's grade, but assigned to {nameof(grade.Student)}."
                )
            );
        }

        [Test]
        public void WhenTimeTableIsAlreadyAssigned()
        {
            currentStudent.AddTimeTable(_timeTable);

            var ex = Assert.Throws<InvalidOperationException>(
                () => currentStudent.AddTimeTable(_timeTable)
            );
            Assert.That(
                ex.Message,
                Is.EqualTo($"TimeTable {_timeTable.Id} is already assigned to Student.")
            );
        }

        [Test]
        public void WhenNoTimeTableIsAssigned()
        {
            var ex = Assert.Throws<InvalidOperationException>(
                () => currentStudent.RemoveTimeTable()
            );
            Assert.That(ex.Message, Is.EqualTo("No timetable is assigned to Student."));
        }

        [Test]
        public void ShouldRemoveAssignedTimeTable()
        {
            currentStudent.AddTimeTable(_timeTable);

            currentStudent.RemoveTimeTable();
            Assert.Throws<InvalidOperationException>(() => currentStudent.RemoveTimeTable());
        }
    }
}
