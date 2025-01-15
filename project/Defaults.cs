using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace assignment_3
{
    public static class Defaults
    {
        public static readonly Student DEFAULT_STUDENT = new(ClassLevel.Freshman);
        public static readonly Coding DEFAULT_CODING =
            new("DEFAULT", new DateTime(2026, 01, 01), "Python", "http://def.com");
        public static readonly Grade DEFAULT_GRADE = new(3);
        public static readonly Teacher DEFAULT_TEACHER = new Teacher("Abdullah");
    }
}
