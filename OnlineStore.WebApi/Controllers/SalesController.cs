using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using OnlineStore.Entities;
using OnlineStore.Business;
using PlacetoPay.Integrations.Library.CSharp;
using PlacetoPay.Integrations.Library.CSharp.Contracts;
using PlacetoPay.Integrations.Library.CSharp.Entities;
using PlacetoPay.Integrations.Library.CSharp.Message;
using System.Configuration;

namespace OnlineStore.WebApi.Controllers
{
    public class SalesController : ApiController
    {
        [HttpPost]
        [Route("ReturnUrl")]
        public Result ReturnUrl(Order order)
        {
            return SalesBC.NewOrder(order);
        }

        [HttpPost]
        [Route("NewOrder")]
        public Result NewOrder(Order order)
        {
            return SalesBC.NewOrder(order);
        }
        [HttpPost]
        [Route("GetOrders")]
        public Result GetOrders(Customer customer)
        {
            return SalesBC.GetOrders(customer);
        }

        [HttpPost]
        [Route("GetOrder")]
        public Result GetOrder(Customer customer)
        {
            return SalesBC.GetOrder(customer);
        }

        private const string LOGIN = "LOGIN";
        private const string TRANKEY = "TRANKEY";
        private const string URL_P2P = "URL_P2P";
        private const string RETURN_URL = "RETURN_URL";

        [HttpPost]
        [Route("PayOrder")]
        public Result PayOrder(Customer customer)
        {
           Result result = new Result();
            try
            {
                OrderItem order = (OrderItem)SalesBC.GetOrder(customer).ObjectResult;
                
                string login = ConfigurationManager.AppSettings[LOGIN].ToString();
                string key = ConfigurationManager.AppSettings[TRANKEY].ToString();
                string urlP2p = ConfigurationManager.AppSettings[URL_P2P].ToString();
                string returnUrl = ConfigurationManager.AppSettings[RETURN_URL].ToString();
                Gateway gateway = new PlacetoPay.Integrations.Library.CSharp.PlacetoPay(login, key, new Uri(urlP2p), Gateway.TP_REST);

                Amount amount = new Amount((double)order.Amount, "COP", null, null, 0, 0, 0); // 'PAYMENT_AMOUNT');
                Payment payment = new Payment(order.OrderId.ToString(), "DESCRIPTION", amount);
                RedirectRequest request = new RedirectRequest(payment,
                    returnUrl, "189.217.0.153","",
                    DateTime.Now.AddMinutes(15).ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssZ").ToString());

                RedirectResponse response = gateway.Request(request);
                if (response.Status.status != "FAILED")
                {
                    result.Status = ResultStatus.Ok;

                    result.Message = response.Status.Message;
                }
                else
                {
                    result.Status = ResultStatus.Error;
                    result.Message = response.Status.Message;
                }
                

            }
            catch (Exception ex)
            {
                result.Status = ResultStatus.Error;
                result.Message = ex.Message;
            }


            return result; // SalesBC.GetOrders(customer);
        }

    }
}
