using System.ComponentModel.DataAnnotations;
using System.Net.Mail;

namespace assignment_3
{
    public class Parent
    {
        private int NAME_LENGTH = 50;
        public int ParentID { get; private set; }
        private string name;
        public string Name
        {
            get { return name; }
            set { name = ValidName(value); }
        }
        public MailAddress email { get; set; }

        static int nextId;

        public Parent(string name, string email)
        {
            ParentID = Interlocked.Increment(ref nextId);
            this.name = name;
            IsValidEmail(email);
            this.email = new MailAddress(email);
        }

        public void ViewStudentAttendance() { }

        private void IsValidEmail(string email)
        {
            try
            {
                new MailAddress(email);
            }
            catch (Exception e)
            {
                throw new ValidationException(
                    $"So-called 'email address' ({email}) is not valid. Error: {e.Message}"
                );
            }

            return;
        }

        private string ValidName(string name)
        {
            if (name.Length > 50)
                throw new ValidationException(
                    $"Make sure the number of characters for the name does not exceed {NAME_LENGTH}"
                );
            return name;
        }
    }
}
