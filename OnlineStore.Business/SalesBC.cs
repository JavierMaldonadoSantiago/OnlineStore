using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineStore.Entities;
using OnlineStore.Data;

namespace OnlineStore.Business
{
    public static class SalesBC
    {
        public static Result NewOrder(Order order)
        {
            return SalesDAC.NewOrder(order);
        }
        public static Result GetOrders(int customerId)
        {
            return SalesDAC.GetOrders(customerId);
        }
    }
}
