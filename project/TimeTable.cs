using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace assignment_3
{
    public class TimeTable
    {
        public int TimeTableId { get; private set; }
        private string dayOfWeek { get; }
        static int nextId;

        public TimeTable(string dayOfWeek)
        {
            TimeTableId = Interlocked.Increment(ref nextId);
            this.dayOfWeek = dayOfWeek;
        }

        public void UpdateTimeTable() { }
    }
}
