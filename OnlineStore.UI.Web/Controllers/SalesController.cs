using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using OnlineStore.Entities;
using OnlineStore.Security;
using OnlineStore.UI.Web.Models;

namespace OnlineStore.UI.Web.Controllers
{
    public class SalesController : Controller
    {
        string baseUrl = ConfigurationManager.AppSettings.Get("virtualPath").ToString();

        public ActionResult Index(OrderModel model)
        {

            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Customer");
            }
            Customer customer = new Customer()
            {
                CustomerEmail = User.Identity.Name
            };
            Result resultApi = new Result();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = client.PostAsJsonAsync("api/Sales/PayOrder/", customer).Result;
                if (res.IsSuccessStatusCode)
                {
                    var response = res.Content.ReadAsStringAsync().Result;
                    resultApi = JsonConvert.DeserializeObject<Result>(response);
                    model.Message = resultApi.Message;
                }
            }
            model.ActiveOrder = GetOrder(customer);
            return View(model);
        }
        private OrderItem GetOrder(Customer customer)
        {
            Result resultApi = new Result();
            OrderItem order = new OrderItem();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = client.PostAsJsonAsync("api/Sales/GetOrder/", customer).Result;
                if (res.IsSuccessStatusCode)
                {
                    var response = res.Content.ReadAsStringAsync().Result;
                    resultApi = JsonConvert.DeserializeObject<Result>(response);
                    if (resultApi.Status == ResultStatus.Ok)
                    {
                        order = JsonConvert.DeserializeObject<OrderItem>(resultApi.ObjectResult.ToString());
                    }
                }
            }
            return order;
        }
        // GET: Sales
        [HttpGet]
        public ActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Customer");
            }
            Customer customer = new Customer()
            {
                CustomerEmail = User.Identity.Name
            };
            OrderModel model = new OrderModel();
            model.ActiveOrder = GetOrder(customer);
            return View(model);
        }
        [HttpGet]
        public ActionResult Orders()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Customer");
            }
            OrdersModel model = new OrdersModel();
            Customer costumer = new Customer()
            {
                CustomerEmail = User.Identity.Name
            };
            Result resultApi = new Result();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = client.PostAsJsonAsync("api/Sales/GetOrders/", costumer).Result;
                if (res.IsSuccessStatusCode)
                {
                    var response = res.Content.ReadAsStringAsync().Result;
                    resultApi = JsonConvert.DeserializeObject<Result>(response);
                    if (resultApi.Status == ResultStatus.Ok)
                    {
                        model.Orders = JsonConvert.DeserializeObject<List<OrderItem>>(resultApi.ObjectResult.ToString());
                    }
                }
            }
            return View(model);
        }


    }
}