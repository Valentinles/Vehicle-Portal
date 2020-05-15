using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using VehiclePortal.Common.ServiceModels;
using VehiclePortal.Common.ViewModels;
using VehiclePortal.Models;
using VehiclePortal.Services.Interfaces;

namespace VehiclePortal.Web.Controllers
{
    [Authorize]
    public class CarController : Controller
    {
        private readonly ICarService carService;

        public CarController(ICarService carService)
        {
            this.carService = carService;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Add()
        {
            return this.View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Add(CarBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View();
            }

            var car = Mapper.Map<Car>(model);

            await this.carService.Add(car);

            return RedirectToAction("All", "Car");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var getCar = await this.carService.GetById(id);

            var car = Mapper.Map<EditCarViewModel>(getCar);

            if (car == null)
            {
                return RedirectToAction("ApplicationError", "Home");
            }

            return this.View(car);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Edit(EditCarViewModel model)
        {
            var car = Mapper.Map<EditCarServiceModel>(model);

            await this.carService.Edit(car);

            return RedirectToAction("All", "Car");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var getCar = await this.carService.GetById(id);

            var car = Mapper.Map<EditCarViewModel>(getCar);

            if (car == null)
            {
                return RedirectToAction("ApplicationError", "Home");
            }

            return this.View(car);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Delete(int id, CarDetailsViewModel model)
        {
            await this.carService.Delete(id);

            return RedirectToAction("All", "Car");
        }

        public async Task<IActionResult> All()
        {
            var cars = (await this.carService.GetAll())
                .Select(Mapper.Map<AllCarsListedViewModel>);

            return this.View(cars);
        }

        [AllowAnonymous]
        public async Task<IActionResult> TopRated()
        {
            var cars = (await this.carService.GetAllByRating())
                .Select(Mapper.Map<AllCarsListedViewModel>);

            return this.View(cars);
        }

        public async Task<IActionResult> Details(int id)
        {
            var getCar = await this.carService.Details(id);

            var car = Mapper.Map<CarDetailsViewModel>(getCar);

            if (car == null)
            {
                return RedirectToAction("ApplicationError", "Home");
            }

            return this.View(car);
        }

        public async Task<IActionResult> Rate(RateCarBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("ApplicationError", "Home");
            }

            var mapRate = Mapper.Map<RateCarServiceModel>(model);

            await this.carService.Rate(mapRate, this.User.Identity.Name);

            return RedirectToAction("Details", "Car", new { id = model.CarId });
        }

        [HttpGet]
        public async Task<IActionResult> Buy(int id)
        {
            var getCar = await this.carService.Details(id);

            var car = Mapper.Map<CarDetailsViewModel>(getCar);

            if (car == null)
            {
                return RedirectToAction("ApplicationError", "Home");
            }

            return this.View(car);
        }

        [HttpPost]
        public async Task<IActionResult> Buy(BuyCarBindingModel model)
        {
            var buyCar = Mapper.Map<BuyCar>(model);

            var result = await this.carService.Buy(buyCar, this.User.Identity.Name);

            if (!result)
            {
                return RedirectToAction("ApplicationError", "Home");
            }

            return RedirectToAction("MyCars", "User");
        }

        [HttpGet]
        public async Task<IActionResult> Rent(int id)
        {
            var getCar = await this.carService.Details(id);

            var car = Mapper.Map<CarDetailsViewModel>(getCar);

            if (car == null)
            {
                return RedirectToAction("ApplicationError", "Home");
            }

            return this.View(car);
        }

        [HttpPost]
        public async Task<IActionResult> Rent(RentCarBindingModel model)
        {
            var rentCar = Mapper.Map<RentCar>(model);

            var result = await this.carService.Rent(rentCar, this.User.Identity.Name);

            if (!result)
            {
                return RedirectToAction("ApplicationError", "Home");
            }

            return RedirectToAction("MyRents", "User");
        }

        public async Task<IActionResult> CompareCars(int firstCarId, int secondCarId)
        {
            var getCars = await this.carService.CompareCars(firstCarId, secondCarId);

            var carsToCompare = Mapper.Map<CompareCarsViewModel>(getCars);

            if (carsToCompare == null)
            {
                return RedirectToAction("ApplicationError", "Home");
            }

            return this.View(carsToCompare);
        }
    }
}