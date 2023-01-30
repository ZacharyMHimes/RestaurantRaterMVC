using Microsoft.EntityFrameworkCore;
using RestaurantRaterMVC.Models.Restaurant;
using RestaurantRaterMVC.Data;



namespace RestaurantRaterMVC.Services
{
    public class RestaurantService
    {
        private RestaurantDbContext _context;
        public RestaurantService(RestaurantDbContext context)
        {
            _context = context;
        }

        //* Create
        public async Task<bool> CreateRestaurant(RestaurantCreate model)
        {
            Restaurant restaurant = new Restaurant
            {
                Name = model.Name,
                Location =  model.Location,
            };

            _context.Restaurants.Add(restaurant);
            return await _context.SaveChangesAsync() == 1;
        }
        //* Read
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

        public async Task<Restaurant> GetRestaurantById(int id)
        {
            Restaurant restaurant = await _context.Restaurants
                .Include(r => r.Ratings)
                .FirstOrDefaultAsync(r => r.Id == id);

            if(restaurant is null)
                return null;

            return restaurant;
        }

        //* Update
        public async Task<bool> UpdateRestaurant(Restaurant model)
        {
            Restaurant restaurant = new Restaurant
            {
                Name = model.Name,
                Location =  model.Location,
            };

            _context.Restaurants.Add(restaurant);
            return await _context.SaveChangesAsync() == 1;
        }

    }
}