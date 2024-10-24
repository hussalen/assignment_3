using System.ComponentModel.DataAnnotations;
using assignment_3;
using NUnit.Framework;

namespace myparent_tests
{
    public class ParentTests
    {
        [Test]
        public void BadEmailFormat()
        {
            Assert.Throws<ValidationException>(() => new Parent("Viridium", "vivi.viiviv.com"));
        }
    }
}
