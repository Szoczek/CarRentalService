using System;

namespace Model
{
    public class User
    {
        public Guid Guid { get; set; }
        public int Oid { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public UserTypes UserType { get; set; }

        public User() { };
        public User(int oid, string firstName, string lastName, int age, UserTypes userType)
        {
            this.Guid = Guid.NewGuid();
            this.Oid = oid;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Age = age;
            this.UserType = userType;
        }
    }

    public enum UserTypes
    {
        Indyvidual,
        Business
    }
}
