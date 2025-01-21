using AutoMapper;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants;

internal class RestaurantService(IRestaurantRepository restaurantRepository,
    ILogger<RestaurantService> logger,
    IMapper mapper
    ) 
    : IRestaurantService
{
    public async Task<IEnumerable<RestaurantDto>> GetAllRestaurants()
    {
        logger.LogInformation("Getting all restaurants");

        var restaurnats =  await restaurantRepository.GetAllAsync();
        var restaurnatsDtos = mapper.Map<IEnumerable<RestaurantDto>>(restaurnats);

        return restaurnatsDtos!;
    }

    public async Task<RestaurantDto?> GetRestaurantById(int id)
    {
        var restaurnat = await restaurantRepository.GetByIdAsync(id);
        var restaurnatDto = mapper.Map<RestaurantDto?>(restaurnat);

        return restaurnatDto;
    }
}