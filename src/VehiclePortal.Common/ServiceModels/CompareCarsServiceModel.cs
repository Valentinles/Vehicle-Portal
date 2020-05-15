using System;
using System.Collections.Generic;
using System.Text;
using VehiclePortal.Models;

namespace VehiclePortal.Common.ServiceModels
{
    public class CompareCarsServiceModel
    {
        public Car FirstCar { get; set; }

        public Car SecondCar { get; set; }
    }
}
