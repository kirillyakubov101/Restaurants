using Microsoft.Extensions.DependencyInjection;
using Restaurants.Application.Restaurants;

namespace Restaurants.Application.Extenstions;

public static class ServiceCollectionExtensions
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IRestaurantService, RestaurantService>();

        // Scans the assembly for classes that inherit from Profile and registers them for AutoMapper.
        services.AddAutoMapper(typeof(ServiceCollectionExtensions).Assembly);
    }
}
