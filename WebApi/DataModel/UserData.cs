using Model;
using System;
using System.Threading.Tasks;

namespace WebApi.DataModel
{
    public class UserData
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }

        public UserData InitFrom(User user)
        {
            this.Login = user.Login;
            this.Password = user.Password;
            this.FirstName = user.FirstName;
            this.LastName = user.LastName;
            this.Age = user.Age;

            return this;
        }

        public UserData InitFrom(Task<User> user)
        {
            this.Login = user.Result.Login;
            this.Password = user.Result.Password;
            this.FirstName = user.Result.FirstName;
            this.LastName = user.Result.LastName;
            this.Age = user.Result.Age;

            return this;
        }

        public User CopyTo(User user)
        {
            user.Login = this.Login;
            user.Password = this.Password;
            user.FirstName = this.FirstName;
            user.LastName = this.LastName;
            user.Age = this.Age;

            return user;
        }

    }
}
