using ExpenseTrackerAPI.Application.Abstraction.Services.Identity;
using ExpenseTrackerAPI.Application.Dtos.User;
using ExpenseTrackerAPI.Application.Features;
using ExpenseTrackerAPI.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackerAPI.Persistence.Services
{
    public class UserService : IUserService
    {
        readonly UserManager<User> _userManager;

        public UserService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<CreateUserResponseDto> CreateUser(CreateUserDto createUser)
        {

            User user = new()
            {
                Id = Guid.NewGuid(),
                FirstName = createUser.FirstName,
                LastName = createUser.LastName,
                Email = createUser.Email,
                UserName = createUser.Email
            };

            IdentityResult result = await _userManager.CreateAsync(user, createUser.Password);

            CreateUserResponseDto response = new() { IsSucceeded=result.Succeeded };


            if (result.Succeeded)
            {
                response.Message = ResponseMessages.UserCreated.ToString();
            }else
                foreach(var error in result.Errors)
                {
                    response.Message += $"{error.Code} - {error.Description}\n";
                }

            return response;
        }
    }
}
