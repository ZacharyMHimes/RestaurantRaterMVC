using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RestaurantRaterMVC.Models.Restaurant;

namespace RestaurantRaterMVC.Data.Services.Restaurant
{
    public class RestaurantService
    {
        private RestaurantDbContext _context;
        public RestaurantService(RestaurantDbContext context)
        {
            _context = context;
        }

        public async Task<List<RestaurantListItem>> GetAllRestaurants()
        {
            List<RestaurantListItem> restaurants = await _context.Restaurants
            .Include(r => r.Ratings)
            .Select(r => new RestaurantListItem()
            {
                Id = r.Id,
                Name = r.Name,
                Score = r.Score,
            }).ToListAsync();

            return restaurants;
        }
    }
}