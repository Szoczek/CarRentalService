using System;
using System.Collections.Generic;

namespace Model
{
    class Document
    {
        public int Oid { get; set; }
        public User User { get; set; }
        public DocumentTypes DocumentType { get; set; }
        public IEnumerable<Car> RentedCars { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public Document(int oid, User user, DocumentTypes documentType, IEnumerable<Car> rentedCars, DateTime startDate, DateTime endDate)
        {
            this.Oid = oid;
            this.User = user;
            this.DocumentType = documentType;
            this.RentedCars = rentedCars;
            this.StartDate = startDate;
            this.EndDate = endDate;
        }
    }

    public enum DocumentTypes
    {
        Invoice,
        Contract
    }
}
