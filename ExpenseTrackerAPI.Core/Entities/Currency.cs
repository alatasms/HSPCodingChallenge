﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackerAPI.Domain.Entities
{
    public class Currency : BaseEntity
    {
        public string Name { get; set; }
    }
}
