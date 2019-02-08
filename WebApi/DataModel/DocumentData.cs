using System;
using System.Threading.Tasks;
using Model;

namespace WebApi.DataModel
{
    public class DocumentData
    {
        public Guid Id { get; set; }
        public User User { get; set; }
        public Car RentedCar { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }


        public DocumentData InitFrom(Task<Document> document)
        {
            this.Id = document.Result.Id;
            this.User = document.Result.User;
            this.RentedCar = document.Result.RentedCar; 
            this.StartDate = document.Result.StartDate;
            this.EndDate = document.Result.EndDate;

            return this;
        }

        public DocumentData InitFrom(Document document)
        {
            this.Id = document.Id;
            this.User = document.User;
            this.RentedCar = document.RentedCar;
            this.StartDate = document.StartDate;
            this.EndDate = document.EndDate;

            return this;
        }

        public Document CopyTo(Document document)
        {
            document.Id = this.Id;
            document.User = this.User;
            document.RentedCar = this.RentedCar;
            document.StartDate = this.StartDate;
            document.EndDate = this.EndDate;

            return document;
        }
    }
}
