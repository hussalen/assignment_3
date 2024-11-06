using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using System.Text.RegularExpressions;

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
            this.name = ValidName(name);
            this.email = ValidEmail(email);
        }

        public void ViewStudentAttendance() { }

        private MailAddress ValidEmail(string email)
        {
            try
            {
                return new MailAddress(email);
            }
            catch (Exception e)
            {
                throw new ValidationException(
                    $"So-called 'email address' ({email}) is not valid. Error: {e.Message}"
                );
            }
        }

        private string ValidName(string name)
        {
            if (name.Length > 50)
                throw new ValidationException(
                    $"Make sure the number of characters for the name does not exceed {NAME_LENGTH} characters"
                );
            return name;
        }
    }
}
