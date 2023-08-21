using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Newtonsoft.Json;
using ParulBeautyCare.GeneralClasses;
using ParulBeautyCareDbClasses.DataModels;
using ParulBeautyCareViewModel.ViewModel;

namespace ParulBeautyCare.Controllers
{
    public class HomeController : GeneralClass
    {
        public ActionResult Index()
        {
            return View();
        }

        #region ==> Error Pages
        public ActionResult Error500()
        {

            return View();
        }

        public ActionResult Error404()
        {
            return View();
        }
        public ActionResult ErrorDefault()
        {
            return View();
        }
        #endregion

        #region ==> Login
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
                    FormsAuthentication.SetAuthCookie(lm.UserName, false);
                    FormsAuthentication.RedirectFromLoginPage(lm.UserName, true);
                    //FormsAuthentication.RedirectFromLoginPage(lm.UserName, true);
                    var Staff = ApiCall.PostApi("LoginUserRtr", Newtonsoft.Json.JsonConvert.SerializeObject(lm));
                    lm = JsonConvert.DeserializeObject<LoginViewModel>(Staff);       
                    //LoginMaster um = db.LoginMasters.Where(x => x.UserName == lm.UserName && x.Password == lm.Password).FirstOrDefault();

                    HttpCookie LoginMaster = new HttpCookie("LoginMaster");
                    if (lm.UserName != null)
                    {
                        LoginMaster["UserName"] = lm.LoginUserList.FirstOrDefault().Username;
                        LoginMaster["RoleId"] = lm.LoginUserList.FirstOrDefault().RoleId.ToString();
                        LoginMaster["CompanyCode"] = lm.LoginUserList.FirstOrDefault().Companycode.ToString();
                    }
                    Response.Cookies.Add(LoginMaster);
                    return RedirectToAction("Dashboard", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid UserName or Password");
                    return View("Login");
                }
            }
            catch (Exception )
            {
                //Danger(ex.Message.ToString(), true);
                return RedirectToAction("login", "Home");
            }
        }
        #endregion

        #region => Demo Pages 
        public ActionResult Dashboard()
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    Response.Redirect("Home/Login");
                   // FormsAuthentication.RedirectToLoginPage();
                }
                HttpCookie reqCookies = Request.Cookies["LoginMaster"];
                if (reqCookies != null)
                {
                    return View();
                }
                else
                {
                   return RedirectToAction("Login","Home");
                }
            }
            catch(Exception)
            {
                if (Request.Cookies["LoginMaster"] != null)
                {
                    Response.Cookies["LoginMaster"].Expires = DateTime.Now.AddDays(-1);
                }
                FormsAuthentication.SignOut();
                return RedirectToAction("Login", "Home");
            }
        }
      
        #endregion

        #region => Menu

        public ActionResult GetMenu()
        {
            try
            {
                MenuViewModel  mv = new MenuViewModel();
                HttpCookie reqCookies = Request.Cookies["LoginMaster"];
                if (reqCookies != null)
                {
                    mv.RoleId = Convert.ToInt32(reqCookies["RoleId"]);
                    var emplog = ApiCall.PostApi("MenuRtr", Newtonsoft.Json.JsonConvert.SerializeObject(mv));
                    mv = JsonConvert.DeserializeObject<MenuViewModel>(emplog);
                }
                return View("Menu", mv.MenuList);
            }
            catch (Exception ex)
            {
                var error = ex.Message.ToString();
                return Content("~/Home/Error404");
            }
        }
        #endregion

        #region ==> Enquiry 
        public ActionResult Enquiry()
        {
            try
            {
               return View();
               
            }
            catch (Exception)
            {
                return RedirectToAction("Login", "Home");
            }
        }
       
        [HttpPost]
        public ActionResult Enquiry(EnquiryViewModel evm)
        {
            try
            {
                //Rights checking
                if (!User.Identity.IsAuthenticated)
                {
                    FormsAuthentication.RedirectToLoginPage();
                }
                MenuRightsViewModel mv1 = new MenuRightsViewModel();
                mv1.Usercode = LoggedUserDetails.UserName;
                string url = generalFunctions.getCommon(Request.Url.AbsoluteUri);
                mv1.PageName = url;
                var MenuRtr = ApiCall.PostApi("MenuRightsRtr", Newtonsoft.Json.JsonConvert.SerializeObject(mv1));
                mv1 = JsonConvert.DeserializeObject<MenuRightsViewModel>(MenuRtr);
                if (mv1.MenuRightsList.Count > 0)
                {
                    //ViewBag.ViewRight = mv1.MenuRightsList.FirstOrDefault().ViewRight;
                    //ViewBag.InsertRight = mv1.MenuRightsList.FirstOrDefault().InsertRight;
                    //ViewBag.UpdateRight = mv1.MenuRightsList.FirstOrDefault().UpdateRight;
                    //ViewBag.DeleteRight = mv1.MenuRightsList.FirstOrDefault().DeleteRight;
                    TempData["ViewRight"] = mv1.MenuRightsList.FirstOrDefault().ViewRight;
                    TempData["InsertRight"] = mv1.MenuRightsList.FirstOrDefault().InsertRight;
                    TempData["UpdateRight"] = mv1.MenuRightsList.FirstOrDefault().UpdateRight;
                    TempData["DeleteRight"] = mv1.MenuRightsList.FirstOrDefault().DeleteRight;
                }
                else
                {
                    var data = new { Message = "Sorry,You have no rights to access this page", Type = "error" };
                    TempData["SweetAlert"] = data;
                    return RedirectToAction("Dashboard", "Home");
                }

                evm.CreateDate = generalFunctions.getTimeZoneDatetimedb();
                evm.Action = "insert";
                evm.CreateUser = User.Identity.Name;
                var emplog = ApiCall.PostApi("EnquiryIns", Newtonsoft.Json.JsonConvert.SerializeObject(evm));
                evm = JsonConvert.DeserializeObject<EnquiryViewModel>(emplog);
                string msg = evm.result;
                if (msg.Contains("successfully"))
                {
                    var data = new { Message = msg, Type = "success" };
                    TempData["SweetAlert"] = data;
                    return RedirectToAction("Enquiry", "Home");
                }
                else
                {
                    var data = new { Message = msg, Type = "error" };
                    TempData["SweetAlert"] = data;
                    return RedirectToAction("Enquiry", "Home");
                }
               
            }
            catch (Exception)
            {
                return RedirectToAction("Login", "Home");
            }
        }
        #endregion
    }
}