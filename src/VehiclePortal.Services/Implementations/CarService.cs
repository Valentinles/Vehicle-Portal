using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehiclePortal.Common.ServiceModels;
using VehiclePortal.Data;
using VehiclePortal.Models;
using VehiclePortal.Services.Interfaces;

namespace VehiclePortal.Services.Implementations
{
    public class CarService : DataService, ICarService
    {
        public CarService(VehiclePortalDbContext context) : base(context)
        {
        }

        public async Task Add(Car car)
        {          
            await this.context.AddAsync(car);
            await this.context.SaveChangesAsync();
        }

        public async Task Edit(EditCarServiceModel model)
        {
            var car = await this.context.Cars.FirstOrDefaultAsync(c => c.Id == model.Id);

            if (car == null)
            {
                return;
            }

            car.Brand = model.Brand;
            car.CarModel = model.CarModel;
            car.Category = model.Category;
            car.Description = model.Description;
            car.Features = model.Features;
            car.Fuel = model.Fuel;
            car.LargeImageUrl = model.LargeImageUrl;
            car.SmallImageUrl = model.SmallImageUrl;
            car.Price = model.Price;
            car.Transmission = model.Transmission;
            car.Year = model.Year;

            this.context.Cars.Update(car);
            await this.context.SaveChangesAsync();

        }

        public async Task Delete(int id)
        {
            var car = await this.context.Cars.FirstOrDefaultAsync(c => c.Id == id);

            if (car == null)
            {
                return;
            }

            this.context.Cars.Remove(car);
            await this.context.SaveChangesAsync();
        }

        public async Task<Car> GetById(int id)
        {
            var car = await this.context.Cars.FirstOrDefaultAsync(c => c.Id == id);

            return car;
        }

        public async Task<Car> Details(int id)
        {
            var car = await this.context.Cars.FirstOrDefaultAsync(c => c.Id == id);

            return car;
        }

        public async Task<IEnumerable<Car>> GetAll()
        {
            var car = await this.context.Cars.OrderBy(x=>x.Price).ToArrayAsync();

            return car;
        }

        public async Task<bool> Rate(RateCarServiceModel model, string username)
        {
            var car = await this.context.Cars.SingleOrDefaultAsync(c => c.Id == model.CarId);

            var user = await this.context.Users.FirstOrDefaultAsync(u => u.UserName == username);

            if (car == null || user == null || model.Rating != 1)
            {
                return false;
            }

            car.Rating+=model.Rating;

            user.IsRated = true;

            this.context.Cars.Update(car);

            await this.context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Buy(BuyCar buyCar, string username)
        {
            var user = await this.context.Users.SingleOrDefaultAsync(u => u.UserName == username);

            var car = await this.context.Cars.SingleOrDefaultAsync(c => c.Id == buyCar.CarId);

            if (user == null || car == null || user.Balance<car.Price) 
            {
                return false;
            }

            buyCar.User = user;
            buyCar.BoughtOn = DateTime.Now;
            buyCar.Price = car.Price;

            user.Balance -= car.Price;

            this.context.BoughtCars.Add(buyCar);

            await this.context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Rent(RentCar rentCar, string username)
        {
            var user = await this.context.Users.SingleOrDefaultAsync(u => u.UserName == username);

            var car = await this.context.Cars.SingleOrDefaultAsync(c => c.Id == rentCar.CarId);

            if (user == null || car == null) 
            {
                return false;
            }

            rentCar.StartDate = DateTime.Now;
            rentCar.User = user;

            var totalDays = (rentCar.EndDate - rentCar.StartDate).Days + 1;

            rentCar.TotalPrice = totalDays * car.RentPricePerDay;

            user.Balance -= rentCar.TotalPrice;

            if (user.Balance < rentCar.TotalPrice) 
            {
                return false;
            }

            this.context.RentedCars.Add(rentCar);

            await this.context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<Car>> GetAllByRating()
        {
            var carsByRating = await this.context.Cars.
                OrderByDescending(c => c.Rating)
                .ToArrayAsync();

            return carsByRating;
        }

        public async Task<CompareCarsServiceModel> CompareCars(int firstCarId, int secondCarId)
        {
            var firstCar = await this.context.Cars.FirstOrDefaultAsync(c => c.Id == firstCarId);

            var secondCar = await this.context.Cars.FirstOrDefaultAsync(c => c.Id == secondCarId);

            return new CompareCarsServiceModel
            {
                FirstCar = firstCar,
                SecondCar = secondCar
            };
        }
    }
}
