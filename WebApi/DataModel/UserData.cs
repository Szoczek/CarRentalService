using Model;
using System;

namespace WebApi.DataModel
{
    public class UserData
    {
        public string Guid { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public int UserTypeId { get; set; }
        public bool IsAdmin { get; set; }

        public UserData InitFrom(User user)
        {
            this.Guid = user.UserGuid.ToString();
            this.FirstName = user.FirstName;
            this.LastName = user.LastName;
            this.Age = user.Age;
            this.UserTypeId = (int)user.UserType;
            this.IsAdmin = user.IsAdmin;

            return this;
        }

        public User CopyTo(User user)
        {
            user.UserGuid = System.Guid.Parse(this.Guid);
            user.FirstName = this.FirstName;
            user.LastName = this.LastName;
            user.Age = this.Age;
            user.UserType = (UserTypes)this.UserTypeId;
            user.IsAdmin = this.IsAdmin;

            return user;
        }

    }
}
