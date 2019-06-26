using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VehiclePortal.Common.ViewModels;
using VehiclePortal.Models;

namespace VehiclePortal.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<RentedCarsViewModel>> GetAllRentedCars();

        Task<IEnumerable<SoldCarsViewModel>> GetAllSoldCars();

        Task<IEnumerable<SoldCarsViewModel>> GetAllBoughtCarsByUser(string username);

        Task<IEnumerable<RentedCarsViewModel>> GetAllRentedCarsByUser(string username);

    }
}
