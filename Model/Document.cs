using System;

namespace Model
{
    public class Document
    {
        public Guid Id { get; set; }
        public User User { get; set; }
        public Car RentedCar { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public bool isValid()
        {
            return User.IsValid() 
                && RentedCar.isValid() 
                && StartDate != default(DateTime) 
                && EndDate != default(DateTime);
        }
    }
}
