using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants;

internal class RestaurantService(IRestaurantRepository restaurantRepository, ILogger<RestaurantService> logger) : IRestaurantService
{
    public async Task<IEnumerable<Restaurant>> GetAllRestaurants()
    {
        logger.LogInformation("Getting all restaurants");
        return await restaurantRepository.GetAllAsync();
    }

    public async Task<Restaurant> GetRestaurantById(int id)
    {
        var restaurnat = await restaurantRepository.GetByIdAsync(id);
        return restaurnat;
    }
}