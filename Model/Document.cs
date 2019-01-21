using System;
using System.Collections.Generic;

namespace Model
{
    public class Document
    {
        public Guid DocumentGuid { get; set; }
        public User User { get; set; }
        public DocumentTypes DocumentType { get; set; }
        public IEnumerable<Car> RentedCars { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public Document(User user, DocumentTypes documentType, IEnumerable<Car> rentedCars, DateTime startDate, DateTime endDate)
        {
            this.DocumentGuid = Guid.NewGuid();
            this.User = user;
            this.DocumentType = documentType;
            this.RentedCars = rentedCars;
            this.StartDate = startDate;
            this.EndDate = endDate;
        }
    }

    public enum DocumentTypes
    {
        Invoice = 1,
        Contract = 2
    }
}
