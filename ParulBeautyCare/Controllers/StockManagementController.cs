using ParulBeautyCareViewModel.ViewModel.StockMgmtViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ParulBeautyCareDbClasses.DataModels;
using System.Web.Security;
using static ParulBeautyCare.GeneralClasses.GeneralClass;
using Newtonsoft.Json;
using ParulBeautyCare.GeneralClasses;
using ParulBeautyCareViewModel.ViewModel;
using ParulBeautyCareViewModel.ViewModel.Master;
using System.Drawing;
using System.IO;
using ZXing;
using System.Data;

namespace ParulBeautyCare.Controllers
{
    public class StockManagementController : GeneralClass
    {
        parulbeautycareEntities db = new parulbeautycareEntities();
        // GET: StockManagement

        #region ==> Stock Purchase
        public ActionResult ViewStockPurchase()
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
                //

                StockPurchaseViewModel spvm = new StockPurchaseViewModel();
                spvm.CompanyCode = LoggedUserDetails.CompanyCode;
                spvm.Action = "all";

                //Stock Purchase List 
                var StockPurchaseList = ApiCall.PostApi("StockPurchaseRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(spvm));
                spvm = JsonConvert.DeserializeObject<StockPurchaseViewModel>(StockPurchaseList);
                //



                return View(spvm);

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
        public ActionResult AddStockPurchase()
        {

            try
            {
                StockPurchaseViewModel spvm = new StockPurchaseViewModel();
                spvm.CompanyCode = LoggedUserDetails.CompanyCode;
                spvm.Action = "active";

                if (!User.Identity.IsAuthenticated)
                {
                    FormsAuthentication.RedirectToLoginPage();
                }
                string url = generalFunctions.getCommon(Request.Url.AbsoluteUri);

                //Product List Bind
                ProductMasterViewModel pm = new ProductMasterViewModel();
                var ProdList = ApiCall.PostApi("ProductMasterRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(spvm));
                pm = JsonConvert.DeserializeObject<ProductMasterViewModel>(ProdList);
                spvm.ProductList = pm.ProductMasterList;
                //

                //Department List Bind
                DepartmentMasterViewModel dm = new DepartmentMasterViewModel();
                var DeptList = ApiCall.PostApi("DepartmentMasterRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(spvm));
                dm = JsonConvert.DeserializeObject<DepartmentMasterViewModel>(DeptList);
                spvm.DeptList = dm.DepartmentList;
                //

                //Vendor List Bind
                VendorMasterViewModel vnd = new VendorMasterViewModel();
                var VendList = ApiCall.PostApi("VendorMasterRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(spvm));
                vnd = JsonConvert.DeserializeObject<VendorMasterViewModel>(VendList);
                spvm.VendorList = vnd.VendorList;
                //

                return View(spvm);
            }
            catch (Exception ex)
            {
                var data = new { Message = ex.Message.ToString(), Type = "error" };
                TempData["SweetAlert"] = data;
                return RedirectToAction("Dashboard", "Home");
            }
        }
       
        [HttpPost]
        public ActionResult AddStockPurchase(StockPurchaseViewModel spvm)
        {
            try
            {
                //Product List Bind
                spvm.Action = "all";
                ProductMasterViewModel pm = new ProductMasterViewModel();
                var ProdList = ApiCall.PostApi("ProductMasterRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(spvm));
                pm = JsonConvert.DeserializeObject<ProductMasterViewModel>(ProdList);
                spvm.ProductList = pm.ProductMasterList;
                //

                //Department List Bind
                spvm.Action = "all";
                DepartmentMasterViewModel dm = new DepartmentMasterViewModel();
                var DeptList = ApiCall.PostApi("DepartmentMasterRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(spvm));
                dm = JsonConvert.DeserializeObject<DepartmentMasterViewModel>(DeptList);
                spvm.DeptList = dm.DepartmentList;
                //

                //Vendor List Bind
                VendorMasterViewModel vnd = new VendorMasterViewModel();
                var VendList = ApiCall.PostApi("VendorMasterRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(spvm));
                vnd = JsonConvert.DeserializeObject<VendorMasterViewModel>(VendList);
                spvm.VendorList = vnd.VendorList;
                //
                if (spvm.PurchaseId == null)
                {
                    spvm.CompanyCode = LoggedUserDetails.CompanyCode;
                    spvm.Action = "Active";
                    spvm.CreateUser = User.Identity.Name;
                    spvm.CreateDate = generalFunctions.getTimeZoneDatetimedb();

                    if (!User.Identity.IsAuthenticated)
                    {
                        FormsAuthentication.RedirectToLoginPage();
                    }
                    string url = generalFunctions.getCommon(Request.Url.AbsoluteUri);


                    spvm.Action = "insert";
                    spvm.MfgDate = generalFunctions.dateconvert(spvm.MfgDate);
                    spvm.ExpDate = generalFunctions.dateconvert(spvm.ExpDate);
                    spvm.PurchaseDate = generalFunctions.dateconvert(spvm.PurchaseDate);
                    var apiResponse = ApiCall.PostApi("StockPurchaseInsUpd", Newtonsoft.Json.JsonConvert.SerializeObject(spvm));
                    spvm = JsonConvert.DeserializeObject<StockPurchaseViewModel>(apiResponse);
                    string msg = spvm.result;

                    if (msg.Contains("Added Successfully"))
                    {
                        var data = new { Message = msg, Type = "success" };
                        TempData["SweetAlert"] = data;
                        return RedirectToAction("AddStockPurchase", "StockManagement");
                    }
                    else
                    {
                        var data = new { Message = msg, Type = "error" };
                        TempData["SweetAlert"] = data;
                        return RedirectToAction("AddStockPurchase", "StockManagement");
                    }

                }
                else
                {
                    spvm.Action = "update";
                    spvm.UpdateDate = generalFunctions.getTimeZoneDatetimedb();
                    spvm.CompanyCode = LoggedUserDetails.CompanyCode;
                    spvm.UpdateUser = User.Identity.Name;
                    spvm.CreateDate = generalFunctions.DateTimeConvert(spvm.CreateDate);
                    spvm.MfgDate = generalFunctions.dateconvert(spvm.MfgDate);
                    spvm.ExpDate = generalFunctions.dateconvert(spvm.ExpDate);
                    spvm.PurchaseDate = generalFunctions.dateconvert(spvm.PurchaseDate);
                    var apiResponse = ApiCall.PostApi("StockPurchaseInsUpd", Newtonsoft.Json.JsonConvert.SerializeObject(spvm));
                    spvm = JsonConvert.DeserializeObject<StockPurchaseViewModel>(apiResponse);
                    string msg = spvm.result;
                    if (msg.Contains("Updated Successfully"))
                    {
                        var data = new { Message = msg, Type = "success" };
                        TempData["SweetAlert"] = data;
                        return RedirectToAction("ViewStockPurchase", "StockManagement");
                    }
                    else
                    {
                        //ViewBag.Message = msg.ToUpper();
                        //Danger(msg, true);
                        //return View(sm);
                        var data = new { Message = msg, Type = "error" };
                        TempData["SweetAlert"] = data;
                        return RedirectToAction("ViewStockPurchase", "StockManagement");
                    }
                }
            }
            catch (Exception ex)
            {
                //Danger(ex.Message.ToString(), true);
                var data = new { Message = ex.Message.ToString(), Type = "error" };

                TempData["SweetAlert"] = data;
                return RedirectToAction("StockPurchase", "StockManagement");
            }
        }

        public ActionResult EditStockPurchase(int purchaseid)
        {

            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    FormsAuthentication.RedirectToLoginPage();
                }
                StockPurchaseViewModel spvm = new StockPurchaseViewModel();
                spvm.Action = "details";
                spvm.PurchaseId = purchaseid.ToString();
                spvm.CompanyCode = LoggedUserDetails.CompanyCode;
                var StockPurchaseList = ApiCall.PostApi("StockPurchaseRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(spvm));
                spvm = JsonConvert.DeserializeObject<StockPurchaseViewModel>(StockPurchaseList);


                //Product List Bind
                spvm.Action = "all";
                ProductMasterViewModel pm = new ProductMasterViewModel();
                var ProdList = ApiCall.PostApi("ProductMasterRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(spvm));
                pm = JsonConvert.DeserializeObject<ProductMasterViewModel>(ProdList);
                spvm.ProductList = pm.ProductMasterList;
                //

                //Department List Bind
                spvm.Action = "all";
                DepartmentMasterViewModel dm = new DepartmentMasterViewModel();
                var DeptList = ApiCall.PostApi("DepartmentMasterRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(spvm));
                dm = JsonConvert.DeserializeObject<DepartmentMasterViewModel>(DeptList);
                spvm.DeptList = dm.DepartmentList;
                //

                //Vendor List Bind
                VendorMasterViewModel vnd = new VendorMasterViewModel();
                var VendList = ApiCall.PostApi("VendorMasterRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(spvm));
                vnd = JsonConvert.DeserializeObject<VendorMasterViewModel>(VendList);
                spvm.VendorList = vnd.VendorList;
                //

                spvm.PurchaseId = spvm.StockPurchaseList.FirstOrDefault().PurchaseId.ToString();
                spvm.PurchaseDate = generalFunctions.ShortDateConvert(spvm.StockPurchaseList.FirstOrDefault().PurchaseDate.ToString());
                spvm.MfgDate = generalFunctions.ShortDateConvert(spvm.StockPurchaseList.FirstOrDefault().MfgDate.ToString());
                spvm.ExpDate = generalFunctions.ShortDateConvert(spvm.StockPurchaseList.FirstOrDefault().ExpDate.ToString());
                spvm.Quantity = spvm.StockPurchaseList.FirstOrDefault().Quantity.ToString();
                spvm.VendorId = spvm.StockPurchaseList.FirstOrDefault().VendorId.ToString();
                spvm.ProductId = spvm.StockPurchaseList.FirstOrDefault().ProductId.ToString();
                spvm.DepartmentId = spvm.StockPurchaseList.FirstOrDefault().DepartmentId.ToString();
                spvm.CompanyCode = spvm.StockPurchaseList.FirstOrDefault().CompanyCode.ToString();
                spvm.CreateDate = spvm.StockPurchaseList.FirstOrDefault().CreateDate.ToString();
                spvm.CreateUser = spvm.StockPurchaseList.FirstOrDefault().CreateUser.ToString();
                spvm.ProductTypeId = spvm.StockPurchaseList.FirstOrDefault().ProductTypeId.ToString();

                ViewBag.action = "Update";
                spvm.Action = "Update";
                return View("AddStockPurchase", spvm);
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

        #region ==> Stock Allocation
        public ActionResult ViewStockAllocation()
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
                //

                StockAllocationMasterViewModel savm = new StockAllocationMasterViewModel();
                savm.CompanyCode = LoggedUserDetails.CompanyCode;
                savm.Action = "all";

                //Stock Purchase List 
                var StockAllocationList = ApiCall.PostApi("StockAllocationRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(savm));
                savm = JsonConvert.DeserializeObject<StockAllocationMasterViewModel>(StockAllocationList);
                //



                return View(savm);

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
        public ActionResult AddStockAllocation()
        {

            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    FormsAuthentication.RedirectToLoginPage();
                }
                string url = generalFunctions.getCommon(Request.Url.AbsoluteUri);

                StockAllocationMasterViewModel sam = new StockAllocationMasterViewModel();
                sam.Action = "Active";
                sam.CompanyCode = LoggedUserDetails.CompanyCode;

                ////Product List Bind
                //ProductMasterViewModel pm = new ProductMasterViewModel();
                //var ProdList = ApiCall.PostApi("ProductMasterRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(sam));
                //pm = JsonConvert.DeserializeObject<ProductMasterViewModel>(ProdList);
                //sam.ProductList = pm.ProductMasterList;
                ////
             
                //Product List Bind from PurchaseMaster
                sam.Action = "all";
                StockPurchaseViewModel pm = new StockPurchaseViewModel();
                var ProdList = ApiCall.PostApi("StockPurchaseRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(sam));
                pm = JsonConvert.DeserializeObject<StockPurchaseViewModel>(ProdList);
                sam.ProdListfromPurchaseMaster = pm.StockPurchaseList;
                //

                //Staff List Bind
                StaffMasterViewModel sm = new StaffMasterViewModel();
                var StaffList = ApiCall.PostApi("StaffMasterRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(sam));
                sm = JsonConvert.DeserializeObject<StaffMasterViewModel>(StaffList);
                sam.StaffList = sm.StaffMasterList;
                //

                sam.Action = "Save";

                return View(sam);
            }
            catch (Exception ex)
            {
                var data = new { Message = ex.Message.ToString(), Type = "error" };
                TempData["SweetAlert"] = data;
                return RedirectToAction("Dashboard", "Home");
            }
        }

        [HttpPost]
        public ActionResult AddStockAllocation(StockAllocationMasterViewModel savm)
        {
            try
            {

                if (savm.StockAllocationId == null)
                {
                    savm.CompanyCode = LoggedUserDetails.CompanyCode;
                    savm.Action = "insert";
                    savm.AllocateUser = LoggedUserDetails.UserName;
                    savm.AllocationDate = generalFunctions.getDate();
                    if (!User.Identity.IsAuthenticated)
                    {
                        FormsAuthentication.RedirectToLoginPage();
                    }
                    string url = generalFunctions.getCommon(Request.Url.AbsoluteUri);

                    savm.Action = "insert";

                    var apiResponse = ApiCall.PostApi("StockAllocationInsUpd", Newtonsoft.Json.JsonConvert.SerializeObject(savm));
                    savm = JsonConvert.DeserializeObject<StockAllocationMasterViewModel>(apiResponse);
                    string msg = savm.result;

                    if (msg.Contains("Allocated Successfully"))
                    {
                        var data = new { Message = msg, Type = "success" };
                        TempData["SweetAlert"] = data;
                        // return RedirectToAction("AddStockAllocation", "StockManagement");
                    }
                    else
                    {
                        var data = new { Message = msg, Type = "error" };
                        TempData["SweetAlert"] = data;
                        // return RedirectToAction("AddStockAllocation", "StockManagement");
                    }

                }
                else
                {
                    savm.Action = "update";
                    savm.UpdateDate = generalFunctions.getTimeZoneDatetimedb();
                    savm.UpdateUser = User.Identity.Name;

                    savm.AllocationDate = generalFunctions.DateTimeConvert(savm.AllocationDate);
                    var apiResponse = ApiCall.PostApi("StockAllocationInsUpd", Newtonsoft.Json.JsonConvert.SerializeObject(savm));
                    savm = JsonConvert.DeserializeObject<StockAllocationMasterViewModel>(apiResponse);
                    string msg = savm.result;
                    if (msg.Contains("Updated Successfully"))
                    {
                        var data = new { Message = msg, Type = "success" };
                        TempData["SweetAlert"] = data;
                        return RedirectToAction("ViewStockAllocation", "StockManagement");
                    }
                    else
                    {
                        //ViewBag.Message = msg.ToUpper();
                        //Danger(msg, true);
                        //return View(sm);
                        var data = new { Message = msg, Type = "error" };
                        TempData["SweetAlert"] = data;
                        return RedirectToAction("ViewStockAllocation", "StockManagement");
                    }
                }
                return RedirectToAction("AddStockAllocation", "StockManagement");
            }
            catch (Exception ex)
            {
                //Danger(ex.Message.ToString(), true);
                var data = new { Message = ex.Message.ToString(), Type = "error" };

                TempData["SweetAlert"] = data;
                return RedirectToAction("AddStockAllocation", "StockManagement");
            }
        }
        public ActionResult EditStockAllocation(int id)
        {

            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    FormsAuthentication.RedirectToLoginPage();
                }
                StockAllocationMasterViewModel samv = new StockAllocationMasterViewModel();
                samv.Action = "detail";
                samv.StockAllocationId = id.ToString();
                samv.CompanyCode = LoggedUserDetails.CompanyCode;

                var StockAllocationList = ApiCall.PostApi("StockAllocationRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(samv));
                samv = JsonConvert.DeserializeObject<StockAllocationMasterViewModel>(StockAllocationList);

                //Staff List Bind
                samv.Action = "Active";
                StaffMasterViewModel sm = new StaffMasterViewModel();
                var StaffList = ApiCall.PostApi("StaffMasterRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(samv));
                sm = JsonConvert.DeserializeObject<StaffMasterViewModel>(StaffList);
                samv.StaffList = sm.StaffMasterList;
                //

                ////Product List Bind
                //samv.Action = "Active";
                //ProductMasterViewModel pm = new ProductMasterViewModel();
                //var ProdList = ApiCall.PostApi("ProductMasterRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(samv));
                //pm = JsonConvert.DeserializeObject<ProductMasterViewModel>(ProdList);
                //samv.ProductList = pm.ProductMasterList;
                ////
                //Product List Bind from PurchaseMaster
                samv.Action = "all";
                StockPurchaseViewModel pm = new StockPurchaseViewModel();
                var ProdList = ApiCall.PostApi("StockPurchaseRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(samv));
                pm = JsonConvert.DeserializeObject<StockPurchaseViewModel>(ProdList);
                samv.ProdListfromPurchaseMaster = pm.StockPurchaseList;
                //

                samv.StockAllocationId = samv.StockAllocationList.FirstOrDefault().StockAllocationId.ToString();
                samv.ProductId = samv.StockAllocationList.FirstOrDefault().ProductId.ToString();
                samv.AllocationDate = generalFunctions.ShortDateConvert(samv.StockAllocationList.FirstOrDefault().AllocationDate.ToString());
                samv.AllocateUser = generalFunctions.ShortDateConvert(samv.StockAllocationList.FirstOrDefault().AllocateUser.ToString());
                samv.StaffId = samv.StockAllocationList.FirstOrDefault().StaffId.ToString();
                samv.Qty = Convert.ToInt32(samv.StockAllocationList.FirstOrDefault().Qty);
                samv.Action = "update";

                return View("AddStockAllocation", samv);
            }
            catch (Exception ex)
            {
                //Danger(ex.Message.ToString(), true);
                //return RedirectToAction("Dashboard", "Home");
                var data = new { Message = ex.Message.ToString(), Type = "error" };
                TempData["SweetAlert"] = data;
                return RedirectToAction("ViewStockAllocation", "StockManagement");
            }

        }
        #endregion

        #region ==> Stock Transfer
        public ActionResult ViewStockTransfer()
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
                //

                StockTransferHeaderViewModel sdmvm = new StockTransferHeaderViewModel();

                //Staff List Bind
                StaffMasterViewModel sm = new StaffMasterViewModel();
                var StaffList = ApiCall.PostApi("StaffMasterRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(sdmvm));
                sm = JsonConvert.DeserializeObject<StaffMasterViewModel>(StaffList);
                sdmvm.StaffList = sm.StaffMasterList;
                //

                return View(sdmvm);

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
        public ActionResult AddStockTransfer()
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

                StockTransferHeaderViewModel sthvm = new StockTransferHeaderViewModel();
                sthvm.Action = "Active";
                sthvm.CompanyCode = LoggedUserDetails.CompanyCode;

                //Staff List Bind
                StaffMasterViewModel sm = new StaffMasterViewModel();
                var StaffList = ApiCall.PostApi("StaffMasterRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(sthvm));
                sm = JsonConvert.DeserializeObject<StaffMasterViewModel>(StaffList);
                sthvm.StaffList = sm.StaffMasterList;
                //

                //Stock Purchase List 
                StockTransferHeaderViewModel ss = new StockTransferHeaderViewModel();
                var StockAvailableList = ApiCall.PostApi("AvailableStockRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(sthvm));
                sthvm = JsonConvert.DeserializeObject<StockTransferHeaderViewModel>(StockAvailableList);
                //

                return View(sthvm);
            }
            catch (Exception ex)
            {
                var data = new { Message = ex.Message.ToString(), Type = "error" };
                TempData["SweetAlert"] = data;
                return RedirectToAction("Dashboard", "Home");
            }
        }

        [HttpPost]
        public ActionResult AddStockTransfer(StockTransferHeaderViewModel sthvm,FormCollection formCollection)
        {
            try
            { 
                var selectedIds = formCollection["selectedIds"].Split(',');

                var StockAvailableList = ApiCall.PostApi("AvailableStockRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(sthvm));
                sthvm = JsonConvert.DeserializeObject<StockTransferHeaderViewModel>(StockAvailableList);

                if (sthvm.StockTransferTypeTable == null)
                {
                    sthvm.StockTransferTypeTable = new List<StockTransferTypeViewModel>();
                }
                foreach (var i in selectedIds)
                {
                    var selectedItem = sthvm.AvailableStockList.FirstOrDefault(x => x.StockDetailId == Convert.ToInt32(i));
                    if (selectedItem != null)
                    {
                        var newTransferItem = new StockTransferTypeViewModel
                        {
                            StockDetailId = selectedItem.StockDetailId.ToString(),
                            StockHeaderId = "", // Set the value of StockHeaderId if needed
                            ProductId = selectedItem.ProductId.ToString(),
                            Qty = "1", // Set the value of Qty if needed
                            TotalPerson = selectedItem.NoOfPerson.ToString(),
                            PersonAvailable = selectedItem.availperson.ToString(),
                            AutoSrNo = selectedItem.AutoSrNo,
                            Barcode = selectedItem.Barcode,
                            Price = selectedItem.Price.ToString(),
                            ExpDate = selectedItem.ExpDate
                        };

                        sthvm.StockTransferTypeTable.Add(newTransferItem);
                    }
                }


                if (sthvm.StockHeaderId == null)
                {
                    if (!User.Identity.IsAuthenticated)
                    {
                        FormsAuthentication.RedirectToLoginPage();
                    }
                    string url = generalFunctions.getCommon(Request.Url.AbsoluteUri);

                    sthvm.CreateDate = generalFunctions.getTimeZoneDatetimedb();
                    sthvm.CreateUser = User.Identity.Name;
                    sthvm.TransferDate = generalFunctions.getTimeZoneDatetimedb();
                    sthvm.Action = "insert";
                    var apiResponse = ApiCall.PostApi("StockTransferIns", Newtonsoft.Json.JsonConvert.SerializeObject(sthvm));
                   sthvm = JsonConvert.DeserializeObject<StockTransferHeaderViewModel>(apiResponse);
                    string msg = sthvm.result;

                    if (msg.Contains("Stock Transfer successfully"))
                    {
                        var data = new { Message = msg, Type = "success" };
                        TempData["SweetAlert"] = data;
                        // return RedirectToAction("AddStockAllocation", "StockManagement");
                    }
                    else
                    {
                        var data = new { Message = msg, Type = "error" };
                        TempData["SweetAlert"] = data;
                        // return RedirectToAction("AddStockAllocation", "StockManagement");
                    }

                }
                //else
                //{
                //    savm.Action = "update";
                //    savm.UpdateDate = generalFunctions.getTimeZoneDatetimedb();
                //    savm.UpdateUser = User.Identity.Name;

                //    savm.AllocationDate = generalFunctions.DateTimeConvert(savm.AllocationDate);
                //    var apiResponse = ApiCall.PostApi("StockAllocationInsUpd", Newtonsoft.Json.JsonConvert.SerializeObject(savm));
                //    savm = JsonConvert.DeserializeObject<StockAllocationMasterViewModel>(apiResponse);
                //    string msg = savm.result;
                //    if (msg.Contains("Updated Successfully"))
                //    {
                //        var data = new { Message = msg, Type = "success" };
                //        TempData["SweetAlert"] = data;
                //        return RedirectToAction("ViewStockAllocation", "StockManagement");
                //    }
                //    else
                //    {
                //        //ViewBag.Message = msg.ToUpper();
                //        //Danger(msg, true);
                //        //return View(sm);
                //        var data = new { Message = msg, Type = "error" };
                //        TempData["SweetAlert"] = data;
                //        return RedirectToAction("ViewStockAllocation", "StockManagement");
                //    }
                //}
                return RedirectToAction("AddStockTransfer", "StockManagement");
            }
            catch (Exception ex)
            {
                //Danger(ex.Message.ToString(), true);
                var data = new { Message = ex.Message.ToString(), Type = "error" };

                TempData["SweetAlert"] = data;
                return RedirectToAction("AddStockTransfer", "StockManagement");
            }
        }
        public ActionResult StockAvailable(int id)
        {

            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    FormsAuthentication.RedirectToLoginPage();
                }
                string url = generalFunctions.getCommon(Request.Url.AbsoluteUri);

                StockTransferHeaderViewModel sthvm = new StockTransferHeaderViewModel();
                sthvm.Action = "Active";
                sthvm.CompanyCode = LoggedUserDetails.CompanyCode;

                //Staff List Bind
                StaffMasterViewModel sm = new StaffMasterViewModel();
                var StaffList = ApiCall.PostApi("StaffMasterRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(sthvm));
                sm = JsonConvert.DeserializeObject<StaffMasterViewModel>(StaffList);
                sthvm.StaffList = sm.StaffMasterList;
                //

                //Stock Purchase List 
                sthvm.FromStaffId = id.ToString();
                StockTransferHeaderViewModel ss = new StockTransferHeaderViewModel();
                var StockAvailableList = ApiCall.PostApi("AvailableStockRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(sthvm));
                sthvm = JsonConvert.DeserializeObject<StockTransferHeaderViewModel>(StockAvailableList);
                //
                foreach (var row in sthvm.AvailableStockList)
                {
                    Bitmap img = null;
                    using (var ms = new MemoryStream())
                    {
                        var writer = new ZXing.BarcodeWriter() { Format = BarcodeFormat.EAN_13 };
                        writer.Options.Height = 40;
                        writer.Options.Width = 60;
                        writer.Options.PureBarcode = true;
                        img = writer.Write(row.AutoSrNo.ToString());
                        img.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                        row.Barcode = Convert.ToBase64String(ms.ToArray());
                    }
                }

                return View("AddStockTransfer", sthvm);
               //return Json(sthvm.AvailableStockList, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                var data = new { Message = ex.Message.ToString(), Type = "error" };
                TempData["SweetAlert"] = data;
                return RedirectToAction("Dashboard", "Home");
            }
        }
        public ActionResult StockAvailable2(int id,string pname)
        {

            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    FormsAuthentication.RedirectToLoginPage();
                }
                string url = generalFunctions.getCommon(Request.Url.AbsoluteUri);

                StockTransferHeaderViewModel sthvm = new StockTransferHeaderViewModel();
                sthvm.Action = "Active";
                sthvm.CompanyCode = LoggedUserDetails.CompanyCode;

                //Staff List Bind
                StaffMasterViewModel sm = new StaffMasterViewModel();
                var StaffList = ApiCall.PostApi("StaffMasterRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(sthvm));
                sm = JsonConvert.DeserializeObject<StaffMasterViewModel>(StaffList);
                sthvm.StaffList = sm.StaffMasterList;
                //

                //Stock Purchase List 
                sthvm.FromStaffId = id.ToString();
                StockTransferHeaderViewModel ss = new StockTransferHeaderViewModel();
                var StockAvailableList = ApiCall.PostApi("AvailableStockRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(sthvm));
                sthvm = JsonConvert.DeserializeObject<StockTransferHeaderViewModel>(StockAvailableList);
                //
                foreach (var row in sthvm.AvailableStockList.Where(x=>x.ProductName==pname))
                {
                    Bitmap img = null;
                    using (var ms = new MemoryStream())
                    {
                        var writer = new ZXing.BarcodeWriter() { Format = BarcodeFormat.EAN_13 };
                        writer.Options.Height = 40;
                        writer.Options.Width = 60;
                        writer.Options.PureBarcode = true;
                        img = writer.Write(row.AutoSrNo.ToString());
                        img.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                        row.Barcode = Convert.ToBase64String(ms.ToArray());
                    }
                }
                sthvm.AvailableStockList = sthvm.AvailableStockList.Where(x => x.ProductName == pname).ToList();

                return View("AddStockTransfer", sthvm);
                //return Json(sthvm.AvailableStockList, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                var data = new { Message = ex.Message.ToString(), Type = "error" };
                TempData["SweetAlert"] = data;
                return RedirectToAction("Dashboard", "Home");
            }
        }

        //public ActionResult EditStockAllocation(int id)
        //{

        //    try
        //    {
        //        if (!User.Identity.IsAuthenticated)
        //        {
        //            FormsAuthentication.RedirectToLoginPage();
        //        }
        //        StockAllocationMasterViewModel samv = new StockAllocationMasterViewModel();
        //        samv.Action = "detail";
        //        samv.StockAllocationId = id.ToString();
        //        samv.CompanyCode = LoggedUserDetails.CompanyCode;

        //        var StockAllocationList = ApiCall.PostApi("StockAllocationRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(samv));
        //        samv = JsonConvert.DeserializeObject<StockAllocationMasterViewModel>(StockAllocationList);

        //        //Staff List Bind
        //        samv.Action = "Active";
        //        StaffMasterViewModel sm = new StaffMasterViewModel();
        //        var StaffList = ApiCall.PostApi("StaffMasterRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(samv));
        //        sm = JsonConvert.DeserializeObject<StaffMasterViewModel>(StaffList);
        //        samv.StaffList = sm.StaffMasterList;
        //        //

        //        //Product List Bind
        //        samv.Action = "Active";
        //        ProductMasterViewModel pm = new ProductMasterViewModel();
        //        var ProdList = ApiCall.PostApi("ProductMasterRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(samv));
        //        pm = JsonConvert.DeserializeObject<ProductMasterViewModel>(ProdList);
        //        samv.ProductList = pm.ProductMasterList;
        //        //

        //        samv.StockAllocationId = samv.StockAllocationList.FirstOrDefault().StockAllocationId.ToString();
        //        samv.ProductId = samv.StockAllocationList.FirstOrDefault().ProductId.ToString();
        //        samv.AllocationDate = generalFunctions.ShortDateConvert(samv.StockAllocationList.FirstOrDefault().AllocationDate.ToString());
        //        samv.AllocateUser = generalFunctions.ShortDateConvert(samv.StockAllocationList.FirstOrDefault().AllocateUser.ToString());
        //        samv.StaffId = samv.StockAllocationList.FirstOrDefault().StaffId.ToString();
        //        samv.Qty = Convert.ToInt32(samv.StockAllocationList.FirstOrDefault().Qty);
        //        samv.Action = "update";

        //        return View("AddStockAllocation", samv);
        //    }
        //    catch (Exception ex)
        //    {
        //        //Danger(ex.Message.ToString(), true);
        //        //return RedirectToAction("Dashboard", "Home");
        //        var data = new { Message = ex.Message.ToString(), Type = "error" };
        //        TempData["SweetAlert"] = data;
        //        return RedirectToAction("ViewStockAllocation", "StockManagement");
        //    }

        //}

        #endregion


        #region==> Stock Allocated to Staff Report
        public ActionResult ViewStockAllocatedToStaff()
        {
            try
            {
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
                //

                StockTransferHeaderViewModel sdmvm = new StockTransferHeaderViewModel();
                sdmvm.Action = "All";
                sdmvm.CompanyCode = LoggedUserDetails.CompanyCode;
                var StockAllocatedToStaffList = ApiCall.PostApi("StockAllocatedToStaffRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(sdmvm));
                sdmvm = JsonConvert.DeserializeObject<StockTransferHeaderViewModel>(StockAllocatedToStaffList);
                return View(sdmvm.StockAllocatedToStaffList);

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
        #endregion
    }
}