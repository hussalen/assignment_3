using System;
using System.Text.Json.Nodes;
using assignment_3;
using NUnit.Framework;

namespace assignment_3.Tests
{
    [TestFixture]
    public class AdminTests
    {
        private Admin _admin = new Admin("Test Admin");
        private JsonArray jsonArrayExample = new JsonArray
        {
            new
            {
                name = "1",
                test = "2",
                testt2 = "3"
            }
        };

        [Test]
        public void Admin_Creation_ShouldInitializeCorrectly()
        {
            // Arrange
            string name = "John Doe";

            // Act
            var admin = new Admin(name);

            // Assert
            Assert.IsNotNull(admin);
            Assert.That(admin.AdminID, Is.EqualTo(3));
            Assert.That(admin.Name, Is.EqualTo(name));
        }

        [Test]
        public void Admin_GenerateReport_ShouldReturnNewReport()
        {
            // Arrange
            var admin = new Admin("Jane Doe");

            // Act
            var report = admin.GenerateReport("test", ["testestsetes"]);

            // Assert
            Assert.IsNotNull(report);
            Assert.AreEqual(2, report.ReportId);
        }

        [Test]
        public void CreatingAdminWithEmptyNameThrowsError()
        {
            Assert.Throws<ArgumentException>(() => new Admin(string.Empty));
        }

        [Test]
        public void CreatingScheduleWithPastDateThrowsError()
        {
            Assert.Throws<ArgumentException>(
                () =>
                    _admin.CreateSchedule(
                        DateTime.Now.AddDays(-1),
                        TimeSpan.FromHours(10),
                        TimeSpan.FromHours(11)
                    )
            );
        }

        [Test]
        public void CreatingScheduleWithInvalidTimeRangeThrowsError()
        {
            Assert.Throws<ArgumentException>(
                () =>
                    _admin.CreateSchedule(
                        DateTime.Now,
                        TimeSpan.FromHours(11),
                        TimeSpan.FromHours(10)
                    )
            );
        }

        [Test]
        public void UpdatingScheduleWithInvalidIdThrowsError()
        {
            Assert.Throws<ArgumentException>(
                () =>
                    _admin.UpdateSchedule(
                        0,
                        DateTime.Now,
                        TimeSpan.FromHours(10),
                        TimeSpan.FromHours(11)
                    )
            );
        }

        [Test]
        public void UpdatingScheduleWithPastDateThrowsError()
        {
            Assert.Throws<ArgumentException>(
                () =>
                    _admin.UpdateSchedule(
                        1,
                        DateTime.Now.AddDays(-1),
                        TimeSpan.FromHours(10),
                        TimeSpan.FromHours(11)
                    )
            );
        }

        [Test]
        public void UpdatingScheduleWithInvalidTimeRangeThrowsError()
        {
            Assert.Throws<ArgumentException>(
                () =>
                    _admin.UpdateSchedule(
                        1,
                        DateTime.Now,
                        TimeSpan.FromHours(11),
                        TimeSpan.FromHours(10)
                    )
            );
        }

        [Test]
        public void DeletingScheduleWithInvalidIdThrowsError()
        {
            Assert.Throws<ArgumentException>(() => _admin.DeleteSchedule(0));
        }

        [Test]
        public void GeneratingReportWithNullTitleThrowsError()
        {
            Assert.Throws<ArgumentException>(() => _admin.GenerateReport("", jsonArrayExample));
        }

        [Test]
        public void GeneratingReportWithEmptyTitleThrowsError()
        {
            Assert.Throws<ArgumentException>(
                () => _admin.GenerateReport(string.Empty, jsonArrayExample)
            );
        }

        [Test]
        public void SettingReportContentToEmptyJsonArrayThrowsError()
        {
            var report = _admin.GenerateReport("Test Report", jsonArrayExample);
            Assert.Throws<ArgumentException>(() => report.Content = new JsonArray());
        }
    }
}
