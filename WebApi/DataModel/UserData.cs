using Model;
using System;

namespace WebApi.DataModel
{
    public class UserData
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public UserTypes UserType { get; set; }
        public bool IsAdmin { get; set; }

        public UserData InitFrom(User user)
        {
            this.Id = user.Id;
            this.FirstName = user.FirstName;
            this.LastName = user.LastName;
            this.Age = user.Age;
            this.UserType = user.UserType;
            this.IsAdmin = user.IsAdmin;

            return this;
        }

        public User CopyTo(User user)
        {
            user.Id = this.Id;
            user.FirstName = this.FirstName;
            user.LastName = this.LastName;
            user.Age = this.Age;
            user.UserType = this.UserType;
            user.IsAdmin = this.IsAdmin;

            return user;
        }

    }
}
