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
        private static int nextId;

        private JsonArray? _content;
        public JsonArray? Content
        {
            get => _content;
            set
            {
                if (value is not null && value.Count == 0)
                {
                    throw new ArgumentException("Content cannot be an empty JsonArray.");
                }
                _content = value;
            }
        }

        private static readonly List<Report> _reportList = new();

        public Report()
        {
            ReportId = Interlocked.Increment(ref nextId);
            AddReport(this);
            SaveManager.SaveToJson(_reportList, nameof(_reportList));
        }

        public void GenerateReport()
        {
            if (Content is null)
            {
                throw new InvalidOperationException("Cannot generate a report without content.");
            }
            Console.WriteLine($"Report {ReportId} generated with content: {Content}");
        }

        private static void AddReport(Report report)
        {
            if (report is null)
            {
                throw new ArgumentException($"{nameof(report)} cannot be null.");
            }
            _reportList.Add(report);
        }

        public static List<Report> GetReportExtent() => new(_reportList);
    }
}