using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Model;

namespace WebApi.DataModel
{
    public class DocumentData
    {
        public int Oid { get; set; }
        public string DocumentGuid { get; set; }
        public int UserOid { get; set; }
        public int DocumentTypeOid { get; set; }
        public IEnumerable<int> RentedCarsOid { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }


        public DocumentData InitFrom(Document document)
        {
            this.Oid = document.Oid;
            this.DocumentGuid = document.DocumentGuid.ToString();
            this.UserOid = document.User.Oid;
            this.DocumentTypeOid = (int)document.DocumentType;
            foreach (var rentedCar in document.RentedCars)
            {
                RentedCarsOid.Append(rentedCar.Oid);
            }
            this.StartDate = document.StartDate;
            this.EndDate = document.EndDate;

            return this;
        }

        public Document CopyTo(Document document)
        {
            document.Oid = this.Oid;
            document.DocumentGuid = Guid.Parse(this.DocumentGuid);
            document.StartDate = this.StartDate;
            document.EndDate = this.EndDate;

            return document;
        }
    }
}
