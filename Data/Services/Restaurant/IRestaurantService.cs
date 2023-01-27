using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantRaterMVC.Data.Services.Restaurant
{
    public interface IRestaurantService
    {
        Task<bool> CreateRestaurant(RestaurantCreate Model);
        Task<List<RestarauntListItem>> GetAllRestaurants();
        Task<RestaurantDetail> GetRestaurantById(int id);
        Task<bool> UpdateRestaurant(RestaurantEdit Model);
        Task<bool> DeleteRestaurant(int id);
    }
}