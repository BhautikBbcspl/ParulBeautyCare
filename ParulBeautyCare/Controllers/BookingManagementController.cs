using Newtonsoft.Json;
using ParulBeautyCare.GeneralClasses;
using ParulBeautyCareDbClasses.DataModels;
using ParulBeautyCareViewModel.ViewModel;
using ParulBeautyCareViewModel.ViewModel.BookingMgmtViewModel;
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
            return View();
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