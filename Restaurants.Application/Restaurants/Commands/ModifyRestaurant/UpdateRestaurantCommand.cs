﻿using MediatR;

namespace Restaurants.Application.Restaurants.Commands.ModifyRestaurant;

public class UpdateRestaurantCommand(int id) : IRequest<bool>
{
    public int Id { get; set; } = id;

    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public bool HasDelivery { get; set; }


}
