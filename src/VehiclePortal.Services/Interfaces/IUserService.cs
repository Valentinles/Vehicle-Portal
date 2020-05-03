using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VehiclePortal.Common.ServiceModels;
using VehiclePortal.Common.ViewModels;
using VehiclePortal.Models;

namespace VehiclePortal.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<RentCar>> GetAllRentedCars();

        Task<IEnumerable<BuyCar>> GetAllSoldCars();

        Task<IEnumerable<BuyCar>> GetAllBoughtCarsByUser(string username);

        Task<IEnumerable<RentCar>> GetAllRentedCarsByUser(string username);

        Task AddFunds(AddFundsServiceModel model, string username);

    }
}
