using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RestaurantRaterMVC.Services;
using RestaurantRaterMVC.Models.Restaurant;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using RestaurantRaterMVC.Data;

namespace RestaurantRaterMVC.Controllers
{
    public class RestaurantController : Controller
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

        [ActionName("Details")]
        public async Task<IActionResult> Restaurant(int id)
        {
            Restaurant restaurant = await _context.Restaurants
                .Include(r => r.Ratings)
                .FirstOrDefaultAsync(r => r.Id == id);

                if (restaurant == null)
                    return RedirectToAction(nameof(Index));
                
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