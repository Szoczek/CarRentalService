using System;
using System.Drawing;

namespace Model
{
    public class Car
    {
        public Guid Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public BodyTypes BodyType { get; set; }
        public FuelTypes FuelType { get; set; }
        public double EngineVolume { get; set; }
        public int EngineHp { get; set; }
        public Color Color { get; set; }
        public int NumberOfDoors { get; set; }
        public string VinNumber { get; set; }
        public DateTime ProductionDate { get; set; }
        public double PriceNet { get; set; }
        public double PriceGross { get; set; }

        public bool isValid()
        {
            return !(string.IsNullOrEmpty(Brand)
                && string.IsNullOrEmpty(Model)
                && BodyType == default(BodyTypes)
                && FuelType == default(FuelTypes)
                && EngineVolume == default(double)
                && EngineHp == default(int));
        }
    }

    public enum BodyTypes
    {
        Hatchback = 1,
        Sedan = 2,
        Suv = 3,
        Crossover = 4,
        Coupe = 5,
        Convertible = 6,
        Mpv = 7
    }

    public enum FuelTypes
    {
        Gasoline = 1,
        GasolineLpg = 2,
        Diesel = 3,
        Electric = 4,
        Hybrid = 5
    }
}
