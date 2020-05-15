using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VehiclePortal.Common.ServiceModels;
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
            this.CreateMap<BuyCar, BuyCarBindingModel>();
            this.CreateMap<RentCar, RentCarBindingModel>();
            this.CreateMap<AddFundsServiceModel, AddFundsBindingModel>();
            this.CreateMap<EditCarServiceModel, EditCarViewModel>();
            this.CreateMap<CompareCarsServiceModel, CompareCarsViewModel>();
        }
    }
}
