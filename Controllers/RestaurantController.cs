using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RestaurantRaterMVC.Data.Services.Restaurant;
using RestaurantRaterMVC.Models.Restaurant;

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
    }
}