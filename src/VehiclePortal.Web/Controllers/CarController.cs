using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VehiclePortal.Common.ViewModels;
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
                return RedirectToAction("ApplicationError", "Home");
            }

            await this.carService.Add(model);

            return RedirectToAction("All", "Car");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var car = await this.carService.GetById(id);

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
            await this.carService.Edit(model);

            return RedirectToAction("All", "Car");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var car = await this.carService.GetById(id);

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
            var car = await this.carService.Details(id);

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

            var rate = await this.carService.Rate(model);

            return RedirectToAction("TopRated", "Car");
        }

        [HttpGet]
        public async Task<IActionResult> Buy(int id)
        {
            var car = await this.carService.Details(id);

            if (car == null)
            {
                return RedirectToAction("ApplicationError", "Home");
            }

            return this.View(car);
        }

        [HttpPost]
        public async Task<IActionResult> Buy(BuyCarBindingModel model)
        {
            var result = await this.carService.Buy(model, this.User.Identity.Name);

            if(!result)
            {
                return RedirectToAction("ApplicationError", "Home");
            }

            return RedirectToAction("MyCars", "User");
        }

        [HttpGet]
        public async Task<IActionResult> Rent(int id)
        {
            var car = await this.carService.Details(id);

            if (car == null)
            {
                return RedirectToAction("ApplicationError", "Home");
            }

            return this.View(car);
        }

        [HttpPost]
        public async Task<IActionResult> Rent(RentCarBindingModel model)
        {
            var result = await this.carService.Rent(model, this.User.Identity.Name);

            if (!result)
            {
                return RedirectToAction("ApplicationError", "Home");
            }

            return RedirectToAction("MyRents", "User");
        }
    }
}