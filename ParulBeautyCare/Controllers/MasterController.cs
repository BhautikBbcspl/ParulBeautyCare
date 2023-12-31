﻿using Newtonsoft.Json;
using ParulBeautyCare.GeneralClasses;
using ParulBeautyCareDbClasses.DataModels;
using ParulBeautyCareViewModel.ViewModel;
using ParulBeautyCareViewModel.ViewModel.Master;
using ParulBeautyCareViewModel.ViewModel.StockMgmtViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ParulBeautyCare.Controllers
{
    public class MasterController : GeneralClass
    {
        parulbeautycareEntities db = new parulbeautycareEntities();
        #region==> Masters

        #region==> Change Status
        public ActionResult ChangeStatus(string Code, string status, string Type)
        {
            string msg = "";
            try
            {
                if (Type == "RoleMaster")
                {
                    RoleMasterViewModel sm = new RoleMasterViewModel();
                    sm.Action = "Active";
                    sm.UpdateDate = generalFunctions.getTimeZoneDatetimedb();
                    sm.UpdateUser = User.Identity.Name;
                    sm.RoleId = Code;
                    sm.IsActive = status.Equals("true") ? "1" : "0";
                    var emplog = ApiCall.PostApi("RoleMasterInsUpd", Newtonsoft.Json.JsonConvert.SerializeObject(sm));
                    sm = JsonConvert.DeserializeObject<RoleMasterViewModel>(emplog);
                    msg = sm.result;
                }
                else if (Type == "StatusMaster")
                {
                    StatusMasterViewModel sm = new StatusMasterViewModel();
                    sm.Action = "Active";
                    sm.UpdateDate = generalFunctions.getTimeZoneDatetimedb();
                    sm.UpdateUser = User.Identity.Name;
                    sm.StatusId = Code;
                    sm.IsActive = status.Equals("true") ? "1" : "0";
                    var emplog = ApiCall.PostApi("StatusMasterInsUpd", Newtonsoft.Json.JsonConvert.SerializeObject(sm));
                    sm = JsonConvert.DeserializeObject<StatusMasterViewModel>(emplog);
                    msg = sm.result;
                }
                else if (Type == "TimeSlotMaster")
                {
                    TimeSlotMasterViewModel sm = new TimeSlotMasterViewModel();
                    sm.Action = "Active";
                    sm.UpdateDate = generalFunctions.getTimeZoneDatetimedb();
                    sm.UpdateUser = User.Identity.Name;
                    sm.SlotId = Code;
                    sm.IsActive = status.Equals("true") ? "1" : "0";
                    var emplog = ApiCall.PostApi("TimeSlotMasterInsUpd", Newtonsoft.Json.JsonConvert.SerializeObject(sm));
                    sm = JsonConvert.DeserializeObject<TimeSlotMasterViewModel>(emplog);
                    msg = sm.result;
                }
                if (Type == "CategoryMaster")
                {
                    CategoryMasterViewModel sm = new CategoryMasterViewModel();
                    sm.Action = "Active";
                    sm.UpdateDate = generalFunctions.getTimeZoneDatetimedb();
                    sm.UpdateUser = User.Identity.Name;
                    sm.CategoryId = Code;
                    sm.IsActive = status.Equals("true") ? "1" : "0";
                    var emplog = ApiCall.PostApi("CategoryMasterInsUpd", Newtonsoft.Json.JsonConvert.SerializeObject(sm));
                    sm = JsonConvert.DeserializeObject<CategoryMasterViewModel>(emplog);
                    msg = sm.result;
                }
                if (Type == "StaffMaster")
                {
                    StaffMasterViewModel sm = new StaffMasterViewModel();
                    sm.Action = "Active";
                    sm.UpdateDate = generalFunctions.getTimeZoneDatetimedb();
                    sm.UpdateUser = User.Identity.Name;
                    sm.StaffId = Code;
                    sm.IsActive = status.Equals("true") ? "1" : "0";
                    var emplog = ApiCall.PostApi("StaffMasterInsUpd", Newtonsoft.Json.JsonConvert.SerializeObject(sm));
                    sm = JsonConvert.DeserializeObject<StaffMasterViewModel>(emplog);
                    msg = sm.result;
                }
                if (Type == "ProductTypeMaster")
                {
                    ProductTypeMasterViewModel sm = new ProductTypeMasterViewModel();
                    sm.Action = "Active";
                    sm.UpdateDate = generalFunctions.getTimeZoneDatetimedb();
                    sm.UpdateUser = User.Identity.Name;
                    sm.ProductTypeId = Code;
                    sm.IsActive = status.Equals("true") ? "1" : "0";
                    var emplog = ApiCall.PostApi("ProductTypeMasterInsUpd", Newtonsoft.Json.JsonConvert.SerializeObject(sm));
                    sm = JsonConvert.DeserializeObject<ProductTypeMasterViewModel>(emplog);
                    msg = sm.result;
                }
                if (Type == "ProductMaster")
                {
                    ProductMasterViewModel sm = new ProductMasterViewModel();
                    sm.Action = "Active";
                    sm.UpdateDate = generalFunctions.getTimeZoneDatetimedb();
                    sm.UpdateUser = User.Identity.Name;
                    sm.ProductId = Code;
                    sm.IsActive = status.Equals("true") ? "1" : "0";
                    var emplog = ApiCall.PostApi("ProductMasterInsUpd", Newtonsoft.Json.JsonConvert.SerializeObject(sm));
                    sm = JsonConvert.DeserializeObject<ProductMasterViewModel>(emplog);
                    msg = sm.result;
                }
                if (Type == "DepartmentMaster")
                {
                    DepartmentMasterViewModel sm = new DepartmentMasterViewModel();
                    sm.Action = "Active";
                    sm.UpdateDate = generalFunctions.getTimeZoneDatetimedb();
                    sm.UpdateUser = User.Identity.Name;
                    sm.DepartmentId = Code;
                    sm.IsActive = status.Equals("true") ? "1" : "0";
                    var emplog = ApiCall.PostApi("DepartmentMasterInsUpd", Newtonsoft.Json.JsonConvert.SerializeObject(sm));
                    sm = JsonConvert.DeserializeObject<DepartmentMasterViewModel>(emplog);
                    msg = sm.result;
                }
                if (Type == "SubCategoryMaster")
                {
                    SubCategoryMasterViewModel sm = new SubCategoryMasterViewModel();
                    sm.Action = "Active";
                    sm.UpdateDate = generalFunctions.getTimeZoneDatetimedb();
                    sm.UpdateUser = User.Identity.Name;
                    sm.SubCategoryId = Code;
                    sm.IsActive = status.Equals("true") ? "1" : "0";
                    var emplog = ApiCall.PostApi("SubCategoryMasterInsUpd", Newtonsoft.Json.JsonConvert.SerializeObject(sm));
                    sm = JsonConvert.DeserializeObject<SubCategoryMasterViewModel>(emplog);
                    msg = sm.result;
                }
                if (Type == "VendorMaster")
                {
                    VendorMasterViewModel vnd = new VendorMasterViewModel();
                    vnd.Action = "Active";
                    vnd.UpdateDate = generalFunctions.getTimeZoneDatetimedb();
                    vnd.UpdateUser = User.Identity.Name;
                    vnd.VendorId = Code;
                    vnd.IsActive = status.Equals("true") ? "1" : "0";
                    var emplog = ApiCall.PostApi("VendorMasterInsUpd", Newtonsoft.Json.JsonConvert.SerializeObject(vnd));
                    vnd = JsonConvert.DeserializeObject<VendorMasterViewModel>(emplog);
                    msg = vnd.result;
                }
                else if (Type == "PackageMaster")
                {
                    PackageMasterViewModel sm = new PackageMasterViewModel();
                    sm.Action = "Active";
                    sm.UpdateDate = generalFunctions.getTimeZoneDatetimedb();
                    sm.UpdateUser = User.Identity.Name;
                    sm.PackageId = Code;
                    sm.IsActive = status.Equals("true") ? "1" : "0";
                    var emplog = ApiCall.PostApi("PackageMasterInsUpd", Newtonsoft.Json.JsonConvert.SerializeObject(sm));
                    sm = JsonConvert.DeserializeObject<PackageMasterViewModel>(emplog);
                    msg = sm.result;
                }
                else if (Type == "IntePackageServiceMaster")
                {
                    IntePackageServiceMasterViewModel sm = new IntePackageServiceMasterViewModel();
                    sm.Action = "Active";
                    sm.UpdateDate = generalFunctions.getTimeZoneDatetimedb();
                    sm.UpdateUser = User.Identity.Name;
                    sm.IntePackageServiceId = Code;
                    sm.IsActive = status.Equals("true") ? "1" : "0";
                    var emplog = ApiCall.PostApi("IntePackageServiceMasterInsUpd", Newtonsoft.Json.JsonConvert.SerializeObject(sm));
                    sm = JsonConvert.DeserializeObject<IntePackageServiceMasterViewModel>(emplog);
                    msg = sm.result;
                }
                else if (Type == "InteServiceProductMaster")
                {
                    InteServiceProductMasterViewModel sm = new InteServiceProductMasterViewModel();
                    sm.Action = "Active";
                    sm.UpdateDate = generalFunctions.getTimeZoneDatetimedb();
                    sm.UpdateUser = User.Identity.Name;
                    sm.InteServiceProductId = Code;
                    sm.IsActive = status.Equals("true") ? "1" : "0";
                    var emplog = ApiCall.PostApi("InteServiceProductMasterInsUpd", Newtonsoft.Json.JsonConvert.SerializeObject(sm));
                    sm = JsonConvert.DeserializeObject<InteServiceProductMasterViewModel>(emplog);
                    msg = sm.result;
                }
                else if (Type == "ItemMaster")
                {
                    ItemMasterViewModel sm = new ItemMasterViewModel();
                    sm.Action = "Active";
                    sm.UpdateDate = generalFunctions.getTimeZoneDatetimedb();
                    sm.UpdateUser = User.Identity.Name;
                    sm.CreateUser = User.Identity.Name;
                    sm.ItemId = Code;
                    sm.IsActive = status.Equals("true") ? "1" : "0";
                    var emplog = ApiCall.PostApi("ItemMasterInsUpd", Newtonsoft.Json.JsonConvert.SerializeObject(sm));
                    sm = JsonConvert.DeserializeObject<ItemMasterViewModel>(emplog);
                    msg = sm.result;
                }
                else
                {
                    msg = "Did not have method";
                }

            }
            catch (Exception)
            {
                //Danger(ex.Message.ToString(), true);
                TempData["SweetAlert"] = new { Message = "An error occurred while updating the status.", Type = "error" };
                return RedirectToAction("Dashboard", "Home");
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region==> Role Master
        public ActionResult ViewRoleMaster()
        {
            try
            {
                //Rights checking
                if (!User.Identity.IsAuthenticated)
                {
                    return RedirectToAction("Login", "Home");
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
                //
                RoleMasterViewModel mv = new RoleMasterViewModel();
                mv.Action = "All";
                mv.CompanyCode = LoggedUserDetails.CompanyCode;

                var emplog = ApiCall.PostApi("RoleMasterRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(mv));
                mv = JsonConvert.DeserializeObject<RoleMasterViewModel>(emplog);
                return View(mv.RoleMasterList);

            }
            catch (Exception ex)
            {
                //Danger(ex.Message.ToString(), true);
                var data = new { Message = ex.Message.ToString(), Type = "error" };
                TempData["SweetAlert"] = data;
                return RedirectToAction("Dashboard", "Home");
            }
        }
        public ActionResult AddRoleMaster()
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return RedirectToAction("Login", "Home");
                }
                RoleMasterViewModel model = new RoleMasterViewModel();
                model.Action = "Add";
                return View(model);
            }
            catch (Exception ex)
            {
                var data = new { Message = ex.Message.ToString(), Type = "error" };
                TempData["SweetAlert"] = data;
                return RedirectToAction("Dashboard", "Home");
            }
        }
        [HttpPost]
        public ActionResult AddRoleMaster(RoleMasterViewModel sm)
        {
            try
            {
                if (sm.RoleId == null)
                {
                    sm.CreateDate = generalFunctions.getTimeZoneDatetimedb();
                    sm.Action = "insert";
                    sm.CreateUser = User.Identity.Name;
                    sm.UpdateUser = User.Identity.Name;
                    sm.CompanyCode = LoggedUserDetails.CompanyCode;
                    var emplog = ApiCall.PostApi("RoleMasterInsUpd", Newtonsoft.Json.JsonConvert.SerializeObject(sm));
                    sm = JsonConvert.DeserializeObject<RoleMasterViewModel>(emplog);
                    string msg = sm.result;
                    if (msg.Contains("successfully"))
                    {
                        var data = new { Message = msg, Type = "success" };
                        TempData["SweetAlert"] = data;
                        return RedirectToAction("AddRoleMaster", "Master");
                    }
                    else
                    {
                        var data = new { Message = msg, Type = "error" };
                        TempData["SweetAlert"] = data;
                        return RedirectToAction("AddRoleMaster", "Master");
                    }
                }
                else
                {
                    sm.Action = "Update";
                    sm.CreateDate = generalFunctions.DateTimeConvert(sm.CreateDate);
                    sm.CreateUser = User.Identity.Name;
                    sm.UpdateDate = generalFunctions.getTimeZoneDatetimedb();
                    sm.UpdateUser = User.Identity.Name;
                    sm.CompanyCode = LoggedUserDetails.CompanyCode;
                    var emplog = ApiCall.PostApi("RoleMasterInsUpd", Newtonsoft.Json.JsonConvert.SerializeObject(sm));
                    sm = JsonConvert.DeserializeObject<RoleMasterViewModel>(emplog);
                    string msg = sm.result;
                    if (msg.Contains("successfully"))
                    {
                        var data = new { Message = msg, Type = "success" };
                        TempData["SweetAlert"] = data;
                        return RedirectToAction("ViewRoleMaster", "Master");
                    }
                    else
                    {
                        var data = new { Message = msg, Type = "error" };
                        TempData["SweetAlert"] = data;
                        return RedirectToAction("ViewRoleMaster", "Master");
                    }
                }
            }
            catch (Exception ex)
            {
                //  Danger(ex.Message.ToString(), true);
                var data = new { Message = ex.Message.ToString(), Type = "error" };
                TempData["SweetAlert"] = data;
                return RedirectToAction("AddRoleMaster", "Master");
            }
        }
        public ActionResult EditRoleMaster(int ID)
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return RedirectToAction("Login", "Home");
                }
                RoleMasterViewModel sb = new RoleMasterViewModel();
                sb.CompanyCode = LoggedUserDetails.CompanyCode;
                var emplog = ApiCall.PostApi("RoleMasterRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(sb));
                sb = JsonConvert.DeserializeObject<RoleMasterViewModel>(emplog);
                sb.RoleId = sb.RoleMasterList.FirstOrDefault().RoleId.ToString();
                sb.RoleName = sb.RoleMasterList.FirstOrDefault().RoleName;
                //sb.PageUrl = sb.PageMasterList.FirstOrDefault().PageUrl;
                //sb.PagePriority = sb.PageMasterList.FirstOrDefault().PagePriority.ToString();
                sb.IsActive = sb.RoleMasterList.FirstOrDefault().IsActive.ToString();
                ViewBag.action = "Update";
                sb.Action = "Update";
                return View("AddRoleMaster", sb);
            }
            catch (Exception ex)
            {
                var data = new { Message = ex.Message.ToString(), Type = "error" };
                TempData["SweetAlert"] = data;
                return RedirectToAction("ViewRoleMaster", "Master");
            }
        }

        #endregion

        #region==> Status Master
        public ActionResult ViewStatusMaster()
        {
            try
            {
                //Rights checking
                if (!User.Identity.IsAuthenticated)
                {
                    return RedirectToAction("Login", "Home");
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
                //
                StatusMasterViewModel mv = new StatusMasterViewModel();
                mv.Action = "All";
                mv.CompanyCode = LoggedUserDetails.CompanyCode;

                var emplog = ApiCall.PostApi("StatusMasterRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(mv));
                mv = JsonConvert.DeserializeObject<StatusMasterViewModel>(emplog);
                return View(mv.StatusMasterList);

            }
            catch (Exception ex)
            {
                //Danger(ex.Message.ToString(), true);
                var data = new { Message = ex.Message.ToString(), Type = "error" };
                TempData["SweetAlert"] = data;
                return RedirectToAction("Dashboard", "Home");
            }
        }
        public ActionResult AddStatusMaster()
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return RedirectToAction("Login", "Home");
                }
                StatusMasterViewModel model = new StatusMasterViewModel();
                model.Action = "Save";
                return View(model);
            }
            catch (Exception ex)
            {
                var data = new { Message = ex.Message.ToString(), Type = "error" };
                TempData["SweetAlert"] = data;
                return RedirectToAction("Dashboard", "Home");
            }
        }
        [HttpPost]
        public ActionResult AddStatusMaster(StatusMasterViewModel sm)
        {
            try
            {
                if (sm.StatusId == null)
                {
                    sm.CreateDate = generalFunctions.getTimeZoneDatetimedb();
                    sm.Action = "insert";
                    sm.CreateUser = User.Identity.Name;
                    sm.UpdateUser = User.Identity.Name;
                    sm.CompanyCode = LoggedUserDetails.CompanyCode;
                    var emplog = ApiCall.PostApi("StatusMasterInsUpd", Newtonsoft.Json.JsonConvert.SerializeObject(sm));
                    sm = JsonConvert.DeserializeObject<StatusMasterViewModel>(emplog);
                    string msg = sm.result;
                    if (msg.Contains("successfully"))
                    {
                        //ViewBag.Message = msg.ToUpper();
                        var data = new { Message = msg, Type = "success" };
                        TempData["SweetAlert"] = data;
                        return RedirectToAction("AddStatusMaster", "Master");
                    }
                    else
                    {
                        //ViewBag.Message = msg.ToUpper();
                        // Danger(msg, true);
                        var data = new { Message = msg, Type = "error" };
                        TempData["SweetAlert"] = data;
                        return RedirectToAction("AddStatusMaster", "Master");
                    }
                }
                else
                {
                    sm.Action = "Update";
                    sm.CreateDate = generalFunctions.DateTimeConvert(sm.CreateDate);
                    sm.CreateUser = User.Identity.Name;
                    sm.UpdateDate = generalFunctions.getTimeZoneDatetimedb();
                    sm.UpdateUser = User.Identity.Name;
                    sm.CompanyCode = LoggedUserDetails.CompanyCode;
                    var emplog = ApiCall.PostApi("StatusMasterInsUpd", Newtonsoft.Json.JsonConvert.SerializeObject(sm));
                    sm = JsonConvert.DeserializeObject<StatusMasterViewModel>(emplog);
                    string msg = sm.result;
                    if (msg.Contains("successfully"))
                    {
                        var data = new { Message = msg, Type = "success" };
                        TempData["SweetAlert"] = data;
                        return RedirectToAction("AddStatusMaster", "Master");
                    }
                    else
                    {
                        var data = new { Message = msg, Type = "error" };
                        TempData["SweetAlert"] = data;
                        return RedirectToAction("AddStatusMaster", "Master");
                    }
                }
            }
            catch (Exception ex)
            {
                var data = new { Message = ex.Message.ToString(), Type = "error" };
                TempData["SweetAlert"] = data;
                return RedirectToAction("AddStatusMaster", "Home");
            }
        }
        public ActionResult EditStatusMaster(int ID)
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return RedirectToAction("Login", "Home");
                }
                StatusMasterViewModel sb = new StatusMasterViewModel();
                sb.CompanyCode = LoggedUserDetails.CompanyCode;
                var emplog = ApiCall.PostApi("StatusMasterRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(sb));
                sb = JsonConvert.DeserializeObject<StatusMasterViewModel>(emplog);
                sb.StatusId = sb.StatusMasterList.FirstOrDefault().StatusId.ToString();
                sb.StatusName = sb.StatusMasterList.FirstOrDefault().StatusName;
                //sb.PageUrl = sb.PageMasterList.FirstOrDefault().PageUrl;
                //sb.PagePriority = sb.PageMasterList.FirstOrDefault().PagePriority.ToString();
                sb.IsActive = sb.StatusMasterList.FirstOrDefault().IsActive.ToString();
                ViewBag.action = "Update";
                sb.Action = "Update";
                return View("AddStatusMaster", sb);
            }
            catch (Exception ex)
            {
                // Danger(ex.Message.ToString(), true);
                var data = new { Message = ex.Message.ToString(), Type = "error" };
                TempData["SweetAlert"] = data;
                return RedirectToAction("ViewStatusMaster", "Master");
            }
        }
        #endregion

        #region==>TimeSlot Master
        public ActionResult ViewTimeSlotMaster()
        {
            try
            {
                //Rights checking
                if (!User.Identity.IsAuthenticated)
                {
                    return RedirectToAction("Login", "Home");
                }
                MenuRightsViewModel mv1 = new MenuRightsViewModel();
                mv1.Usercode = LoggedUserDetails.UserName;
                string url = generalFunctions.getCommon(Request.Url.AbsoluteUri);
                mv1.PageName = url;
                var MenuRtr = ApiCall.PostApi("MenuRightsRtr", Newtonsoft.Json.JsonConvert.SerializeObject(mv1));
                mv1 = JsonConvert.DeserializeObject<MenuRightsViewModel>(MenuRtr);
                if (mv1.MenuRightsList.Count > 0)
                {
                    // ViewBag.ViewRight = mv1.MenuRightsList.FirstOrDefault().ViewRight;
                    // ViewBag.InsertRight = mv1.MenuRightsList.FirstOrDefault().InsertRight;
                    // ViewBag.UpdateRight = mv1.MenuRightsList.FirstOrDefault().UpdateRight;
                    //  ViewBag.DeleteRight = mv1.MenuRightsList.FirstOrDefault().DeleteRight;
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
                //
                TimeSlotMasterViewModel mv = new TimeSlotMasterViewModel();
                mv.Action = "All";
                mv.CompanyCode = LoggedUserDetails.CompanyCode;

                var emplog = ApiCall.PostApi("TimeSlotMasterRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(mv));
                mv = JsonConvert.DeserializeObject<TimeSlotMasterViewModel>(emplog);
                return View(mv.TimeSlotMasterList);

            }
            catch (Exception ex)
            {
                var data = new { Message = ex.Message.ToString(), Type = "error" };
                TempData["SweetAlert"] = data;
                return RedirectToAction("Dashboard", "Home");
            }
        }
        public ActionResult AddTimeSlotMaster()
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return RedirectToAction("Login", "Home");
                }
                TimeSlotMasterViewModel model = new TimeSlotMasterViewModel();
                model.Action = "Save";
                return View(model);
            }
            catch (Exception ex)
            {
                var data = new { Message = ex.Message.ToString(), Type = "error" };
                TempData["SweetAlert"] = data;
                return RedirectToAction("Dashboard", "Home");
            }
        }
        [HttpPost]
        public ActionResult AddTimeSlotMaster(TimeSlotMasterViewModel sm)
        {
            try
            {
                if (sm.SlotId == null)
                {
                    sm.CreateDate = generalFunctions.getTimeZoneDatetimedb();
                    sm.Action = "insert";
                    sm.CreateUser = User.Identity.Name;
                    sm.UpdateUser = User.Identity.Name;
                    sm.CompanyCode = LoggedUserDetails.CompanyCode;
                    var emplog = ApiCall.PostApi("TimeSlotMasterInsUpd", Newtonsoft.Json.JsonConvert.SerializeObject(sm));
                    sm = JsonConvert.DeserializeObject<TimeSlotMasterViewModel>(emplog);
                    string msg = sm.result;
                    if (msg.Contains("successfully"))
                    {
                        var data = new { Message = msg, Type = "success" };
                        TempData["SweetAlert"] = data;
                        return RedirectToAction("AddTimeSlotMaster", "Master");
                    }
                    else
                    {
                        var data = new { Message = msg, Type = "error" };
                        TempData["SweetAlert"] = data;
                        return RedirectToAction("AddTimeSlotMaster", "Master");
                    }
                }
                else
                {
                    sm.Action = "Update";
                    sm.CreateDate = generalFunctions.DateTimeConvert(sm.CreateDate);
                    sm.CreateUser = User.Identity.Name;
                    sm.UpdateDate = generalFunctions.getTimeZoneDatetimedb();
                    sm.UpdateUser = User.Identity.Name;
                    sm.CompanyCode = LoggedUserDetails.CompanyCode;
                    var emplog = ApiCall.PostApi("TimeSlotMasterInsUpd", Newtonsoft.Json.JsonConvert.SerializeObject(sm));
                    sm = JsonConvert.DeserializeObject<TimeSlotMasterViewModel>(emplog);
                    string msg = sm.result;
                    if (msg.Contains("successfully"))
                    {
                        var data = new { Message = msg, Type = "success" };
                        TempData["SweetAlert"] = data;
                        return RedirectToAction("AddTimeSlotMaster", "Master");
                    }
                    else
                    {
                        var data = new { Message = msg, Type = "error" };
                        TempData["SweetAlert"] = data;
                        return RedirectToAction("AddTimeSlotMaster", "Master");
                    }
                }
            }
            catch (Exception ex)
            {
                var data = new { Message = ex.Message.ToString(), Type = "error" };
                TempData["SweetAlert"] = data;
                return RedirectToAction("AddTimeSlotMaster", "Master");
            }
        }
        public ActionResult EditTimeSlotMaster(int ID)
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return RedirectToAction("Login", "Home");
                }
                TimeSlotMasterViewModel sb = new TimeSlotMasterViewModel();
                sb.CompanyCode = LoggedUserDetails.CompanyCode;
                var emplog = ApiCall.PostApi("RoleMasterRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(sb));
                sb = JsonConvert.DeserializeObject<TimeSlotMasterViewModel>(emplog);
                sb.SlotId = sb.TimeSlotMasterList.FirstOrDefault().SlotId.ToString();
                sb.SlotName = sb.TimeSlotMasterList.FirstOrDefault().SlotName;
                //sb.PageUrl = sb.PageMasterList.FirstOrDefault().PageUrl;
                //sb.PagePriority = sb.PageMasterList.FirstOrDefault().PagePriority.ToString();
                sb.IsActive = sb.TimeSlotMasterList.FirstOrDefault().IsActive.ToString();
                ViewBag.action = "Update";
                sb.Action = "Update";
                return View("AddTimeSlotMaster", sb);
            }
            catch (Exception ex)
            {
                var data = new { Message = ex.Message.ToString(), Type = "error" };
                TempData["SweetAlert"] = data;
                return RedirectToAction("AddTimeSlotMaster", "Master");
            }
        }
        #endregion

        #region==> Category Master
        public ActionResult ViewCategoryMaster()
        {
            try
            {
                //Rights checking
                if (!User.Identity.IsAuthenticated)
                {
                    return RedirectToAction("Login", "Home");
                }
                MenuRightsViewModel mv1 = new MenuRightsViewModel();
                mv1.Usercode = LoggedUserDetails.UserName;
                string url = generalFunctions.getCommon(Request.Url.AbsoluteUri);
                mv1.PageName = url;
                var MenuRtr = ApiCall.PostApi("MenuRightsRtr", Newtonsoft.Json.JsonConvert.SerializeObject(mv1));
                mv1 = JsonConvert.DeserializeObject<MenuRightsViewModel>(MenuRtr);
                if (mv1.MenuRightsList.Count > 0)
                {
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
                //
                CategoryMasterViewModel mv = new CategoryMasterViewModel();
                mv.Action = "All";
                mv.CompanyCode = LoggedUserDetails.CompanyCode;

                var emplog = ApiCall.PostApi("CategoryMasterRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(mv));
                mv = JsonConvert.DeserializeObject<CategoryMasterViewModel>(emplog);

               
                return View(mv.CategoryMasterList);

            }
            catch (Exception ex)
            {
                var data = new { Message = ex.Message.ToString(), Type = "error" };
                TempData["SweetAlert"] = data;
                return RedirectToAction("Dashboard", "Home");
            }
        }
        public ActionResult AddCategoryMaster()
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return RedirectToAction("Login", "Home");
                }
                CategoryMasterViewModel model = new CategoryMasterViewModel();
                model.Action = "Save";
                return View(model);
            }
            catch (Exception ex)
            {
                var data = new { Message = ex.Message.ToString(), Type = "error" };
                TempData["SweetAlert"] = data;
                return RedirectToAction("Dashboard", "Home");
            }
        }
        [HttpPost]
        public ActionResult AddCategoryMaster(CategoryMasterViewModel sm)
        {
            try
            {
                if (sm.CategoryId == null)
                {
                    sm.CreateDate = generalFunctions.getTimeZoneDatetimedb();
                    sm.Action = "insert";
                    sm.CreateUser = User.Identity.Name;
                    sm.UpdateUser = User.Identity.Name;
                    sm.CompanyCode = LoggedUserDetails.CompanyCode;
                    var emplog = ApiCall.PostApi("CategoryMasterInsUpd", Newtonsoft.Json.JsonConvert.SerializeObject(sm));
                    sm = JsonConvert.DeserializeObject<CategoryMasterViewModel>(emplog);
                    string msg = sm.result;
                    if (msg.Contains("successfully"))
                    {
                        var data = new { Message = msg, Type = "success" };
                        TempData["SweetAlert"] = data;
                        return RedirectToAction("AddCategoryMaster", "Master");
                    }
                    else
                    {
                        var data = new { Message = msg, Type = "error" };
                        TempData["SweetAlert"] = data;
                        return RedirectToAction("AddCategoryMaster", "Master");
                    }
                }
                else
                {
                    sm.Action = "Update";
                    sm.CreateDate = generalFunctions.DateTimeConvert(sm.CreateDate);
                    sm.CreateUser = User.Identity.Name;
                    sm.UpdateDate = generalFunctions.getTimeZoneDatetimedb();
                    sm.UpdateUser = User.Identity.Name;
                    sm.CompanyCode = LoggedUserDetails.CompanyCode;
                    var emplog = ApiCall.PostApi("CategoryMasterInsUpd", Newtonsoft.Json.JsonConvert.SerializeObject(sm));
                    sm = JsonConvert.DeserializeObject<CategoryMasterViewModel>(emplog);
                    string msg = sm.result;
                    if (msg.Contains("successfully"))
                    {
                        var data = new { Message = msg, Type = "success" };
                        TempData["SweetAlert"] = data;
                        return RedirectToAction("AddCategoryMaster", "Master");
                    }
                    else
                    {
                        var data = new { Message = msg, Type = "error" };
                        TempData["SweetAlert"] = data;
                        return RedirectToAction("AddCategoryMaster", "Master");
                    }
                }
            }
            catch (Exception ex)
            {
                var data = new { Message = ex.Message.ToString(), Type = "error" };
                TempData["SweetAlert"] = data;
                return RedirectToAction("AddCategoryMaster", "Master");
            }
        }
        public ActionResult EditCategoryMaster(int id)
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return RedirectToAction("Login", "Home");
                }
                CategoryMasterViewModel sb = new CategoryMasterViewModel();
                sb.CompanyCode = LoggedUserDetails.CompanyCode;
                sb.Action = "details";
                sb.CategoryId = id.ToString();
                var emplog = ApiCall.PostApi("CategoryMasterRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(sb));
                sb = JsonConvert.DeserializeObject<CategoryMasterViewModel>(emplog);
                sb.CategoryId = sb.CategoryMasterList.FirstOrDefault().CategoryId.ToString();
                sb.CategoryName = sb.CategoryMasterList.FirstOrDefault().CategoryName;
                sb.Description = sb.CategoryMasterList.FirstOrDefault().description;
                sb.CreateDate = sb.CategoryMasterList.FirstOrDefault().CreateDate.ToString();
                sb.CreateUser = sb.CategoryMasterList.FirstOrDefault().CreateUser;
                sb.IsActive = sb.CategoryMasterList.FirstOrDefault().IsActive.ToString();
                ViewBag.action = "Update";
                sb.Action = "Update";
                return View("AddCategoryMaster", sb);
            }
            catch (Exception ex)
            {
                var data = new { Message = ex.Message.ToString(), Type = "error" };
                TempData["SweetAlert"] = data;
                return RedirectToAction("ViewCategoryMaster", "Master");
            }
        }
        #endregion

        #region==>Staff Master
        public ActionResult ViewStaffMaster()
        {
            try
            {
                //Rights checking
                if (!User.Identity.IsAuthenticated)
                {
                    return RedirectToAction("Login", "Home");
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
                //
                StaffMasterViewModel mv = new StaffMasterViewModel();
                mv.Action = "All";
                mv.CompanyCode = LoggedUserDetails.CompanyCode;

                var emplog = ApiCall.PostApi("StaffMasterRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(mv));
                mv = JsonConvert.DeserializeObject<StaffMasterViewModel>(emplog);
                return View(mv.StaffMasterList);

            }
            catch (Exception ex)
            {
                var data = new { Message = ex.Message.ToString(), Type = "error" };
                TempData["SweetAlert"] = data;
                return RedirectToAction("Dashboard", "Home");
            }
        }
        public ActionResult AddStaffMaster()
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return RedirectToAction("Login", "Home");
                }
                var list = new List<string>() { "Male", "Female", "Other" };
                ViewBag.list = list;

                StaffMasterViewModel model = new StaffMasterViewModel();
                var Rolelist = db.PBRoleMasterRetrieve("active", "", "").ToList();
                ViewBag.RolesList = Rolelist;
                model.RoleMasterList = db.PBRoleMasterRetrieve("active", "", "").ToList();
                ViewBag.RoleMasterList = model.RoleMasterList;
                model.Action = "Save";

                return View(model);
            }
            catch (Exception ex)
            {
                var data = new { Message = ex.Message.ToString(), Type = "error" };
                TempData["SweetAlert"] = data;
                return RedirectToAction("Dashboard", "Home");
            }
        }
        [HttpPost]
        public ActionResult AddStaffMaster(StaffMasterViewModel sm)
        {
            try
            {
                if (sm.StaffId == null)
                {
                    //Role List Bind
                    RoleMasterViewModel pm = new RoleMasterViewModel();
                    var ProdList = ApiCall.PostApi("RoleMasterRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(sm));
                    pm = JsonConvert.DeserializeObject<RoleMasterViewModel>(ProdList);
                    sm.RoleMasterList = pm.RoleMasterList;
                    //
                    sm.CreateDate = generalFunctions.getTimeZoneDatetimedb();
                    sm.Action = "insert";
                    sm.CreateUser = User.Identity.Name;
                    sm.UpdateUser = User.Identity.Name;
                    sm.CompanyCode = LoggedUserDetails.CompanyCode;
                    var emplog = ApiCall.PostApi("StaffMasterInsUpd", Newtonsoft.Json.JsonConvert.SerializeObject(sm));
                    sm = JsonConvert.DeserializeObject<StaffMasterViewModel>(emplog);
                    string msg = sm.result;
                    if (msg.Contains("successfully"))
                    {
                        var data = new { Message = msg, Type = "success" };
                        TempData["SweetAlert"] = data;
                        return RedirectToAction("AddStaffMaster", "Master");
                    }
                    else
                    {
                        var data = new { Message = msg, Type = "error" };
                        TempData["SweetAlert"] = data;
                        return RedirectToAction("AddStaffMaster", "Master");
                    }
                }
                else
                {
                    sm.Action = "Update";
                    sm.CreateDate = generalFunctions.DateTimeConvert(sm.CreateDate);
                    sm.CreateUser = User.Identity.Name;
                    sm.UpdateDate = generalFunctions.getTimeZoneDatetimedb();
                    sm.UpdateUser = User.Identity.Name;
                    sm.CompanyCode = LoggedUserDetails.CompanyCode;
                    var emplog = ApiCall.PostApi("StaffMasterInsUpd", Newtonsoft.Json.JsonConvert.SerializeObject(sm));
                    sm = JsonConvert.DeserializeObject<StaffMasterViewModel>(emplog);
                    string msg = sm.result;
                    if (msg.Contains("successfully"))
                    {
                        var data = new { Message = msg, Type = "success" };
                        TempData["SweetAlert"] = data;
                        return RedirectToAction("AddStaffMaster", "Master");
                    }
                    else
                    {
                        var data = new { Message = msg, Type = "error" };
                        TempData["SweetAlert"] = data;
                        return RedirectToAction("AddStaffMaster", "Master");
                    }
                }
            }
            catch (Exception ex)
            {
                var data = new { Message = ex.Message.ToString(), Type = "error" };
                TempData["SweetAlert"] = data;
                return RedirectToAction("AddStaffMaster", "Master");
            }
        }
        public ActionResult EditStaffMaster(int id)
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return RedirectToAction("Login", "Home");
                }
                var list = new List<string>() { "Male", "Female", "Other" };
                ViewBag.list = list;
                StaffMasterViewModel sb = new StaffMasterViewModel();
                //role List Bind
                sb.Action = "Active";
                RoleMasterViewModel pm = new RoleMasterViewModel();
                var ProdList = ApiCall.PostApi("RoleMasterRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(sb));
                pm = JsonConvert.DeserializeObject<RoleMasterViewModel>(ProdList);
                sb.RoleMasterList = pm.RoleMasterList;
                //
                sb.CompanyCode = LoggedUserDetails.CompanyCode;
                sb.Action = "details";
                sb.StaffId = id.ToString();
                var emplog = ApiCall.PostApi("StaffMasterRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(sb));
                sb = JsonConvert.DeserializeObject<StaffMasterViewModel>(emplog);
                sb.StaffId = sb.StaffMasterList.FirstOrDefault().StaffId.ToString();
                sb.StaffCode = sb.StaffMasterList.FirstOrDefault().StaffCode.ToString();
                sb.StaffName = sb.StaffMasterList.FirstOrDefault().StaffName.ToString();
                sb.Contact = sb.StaffMasterList.FirstOrDefault().Contact.ToString();
                sb.Gender = sb.StaffMasterList.FirstOrDefault().Gender.ToString();
                sb.Password = sb.StaffMasterList.FirstOrDefault().Password.ToString();
                sb.RoleId = sb.StaffMasterList.FirstOrDefault().RoleId.ToString();
                sb.CreateDate = sb.StaffMasterList.FirstOrDefault().CreateDate.ToString();
                sb.CreateUser = sb.StaffMasterList.FirstOrDefault().CreateUser;
                sb.IsActive = sb.StaffMasterList.FirstOrDefault().IsActive.ToString();
                ViewBag.action = "Update";
                sb.Action = "Update";
                return View("AddStaffMaster", sb);
            }
            catch (Exception ex)
            {
                var data = new { Message = ex.Message.ToString(), Type = "error" };
                TempData["SweetAlert"] = data;
                return RedirectToAction("ViewStaffMaster", "Master");
            }
        }
        #endregion

        #region==> ProductType Master
        public ActionResult ViewProductTypeMaster()
        {
            try
            {
                //Rights checking
                if (!User.Identity.IsAuthenticated)
                {
                    return RedirectToAction("Login", "Home");
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
                //
                ProductTypeMasterViewModel mv = new ProductTypeMasterViewModel();
                mv.Action = "All";
                mv.CompanyCode = LoggedUserDetails.CompanyCode;

                var emplog = ApiCall.PostApi("ProductTypeMasterRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(mv));
                mv = JsonConvert.DeserializeObject<ProductTypeMasterViewModel>(emplog);
                return View(mv.ProductTypeMasterList);

            }
            catch (Exception ex)
            {
                var data = new { Message = ex.Message.ToString(), Type = "error" };
                TempData["SweetAlert"] = data;
                return RedirectToAction("Dashboard", "Home");
            }
        }
        public ActionResult AddProductTypeMaster()
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return RedirectToAction("Login", "Home");
                }
                ProductTypeMasterViewModel model = new ProductTypeMasterViewModel();
                model.Action = "Save";
                return View(model);
            }
            catch (Exception ex)
            {
                var data = new { Message = ex.Message.ToString(), Type = "error" };
                TempData["SweetAlert"] = data;
                return RedirectToAction("AddProductTypeMaster", "Master");
            }
        }
        [HttpPost]
        public ActionResult AddProductTypeMaster(ProductTypeMasterViewModel sm)
        {
            try
            {
                if (sm.ProductTypeId == null)
                {
                    sm.CreateDate = generalFunctions.getTimeZoneDatetimedb();
                    sm.Action = "insert";
                    sm.CreateUser = User.Identity.Name;
                    sm.UpdateUser = User.Identity.Name;
                    sm.CompanyCode = LoggedUserDetails.CompanyCode;
                    var emplog = ApiCall.PostApi("ProductTypeMasterInsUpd", Newtonsoft.Json.JsonConvert.SerializeObject(sm));
                    sm = JsonConvert.DeserializeObject<ProductTypeMasterViewModel>(emplog);
                    string msg = sm.result;
                    if (msg.Contains("successfully"))
                    {
                        var data = new { Message = msg, Type = "success" };
                        TempData["SweetAlert"] = data;
                        return RedirectToAction("AddProductTypeMaster", "Master");
                    }
                    else
                    {
                        var data = new { Message = msg, Type = "error" };
                        TempData["SweetAlert"] = data;
                        return RedirectToAction("AddProductTypeMaster", "Master");
                    }
                }
                else
                {
                    sm.Action = "update";
                    sm.CreateDate = generalFunctions.DateTimeConvert(sm.CreateDate);
                    sm.CreateUser = User.Identity.Name;
                    sm.UpdateDate = generalFunctions.getTimeZoneDatetimedb();
                    sm.UpdateUser = User.Identity.Name;
                    sm.CompanyCode = LoggedUserDetails.CompanyCode;
                    var emplog = ApiCall.PostApi("ProductTypeMasterInsUpd", Newtonsoft.Json.JsonConvert.SerializeObject(sm));
                    sm = JsonConvert.DeserializeObject<ProductTypeMasterViewModel>(emplog);
                    string msg = sm.result;
                    if (msg.Contains("successfully"))
                    {
                        var data = new { Message = msg, Type = "success" };
                        TempData["SweetAlert"] = data;
                        return RedirectToAction("AddProductTypeMaster", "Master");
                    }
                    else
                    {
                        var data = new { Message = msg, Type = "error" };
                        TempData["SweetAlert"] = data;
                        return RedirectToAction("AddProductTypeMaster", "Master");
                    }
                }
            }
            catch (Exception ex)
            {
                var data = new { Message = ex.Message.ToString(), Type = "error" };
                TempData["SweetAlert"] = data;
                return RedirectToAction("AddProductTypeMaster", "Master");
            }
        }
        public ActionResult EditProductTypeMaster(int id)
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return RedirectToAction("Login", "Home");
                }
                ProductTypeMasterViewModel sb = new ProductTypeMasterViewModel();
                sb.CompanyCode = LoggedUserDetails.CompanyCode;
                sb.Action = "details";
                sb.ProductTypeId = id.ToString();
                var emplog = ApiCall.PostApi("ProductTypeMasterRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(sb));
                sb = JsonConvert.DeserializeObject<ProductTypeMasterViewModel>(emplog);
                sb.ProductTypeId = sb.ProductTypeMasterList.FirstOrDefault().ProductTypeId.ToString();
                sb.ProductTName = sb.ProductTypeMasterList.FirstOrDefault().ProductTName;
                sb.IsActive = sb.ProductTypeMasterList.FirstOrDefault().IsActive.ToString();
                sb.CreateDate = sb.ProductTypeMasterList.FirstOrDefault().CreateDate.ToString();
                sb.CreateUser = sb.ProductTypeMasterList.FirstOrDefault().CreateUser;
                ViewBag.action = "Update";
                sb.Action = "Update";
                return View("AddProductTypeMaster", sb);
            }
            catch (Exception ex)
            {
                var data = new { Message = ex.Message.ToString(), Type = "error" };
                TempData["SweetAlert"] = data;
                return RedirectToAction("ViewProductTypeMaster", "Master");
            }
        }
        #endregion

        #region==> ProductMaster
        public ActionResult ViewProductMaster()
        {
            try
            {
                //Rights checking
                if (!User.Identity.IsAuthenticated)
                {
                    return RedirectToAction("Login", "Home");
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
                //
                ProductMasterViewModel mv = new ProductMasterViewModel();
                mv.Action = "All";
                mv.CompanyCode = LoggedUserDetails.CompanyCode;

                var emplog = ApiCall.PostApi("ProductMasterRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(mv));
                mv = JsonConvert.DeserializeObject<ProductMasterViewModel>(emplog);
                return View(mv.ProductMasterList);

            }
            catch (Exception ex)
            {
                var data = new { Message = ex.Message.ToString(), Type = "error" };
                TempData["SweetAlert"] = data;
                return RedirectToAction("Dashboard", "Home");
            }
        }
        public ActionResult AddProductMaster()
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return RedirectToAction("Login", "Home");
                }
                ProductMasterViewModel model = new ProductMasterViewModel();
                model.CompanyCode = LoggedUserDetails.CompanyCode;
                model.Action = "Save";

                //Product List Bind
                model.Action = "Active";
                ProductTypeMasterViewModel pm = new ProductTypeMasterViewModel();
                var ProdList = ApiCall.PostApi("ProductTypeMasterRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(model));
                pm = JsonConvert.DeserializeObject<ProductTypeMasterViewModel>(ProdList);
                model.ProductTypeMasterList = pm.ProductTypeMasterList;
                //

                return View(model);
            }
            catch (Exception ex)
            {
                var data = new { Message = ex.Message.ToString(), Type = "error" };
                TempData["SweetAlert"] = data;
                return RedirectToAction("Dashboard", "Home");
            }
        }
        [HttpPost]
        public ActionResult AddProductMaster(ProductMasterViewModel sm, HttpPostedFileBase ProductImage)
        {
            try
            {
                //Product List Bind
                sm.Action = "Active";
                ProductTypeMasterViewModel pm = new ProductTypeMasterViewModel();
                var ProdList = ApiCall.PostApi("ProductTypeMasterRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(sm));
                pm = JsonConvert.DeserializeObject<ProductTypeMasterViewModel>(ProdList);
                sm.ProductTypeMasterList = pm.ProductTypeMasterList;
                //

                if (sm.ProductId == null)
                {
                    if (ProductImage != null)
                    {
                        string path = Server.MapPath("~/Uploads/ProductImages/");
                        string ImagePath = "Uploads/ProductImages/";
                        string extension = Path.GetExtension(ProductImage.FileName);

                        var newfilename = string.Concat(sm.ProductCode, extension);
                        newfilename = newfilename.Replace(" ", ""); // Remove spaces from the filename

                        ProductImage.SaveAs(path + Path.GetFileName(newfilename));
                        sm.ProductImage = string.Concat(ImagePath, newfilename);
                    }

                    sm.CreateDate = generalFunctions.getTimeZoneDatetimedb();
                    sm.Action = "insert";
                    sm.CreateUser = User.Identity.Name;
                    sm.UpdateUser = User.Identity.Name;
                    sm.CompanyCode = LoggedUserDetails.CompanyCode;
                    var emplog = ApiCall.PostApi("ProductMasterInsUpd", Newtonsoft.Json.JsonConvert.SerializeObject(sm));
                    sm = JsonConvert.DeserializeObject<ProductMasterViewModel>(emplog);
                    string msg = sm.result;
                    if (msg.Contains("successfully"))
                    {
                        var data = new { Message = msg, Type = "success" };
                        TempData["SweetAlert"] = data;
                        return RedirectToAction("AddProductMaster", "Master");
                    }
                    else
                    {
                        var data = new { Message = msg, Type = "error" };
                        TempData["SweetAlert"] = data;
                        return RedirectToAction("AddProductMaster", "Master");
                    }
                }
                else
                {
                    if (ProductImage != null)
                    {

                        string path = Server.MapPath("~/Uploads/ProductImages/");
                        string ImagePath = "Uploads/ProductImages/";
                        string extension = Path.GetExtension(ProductImage.FileName);

                        var newfilename = string.Concat(sm.ProductName, extension);
                        newfilename = newfilename.Replace(" ", ""); // Remove spaces from the filename

                        ProductImage.SaveAs(path + Path.GetFileName(newfilename));
                        sm.ProductImage = string.Concat(ImagePath, newfilename);
                    }
                    sm.Action = "Update";
                    sm.CreateDate = generalFunctions.DateTimeConvert(sm.CreateDate);
                    sm.CreateUser = User.Identity.Name;
                    sm.UpdateDate = generalFunctions.getTimeZoneDatetimedb();
                    sm.UpdateUser = User.Identity.Name;
                    sm.CompanyCode = LoggedUserDetails.CompanyCode;
                    var emplog = ApiCall.PostApi("ProductMasterInsUpd", Newtonsoft.Json.JsonConvert.SerializeObject(sm));
                    sm = JsonConvert.DeserializeObject<ProductMasterViewModel>(emplog);
                    string msg = sm.result;
                    if (msg.Contains("successfully"))
                    {
                        var data = new { Message = msg, Type = "success" };
                        TempData["SweetAlert"] = data;
                        return RedirectToAction("AddProductMaster", "Master");
                    }
                    else
                    {
                        var data = new { Message = msg, Type = "error" };
                        TempData["SweetAlert"] = data;
                        return RedirectToAction("AddProductMaster", "Master");
                    }
                }
            }
            catch (Exception ex)
            {
                var data = new { Message = ex.Message.ToString(), Type = "error" };
                TempData["SweetAlert"] = data;
                return RedirectToAction("AddProductMaster", "Master");
            }
        }
        public ActionResult EditProductMaster(int id)
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return RedirectToAction("Login", "Home");
                }
                ProductMasterViewModel sb = new ProductMasterViewModel();
                //Product List Bind
                sb.Action = "Active";
                ProductTypeMasterViewModel pm = new ProductTypeMasterViewModel();
                var ProdList = ApiCall.PostApi("ProductTypeMasterRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(sb));
                pm = JsonConvert.DeserializeObject<ProductTypeMasterViewModel>(ProdList);
                sb.ProductTypeMasterList = pm.ProductTypeMasterList;
                //
                sb.CompanyCode = LoggedUserDetails.CompanyCode;
                sb.Action = "Details";
                sb.ProductId = id.ToString();
                var emplog = ApiCall.PostApi("ProductMasterRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(sb));
                sb = JsonConvert.DeserializeObject<ProductMasterViewModel>(emplog);
                sb.ProductId = sb.ProductMasterList.FirstOrDefault().ProductId.ToString();
                sb.ProductCode = sb.ProductMasterList.FirstOrDefault().ProductCode;
                sb.ProductName = sb.ProductMasterList.FirstOrDefault().ProductName;
                sb.NumberOfPerson = sb.ProductMasterList.FirstOrDefault().NumberOfPerson;
                sb.ProductTypeId = sb.ProductMasterList.FirstOrDefault().ProductTypeId.ToString();
                sb.CreateDate = sb.ProductMasterList.FirstOrDefault().CreateDate.ToString();
                sb.CreateUser = sb.ProductMasterList.FirstOrDefault().CreateUser;
                sb.IsActive = sb.ProductMasterList.FirstOrDefault().IsActive.ToString();
                sb.ProductImage = sb.ProductMasterList.FirstOrDefault().ProductImage;
                ViewBag.action = "Update";
                sb.Action = "Update";
                return View("AddProductMaster", sb);
            }
            catch (Exception ex)
            {
                var data = new { Message = ex.Message.ToString(), Type = "error" };
                TempData["SweetAlert"] = data;
                return RedirectToAction("ViewProductMaster", "Master");
            }
        }
        #endregion

        #region ==>SubCategoryMaster
        public ActionResult ViewSubCategoryMaster()
        {
            try
            {
                //Rights checking
                if (!User.Identity.IsAuthenticated)
                {
                    return RedirectToAction("Login", "Home");
                }
                MenuRightsViewModel mv1 = new MenuRightsViewModel();
                mv1.Usercode = LoggedUserDetails.UserName;
                string url = generalFunctions.getCommon(Request.Url.AbsoluteUri);
                mv1.PageName = url;
                var MenuRtr = ApiCall.PostApi("MenuRightsRtr", Newtonsoft.Json.JsonConvert.SerializeObject(mv1));
                mv1 = JsonConvert.DeserializeObject<MenuRightsViewModel>(MenuRtr);
                if (mv1.MenuRightsList.Count > 0)
                {
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
                //
                SubCategoryMasterViewModel mv = new SubCategoryMasterViewModel();
                mv.Action = "active";
                mv.CompanyCode = LoggedUserDetails.CompanyCode;
                var emplog = ApiCall.PostApi("SubCategoryMasterRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(mv));
                mv = JsonConvert.DeserializeObject<SubCategoryMasterViewModel>(emplog);
                return View(mv.SubCategoryMasterList);

            }
            catch (Exception ex)
            {
                var data = new { Message = ex.Message.ToString(), Type = "error" };
                TempData["SweetAlert"] = data;
                return RedirectToAction("Dashboard", "Home");
            }
        }
        public ActionResult AddSubCategoryMaster()
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return RedirectToAction("Login", "Home");
                }
                SubCategoryMasterViewModel model = new SubCategoryMasterViewModel();
                var YearList = db.PBYearMasterRetrieve("active", "PBC01", "").ToList();
                ViewBag.YearList = YearList;
                var CategoryList = db.PBCategoryMasterRetrieve("active", "", "").ToList();
                ViewBag.categorylist = CategoryList;

                //GST Master List Bind
                GSTMasterViewModel gvm = new GSTMasterViewModel();
                gvm.CompanyCode = LoggedUserDetails.CompanyCode;
                gvm.Action = "active";
                var GSTList = ApiCall.PostApi("GSTMasterRtr", Newtonsoft.Json.JsonConvert.SerializeObject(gvm));
                gvm = JsonConvert.DeserializeObject<GSTMasterViewModel>(GSTList);
                model.GSTMasterList = gvm.GSTMasterList;
                //

                model.CategoryMasterList = db.PBCategoryMasterRetrieve("active", "", "").ToList();
                model.YearMasterList = db.PBYearMasterRetrieve("active", "PBC01", "").ToList();
                model.Action = "Save";
                return View(model);
            }
            catch (Exception ex)
            {
                var data = new { Message = ex.Message.ToString(), Type = "error" };
                TempData["SweetAlert"] = data;
                return RedirectToAction("Dashboard", "Home");
            }
        }
        [HttpPost]
        public ActionResult AddSubCategoryMaster(SubCategoryMasterViewModel sm)
        {
            try
            {
                if (sm.SubCategoryId == null)
                {
                    //Category List Bind
                    CategoryMasterViewModel pm = new CategoryMasterViewModel();
                    var ProdList = ApiCall.PostApi("CategoryMasterRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(sm));
                    pm = JsonConvert.DeserializeObject<CategoryMasterViewModel>(ProdList);
                    sm.CategoryMasterList = pm.CategoryMasterList;
                    //
                    //Year List Bind
                    YearMasterViewModel ym = new YearMasterViewModel();
                    var Yearlist = ApiCall.PostApi("YearMasterRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(sm));
                    ym = JsonConvert.DeserializeObject<YearMasterViewModel>(Yearlist);
                    sm.YearMasterList = ym.YearMasterList;
                    //
                    //if (sm.DayInterval != null)
                    //{
                    //    sm.DayInterval = sm.DayInterval + " Day";
                    //}
                    sm.CreateDate = generalFunctions.getTimeZoneDatetimedb();
                    sm.UpdateDate = generalFunctions.getTimeZoneDatetimedb();
                    sm.Action = "insert";
                    sm.CreateUser = User.Identity.Name;
                    sm.UpdateUser = User.Identity.Name;
                    sm.CompanyCode = LoggedUserDetails.CompanyCode;
                    var emplog = ApiCall.PostApi("SubCategoryMasterInsUpd", Newtonsoft.Json.JsonConvert.SerializeObject(sm));
                    sm = JsonConvert.DeserializeObject<SubCategoryMasterViewModel>(emplog);
                    string msg = sm.result;
                    if (msg.Contains("successfully"))
                    {
                        var data = new { Message = msg, Type = "success" };
                        TempData["SweetAlert"] = data;
                        return RedirectToAction("AddSubCategoryMaster", "Master");
                    }
                    else
                    {
                        var data = new { Message = msg, Type = "error" };
                        TempData["SweetAlert"] = data;
                        return RedirectToAction("AddSubCategoryMaster", "Master");
                    }
                }
                else
                {
                    sm.Action = "Update";
                    sm.CreateDate = generalFunctions.DateTimeConvert(sm.CreateDate);
                    sm.CreateUser = User.Identity.Name;
                    sm.UpdateDate = generalFunctions.getTimeZoneDatetimedb();
                    sm.UpdateUser = User.Identity.Name;
                    sm.CompanyCode = LoggedUserDetails.CompanyCode;
                    var emplog = ApiCall.PostApi("SubCategoryMasterInsUpd", Newtonsoft.Json.JsonConvert.SerializeObject(sm));
                    sm = JsonConvert.DeserializeObject<SubCategoryMasterViewModel>(emplog);
                    string msg = sm.result;
                    if (msg.Contains("successfully"))
                    {
                        var data = new { Message = msg, Type = "success" };
                        TempData["SweetAlert"] = data;
                        return RedirectToAction("ViewSubCategoryMaster", "Master");
                    }
                    else
                    {
                        var data = new { Message = msg, Type = "error" };
                        TempData["SweetAlert"] = data;
                        return RedirectToAction("ViewSubCategoryMaster", "Master");
                    }
                }
            }
            catch (Exception ex)
            {
                var data = new { Message = ex.Message.ToString(), Type = "error" };
                TempData["SweetAlert"] = data;
                return RedirectToAction("AddSubCategoryMaster", "Home");
            }
        }
        public ActionResult EditSubCategoryMaster(int ID)
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return RedirectToAction("Login", "Home");
                }
                SubCategoryMasterViewModel sb = new SubCategoryMasterViewModel();
                //Year List Bind
                sb.Action = "Active";
                sb.CompanyCode = LoggedUserDetails.CompanyCode;
                YearMasterViewModel ym = new YearMasterViewModel();
                var Yearlist = ApiCall.PostApi("YearMasterRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(sb));
                ym = JsonConvert.DeserializeObject<YearMasterViewModel>(Yearlist);
                sb.YearMasterList = ym.YearMasterList;
                //

                //Category List Bind
                sb.Action = "Active";
                CategoryMasterViewModel pm = new CategoryMasterViewModel();
                var ProdList = ApiCall.PostApi("CategoryMasterRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(sb));
                pm = JsonConvert.DeserializeObject<CategoryMasterViewModel>(ProdList);
                sb.CategoryMasterList = pm.CategoryMasterList;
                //

                //GST Master List Bind
                GSTMasterViewModel gvm = new GSTMasterViewModel();
                gvm.CompanyCode = LoggedUserDetails.CompanyCode;
                gvm.Action = "active";
                var GSTList = ApiCall.PostApi("GSTMasterRtr", Newtonsoft.Json.JsonConvert.SerializeObject(gvm));
                gvm = JsonConvert.DeserializeObject<GSTMasterViewModel>(GSTList);
                sb.GSTMasterList = gvm.GSTMasterList;
                //

                sb.Action = "Detail";
                sb.SubCategoryId = ID.ToString();
                var emplog = ApiCall.PostApi("SubCategoryMasterRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(sb));
                sb = JsonConvert.DeserializeObject<SubCategoryMasterViewModel>(emplog);

                sb.CategoryId = sb.SubCategoryMasterList.FirstOrDefault().CategoryId.ToString();
                sb.SubCategoryId = sb.SubCategoryMasterList.FirstOrDefault().SubCategoryId.ToString();
                sb.SubCategoryName = sb.SubCategoryMasterList.FirstOrDefault().SubCategoryName.ToString();
                sb.IsMultiPerson = sb.SubCategoryMasterList.FirstOrDefault().ismultiperson.ToString();
                sb.NumberOfPerson = sb.SubCategoryMasterList.FirstOrDefault().NumberOfPerson.ToString();
                sb.YearId = sb.SubCategoryMasterList.FirstOrDefault().YearId.ToString();
                sb.IsActive = sb.SubCategoryMasterList.FirstOrDefault().IsActive.ToString();
                sb.NoOfSitting = sb.SubCategoryMasterList.FirstOrDefault().NoOfSitting.ToString();
                sb.TimeDuraion = sb.SubCategoryMasterList.FirstOrDefault().TimeDuraion.ToString();
                sb.Amount = sb.SubCategoryMasterList.FirstOrDefault().Amount.ToString();
                sb.DayInterval = sb.SubCategoryMasterList.FirstOrDefault()?.DayInterval?.ToString() ?? "0";
                sb.Incentive = sb.SubCategoryMasterList.FirstOrDefault()?.Incentive.ToString() ?? "0";
                sb.GSTPercentage = sb.SubCategoryMasterList.FirstOrDefault().GSTPercentage.ToString();
                sb.CreateDate = sb.SubCategoryMasterList.FirstOrDefault().CreateDate.ToString();
                sb.CreateUser = sb.SubCategoryMasterList.FirstOrDefault().CreateUser;
                ViewBag.action = "Update";
                sb.Action = "Update";
                return View("AddSubCategoryMaster", sb);
            }
            catch (Exception ex)
            {
                var data = new { Message = ex.Message.ToString(), Type = "error" };
                TempData["SweetAlert"] = data;
                return RedirectToAction("ViewSubCategoryMaster", "Master");
            }
        }
        #endregion

        #region ==>YearMaster
        public ActionResult ViewYearMaster()
        {
            try
            {
                //Rights checking
                if (!User.Identity.IsAuthenticated)
                {
                    return RedirectToAction("Login", "Home");
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
                //
                YearMasterViewModel mv = new YearMasterViewModel();
                mv.Action = "All";
                mv.CompanyCode = LoggedUserDetails.CompanyCode;

                var emplog = ApiCall.PostApi("YearMasterRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(mv));
                mv = JsonConvert.DeserializeObject<YearMasterViewModel>(emplog);
                return View(mv.YearMasterList);

            }
            catch (Exception ex)
            {
                var data = new { Message = ex.Message.ToString(), Type = "error" };
                TempData["SweetAlert"] = data;
                return RedirectToAction("Dashboard", "Home");
            }
        }
        public ActionResult AddYearMaster()
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return RedirectToAction("Login", "Home");
                }

                YearMasterViewModel model = new YearMasterViewModel();
                model.Action = "Save";
                return View(model);
            }
            catch (Exception ex)
            {
                var data = new { Message = ex.Message.ToString(), Type = "error" };
                TempData["SweetAlert"] = data;
                return RedirectToAction("Dashboard", "Home");
            }
        }
        [HttpPost]
        public ActionResult AddYearMaster(YearMasterViewModel sm)
        {
            try
            {
                if (sm.YearId == null)
                {
                    sm.CreateDate = generalFunctions.getTimeZoneDatetimedb();
                    sm.UpdateDate = generalFunctions.getTimeZoneDatetimedb();
                    sm.Action = "insert";
                    sm.CreateUser = User.Identity.Name;
                    sm.UpdateUser = User.Identity.Name;
                    sm.CompanyCode = LoggedUserDetails.CompanyCode;
                    sm.ToDate = generalFunctions.dateconvert(sm.ToDate);
                    sm.FromDate = generalFunctions.dateconvert(sm.FromDate);
                    var emplog = ApiCall.PostApi("YearMasterInsUpd", Newtonsoft.Json.JsonConvert.SerializeObject(sm));
                    sm = JsonConvert.DeserializeObject<YearMasterViewModel>(emplog);
                    string msg = sm.result;
                    if (msg.Contains("successfully"))
                    {
                        var data = new { Message = msg, Type = "success" };
                        TempData["SweetAlert"] = data;
                        return RedirectToAction("AddYearMaster", "Master");
                    }
                    else
                    {
                        var data = new { Message = msg, Type = "error" };
                        TempData["SweetAlert"] = data;
                        return RedirectToAction("AddYearMaster", "Master");
                    }
                }
                else
                {
                    sm.Action = "Update";
                    sm.CreateDate = generalFunctions.DateTimeConvert(sm.CreateDate);
                    sm.CreateUser = User.Identity.Name;
                    sm.UpdateDate = generalFunctions.getTimeZoneDatetimedb();
                    sm.UpdateUser = User.Identity.Name;
                    sm.CompanyCode = LoggedUserDetails.CompanyCode;
                    sm.ToDate = generalFunctions.dateconvert(sm.ToDate);
                    sm.FromDate = generalFunctions.dateconvert(sm.FromDate);
                    var emplog = ApiCall.PostApi("YearMasterInsUpd", Newtonsoft.Json.JsonConvert.SerializeObject(sm));
                    sm = JsonConvert.DeserializeObject<YearMasterViewModel>(emplog);
                    string msg = sm.result;
                    if (msg.Contains("successfully"))
                    {
                        var data = new { Message = msg, Type = "success" };
                        TempData["SweetAlert"] = data;
                        return RedirectToAction("ViewYearMaster", "Master");
                    }
                    else
                    {
                        var data = new { Message = msg, Type = "error" };
                        TempData["SweetAlert"] = data;
                        return RedirectToAction("ViewYearMaster", "Master");
                    }
                }
            }
            catch (Exception ex)
            {
                var data = new { Message = ex.Message.ToString(), Type = "error" };
                TempData["SweetAlert"] = data;
                return RedirectToAction("AddYearMaster", "Home");
            }
        }
        public ActionResult EditYearMaster(int ID)
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return RedirectToAction("Login", "Home");
                }
                YearMasterViewModel sb = new YearMasterViewModel();
                sb.CompanyCode = LoggedUserDetails.CompanyCode;
                sb.Action = "details";
                sb.YearId = ID.ToString();
                var emplog = ApiCall.PostApi("YearMasterRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(sb));
                sb = JsonConvert.DeserializeObject<YearMasterViewModel>(emplog);
                sb.YearId = sb.YearMasterList.FirstOrDefault().YearId.ToString();
                sb.Yearperiod = sb.YearMasterList.FirstOrDefault().Yearperiod;
                sb.FromDate = sb.YearMasterList.FirstOrDefault().FromDate;
                sb.ToDate = sb.YearMasterList.FirstOrDefault().ToDate;
                sb.IsActive = sb.YearMasterList.FirstOrDefault().IsActive.ToString();
                sb.CreateDate = sb.YearMasterList.FirstOrDefault().CreateDate.ToString();
                sb.CreateUser = sb.YearMasterList.FirstOrDefault().CreateUser;
                ViewBag.action = "Update";
                sb.Action = "Update";
                return View("AddYearMaster", sb);
            }
            catch (Exception ex)
            {
                var data = new { Message = ex.Message.ToString(), Type = "error" };
                TempData["SweetAlert"] = data;
                return RedirectToAction("ViewYearMaster", "Master");
            }
        }
        #endregion

        #region==> DepartmentMaster
        public ActionResult ViewDepartmentMaster()
        {
            try
            {
                //Rights checking
                if (!User.Identity.IsAuthenticated)
                {
                    return RedirectToAction("Login", "Home");
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
                //
                DepartmentMasterViewModel mv = new DepartmentMasterViewModel();
                mv.Action = "All";
                mv.CompanyCode = LoggedUserDetails.CompanyCode;

                var emplog = ApiCall.PostApi("DepartmentMasterRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(mv));
                mv = JsonConvert.DeserializeObject<DepartmentMasterViewModel>(emplog);
                return View(mv.DepartmentList);

            }
            catch (Exception ex)
            {
                //Danger(ex.Message.ToString(), true);
                var data = new { Message = ex.Message.ToString(), Type = "error" };
                TempData["SweetAlert"] = data;
                return RedirectToAction("Dashboard", "Home");
            }
        }
        public ActionResult AddDepartmentMaster()
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return RedirectToAction("Login", "Home");
                }
                DepartmentMasterViewModel model = new DepartmentMasterViewModel();
                model.Action = "Add";
                return View(model);
            }
            catch (Exception ex)
            {
                var data = new { Message = ex.Message.ToString(), Type = "error" };
                TempData["SweetAlert"] = data;
                return RedirectToAction("Dashboard", "Home");
            }
        }
        [HttpPost]
        public ActionResult AddDepartmentMaster(DepartmentMasterViewModel sm)
        {
            try
            {
                if (sm.DepartmentId == null)
                {
                    sm.CreateDate = generalFunctions.getTimeZoneDatetimedb();
                    sm.Action = "insert";
                    sm.CreateUser = User.Identity.Name;
                    sm.UpdateUser = User.Identity.Name;
                    sm.CompanyCode = LoggedUserDetails.CompanyCode;
                    var emplog = ApiCall.PostApi("DepartmentMasterInsUpd", Newtonsoft.Json.JsonConvert.SerializeObject(sm));
                    sm = JsonConvert.DeserializeObject<DepartmentMasterViewModel>(emplog);
                    string msg = sm.result;
                    if (msg.Contains("successfully"))
                    {
                        var data = new { Message = msg, Type = "success" };
                        TempData["SweetAlert"] = data;
                        return RedirectToAction("AddDepartmentMaster", "Master");
                    }
                    else
                    {
                        var data = new { Message = msg, Type = "error" };
                        TempData["SweetAlert"] = data;
                        return RedirectToAction("AddDepartmentMaster", "Master");
                    }
                }
                else
                {
                    sm.Action = "Update";
                    sm.CreateDate = generalFunctions.DateTimeConvert(sm.CreateDate);
                    sm.CreateUser = User.Identity.Name;
                    sm.UpdateDate = generalFunctions.getTimeZoneDatetimedb();
                    sm.UpdateUser = User.Identity.Name;
                    sm.CompanyCode = LoggedUserDetails.CompanyCode;
                    var emplog = ApiCall.PostApi("DepartmentMasterInsUpd", Newtonsoft.Json.JsonConvert.SerializeObject(sm));
                    sm = JsonConvert.DeserializeObject<DepartmentMasterViewModel>(emplog);
                    string msg = sm.result;
                    if (msg.Contains("successfully"))
                    {
                        var data = new { Message = msg, Type = "success" };
                        TempData["SweetAlert"] = data;
                        return RedirectToAction("ViewDepartmentMaster", "Master");
                    }
                    else
                    {
                        var data = new { Message = msg, Type = "error" };
                        TempData["SweetAlert"] = data;
                        return RedirectToAction("ViewDepartmentMaster", "Master");
                    }
                }
            }
            catch (Exception ex)
            {
                //  Danger(ex.Message.ToString(), true);
                var data = new { Message = ex.Message.ToString(), Type = "error" };
                TempData["SweetAlert"] = data;
                return RedirectToAction("AddDepartmentMaster", "Master");
            }
        }
        public ActionResult EditDepartmentMaster(int ID)
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return RedirectToAction("Login", "Home");
                }
                DepartmentMasterViewModel sb = new DepartmentMasterViewModel();
                sb.CompanyCode = LoggedUserDetails.CompanyCode;
                var emplog = ApiCall.PostApi("DepartmentMasterRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(sb));
                sb = JsonConvert.DeserializeObject<DepartmentMasterViewModel>(emplog);
                sb.DepartmentId = sb.DepartmentList.FirstOrDefault().DepartmentId.ToString();
                sb.DepartmentName = sb.DepartmentList.FirstOrDefault().DepartmentName;
                sb.IsActive = sb.DepartmentList.FirstOrDefault().IsActive.ToString();
                sb.CreateDate = sb.DepartmentList.FirstOrDefault().CreateDate.ToString();
                sb.CreateUser = sb.DepartmentList.FirstOrDefault().CreateUser;
                ViewBag.action = "Update";
                sb.Action = "Update";
                return View("AddDepartmentMaster", sb);
            }
            catch (Exception ex)
            {
                var data = new { Message = ex.Message.ToString(), Type = "error" };
                TempData["SweetAlert"] = data;
                return RedirectToAction("ViewDepartmentMaster", "Master");
            }
        }

        #endregion

        #region==> VendorMaster
        public ActionResult ViewVendorMaster()
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
                //

                VendorMasterViewModel vnd = new VendorMasterViewModel();
                vnd.CompanyCode = LoggedUserDetails.CompanyCode;
                vnd.Action = "all";

                //Vendor Purchase List 
                var vendorlist = ApiCall.PostApi("VendorMasterRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(vnd));
                vnd = JsonConvert.DeserializeObject<VendorMasterViewModel>(vendorlist);
                //
                return View(vnd);

            }
            catch (Exception ex)
            {
                var data = new { Message = ex.Message.ToString(), Type = "error" };
                TempData["SweetAlert"] = data;
                return RedirectToAction("Dashboard", "Home");
            }
        }

        public ActionResult AddVendorMaster()
        {
            try
            {
                VendorMasterViewModel vnd = new VendorMasterViewModel();
                vnd.CompanyCode = LoggedUserDetails.CompanyCode;
                vnd.Action = "all";

                if (!User.Identity.IsAuthenticated)
                {
                    FormsAuthentication.RedirectToLoginPage();
                }
                string url = generalFunctions.getCommon(Request.Url.AbsoluteUri);

                return View(vnd);
            }
            catch (Exception ex)
            {
                var data = new { Message = ex.Message.ToString(), Type = "error" };
                TempData["SweetAlert"] = data;
                return RedirectToAction("Dashboard", "Home");
            }
        }

        [HttpPost]
        public ActionResult AddVendorMaster(VendorMasterViewModel vnd)
        {
            try
            {

                if (vnd.VendorId == null)
                {
                    vnd.CompanyCode = LoggedUserDetails.CompanyCode;
                    vnd.Action = "Active";
                    vnd.CreateUser = User.Identity.Name;
                    vnd.CreateDate = generalFunctions.getTimeZoneDatetimedb();

                    if (!User.Identity.IsAuthenticated)
                    {
                        FormsAuthentication.RedirectToLoginPage();
                    }
                    string url = generalFunctions.getCommon(Request.Url.AbsoluteUri);


                    vnd.Action = "insert";
                    var apiResponse = ApiCall.PostApi("VendorMasterInsUpd", Newtonsoft.Json.JsonConvert.SerializeObject(vnd));
                    vnd = JsonConvert.DeserializeObject<VendorMasterViewModel>(apiResponse);
                    string msg = vnd.result;

                    if (msg.Contains("successfully"))
                    {
                        var data = new { Message = msg, Type = "success" };
                        TempData["SweetAlert"] = data;
                        return RedirectToAction("AddVendorMaster", "Master");
                    }
                    else
                    {
                        var data = new { Message = msg, Type = "error" };
                        TempData["SweetAlert"] = data;
                        return RedirectToAction("AddVendorMaster", "Master");
                    }

                }
                else
                {
                    vnd.Action = "update";
                    vnd.UpdateDate = generalFunctions.getTimeZoneDatetimedb();
                    vnd.CompanyCode = LoggedUserDetails.CompanyCode;

                    var apiResponse = ApiCall.PostApi("VendorMasterInsUpd", Newtonsoft.Json.JsonConvert.SerializeObject(vnd));
                    vnd = JsonConvert.DeserializeObject<VendorMasterViewModel>(apiResponse);
                    string msg = vnd.result;
                    if (msg.Contains("successfully"))
                    {
                        var data = new { Message = msg, Type = "success" };
                        TempData["SweetAlert"] = data;
                        return RedirectToAction("ViewVendorMaster", "Master");
                    }
                    else
                    {
                        var data = new { Message = msg, Type = "error" };
                        TempData["SweetAlert"] = data;
                        return RedirectToAction("ViewVendorMaster", "Master");
                    }
                }
            }
            catch (Exception ex)
            {
                //Danger(ex.Message.ToString(), true);
                var data = new { Message = ex.Message.ToString(), Type = "error" };

                TempData["SweetAlert"] = data;
                return RedirectToAction("ViewVendorMaster", "Master");
            }
        }

        public ActionResult EditVendorMaster(int vendorid)
        {

            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    FormsAuthentication.RedirectToLoginPage();
                }
                VendorMasterViewModel vnd = new VendorMasterViewModel();
                vnd.Action = "details";
                vnd.VendorId = vendorid.ToString();
                vnd.CompanyCode = LoggedUserDetails.CompanyCode;
                var VendorList = ApiCall.PostApi("VendorMasterRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(vnd));
                vnd = JsonConvert.DeserializeObject<VendorMasterViewModel>(VendorList);


                vnd.VendorId = vnd.VendorList.FirstOrDefault().VendorId.ToString();
                vnd.VendorCode = vnd.VendorList.FirstOrDefault().VendorCode.ToString();
                vnd.VendorName = vnd.VendorList.FirstOrDefault().VendorName.ToString();
                vnd.Phone = vnd.VendorList.FirstOrDefault().Phone.ToString();
                vnd.Email = vnd.VendorList.FirstOrDefault().Email.ToString();
                vnd.Address = vnd.VendorList.FirstOrDefault().Address.ToString();
                vnd.CompanyCode = vnd.VendorList.FirstOrDefault().CompanyCode.ToString();
                vnd.CreateDate = vnd.VendorList.FirstOrDefault().CreateDate.ToString();
                vnd.CreateUser = vnd.VendorList.FirstOrDefault().CreateUser.ToString();

                ViewBag.action = "Update";
                vnd.Action = "Update";
                return View("AddVendorMaster", vnd);
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

        #region==> Package Master
        public ActionResult ViewPackageMaster()
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return RedirectToAction("Login", "Home");
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
                //
                PackageMasterViewModel mv = new PackageMasterViewModel();
                mv.Action = "all";
                mv.CompanyCode = LoggedUserDetails.CompanyCode;

                var emplog = ApiCall.PostApi("PackageMasterRtr", Newtonsoft.Json.JsonConvert.SerializeObject(mv));
                mv = JsonConvert.DeserializeObject<PackageMasterViewModel>(emplog);
                return View(mv.PackageList);

            }
            catch (Exception ex)
            {
                var data = new { Message = ex.Message.ToString(), Type = "error" };
                TempData["SweetAlert"] = data;
                return RedirectToAction("Dashboard", "Home");
            }
        }
        public ActionResult AddPackageMaster()
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return RedirectToAction("Login", "Home");
                }
                PackageMasterViewModel model = new PackageMasterViewModel();
                model.CompanyCode = LoggedUserDetails.CompanyCode;
                model.Action = "Save";
                return View(model);
            }
            catch (Exception ex)
            {
                var data = new { Message = ex.Message.ToString(), Type = "error" };
                TempData["SweetAlert"] = data;
                return RedirectToAction("Dashboard", "Home");
            }
        }
        [HttpPost]
        public ActionResult AddPackageMaster(PackageMasterViewModel sm)
        {
            try
            {
                if (sm.PackageId == null)
                {
                    sm.CreateDate = generalFunctions.getTimeZoneDatetimedb();
                    sm.Action = "insert";
                    sm.CreateUser = User.Identity.Name;
                    sm.UpdateUser = User.Identity.Name;
                    sm.CompanyCode = LoggedUserDetails.CompanyCode;
                    var emplog = ApiCall.PostApi("PackageMasterInsUpd", Newtonsoft.Json.JsonConvert.SerializeObject(sm));
                    sm = JsonConvert.DeserializeObject<PackageMasterViewModel>(emplog);
                    string msg = sm.result;
                    if (msg.Contains("successfully"))
                    {
                        var data = new { Message = msg, Type = "success" };
                        TempData["SweetAlert"] = data;
                        return RedirectToAction("AddPackageMaster", "Master");
                    }
                    else
                    {
                        var data = new { Message = msg, Type = "error" };
                        TempData["SweetAlert"] = data;
                        return RedirectToAction("AddPackageMaster", "Master");
                    }
                }
                else
                {
                    sm.Action = "update";
                    sm.CreateDate = generalFunctions.DateTimeConvert(sm.CreateDate);
                    sm.CreateUser = User.Identity.Name;
                    sm.UpdateDate = generalFunctions.getTimeZoneDatetimedb();
                    sm.UpdateUser = User.Identity.Name;
                    sm.CompanyCode = LoggedUserDetails.CompanyCode;
                    var emplog = ApiCall.PostApi("PackageMasterInsUpd", Newtonsoft.Json.JsonConvert.SerializeObject(sm));
                    sm = JsonConvert.DeserializeObject<PackageMasterViewModel>(emplog);
                    string msg = sm.result;
                    if (msg.Contains("successfully"))
                    {
                        var data = new { Message = msg, Type = "success" };
                        TempData["SweetAlert"] = data;
                        return RedirectToAction("AddPackageMaster", "Master");
                    }
                    else
                    {
                        var data = new { Message = msg, Type = "error" };
                        TempData["SweetAlert"] = data;
                        return RedirectToAction("AddPackageMaster", "Master");
                    }
                }
            }
            catch (Exception ex)
            {
                var data = new { Message = ex.Message.ToString(), Type = "error" };
                TempData["SweetAlert"] = data;
                return RedirectToAction("AddPackageMaster", "Master");
            }
        }
        public ActionResult EditPackageMaster(int id)
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return RedirectToAction("Login", "Home");
                }
                PackageMasterViewModel sb = new PackageMasterViewModel();
                sb.CompanyCode = LoggedUserDetails.CompanyCode;
                sb.Action = "Details";
                sb.PackageId = id.ToString();
                var emplog = ApiCall.PostApi("PackageMasterRtr", Newtonsoft.Json.JsonConvert.SerializeObject(sb));
                sb = JsonConvert.DeserializeObject<PackageMasterViewModel>(emplog);
                sb.PackageId = sb.PackageList.FirstOrDefault().PackageId.ToString();
                sb.PackageName = sb.PackageList.FirstOrDefault().PackageName;
                sb.PackageAmount = sb.PackageList.FirstOrDefault().PackageAmount.ToString();
                sb.CreateDate = sb.PackageList.FirstOrDefault().CreateDate.ToString();
                sb.CreateUser = sb.PackageList.FirstOrDefault().CreateUser;
                sb.IsActive = sb.PackageList.FirstOrDefault().IsActive.ToString();
                ViewBag.action = "Update";
                sb.Action = "Update";
                return View("AddPackageMaster", sb);
            }
            catch (Exception ex)
            {
                var data = new { Message = ex.Message.ToString(), Type = "error" };
                TempData["SweetAlert"] = data;
                return RedirectToAction("AddPackageMaster", "Master");
            }
        }
        #endregion

        #region==> Integration Package Service Master
        public ActionResult ViewIntePackageServiceMaster()
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return RedirectToAction("Login", "Home");
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

                IntePackageServiceMasterViewModel mv = new IntePackageServiceMasterViewModel();
                mv.Action = "All";
                mv.CompanyCode = LoggedUserDetails.CompanyCode;
                var emplog = ApiCall.PostApi("IntePackageServiceMasterRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(mv));
                mv = JsonConvert.DeserializeObject<IntePackageServiceMasterViewModel>(emplog);
                return View(mv.IntePackageServiceMasterList);

            }
            catch (Exception ex)
            {
                //Danger(ex.Message.ToString(), true);
                var data = new { Message = ex.Message.ToString(), Type = "error" };
                TempData["SweetAlert"] = data;
                return RedirectToAction("Dashboard", "Home");
            }
        }
        public ActionResult AddIntePackageServiceMaster()
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return RedirectToAction("Login", "Home");
                }
                IntePackageServiceMasterViewModel model = new IntePackageServiceMasterViewModel();
                model.Action = "Add";

                SubCategoryMasterViewModel mv = new SubCategoryMasterViewModel();
                mv.Action = "active";
                mv.CompanyCode = LoggedUserDetails.CompanyCode;
                var emplog = ApiCall.PostApi("SubCategoryMasterRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(mv));
                mv = JsonConvert.DeserializeObject<SubCategoryMasterViewModel>(emplog);
                model.SubCategoryMasterList = mv.SubCategoryMasterList;

                PackageMasterViewModel pmv = new PackageMasterViewModel();
                pmv.Action = "active";
                pmv.CompanyCode = LoggedUserDetails.CompanyCode;
                var package = ApiCall.PostApi("PackageMasterRtr", Newtonsoft.Json.JsonConvert.SerializeObject(pmv));
                pmv = JsonConvert.DeserializeObject<PackageMasterViewModel>(package);
                model.PackageMasterList = pmv.PackageList;

                ProductMasterViewModel pv = new ProductMasterViewModel();
                pv.Action = "active";
                pv.CompanyCode = LoggedUserDetails.CompanyCode;
                var product = ApiCall.PostApi("ProductMasterRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(pv));
                pv = JsonConvert.DeserializeObject<ProductMasterViewModel>(product);
                model.ProductMasterList = pv.ProductMasterList;

                return View(model);
            }
            catch (Exception ex)
            {
                var data = new { Message = ex.Message.ToString(), Type = "error" };
                TempData["SweetAlert"] = data;
                return RedirectToAction("Dashboard", "Home");
            }
        }
        [HttpPost]
        public ActionResult AddIntePackageServiceMaster(IntePackageServiceMasterViewModel sm)
        {
            try
            {
                if (sm.IntePackageServiceId == null)
                {
                    sm.CreateDate = generalFunctions.getTimeZoneDatetimedb();
                    sm.Action = "insert";
                    sm.CreateUser = User.Identity.Name;
                    sm.UpdateUser = User.Identity.Name;
                    sm.CompanyCode = LoggedUserDetails.CompanyCode;
                    var emplog = ApiCall.PostApi("IntePackageServiceMasterInsUpd", Newtonsoft.Json.JsonConvert.SerializeObject(sm));
                    sm = JsonConvert.DeserializeObject<IntePackageServiceMasterViewModel>(emplog);
                    string msg = sm.result;
                    if (msg.Contains("successfully"))
                    {
                        var data = new { Message = msg, Type = "success" };
                        TempData["SweetAlert"] = data;
                        return RedirectToAction("AddIntePackageServiceMaster", "Master");
                    }
                    else
                    {
                        var data = new { Message = msg, Type = "error" };
                        TempData["SweetAlert"] = data;
                        return RedirectToAction("AddIntePackageServiceMaster", "Master");
                    }
                }
                else
                {
                    sm.Action = "Update";
                    sm.CreateDate = generalFunctions.DateTimeConvert(sm.CreateDate);
                    sm.CreateUser = User.Identity.Name;
                    sm.UpdateDate = generalFunctions.getTimeZoneDatetimedb();
                    sm.UpdateUser = User.Identity.Name;
                    sm.CompanyCode = LoggedUserDetails.CompanyCode;
                    var emplog = ApiCall.PostApi("IntePackageServiceMasterInsUpd", Newtonsoft.Json.JsonConvert.SerializeObject(sm));
                    sm = JsonConvert.DeserializeObject<IntePackageServiceMasterViewModel>(emplog);
                    string msg = sm.result;
                    if (msg.Contains("successfully"))
                    {
                        var data = new { Message = msg, Type = "success" };
                        TempData["SweetAlert"] = data;
                        return RedirectToAction("ViewIntePackageServiceMaster", "Master");
                    }
                    else
                    {
                        var data = new { Message = msg, Type = "error" };
                        TempData["SweetAlert"] = data;
                        return RedirectToAction("ViewIntePackageServiceMaster", "Master");
                    }
                }
            }
            catch (Exception ex)
            {
                //  Danger(ex.Message.ToString(), true);
                var data = new { Message = ex.Message.ToString(), Type = "error" };
                TempData["SweetAlert"] = data;
                return RedirectToAction("AddIntePackageServiceMaster", "Master");
            }
        }
        public ActionResult EditIntePackageServiceMaster(int id)
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return RedirectToAction("Login", "Home");
                }
                IntePackageServiceMasterViewModel sb = new IntePackageServiceMasterViewModel();
                //Product List Bind
                sb.Action = "Active";

                IntePackageServiceMasterViewModel mv = new IntePackageServiceMasterViewModel();
                mv.Action = "All";
                mv.CompanyCode = LoggedUserDetails.CompanyCode;
                var emplog = ApiCall.PostApi("IntePackageServiceMasterRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(mv));
                mv = JsonConvert.DeserializeObject<IntePackageServiceMasterViewModel>(emplog);
                sb.IntePackageServiceMasterList = mv.IntePackageServiceMasterList;

                SubCategoryMasterViewModel SCV = new SubCategoryMasterViewModel();
                SCV.Action = "active";
                SCV.CompanyCode = LoggedUserDetails.CompanyCode;
                var emplog3 = ApiCall.PostApi("SubCategoryMasterRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(SCV));
                SCV = JsonConvert.DeserializeObject<SubCategoryMasterViewModel>(emplog3);
                sb.SubCategoryMasterList = SCV.SubCategoryMasterList;

                PackageMasterViewModel pmv = new PackageMasterViewModel();
                pmv.Action = "active";
                pmv.CompanyCode = LoggedUserDetails.CompanyCode;
                var package = ApiCall.PostApi("PackageMasterRtr", Newtonsoft.Json.JsonConvert.SerializeObject(pmv));
                pmv = JsonConvert.DeserializeObject<PackageMasterViewModel>(package);
                sb.PackageMasterList = pmv.PackageList;

                ProductMasterViewModel pv = new ProductMasterViewModel();
                pv.Action = "active";
                pv.CompanyCode = LoggedUserDetails.CompanyCode;
                var product = ApiCall.PostApi("ProductMasterRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(pv));
                pv = JsonConvert.DeserializeObject<ProductMasterViewModel>(product);
                sb.ProductMasterList = pv.ProductMasterList;

                //
                sb.CompanyCode = LoggedUserDetails.CompanyCode;
                sb.Action = "details";
                sb.IntePackageServiceId = id.ToString();
                var emplog2 = ApiCall.PostApi("IntePackageServiceMasterRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(sb));
                sb = JsonConvert.DeserializeObject<IntePackageServiceMasterViewModel>(emplog2);
                sb.ProductId = sb.IntePackageServiceMasterList.FirstOrDefault().ProductId.ToString();
                sb.ServiceId = sb.IntePackageServiceMasterList.FirstOrDefault().ServiceId.ToString();
                sb.PackageId = sb.IntePackageServiceMasterList.FirstOrDefault().PackageId.ToString();
                sb.CreateDate = sb.ProductMasterList.FirstOrDefault().CreateDate.ToString();
                sb.CreateUser = sb.ProductMasterList.FirstOrDefault().CreateUser;
                sb.IsActive = sb.ProductMasterList.FirstOrDefault().IsActive.ToString();
                ViewBag.action = "Update";
                sb.Action = "Update";
                return View("AddIntePackageServiceMaster", sb);
            }
            catch (Exception ex)
            {
                var data = new { Message = ex.Message.ToString(), Type = "error" };
                TempData["SweetAlert"] = data;
                return RedirectToAction("ViewIntePackageServiceMaster", "Master");
            }
        }
        #endregion

        #region==> Integration Service Product Master
        public ActionResult ViewInteServiceProductMaster()
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return RedirectToAction("Login", "Home");
                }
                MenuRightsViewModel mv1 = new MenuRightsViewModel();
                mv1.Usercode = LoggedUserDetails.UserName;
                string url = generalFunctions.getCommon(Request.Url.AbsoluteUri);
                mv1.PageName = url;
                var MenuRtr = ApiCall.PostApi("MenuRightsRtr", Newtonsoft.Json.JsonConvert.SerializeObject(mv1));
                mv1 = JsonConvert.DeserializeObject<MenuRightsViewModel>(MenuRtr);
                if (mv1.MenuRightsList.Count > 0)
                {
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

                InteServiceProductMasterViewModel mv = new InteServiceProductMasterViewModel();
                mv.Action = "All";
                mv.CompanyCode = LoggedUserDetails.CompanyCode;
                var emplog = ApiCall.PostApi("InteServiceProductMasterRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(mv));
                mv = JsonConvert.DeserializeObject<InteServiceProductMasterViewModel>(emplog);
                return View(mv.InteServiceProductMasterList);

            }
            catch (Exception ex)
            {
                //Danger(ex.Message.ToString(), true);
                var data = new { Message = ex.Message.ToString(), Type = "error" };
                TempData["SweetAlert"] = data;
                return RedirectToAction("Dashboard", "Home");
            }
        }
        public ActionResult AddInteServiceProductMaster()
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return RedirectToAction("Login", "Home");
                }
                InteServiceProductMasterViewModel model = new InteServiceProductMasterViewModel();
                model.Action = "Add";

                SubCategoryMasterViewModel mv = new SubCategoryMasterViewModel();
                mv.Action = "active";
                mv.CompanyCode = LoggedUserDetails.CompanyCode;
                var emplog = ApiCall.PostApi("SubCategoryMasterRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(mv));
                mv = JsonConvert.DeserializeObject<SubCategoryMasterViewModel>(emplog);
                model.SubCategoryMasterList = mv.SubCategoryMasterList;

                ProductMasterViewModel pv = new ProductMasterViewModel();
                pv.Action = "active";
                pv.CompanyCode = LoggedUserDetails.CompanyCode;
                var product = ApiCall.PostApi("ProductMasterRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(pv));
                pv = JsonConvert.DeserializeObject<ProductMasterViewModel>(product);
                model.ProductMasterList = pv.ProductMasterList;

                return View(model);
            }
            catch (Exception ex)
            {
                var data = new { Message = ex.Message.ToString(), Type = "error" };
                TempData["SweetAlert"] = data;
                return RedirectToAction("Dashboard", "Home");
            }
        }
        [HttpPost]
        public ActionResult AddInteServiceProductMaster(InteServiceProductMasterViewModel sm)
        {
            try
            {
                if (sm.InteServiceProductId == null)
                {
                    sm.CreateDate = generalFunctions.getTimeZoneDatetimedb();
                    sm.Action = "insert";
                    sm.CreateUser = User.Identity.Name;
                    sm.UpdateUser = User.Identity.Name;
                    sm.CompanyCode = LoggedUserDetails.CompanyCode;
                    var emplog = ApiCall.PostApi("InteServiceProductMasterInsUpd", Newtonsoft.Json.JsonConvert.SerializeObject(sm));
                    sm = JsonConvert.DeserializeObject<InteServiceProductMasterViewModel>(emplog);
                    string msg = sm.result;
                    if (msg.Contains("successfully"))
                    {
                        var data = new { Message = msg, Type = "success" };
                        TempData["SweetAlert"] = data;
                        return RedirectToAction("AddInteServiceProductMaster", "Master");
                    }
                    else
                    {
                        var data = new { Message = msg, Type = "error" };
                        TempData["SweetAlert"] = data;
                        return RedirectToAction("AddInteServiceProductMaster", "Master");
                    }
                }
                else
                {
                    sm.Action = "Update";
                    sm.CreateDate = generalFunctions.DateTimeConvert(sm.CreateDate);
                    sm.CreateUser = User.Identity.Name;
                    sm.UpdateDate = generalFunctions.getTimeZoneDatetimedb();
                    sm.UpdateUser = User.Identity.Name;
                    sm.CompanyCode = LoggedUserDetails.CompanyCode;
                    var emplog = ApiCall.PostApi("InteServiceProductMasterInsUpd", Newtonsoft.Json.JsonConvert.SerializeObject(sm));
                    sm = JsonConvert.DeserializeObject<InteServiceProductMasterViewModel>(emplog);
                    string msg = sm.result;
                    if (msg.Contains("successfully"))
                    {
                        var data = new { Message = msg, Type = "success" };
                        TempData["SweetAlert"] = data;
                        return RedirectToAction("ViewInteServiceProductMaster", "Master");
                    }
                    else
                    {
                        var data = new { Message = msg, Type = "error" };
                        TempData["SweetAlert"] = data;
                        return RedirectToAction("ViewInteServiceProductMaster", "Master");
                    }
                }
            }
            catch (Exception ex)
            {
                //  Danger(ex.Message.ToString(), true);
                var data = new { Message = ex.Message.ToString(), Type = "error" };
                TempData["SweetAlert"] = data;
                return RedirectToAction("AddInteServiceProductMaster", "Master");
            }
        }
        public ActionResult EditInteServiceProductMaster(int id)
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return RedirectToAction("Login", "Home");
                }
                InteServiceProductMasterViewModel sb = new InteServiceProductMasterViewModel();

                InteServiceProductMasterViewModel mv = new InteServiceProductMasterViewModel();
                mv.Action = "All";
                mv.CompanyCode = LoggedUserDetails.CompanyCode;
                var emplog = ApiCall.PostApi("InteServiceProductMasterRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(mv));
                mv = JsonConvert.DeserializeObject<InteServiceProductMasterViewModel>(emplog);
                sb.InteServiceProductMasterList = mv.InteServiceProductMasterList;

                SubCategoryMasterViewModel SCV = new SubCategoryMasterViewModel();
                SCV.Action = "active";
                SCV.CompanyCode = LoggedUserDetails.CompanyCode;
                var emplog3 = ApiCall.PostApi("SubCategoryMasterRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(SCV));
                SCV = JsonConvert.DeserializeObject<SubCategoryMasterViewModel>(emplog3);
                sb.SubCategoryMasterList = SCV.SubCategoryMasterList;

                ProductMasterViewModel pv = new ProductMasterViewModel();
                pv.Action = "active";
                pv.CompanyCode = LoggedUserDetails.CompanyCode;
                var product = ApiCall.PostApi("ProductMasterRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(pv));
                pv = JsonConvert.DeserializeObject<ProductMasterViewModel>(product);
                sb.ProductMasterList = pv.ProductMasterList;

                //
                sb.CompanyCode = LoggedUserDetails.CompanyCode;
                sb.Action = "details";
                sb.InteServiceProductId = id.ToString();
                var emplog2 = ApiCall.PostApi("InteServiceProductMasterRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(sb));
                sb = JsonConvert.DeserializeObject<InteServiceProductMasterViewModel>(emplog2);
                sb.ProductId = sb.InteServiceProductMasterList.FirstOrDefault().ProductId.ToString();
                sb.ServiceId = sb.InteServiceProductMasterList.FirstOrDefault().ServiceId.ToString();
                sb.CreateDate = sb.ProductMasterList.FirstOrDefault().CreateDate.ToString();
                sb.CreateUser = sb.ProductMasterList.FirstOrDefault().CreateUser;
                sb.IsActive = sb.ProductMasterList.FirstOrDefault().IsActive.ToString();
                ViewBag.action = "Update";
                sb.Action = "Update";
                return View("AddInteServiceProductMaster", sb);
            }
            catch (Exception ex)
            {
                var data = new { Message = ex.Message.ToString(), Type = "error" };
                TempData["SweetAlert"] = data;
                return RedirectToAction("ViewInteServiceProductMaster", "Master");
            }
        }
        #endregion

        #region==> Item Master
        public ActionResult ViewItemMaster()
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return RedirectToAction("Login", "Home");
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
                //
                ItemMasterViewModel mv = new ItemMasterViewModel();
                mv.Action = "all";
                mv.CompanyCode = LoggedUserDetails.CompanyCode;

                var emplog = ApiCall.PostApi("ItemMasterRtr", Newtonsoft.Json.JsonConvert.SerializeObject(mv));
                mv = JsonConvert.DeserializeObject<ItemMasterViewModel>(emplog);
                return View(mv.ItemMasterList);

            }
            catch (Exception ex)
            {
                var data = new { Message = ex.Message.ToString(), Type = "error" };
                TempData["SweetAlert"] = data;
                return RedirectToAction("Dashboard", "Home");
            }
        }
        public ActionResult AddItemMaster()
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return RedirectToAction("Login", "Home");
                }
                ItemMasterViewModel model = new ItemMasterViewModel();
                model.CompanyCode = LoggedUserDetails.CompanyCode;
                model.Action = "Save";
                return View(model);
            }
            catch (Exception ex)
            {
                var data = new { Message = ex.Message.ToString(), Type = "error" };
                TempData["SweetAlert"] = data;
                return RedirectToAction("Dashboard", "Home");
            }
        }
        [HttpPost]
        public ActionResult AddItemMaster(ItemMasterViewModel sm)
        {
            try
            {
                if (sm.ItemId == null)
                {
                    sm.CreateDate = generalFunctions.getTimeZoneDatetimedb();
                    sm.Action = "insert";
                    sm.CreateUser = User.Identity.Name;
                    sm.UpdateUser = User.Identity.Name;
                    sm.CompanyCode = LoggedUserDetails.CompanyCode;
                    var emplog = ApiCall.PostApi("ItemMasterInsUpd", Newtonsoft.Json.JsonConvert.SerializeObject(sm));
                    sm = JsonConvert.DeserializeObject<ItemMasterViewModel>(emplog);
                    string msg = sm.result;
                    if (msg.Contains("successfully"))
                    {
                        var data = new { Message = msg, Type = "success" };
                        TempData["SweetAlert"] = data;
                        return RedirectToAction("AddItemMaster", "Master");
                    }
                    else
                    {
                        var data = new { Message = msg, Type = "error" };
                        TempData["SweetAlert"] = data;
                        return RedirectToAction("AddItemMaster", "Master");
                    }
                }
                else
                {
                    sm.Action = "update";
                    sm.CreateDate = generalFunctions.DateTimeConvert(sm.CreateDate);
                    sm.CreateUser = User.Identity.Name;
                    sm.UpdateDate = generalFunctions.getTimeZoneDatetimedb();
                    sm.UpdateUser = User.Identity.Name;
                    sm.CompanyCode = LoggedUserDetails.CompanyCode;
                    var emplog = ApiCall.PostApi("ItemMasterInsUpd", Newtonsoft.Json.JsonConvert.SerializeObject(sm));
                    sm = JsonConvert.DeserializeObject<ItemMasterViewModel>(emplog);
                    string msg = sm.result;
                    if (msg.Contains("successfully"))
                    {
                        var data = new { Message = msg, Type = "success" };
                        TempData["SweetAlert"] = data;
                        return RedirectToAction("AddItemMaster", "Master");
                    }
                    else
                    {
                        var data = new { Message = msg, Type = "error" };
                        TempData["SweetAlert"] = data;
                        return RedirectToAction("AddItemMaster", "Master");
                    }
                }
            }
            catch (Exception ex)
            {
                var data = new { Message = ex.Message.ToString(), Type = "error" };
                TempData["SweetAlert"] = data;
                return RedirectToAction("AddItemMaster", "Master");
            }
        }
        public ActionResult EditItemMaster(int id)
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return RedirectToAction("Login", "Home");
                }
                ItemMasterViewModel sb = new ItemMasterViewModel();
                sb.CompanyCode = LoggedUserDetails.CompanyCode;
                sb.Action = "details";
                sb.ItemId = id.ToString();
                var emplog = ApiCall.PostApi("ItemMasterRtr", Newtonsoft.Json.JsonConvert.SerializeObject(sb));
                sb = JsonConvert.DeserializeObject<ItemMasterViewModel>(emplog);
                sb.ItemId = sb.ItemMasterList.FirstOrDefault().ItemId.ToString();
                sb.ItemName = sb.ItemMasterList.FirstOrDefault().ItemName;
                sb.CreateDate = sb.ItemMasterList.FirstOrDefault().CreateDate.ToString();
                sb.CreateUser = sb.ItemMasterList.FirstOrDefault().CreateUser;
                sb.IsActive = sb.ItemMasterList.FirstOrDefault().IsActive.ToString();
                ViewBag.action = "Update";
                sb.Action = "Update";
                return View("AddItemMaster", sb);
            }
            catch (Exception ex)
            {
                var data = new { Message = ex.Message.ToString(), Type = "error" };
                TempData["SweetAlert"] = data;
                return RedirectToAction("AddItemMaster", "Master");
            }
        }
        #endregion

        #region==> GST Master
        public ActionResult ViewGSTMaster()
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return RedirectToAction("Login", "Home");
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
                //
                GSTMasterViewModel mv = new GSTMasterViewModel();
                mv.Action = "all";
                mv.CompanyCode = LoggedUserDetails.CompanyCode;
                var GSTMasterList = ApiCall.PostApi("GSTMasterRtr", Newtonsoft.Json.JsonConvert.SerializeObject(mv));
                mv = JsonConvert.DeserializeObject<GSTMasterViewModel>(GSTMasterList);
                return View(mv.GSTMasterList);

            }
            catch (Exception ex)
            {
                var data = new { Message = ex.Message.ToString(), Type = "error" };
                TempData["SweetAlert"] = data;
                return RedirectToAction("Dashboard", "Home");
            }
        }
        public ActionResult AddGSTMaster()
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return RedirectToAction("Login", "Home");
                }
                GSTMasterViewModel model = new GSTMasterViewModel();
                model.CompanyCode = LoggedUserDetails.CompanyCode;
                model.Action = "Save";
                return View(model);
            }
            catch (Exception ex)
            {
                var data = new { Message = ex.Message.ToString(), Type = "error" };
                TempData["SweetAlert"] = data;
                return RedirectToAction("Dashboard", "Home");
            }
        }
        [HttpPost]
        public ActionResult AddGSTMaster(GSTMasterViewModel sm)
        {
            try
            {
                if (sm.GSTId == null)
                {
                    sm.CreateDate = generalFunctions.getTimeZoneDatetimedb();
                    sm.Action = "insert";
                    sm.CreateUser = User.Identity.Name;
                    sm.CompanyCode = LoggedUserDetails.CompanyCode;
                    sm.FromDate = generalFunctions.dateconvert(sm.FromDate);
                    sm.ToDate = generalFunctions.dateconvert(sm.ToDate);
                    var GSTMasterList = ApiCall.PostApi("GSTMasterInsUpd", Newtonsoft.Json.JsonConvert.SerializeObject(sm));
                    sm = JsonConvert.DeserializeObject<GSTMasterViewModel>(GSTMasterList);

                    string msg = sm.result;
                    if (msg.Contains("successfully"))
                    {
                        var data = new { Message = msg, Type = "success" };
                        TempData["SweetAlert"] = data;
                        return RedirectToAction("AddGSTMaster", "Master");
                    }
                    else
                    {
                        var data = new { Message = msg, Type = "error" };
                        TempData["SweetAlert"] = data;
                        return RedirectToAction("AddGSTMaster", "Master");
                    }
                }
                else
                {
                    sm.Action = "active";
                    sm.CreateDate = generalFunctions.DateTimeConvert(sm.CreateDate);
                    sm.CreateUser = User.Identity.Name;
                    sm.updatedate = generalFunctions.getTimeZoneDatetimedb();
                    sm.UpdateUser = User.Identity.Name;
                    sm.CompanyCode = LoggedUserDetails.CompanyCode;
                    var GSTMasterList = ApiCall.PostApi("GSTMasterInsUpd", Newtonsoft.Json.JsonConvert.SerializeObject(sm));
                    sm = JsonConvert.DeserializeObject<GSTMasterViewModel>(GSTMasterList);
                    string msg = sm.result;
                    if (msg.Contains("successfully"))
                    {
                        var data = new { Message = msg, Type = "success" };
                        TempData["SweetAlert"] = data;
                        return RedirectToAction("AddGSTMaster", "Master");
                    }
                    else
                    {
                        var data = new { Message = msg, Type = "error" };
                        TempData["SweetAlert"] = data;
                        return RedirectToAction("AddGSTMaster", "Master");
                    }
                }
                //    sm.Action = "update";
                //    sm.CreateDate = generalFunctions.DateTimeConvert(sm.CreateDate);
                //    sm.CreateUser = User.Identity.Name;                   
                //    sm.CompanyCode = LoggedUserDetails.CompanyCode;
                //    //sm.FromDate = generalFunctions.dateconvert(sm.FromDate);
                //    //sm.ToDate = generalFunctions.dateconvert(sm.ToDate);
                //    var GSTMasterList = ApiCall.PostApi("GSTMasterInsUpd", Newtonsoft.Json.JsonConvert.SerializeObject(sm));
                //    sm = JsonConvert.DeserializeObject<GSTMasterViewModel>(GSTMasterList);
                //    //sm.FromDate = generalFunctions.dateconvert(sm.FromDate);
                //    //sm.ToDate = generalFunctions.dateconvert(sm.ToDate);
                //    string msg = sm.result;
                //    //if (msg.Contains("successfully"))
                //    //{
                //    //    var data = new { Message = msg, Type = "success" };
                //    //    TempData["SweetAlert"] = data;
                //    //    return RedirectToAction("AddGSTMaster", "Master");
                //    //}
                //    //else
                //    //{
                //    //    var data = new { Message = msg, Type = "error" };
                //    //    TempData["SweetAlert"] = data;
                //    //    return RedirectToAction("AddGSTMaster", "Master");
                //    //}
                //    return RedirectToAction("AddGSTMaster", "Master");
                //}
            }
            catch (Exception ex)
            {
                var data = new { Message = ex.Message.ToString(), Type = "error" };
                TempData["SweetAlert"] = data;
                return RedirectToAction("AddGSTMaster", "Master");
            }
        }
        public ActionResult EditGSTMaster(int id)
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return RedirectToAction("Login", "Home");
                }
                GSTMasterViewModel sb = new GSTMasterViewModel();
                sb.CompanyCode = LoggedUserDetails.CompanyCode;
                sb.Action = "Details";
                sb.GSTId = id.ToString();
                var GSTMasterList = ApiCall.PostApi("GSTMasterRtr", Newtonsoft.Json.JsonConvert.SerializeObject(sb));
                sb = JsonConvert.DeserializeObject<GSTMasterViewModel>(GSTMasterList);
                sb.GSTId = sb.GSTMasterList.FirstOrDefault().GSTId.ToString();
                sb.GSTName = sb.GSTMasterList.FirstOrDefault().GSTName;
                sb.GSTPerc = sb.GSTMasterList.FirstOrDefault().GSTPerc.ToString();
                sb.IsActive = sb.GSTMasterList.FirstOrDefault().IsActive.ToString();
                sb.FromDate = sb.GSTMasterList.FirstOrDefault().FromDate.ToString();
                sb.ToDate = sb.GSTMasterList.FirstOrDefault().ToDate.ToString();
                //sb.FromDate = generalFunctions.dateconvert(sb.FromDate);
                //sb.ToDate = generalFunctions.dateconvert(sb.ToDate);
                sb.CreateDate = sb.GSTMasterList.FirstOrDefault().CreateDate.ToString();
                sb.CreateUser = sb.GSTMasterList.FirstOrDefault().CreateUser;

                ViewBag.action = "Update";
                sb.Action = "Update";
                return View("AddGSTMaster", sb);
            }
            catch (Exception ex)
            {
                var data = new { Message = ex.Message.ToString(), Type = "error" };
                TempData["SweetAlert"] = data;
                return RedirectToAction("AddGSTMaster", "Master");
            }
        }
        #endregion

        #endregion
    }
}