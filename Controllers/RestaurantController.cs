using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RestaurantRaterMVC.Data.Services.Restaurant;
using RestaurantRaterMVC.Models.Restaurant;
using Microsoft.EntityFrameworkCore;

namespace RestaurantRaterMVC.Controllers
{
    public class RestaurantController
    {
        private IRestaurantService _service;
        public RestaurantController(IRestaurantService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            List<RestaurantListItem> restaurants = await _service.GetAllRestaurants();
            return View(restaurants);
        }

        //Get Method
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(RestaurantCreate model)
        {
            if(ModelState.IsValid)
                return View(model);
            
            await _service.CreateRestaurant(model);
            return RedirectToAction(nameof(Index));
        }
    }
}