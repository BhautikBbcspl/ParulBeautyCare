using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Newtonsoft.Json;
using ParulBeautyCareDbClasses.DataModels;
using ParulBeautyCareViewModel.ViewModel;

namespace ParulBeautyCare.Controllers
{
    public class HomeController : Controller
    {
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

        #region ==>Login
        public ActionResult Login()
        {
            HttpCookie reqCookies = Request.Cookies["LoginMaster"];
            if (reqCookies != null)
            {
                return View();
            }
            return View();
        }
        public ActionResult Login1()
        {
            HttpCookie reqCookies = Request.Cookies["LoginMaster"];
            if (reqCookies != null)
            {
                return View();
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel lm)
        {
            try
            {
                var emplog = ApiCall.PostApi("LoginCheckGet", Newtonsoft.Json.JsonConvert.SerializeObject(lm));
                lm = JsonConvert.DeserializeObject<LoginViewModel>(emplog);
                if (lm.result.Equals("1"))
                {
                    FormsAuthentication.RedirectFromLoginPage(lm.UserName, true);
                    var Staff = ApiCall.PostApi("LoginUserRtr", Newtonsoft.Json.JsonConvert.SerializeObject(lm));
                    lm = JsonConvert.DeserializeObject<LoginViewModel>(Staff);                    //LoginMaster um = db.LoginMasters.Where(x => x.UserName == lm.UserName && x.Password == lm.Password).FirstOrDefault();

                    HttpCookie LoginMaster = new HttpCookie("LoginMaster");
                    if (lm.UserName != null)
                    {
                        LoginMaster["UserName"] = lm.LoginUserList.FirstOrDefault().Username;
                        LoginMaster["RoleId"] = lm.LoginUserList.FirstOrDefault().RoleId.ToString();
                    }
                    Response.Cookies.Add(LoginMaster);
                    return RedirectToAction("Dashboard", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid UserName or Password");
                    return View("Login1");
                }
            }
            catch (Exception ex)
            {
                //Danger(ex.Message.ToString(), true);
                return RedirectToAction("Dashboard", "Home");
            }
        }
        #endregion

        #region => Dashboard
        public ActionResult Dashboard()
        {
            HttpCookie reqCookies = Request.Cookies["LoginMaster"];
            if (reqCookies != null)
            {
                return View();
            }
            return View();
        }
        #endregion
    }
}