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
        public string VehiclePortalUsername { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }
    }
}
