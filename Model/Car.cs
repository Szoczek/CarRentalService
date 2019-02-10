using System;
using System.Drawing;

namespace Model
{
    public class Car
    {
        public Guid Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
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
                && EngineVolume == default(double)
                && EngineHp == default(int));
        }
    }
}
