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

        private uint _gradeValue;
        public uint GradeValue
        {
            get => _gradeValue;
            init
            {
                if (value is > 5)
                {
                    throw new ArgumentOutOfRangeException($"Invalid grade value: {value}");
                }
                _gradeValue = value;
            }
        }

        static int nextId;

        public Grade(uint GradeValue)
        {
            GradeId = Interlocked.Increment(ref nextId);
            this.GradeValue = GradeValue;
            addGrade(this);
            SaveManager.SaveToJson(_grade_List, nameof(_grade_List));
        }

        public void GenerateBasicGrades() { }

        public void UpdateGrade() { }

        private static List<Grade> _grade_List = new();

        private static void addGrade(Grade grade)
        {
            if (grade is null)
            {
                throw new ArgumentException($"{nameof(grade)} cannot be null.");
            }
            _grade_List.Add(grade);
        }

        public static List<Grade> GetGradeExtent() => new List<Grade>(_grade_List);
    }
}
