using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.DeleteRestaurant;

public class DeleteRestaurantCommandHandler(ILogger<DeleteRestaurantCommandHandler> logger,
    IRestaurantRepository restaurantRepository) : IRequestHandler<DeleteRestaurantCommand,bool>
{
    public async Task<bool> Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Deleting a restaurant with id : {request.Id}");
        var restuarant = await restaurantRepository.GetByIdAsync( request.Id );
        if(restuarant == null)
        {
            return false;
        }
        await restaurantRepository.Delete(restuarant);
        return true;
    }
}
