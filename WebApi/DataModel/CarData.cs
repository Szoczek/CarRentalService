using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Model;

namespace WebApi.DataModel
{
    public class CarData
    {
        public int Oid { get; set; }
        public Guid CarGuid { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public BodyTypes BodyType { get; set; }
        public EngineTypes EngineType { get; set; }
        public double EngineVolume { get; set; }
        public int EngineHp { get; set; }
        public Color Color { get; set; }
        public int NumberOfDoors { get; set; }
        public string VinNumber { get; set; }
        public DateTime ProductionDate { get; set; }
        public double PriceNet { get; set; }
        public double PriceGross { get; set; }

        public CarData InitFrom(Car car)
        {
            this.Oid = car.Oid;
            this.CarGuid = car.CarGuid;
            this.Brand = car.Brand;
            this.Model = car.Model;
            this.BodyType = car.BodyType;
            this.EngineType = car.EngineType;
            this.EngineVolume = car.EngineVolume;
            this.EngineHp = car.EngineHp;
            this.Color = car.Color;
            this.NumberOfDoors = car.NumberOfDoors;
            this.VinNumber = car.VinNumber;
            this.ProductionDate = car.ProductionDate;
            this.PriceNet = car.PriceNet;
            this.PriceGross = car.PriceGross;

            return this;
        }

        public Car CopyTo(Car car)
        {
            car.Oid = this.Oid;
            car.CarGuid = this.CarGuid;
            car.Brand = this.Brand;
            car.Model = this.Model;
            car.BodyType = this.BodyType;
            car.EngineType = this.EngineType;
            car.EngineVolume = this.EngineVolume;
            car.EngineHp = this.EngineHp;
            car.Color = this.Color;
            car.NumberOfDoors = this.NumberOfDoors;
            car.VinNumber = this.VinNumber;
            car.ProductionDate = this.ProductionDate;
            car.PriceNet = this.PriceNet;
            car.PriceGross = this.PriceGross;

            return car;
        }

    }
}
