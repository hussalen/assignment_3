using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace assignment_3
{
    public enum Day
    {
        MONDAY,
        TUESDAY,
        WEDNESDAY,
        THURSDAY,
        FRIDAY
    }

    public class TimeTable
    {
        public int TimeTableId { get; private set; }

        public string dayOfWeek { get; }

        static int nextId;
        private Day DayOfWeek { get; set; }

        public TimeTable(Day DayOfWeek)
        {
            TimeTableId = Interlocked.Increment(ref nextId);
            this.DayOfWeek = ValidDayOfWeek(DayOfWeek);
        }

        public Day ValidDayOfWeek(Day dayOfWeek)
        {
            if (!Enum.IsDefined(typeof(Day), dayOfWeek))
                throw new ArgumentOutOfRangeException($"Day {dayOfWeek} is invalid");
            return dayOfWeek;
        }

        public void UpdateTimeTable() { }
    }
}
