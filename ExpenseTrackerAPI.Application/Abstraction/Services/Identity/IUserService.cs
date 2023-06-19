using ExpenseTrackerAPI.Application.Dtos.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackerAPI.Application.Abstraction.Services.Identity
{
    public interface IUserService
    {
        Task<CreateUserResponseDto> CreateUser(CreateUserDto createUser);
    }
}
