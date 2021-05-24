using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using OnlineStore.Business;
using OnlineStore.Entities;

namespace OnlineStore.WebApi.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {

          Result result =   CustomerBC.GetCustomerByEmail("customer1@hotmail.com");
            //SalesBC.GetOrders(1);
        //  Result result =  CustomerBC.RegisterCustomer(new Customer()
        //    {
        //        CustomerName = "Customer1",
        //        CustomerEmail = "customer1@hotmail.com",
        //        CustomerMobil = "5513133113"
        //    });
           return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public Result Get(int id)
        {
            Result result = new Result();
            result.Status = ResultStatus.Ok;
            return result;
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
