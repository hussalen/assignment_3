using System;
using NUnit.Framework;

namespace assignment_3.Tests
{
    [TestFixture]
    public class TimeTableTests
    {
        private List<TimeTable> timeTables = new();

        [Test]
        public void InvalidDayOfWeekThrowsException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new TimeTable((Day)999));
        }
    }
}
