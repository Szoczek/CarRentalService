using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Model;

namespace WebApi.DataModel
{
    public class DocumentData
    {
        public string DocumentGuid { get; set; }
        public int UserId { get; set; }
        public int DocumentTypeOid { get; set; }
        public IEnumerable<int> RentedCarsId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }


        public DocumentData InitFrom(Document document)
        {
            this.DocumentGuid = document.DocumentGuid.ToString();
            this.UserId = document.User.Id;
            this.DocumentTypeOid = (int)document.DocumentType;
            foreach (var rentedCar in document.RentedCars)
            {
                RentedCarsId.Append(rentedCar.Id);
            }
            this.StartDate = document.StartDate;
            this.EndDate = document.EndDate;

            return this;
        }

        public Document CopyTo(Document document)
        {
            document.DocumentGuid = Guid.Parse(this.DocumentGuid);
            document.StartDate = this.StartDate;
            document.EndDate = this.EndDate;

            return document;
        }
    }
}
