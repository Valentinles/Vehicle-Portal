using System;
using System.Collections.Generic;
using System.Text;
using VehiclePortal.Models;

namespace VehiclePortal.Common.ViewModels
{
    public class BuyCarBindingModel
    {
        public int BuyCarId { get; set; }

        public int CarId { get; set; }

        public Car Car { get; set; }

        public string UserId { get; set; }

        public VehiclePortalUser User { get; set; }

        public decimal Price { get; set; }

        public DateTime BoughtOn { get; set; }
    }
}
