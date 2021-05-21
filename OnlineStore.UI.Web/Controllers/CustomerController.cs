using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using OnlineStore.UI.Web.Models;
using OnlineStore.Security;
using OnlineStore.Entities;

namespace OnlineStore.UI.Web.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        [HttpGet]
        public ActionResult Login(string ReturnUrl = "")
        {
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel model, string ReturnUrl = "")
        {
            CustomMembershipProvider customMembershipProvider = new CustomMembershipProvider();
            if (ModelState.IsValid)
            {
                if (customMembershipProvider.ValidateUser(model.UserName, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, false);
                    return RedirectToLocal(ReturnUrl);
                }

            }
            ModelState.AddModelError("", "Error : El usuario o la clave no son válidos. ");
            return View(model);
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl) && returnUrl != "/")
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            CustomMembershipProvider customMembershipProvider = new CustomMembershipProvider();
            if (ModelState.IsValid)
            {
                Result result = customMembershipProvider.RegisterCustomer(model.GetUserSession());
                if (result.Status == ResultStatus.Ok)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Error : " + result.Message);
                    return View();
                }
                
            }
            ModelState.AddModelError("", "Error : El usuario o la clave no son válidos. ");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            string username = User.Identity.Name;
            var cacheKey = string.Format("UserData_{0}", username);
            HttpRuntime.Cache.Remove(cacheKey);
            Response.Cookies.Clear();
            FormsAuthentication.SignOut();
            Session.Clear();
            Session["CustomerId"] = null;

            return RedirectToAction("Index", "Home");
        }

    }
}