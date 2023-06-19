using ExpenseTrackerAPI.Application.Repositories;
using ExpenseTrackerAPI.Domain.Entities;
using ExpenseTrackerAPI.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackerAPI.Persistence.Repositories
{
    public class CurrencyRepository : Repository<Currency>, ICurrencyRepository
    {
        public CurrencyRepository(ExpenseTrackerAPIDBContext context) : base(context)
        {
        }
    }
}
