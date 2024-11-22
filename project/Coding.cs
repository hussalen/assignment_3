using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace assignment_3
{
    public class Coding : Assignment
    {
        int Assignment.AssignmentID => throw new NotImplementedException();

        public string Topic { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? SubmissionDate { get; set; }

        public string language;
        public string repositoryUrl;

        public Coding(string language, string repositoryUrl)
        {
            this.language = language;
            this.repositoryUrl = repositoryUrl;
            addCoding(this);
        }

        private static List<Coding> _coding_List = new();

        public static List<Coding> GetCodingExtent() => new List<Coding>(_coding_List);

        private static void addCoding(Coding coding)
        {
            if (coding is null)
            {
                throw new ArgumentException($"{nameof(coding)} cannot be null.");
            }
            _coding_List.Add(coding);
        }
    }
}
