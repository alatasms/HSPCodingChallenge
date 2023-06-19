using ExpenseTrackerAPI.Application.Features.User.Command.CreateUser;
using ExpenseTrackerAPI.Application.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTrackerAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        
        readonly IMediator _mediator;


        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("[Action]")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserCommandRequest request)
        {
            CreateUserCommandResponse response = await _mediator.Send(request);
            return response.IsSucceeded ? Ok(response) : BadRequest(response);
        }
    }
}
