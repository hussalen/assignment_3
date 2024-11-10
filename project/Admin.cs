using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace assignment_3
{
    public class Admin
    {
        public int AdminID { get; private set; }
        public string Name { get; set; }
        public static int nextId = 1;

        public Admin(string name)
        {
            AdminID = nextId++;
            Name = name;
        }

        public void CreateSchedule() { }
        public void UpdateSchedule() { }
        public void DeleteSchedule() { }
        public Report GenerateReport()
        {
            return new Report();
        }
    }
}