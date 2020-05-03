using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace VehiclePortal.Models
{
    // Add profile data for application users by adding properties to the VehiclePortalUser class
    public class VehiclePortalUser : IdentityUser
    {
        public VehiclePortalUser()
        {
            this.OwnedCars = new List<BuyCar>();
            this.CarsUnderRent = new List<RentCar>();
        }

        public string VehiclePortalUsername { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public decimal Balance { get; set; }

        public bool IsRated { get; set; }

        public ICollection<BuyCar> OwnedCars { get; set; }

        public ICollection<RentCar> CarsUnderRent { get; set; }

    }
}
