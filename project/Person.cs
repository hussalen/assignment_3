using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mail;
using System.Reflection.Emit;
using System.Threading.Tasks;

namespace assignment_3
{
    public enum PersonRole
    {
        Admin,
        Teacher,
        Student
    };

    public class Person(string name, string email, string[] addressLines, string password)
    {
        private string _name = !string.IsNullOrWhiteSpace(name)
            ? name
            : throw new ArgumentException("Name cannot be null or empty.");
        public string Name
        {
            get => _name;
            set => _name = value;
        }

        public MailAddress Email { get; set; } = new MailAddress(email);
        public string[] AddressLines { get; set; } = addressLines;
        public string Password { get; set; } = password;

        public Person ChangeRole(PersonRole newRole)
        {
            // From
            switch (this)
            {
                case Admin admin:
                    admin.ResetAdmin();
                    break;
                case Teacher teacher:
                    teacher.ResetTeacher();
                    break;
                case Student student:
                    student.ResetStudent();
                    break;
            }
            // To
            switch (newRole)
            {
                case PersonRole.Admin:
                    return new Admin(this);
                case PersonRole.Teacher:
                    return new Teacher(this);
                case PersonRole.Student:
                    return new Student(this, ClassLevel.Freshman);
            }
            return this;
        }
    }
}
