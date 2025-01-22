using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.ModifyRestaurant;

public class UpdateRestaurantCommandHandler(ILogger<UpdateRestaurantCommandHandler> logger,
    IRestaurantRepository restaurantRepository,
    IMapper mapper)
    : IRequestHandler<UpdateRestaurantCommand, bool>
{
    public async Task<bool> Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Modifying a restaurant with id : {request.Id}");
        var restuarant = await restaurantRepository.GetByIdAsync(request.Id);
        if (restuarant == null)
        {
            return false;
        }

        mapper.Map(request, restuarant);
        //restuarant.Name = request.Name;
        //restuarant.Description = request.Description;
        //restuarant.HasDelivery = request.HasDelivery;
        await restaurantRepository.SaveChanges();
        return true;
    }
}
