using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
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
                if (value is < 1 or > 5)
                {
                    throw new ArgumentOutOfRangeException($"Invalid grade value: {value}");
                }
                _gradeValue = value;
            }
        }

        private Student _student;
        public Student Student
        {
            get => new(_student.ClassLevel);
            set => _student = value;
        }

        static int nextId;

        public Grade(uint GradeValue)
        {
            GradeId = Interlocked.Increment(ref nextId);
            this.GradeValue = GradeValue;
            Student = Defaults.DEFAULT_STUDENT;
            addGrade(this);
            //SaveManager.SaveToJson(_grade_List, nameof(_grade_List));
        }

        public void ClearStudent()
        {
            Student = Defaults.DEFAULT_STUDENT;
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
