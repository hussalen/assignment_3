using System;
using NUnit.Framework;

namespace assignment_3.Tests
{
    [TestFixture]
    public class TimeTableTests
    {
        private List<TimeTable> timeTables = new();

        [Test]
        public void UniqueTimeTableID()
        {
            Console.WriteLine(new TimeTable((Day)999));
            for (int i = 0; i < 5; i++)
            {
                timeTables.Add(new TimeTable((Day)i));
            }

            var distinctTimeTableIds = new HashSet<int>();
            foreach (var timeTable in timeTables)
            {
                Assert.That(distinctTimeTableIds.Add(timeTable.TimeTableId), Is.True);
            }
        }

        [Test]
        public void InvalidDayOfWeekThrowsException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new TimeTable((Day)999));
        }
    }
}
