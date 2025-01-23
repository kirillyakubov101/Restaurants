﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Dishes.Commands.CreateDish;

namespace Restaurants.API.Controllers;

[ApiController]
[Route("api/restaurant/{restaurantId}/dishes")]
public class DishesController(IMediator mediator)  : Controller
{
    [HttpPost]
    public async Task<IActionResult> CreateDish([FromRoute]int restaurantId, CreateDishCommand command)
    {
        command.RestaurantId = restaurantId;

        await mediator.Send(command);
        return Created();
    }
}
