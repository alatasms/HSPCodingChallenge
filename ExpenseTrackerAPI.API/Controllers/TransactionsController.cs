using ExpenseTrackerAPI.Application.Features.Account.Command.UpdateAccount;
using ExpenseTrackerAPI.Application.Features.Transaction.Command.CreateTransaction;
using ExpenseTrackerAPI.Application.Features.Transaction.Command.RemoveTransaction;
using ExpenseTrackerAPI.Application.Features.Transaction.Command.UpdateTransaction;
using ExpenseTrackerAPI.Application.Features.Transaction.Query.GetAllTransactions;
using ExpenseTrackerAPI.Application.Features.Transaction.Query.GetTransactionsByDate;
using ExpenseTrackerAPI.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTrackerAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]

    public class TransactionsController : ControllerBase
    {
        readonly IMediator _mediator;
        private User? _user;
        public TransactionsController(IMediator mediator, IHttpContextAccessor contextAccessor)
        {
            _mediator = mediator;
            _user = contextAccessor.HttpContext.Items["User"] as User;
        }

        [HttpPost("[Action]")]
        public async Task<IActionResult> NewTransaction([FromBody] CreateTransactionCommandRequest request)
        {
            request.User = _user!;
            CreateTransactionCommandResponse response = await _mediator.Send(request);
            return response.IsSucceeded?Ok(response) : BadRequest(response);
        }

        [HttpDelete("[Action]/{Id}")]
        public async Task<IActionResult> DeleteTransaction([FromRoute] string Id)
        {
            RemoveTransactionCommandResponse response = await _mediator.Send(new RemoveTransactionCommandRequest { User =_user, Id =Id});
            return response.IsSucceeded?Ok(response) : BadRequest(response);

        }

        [HttpPut("[Action]")]
        public async Task<IActionResult> UpdateTransaction([FromBody] UpdateTransactionCommandRequest request)
        {
            request.User = _user!;
            UpdateTransactionCommandResponse response = await _mediator.Send(request);
            return response.IsSucceeded ? Ok(response) : BadRequest(response);
        }


        [HttpGet("[Action]/{AccountId}")]
        public async Task<IActionResult> GetAllTransactions([FromRoute] string AccountId)
        {
            GetAllTransactionsQueryResponse response = await _mediator.Send(new GetAllTransactionsQueryRequest { AccountId = AccountId, User = _user});
            return Ok(response);
        }

        [HttpPost("[Action]")]
        public async Task<IActionResult> GetTransactionsByDate([FromBody] GetTransactionsByDateQueryRequest request)
        {
            request.User = _user!;
            GetTransactionsByDateQueryResponse response = await _mediator.Send(request);
            return Ok(response);
        }

    }
}
