using ExpenseTrackerAPI.Application.Abstraction.Services.Identity;
using ExpenseTrackerAPI.Application.Abstraction.Token;
using ExpenseTrackerAPI.Application.Dtos.Auth;
using ExpenseTrackerAPI.Application.Features;
using ExpenseTrackerAPI.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackerAPI.Persistence.Services
{
    public class AuthService : IAuthService
    {
        readonly UserManager<User> _userManager;
        readonly SignInManager<User> _signInManager;

        readonly ITokenHandler _tokenHandler;
        readonly IConfiguration _configuration;

        public AuthService(UserManager<User> userManager, SignInManager<User> signInManager, ITokenHandler tokenHandler, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenHandler = tokenHandler;
            _configuration = configuration;
        }

        public async Task<LoginUserResponseDto> Login(string email, string password)
        {
            User user = await _userManager.FindByEmailAsync(email);

            if (user == null) { throw new Exception(ResponseMessages.WrongEmailOrPassword.ToString()); }


            SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
            if (result.Succeeded)
            {
                AccessToken token = _tokenHandler.CreateAccessToken(user);

                return new()
                {
                    AccessToken = token,
                    IsSucceeded = true,
                    Message = ResponseMessages.UserLoggedInSuccessfully.ToString()
                };
            }

            return new()
            {
                IsSucceeded = false,
                Message = ResponseMessages.UserLoggedInFailed.ToString()
            };
        }
    }
}
