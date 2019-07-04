using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using VehiclePortal.Models;

namespace VehiclePortal.Common.ViewModels
{
    public class RentCarBindingModel
    {
        public int RentCarId { get; set; }

        public string UserId { get; set; }

        public VehiclePortalUser User { get; set; }

        public int CarId { get; set; }

        public Car Car { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        public decimal TotalPrice { get; set; }
    }
}
