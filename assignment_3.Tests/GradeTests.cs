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
        [Test]
        public void GradeValueCannotBeGreaterThan5()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Grade(6));
        }
    }
}
