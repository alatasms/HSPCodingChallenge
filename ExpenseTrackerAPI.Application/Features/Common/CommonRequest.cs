using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ExpenseTrackerAPI.Application.Features.Common
{
    public class CommonRequest
    {

        
        [JsonIgnore(Condition =JsonIgnoreCondition.Always)]
        public E.Identity.User? User { get; set; }
    }
}
