using Restaurants.Application.Restaurants.Dtos;

namespace Restaurants.Application.Restaurants
{
    public interface IRestaurantService
    {
        Task<IEnumerable<RestaurantDto>> GetAllRestaurants();
        Task<RestaurantDto?> GetRestaurantById(int id);
    }
}