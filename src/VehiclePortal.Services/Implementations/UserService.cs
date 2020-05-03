using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VehiclePortal.Data;
using VehiclePortal.Models;
using System.Linq;
using VehiclePortal.Services.Interfaces;
using VehiclePortal.Common.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using VehiclePortal.Common.ServiceModels;

namespace VehiclePortal.Services.Implementations
{
    public class UserService : DataService, IUserService
    {
        public UserService(VehiclePortalDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<RentCar>> GetAllRentedCars()
        {
            var rentedCars = await this.context.RentedCars.Include(c => c.Car).Include(rc=>rc.User).ToListAsync();

            return rentedCars;
        }

        public async Task<IEnumerable<BuyCar>> GetAllSoldCars()
        {
            var soldCars = await this.context.BoughtCars.Include(c => c.Car).Include(rc => rc.User).ToListAsync();

            return soldCars;
        }

        public async Task<IEnumerable<BuyCar>> GetAllBoughtCarsByUser(string username)
        {
            var boughtCarsByUser = await this.context.BoughtCars
                .Include(c => c.Car)
                .Where(u => u.User.UserName == username)
                .ToListAsync();

            return boughtCarsByUser;
        }

        public async Task<IEnumerable<RentCar>> GetAllRentedCarsByUser(string username)
        {
            var rentedCarsByUser = await this.context.RentedCars
                .Include(c => c.Car)
                .Where(u => u.User.UserName == username)
                .ToListAsync();
     
            return rentedCarsByUser;
        }

        public async Task AddFunds(AddFundsServiceModel model, string username)
        {
            var user = await this.context.Users.SingleOrDefaultAsync(u => u.UserName == username);

            user.Balance += model.Balance;

            this.context.Update(user);
            await this.context.SaveChangesAsync();
        }
    }
}
