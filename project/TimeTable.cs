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

        static int nextId;
        public Day DayOfWeek { get; private set; }

        public TimeTable(Day DayOfWeek)
        {
            TimeTableId = Interlocked.Increment(ref nextId);
            this.DayOfWeek = ValidDayOfWeek(DayOfWeek);
            addTimeTable(this);
        }

        private static List<TimeTable> _timetable_List = new();

        private static void addTimeTable(TimeTable timetable)
        {
            if (timetable is null)
            {
                throw new ArgumentException($"{nameof(timetable)} cannot be null.");
            }
            _timetable_List.Add(timetable);
        }

        public static List<TimeTable> GetTimeTableExtent() => new List<TimeTable>(_timetable_List);

        public Day ValidDayOfWeek(Day dayOfWeek)
        {
            if (!Enum.IsDefined(typeof(Day), dayOfWeek))
                throw new ArgumentOutOfRangeException($"Day {dayOfWeek} is invalid");
            return dayOfWeek;
        }

        public void UpdateTimeTable() { }
    }
}
