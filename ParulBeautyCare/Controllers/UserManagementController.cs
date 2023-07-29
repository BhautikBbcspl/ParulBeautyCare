using Newtonsoft.Json;
using ParulBeautyCare.GeneralClasses;
using ParulBeautyCareDbClasses.DataModels;
using ParulBeautyCareViewModel.ViewModel;
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
        #region ==> ModuleMaster
        public ActionResult ViewModuleMaster()
        {
            try
            {
                ModuleViewModel mv = new ModuleViewModel();
                mv.Action = "All";
                //if (!User.Identity.IsAuthenticated)
                //{
                //    FormsAuthentication.RedirectToLoginPage();
                //}
                string url = generalFunctions.getCommon(Request.Url.AbsoluteUri);
                //var re = db.SDHMenuRightsRtr(User.Identity.Name, url).FirstOrDefault();
                //if (re != null)
                //{
                //    ViewBag.ViewRight = re.ViewRight;
                //    ViewBag.InsertRight = re.InsertRight;
                //    ViewBag.UpdateRight = re.UpdateRight;
                //    ViewBag.DeleteRight = re.DeleteRight;
                //}
                //if (ViewBag.ViewRight == 1)
                //{
                var emplog = ApiCall.PostApi("ModuleMasterRtr", Newtonsoft.Json.JsonConvert.SerializeObject(mv));
                mv = JsonConvert.DeserializeObject<ModuleViewModel>(emplog);
                return View(mv.ModuleViewList);
                //}
                //else
                //{
                //    return RedirectToAction("Dashboard", "Home");
                //}
            }
            catch (Exception ex)
            {
                Danger(ex.Message.ToString(), true);
                return RedirectToAction("Dashboard", "Home");
            }

        }
        public ActionResult AddModuleMaster()
        {
            try
            {
                HttpCookie reqCookies = Request.Cookies["EmployeeMaster"];
                //if (!User.Identity.IsAuthenticated)
                //{
                //    FormsAuthentication.RedirectToLoginPage();
                //}
                ModuleViewModel sb = new ModuleViewModel();
                sb.ModuleId = "0";
                return View(sb);
            }
            catch (Exception ex)
            {
                Danger(ex.Message.ToString(), true);
                return RedirectToAction("Dashboard", "Home");
            }
        }
        [HttpPost]
        public ActionResult AddModuleMaster(ModuleViewModel sm)
        {
            try
            {
                if (sm.ModuleId == null)
                {
                    sm.CreateDate = generalFunctions.getTimeZoneDatetimedb();
                    sm.Action = "insert";
                    sm.CreateUser = User.Identity.Name;
                    var emplog = ApiCall.PostApi("ModuleMasterInsUpd", Newtonsoft.Json.JsonConvert.SerializeObject(sm));
                    sm = JsonConvert.DeserializeObject<ModuleViewModel>(emplog);
                    string msg = sm.result;
                    if (msg.Contains("successfully"))
                    {
                        ViewBag.Message = msg.ToUpper();
                        Success(msg, true);
                    }
                    else
                    {
                        ViewBag.Message = msg.ToUpper();
                        Danger(msg, true);
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
                        ViewBag.Message = msg.ToUpper();
                        Success(msg, true);
                    }
                    else
                    {
                        ViewBag.Message = msg.ToUpper();
                        Danger(msg, true);
                    }
                }
                return RedirectToAction("ViewModuleMaster", "UserManagement");
            }
            catch (Exception ex)
            {
                Danger(ex.Message.ToString(), true);
                return RedirectToAction("Dashboard", "Home");
            }
        }

        public ActionResult EditModuleMaster(int id)
        {
            HttpCookie reqCookies = Request.Cookies["EmployeeMaster"];
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    FormsAuthentication.RedirectToLoginPage();
                }
                ModuleViewModel sb = new ModuleViewModel();
                sb.ModuleId = id.ToString();
                sb.Action = "All";
                var emplog = ApiCall.PostApi("ModuleMasterRtr", Newtonsoft.Json.JsonConvert.SerializeObject(sb));
                sb = JsonConvert.DeserializeObject<ModuleViewModel>(emplog);
                sb.ModuleId = sb.ModuleViewList.FirstOrDefault().ModuleId.ToString();
                sb.ModuleName = sb.ModuleViewList.FirstOrDefault().ModuleName.ToString();
                sb.ModulePriority = sb.ModuleViewList.FirstOrDefault().ModulePriority.ToString();
                sb.selfPage = Convert.ToBoolean(sb.ModuleViewList.FirstOrDefault().IsSelfURL);
                sb.FaIcon = sb.ModuleViewList.FirstOrDefault().FaIcon.ToString();
                sb.IsActive = sb.ModuleViewList.FirstOrDefault().IsActive.ToString();
                return View("AddModuleMaster", sb);
            }
            catch (Exception ex)
            {
                Danger(ex.Message.ToString(), true);
                return RedirectToAction("Dashboard", "Home");
            }
        }

        #endregion

        #region ==> PageMaster
        public ActionResult ViewPageMaster()
        {
            try
            {
                //if (!User.Identity.IsAuthenticated)
                //{
                //    FormsAuthentication.RedirectToLoginPage();
                //}
                PageViewModel mv = new PageViewModel();
                mv.Action = "All";
                string url = generalFunctions.getCommon(Request.Url.AbsoluteUri);

                var emplog = ApiCall.PostApi("PageMasterRtr", Newtonsoft.Json.JsonConvert.SerializeObject(mv));
                mv = JsonConvert.DeserializeObject<PageViewModel>(emplog);
                return View(mv.PageMasterList);

            }
            catch (Exception ex)
            {
                Danger(ex.Message.ToString(), true);
                return RedirectToAction("Dashboard", "Home");
            }
        }
        public ActionResult AddPageMaster()
        {
            try
            {
                //if (!User.Identity.IsAuthenticated)
                //{
                //    FormsAuthentication.RedirectToLoginPage();
                //}
                PageViewModel sb = new PageViewModel();
                sb.PageId = "0";
                ViewBag.action = "Add";
                sb.Action = "Save";
                return View(sb);
            }
            catch (Exception ex)
            {
                Danger(ex.Message.ToString(), true);
                return RedirectToAction("Dashboard", "Home");
            }
        }
        [HttpPost]
        public ActionResult AddPageMaster(PageViewModel sm)
        {
            try
            {
                if (sm.PageId ==null)
                {
                    sm.CreateDate = generalFunctions.getTimeZoneDatetimedb();
                    sm.Action = "insert";
                    sm.CreateUser = User.Identity.Name;
                    var emplog = ApiCall.PostApi("PageMasterInsUpd", Newtonsoft.Json.JsonConvert.SerializeObject(sm));
                    sm = JsonConvert.DeserializeObject<PageViewModel>(emplog);
                    string msg = sm.result;
                    if (msg.Contains("successfully"))
                    {
                        ViewBag.Message = msg.ToUpper();
                        Success(msg, true);
                        return RedirectToAction("ViewPageMaster", "UserManagement");
                    }
                    else
                    {
                        ViewBag.Message = msg.ToUpper();
                        Danger(msg, true);
                        return View(sm);
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
                        ViewBag.Message = msg.ToUpper();
                        Success(msg, true);
                        return RedirectToAction("ViewPageMaster", "UserManagement");
                    }
                    else
                    {
                        ViewBag.Message = msg.ToUpper();
                        Danger(msg, true);
                        return View(sm);
                    }
                }
            }
            catch (Exception ex)
            {
                Danger(ex.Message.ToString(), true);
                return RedirectToAction("Dashboard", "Home");
            }
        }

        public ActionResult EditPageMaster(int ID)
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    FormsAuthentication.RedirectToLoginPage();
                }
                PageViewModel sb = new PageViewModel();
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
                Danger(ex.Message.ToString(), true);
                return RedirectToAction("Dashboard", "Home");
            }
        }
        #endregion
        #region==> Page Module Integration
        public ActionResult ViewPageModuleMaster()
        {
            try
            {
                PageModuleViewModel pmv = new PageModuleViewModel();
                pmv.Action = "all";
                //if (!User.Identity.IsAuthenticated)
                //{
                //    FormsAuthentication.RedirectToLoginPage();
                //}
                string url = generalFunctions.getCommon(Request.Url.AbsoluteUri);

                var emplog = ApiCall.PostApi("IntePageModuleRtr", Newtonsoft.Json.JsonConvert.SerializeObject(pmv));
                pmv = JsonConvert.DeserializeObject<PageModuleViewModel>(emplog);
                return View(pmv.PageModuleInteList);
            }
            catch (Exception ex)
            {
                Danger(ex.Message.ToString(), true);
                return RedirectToAction("Dashboard", "Home");
            }
        }
        public ActionResult AddPageModuleMaster()
        {
            try
            {
                //if (!User.Identity.IsAuthenticated)
                //{
                //    FormsAuthentication.RedirectToLoginPage();
                //}
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
                Danger(ex.Message.ToString(), true);
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
                    ViewBag.Message = msg.ToUpper();
                    Success(msg, true);
                    return RedirectToAction("ViewPageModuleMaster", "UserManagement");
                }
                else
                {
                    ViewBag.Message = msg.ToUpper();
                    Danger(msg, true);
                    return View(sm);
                }
            }
            catch (Exception ex)
            {
                Danger(ex.Message.ToString(), true);
                return RedirectToAction("Dashboard", "Home");
            }
        }
        #endregion

        #region ==> Role Module Integration
        public ActionResult ViewRoleModuleMaster()
        {
            try
            {
                RoleModuleViewModel rmv = new RoleModuleViewModel();
                rmv.Action = "all";

                var emplog = ApiCall.PostApi("InteRoleModuleRtr", Newtonsoft.Json.JsonConvert.SerializeObject(rmv));
                rmv = JsonConvert.DeserializeObject<RoleModuleViewModel>(emplog);
                return View(rmv.RoleModuleInteList);

            }
            catch (Exception ex)
            {
                Danger(ex.Message.ToString(), true);
                return RedirectToAction("Dashboard", "Home");
            }
        }
        public ActionResult AddRoleModuleMaster()
        {
            try
            {
                //if (!User.Identity.IsAuthenticated)
                //{
                //    FormsAuthentication.RedirectToLoginPage();
                //}
                RoleModuleViewModel sb = new RoleModuleViewModel();
                sb.Action = "insert";
                RoleViewModel rv = new RoleViewModel();
                rv.Action = "active";
                var emplog2 = ApiCall.PostApi("RoleMasterRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(rv));
                rv = JsonConvert.DeserializeObject<RoleViewModel>(emplog2);
                sb.RoleList = rv.RoleViewList; ModuleViewModel mv = new ModuleViewModel();
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
                Danger(ex.Message.ToString(), true);
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
                        ViewBag.Message = msg.ToUpper();
                        Success(msg, true);
                        return RedirectToAction("ViewRoleModuleMaster", "UserManagement");
                    }
                    else
                    {
                        ViewBag.Message = msg.ToUpper();
                        Danger(msg, true);
                        return View(sm);
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
                        ViewBag.Message = msg.ToUpper();
                        Success(msg, true);
                        return RedirectToAction("ViewInteRoleModule", "UserManagement");
                    }
                    else
                    {
                        ViewBag.Message = msg.ToUpper();
                        Danger(msg, true);
                        return View(sm);
                    }
                }
                return RedirectToAction("ViewInteRoleModule", "UserManagement");
            }
            catch (Exception ex)
            {
                Danger(ex.Message.ToString(), true);
                return RedirectToAction("Dashboard", "Home");
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