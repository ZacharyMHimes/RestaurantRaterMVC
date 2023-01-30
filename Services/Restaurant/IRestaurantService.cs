using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestaurantRaterMVC.Models.Restaurant;
using RestaurantRaterMVC.Data;

namespace RestaurantRaterMVC.Services;

public interface IRestaurantService
{
    Task<bool> CreateRestaurant(RestaurantCreate model);
    Task<List<RestaurantListItem>> GetAllRestaurants();
    Task<Restaurant> GetRestaurantById(int id);
    Task<bool> UpdateRestaurant(Restaurant restaurant);
    // Task<bool> DeleteRestaurant(int id);
}