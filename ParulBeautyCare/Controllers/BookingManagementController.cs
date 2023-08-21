using Newtonsoft.Json;
using ParulBeautyCare.GeneralClasses;
using ParulBeautyCareDbClasses.DataModels;
using ParulBeautyCareViewModel.ViewModel;
using ParulBeautyCareViewModel.ViewModel.BookingMgmtViewModel;
using ParulBeautyCareViewModel.ViewModel.Master;
using System;
using System.Collections.Generic;
using System.Data;
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
            PackageMasterViewModel am = new PackageMasterViewModel();
            am.Action = mv.Action;
            am.CompanyCode = mv.CompanyCode;
            var PackageList = ApiCall.PostApi("PackageMasterRtr", Newtonsoft.Json.JsonConvert.SerializeObject(mv));
            am = JsonConvert.DeserializeObject<PackageMasterViewModel>(PackageList);
            mv.PackageList = am.PackageList;
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

        [HttpPost]
        public ActionResult BookAppointment(BookAppointmentViewModel mv, string BookAppointment)
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(
                     new DataColumn[10] {
                       new DataColumn("CategoryId", typeof(string)),
                         new DataColumn("IntePackageServiceId", typeof(string)),
                          new DataColumn("SubCategoryId", typeof(string)),
                          new DataColumn("AppointmentDate", typeof(string)),
                          new DataColumn("DoneBy", typeof(string)),
                          new DataColumn("DoneDate", typeof(string)),
                          new DataColumn("Amount", typeof(string)),
                          new DataColumn("Discount", typeof(string)),
                          new DataColumn("FinalAmount", typeof(string)),
                          new DataColumn("CustomerName", typeof(string))
                       });
            foreach (var item in mv.BookAppointmentTable)
            {
                string CategoryId = item.CategoryId;
                string IntePackageServiceId = item.IntePackageServiceId;
                string SubCategoryId = item.SubcategoryId;
                string AppointmentDateTime = item.AppointmentDateTime;
                string DoneBy = "Test";
                string DoneDate = "Test";
                string Amount = "Test";
                string Discount = "Test";
                string FinalAmount = "Test";
                string CustomerName = "Test";
                dt.Rows.Add(CategoryId, IntePackageServiceId, SubCategoryId, AppointmentDateTime, DoneBy, DoneDate, Amount, Discount, FinalAmount, CustomerName);

            }
            return View();

        }

        #region==> Booking Allocation
        public ActionResult BookingStaffAllocation()
        {
            try
            {
                MenuRightsViewModel mv1 = new MenuRightsViewModel();
                //mv1.Usercode = LoggedUserDetails.UserName;
                //string url = generalFunctions.getCommon(Request.Url.AbsoluteUri);
                //mv1.PageName = url;
                //var MenuRtr = ApiCall.PostApi("MenuRightsRtr", Newtonsoft.Json.JsonConvert.SerializeObject(mv1));
                //mv1 = JsonConvert.DeserializeObject<MenuRightsViewModel>(MenuRtr);
                //if (mv1.MenuRightsList.Count > 0)
                //{
                //    //ViewBag.ViewRight = mv1.MenuRightsList.FirstOrDefault().ViewRight;
                //    //ViewBag.InsertRight = mv1.MenuRightsList.FirstOrDefault().InsertRight;
                //    //ViewBag.UpdateRight = mv1.MenuRightsList.FirstOrDefault().UpdateRight;
                //    //ViewBag.DeleteRight = mv1.MenuRightsList.FirstOrDefault().DeleteRight;
                //    TempData["ViewRight"] = mv1.MenuRightsList.FirstOrDefault().ViewRight;
                //    TempData["InsertRight"] = mv1.MenuRightsList.FirstOrDefault().InsertRight;
                //    TempData["UpdateRight"] = mv1.MenuRightsList.FirstOrDefault().UpdateRight;
                //    TempData["DeleteRight"] = mv1.MenuRightsList.FirstOrDefault().DeleteRight;
                //}
                //else
                //{
                //    var data = new { Message = "Sorry,You have no rights to access this page", Type = "error" };
                //    TempData["SweetAlert"] = data;
                //    return RedirectToAction("Dashboard", "Home");
                //}

                BookingStaffAllocationViewModel model = new BookingStaffAllocationViewModel();
                model.CompanyCode = LoggedUserDetails.CompanyCode;
                model.Action = "all";

                using (parulbeautycareEntities db = new parulbeautycareEntities())
                {
                    BookingHeaderViewModel bvm = new BookingHeaderViewModel();
                    var BookingList = ApiCall.PostApi("BookingHeaderRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(model));
                    bvm = JsonConvert.DeserializeObject<BookingHeaderViewModel>(BookingList);
                    model.BookingHeaderList = bvm.BookingHeaderList;
                    model.BookingHeaderList.ForEach(e =>
                    {
                        e.CustomerName = $"{e.BookingCode} - {e.CustomerName}";
                    });
                }
                return View(model);
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
        public ActionResult AddBookingStaffAllocation(List<BookingDetailViewModel> formData)
        {
            try
            {
                BookingDetailViewModel bdv = new BookingDetailViewModel();
                foreach (var detail in formData)
                {
                    bdv.BookingDetailId = detail.BookingDetailId;
                    bdv.AppointmentDate = generalFunctions.dateconvert(detail.AppointmentDate);
                    bdv.AppointmentTime = detail.AppointmentTime;
                    bdv.AllocatedTo = detail.AllocatedTo;
                    bdv.UpdateDate = generalFunctions.getTimeZoneDatetimedb();
                    bdv.Action = "allocation";
                    bdv.Status = "1";
                    bdv.UpdateUser = User.Identity.Name;
                    bdv.CompanyCode = LoggedUserDetails.CompanyCode;
                    bdv.AllocationDate = generalFunctions.getTimeZoneDatetimedb();
                    var bookdetail = ApiCall.PostApi("BookingDetailInsUpd", Newtonsoft.Json.JsonConvert.SerializeObject(bdv));
                    bdv = JsonConvert.DeserializeObject<BookingDetailViewModel>(bookdetail);
                }
                string msg = "Booking allocated successfully.";
                if (msg.Contains("successfully"))
                {
                    var data = new { Message = msg, Type = "success" };
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var data = new { Message = msg, Type = "error" };
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                var data = new { Message = ex.Message.ToString(), Type = "error" };
                TempData["SweetAlert"] = data;
                return RedirectToAction("Dashboard", "Home");
            }
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

        [HttpPost]
        public ActionResult CheckBookingAllocation(BookingDetailViewModel formData)
        {
            try
            {
                BookingDetailViewModel bdv = new BookingDetailViewModel();
                if (formData.AppointmentDate!=null)
                {
                    bdv.AppointmentDate = generalFunctions.dateconvert(formData.AppointmentDate);
                }
               
                bdv.AppointmentTime = formData.AppointmentTime;
                bdv.AllocatedTo = formData.AllocatedTo;

                bdv.Action = "checkAllocation";

                bdv.CompanyCode = LoggedUserDetails.CompanyCode;

                var bookdetail = ApiCall.PostApi("BookingDetailInsUpd", Newtonsoft.Json.JsonConvert.SerializeObject(bdv));
                bdv = JsonConvert.DeserializeObject<BookingDetailViewModel>(bookdetail);

                if (bdv.result != null)
                {
                    string msg = bdv.result;
                    if (msg.Contains("allocated"))
                    {
                        var data = new { Message = msg, Type = "info" };
                        return Json(data, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        var data = new { Message = msg, Type = "error" };
                        return Json(data, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    var data = new { Message = "continue", Type = "error" };
                    return Json(data, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                var data = new { Message = ex.Message.ToString(), Type = "error" };
                TempData["SweetAlert"] = data;
                return RedirectToAction("Dashboard", "Home");
            }
        }
        #endregion

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

        public ActionResult SelectPackages(string PackageId)
        {
            BookingPackageViewModel sm = new BookingPackageViewModel();
            sm.Action = "services";
            sm.PackageId = PackageId;
            var BookingPackagelog = ApiCall.PostApi("BookingPackageRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(sm));
            sm = JsonConvert.DeserializeObject<BookingPackageViewModel>(BookingPackagelog);

            List<BookAppointmentDetailViewModel> sittingList = new List<BookAppointmentDetailViewModel>();

            foreach (var serviceResult in sm.PBIntePackageServiceList)
            {
                int noOfSitting = (int)serviceResult.NoOfSitting; // Get the NoOfSitting value
                string CategoryId = serviceResult.CategoryId.ToString();
                string DayInterval = serviceResult.DayInterval;
                string IntePackageServiceId = serviceResult.IntePackageServiceId.ToString();
                string IsActive = serviceResult.IsActive.ToString();
                string PackageId2 = serviceResult.PackageId.ToString();
                string ServiceId = serviceResult.ServiceId.ToString();
                string SubCategoryName = serviceResult.SubCategoryName.ToString();
                string CategoryName = serviceResult.CategoryName.ToString();

                for (int i = 0; i < noOfSitting; i++)
                {
                    BookAppointmentDetailViewModel PackageDetail = new BookAppointmentDetailViewModel
                    {
                        IntePackageServiceId = IntePackageServiceId,
                        CategoryId = CategoryId,
                        CategoryName = CategoryName,
                        DayInterval = DayInterval,
                        PackageId = PackageId2.ToString(),
                        ServiceId = ServiceId,
                        SubCategoryName = SubCategoryName

                    };

                    // Add the current PackageDetail to the sittingList

                    sittingList.Add(PackageDetail);
                }
            }
            var obj = new { sittingList };

            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        #region ==> Check In/Out
        public ActionResult CheckIn()
        {
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


            CheckInCheckOutViewModel chkinout = new CheckInCheckOutViewModel();
            chkinout.IsBooking = false;

            chkinout.Action = "all";

            BookingStaffAllocationViewModel sm = new BookingStaffAllocationViewModel();
            var StaffList = ApiCall.PostApi("BookingHeaderRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(chkinout));
            sm = JsonConvert.DeserializeObject<BookingStaffAllocationViewModel>(StaffList);
            chkinout.BookingList = sm.BookingHeaderList;
            chkinout.BookingList.ForEach(e =>
            {
                e.CustomerName = $"{e.BookingCode} - {e.CustomerName}";
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

                    if (chkinout.WaitingTime != null)
                    {
                        chkinout.WaitingTime = chkinout.WaitingTime + " Minutes";
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
        #endregion

        #region ==> Service Complition RUFF
        //public ActionResult ServiceComplition()
        //{
        //    try
        //    {
        //        //MenuRightsViewModel mv1 = new MenuRightsViewModel();
        //        //mv1.Usercode = LoggedUserDetails.UserName;
        //        //string url = generalFunctions.getCommon(Request.Url.AbsoluteUri);
        //        //mv1.PageName = url;
        //        //var MenuRtr = ApiCall.PostApi("MenuRightsRtr", Newtonsoft.Json.JsonConvert.SerializeObject(mv1));
        //        //mv1 = JsonConvert.DeserializeObject<MenuRightsViewModel>(MenuRtr);
        //        //if (mv1.MenuRightsList.Count > 0)
        //        //{
        //        //    TempData["ViewRight"] = mv1.MenuRightsList.FirstOrDefault().ViewRight;
        //        //    TempData["InsertRight"] = mv1.MenuRightsList.FirstOrDefault().InsertRight;
        //        //    TempData["UpdateRight"] = mv1.MenuRightsList.FirstOrDefault().UpdateRight;
        //        //    TempData["DeleteRight"] = mv1.MenuRightsList.FirstOrDefault().DeleteRight;
        //        //}
        //        //else
        //        //{
        //        //    var data = new { Message = "Sorry,You have no rights to access this page", Type = "error" };
        //        //    TempData["SweetAlert"] = data;
        //        //    return RedirectToAction("Dashboard", "Home");
        //        //}

        //        BookingDetailViewModel bdv = new BookingDetailViewModel();
        //        bdv.CompanyCode = LoggedUserDetails.CompanyCode;
        //        bdv.Action = "all";
        //        BookingHeaderViewModel bhv = new BookingHeaderViewModel();
        //        var BookingHeaderList = ApiCall.PostApi("BookingHeaderRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(bdv));
        //        bhv = JsonConvert.DeserializeObject<BookingHeaderViewModel>(BookingHeaderList);
        //        bdv.BookingHeaderList = bhv.BookingHeaderList;

        //        bdv.BookingHeaderList.ForEach(e =>
        //        {
        //          e.CustomerName = $"{e.BookingCode} - {e.CustomerName}";
        //         });
        //        bdv.Action = "details";
        //        var bookingDetailList = ApiCall.PostApi("BookingDetailRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(bdv));
        //        bdv = JsonConvert.DeserializeObject<BookingDetailViewModel>(bookingDetailList);


        //        return View(bdv);
        //    }
        //    catch (Exception ex)
        //    {
        //        //Danger(ex.Message.ToString(), true);
        //        //return RedirectToAction("Dashboard", "Home");
        //        var data = new { Message = ex.Message.ToString(), Type = "error" };
        //        TempData["SweetAlert"] = data;
        //        return RedirectToAction("Dashboard", "Home");
        //    }
        //}
        //[HttpPost]
        //public ActionResult ServiceComplition(BookingDetailViewModel bdvm, FormCollection formCollection)
        //{
        //    try
        //    {
        //        var selectedIds = formCollection?["selectedIds"]?.Split(',') ?? new string[0];
        //        BookingHeaderViewModel bhv = new BookingHeaderViewModel();
        //        bdvm.Action = "all";
        //        var BookingHeaderList = ApiCall.PostApi("BookingHeaderRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(bdvm));
        //        bhv = JsonConvert.DeserializeObject<BookingHeaderViewModel>(BookingHeaderList);
        //        bdvm.BookingHeaderList = bhv.BookingHeaderList;
        //        bdvm.BookingHeaderList.ForEach(e =>
        //        {
        //            e.CustomerName = $"{e.BookingCode} - {e.CustomerName}";
        //        });
        //        //foreach (var i in selectedIds)
        //        //{
        //        //    var selectedItem = sthvm.AvailableStockList.FirstOrDefault(x => x.StockDetailId == Convert.ToInt32(i));
        //        //    if (selectedItem != null)
        //        //    {
        //        //        var newTransferItem = new StockTransferTypeViewModel
        //        //        {
        //        //            StockDetailId = selectedItem.StockDetailId.ToString(),
        //        //            StockHeaderId = "", // Set the value of StockHeaderId if needed
        //        //            ProductId = selectedItem.ProductId.ToString(),
        //        //            Qty = "1", // Set the value of Qty if needed
        //        //            TotalPerson = selectedItem.NoOfPerson.ToString(),
        //        //            PersonAvailable = selectedItem.availperson.ToString(),
        //        //            AutoSrNo = selectedItem.AutoSrNo,
        //        //            Barcode = selectedItem.Barcode
        //        //        };

        //        //        sthvm.StockTransferTypeTable.Add(newTransferItem);
        //        //    }
        //        //}


        //        if (bdvm.BookingDetailId == null)
        //        {
        //            if (!User.Identity.IsAuthenticated)
        //            {
        //                FormsAuthentication.RedirectToLoginPage();
        //            }
        //            string url = generalFunctions.getCommon(Request.Url.AbsoluteUri);

        //            bdvm.Action = "insert";
        //            //var apiResponse = ApiCall.PostApi("StockTransferIns", Newtonsoft.Json.JsonConvert.SerializeObject(bdvm));
        //            //bdvm = JsonConvert.DeserializeObject<BookingDetailViewModel>(apiResponse);
        //            //string msg = bdvm.result;
        //            string msg = "Service completed successfully";
        //            if (msg.Contains("Service completed successfully"))
        //            {
        //                var data = new { Message = msg, Type = "success" };
        //                TempData["SweetAlert"] = data;
        //            }
        //            else
        //            {
        //                var data = new { Message = msg, Type = "error" };
        //                TempData["SweetAlert"] = data;
        //                // return RedirectToAction("AddStockAllocation", "StockManagement");
        //            }
        //        }

        //        return RedirectToAction("ServiceComplition", "BookingManagement");
        //    }
        //    catch (Exception ex)
        //    {
        //        var data = new { Message = ex.Message.ToString(), Type = "error" };

        //        TempData["SweetAlert"] = data;
        //        return RedirectToAction("ServiceComplition", "BookingManagement");
        //    }
        //}

        //public ActionResult ServiceDetails(int id)
        //{

        //    try
        //    {
        //        if (!User.Identity.IsAuthenticated)
        //        {
        //            FormsAuthentication.RedirectToLoginPage();
        //        }
        //        string url = generalFunctions.getCommon(Request.Url.AbsoluteUri);

        //        BookingDetailViewModel bdvm = new BookingDetailViewModel();
        //        bdvm.CompanyCode = LoggedUserDetails.CompanyCode;

        //        //bind dropdown
        //        BookingHeaderViewModel bhv = new BookingHeaderViewModel();
        //        bdvm.Action = "all";
        //        var BookingHeaderList = ApiCall.PostApi("BookingHeaderRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(bdvm));
        //        bhv = JsonConvert.DeserializeObject<BookingHeaderViewModel>(BookingHeaderList);
        //        bdvm.BookingHeaderList = bhv.BookingHeaderList;
        //        bdvm.BookingHeaderList2 = bhv.BookingHeaderList.Where(x=>x.BookingId==id).ToList();
        //        bdvm.BookingHeaderList.ForEach(e =>
        //        {
        //            e.CustomerName = $"{e.BookingCode} - {e.CustomerName}";
        //        });
        //        //

        //        //Get Customer Details
        //        //bdvm.Action = "details";
        //        //bdvm.BookingId = id.ToString();
        //        //var BookingHeaderCustomerDetail = ApiCall.PostApi("BookingHeaderRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(bdvm));
        //        //bhv = JsonConvert.DeserializeObject<BookingHeaderViewModel>(BookingHeaderCustomerDetail);
        //        //bdvm.BookingHeaderList2 = bhv.BookingHeaderList;
        //        //

        //        //Get Details 
        //        bdvm.Action = "details";
        //        bdvm.BookingId = id.ToString();
        //        var bookingDetailList = ApiCall.PostApi("BookingDetailRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(bdvm));
        //        bdvm = JsonConvert.DeserializeObject<BookingDetailViewModel>(bookingDetailList);
        //        //


        //        return View("ServiceComplition", bdvm);

        //    }
        //    catch (Exception ex)
        //    {
        //        var data = new { Message = ex.Message.ToString(), Type = "error" };
        //        TempData["SweetAlert"] = data;
        //        return RedirectToAction("ServiceComplition", "BookingManagement");
        //    }
        //}
        #endregion

        #region ==> Service Completion

        public ActionResult ServiceCompletion()
        {
            try
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

                BookingDetailViewModel bdv = new BookingDetailViewModel();
                bdv.CompanyCode = LoggedUserDetails.CompanyCode;
                bdv.Action = "all";
                BookingHeaderViewModel bhv = new BookingHeaderViewModel();
                var BookingHeaderList = ApiCall.PostApi("BookingHeaderRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(bdv));
                bhv = JsonConvert.DeserializeObject<BookingHeaderViewModel>(BookingHeaderList);
                bdv.BookingHeaderList = bhv.BookingHeaderList;

                bdv.BookingHeaderList.ForEach(e =>
                {
                    e.CustomerName = $"{e.BookingCode} - {e.CustomerName}";
                });
                bdv.Action = "details";
                var bookingDetailList = ApiCall.PostApi("BookingDetailRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(bdv));
                bdv = JsonConvert.DeserializeObject<BookingDetailViewModel>(bookingDetailList);

                return View(bdv);
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
        public ActionResult ServiceCompletion(List<BookingDetailViewModel> formDataArray)
        {
            // Handle the received data
            BookingDetailViewModel bdv = new BookingDetailViewModel(); 
            foreach (var formDataItem in formDataArray)
            {
                bdv.BookingDetailId = formDataItem.BookingDetailId;
                bdv.DoneBy = formDataItem.AllocatedTo;
                bdv.DoneDate = generalFunctions.getTimeZoneDatetimedb();
                bdv.UpdateDate = generalFunctions.getTimeZoneDatetimedb();
                bdv.UpdateUser = LoggedUserDetails.UserName;
                bdv.CompanyCode = LoggedUserDetails.CompanyCode;
                bdv.Action = "serviceDone";
                bdv.Status = "2";
                bdv.SubCategoryId = formDataItem.SubCategoryId;
                var bookdetail = ApiCall.PostApi("BookingDetailInsUpd", Newtonsoft.Json.JsonConvert.SerializeObject(bdv));
                bdv = JsonConvert.DeserializeObject<BookingDetailViewModel>(bookdetail);
            }


            string msg = bdv.result;
            if (msg.Contains("Completed"))
            {
                var data = new { Message = msg, Type = "success",BId= formDataArray.FirstOrDefault().BookingId };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var data = new { Message = msg, Type = "error" };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            return RedirectToAction("ServiceCompletion", "BookingManagement");
        }


        #endregion

        #region =======> Check In 2

        public ActionResult CheckIn2()
        {
            CheckInCheckOutViewModel chkinout = new CheckInCheckOutViewModel();
            chkinout.IsBooking = false;

            chkinout.Action = "booking_checkin";
            BookingStaffAllocationViewModel sm = new BookingStaffAllocationViewModel();
            var StaffList = ApiCall.PostApi("BookingHeaderRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(chkinout));
            sm = JsonConvert.DeserializeObject<BookingStaffAllocationViewModel>(StaffList);
            chkinout.BookingList = sm.BookingHeaderList;
            //chkinout.BookingList.ForEach(e =>
            //{
            //    e.CustomerName = $"PBC00000{e.BookingId} - {e.CustomerName}";
            //});
            return View(chkinout);
        }
        [HttpPost]
        public ActionResult CheckIn2(CheckInCheckOutViewModel chkinout, FormCollection formCollection)
        {
            try
            {
                var selectedIds = formCollection["selectedIds"].Split(',');
                if (chkinout.CheckId == null)
                {
                    if (!User.Identity.IsAuthenticated)
                    {
                        FormsAuthentication.RedirectToLoginPage();
                    }
                    string url = generalFunctions.getCommon(Request.Url.AbsoluteUri);
                    string msg = "";


                    //Get Booking Details
                    chkinout.CompanyCode = LoggedUserDetails.CompanyCode;
                    chkinout.Action = "Insert";
                    chkinout.CreateUser = User.Identity.Name;
                    chkinout.CreateDate = generalFunctions.getTimeZoneDatetimedb();
                    chkinout.CheckinDateTime = generalFunctions.getTimeZoneDatetimedb();
                    //if (chkinout.WaitingTime != null)
                    //{
                    //    chkinout.WaitingTime = chkinout.WaitingTime + " Minutes";
                    //}
                    foreach (var i in selectedIds)
                    {
                        chkinout.BookingId = i;
                        chkinout.Action = "details";
                        BookingHeaderViewModel sm = new BookingHeaderViewModel();
                        var bookinglist = ApiCall.PostApi("BookingHeaderRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(chkinout));
                        sm = JsonConvert.DeserializeObject<BookingHeaderViewModel>(bookinglist);
                        chkinout.BookingList = sm.BookingHeaderList;

                        chkinout.Action = "insert";
                        chkinout.CustomerName = chkinout.BookingList.FirstOrDefault().CustomerName;
                        chkinout.ContactNo = chkinout.BookingList.FirstOrDefault().ContactNo;
                        chkinout.Address = chkinout.BookingList.FirstOrDefault().Address;

                        var apiResponse = ApiCall.PostApi("CheckInCheckOutIns", Newtonsoft.Json.JsonConvert.SerializeObject(chkinout));
                        chkinout = JsonConvert.DeserializeObject<CheckInCheckOutViewModel>(apiResponse);
                        msg = chkinout.result;

                    }

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
        #endregion\

        #region ==> View Bookings
        public ActionResult ViewBookings()
        {
            return View();
        }
        public ActionResult ViewBookingsCalander()
        {
            BookingHeaderViewModel bvm = new BookingHeaderViewModel();
            bvm.Action = "all";
            var BookingList = ApiCall.PostApi("BookingHeaderRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(bvm));
            bvm = JsonConvert.DeserializeObject<BookingHeaderViewModel>(BookingList);

            return Json(bvm.BookingHeaderList, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetData(string id)
        {
            BookingHeaderViewModel bvm = new BookingHeaderViewModel();
            bvm.Action = "details";
            bvm.BookingId = id;
            var BookingList = ApiCall.PostApi("BookingHeaderRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(bvm));
            bvm = JsonConvert.DeserializeObject<BookingHeaderViewModel>(BookingList);
            return Json(bvm.BookingHeaderList, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}