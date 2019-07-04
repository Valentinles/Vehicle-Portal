using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using VehiclePortal.Models;

namespace VehiclePortal.Common.ViewModels
{
    public class AddFundsBindingModel
    {
        public string UserId { get; set; }

        [Required(ErrorMessage ="Enter correct value, please!")]
        [Range(1, double.MaxValue, ErrorMessage = "Cannot transfer negative values!")]
        public decimal Balance { get; set; }
    }
}
