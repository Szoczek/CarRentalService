using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Model;

namespace WebApi.DataModel
{
    public class DocumentData
    {
        public Guid Id { get; set; }
        public User User { get; set; }
        public DocumentTypes DocumentType { get; set; }
        public IEnumerable<Car> RentedCars { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }


        public DocumentData InitFrom(Task<Document> document)
        {
            this.Id = document.Result.Id;
            this.User = document.Result.User;
            this.DocumentType = document.Result.DocumentType;
            this.RentedCars = document.Result.RentedCars; 
            this.StartDate = document.Result.StartDate;
            this.EndDate = document.Result.EndDate;

            return this;
        }

        public DocumentData InitFrom(Document document)
        {
            this.Id = document.Id;
            this.User = document.User;
            this.DocumentType = document.DocumentType;
            this.RentedCars = document.RentedCars;
            this.StartDate = document.StartDate;
            this.EndDate = document.EndDate;

            return this;
        }

        public Document CopyTo(Document document)
        {
            document.Id = this.Id;
            document.User = this.User;
            document.DocumentType = this.DocumentType;
            document.RentedCars = this.RentedCars;
            document.StartDate = this.StartDate;
            document.EndDate = this.EndDate;

            return document;
        }
    }
}
