using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Users.Commands.AssignUserRoles;
using Restaurants.Application.Users.Commands.UpdateUserDetails;
using Restaurants.Domain.Constants;

namespace Restaurants.API.Controllers
{
    [ApiController]
    [Route("api/identity")]
    public class IdentityController(IMediator mediator) : ControllerBase
    {
        [HttpPatch("user")]
        [Authorize]
        public async Task<IActionResult> UpdateUserDetails(UpdateUserDetailsCommand command)
        {
            await mediator.Send(command);
            return NoContent();
        }
       
        [HttpPost("userRole")]
        [Authorize (Roles = UserRoles.Admin)]
        public async Task<IActionResult> AssignedUserRole(AssignUserRoleCommand command)
        {
            await mediator.Send(command);
            return NoContent ();
        }
    }
}
