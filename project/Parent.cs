using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using System.Threading;

namespace assignment_3
{
    public class Parent
    {
        private const int MAX_NAME_LENGTH = 50;

        public int ParentID { get; private set; }

        private string _name;
        public string Name
        {
            get => _name;
            set => _name = ValidateName(value);
        }

        private MailAddress _email;
        public MailAddress Email
        {
            get => _email;
            set => _email = ValidateEmail(value.Address);
        }

        private static int nextId;
        private static readonly List<Parent> _parentList = new();

        public Parent(string name, string email)
        {
            ParentID = Interlocked.Increment(ref nextId);
            Name = name; // Validates via setter
            Email = ValidateEmail(email);
            AddParent(this);
        }

        private static void AddParent(Parent parent)
        {
            if (parent == null)
                throw new ArgumentException($"{nameof(parent)} cannot be null.");

            if (_parentList.Exists(p => p.Email.Address == parent.Email.Address))
                throw new ArgumentException($"A parent with the email {parent.Email.Address} already exists.");

            _parentList.Add(parent);
        }

        public static List<Parent> GetParentExtent() => new List<Parent>(_parentList);

        private string ValidateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name) || name.Length > MAX_NAME_LENGTH)
            {
                throw new ArgumentException(
                    $"Name must be non-empty and no longer than {MAX_NAME_LENGTH} characters.");
            }
            return name;
        }

        private MailAddress ValidateEmail(string email)
        {
            try
            {
                return new MailAddress(email);
            }
            catch (Exception ex)
            {
                throw new ValidationException($"Invalid email address '{email}'. Error: {ex.Message}");
            }
        }

        public void ViewStudentAttendance()
        {
            Console.WriteLine($"Parent {Name} with Email {Email.Address} is viewing student attendance.");
            // Logic for viewing student attendance goes here
        }
    }
}
