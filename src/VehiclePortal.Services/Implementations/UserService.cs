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

namespace VehiclePortal.Services.Implementations
{
    public class UserService : DataService, IUserService
    {
        public UserService(VehiclePortalDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<RentedCarsViewModel>> GetAllRentedCars()
        {
            var rentedCars = await(from rc in context.RentedCars
                                 join c in context.Cars
                                 on rc.CarId equals c.Id
                                 select new RentedCarsViewModel
                                 {
                                     CarId = c.Id,
                                     Car = c.CarModel,
                                     User = rc.User.UserName,
                                     StartDate = rc.StartDate,
                                     EndDate=rc.EndDate,
                                     TotalPrice = rc.TotalPrice
                                 }).ToArrayAsync();

            return rentedCars;
        }

        public async Task<IEnumerable<SoldCarsViewModel>> GetAllSoldCars()
        {
            var soldCars = await ( from sc in context.BoughtCars
                            join c in context.Cars
                            on sc.CarId equals c.Id
                            select new SoldCarsViewModel
                            {
                                CarId = c.Id,
                                Car = c.CarModel,
                                User = sc.User.UserName,
                                BoughtOn=sc.BoughtOn,
                                Price=sc.Price
                            }).ToArrayAsync();

            return soldCars;
        }

        public async Task<IEnumerable<SoldCarsViewModel>> GetAllBoughtCarsByUser(string username)
        {
            var boughtCarsByUser = await (from sc in context.BoughtCars
                                  join c in context.Cars
                                  on sc.CarId equals c.Id
                                  where sc.User.UserName == username
                                  select new SoldCarsViewModel
                                  {
                                      CarId = c.Id,
                                      Car = c.CarModel,
                                      BoughtOn = sc.BoughtOn,
                                      Price = sc.Price
                                  }).ToArrayAsync();

            return boughtCarsByUser;
        }

        public async Task<IEnumerable<RentedCarsViewModel>> GetAllRentedCarsByUser(string username)
        {
            var rentedCarsByUser = await (from rc in context.RentedCars
                                    join c in context.Cars
                                    on rc.CarId equals c.Id
                                    where rc.User.UserName==username
                                    select new RentedCarsViewModel
                                    {
                                        CarId = c.Id,
                                        Car = c.CarModel,
                                        StartDate = rc.StartDate,
                                        EndDate = rc.EndDate,
                                        TotalPrice = rc.TotalPrice
                                    }).ToArrayAsync();

            return rentedCarsByUser;
        } 
    }
}
