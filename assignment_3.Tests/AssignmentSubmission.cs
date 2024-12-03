using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using NUnit.Framework;

namespace assignment_3.Tests
{
    public class AssignmentSubmission
    {
        readonly Student student = new(ClassLevel.Freshman, 4.0f);
        readonly Student student2 = new(ClassLevel.Freshman, 4.0f);

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
