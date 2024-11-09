using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading;

namespace assignment_3
{
    public class Grade
    {
        public int GradeId { get; private set; }
        public uint GradeValue
        {
            get => GradeValue;
            init
            {
                if (value is > 5)
                {
                    throw new ArgumentOutOfRangeException($"Invalid grade value: {value}");
                }
            }
        }

        static int nextId;

        public Grade(uint GradeValue)
        {
            GradeId = Interlocked.Increment(ref nextId);
            this.GradeValue = GradeValue;
        }

        public void GenerateBasicGrades() { }

        public void UpdateGrade() { }
    }
}
