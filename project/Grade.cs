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

        
        public Teacher AssignedTeacher { get; private set; }

        public Grade(uint gradeValue)
        {
            GradeId = Interlocked.Increment(ref nextId);
            this.GradeValue = gradeValue;
            AddGrade(this);
            SaveManager.SaveToJson(_grade_List, nameof(_grade_List));
        }

        public void GenerateBasicGrades()
        {
            
        }

        public void UpdateGrade()
        {
            
        }

        private static List<Grade> _grade_List = new();

        private static void AddGrade(Grade grade)
        {
            if (grade is null)
            {
                throw new ArgumentException($"{nameof(grade)} cannot be null.");
            }
            _grade_List.Add(grade);
        }

        public static List<Grade> GetGradeExtent() => new List<Grade>(_grade_List);

        
        public void SetTeacher(Teacher teacher)
        {
            if (teacher == null)
                throw new ArgumentException("Teacher cannot be null.");

            if (AssignedTeacher != teacher)
            {
                AssignedTeacher = teacher;
                if (!teacher.AssignedGrades.Contains(this))
                {
                    teacher.AssignGrade(this); 
            }
        }
    }
}
