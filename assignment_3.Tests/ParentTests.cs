using System.ComponentModel.DataAnnotations;
using assignment_3;
using NUnit.Framework;

namespace assignment_3.Tests
{
    public class ParentTests
    {
        List<Parent> parents = new();

        [Test]
        public void UniqueParentID()
        {
            for (int i = 0; i < 5; i++)
            {
                parents.Add(new Parent($"P{i}", $"P{i}@meme.com"));
            }
            var distinctParent = new HashSet<int>();
            foreach (var parent in parents)
            {
                Assert.That(distinctParent.Add(parent.ParentID), Is.True);
            }
        }

        [Test]
        public void BadEmailFormat()
        {
            Assert.Throws<ValidationException>(() => new Parent("Viridium", "vivi.viiviv.com"));
            Assert.Throws<ValidationException>(() => new Parent("Viridium", ""));
        }

        [Test]
        public void NameTooLong()
        {
            Assert.Throws<ArgumentOutOfRangeException>(
                () =>
                    new Parent(
                        "ViridiumViridiumViridiumViridiumViridiumViridiumViridiumViridium",
                        "vivi@viiviv.com"
                    )
            );
        }

        [Test]
        public void NameIsAnEmptyString()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Parent("", "vivi@viiviv.com"));
        }
    }
}
