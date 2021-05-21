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
            int customerId = CustomerDAC.IsCustomerRegistered(new Customer() { CustomerEmail = order.UserEmail });
            if (customerId != 0)
            {
                order.CustomerId = customerId;
                return SalesDAC.NewOrder(order);
            }
            else
            {
                return new Result()
                {
                    Status = ResultStatus.Error,
                    Message = "El usuario no esta registrado"
                };
            }
            
        }
        public static Result GetOrders(Customer customer)
        {
            int customerId = CustomerDAC.IsCustomerRegistered(new Customer() { CustomerEmail = customer.CustomerEmail });
            if (customerId != 0)
            {
                return SalesDAC.GetOrders(customerId);
            }
            else
            {
                return new Result()
                {
                    Status = ResultStatus.Error,
                    Message = "El usuario no esta registrado"
                };
            }
            
        }
    }
}
