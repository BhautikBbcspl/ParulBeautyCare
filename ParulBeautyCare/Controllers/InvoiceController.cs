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
using ParulBeautyCareViewModel.ViewModel.BookingMgmtViewModel;
using ParulBeautyCareViewModel.ViewModel.InvoiceMgmtViewModel;
using ParulBeautyCareViewModel.ViewModel.Master;

namespace ParulBeautyCare.Controllers
{
    public class InvoiceController : GeneralClass
    {
        parulbeautycareEntities db = new parulbeautycareEntities();
        public ActionResult Index()
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

            InvoiceDetailViewModel invm = new InvoiceDetailViewModel();
            invm.CompanyCode = LoggedUserDetails.CompanyCode;
            invm.Action = "active";

            //Department List Bind
            invm.Action = "active";
            DepartmentMasterViewModel dm = new DepartmentMasterViewModel();
            var DeptList = ApiCall.PostApi("DepartmentMasterRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(invm));
            dm = JsonConvert.DeserializeObject<DepartmentMasterViewModel>(DeptList);
            invm.DeptList = dm.DepartmentList;
            //

            // List Bind
            invm.Action = "all";
            BookingHeaderViewModel bh = new BookingHeaderViewModel();
            var donebookinglist = ApiCall.PostApi("BookingHeaderRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(invm));
            bh = JsonConvert.DeserializeObject<BookingHeaderViewModel>(donebookinglist);
            invm.DoneBookingsList = bh.BookingHeaderList.Where(x=>x.Status==0).ToList();
            invm.DoneBookingsList.ForEach(e =>
            {
                e.CustomerName = $"{e.BookingCode} - {e.CustomerName}";
            });
            //

            return View(invm);
        }

        [HttpPost]
        public ActionResult BookingDetailRTR(string BookingHeaderId)
        {
            BookingDetailViewModel bvm = new BookingDetailViewModel();
            bvm.BookingId = BookingHeaderId;
            bvm.CompanyCode = LoggedUserDetails.CompanyCode;
            bvm.Action = "details";
            var BookingDetailList = ApiCall.PostApi("BookingDetailRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(bvm));
            bvm = JsonConvert.DeserializeObject<BookingDetailViewModel>(BookingDetailList);
            List<PBBookingDetailRtr_Result> bdv = bvm.BookingDetailList;

            StaffMasterViewModel mv = new StaffMasterViewModel();
            mv.Action = "Active";
            mv.CompanyCode = LoggedUserDetails.CompanyCode;
            var StaffList = ApiCall.PostApi("StaffMasterRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(mv));
            mv = JsonConvert.DeserializeObject<StaffMasterViewModel>(StaffList);
            List<PBStaffMasterRtr_Result> smv = mv.StaffMasterList;

            BookingHeaderViewModel bhm = new BookingHeaderViewModel();
            bhm.CompanyCode = LoggedUserDetails.CompanyCode;
            bhm.Action = "all";
            var BookingHeaderList = ApiCall.PostApi("BookingHeaderRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(bhm));
            bhm = JsonConvert.DeserializeObject<BookingHeaderViewModel>(BookingHeaderList);
            List<PBBookingHeaderRtr_Result> bhv = bhm.BookingHeaderList.Where(x => x.BookingId.ToString() == BookingHeaderId).ToList();

            TimeSlotMasterViewModel tsv = new TimeSlotMasterViewModel();
            tsv.Action = "All";
            tsv.CompanyCode = LoggedUserDetails.CompanyCode;
            var TimeslotList = ApiCall.PostApi("TimeSlotMasterRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(tsv));
            tsv = JsonConvert.DeserializeObject<TimeSlotMasterViewModel>(TimeslotList);
            List<PBTimeSlotMasterRetrieve_Result> tsmv = tsv.TimeSlotMasterList;

            var obj = new { bdv, smv, bhv, tsmv };
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
    }
}