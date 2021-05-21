using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Entities
{
    public class OrderItem
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public string Product { get; set; }
        public int Pieces { get; set; }
        public decimal Amount { get; set; }

        public string Status { get; set; }
    }
}
