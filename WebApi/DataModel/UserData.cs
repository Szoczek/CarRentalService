using Model;
using System;

namespace WebApi.DataModel
{
    public class UserData
    {
        public Guid Guid { get; set; }
        public int Oid { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public UserTypes UserType { get; set; }

        public UserData InitFrom(User user)
        {
            this.Guid = user.Guid;
            this.Oid = user.Oid;
            this.FirstName = user.FirstName;
            this.LastName = user.LastName;
            this.Age = user.Age;
            this.UserType = user.UserType;

            return this;
        }

        public User CopyTo(User user)
        {
            user.Guid = this.Guid;
            user.Oid = this.Oid;
            user.FirstName = this.FirstName;
            user.LastName = this.LastName;
            user.Age = this.Age;
            user.UserType = this.UserType;
            return user;
        }

    }
}
