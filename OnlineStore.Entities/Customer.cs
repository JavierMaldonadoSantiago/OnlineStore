﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Entities
{
    public class Customer
    {
        public Customer()
        {
            this.CustomerId = 0;
        }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerMobil { get; set; }
    }
}
