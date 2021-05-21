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
    public class SalesController : ApiController
    {
        [HttpPost]
        [Route("NewOrder")]
        public Result NewOrder(Order order)
        {
            return SalesBC.NewOrder(order);
        }

        [HttpPost]
        [Route("GetOrders")]
        public Result GetOrders(Order order)
        {
            return SalesBC.NewOrder(order);
        }

    }
}
