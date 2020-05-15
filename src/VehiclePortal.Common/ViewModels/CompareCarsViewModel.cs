using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using VehiclePortal.Models;

namespace VehiclePortal.Common.ViewModels
{
    public class CompareCarsViewModel
    {
        [Required]
        public Car FirstCar { get; set; }

        [Required]
        public Car SecondCar { get; set; }
    }
}
