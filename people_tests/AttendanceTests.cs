using System;
using NUnit.Framework;

namespace assignment_3.Tests
{
    [TestFixture]
    public class AttendanceTests
    {
        private List<Attendance> attendances = new();

        [Test]
        public void UniqueAttendanceID()
        {
            for (int i = 0; i < 5; i++)
            {
                attendances.Add(new Attendance());
            }

            var distinctAttendanceIds = new HashSet<int>();
            foreach (var attendance in attendances)
            {
                Assert.That(distinctAttendanceIds.Add(attendance.AttendanceID), Is.True);
            }
        }

        [Test]
        public void DefaultAttendanceIsMarkedAsAbsent()
        {
            var attendance = new Attendance();
            Assert.That(
                attendance.isPresent,
                Is.False,
                "New attendance should be marked as absent by default."
            );
        }
    }
}
