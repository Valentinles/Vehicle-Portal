using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VehiclePortal.Common.ViewModels;
using VehiclePortal.Models;

namespace VehiclePortal.Common.Mapper
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            this.CreateMap<Car, CarBindingModel>().ReverseMap().ForMember(m=>m.Rating, opt=>opt.Ignore());
            this.CreateMap<Car, CarDetailsViewModel>();
            this.CreateMap<Car, RateCarBindingModel>().ReverseMap().ForMember(m => m.Id, opt => opt.Ignore())
                .ForMember(m => m.CarModel, opt => opt.Ignore())
                .ForMember(m => m.Brand, opt => opt.Ignore())
                .ForMember(m => m.Features, opt => opt.Ignore())
                .ForMember(m => m.Fuel, opt => opt.Ignore())
                .ForMember(m => m.Transmission, opt => opt.Ignore())
                .ForMember(m => m.Price, opt => opt.Ignore())
                .ForMember(m => m.SmallImageUrl, opt => opt.Ignore())
                .ForMember(m => m.LargeImageUrl, opt => opt.Ignore())
                .ForMember(m => m.Year, opt => opt.Ignore())
                .ForMember(m => m.Category, opt => opt.Ignore())
                .ForMember(m => m.Description, opt => opt.Ignore());



        }
    }
}
