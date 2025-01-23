using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.DeleteRestaurant;

public class DeleteRestaurantCommandHandler(ILogger<DeleteRestaurantCommandHandler> logger,
    IRestaurantRepository restaurantRepository) : IRequestHandler<DeleteRestaurantCommand>
{
    public async Task Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Deleting a restaurant with id : {request.Id}");
        var restuarant = await restaurantRepository.GetByIdAsync( request.Id );
        if(restuarant == null)
        {
            throw new NotFoundException($"Restaurant with {request.Id} does not exist");
        }
        await restaurantRepository.Delete(restuarant);
    }
}
