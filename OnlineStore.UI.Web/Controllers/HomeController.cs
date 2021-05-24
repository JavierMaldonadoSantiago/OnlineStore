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
    public class HomeController : Controller
    {
        string baseUrl = ConfigurationManager.AppSettings.Get("virtualPath").ToString();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";


            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public ActionResult Index(NewOrderModel model)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Customer");
            }
            if (ModelState.IsValid)
            {
                Order order = new Order()
                {
                    UserEmail = User.Identity.Name,
                    Detail = new OrderDetail() { Pieces = 1, ProductId = 1 }
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