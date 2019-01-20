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
        public bool IsAdmin { get; set; }

        public User() { }

        public User(int oid, string firstName, string lastName, int age,
            UserTypes userType = UserTypes.Individual, bool isAdmin = false)
        {
            this.Guid = Guid.NewGuid();
            this.Oid = oid;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Age = age;
            this.UserType = userType;
            this.IsAdmin = isAdmin;
        }
    }

    public enum UserTypes
    {
        Individual = 1,
        Business = 2
    }
}
