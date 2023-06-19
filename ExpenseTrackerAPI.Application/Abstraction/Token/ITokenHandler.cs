using ExpenseTrackerAPI.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackerAPI.Application.Abstraction.Token
{
    public interface ITokenHandler
    {
        AccessToken CreateAccessToken(User user);
    }
}
