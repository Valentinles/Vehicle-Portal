using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VehiclePortal.Common.ViewModels;
using VehiclePortal.Models;

namespace VehiclePortal.Services.Interfaces
{
    public interface ICarService
    {
        Task Add(CarBindingModel model);

        Task Edit(EditCarViewModel model);

        Task Delete(int id);

        Task<CarDetailsViewModel> Details(int id);

        Task<EditCarViewModel> GetById(int id);

        Task<IEnumerable<Car>> GetAll();

        Task<IEnumerable<Car>> GetAllByRating();

        Task<bool> Rate(RateCarBindingModel model);

        Task<bool> Buy(BuyCarBindingModel model, string username);

        Task<bool> Rent(RentCarBindingModel model, string username);

    }
}
