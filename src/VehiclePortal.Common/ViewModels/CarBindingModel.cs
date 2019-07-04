using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using VehiclePortal.Models.Enums;

namespace VehiclePortal.Common.ViewModels
{
    public class CarBindingModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Enter the brand, please!")]
        [StringLength(30, ErrorMessage =("The length mst be between {2} and {1}"), MinimumLength =3)]
        public string Brand { get; set; }

        [Required(ErrorMessage = "Enter the model, please!")]
        [StringLength(25, ErrorMessage = ("The length mst be between {2} and {1}"), MinimumLength = 1)]
        public string CarModel { get; set; }

        [Required(ErrorMessage = "Enter the year, please!")]
        [Range(1950, 2025, ErrorMessage ="Invalid year")]
        public int? Year { get; set; }

        [Required(ErrorMessage = "Enter some features, please!")]
        public string Features { get; set; }

        [Required(ErrorMessage = "Enter a description, please!")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Select the category, please!")]
        [EnumDataType(typeof(Category))]
        public Category? Category { get; set; }

        [Required(ErrorMessage = "Select the fuel type, please!")]
        [EnumDataType(typeof(Fuel))]
        public Fuel? Fuel { get; set; }

        [Required(ErrorMessage = "Select the transmission, please!")]
        [EnumDataType(typeof(Transmission))]
        public Transmission? Transmission { get; set; }

        [Required(ErrorMessage = "Enter the url, please!")]
        [Url]
        public string SmallImageUrl { get; set; }

        [Required(ErrorMessage = "Enter the url, please!")]
        [Url]
        public string LargeImageUrl { get; set; }

        [Required(ErrorMessage = "Enter price, please!")]
        [Range(1, double.MaxValue, ErrorMessage = "Cannot enter negative values!")]
        public decimal? Price { get; set; }

        [Required(ErrorMessage ="Enter rent price,please!")]
        [Range(1, double.MaxValue, ErrorMessage ="Cannot enter negative values!")]
        public decimal? RentPricePerDay { get; set; }
    }
}
