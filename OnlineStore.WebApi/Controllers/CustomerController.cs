using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using OnlineStore.Entities;
using OnlineStore.Business;

namespace OnlineStore.WebApi.Controllers
{
    public class CustomerController : ApiController
    {
        [HttpPost]
        [Route("RegisterCustomer")]
        public Result RegisterCustomer(Customer customer)
        {
            return CustomerBC.RegisterCustomer(customer);
        }

    }
}
