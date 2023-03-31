﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class AccountCategory
    {
        public Guid AccountId  { get; set; }
        public Account Account { get; set; }

        public Guid CategoryId { get; set; }
        public Category Category { get; set; }


    }
}
