using Newtonsoft.Json;
using ParulBeautyCare.GeneralClasses;
using ParulBeautyCareDbClasses.DataModels;
using ParulBeautyCareViewModel.ViewModel;
using ParulBeautyCareViewModel.ViewModel.BookingMgmtViewModel;
using ParulBeautyCareViewModel.ViewModel.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ParulBeautyCare.Controllers
{
    public class BookingManagementController : GeneralClass
    {
        parulbeautycareEntities db = new parulbeautycareEntities();
        public ActionResult Index()
        {
            BookAppointmentViewModel em = new BookAppointmentViewModel();
            var list = new List<string>() { "09:00", "10:00", "11:30", "01:30", "02:00", "03:00", "04:30" };
            ViewBag.list = list;
            return View(em);
        }
        public ActionResult BookAppointment()
        {
            BookAppointmentViewModel mv = new BookAppointmentViewModel();
            mv.Action = "active";
            mv.CompanyCode = LoggedUserDetails.CompanyCode;
            CategoryMasterViewModel pm = new CategoryMasterViewModel();
            pm.Action = mv.Action;
            pm.CompanyCode = mv.CompanyCode;
            var CategoryList = ApiCall.PostApi("CategoryMasterRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(mv));
            pm = JsonConvert.DeserializeObject<CategoryMasterViewModel>(CategoryList);
            SubCategoryMasterViewModel sm = new SubCategoryMasterViewModel();
            sm.Action = mv.Action;
            sm.CompanyCode = mv.CompanyCode;
            var SubCategoryList = ApiCall.PostApi("SubCategoryMasterRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(mv));
            sm = JsonConvert.DeserializeObject<SubCategoryMasterViewModel>(SubCategoryList);
            mv.CategoryList = pm.CategoryMasterList.Select(c => new BookCategoryViewModel()
            {
                CategoryId = c.CategoryId.ToString(),
                CategoryName = c.CategoryName
            }).ToList();
            mv.SubCategoryList = sm.SubCategoryMasterList.Select(c => new BookSubCategoryViewModel()
            {
                SubcategoryId = c.SubCategoryId.ToString(),
                SubcategoryName = c.SubCategoryName
            }).ToList();
            return View(mv);
        }
        public ActionResult SelectSubCategoryJson(string ddlCategoryDropdown)
        {
            BookAppointmentViewModel mv = new BookAppointmentViewModel();

            SubCategoryMasterViewModel sm = new SubCategoryMasterViewModel();
            sm.Action = "GetList";
            sm.CategoryId = ddlCategoryDropdown;
            sm.CompanyCode = LoggedUserDetails.CompanyCode;
            var emplog = ApiCall.PostApi("SubCategoryMasterRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(sm));
            sm = JsonConvert.DeserializeObject<SubCategoryMasterViewModel>(emplog);
            //var SubCateGory = mv.SubCategoryList.Select(c => new SubCategoryMasterViewModel() { SubCategoryId = c.SubCategoryId.ToString(), SubCategoryName= c.SubCategoryName, Amount = c.Amount.ToString() });
            List<SubCategoryMasterViewModel> SubCateGory = new List<SubCategoryMasterViewModel>();
            if (sm != null && sm.SubCategoryMasterList != null)
            {
                SubCateGory = sm.SubCategoryMasterList.Select(c => new SubCategoryMasterViewModel()
                {
                    SubCategoryId = c.SubCategoryId.ToString(),
                    SubCategoryName = c.SubCategoryName
                }).ToList();
            }
            var obj = new { SubCateGory };
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SelectSubCategoryDetails(string Subcateid)
        {
            SubCategoryMasterViewModel sm = new SubCategoryMasterViewModel();
            sm.Action = "Detail";
            sm.CategoryId = Subcateid;
            sm.CompanyCode = LoggedUserDetails.CompanyCode;
            var emplog = ApiCall.PostApi("SubCategoryMasterRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(sm));
            sm = JsonConvert.DeserializeObject<SubCategoryMasterViewModel>(emplog);
            var SubCateGory = sm.SubCategoryMasterList.Select(c => new SubCategoryMasterViewModel()
            {
                SubCategoryId = c.SubCategoryId.ToString(),
                SubCategoryName = c.SubCategoryName,
                Amount = c.Amount.ToString()
            }).ToList();
            var obj = new { SubCateGory };
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CheckIn()
        {
            CheckInCheckOutViewModel chkinout = new CheckInCheckOutViewModel();
            chkinout.IsBooking = false;

            chkinout.Action = "all";
            
            BookingStaffAllocationViewModel sm = new BookingStaffAllocationViewModel();
            var StaffList = ApiCall.PostApi("BookingHeaderRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(chkinout));
            sm = JsonConvert.DeserializeObject<BookingStaffAllocationViewModel>(StaffList);
            chkinout.BookingList = sm.BookingHeaderList;
            chkinout.BookingList.ForEach(e =>
            {
                e.CustomerName = $"PBC00000{e.BookingId} - {e.CustomerName}";
            });
            return View(chkinout);
        }
        [HttpPost]
        public ActionResult CheckIn(CheckInCheckOutViewModel chkinout)
        {
            try
            {
                if (chkinout.CheckId == null)
                {
                    if (!User.Identity.IsAuthenticated)
                    {
                        FormsAuthentication.RedirectToLoginPage();
                    }
                    string url = generalFunctions.getCommon(Request.Url.AbsoluteUri);
                   
                    chkinout.Action = "all";
                    BookingStaffAllocationViewModel sm = new BookingStaffAllocationViewModel();
                    var StaffList = ApiCall.PostApi("BookingHeaderRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(chkinout));
                    sm = JsonConvert.DeserializeObject<BookingStaffAllocationViewModel>(StaffList);
                    chkinout.BookingList = sm.BookingHeaderList;
                    chkinout.BookingList.ForEach(e =>
                    {
                        e.CustomerName = $"PBC00000{e.BookingId} - {e.CustomerName}";
                    });     

                    chkinout.CompanyCode = LoggedUserDetails.CompanyCode;
                    chkinout.Action = "Insert";
                    chkinout.CreateUser = User.Identity.Name;
                    chkinout.CreateDate = generalFunctions.getTimeZoneDatetimedb();
                   
                    if (chkinout.WaitingTime!=null)
                    {
                        chkinout.WaitingTime = chkinout.WaitingTime+" Minutes";
                    }
                    chkinout.CheckinDateTime = generalFunctions.getTimeZoneDatetimedb();
                    var apiResponse = ApiCall.PostApi("CheckInCheckOutIns", Newtonsoft.Json.JsonConvert.SerializeObject(chkinout));
                    chkinout = JsonConvert.DeserializeObject<CheckInCheckOutViewModel>(apiResponse);
                    string msg = chkinout.result;

                    if (msg.Contains("Successfully"))
                    {
                        var data = new { Message = msg, Type = "success" };
                        TempData["SweetAlert"] = data;
                        return RedirectToAction("CheckIn", "BookingManagement");
                    }
                    else
                    {
                        var data = new { Message = msg, Type = "error" };
                        TempData["SweetAlert"] = data;
                        return RedirectToAction("CheckIn", "BookingManagement");
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

            return View(chkinout);
        }
        public ActionResult CheckOut()
        {
            CheckInCheckOutViewModel chkinout = new CheckInCheckOutViewModel();
            chkinout.IsBooking = false;

            chkinout.Action = "all";

            var CheckInCheckOutList = ApiCall.PostApi("CheckInCheckOutRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(chkinout));
            chkinout = JsonConvert.DeserializeObject<CheckInCheckOutViewModel>(CheckInCheckOutList);
            chkinout.CheckInCheckOutList = chkinout.CheckInCheckOutList.Where(x => x.CheckoutDateTime == null).ToList();



            return View(chkinout);
        }
        [HttpPost]
        public ActionResult CheckOut(int CheckId)
        {
            CheckInCheckOutViewModel chkinout = new CheckInCheckOutViewModel();
            try
            {
                
                if (CheckId.ToString() != null)
                {
                    if (!User.Identity.IsAuthenticated)
                    {
                        FormsAuthentication.RedirectToLoginPage();
                    }
                    string url = generalFunctions.getCommon(Request.Url.AbsoluteUri);

                   
                    chkinout.CheckId = CheckId.ToString();
                    chkinout.Action = "checkout";
                    // chkinout.CheckId = CheckId.ToString();
                    chkinout.CheckoutDateTime = generalFunctions.getTimeZoneDatetimedb();
                    var apiResponse = ApiCall.PostApi("CheckInCheckOutIns", Newtonsoft.Json.JsonConvert.SerializeObject(chkinout));
                    chkinout = JsonConvert.DeserializeObject<CheckInCheckOutViewModel>(apiResponse);
                    string msg = chkinout.result;

                    if (msg.Contains("Successfully"))
                    {
                        var data = new { Message = msg, Type = "success" };
                        TempData["SweetAlert"] = data;
                        return RedirectToAction("CheckOut", "BookingManagement");
                    }
                    else
                    {
                        var data = new { Message = msg, Type = "error" };
                        TempData["SweetAlert"] = data;
                        return RedirectToAction("CheckOut", "BookingManagement");
                    }

                    var CheckInCheckOutList = ApiCall.PostApi("CheckInCheckOutRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(chkinout));
                    chkinout = JsonConvert.DeserializeObject<CheckInCheckOutViewModel>(CheckInCheckOutList);
                    chkinout.CheckInCheckOutList = chkinout.CheckInCheckOutList.Where(x => x.CheckoutDateTime == null).ToList();

                }
            }
            catch (Exception ex)
            {
                //Danger(ex.Message.ToString(), true);
                var data = new { Message = ex.Message.ToString(), Type = "error" };
                TempData["SweetAlert"] = data;
                return RedirectToAction("StockPurchase", "StockManagement");
            }

            return View(chkinout);
        }


    }
}