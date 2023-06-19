using ExpenseTrackerAPI.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackerAPI.Domain.Entities
{
    public class Account :BaseEntity
    {
        public string AccountNumber { get; set; }
        public string Name { get; set; }
        public Currency Currency { get; set; }
        
        public int Balance { get; set; }

        public User User { get; set; }
        public ICollection<Transaction> Transactions { get; set; }

        public Account()
        {
            Transactions = new List<Transaction>(); 
        }
    }
}
