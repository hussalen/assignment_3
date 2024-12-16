using System;
using System.Collections.Generic;
using System.Text.Json.Nodes;
using System.Threading;

namespace assignment_3
{
    public class Admin
    {
        private static int nextId = 1;

        public int AdminID { get; private set; }

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Name cannot be null or empty.");
                _name = value;
            }
        }

        public List<Timeslot> ManagedTimeslots { get; private set; } = new();

        public Admin(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Admin name cannot be null or empty.");

            AdminID = Interlocked.Increment(ref nextId);
            Name = name;
        }

        public void CreateSchedule(DateTime date, TimeSpan startTime, TimeSpan endTime)
        {
            if (date < DateTime.Now.Date)
                throw new ArgumentException("Schedule date cannot be in the past.");
            if (startTime >= endTime)
                throw new ArgumentException("Start time must be earlier than end time.");

            var newTimeslot = new Timeslot(ManagedTimeslots.Count + 1, date, startTime, endTime);
            AddTimeslot(newTimeslot);

            Console.WriteLine(
                $"Schedule created by Admin {Name}: {date.ToShortDateString()}, {startTime} - {endTime}"
            );
        }

        public void UpdateSchedule(
            int scheduleId,
            DateTime date,
            TimeSpan startTime,
            TimeSpan endTime
        )
        {
            if (scheduleId <= 0)
                throw new ArgumentException("Schedule ID must be a positive integer.");
            if (date < DateTime.Now.Date)
                throw new ArgumentException("Schedule date cannot be in the past.");
            if (startTime >= endTime)
                throw new ArgumentException("Start time must be earlier than end time.");

            var timeslot = ManagedTimeslots.Find(t => t.ScheduleId == scheduleId);
            if (timeslot != null)
            {
                timeslot.Date = date;
                timeslot.UpdateTime(startTime, endTime);
                Console.WriteLine(
                    $"Schedule ID {scheduleId} updated by Admin {Name}: {date.ToShortDateString()}, {startTime} - {endTime}"
                );
            }
            else
            {
                throw new ArgumentException($"No timeslot with Schedule ID {scheduleId} found.");
            }
        }

        public void DeleteSchedule(int scheduleId)
        {
            if (scheduleId <= 0)
                throw new ArgumentException("Schedule ID must be a positive integer.");

            var timeslot = ManagedTimeslots.Find(t => t.ScheduleId == scheduleId);
            if (timeslot != null)
            {
                ManagedTimeslots.Remove(timeslot);
                Console.WriteLine($"Schedule ID {scheduleId} deleted by Admin {Name}");
            }
            else
            {
                throw new ArgumentException($"No timeslot with Schedule ID {scheduleId} found.");
            }
        }

        public Report GenerateReport(string title, JsonArray content)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Report title cannot be null or empty.");

            var report = new Report(content);
            Console.WriteLine(
                $"Report generated by Admin {Name}: {report.Content}, ID {report.ReportId}"
            );
            return report;
        }

        public void AddTimeslot(Timeslot timeslot)
        {
            if (timeslot == null)
                throw new ArgumentException("Timeslot cannot be null.");

            if (!ManagedTimeslots.Contains(timeslot))
            {
                ManagedTimeslots.Add(timeslot);
                timeslot.SetAdmin(this); 
            }
        }
    }
}
