using System;
using System.Collections.Generic;
using System.Text;

namespace VehiclePortal.Models
{
    public class RentCar
    {
        public int RentCarId { get; set; }

        public string UserId { get; set; }

        public VehiclePortalUser User { get; set; }

        public int CarId { get; set; }

        public Car Car { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public decimal TotalPrice { get; set; }
    }
}
