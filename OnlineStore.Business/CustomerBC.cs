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
            if (CustomerDAC.IsCustomerRegistered(customer) == 0)
            {
                return CustomerDAC.RegisterCustomer(customer);
            }
            else
            {
                return new Result()
                {
                    Message = "El usuario ya exite, use login para ingresar",
                    Status = ResultStatus.Error

                };
            }

        }

        public static Result GetCustomerByEmail(string email)
        {
            return CustomerDAC.GetCustomerByEmail(email);
        }
    }
}
