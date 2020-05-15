using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using VehiclePortal.Common.ViewModels;
using VehiclePortal.Services.Interfaces;

namespace VehiclePortal.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICarService carService;

        public HomeController(ICarService carService)
        {
            this.carService = carService;
        }

        public async Task<IActionResult> Index()
        {
            var allCars = (await this.carService.GetAll())
                .Select(Mapper.Map<AllCarsListedViewModel>);

            ViewData["Cars"] = new SelectList(allCars, "Id","CarModel");

            var topRated = (await this.carService.GetAllByRating())
                .Select(Mapper.Map<AllCarsListedViewModel>);

            return View(topRated);
        }

        public IActionResult ApplicationError()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
