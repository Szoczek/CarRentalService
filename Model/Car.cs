using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace Model
{
    public class Car
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
        public bool IsRented { get; set; }

        public Car(string brand, string model, BodyTypes bodyType, EngineTypes engineType, double engineVolume, int engineHp,
            Color color, int numberOfDoors, double priceNet, double priceGross, DateTime productionDate, string vinNumber = "",
            bool isRented = false)
        {
            this.CarGuid = Guid.NewGuid();
            this.Brand = brand;
            this.Model = model;
            this.EngineVolume = engineVolume;
            this.BodyType = bodyType;
            this.EngineType = engineType;
            this.EngineHp = engineHp;
            this.Color = color;
            this.NumberOfDoors = numberOfDoors;
            this.PriceNet = priceNet;
            this.PriceGross = priceGross;
            this.ProductionDate = productionDate;
            this.VinNumber = vinNumber == "" ? Utils.CarUtils.GenerateVinNumber() : vinNumber;
            this.IsRented = isRented;
        }
    }

    public enum BodyTypes
    {
        Hatchback,
        Sedan,
        Suv,
        Crossover,
        Coupe,
        Convertible,
        Mpv
    }

    public enum EngineTypes
    {
        Gasoline,
        GasolineLpg,
        Diesel,
        Electric,
        Hybrid
    }
}
