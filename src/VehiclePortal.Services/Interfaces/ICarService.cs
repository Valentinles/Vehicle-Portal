using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VehiclePortal.Common.ServiceModels;
using VehiclePortal.Common.ViewModels;
using VehiclePortal.Models;

namespace VehiclePortal.Services.Interfaces
{
    public interface ICarService
    {
        Task Add(Car car);

        Task Edit(EditCarServiceModel model);

        Task Delete(int id);

        Task<Car> Details(int id);

        Task<Car> GetById(int id);

        Task<IEnumerable<Car>> GetAll();

        Task<IEnumerable<Car>> GetAllByRating();

        Task<bool> Rate(RateCarServiceModel model, string username);

        Task<bool> Buy(BuyCar buyCar, string username);

        Task<bool> Rent(RentCar rentCar, string username);

        Task<CompareCarsServiceModel> CompareCars(int firstCarId, int secondCarId);
    }
}
