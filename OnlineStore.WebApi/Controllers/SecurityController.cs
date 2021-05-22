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
    public class SecurityController : ApiController
    {
        [HttpPost]
        [Route("GetUserByUserName")]
        public Result GetUserByUserName(UserSession user)
        {
            return CustomerBC.GetCustomerByEmail(user.Email);
        }

    }
}