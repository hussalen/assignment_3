using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

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

        private static int nextId = 1;

        private Day _dayOfWeek;
        public Day DayOfWeek
        {
            get => _dayOfWeek;
            private set => _dayOfWeek = ValidateDayOfWeek(value);
        }

        private static List<TimeTable> _timetableList = new();

        public TimeTable(Day dayOfWeek)
        {
            TimeTableId = Interlocked.Increment(ref nextId);
            DayOfWeek = dayOfWeek;
            AddTimeTable(this);
            SaveManager.SaveToJson(_timetableList, nameof(_timetableList));
        }

        private static void AddTimeTable(TimeTable timetable)
        {
            if (timetable is null)
                throw new ArgumentException($"{nameof(timetable)} cannot be null.");

            if (_timetableList.Any(t => t.DayOfWeek == timetable.DayOfWeek))
                throw new ArgumentException(
                    $"A timetable for {timetable.DayOfWeek} already exists."
                );

            _timetableList.Add(timetable);
        }

        public static List<TimeTable> GetTimeTableExtent() => new(_timetableList);

        private Day ValidateDayOfWeek(Day dayOfWeek)
        {
            if (!Enum.IsDefined(typeof(Day), dayOfWeek))
                throw new ArgumentOutOfRangeException($"Day {dayOfWeek} is invalid.");
            return dayOfWeek;
        }

        public void UpdateTimeTable(Day newDayOfWeek)
        {
            if (!Enum.IsDefined(typeof(Day), newDayOfWeek))
                throw new ArgumentOutOfRangeException($"Day {newDayOfWeek} is invalid.");

            if (
                _timetableList.Any(t => t.TimeTableId != TimeTableId && t.DayOfWeek == newDayOfWeek)
            )
                throw new ArgumentException($"A timetable for {newDayOfWeek} already exists.");

            DayOfWeek = newDayOfWeek;
        }
    }
}
