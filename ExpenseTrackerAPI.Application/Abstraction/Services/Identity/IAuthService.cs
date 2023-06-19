using ExpenseTrackerAPI.Application.Dtos.Auth;
using ExpenseTrackerAPI.Application.Dtos.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackerAPI.Application.Abstraction.Services.Identity
{
    public interface IAuthService
    {
        Task<LoginUserResponseDto> Login(string email, string password);
    }
}
