using System;
using System.Threading.Tasks;

namespace Model
{
    public class User
    {
        public Guid Id{ get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }

        public bool IsValid()
        {
            return !(string.IsNullOrEmpty(Login)
                && string.IsNullOrEmpty(Password)
                && string.IsNullOrEmpty(FirstName));
        }
    }
}
