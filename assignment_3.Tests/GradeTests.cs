using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using assignment_3;
using NUnit.Framework;

namespace assignment_3.Tests
{
    public class GradeTests
    {
        List<Grade> grades = new();

        [Test]
        public void UniqueGradeID()
        {
            for (uint i = 0; i < 5; i++)
            {
                grades.Add(new Grade(i));
            }

            var distinctGradeIds = new HashSet<int>();
            foreach (var grade in grades)
            {
                Assert.That(distinctGradeIds.Add(grade.GradeId), Is.True);
            }
        }

        [Test]
        public void GradeValueCannotBeGreaterThan5()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Grade(6));
        }
    }
}
