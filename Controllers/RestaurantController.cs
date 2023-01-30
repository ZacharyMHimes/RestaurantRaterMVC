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
//* Restaurant Get Methods
//Returns a List of All Restaurants for Indexing
        public async Task<IActionResult> Index()
        {
            List<RestaurantListItem> restaurants = await _service.GetAllRestaurants();
            return View(restaurants);
        }

// Returns Restaurant Detail View Model for single page view
        [ActionName("Details")]
        public async Task<IActionResult> Restaurant(int id)
        {
            Restaurant restaurant = await _service.GetRestaurantById(id);

            if (restaurant == null)
                return RedirectToAction(nameof(Index));

            RestaurantDetail restaurantDetail = new RestaurantDetail()
            {
                Id = restaurant.Id,
                Name = restaurant.Name,
                Location = restaurant.Location,
                Score = restaurant.Score,
            };

            return View(restaurantDetail);               
        }
//* Restaurant Create Methods
// Posts Newly Created Restaurant to the DB
        [HttpPost]
        public async Task<IActionResult> Create(RestaurantCreate model)
        {
            if(ModelState.IsValid)
                return View(model);
            
            await _service.CreateRestaurant(model);
            return RedirectToAction(nameof(Index));
        }

//Returns Newly Created Restaurant Model for single Page view
        public async Task<IActionResult> Create()
        {
            return View();
        }

//* Restaurant Edit Methods
// Returns a restaurant Edit View Model populated with data
        public async Task<IActionResult> Edit(int id)
        {
            Restaurant restaurant = await _service.GetRestaurantById(id);

            if (restaurant == null)
                return RedirectToAction(nameof(Index));
            
            RestaurantEdit restaurantEdit = new RestaurantEdit()
            {
                Id = restaurant.Id,
                Name = restaurant.Name,
                Location = restaurant.Location,
            };

            return View(restaurantEdit);
        }
// Posts the Edit restaurant Data to the Database
        [HttpPost]
        public async Task<IActionResult> Edit(int id, RestaurantEdit model)
        {
            if (!ModelState.IsValid)
            {
                return View(ModelState);
            }

            Restaurant restaurant = await _service.GetRestaurantById(id);

            if (restaurant is null)
                return RedirectToAction(nameof(Index));
            
            restaurant.Name = model.Name;
            restaurant.Location = model.Location;

            await _service.UpdateRestaurant(restaurant);

            return RedirectToAction("Details", new {id = restaurant.Id});
        }
    }
}