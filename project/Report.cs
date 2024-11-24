using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace assignment_3
{
    public class Report
    {
        public int ReportId { get; private set; }
        public JsonArray? Content { get; set; }

        static int nextId;

        public Report()
        {
            ReportId = Interlocked.Increment(ref nextId);
            addReport(this);
        }

        public void GenerateReport() { }

        private static List<Report> _report_List = new();

        private static void addReport(Report report)
        {
            if (report is null)
            {
                throw new ArgumentException($"{nameof(report)} cannot be null.");
            }
            _report_List.Add(report);
        }

        public static List<Report> GetReportExtent() => new List<Report>(_report_List);
    }
}
