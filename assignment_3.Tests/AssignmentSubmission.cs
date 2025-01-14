using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using NUnit.Framework;

namespace assignment_3.Tests
{
    public class AssignmentSubmission
    {
        private readonly Student student = new(ClassLevel.Freshman);
        private readonly Student student2 = new(ClassLevel.Freshman);

        private readonly IAssignment coding = new Coding(
            "test",
            DateTime.MaxValue,
            "Python",
            "https://yes.com"
        );

        [Test]
        public void Submit_Essay_WithInvalidWordCount_Fails()
        {
            // Arrange
            uint invalidMinWordCount = 500;
            uint invalidMaxWordCount = 300; // Invalid range

            // Act & Assert
            Assert.Throws<ValidationException>(
                () =>
                    new Essay(
                        "Essay Topic",
                        DateTime.Now.AddDays(7),
                        invalidMinWordCount,
                        invalidMaxWordCount
                    )
            );
        }

        [Test]
        public void EssayConstructor_InvalidWordCount_ThrowsValidationException()
        {
            // Arrange
            uint validMinWordCount = 300;
            uint validMaxWordCount = 500;
            Essay essay = new Essay(
                "Valid Essay Topic",
                DateTime.Now.AddDays(7),
                validMinWordCount,
                validMaxWordCount
            );

            var ex = Assert.Throws<ValidationException>(() => essay.WordCount = 250); // Below MinWordCount
            Assert.That(
                ex.Message,
                Is.EqualTo(
                    $"Word count must be between {validMinWordCount} and {validMaxWordCount}."
                )
            );

            ex = Assert.Throws<ValidationException>(() => essay.WordCount = 600); // Above MaxWordCount
            Assert.That(
                ex.Message,
                Is.EqualTo(
                    $"Word count must be between {validMinWordCount} and {validMaxWordCount}."
                )
            );
        }

        [Test]
        public void IndividualProjectSubmit_ValidSubmission_SetsSubmissionDate()
        {
            // Arrange
            IAssignment assignment = new IndividualProject(
                "Test Project",
                DateTime.Now.AddDays(7),
                ["test"]
            );

            // Act
            assignment.SubmissionDate = DateTime.Now;

            // Assert
            Assert.IsNotNull(assignment.SubmissionDate);
            Assert.That(assignment.SubmissionDate.Value, Is.LessThanOrEqualTo(DateTime.Now)); // Submission date cannot be in the future
        }

        [Test]
        public void RemoveAssignmentSubmission_AssignmentHasNoDate()
        {
            IAssignment assignment = new IndividualProject(
                "testtestest",
                DateTime.MaxValue,
                ["test"]
            );
            // Explicitly setting to null despite it already being null
            assignment.SubmissionDate = null;
            Assert.Throws<ArgumentNullException>(
                () => student.RemoveAssignmentSubmission(assignment)
            );
        }

        [Test]
        public void RemoveAssignmentSubmission_AssignmentNotInList()
        {
            IAssignment assignment = new IndividualProject(
                "testtestest",
                DateTime.MaxValue,
                ["test"]
            );

            assignment.SubmissionDate = new DateTime(2020, 11, 11);
            Assert.Throws<ArgumentException>(() => student.RemoveAssignmentSubmission(assignment));
        }

        [Test]
        public void WhenAssignmentAlreadyExists()
        {
            student.SubmitAssignment(coding);

            Assert.Throws<ArgumentException>(() => student.SubmitAssignment(coding));
        }

        [Test]
        public void WhenAssignmentAlreadySubmitted()
        {
            var coding = new Coding(
                "testtest",
                DateTime.MaxValue,
                "python",
                "https://ThreadStart.com"
            );
            coding.SubmissionDate = DateTime.UtcNow;

            var ex = Assert.Throws<InvalidOperationException>(
                () => student.SubmitAssignment(coding)
            );
            Assert.That(ex.Message, Is.EqualTo("Assignment already submitted, edit to modify"));
        }

        [Test]
        public void WhenDueDateIsPassed()
        {
            var coding = new Coding(
                "test",
                DateTime.UtcNow.AddMilliseconds(1),
                "python",
                "https://ThreadStart.com"
            );
            var ex = Assert.Throws<InvalidOperationException>(() =>
            {
                Thread.Sleep(101);
                student.SubmitAssignment(coding);
            });
            Assert.That(ex.Message, Is.EqualTo("Cannot submit the assignment after the due date"));
        }
    }
}
