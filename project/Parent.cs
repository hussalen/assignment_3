using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace assignment_3
{
    public class Parent
    {
        private const int MAX_NAME_LENGTH = 50;
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
            if (name.Length is 0 or > MAX_NAME_LENGTH)
                throw new ArgumentOutOfRangeException(
                    $"Name invalid, make sure it's non-empty and the number of characters does not exceed {MAX_NAME_LENGTH} characters"
                );
            return name;
        }
    }
}
