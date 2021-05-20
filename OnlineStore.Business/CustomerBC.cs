using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineStore.Data;
using OnlineStore.Entities;

namespace OnlineStore.Business
{
    public static class CustomerBC
    {
        public static Result RegisterCustomer(Customer customer)
        {
            return CustomerDAC.RegisterCustomer(customer);
        }
    }
}
