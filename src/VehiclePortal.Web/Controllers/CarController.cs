using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VehiclePortal.Common.ViewModels;
using VehiclePortal.Services.Interfaces;

namespace VehiclePortal.Web.Controllers
{
    public class CarController : Controller
    {
        private readonly ICarService carService;

        public CarController(ICarService carService)
        {
            this.carService = carService;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(CarBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return NotFound();
            }

            await this.carService.Add(model);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var car = await this.carService.GetById(id);

            if (car == null)
            {
                return this.NotFound();
            }

            return this.View(car);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditCarViewModel model)
        {
            await this.carService.Edit(model);

            return RedirectToAction("All", "Car");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var car = await this.carService.GetById(id);

            if (car == null)
            {
                return this.NotFound();
            }

            return this.View(car);
        }

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

        public async Task<IActionResult> Details(int id)
        {
            var car = await this.carService.Details(id);

            if (car == null)
            {
                return this.NotFound();
            }

            return this.View(car);
        }

        public async Task<IActionResult> Rate(RateCarBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return NotFound();
            }

            var rate = await this.carService.Rate(model);

            return RedirectToAction("Details", "Car");
        }
    }
}