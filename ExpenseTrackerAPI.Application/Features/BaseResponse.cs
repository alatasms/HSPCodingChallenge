using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackerAPI.Application.Features
{
    public class BaseResponse
    {
        public bool IsSucceeded { get; set; } = true;
        public string? Message { get; set; }
    }
}
