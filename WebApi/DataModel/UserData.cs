using Model;
using System;

namespace WebApi.DataModel
{
    public class UserData
    {
        public string Guid { get; set; }
        public int Oid { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public int UserTypeOid { get; set; }
        public bool IsAdmin { get; set; }

        public UserData InitFrom(User user)
        {
            this.Guid = user.Guid.ToString();
            this.Oid = user.Oid;
            this.FirstName = user.FirstName;
            this.LastName = user.LastName;
            this.Age = user.Age;
            this.UserTypeOid = (int)user.UserType;
            this.IsAdmin = user.IsAdmin;

            return this;
        }

        public User CopyTo(User user)
        {
            user.Guid = System.Guid.Parse(this.Guid);
            user.Oid = this.Oid;
            user.FirstName = this.FirstName;
            user.LastName = this.LastName;
            user.Age = this.Age;
            user.UserType = (UserTypes)this.UserTypeOid;
            user.IsAdmin = this.IsAdmin;

            return user;
        }

    }
}
