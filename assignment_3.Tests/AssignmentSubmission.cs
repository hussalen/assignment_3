using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
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
            new DateTime(2027, 10, 10),
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
            var ex = Assert.Throws<ValidationException>(() =>
            {
                Essay essay = new Essay(
                    "Essay Topic",
                    DateTime.Now.AddDays(7),
                    invalidMinWordCount,
                    invalidMaxWordCount
                );
            });

            // Assert that the exception message is as expected
            Assert.AreEqual(
                "Minimum word count cannot be greater than the maximum word count.",
                ex.Message
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
                new DateTime(2024, 12, 12),
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
                new DateTime(2024, 12, 12),
                ["test"]
            );

            assignment.SubmissionDate = new DateTime(2020, 11, 11);
            Assert.Throws<ArgumentException>(() => student.RemoveAssignmentSubmission(assignment));
        }

        [Test]
        public void WhenAssignmentAlreadyExists()
        {
            student.SubmitAssignment(coding, 100);

            var ex = Assert.Throws<ArgumentException>(() => student.SubmitAssignment(coding, 100));
            Assert.That(ex.Message, Is.EqualTo("Assignment already exists."));
        }

        [Test]
        public void WhenAssignmentAlreadySubmitted()
        {
            coding.SubmissionDate = DateTime.UtcNow;

            var ex = Assert.Throws<InvalidOperationException>(
                () => student.SubmitAssignment(coding, 100)
            );
            Assert.That(ex.Message, Is.EqualTo("Assignment already submitted, edit to modify"));
        }

        [Test]
        public void WhenDueDateIsPassed()
        {
            coding.DueDate = DateTime.UtcNow.AddDays(-1);

            var ex = Assert.Throws<InvalidOperationException>(
                () => student.SubmitAssignment(coding, 100)
            );
            Assert.That(ex.Message, Is.EqualTo("Cannot submit the assignment after the due date"));
        }
    }
}
