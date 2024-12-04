using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using NUnit.Framework;
using System.ComponentModel.DataAnnotations;


namespace assignment_3.Tests
{
    public class AssignmentSubmission
    {
        readonly Student student = new(ClassLevel.Freshman, 4.0f);
        readonly Student student2 = new(ClassLevel.Freshman, 4.0f);

        [Test]
        public void Submit_Essay_WithInvalidWordCount_Fails()
        {
            // Arrange
            uint invalidMinWordCount = 500;
            uint invalidMaxWordCount = 300; // Invalid range

            // Act & Assert
            var ex = Assert.Throws<ValidationException>(() =>
            {
                Essay essay = new Essay("Essay Topic", DateTime.Now.AddDays(7), invalidMinWordCount, invalidMaxWordCount);
            });

            // Assert that the exception message is as expected
            Assert.AreEqual("Minimum word count cannot be greater than the maximum word count.", ex.Message);
        }
        [Test]
        public void EssayConstructor_InvalidWordCount_ThrowsValidationException()
        {
            // Arrange
            uint validMinWordCount = 300;
            uint validMaxWordCount = 500;
            Essay essay = new Essay("Valid Essay Topic", DateTime.Now.AddDays(7), validMinWordCount, validMaxWordCount);
            
            var ex = Assert.Throws<ValidationException>(() => essay.WordCount = 250); // Below MinWordCount
            Assert.AreEqual($"Word count must be between {validMinWordCount} and {validMaxWordCount}.", ex.Message);

            ex = Assert.Throws<ValidationException>(() => essay.WordCount = 600); // Above MaxWordCount
            Assert.AreEqual($"Word count must be between {validMinWordCount} and {validMaxWordCount}.", ex.Message);
        }
        [Test]
        public void IndividualProjectSubmit_ValidSubmission_SetsSubmissionDate()
        {
            // Arrange
            IAssignment assignment = new IndividualProject("Test Project", DateTime.Now.AddDays(7));

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
                new DateTime(2024, 12, 12)
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
                new DateTime(2024, 12, 12)
            );

            assignment.SubmissionDate = new DateTime(2020, 11, 11);
            Assert.Throws<ArgumentException>(() => student.RemoveAssignmentSubmission(assignment));
        }
    }
}
