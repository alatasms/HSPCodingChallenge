using ExpenseTrackerAPI.Application.Abstraction.Token;
using ExpenseTrackerAPI.Application.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackerAPI.Application.Dtos.User
{
    public class CreateUserResponseDto : BaseResponse
    {
        public AccessToken? AccessToken { get; set; }
    }
}
