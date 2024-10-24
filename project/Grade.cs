using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading;

namespace assignment_3
{
    public class Grade
    {
        public int GradeId { get; private set; }
        public int GradeValue { get; set; }

        static int nextId;

        public Grade(int GradeValue)
        {
            GradeId = Interlocked.Increment(ref nextId);
            this.GradeValue = GradeValue;
        }

        public void GenerateBasicGrades() { }

        public void UpdateGrade() { }
    }
}
