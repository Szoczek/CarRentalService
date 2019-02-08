using System;
using System.Drawing;
using Model;

namespace WebApi.DataModel
{
    public class CarData
    {
        public Guid Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int BodyTypeId { get; set; }
        public int FuelTypeId { get; set; }
        public double EngineVolume { get; set; }
        public int EngineHp { get; set; }
        public Color CarColor { get; set; }
        public int NumberOfDoors { get; set; }
        public string VinNumber { get; set; }
        public DateTime ProductionDate { get; set; }
        public double PriceNet { get; set; }
        public double PriceGross { get; set; }

        public CarData InitFrom(Car car)
        {
            this.Id = car.Id;
            this.Brand = car.Brand;
            this.Model = car.Model;
            this.BodyTypeId = (int)car.BodyType;
            this.FuelTypeId = (int)car.FuelType;
            this.EngineVolume = car.EngineVolume;
            this.EngineHp = car.EngineHp;
            this.CarColor = car.Color;
            this.NumberOfDoors = car.NumberOfDoors;
            this.VinNumber = car.VinNumber;
            this.ProductionDate = car.ProductionDate;
            this.PriceNet = car.PriceNet;
            this.PriceGross = car.PriceGross;

            return this;
        }

        public Car CopyTo(Car car)
        {
            car.Id = this.Id;
            car.Brand = this.Brand;
            car.Model = this.Model;
            car.BodyType = (BodyTypes)this.BodyTypeId;
            car.FuelType = (FuelTypes)this.FuelTypeId;
            car.EngineVolume = this.EngineVolume;
            car.EngineHp = this.EngineHp;
            car.Color = this.CarColor;
            car.NumberOfDoors = this.NumberOfDoors;
            car.VinNumber = this.VinNumber;
            car.ProductionDate = this.ProductionDate;
            car.PriceNet = this.PriceNet;
            car.PriceGross = this.PriceGross;

            return car;
        }

    }
}
