using Newtonsoft.Json;
using ParulBeautyCare.GeneralClasses;
using ParulBeautyCareDbClasses.DataModels;
using ParulBeautyCareViewModel.ViewModel;
using ParulBeautyCareViewModel.ViewModel.BookingMgmtViewModel;
using ParulBeautyCareViewModel.ViewModel.Master;
using ParulBeautyCareViewModel.ViewModel.StaffMgmtViewModel;
using ParulBeautyCareViewModel.ViewModel.StockMgmtViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ParulBeautyCare.Controllers
{
    public class StaffManagementController : GeneralClass
    {
        parulbeautycareEntities db = new parulbeautycareEntities();
        public ActionResult ViewStaffMgmt()
        {
            return View();
        }
        public ActionResult AddStaffMgmt()
        {
            return View();
        }

        #region==> Staff Dashboard
        public ActionResult StaffDashboard()
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return RedirectToAction("Login", "Home");
                }
                HttpCookie reqCookies = Request.Cookies["LoginMaster"];
                if (reqCookies != null)
                {
                    StaffDashboardViewModel dm = new StaffDashboardViewModel();

                    StaffDashboardServicesViewModel svm = new StaffDashboardServicesViewModel();
                    svm.Action = "staffTodayServices";
                    svm.CompanyCode = LoggedUserDetails.CompanyCode;
                    svm.StaffCode = LoggedUserDetails.UserName;
                    svm.TodayDate = generalFunctions.getDate();
                    var StaffServicesList = ApiCall.PostApi("StaffDashboardTodayServicesRtr", Newtonsoft.Json.JsonConvert.SerializeObject(svm));
                    svm = JsonConvert.DeserializeObject<StaffDashboardServicesViewModel>(StaffServicesList);
                    if(svm.StaffDashboardTodayServicesList.Count > 0)
                    {
                        dm.TodayWorks = svm.StaffDashboardTodayServicesList.FirstOrDefault().ServiceCount.ToString();
                    }
                    else
                    {
                        dm.TodayWorks = "0";
                    }

                    StaffAppointmentsViewModel dvm = new StaffAppointmentsViewModel();
                    dvm.Action = "all";
                    dvm.CompanyCode = LoggedUserDetails.CompanyCode;
                    dvm.StaffCode = LoggedUserDetails.UserName;
                    dvm.TodayDate = generalFunctions.getDate();
                    var AppointmentsList = ApiCall.PostApi("StaffDashboardAppointmentsRtr", Newtonsoft.Json.JsonConvert.SerializeObject(dvm));
                    dvm = JsonConvert.DeserializeObject<StaffAppointmentsViewModel>(AppointmentsList);
                    dm.StaffAppointmentsList = dvm.StaffAppointmentsList;

                    dvm.Action = "todayCompleted";
                    dvm.CompanyCode = LoggedUserDetails.CompanyCode;
                    dvm.StaffCode = LoggedUserDetails.UserName;
                    dvm.TodayDate = generalFunctions.getDate();
                    var CompletedAppointmentsList = ApiCall.PostApi("StaffDashboardAppointmentsRtr", Newtonsoft.Json.JsonConvert.SerializeObject(dvm));
                    dvm = JsonConvert.DeserializeObject<StaffAppointmentsViewModel>(CompletedAppointmentsList);
                    dm.StaffCompletedAppointmentsList = dvm.StaffAppointmentsList;

                    return View(dm);
                }
                else
                {
                    return RedirectToAction("Login", "Home");
                }
            }
            catch (Exception)
            {
                if (Request.Cookies["LoginMaster"] != null)
                {
                    Response.Cookies["LoginMaster"].Expires = DateTime.Now.AddDays(-1);
                }
                FormsAuthentication.SignOut();
                return RedirectToAction("Login", "Home");
            }
        }

        public ActionResult CompletedServicesDetails(string id)
        {
            try
            {
                StaffMasterViewModel mv = new StaffMasterViewModel();
                mv.Action = "All";
                mv.CompanyCode = LoggedUserDetails.CompanyCode;
                var emplog = ApiCall.PostApi("StaffMasterRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(mv));
                mv = JsonConvert.DeserializeObject<StaffMasterViewModel>(emplog);

                var StaffMemberId = mv.StaffMasterList.Where(x => x.StaffCode == LoggedUserDetails.UserName).FirstOrDefault().StaffId.ToString();

                BookingDetailViewModel bvm = new BookingDetailViewModel();
                bvm.BookingId = id;
                bvm.CompanyCode = LoggedUserDetails.CompanyCode;
                bvm.Action = "details";
                var BookingDetailList = ApiCall.PostApi("BookingDetailRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(bvm));
                bvm = JsonConvert.DeserializeObject<BookingDetailViewModel>(BookingDetailList);
                List<PBBookingDetailRtr_Result> bdv = bvm.BookingDetailList.Where(x=>x.AllocatedTo.ToString() ==StaffMemberId).ToList();

                return PartialView("StaffCompletedServicesDetails", bdv);

            }
            catch (Exception ex)
            {
                Danger(ex.Message.ToString(), true);
                return RedirectToAction("Dashboard", "Home");
            }
        }

        public ActionResult AllServicesDetails(string id)
        {
            try
            {
                StaffMasterViewModel mv = new StaffMasterViewModel();
                mv.Action = "All";
                mv.CompanyCode = LoggedUserDetails.CompanyCode;
                var emplog = ApiCall.PostApi("StaffMasterRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(mv));
                mv = JsonConvert.DeserializeObject<StaffMasterViewModel>(emplog);

                var StaffMemberId = mv.StaffMasterList.Where(x => x.StaffCode == LoggedUserDetails.UserName).FirstOrDefault().StaffId.ToString();

                BookingDetailViewModel bvm = new BookingDetailViewModel();
                bvm.BookingId = id;
                bvm.CompanyCode = LoggedUserDetails.CompanyCode;
                bvm.Action = "details";
                var BookingDetailList = ApiCall.PostApi("BookingDetailRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(bvm));
                bvm = JsonConvert.DeserializeObject<BookingDetailViewModel>(BookingDetailList);
                List<PBBookingDetailRtr_Result> bdv = bvm.BookingDetailList.Where(x => x.AllocatedTo.ToString() == StaffMemberId).ToList(); 

                return PartialView("StaffServicesDetails", bdv);

            }
            catch (Exception ex)
            {
                Danger(ex.Message.ToString(), true);
                return RedirectToAction("Dashboard", "Home");
            }
        }
        #endregion

        #region==> Staff Remaining Service
        public ActionResult ViewAllocatedService()
        {
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

            BookingDetailViewModel bvm = new BookingDetailViewModel();
            bvm.serviceDate = generalFunctions.getDate();
            return View(bvm);
        }
        public ActionResult ViewServicesCalendar()
        {
            BookingDetailViewModel bvm = new BookingDetailViewModel();
            bvm.Action = "staffremainingservice";
            bvm.StaffUserCode = LoggedUserDetails.UserName;
            var BookingList = ApiCall.PostApi("BookingDetailRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(bvm));
            bvm = JsonConvert.DeserializeObject<BookingDetailViewModel>(BookingList);
            return Json(bvm.BookingDetailList, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetServiceData(string id)
        {
            BookingDetailViewModel bvm = new BookingDetailViewModel();
            bvm.Action = "details";
            bvm.BookingId = id;
            var BookingList = ApiCall.PostApi("BookingDetailRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(bvm));
            bvm = JsonConvert.DeserializeObject<BookingDetailViewModel>(BookingList);
            return Json(bvm.BookingDetailList.Where(x=>x.Status==1), JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region==> Staff Completed Service
        public ActionResult ViewCompletedService()
        {
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

            BookingDetailViewModel bvm = new BookingDetailViewModel();
            bvm.serviceDate = generalFunctions.getDate();
            return View(bvm);
        }
        public ActionResult ViewCompletedServicesCalendar()
        {
            BookingDetailViewModel bvm = new BookingDetailViewModel();
            bvm.Action = "staffcompletedservice";
            bvm.StaffUserCode = LoggedUserDetails.UserName;
            var BookingList = ApiCall.PostApi("BookingDetailRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(bvm));
            bvm = JsonConvert.DeserializeObject<BookingDetailViewModel>(BookingList);
            return Json(bvm.BookingDetailList, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetCompletedServiceData(string id)
        {
            BookingDetailViewModel bvm = new BookingDetailViewModel();
            bvm.Action = "details";
            bvm.BookingId = id;
            var BookingList = ApiCall.PostApi("BookingDetailRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(bvm));
            bvm = JsonConvert.DeserializeObject<BookingDetailViewModel>(BookingList);
            return Json(bvm.BookingDetailList.Where(x => x.Status == 2), JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region==> Staff Member Stock
        public ActionResult StaffMemberStock()
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

                StockTransferHeaderViewModel sdmvm = new StockTransferHeaderViewModel();
                sdmvm.Action = "reports";
                sdmvm.StaffCode = LoggedUserDetails.UserName;
                sdmvm.CompanyCode = LoggedUserDetails.CompanyCode;
                ViewBag.Subtitle = LoggedUserDetails.UserName + " Products Stock Details";
                var StockAllocatedToStaffList = ApiCall.PostApi("StockAllocatedToStaffRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(sdmvm));
                sdmvm = JsonConvert.DeserializeObject<StockTransferHeaderViewModel>(StockAllocatedToStaffList);
                return View(sdmvm.StockAllocatedToStaffList);

            }
            catch (Exception ex)
            {
                var data = new { Message = ex.Message.ToString(), Type = "error" };
                TempData["SweetAlert"] = data;
                return RedirectToAction("Dashboard", "Home");
            }
        }
        #endregion

        #region==> Staff Incentive Report
        public ActionResult StaffIncentiveReport()
        {
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
         
            if (!User.Identity.IsAuthenticated)
            {
                FormsAuthentication.RedirectToLoginPage();
            }
            IncentiveReportViewModel irvm = new IncentiveReportViewModel();
            irvm.Action = "incentiveall";
            irvm.StaffCode = LoggedUserDetails.UserName;
            var IncentiveReportList = ApiCall.PostApi("IncentiveReport", Newtonsoft.Json.JsonConvert.SerializeObject(irvm));
            irvm = JsonConvert.DeserializeObject<IncentiveReportViewModel>(IncentiveReportList);

            return View(irvm);
        }
        [HttpPost]
        public ActionResult StaffIncentiveReport(IncentiveReportViewModel invm)
        {
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
            if (!User.Identity.IsAuthenticated)
            {
                FormsAuthentication.RedirectToLoginPage();
            }

            IncentiveReportViewModel brm = new IncentiveReportViewModel();
            brm.Action = "incentivereport";
            brm.StaffCode = LoggedUserDetails.UserName;
            brm.FromDate = generalFunctions.dateconvert(invm.FromDate);
            brm.ToDate = generalFunctions.dateconvert(invm.ToDate);
            var IncentiveReportList = ApiCall.PostApi("IncentiveReport", Newtonsoft.Json.JsonConvert.SerializeObject(brm));
            brm = JsonConvert.DeserializeObject<IncentiveReportViewModel>(IncentiveReportList);
            invm.StaffIncentiveReportList = brm.StaffIncentiveReportList;

            return View(invm);
        }
        #endregion
    }
}