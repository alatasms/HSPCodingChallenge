using ExpenseTrackerAPI.Application.Features.Account.Command.CreateAccount;
using ExpenseTrackerAPI.Application.Features.Account.Command.RemoveAccount;
using ExpenseTrackerAPI.Application.Features.Account.Command.UpdateAccount;
using ExpenseTrackerAPI.Application.Features.Account.Query.GetAccounts;
using ExpenseTrackerAPI.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTrackerAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes ="Admin")]
    public class AccountsController : ControllerBase
    {
        readonly IMediator _mediator;
        private User? _user;
        public AccountsController(IMediator mediator, IHttpContextAccessor contextAccessor)
        {
            _mediator = mediator;
            _user = contextAccessor.HttpContext.Items["User"] as User;
        }

        [HttpPost("[Action]")]
        public async Task<IActionResult> CreateAccount([FromBody] CreateAccountCommandRequest request)
        {
            request.User = _user!;
            CreateAccountCommandResponse response = await _mediator.Send(request);
            return response.IsSucceeded ? Ok(response) : BadRequest(response);
        }

        [HttpPut("[Action]")]
        public async Task<IActionResult> UpdateAccount([FromBody] UpdateAccountCommandRequest request)
        {
            request.User = _user!;
            UpdateAccountCommandResponse response = await _mediator.Send(request);
            return response.IsSucceeded ? Ok(response) : BadRequest(response);
        }

        [HttpDelete("[Action]{Id}")]
        public async Task<IActionResult> DeleteAccount([FromRoute] string Id)
        {
            RemoveAccountCommandResponse response = await _mediator.Send(new RemoveAccountCommandRequest { Id = Id, User = _user });
            return response.IsSucceeded ? Ok(response) : BadRequest(response);

        }

        [HttpGet("[Action]")]
        public async Task<IActionResult> GetAccounts()
        {
            GetAccountsQueryResponse response = await _mediator.Send(new GetAccountsQueryRequest() { User = _user});
            return Ok(response);
        }
    }
}
