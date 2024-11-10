using NUnit.Framework;
using System;
using assignment_3;

namespace assignment_3.Tests
{
    [TestFixture]
    public class AdminTests
    {
        [Test]
        public void Admin_Creation_ShouldInitializeCorrectly()
        {
            // Arrange
            string name = "John Doe";

            // Act
            var admin = new Admin(name);

            // Assert
            Assert.IsNotNull(admin);
            Assert.AreEqual(1, admin.AdminID);
            Assert.AreEqual(name, admin.Name);
        }

        [Test]
        public void Admin_GenerateReport_ShouldReturnNewReport()
        {
            // Arrange
            var admin = new Admin("Jane Doe");

            // Act
            var report = admin.GenerateReport();

            // Assert
            Assert.IsNotNull(report);
            Assert.AreEqual(1, report.ReportId);
        }
    }
}
