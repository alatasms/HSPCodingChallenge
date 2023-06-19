using ExpenseTrackerAPI.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace ExpenseTrackerAPI.API.Middlewares
{
    public class JwtUserMiddleware : IMiddleware
    {
        private readonly UserManager<User> _userManager;

        public JwtUserMiddleware(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var emailClaim = context.User.Identity.Name;
                if (emailClaim != null)
                {
                    var user =await _userManager.FindByEmailAsync(emailClaim);

                    if (user != null)
                    {
                        context.Items["User"] = user;
                    }
                }

            await next(context);
        }
    }
}
