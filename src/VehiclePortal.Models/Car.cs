using System;
using System.Collections.Generic;
using System.Text;
using VehiclePortal.Models.Enums;

namespace VehiclePortal.Models
{
    public class Car
    {
        public Car()
        {
            this.Owners = new List<BuyCar>();
            this.Renters = new List<RentCar>();
        }

        public int Id { get; set; }

        public string Brand { get; set; }

        public string CarModel { get; set; }

        public int Year { get; set; }

        public string Features { get; set; }

        public string Description { get; set; }

        public Category Category { get; set; }

        public Fuel Fuel { get; set; }

        public Transmission Transmission { get; set; }

        public string SmallImageUrl { get; set; }

        public string LargeImageUrl { get; set; }

        public decimal RentPricePerDay { get; set; }

        public decimal Price { get; set; }

        public int Rating { get; set; }

        public ICollection<BuyCar> Owners { get; set; }

        public ICollection <RentCar> Renters { get; set; }
    }
}
