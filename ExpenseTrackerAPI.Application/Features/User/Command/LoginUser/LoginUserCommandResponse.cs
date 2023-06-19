using ExpenseTrackerAPI.Application.Abstraction.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackerAPI.Application.Features.User.Command.LoginUser
{
    public class LoginUserCommandResponse : BaseResponse
    {
        public AccessToken? AccessToken{ get; set; }
    }
}
