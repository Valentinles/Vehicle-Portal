using System;
using System.Collections.Generic;
using System.Text;
using VehiclePortal.Models;

namespace VehiclePortal.Common.ServiceModels
{
    public class RateCarServiceModel
    {
        public int CarId { get; set; }

        public int Rating { get; set; }

        public string UserId { get; set; }

        public VehiclePortalUser User { get; set; }
    }
}
