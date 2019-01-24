using System;

namespace Model
{
    public class User
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public UserTypes UserType { get; set; }
        public bool IsAdmin { get; set; }

        public User() { }

        public User(Guid userId, string firstName, string lastName, int age,
             UserTypes userType = UserTypes.Individual, bool isAdmin = false)
        {
            this.Id = userId;
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
