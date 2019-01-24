using System;
using System.Drawing;
using Model;

namespace WebApi.DataModel
{
    public class CarData
    {
        public Guid CarGuid { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int BodyTypeId { get; set; }
        public int EngineTypeId { get; set; }
        public double EngineVolume { get; set; }
        public int EngineHp { get; set; }
        public Color CarColor { get; set; }
        public int NumberOfDoors { get; set; }
        public string VinNumber { get; set; }
        public DateTime ProductionDate { get; set; }
        public double PriceNet { get; set; }
        public double PriceGross { get; set; }
        public bool IsRented { get; set; }

        public CarData InitFrom(Car car)
        {
            this.CarGuid = car.CarGuid;
            this.Brand = car.Brand;
            this.Model = car.Model;
            this.BodyTypeId = (int)car.BodyType;
            this.EngineTypeId = (int)car.EngineType;
            this.EngineVolume = car.EngineVolume;
            this.EngineHp = car.EngineHp;
            this.CarColor = car.Color;
            this.NumberOfDoors = car.NumberOfDoors;
            this.VinNumber = car.VinNumber;
            this.ProductionDate = car.ProductionDate;
            this.PriceNet = car.PriceNet;
            this.PriceGross = car.PriceGross;
            this.IsRented = car.IsRented;

            return this;
        }

        public Car CopyTo(Car car)
        {
            car.CarGuid = this.CarGuid;
            car.Brand = this.Brand;
            car.Model = this.Model;
            car.BodyType = (BodyTypes)this.BodyTypeId;
            car.EngineType = (EngineTypes)this.EngineTypeId;
            car.EngineVolume = this.EngineVolume;
            car.EngineHp = this.EngineHp;
            car.Color = this.CarColor;
            car.NumberOfDoors = this.NumberOfDoors;
            car.VinNumber = this.VinNumber;
            car.ProductionDate = this.ProductionDate;
            car.PriceNet = this.PriceNet;
            car.PriceGross = this.PriceGross;
            car.IsRented = this.IsRented;

            return car;
        }

    }
}
