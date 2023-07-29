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
                spvm.Action = "all";

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

                spvm.PurchaseId = spvm.StockPurchaseList.FirstOrDefault().PurchaseId.ToString();
                spvm.PurchaseDate = generalFunctions.ShortDateConvert(spvm.StockPurchaseList.FirstOrDefault().PurchaseDate.ToString());
                spvm.MfgDate = generalFunctions.ShortDateConvert(spvm.StockPurchaseList.FirstOrDefault().MfgDate.ToString());
                spvm.ExpDate = generalFunctions.ShortDateConvert(spvm.StockPurchaseList.FirstOrDefault().ExpDate.ToString());
                spvm.Quantity = spvm.StockPurchaseList.FirstOrDefault().Quantity.ToString();
                spvm.Vendor = spvm.StockPurchaseList.FirstOrDefault().Vendor.ToString();
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
                //Staff List Bind
                ProductMasterViewModel pm = new ProductMasterViewModel();
                var ProdList = ApiCall.PostApi("ProductMasterRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(sam));
                pm = JsonConvert.DeserializeObject<ProductMasterViewModel>(ProdList);
                sam.ProductList = pm.ProductMasterList;
                //
                //Product List Bind
                StaffMasterViewModel sm = new StaffMasterViewModel();
                var StaffList = ApiCall.PostApi("StaffMasterRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(sam));
                sm = JsonConvert.DeserializeObject<StaffMasterViewModel>(StaffList);
                sam.StaffList = sm.StaffMasterList;
                //



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

                //Product List Bind
                samv.Action = "Active";
                ProductMasterViewModel pm = new ProductMasterViewModel();
                var ProdList = ApiCall.PostApi("ProductMasterRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(samv));
                pm = JsonConvert.DeserializeObject<ProductMasterViewModel>(ProdList);
                samv.ProductList = pm.ProductMasterList;
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
    }
}