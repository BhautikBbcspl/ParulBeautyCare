using Newtonsoft.Json;
using ParulBeautyCare.GeneralClasses;
using ParulBeautyCareDbClasses.DataModels;
using ParulBeautyCareViewModel.ViewModel;
using ParulBeautyCareViewModel.ViewModel.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ParulBeautyCare.Controllers
{
    public class UserManagementController : GeneralClass
    {

        #region==> Change Status
        public ActionResult ChangeStatus(string Code, string status, string Type)
        {
            HttpCookie reqCookies = Request.Cookies["LoginMaster"];
            string msg = ""; try
            {
                if (Type == "ModuleMaster")
                {
                    ModuleViewModel sm = new ModuleViewModel();
                    sm.Action = "Active";
                    sm.UpdateDate = generalFunctions.getTimeZoneDatetimedb();
                    sm.UpdateUser = User.Identity.Name;
                    sm.ModuleId = Code;
                    sm.IsActive = status.Equals("true") ? "1" : "0";
                    var emplog = ApiCall.PostApi("ModuleMasterInsUpd", Newtonsoft.Json.JsonConvert.SerializeObject(sm));
                    sm = JsonConvert.DeserializeObject<ModuleViewModel>(emplog);
                    msg = sm.result;
                }
                else if (Type == "PageMaster")
                {
                    PageViewModel sm = new PageViewModel();
                    sm.Action = "Active";
                    sm.UpdateDate = generalFunctions.getTimeZoneDatetimedb();
                    sm.UpdateUser = User.Identity.Name;
                    sm.PageId = Code;
                    sm.IsActive = status.Equals("true") ? "1" : "0";
                    var emplog = ApiCall.PostApi("PageMasterInsUpd", Newtonsoft.Json.JsonConvert.SerializeObject(sm));
                    sm = JsonConvert.DeserializeObject<PageViewModel>(emplog);
                    msg = sm.result;
                }
                else if (Type == "PageModuleMaster")
                {
                    PageModuleViewModel sm = new PageModuleViewModel();
                    sm.Action = "Active";
                    sm.UpdateDate = generalFunctions.getTimeZoneDatetimedb();
                    sm.UpdateUser = User.Identity.Name;
                    sm.InteId = Code;
                    sm.IsActive = status.Equals("true") ? "1" : "0";
                    var emplog = ApiCall.PostApi("IntePageModuleInsUpd", Newtonsoft.Json.JsonConvert.SerializeObject(sm));
                    sm = JsonConvert.DeserializeObject<PageModuleViewModel>(emplog);
                    msg = sm.result;
                }
                else if (Type == "InteRoleModule")
                {
                    RoleModuleViewModel sm = new RoleModuleViewModel();
                    sm.Action = "Active";
                    sm.UpdateDate = generalFunctions.getTimeZoneDatetimedb();
                    sm.UpdateUser = User.Identity.Name;
                    sm.InteId = Code;
                    sm.IsActive = status.Equals("true") ? "1" : "0";
                    var emplog = ApiCall.PostApi("InteRoleModuleInsUpd", Newtonsoft.Json.JsonConvert.SerializeObject(sm));
                    sm = JsonConvert.DeserializeObject<RoleModuleViewModel>(emplog);
                    msg = sm.result;
                }

                else
                {
                    msg = "Did not have method";
                }
            }
            catch (Exception ex)
            {
                //  Danger(ex.Message.ToString(), true);
                var data = new { Message = ex.Message.ToString(), Type = "error" };
                TempData["SweetAlert"] = data;
                return RedirectToAction("Dashboard", "Home");
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region ==> ModuleMaster
        public ActionResult ViewModuleMaster()
        {
            try
            {
                HttpCookie reqCookies = Request.Cookies["LoginMaster"];
                ModuleViewModel mv = new ModuleViewModel();
                mv.Action = "All";
                if (!User.Identity.IsAuthenticated)
                {
                    return RedirectToAction("Login", "Home");
                }
                string url = generalFunctions.getCommon(Request.Url.AbsoluteUri);
                MenuRightsViewModel mv1 = new MenuRightsViewModel();
                mv1.Usercode = reqCookies["UserName"].ToString();
                mv1.PageName = url;
                var MenuRtr = ApiCall.PostApi("MenuRightsRtr", Newtonsoft.Json.JsonConvert.SerializeObject(mv1));
                mv1 = JsonConvert.DeserializeObject<MenuRightsViewModel>(MenuRtr);
                if (mv1.MenuRightsList.Count>0)
                {
                    ViewBag.ViewRight = mv1.MenuRightsList.FirstOrDefault().ViewRight;
                    ViewBag.InsertRight = mv1.MenuRightsList.FirstOrDefault().InsertRight;
                    ViewBag.UpdateRight = mv1.MenuRightsList.FirstOrDefault().UpdateRight;
                    ViewBag.DeleteRight = mv1.MenuRightsList.FirstOrDefault().DeleteRight;
                }
                else
                {
                    var data = new { Message = "Sorry,You have no rights to access this page", Type = "error" };
                    TempData["SweetAlert"] = data;
                    return RedirectToAction("Dashboard", "Home");
                }
                if (ViewBag.ViewRight == 1)
                {
                var ModelData = ApiCall.PostApi("ModuleMasterRtr", Newtonsoft.Json.JsonConvert.SerializeObject(mv));
                mv = JsonConvert.DeserializeObject<ModuleViewModel>(ModelData);
                return View(mv.ModuleViewList);
                }
                else
                {
                    return RedirectToAction("Dashboard", "Home");
                }
            }
            catch (Exception ex)
            {
                //Danger(ex.Message.ToString(), true);
                //return RedirectToAction("Dashboard", "Home");
                var data = new { Message = ex.Message.ToString(), Type = "error" };
                TempData["SweetAlert"] = data;
                return RedirectToAction("Dashboard", "Home");
            }

        }
        public ActionResult AddModuleMaster()
        {
            try
            {
                HttpCookie reqCookies = Request.Cookies["EmployeeMaster"];
                if (!User.Identity.IsAuthenticated)
                {
                    return RedirectToAction("Login", "Home");
                }
                ModuleViewModel sb = new ModuleViewModel();
                sb.ModuleId = "0";

                ViewBag.action = "Add";
                return View(sb);
            }
            catch (Exception ex)
            {
                var data = new { Message = ex.Message.ToString(), Type = "error" };
                TempData["SweetAlert"] = data;
                return RedirectToAction("Dashboard", "Home");
            }
        }
        [HttpPost]
        public ActionResult AddModuleMaster(ModuleViewModel sm)
        {
            try
            {
                if (sm.ModuleId == null || sm.ModuleId == "0")
                {
                    sm.CreateDate = generalFunctions.getTimeZoneDatetimedb();
                    sm.Action = "insert";
                    sm.CreateUser = User.Identity.Name;
                    var emplog = ApiCall.PostApi("ModuleMasterInsUpd", Newtonsoft.Json.JsonConvert.SerializeObject(sm));
                    sm = JsonConvert.DeserializeObject<ModuleViewModel>(emplog);
                    string msg = sm.result;
                    if (msg.Contains("successfully"))
                    {
                        //ViewBag.Message = msg.ToUpper();
                        //Success(msg, true);
                        var data = new { Message = msg, Type = "success" };
                        TempData["SweetAlert"] = data;
                        return RedirectToAction("AddModuleMaster", "UserManagement");
                    }
                    else
                    {
                        //ViewBag.Message = msg.ToUpper();
                        //Danger(msg, true);
                        var data = new { Message = msg, Type = "error" };
                        TempData["SweetAlert"] = data;
                        return RedirectToAction("AddModuleMaster", "UserManagement");
                    }
                }
                else
                {
                    sm.Action = "update";
                    sm.CreateDate = generalFunctions.getTimeZoneDatetimedb();
                    sm.CreateUser = User.Identity.Name;
                    sm.UpdateDate = generalFunctions.getTimeZoneDatetimedb();
                    sm.UpdateUser = User.Identity.Name;
                    var emplog = ApiCall.PostApi("ModuleMasterInsUpd", Newtonsoft.Json.JsonConvert.SerializeObject(sm));
                    sm = JsonConvert.DeserializeObject<ModuleViewModel>(emplog);
                    string msg = sm.result;
                    if (msg.Contains("successfully"))
                    {
                        var data = new { Message = msg, Type = "success" };
                        TempData["SweetAlert"] = data;
                        return RedirectToAction("AddModuleMaster", "UserManagement");
                    }
                    else
                    {
                        var data = new { Message = msg, Type = "error" };
                        TempData["SweetAlert"] = data;
                        return RedirectToAction("AddModuleMaster", "UserManagement");
                    }
                }
                
            }
            catch (Exception ex)
            {
                //Danger(ex.Message.ToString(), true);
                //return RedirectToAction("Dashboard", "Home");
                var data = new { Message = ex.Message.ToString(), Type = "error" };
                TempData["SweetAlert"] = data;
                return RedirectToAction("AddModuleMaster", "UserManagement");
            }
        }

        public ActionResult EditModuleMaster(int id)
        {
            HttpCookie reqCookies = Request.Cookies["EmployeeMaster"];
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return RedirectToAction("Login", "Home");
                }
                ModuleViewModel sb = new ModuleViewModel();
                sb.ModuleId = id.ToString();
                sb.Action = "GetModule";
                var emplog = ApiCall.PostApi("ModuleMasterRtr", Newtonsoft.Json.JsonConvert.SerializeObject(sb));
                sb = JsonConvert.DeserializeObject<ModuleViewModel>(emplog);
                sb.ModuleId = sb.ModuleViewList.FirstOrDefault().ModuleId.ToString();
                sb.ModuleName = sb.ModuleViewList.FirstOrDefault().ModuleName.ToString();
                sb.ModulePriority = sb.ModuleViewList.FirstOrDefault().ModulePriority.ToString();
                sb.selfPage = Convert.ToBoolean(sb.ModuleViewList.FirstOrDefault().IsSelfURL);
                sb.FaIcon = sb.ModuleViewList.FirstOrDefault().FaIcon.ToString();
                sb.IsActive = sb.ModuleViewList.FirstOrDefault().IsActive.ToString();
                ViewBag.action = "Update";

                return View("AddModuleMaster", sb);
            }
            catch (Exception ex)
            {
                //Danger(ex.Message.ToString(), true);
                //return RedirectToAction("Dashboard", "Home");
                var data = new { Message = ex.Message.ToString(), Type = "error" };
                TempData["SweetAlert"] = data;
                return RedirectToAction("ViewModuleMaster", "UserManagement");
            }
        }

        #endregion

        #region ==> PageMaster
        public ActionResult ViewPageMaster()
        {
            try
            {
                HttpCookie reqCookies = Request.Cookies["LoginMaster"];
                if (!User.Identity.IsAuthenticated)
                {
                    return RedirectToAction("Login", "Home");
                }
                PageViewModel mv = new PageViewModel();
                string url = generalFunctions.getCommon(Request.Url.AbsoluteUri);
                MenuRightsViewModel mv1 = new MenuRightsViewModel();
                mv1.Usercode = reqCookies["UserName"].ToString();
                mv1.PageName = url;
                var MenuRtr = ApiCall.PostApi("MenuRightsRtr", Newtonsoft.Json.JsonConvert.SerializeObject(mv1));
                mv1 = JsonConvert.DeserializeObject<MenuRightsViewModel>(MenuRtr);
                if (mv1.MenuRightsList.Count>0)
                {
                    ViewBag.ViewRight = mv1.MenuRightsList.FirstOrDefault().ViewRight;
                    ViewBag.InsertRight = mv1.MenuRightsList.FirstOrDefault().InsertRight;
                    ViewBag.UpdateRight = mv1.MenuRightsList.FirstOrDefault().UpdateRight;
                    ViewBag.DeleteRight = mv1.MenuRightsList.FirstOrDefault().DeleteRight;
                }
                else
                {
                    var data = new { Message = "Sorry,You have no rights to access this page", Type = "error" };
                    TempData["SweetAlert"] = data;
                    return RedirectToAction("Dashboard", "Home");
                }
                mv.Action = "All";
                var emplog = ApiCall.PostApi("PageMasterRtr", Newtonsoft.Json.JsonConvert.SerializeObject(mv));
                mv = JsonConvert.DeserializeObject<PageViewModel>(emplog);
                return View(mv.PageMasterList);

            }
            catch (Exception ex)
            {
                //Danger(ex.Message.ToString(), true);
                //return RedirectToAction("Dashboard", "Home");
                var data = new { Message = ex.Message.ToString(), Type = "error" };
                TempData["SweetAlert"] = data;
                return RedirectToAction("Dashboard", "Home");
            }
        }
        public ActionResult AddPageMaster()
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return RedirectToAction("Login", "Home");
                }
                PageViewModel sb = new PageViewModel();
                sb.PageId = "0";
                ViewBag.action = "Add";
                sb.Action = "Save";
                return View(sb);
            }
            catch (Exception ex)
            {
                //Danger(ex.Message.ToString(), true);
                //return RedirectToAction("Dashboard", "Home");
                var data = new { Message = ex.Message.ToString(), Type = "error" };
                TempData["SweetAlert"] = data;
                return RedirectToAction("Dashboard", "Home");
            }
        }
        [HttpPost]
        public ActionResult AddPageMaster(PageViewModel sm)
        {
            try
            {
                if (sm.PageId == null || sm.PageId=="0")
                {
                    sm.CreateDate = generalFunctions.getTimeZoneDatetimedb();
                    sm.Action = "insert";
                    sm.CreateUser = User.Identity.Name;
                    var emplog = ApiCall.PostApi("PageMasterInsUpd", Newtonsoft.Json.JsonConvert.SerializeObject(sm));
                    sm = JsonConvert.DeserializeObject<PageViewModel>(emplog);
                    string msg = sm.result;
                    if (msg.Contains("successfully"))
                    {
                        //ViewBag.Message = msg.ToUpper();
                        //Success(msg, true);
                        //return RedirectToAction("ViewPageMaster", "UserManagement");
                        var data = new { Message = msg, Type = "success" };
                        TempData["SweetAlert"] = data;
                        return RedirectToAction("AddPageMaster", "UserManagement");
                    }
                    else
                    {
                        //ViewBag.Message = msg.ToUpper();
                        //Danger(msg, true);
                        //return View(sm);
                        var data = new { Message = msg, Type = "error" };
                        TempData["SweetAlert"] = data;
                        return RedirectToAction("AddPageMaster", "UserManagement");
                    }
                }
                else
                {
                    sm.Action = "Update";
                    sm.CreateDate = generalFunctions.getTimeZoneDatetimedb();
                    sm.CreateUser = User.Identity.Name;
                    sm.UpdateDate = generalFunctions.getTimeZoneDatetimedb();
                    sm.UpdateUser = User.Identity.Name;
                    var emplog = ApiCall.PostApi("PageMasterInsUpd", Newtonsoft.Json.JsonConvert.SerializeObject(sm));
                    sm = JsonConvert.DeserializeObject<PageViewModel>(emplog);
                    string msg = sm.result;
                    if (msg.Contains("successfully"))
                    {
                        //ViewBag.Message = msg.ToUpper();
                        //Success(msg, true);
                        //return RedirectToAction("ViewPageMaster", "UserManagement");
                        var data = new { Message = msg, Type = "success" };
                        TempData["SweetAlert"] = data;
                        return RedirectToAction("AddPageMaster", "UserManagement");
                    }
                    else
                    {
                        //ViewBag.Message = msg.ToUpper();
                        //Danger(msg, true);
                        //return View(sm);
                        var data = new { Message = msg, Type = "error" };
                        TempData["SweetAlert"] = data;
                        return RedirectToAction("AddPageMaster", "UserManagement");
                    }
                }
            }
            catch (Exception ex)
            {
                //Danger(ex.Message.ToString(), true);
                //return RedirectToAction("Dashboard", "Home");
                var data = new { Message = ex.Message.ToString(), Type = "error" };
                TempData["SweetAlert"] = data;
                return RedirectToAction("AddPageMaster", "UserManagement");
            }
        }

        public ActionResult EditPageMaster(int ID)
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return RedirectToAction("Login", "Home");
                }
                PageViewModel sb = new PageViewModel();
                sb.Action = "GetPage";
                sb.PageId = ID.ToString();
                var emplog = ApiCall.PostApi("PageMasterRtr", Newtonsoft.Json.JsonConvert.SerializeObject(sb));
                sb = JsonConvert.DeserializeObject<PageViewModel>(emplog);
                sb.PageId = sb.PageMasterList.FirstOrDefault().PageId.ToString();
                sb.PageName = sb.PageMasterList.FirstOrDefault().PageName;
                sb.PageUrl = sb.PageMasterList.FirstOrDefault().PageUrl;
                sb.PagePriority = sb.PageMasterList.FirstOrDefault().PagePriority.ToString();
                sb.IsActive = sb.PageMasterList.FirstOrDefault().IsActive.ToString();
                ViewBag.action = "Update";
                sb.Action = "Update";
                return View("AddPageMaster", sb);
            }
            catch (Exception ex)
            {
                //Danger(ex.Message.ToString(), true);
                //return RedirectToAction("Dashboard", "Home");
                var data = new { Message = ex.Message.ToString(), Type = "error" };
                TempData["SweetAlert"] = data;
                return RedirectToAction("ViewPageMaster", "UserManagement");
            }
        }
        #endregion

        #region==> Page Module Integration
        public ActionResult ViewPageModuleMaster()
        {
            try
            {
                HttpCookie reqCookies = Request.Cookies["LoginMaster"];
                PageModuleViewModel pmv = new PageModuleViewModel();
                pmv.Action = "all";
                if (!User.Identity.IsAuthenticated)
                {
                    return RedirectToAction("Login", "Home");
                }
                string url = generalFunctions.getCommon(Request.Url.AbsoluteUri);
                MenuRightsViewModel mv1 = new MenuRightsViewModel();
                mv1.Usercode = reqCookies["UserName"].ToString();
                mv1.PageName = url;
                var MenuRtr = ApiCall.PostApi("MenuRightsRtr", Newtonsoft.Json.JsonConvert.SerializeObject(mv1));
                mv1 = JsonConvert.DeserializeObject<MenuRightsViewModel>(MenuRtr);
                if (mv1.MenuRightsList.Count>0)
                {
                    ViewBag.ViewRight = mv1.MenuRightsList.FirstOrDefault().ViewRight;
                    ViewBag.InsertRight = mv1.MenuRightsList.FirstOrDefault().InsertRight;
                    ViewBag.UpdateRight = mv1.MenuRightsList.FirstOrDefault().UpdateRight;
                    ViewBag.DeleteRight = mv1.MenuRightsList.FirstOrDefault().DeleteRight;
                }
                else
                {
                    var data = new { Message = "Sorry,You have no rights to access this page", Type = "error" };
                    TempData["SweetAlert"] = data;
                    return RedirectToAction("Dashboard", "Home");
                }
                var emplog = ApiCall.PostApi("IntePageModuleRtr", Newtonsoft.Json.JsonConvert.SerializeObject(pmv));
                pmv = JsonConvert.DeserializeObject<PageModuleViewModel>(emplog);
                return View(pmv.PageModuleInteList);
            }
            catch (Exception ex)
            {
                //Danger(ex.Message.ToString(), true);
                //return RedirectToAction("Dashboard", "Home");
                var data = new { Message = ex.Message.ToString(), Type = "error" };
                TempData["SweetAlert"] = data;
                return RedirectToAction("Dashboard", "Home");
            }
        }
        public ActionResult AddPageModuleMaster()
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return RedirectToAction("Login", "Home");
                }
                PageModuleViewModel sb = new PageModuleViewModel();
                sb.Action = "insert";
                ModuleViewModel mv = new ModuleViewModel();
                mv.Action = "active";
                var emplog = ApiCall.PostApi("ModuleMasterRtr", Newtonsoft.Json.JsonConvert.SerializeObject(mv));
                mv = JsonConvert.DeserializeObject<ModuleViewModel>(emplog);
                sb.ModulesList = mv.ModuleViewList;
                PageViewModel pv = new PageViewModel();
                pv.Action = "active";
                var emplog1 = ApiCall.PostApi("PageMasterRtr", Newtonsoft.Json.JsonConvert.SerializeObject(pv));
                pv = JsonConvert.DeserializeObject<PageViewModel>(emplog1);
                sb.PagesList = pv.PageMasterList;
                return View(sb);
            }
            catch (Exception ex)
            {
                //Danger(ex.Message.ToString(), true);
                //return RedirectToAction("Dashboard", "Home");
                var data = new { Message = ex.Message.ToString(), Type = "error" };
                TempData["SweetAlert"] = data;
                return RedirectToAction("Dashboard", "Home");
            }
        }
        [HttpPost]
        public ActionResult AddPageModuleMaster(PageModuleViewModel sm)
        {
            try
            {

                sm.CreateDate = generalFunctions.getTimeZoneDatetimedb();
                sm.Action = "insert";
                sm.CreateUser = User.Identity.Name;
                var emplog = ApiCall.PostApi("IntePageModuleInsUpd", Newtonsoft.Json.JsonConvert.SerializeObject(sm));
                sm = JsonConvert.DeserializeObject<PageModuleViewModel>(emplog);
                string msg = sm.result;
                if (msg.Contains("successfully"))
                {
                    //ViewBag.Message = msg.ToUpper();
                    //Success(msg, true);
                    //return RedirectToAction("ViewPageModuleMaster", "UserManagement");
                    var data = new { Message = msg, Type = "success" };
                    TempData["SweetAlert"] = data;
                    return RedirectToAction("AddPageModuleMaster", "UserManagement");
                }
                else
                {
                    //ViewBag.Message = msg.ToUpper();
                    //Danger(msg, true);
                    //return View(sm);
                    var data = new { Message = msg, Type = "error" };
                    TempData["SweetAlert"] = data;
                    return RedirectToAction("AddPageModuleMaster", "UserManagement");
                }
            }
            catch (Exception ex)
            {
                //Danger(ex.Message.ToString(), true);
                //return RedirectToAction("Dashboard", "Home");
                var data = new { Message = ex.Message.ToString(), Type = "error" };
                TempData["SweetAlert"] = data;
                return RedirectToAction("AddPageModuleMaster", "UserManagement");
            }
        }
        #endregion

        #region ==> Role Module Integration
        public ActionResult ViewRoleModuleMaster()
        {
            try
            {
                HttpCookie reqCookies = Request.Cookies["LoginMaster"];
                RoleModuleViewModel rmv = new RoleModuleViewModel();
                rmv.Action = "all";
                string url = generalFunctions.getCommon(Request.Url.AbsoluteUri);
                MenuRightsViewModel mv1 = new MenuRightsViewModel();
                mv1.Usercode = reqCookies["UserName"].ToString();
                mv1.PageName = url;
                var MenuRtr = ApiCall.PostApi("MenuRightsRtr", Newtonsoft.Json.JsonConvert.SerializeObject(mv1));
                mv1 = JsonConvert.DeserializeObject<MenuRightsViewModel>(MenuRtr);
                if (mv1.MenuRightsList.Count>0)
                {
                    ViewBag.ViewRight = mv1.MenuRightsList.FirstOrDefault().ViewRight;
                    ViewBag.InsertRight = mv1.MenuRightsList.FirstOrDefault().InsertRight;
                    ViewBag.UpdateRight = mv1.MenuRightsList.FirstOrDefault().UpdateRight;
                    ViewBag.DeleteRight = mv1.MenuRightsList.FirstOrDefault().DeleteRight;
                }
                else
                {
                    var data = new { Message = "Sorry,You have no rights to access this page", Type = "error" };
                    TempData["SweetAlert"] = data;
                    return RedirectToAction("Dashboard", "Home");
                }
                var emplog = ApiCall.PostApi("InteRoleModuleRtr", Newtonsoft.Json.JsonConvert.SerializeObject(rmv));
                rmv = JsonConvert.DeserializeObject<RoleModuleViewModel>(emplog);
                return View(rmv.RoleModuleInteList);

            }
            catch (Exception ex)
            {
                //Danger(ex.Message.ToString(), true);
                //return RedirectToAction("Dashboard", "Home");
                var data = new { Message = ex.Message.ToString(), Type = "error" };
                TempData["SweetAlert"] = data;
                return RedirectToAction("Dashboard", "Home");
            }
        }
        public ActionResult AddRoleModuleMaster()
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return RedirectToAction("Login", "Home");
                }
                RoleModuleViewModel sb = new RoleModuleViewModel();
                sb.Action = "insert";
                RoleMasterViewModel rv = new RoleMasterViewModel();
                rv.Action = "active";
                var emplog2 = ApiCall.PostApi("RoleMasterRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(rv));
                rv = JsonConvert.DeserializeObject<RoleMasterViewModel>(emplog2);
                sb.RoleList = rv.RoleMasterList;
                ModuleViewModel mv = new ModuleViewModel();
                mv.Action = "active";
                var emplog = ApiCall.PostApi("ModuleMasterRtr", Newtonsoft.Json.JsonConvert.SerializeObject(mv));
                mv = JsonConvert.DeserializeObject<ModuleViewModel>(emplog);
                sb.ModuleList = mv.ModuleViewList;
                PageViewModel pv = new PageViewModel();
                pv.Action = "active";
                var emplog1 = ApiCall.PostApi("PageMasterRtr", Newtonsoft.Json.JsonConvert.SerializeObject(pv));
                pv = JsonConvert.DeserializeObject<PageViewModel>(emplog1);
                sb.PageList = pv.PageMasterList;
                return View(sb);
            }
            catch (Exception ex)
            {
                //Danger(ex.Message.ToString(), true);
                //return RedirectToAction("Dashboard", "Home");
                var data = new { Message = ex.Message.ToString(), Type = "error" };
                TempData["SweetAlert"] = data;
                return RedirectToAction("Dashboard", "Home");
            }
        }
        [HttpPost]
        public ActionResult AddRoleModuleMaster(RoleModuleViewModel sm)
        {
            try
            {
                if (sm.Action == "insert")
                {
                    sm.CreateDate = generalFunctions.getTimeZoneDatetimedb();
                    sm.CreateUser = User.Identity.Name;
                    var emplog = ApiCall.PostApi("InteRoleModuleInsUpd", Newtonsoft.Json.JsonConvert.SerializeObject(sm));
                    sm = JsonConvert.DeserializeObject<RoleModuleViewModel>(emplog);
                    string msg = sm.result;
                    if (msg.Contains("successfully"))
                    {
                        //ViewBag.Message = msg.ToUpper();
                        //Success(msg, true);
                        //return RedirectToAction("ViewRoleModuleMaster", "UserManagement");
                        var data = new { Message = msg, Type = "success" };
                        TempData["SweetAlert"] = data;
                        return RedirectToAction("AddRoleModuleMaster", "UserManagement");
                    }
                    else
                    {
                        //ViewBag.Message = msg.ToUpper();
                        //Danger(msg, true);
                        //return View(sm);
                        var data = new { Message = msg, Type = "error" };
                        TempData["SweetAlert"] = data;
                        return RedirectToAction("AddRoleModuleMaster", "UserManagement");
                    }
                }
                else
                {
                    sm.CreateDate = generalFunctions.getTimeZoneDatetimedb();
                    sm.CreateUser = User.Identity.Name;
                    sm.UpdateDate = generalFunctions.getTimeZoneDatetimedb();
                    sm.UpdateUser = User.Identity.Name;
                    var emplog = ApiCall.PostApi("InteRoleModuleInsUpd", Newtonsoft.Json.JsonConvert.SerializeObject(sm));
                    sm = JsonConvert.DeserializeObject<RoleModuleViewModel>(emplog);
                    string msg = sm.result;
                    if (msg.Contains("successfully"))
                    {
                        //ViewBag.Message = msg.ToUpper();
                        //Success(msg, true);
                        //return RedirectToAction("ViewInteRoleModule", "UserManagement");
                        var data = new { Message = msg, Type = "success" };
                        TempData["SweetAlert"] = data;
                        return RedirectToAction("AddRoleModuleMaster", "UserManagement");
                    }
                    else
                    {
                        //ViewBag.Message = msg.ToUpper();
                        //Danger(msg, true);
                        //return View(sm);
                        var data = new { Message = msg, Type = "error" };
                        TempData["SweetAlert"] = data;
                        return RedirectToAction("AddRoleModuleMaster", "UserManagement");
                    }
                }
            }
            catch (Exception ex)
            {
                //Danger(ex.Message.ToString(), true);
                //return RedirectToAction("Dashboard", "Home");
                var data = new { Message = ex.Message.ToString(), Type = "error" };
                TempData["SweetAlert"] = data;
                return RedirectToAction("AddRoleModuleMaster", "UserManagement");
            }
        }
        public ActionResult SelectPageJson(int Moduleid)
        {
            try
            {
                PageModuleViewModel pv = new PageModuleViewModel();
                pv.Action = "active";
                pv.ModuleId = Moduleid.ToString();
                var emplog1 = ApiCall.PostApi("IntePageModuleRtr", Newtonsoft.Json.JsonConvert.SerializeObject(pv));
                pv = JsonConvert.DeserializeObject<PageModuleViewModel>(emplog1);
                var obj = new { pv.PageModuleInteList };
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion


    }
}