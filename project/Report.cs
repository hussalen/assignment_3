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
        public JsonArray? content { get; set; }

        static int nextId;

        public Report()
        {
            ReportId = Interlocked.Increment(ref nextId);
        }

        public void GenerateReport() { }
    }
}
