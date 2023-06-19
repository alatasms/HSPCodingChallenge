using ExpenseTrackerAPI.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackerAPI.Domain.Entities
{
    public class Transaction :BaseEntity
    {
        public DateTime TransactionDate { get; set; }
        public int  Spend { get; set; }
        public Category Category { get; set; }
        public Account Account { get; set; }

    }
}
