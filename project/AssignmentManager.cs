using System;
using System.Collections.Generic;
using System.Linq;

namespace assignment_3
{
	public class AssignmentManager
	{
		private readonly List<IAssignment> _assignments = new();

		
		public void AddAssignment(IAssignment assignment)
		{
			if (assignment == null)
			{
				throw new ArgumentNullException(nameof(assignment), "Assignment cannot be null.");
			}

			_assignments.Add(assignment);
			Console.WriteLine($"Assignment '{assignment.Topic}' added successfully.");
		}

		
		public void SubmitAssignment(int assignmentId, DateTime submissionDate, Student student)
		{
			var assignment = _assignments.FirstOrDefault(a => a.AssignmentID == assignmentId);
			if (assignment == null)
			{
				throw new ArgumentException("Assignment not found.");
			}

			assignment.Submit(submissionDate, student);
			Console.WriteLine($"Assignment '{assignment.Topic}' submitted by {student.Name}.");
		}

		
		public void ListAssignments()
		{
			if (_assignments.Count == 0)
			{
				Console.WriteLine("No assignments available.");
				return;
			}

			Console.WriteLine("List of Assignments:");
			foreach (var assignment in _assignments)
			{
				Console.WriteLine($"ID: {assignment.AssignmentID}, Topic: {assignment.Topic}, DueDate: {assignment.DueDate}");
			}
		}

		
		public List<IAssignment> FindAssignmentsByTopic(string topic)
		{
			return _assignments.Where(a => a.Topic.Contains(topic, StringComparison.OrdinalIgnoreCase)).ToList();
		}
	}
}
