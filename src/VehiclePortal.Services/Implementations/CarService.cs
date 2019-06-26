using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehiclePortal.Common.ViewModels;
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

        public async Task Add(CarBindingModel model)
        {
            var car = Mapper.Map<Car>(model);

            await this.context.AddAsync(car);

            await this.context.SaveChangesAsync();
        }

        public async Task Edit(EditCarViewModel model)
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
            car.Rating = model.Rating;
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

        public async Task<EditCarViewModel> GetById(int id)
        {
            var car = await this.context.Cars
                .ProjectTo<EditCarViewModel>()
                .FirstOrDefaultAsync(c => c.Id == id);

            return car;
        }

        public async Task<CarDetailsViewModel> Details(int id)
        {
            var car = await this.context.Cars
                .ProjectTo<CarDetailsViewModel>()
                .FirstOrDefaultAsync(c => c.Id == id);

            return car;
        }

        public async Task<IEnumerable<Car>> GetAll()
        {
            var car = await this.context.Cars.ToArrayAsync();

            return car;
        }

        public async Task<bool> Rate(RateCarBindingModel model)
        {
            var car = await this.context.Cars.SingleOrDefaultAsync(c => c.Id == model.CarId);

            if (car == null)
            {
                return false;
            }

            var rate = Mapper.Map<Car>(model);

            car.Rating+=model.Rating;

            this.context.Cars.Update(car);

            await this.context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Buy(BuyCarBindingModel model, string username)
        {
            var user = await this.context.Users.SingleOrDefaultAsync(u => u.UserName == username);

            var car = await this.context.Cars.SingleOrDefaultAsync(c => c.Id == model.CarId);

            if (user == null || car == null || user.Balance<car.Price) 
            {
                return false;
            }

            var buyCar = Mapper.Map<BuyCar>(model);

            buyCar.User = user;
            buyCar.BoughtOn = DateTime.Now;
            buyCar.Price = car.Price;

            user.Balance -= car.Price;

            this.context.BoughtCars.Add(buyCar);

            await this.context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Rent(RentCarBindingModel model, string username)
        {
            var user = await this.context.Users.SingleOrDefaultAsync(u => u.UserName == username);

            var car = await this.context.Cars.SingleOrDefaultAsync(c => c.Id == model.CarId);

            if (user == null || car == null) 
            {
                return false;
            }

            var rentCar = Mapper.Map<RentCar>(model);
            rentCar.StartDate = DateTime.Now;
            rentCar.User = user;

            var totalDays = (model.EndDate - model.StartDate).Days;

            rentCar.TotalPrice = totalDays * car.RentPricePerDay;

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
    }
}
