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
        // GET: Sales
        [HttpGet]
        public ActionResult Index()
        {
            
            
            return View();
        }
        [HttpGet]
        public ActionResult Orders()
        {
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
        [HttpPost]
        public ActionResult Index(NewOrderModel model)
        {
            if (ModelState.IsValid)
            {
                Order order = new Order()
                {
                    UserEmail = User.Identity.Name,
                    Detail = new OrderDetail() { Pieces = model.Pieces, ProductId = 1 }
                };
                Result resultApi = new Result();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUrl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage res = client.PostAsJsonAsync("api/Sales/NewOrder/", order).Result;
                    if (res.IsSuccessStatusCode)
                    {
                        var response = res.Content.ReadAsStringAsync().Result;
                        resultApi = JsonConvert.DeserializeObject<Result>(response);
                        if (resultApi.Status == ResultStatus.Ok)
                        {
                            //resultApi = JsonConvert.DeserializeObject<Result>(resultApi.ObjectResult.ToString());
                            string message = resultApi.Message;
                        }
                    }
                }
            }

            return View(model);
        }

    }
}