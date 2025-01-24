using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes.Commands.DeleteAllDishes
{
    public class DeleteAllDishesCommandHandler(ILogger<DeleteAllDishesCommandHandler> logger, IRestaurantRepository restaurantRepository,
        IDishesRepository dishesRepository) : IRequestHandler<DeleteAllDishesCommand>
    {
        public async Task Handle(DeleteAllDishesCommand request, CancellationToken cancellationToken)
        {
            var restaurant = await restaurantRepository.GetByIdAsync(request.RestaurantId);
            if (restaurant == null)
            {
                throw new NotFoundException($"No restaurant with id: {request.RestaurantId} was found");
            }

            await dishesRepository.DeleteAll(restaurant.Dishes);
        }
    }
}
