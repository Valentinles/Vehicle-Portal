using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VehiclePortal.Common.ServiceModels;
using VehiclePortal.Common.ViewModels;
using VehiclePortal.Services.Interfaces;

namespace VehiclePortal.Web.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> SoldCars()
        {
            var soldCars = (await this.userService.GetAllSoldCars())
                .Select(Mapper.Map<SoldCarsViewModel>);

            return this.View(soldCars);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RentedCars()
        {
            var rentedCars = (await this.userService.GetAllRentedCars())
                .Select(Mapper.Map<RentedCarsViewModel>);

            return this.View(rentedCars);
        }

        public async Task<IActionResult> MyRents()
        {
            var rentedCarsByUser = (await this.userService.GetAllRentedCarsByUser(this.User.Identity.Name))
                .Select(Mapper.Map<RentedCarsViewModel>);

            return this.View(rentedCarsByUser);
        }

        public async Task<IActionResult> MyCars()
        {
            var boughtCarsByUser = (await this.userService.GetAllBoughtCarsByUser(this.User.Identity.Name))
                .Select(Mapper.Map<SoldCarsViewModel>);

            return this.View(boughtCarsByUser);
        }

        [HttpGet]
        public IActionResult AddFunds()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> AddFunds(AddFundsBindingModel model)
        {
            if(!ModelState.IsValid)
            {
                return this.View();
            }

            var result = Mapper.Map<AddFundsServiceModel>(model);

            await this.userService.AddFunds(result, this.User.Identity.Name);

            return RedirectToAction("AddFunds", "User");
        }
    }
}