﻿using Newtonsoft.Json;
using ParulBeautyCare.GeneralClasses;
using ParulBeautyCareDbClasses.DataModels;
using ParulBeautyCareViewModel.ViewModel;
using ParulBeautyCareViewModel.ViewModel.BookingMgmtViewModel;
using ParulBeautyCareViewModel.ViewModel.Master;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
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
            //Rights checking
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Home");
            }
            MenuRightsViewModel mv1 = new MenuRightsViewModel();
            mv1.Usercode = LoggedUserDetails.UserName;
            string url = generalFunctions.getCommon(Request.Url.AbsoluteUri);
            mv1.PageName = url;
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

            BookAppointmentViewModel mv = new BookAppointmentViewModel();
            mv.Action = "active";
            mv.CompanyCode = LoggedUserDetails.CompanyCode;
            mv.DayInterval = null;
            mv.NoOfSitting = null;
            mv.IntePackageServiceId = 0;
            PackageMasterViewModel am = new PackageMasterViewModel();
            am.Action = mv.Action;
            am.CompanyCode = mv.CompanyCode;
            var PackageList = ApiCall.PostApi("PackageMasterRtr", Newtonsoft.Json.JsonConvert.SerializeObject(mv));
            am = JsonConvert.DeserializeObject<PackageMasterViewModel>(PackageList);
            mv.PackageList = am.PackageList.Select(c => new BookPackageViewModel()
            {
                PackageId = c.PackageId.ToString(),
                PackageName = c.PackageName
            }).ToList();
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
                SubCategoryId = c.SubCategoryId.ToString(),
                SubCategoryName = c.SubCategoryName
            }).ToList();
            DepartmentMasterViewModel dm = new DepartmentMasterViewModel();
            dm.Action = mv.Action;
            dm.CompanyCode = mv.CompanyCode;
            var DeptList = ApiCall.PostApi("DepartmentMasterRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(dm));
            dm = JsonConvert.DeserializeObject<DepartmentMasterViewModel>(DeptList);
            mv.DeptList = dm.DepartmentList;

            return View(mv);
        }

        [HttpPost]
        public ActionResult BookAppointment(int id)
        {
            BookAppointmentViewModel mv = new BookAppointmentViewModel();
            mv.Action = "active";
            mv.CompanyCode = LoggedUserDetails.CompanyCode;
            mv.DayInterval = null;
            mv.NoOfSitting = null;
            mv.IntePackageServiceId = 0;
            PackageMasterViewModel am = new PackageMasterViewModel();
            am.Action = mv.Action;
            am.CompanyCode = mv.CompanyCode;
            var PackageList = ApiCall.PostApi("PackageMasterRtr", Newtonsoft.Json.JsonConvert.SerializeObject(mv));
            am = JsonConvert.DeserializeObject<PackageMasterViewModel>(PackageList);
            mv.PackageList = am.PackageList.Select(c => new BookPackageViewModel()
            {
                PackageId = c.PackageId.ToString(),
                PackageName = c.PackageName
            }).ToList();
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
                SubCategoryId = c.SubCategoryId.ToString(),
                SubCategoryName = c.SubCategoryName
            }).ToList();
            DepartmentMasterViewModel dm = new DepartmentMasterViewModel();
            dm.Action = mv.Action;
            dm.CompanyCode = mv.CompanyCode;
            var DeptList = ApiCall.PostApi("DepartmentMasterRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(dm));
            dm = JsonConvert.DeserializeObject<DepartmentMasterViewModel>(DeptList);
            mv.DeptList = dm.DepartmentList;
            if (id != 0)
            {
                AdvanceBookingViewModel samv = new AdvanceBookingViewModel();
                samv.Action = "Details";
                samv.AdvanceBookingId = id;
                samv.CompanyCode = LoggedUserDetails.CompanyCode;
                var AdvanceBookingServicesList = ApiCall.PostApi("AdvanceBookingRtr", Newtonsoft.Json.JsonConvert.SerializeObject(samv));
                samv = JsonConvert.DeserializeObject<AdvanceBookingViewModel>(AdvanceBookingServicesList);
                mv.AdvanceBookingId = samv.AdvanceBookingList.FirstOrDefault().AdvanceBookingId;
                samv.Name = samv.AdvanceBookingList.FirstOrDefault().Name;
                mv.CustomerName = samv.AdvanceBookingList.FirstOrDefault().ServicePersonName;
                samv.ContactNo = samv.AdvanceBookingList.FirstOrDefault().ContactNo;
                mv.ContactNo = samv.AdvanceBookingList.FirstOrDefault().ServicePersonContact;
                samv.Address = samv.AdvanceBookingList.FirstOrDefault().Address;
                mv.Address = samv.AdvanceBookingList.FirstOrDefault().SpecialPersonAddress;
                mv.FunctionDate = samv.AdvanceBookingList.FirstOrDefault().fdate;
            }
            return View(mv);
        }
        [HttpPost]
        public ActionResult AddBookAppointment(BookAppointmentViewModel mv, string BookAppointment)
        {
            List<BookAppointmentDetailViewModel> myDataList = JsonConvert.DeserializeObject<List<BookAppointmentDetailViewModel>>(BookAppointment);
            List<BookAppointmentDetailViewModel> BookServicesTable = new List<BookAppointmentDetailViewModel>();
            List<BookAppointmentDetailViewModel> BookAppointmentTable = new List<BookAppointmentDetailViewModel>();
            TimeSlotMasterViewModel tsv = new TimeSlotMasterViewModel();
            tsv.Action = "All";
            tsv.CompanyCode = LoggedUserDetails.CompanyCode;
            var TimeslotList = ApiCall.PostApi("TimeSlotMasterRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(tsv));
            tsv = JsonConvert.DeserializeObject<TimeSlotMasterViewModel>(TimeslotList);
            mv.TimeSlotMasterList = tsv.TimeSlotMasterList;
            mv.count = 0;
            if (myDataList.Count > 0)
            {
                foreach (var serviceResult in myDataList)
                {
                    int count = (mv.count + 1);
                    mv.count = count;
                    string CategoryId = serviceResult.CategoryId.ToString();
                    string SubCategoryId = serviceResult.SubCategoryId.ToString();
                    int noOfSitting = Convert.ToInt32(serviceResult.NoOfSitting);
                    string DayInterval = serviceResult.DayInterval;
                    string remark = serviceResult.Remark;
                    string IntePackageServiceId = serviceResult.IntePackageServiceId;
                    //string PackageId2 = serviceResult.PackageId.ToString();
                    string SubCategoryName = serviceResult.SubCategoryName.ToString();
                    string CategoryName = serviceResult.CategoryName.ToString();
                    ViewBag.Amount = mv.Amount;
                    BookServicesTable.Add(new BookAppointmentDetailViewModel
                    {
                        IntePackageServiceId = IntePackageServiceId,
                        CategoryId = CategoryId,
                        SubCategoryId = SubCategoryId,
                        Remark = remark,
                        count = count.ToString(),
                    });
                    for (int i = 0; i < noOfSitting; i++)
                    {
                        BookAppointmentTable.Add(new BookAppointmentDetailViewModel
                        {
                            IntePackageServiceId = IntePackageServiceId,
                            CategoryId = CategoryId,
                            CategoryName = CategoryName,
                            SubCategoryId = SubCategoryId,
                            SubCategoryName = SubCategoryName,
                            AppointmentDateTime = "",
                            DayInterval = DayInterval,
                            Remark = remark,
                            NoOfSitting = noOfSitting.ToString(),
                            TimeSlotMasterList = mv.TimeSlotMasterList,
                            count = count.ToString(),
                        });
                    }
                }
                mv.BookAppointmentTable = BookAppointmentTable;
                mv.BookServicesTable = BookServicesTable;
                //List<BookAppointmentDetailViewModel> appointmentData = JsonConvert.DeserializeObject<List<BookAppointmentDetailViewModel>>(BookAppointment);
                //DataTable dt = new DataTable();
                //dt.Columns.AddRange(
                //         new DataColumn[10] {
                //              new DataColumn("CategoryId", typeof(string)),
                //              new DataColumn("IntePackageServiceId", typeof(string)) { AllowDBNull = true },
                //              new DataColumn("SubCategoryId", typeof(string)),
                //              new DataColumn("AppointmentDateTime", typeof(string)),
                //              new DataColumn("DoneBy", typeof(string)){ AllowDBNull = true },
                //              new DataColumn("DoneDate", typeof(string)){ AllowDBNull = true },
                //              new DataColumn("Amount", typeof(string)){ AllowDBNull = true },
                //              new DataColumn("Discount", typeof(string)){ AllowDBNull = true },
                //              new DataColumn("FinalAmount", typeof(string)){ AllowDBNull = true },
                //              new DataColumn("CustomerName", typeof(string)){ AllowDBNull = true }
                //           });
                //foreach (var item in appointmentData)
                //{
                //    string CategoryId = item.CategoryId;
                //    string IntePackageServiceId = item.IntePackageServiceId;
                //    string SubCategoryId = item.SubCategoryId;
                //    string AppointmentDateTimeString = item.AppointmentDateTime;
                //    string DoneBy = item.DoneBy;
                //    string DoneDate =item.DoneDate ;
                //    string Amount = item.Amount;
                //    string Discount = item.Discount;
                //    string FinalAmount = item.FinalAmount;
                //    string CustomerName = item.CustomerName;
                //    DateTime AppointmentDateTime = DateTime.ParseExact(AppointmentDateTimeString, "dd-MM-yyyy HH:mm", CultureInfo.InvariantCulture);
                //    string AppointmentDateFormatted = AppointmentDateTime.ToString("yyyy-MM-dd HH:mm");
                //    dt.Rows.Add(CategoryId, IntePackageServiceId, SubCategoryId, AppointmentDateFormatted, DoneBy, DoneDate, Amount, Discount, FinalAmount, CustomerName);

                //}
                ////dt.Columns.AddRange(new DataColumn[]
                ////{
                ////    new DataColumn("CategoryId", typeof(string)),
                ////    new DataColumn("SubCategoryId", typeof(string)),
                ////    new DataColumn("AppointmentDateTime", typeof(string)) // Assuming you want to store it as DateTime
                ////});

                ////foreach (var item in appointmentData)
                ////{
                ////    string CategoryId = item.CategoryId;
                ////    string SubCategoryId = item.SubCategoryId;
                ////    string AppointmentDateTimeString = item.AppointmentDateTime;
                ////    DateTime AppointmentDateTime = DateTime.ParseExact(AppointmentDateTimeString, "dd-MM-yyyy HH:mm", CultureInfo.InvariantCulture);
                ////    string AppointmentDateFormatted = AppointmentDateTime.ToString("yyyy-MM-dd HH:mm");
                ////    dt.Rows.Add(CategoryId, SubCategoryId, AppointmentDateFormatted);
                ////}

                //ViewBag.AppointmentDataTable = dt;
                //mv.CreateDate = generalFunctions.getTimeZoneDatetimedb();
                //mv.CreateUser = LoggedUserDetails.UserName;
                //mv.CompanyCode = LoggedUserDetails.CompanyCode;
                //mv.mytable = dt;
                //mv.Action = "insert";
                //mv.FunctionDate = generalFunctions.dateconvert(mv.FunctionDate);
                //var book = ApiCall.PostApi("BookingAppointmentInsert", Newtonsoft.Json.JsonConvert.SerializeObject(mv));
                //mv = JsonConvert.DeserializeObject<BookAppointmentViewModel>(book);

                //TempData code
            }
            return View("AddBookAppointment", mv);
        }
        [HttpPost]
        public ActionResult SaveBookAppointment(BookAppointmentViewModel mv)
        {
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            string jsonData = Request.Form["bookServicesTableJson"];
            List<BookAppointmentDetailViewModel> bookServicesList = JsonConvert.DeserializeObject<List<BookAppointmentDetailViewModel>>(jsonData);
            mv.BookServicesTable = bookServicesList;
            dt.Columns.AddRange(
                     new DataColumn[12] {
                       new DataColumn("CategoryId", typeof(string)),
                         new DataColumn("IntePackageServiceId", typeof(string)) { AllowDBNull = true },
                          new DataColumn("SubCategoryId", typeof(string)),
                          new DataColumn("AppointmentDate", typeof(string)),
                          new DataColumn("DoneBy", typeof(string)) { AllowDBNull = true },
                          new DataColumn("DoneDate", typeof(string)) { AllowDBNull = true },
                          new DataColumn("Amount", typeof(string)){ AllowDBNull = true },
                          new DataColumn("Discount", typeof(string)) { AllowDBNull = true },
                          new DataColumn("FinalAmount", typeof(string)){ AllowDBNull = true },
                          new DataColumn("Remark", typeof(string)) { AllowDBNull = true },
                          new DataColumn("TimeSlotId", typeof(string)) { AllowDBNull = true },
                          new DataColumn("count", typeof(string)) { AllowDBNull = true }
                       });
            if (mv.BookAppointmentTable != null)
            {
                foreach (BookAppointmentDetailViewModel item in mv.BookAppointmentTable)
                {
                    string CategoryId = item.CategoryId.Trim();
                    string IntePSId = item.IntePackageServiceId;
                    string SubCategoryId = item.SubCategoryId.Trim();
                    string AppointmentDate = generalFunctions.dateconvert(item.AppointmentDateTime.Trim());
                    string Amount = item.Amount;
                    string Discount = item.Discount;
                    string FinalAmount = item.FinalAmount;
                    string Remark = item.Remark;
                    string DoneBy = item.DoneBy;
                    string DoneDate = item.DoneDate;
                    string TimeSlotId = item.TimeSlotId;
                    string count = item.count;
                    dt.Rows.Add(CategoryId, IntePSId, SubCategoryId, AppointmentDate, DoneBy, DoneDate, Amount, Discount, FinalAmount, Remark, TimeSlotId, count);
                }
            }
            dt1.Columns.AddRange(
                   new DataColumn[6] {
                       new DataColumn("CategoryId", typeof(string)),
                         new DataColumn("IntePackageServiceId", typeof(string)) { AllowDBNull = true },
                          new DataColumn("SubCategoryId", typeof(string)),
                          new DataColumn("Amount", typeof(string)) { AllowDBNull = true },
                          new DataColumn("Remark", typeof(string)) { AllowDBNull = true },
                          new DataColumn("count", typeof(string)) { AllowDBNull = true }
                     });
            if (mv.BookServicesTable != null)
            {
                foreach (BookAppointmentDetailViewModel item in mv.BookServicesTable)
                {
                    string CategoryId = item.CategoryId.Trim();
                    string IntePSId = item.IntePackageServiceId;
                    string SubCategoryId = item.SubCategoryId.Trim();
                    string Amount = item.Amount;
                    string Remark = item.Remark;
                    string count = item.count;
                    dt1.Rows.Add(CategoryId, IntePSId, SubCategoryId, Amount, Remark, count);
                }
            }
            mv.mytable = dt;
            mv.mytable1 = dt1;
            mv.CompanyCode = LoggedUserDetails.CompanyCode;
            mv.CreateDate = generalFunctions.getTimeZoneDatetimedb();
            mv.CreateUser = LoggedUserDetails.UserName;
            mv.FunctionDate = generalFunctions.dateconvert(mv.FunctionDate);
            mv.Action = "Insert";
            if (dt.Rows.Count > 0 && dt1.Rows.Count > 0)
            {
                var AttLog = ApiCall.PostApi("BookingAppointmentInsUpd", Newtonsoft.Json.JsonConvert.SerializeObject(mv));
                mv = JsonConvert.DeserializeObject<BookAppointmentViewModel>(AttLog);
                string msg = mv.Result;
                bool containsOnlyNumbers = Regex.IsMatch(msg, "^[0-9]+$");
                if (containsOnlyNumbers)
                {
                    var data = new { Message = "Appointment Booked Successfully", Type = "success" };
                    TempData["SweetAlert"] = data;
                    return RedirectToAction("OrderBook", "BookingManagement", new { msg = msg });
                }
                else
                {
                    return View("AddBookAppointment", mv);
                }
            }
            else
            {
                var data = new { Message = "Please try Again", Type = "error" };
                TempData["SweetAlert"] = data;
                return View("AddBookAppointment", mv);
            }
        }

        public ActionResult OrderBook(string msg)
        {
            BookingHeaderViewModel bvm = new BookingHeaderViewModel();
            bvm.CompanyCode = LoggedUserDetails.CompanyCode;
            bvm.Action = "details";
            bvm.BookingId = msg;
            bvm.CreateDate = generalFunctions.dateconvert(generalFunctions.getDate());
            var BookingList = ApiCall.PostApi("BookingHeaderRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(bvm));
            bvm = JsonConvert.DeserializeObject<BookingHeaderViewModel>(BookingList);
            return View(bvm);
        }
        public ActionResult SelectPackages(string PackageId)
        {
            BookingPackageViewModel sm = new BookingPackageViewModel();
            sm.Action = "services";
            sm.PackageId = PackageId;
            ViewBag.Amount = "0";
            var BookingPackagelog = ApiCall.PostApi("BookingPackageRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(sm));
            sm = JsonConvert.DeserializeObject<BookingPackageViewModel>(BookingPackagelog);

            List<BookAppointmentDetailViewModel> sittingList = new List<BookAppointmentDetailViewModel>();
            if (sm.PBIntePackageServiceList.Count > 0)
            {
                foreach (var serviceResult in sm.PBIntePackageServiceList)
                {
                    BookAppointmentDetailViewModel PackageDetail = new BookAppointmentDetailViewModel
                    {
                        NoOfSitting = serviceResult.NoOfSitting.ToString(),
                        IntePackageServiceId = serviceResult.IntePackageServiceId.ToString(),
                        CategoryId = serviceResult.CategoryId.ToString(),
                        CategoryName = serviceResult.CategoryName.ToString(),
                        DayInterval = serviceResult.DayInterval,
                        PackageId = serviceResult.PackageId.ToString(),
                        SubCategoryId = serviceResult.ServiceId.ToString(),
                        SubCategoryName = serviceResult.SubCategoryName.ToString(),
                        Amount = serviceResult.PackageAmount.ToString(),
                        Remark = ""
                    };
                    ViewBag.Amount = PackageDetail.Amount;
                    //for (int i = 0; i < noOfSitting; i++)
                    //{
                    //    BookAppointmentDetailViewModel PackageDetail = new BookAppointmentDetailViewModel
                    //    {
                    //        IntePackageServiceId = IntePackageServiceId,
                    //        CategoryId = CategoryId,
                    //        CategoryName = CategoryName,
                    //        DayInterval = DayInterval,
                    //        PackageId = PackageId2.ToString(),
                    //        SubCategoryId = ServiceId,
                    //        SubCategoryName = SubCategoryName
                    //    };
                    // Add the current PackageDetail to the sittingList
                    sittingList.Add(PackageDetail);
                    // }
                }
            }
            var obj = new { sittingList, amount = ViewBag.Amount };
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AppointmentChecking(string selectedDate)
        {
            BookAppointmentViewModel bvm = new BookAppointmentViewModel();
            bvm.FunctionDate = generalFunctions.dateconvert(selectedDate);
            return PartialView("AppointmentChecking", bvm);
        }

        //public ActionResult SelectPackageJson(string ddlCategoryDropdown)
        //{
        //    BookAppointmentViewModel mv = new BookAppointmentViewModel();

        //    SubCategoryMasterViewModel sm = new SubCategoryMasterViewModel();
        //    sm.Action = "GetList";
        //    sm.CategoryId = ddlCategoryDropdown;
        //    sm.CompanyCode = LoggedUserDetails.CompanyCode;
        //    var emplog = ApiCall.PostApi("SubCategoryMasterRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(sm));
        //    sm = JsonConvert.DeserializeObject<SubCategoryMasterViewModel>(emplog);
        //    //var SubCateGory = mv.SubCategoryList.Select(c => new SubCategoryMasterViewModel() { SubCategoryId = c.SubCategoryId.ToString(), SubCategoryName= c.SubCategoryName, Amount = c.Amount.ToString() });
        //    List<SubCategoryMasterViewModel> SubCateGory = new List<SubCategoryMasterViewModel>();
        //    if (sm != null && sm.SubCategoryMasterList != null)
        //    {
        //        SubCateGory = sm.SubCategoryMasterList.Select(c => new SubCategoryMasterViewModel()
        //        {
        //            SubCategoryId = c.SubCategoryId.ToString(),
        //            SubCategoryName = c.SubCategoryName
        //        }).ToList();
        //    }
        //    var obj = new { SubCateGory };
        //    return Json(obj, JsonRequestBehavior.AllowGet);
        //}
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
                Amount = c.Amount.ToString(),
                NoOfSitting = c.NoOfSitting.ToString(),
                DayInterval = c.DayInterval
            }).ToList();
            var obj = new { SubCateGory };
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        #region==> Booking Allocation
        public ActionResult BookingStaffAllocation()
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

                BookingStaffAllocationViewModel model = new BookingStaffAllocationViewModel();
                model.CompanyCode = LoggedUserDetails.CompanyCode;
                model.Action = "all";

                using (parulbeautycareEntities db = new parulbeautycareEntities())
                {
                    BookingHeaderViewModel bvm = new BookingHeaderViewModel();
                    var BookingList = ApiCall.PostApi("BookingHeaderRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(model));
                    bvm = JsonConvert.DeserializeObject<BookingHeaderViewModel>(BookingList);
                    model.BookingHeaderList = bvm.BookingHeaderList.Where(x => x.Status.ToString() != "4").ToList();

                    if (model.BookingHeaderList != null)
                    {
                        model.BookingHeaderList.ForEach(e =>
                        {
                            e.CustomerName = $"{e.BookingCode} - {e.CustomerName}";
                        });
                    }
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
                if (formData.AppointmentDate != null)
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
                //BookingDetailViewModel bdv = new BookingDetailViewModel();
                bdv.CompanyCode = LoggedUserDetails.CompanyCode;
                bdv.Action = "Pending";
                BookingHeaderViewModel bhv = new BookingHeaderViewModel();
                var BookingHeaderList = ApiCall.PostApi("BookingHeaderRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(bdv));
                bhv = JsonConvert.DeserializeObject<BookingHeaderViewModel>(BookingHeaderList);
                bdv.BookingHeaderList = bhv.BookingHeaderList;

                if (bdv.BookingHeaderList != null)
                {
                    bdv.BookingHeaderList.ForEach(e =>
                    {
                        e.CustomerName = $"{e.BookingCode} - {e.CustomerName}";
                    });
                }
                bdv.Action = "details";
                var bookingDetailList = ApiCall.PostApi("BookingDetailRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(bdv));
                bdv = JsonConvert.DeserializeObject<BookingDetailViewModel>(bookingDetailList);

                return View(bdv);
            }
            catch (Exception ex)
            {
                Danger(ex.Message.ToString(), true);
                var data = new { Message = ex.Message.ToString(), Type = "error" };
                TempData["SweetAlert"] = data;
                return RedirectToAction("Dashboard", "Home");
            }
        }
        [HttpPost]
        public ActionResult ServiceCompletion(ServiceDataModel data)
        {
            string msg2 = "";
            // Handle the received data
            BookingDetailViewModel bdv = new BookingDetailViewModel();
            foreach (var formDataItem in data.FormDataArray)
            {
                bdv.BookingDetailId = formDataItem.BookingDetailId;
                bdv.DoneBy = formDataItem.AllocatedTo;
                bdv.DoneDate = generalFunctions.getTimeZoneDatetimedb();
                bdv.UpdateDate = generalFunctions.getTimeZoneDatetimedb();
                //bdv.UpdateUser = LoggedUserDetails.UserName;
                bdv.CompanyCode = LoggedUserDetails.CompanyCode;
                bdv.Action = "serviceDone";
                bdv.Status = "2";
                bdv.SubCategoryId = formDataItem.SubCategoryId;

                var bookdetail = ApiCall.PostApi("BookingDetailInsUpd", Newtonsoft.Json.JsonConvert.SerializeObject(bdv));
                bdv = JsonConvert.DeserializeObject<BookingDetailViewModel>(bookdetail);
            }

            PaymentHistoryViewModel phv = new PaymentHistoryViewModel();
            foreach (var paymentItem in data.PaymentHistoryArray)
            {
                phv.IsPaymentReceived = paymentItem.IsPaymentReceived;
                phv.PaymentType = paymentItem.PaymentType;
                phv.GPayNo = paymentItem.GPayNo;
                phv.ChequeNo = paymentItem.ChequeNo;
                phv.BookingId = paymentItem.BookingId;
                phv.BookingCode = paymentItem.BookingCode;
                phv.ReceivedAmount = paymentItem.ReceivedAmount;
                phv.PaymentRecievedDate = generalFunctions.getTimeZoneDatetimedb();
                phv.CreateDate = generalFunctions.getTimeZoneDatetimedb();
                phv.CreateUser = LoggedUserDetails.UserName;
                phv.CompanyCode = LoggedUserDetails.CompanyCode;
                phv.Action = "insert";

                if (paymentItem.IsPaymentReceived == "0" || paymentItem.IsPaymentReceived == null)
                {
                    msg2 = "Amount not inserted.";
                }
                else
                {
                    var paymentdetail = ApiCall.PostApi("PaymentHistoryInsUpd", Newtonsoft.Json.JsonConvert.SerializeObject(phv));
                    phv = JsonConvert.DeserializeObject<PaymentHistoryViewModel>(paymentdetail);
                    msg2 = phv.result;
                }
            }
            string msg = bdv.result;

            if (msg.Contains("Completed") && msg2.Contains("successfully"))
            {
                var data1 = new { Message = "Service completed and amount inserted!!", Type = "success", BId = data.FormDataArray.FirstOrDefault().BookingId };
                return Json(data1, JsonRequestBehavior.AllowGet);
            }
            else if (msg.Contains("Completed") && msg2.Contains("exceeds"))
            {
                var data1 = new { Message = "Service completed but amount exceeds the total amount of booking!!", Type = "success", BId = data.FormDataArray.FirstOrDefault().BookingId };
                return Json(data1, JsonRequestBehavior.AllowGet);
            }
            else if (msg.Contains("Completed"))
            {
                var data1 = new { Message = msg, Type = "success" };
                return Json(data1, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var data1 = new { Message = msg, Type = "error" };
                return Json(data1, JsonRequestBehavior.AllowGet);
            }
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
            BookingHeaderViewModel bvm = new BookingHeaderViewModel();
            bvm.BookDate = generalFunctions.getDate();
            return View(bvm);
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
            BookingDetailViewModel bvm = new BookingDetailViewModel();
            bvm.Action = "details";
            bvm.BookingId = id;
            var BookingList = ApiCall.PostApi("BookingDetailRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(bvm));
            bvm = JsonConvert.DeserializeObject<BookingDetailViewModel>(BookingList);
            return Json(bvm.BookingDetailList, JsonRequestBehavior.AllowGet);
        }
        #endregion

        [HttpGet]
        public ActionResult ViewAdvanceBooking()
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

            AdvanceBookingViewModel mv = new AdvanceBookingViewModel();
            mv.Action = "Pending";
            if (mv.FromDate == null)
            {
                mv.NewFromDate = generalFunctions.getDate();
                mv.FromDate = generalFunctions.dateconvert(generalFunctions.getDate());
            }
            else
            {
                mv.NewFromDate = generalFunctions.dateconvert(mv.FromDate);
            }
            if (mv.ToDate == null)
            {
                mv.NewToDate = generalFunctions.getDate();
                mv.ToDate = generalFunctions.dateconvert(generalFunctions.getDate());
            }
            else
            {
                mv.NewToDate = generalFunctions.dateconvert(mv.ToDate);
            }

            var BookingList = ApiCall.PostApi("AdvanceBookingRtr", Newtonsoft.Json.JsonConvert.SerializeObject(mv));
            mv = JsonConvert.DeserializeObject<AdvanceBookingViewModel>(BookingList);
            return View(mv);
        }
        [HttpPost]
        public ActionResult ViewAdvanceBooking(AdvanceBookingViewModel mv)
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
            mv.Action = "Pending";
            if (mv.FromDate == null)
            {
                mv.NewFromDate = generalFunctions.getDate();
                mv.FromDate = generalFunctions.getDate();
            }
            else
            {
                mv.NewFromDate = generalFunctions.dateconvert(mv.FromDate);
            }
            if (mv.ToDate == null)
            {
                mv.NewToDate = generalFunctions.getDate();
                mv.ToDate = generalFunctions.getDate();
            }
            else
            {
                mv.NewToDate = generalFunctions.dateconvert(mv.ToDate);
            }

            var BookingList = ApiCall.PostApi("AdvanceBookingRtr", Newtonsoft.Json.JsonConvert.SerializeObject(mv));
            mv = JsonConvert.DeserializeObject<AdvanceBookingViewModel>(BookingList);
            return View(mv);
        }

        #region ==> Advance Booking 
        public ActionResult AddAdvanceBooking()
        {
            AdvanceBookingViewModel mv = new AdvanceBookingViewModel();
            mv.SidersDeposit = "1000";
            mv.BridalDeposit = "5000";
            ViewBag.action = "Save";
            mv.CompanyCode = LoggedUserDetails.CompanyCode;
            //DepartmentMasterViewModel dm = new DepartmentMasterViewModel();
            //dm.Action = mv.Action;
            //dm.CompanyCode = mv.CompanyCode;
            //var DeptList = ApiCall.PostApi("DepartmentMasterRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(dm));
            //dm = JsonConvert.DeserializeObject<DepartmentMasterViewModel>(DeptList);
            //mv.DepartmentList = dm.DepartmentList;
            return View(mv);
        }
        [HttpPost]
        public ActionResult AddAdvanceBooking(AdvanceBookingViewModel mv)
        {
            try
            {
                if (mv.AdvanceBookingId == 0)
                {
                    mv.CompanyCode = LoggedUserDetails.CompanyCode;
                    mv.CreateDate = generalFunctions.getTimeZoneDatetimedb();
                    mv.CreateUser = User.Identity.Name;
                    mv.FunctionDate = generalFunctions.dateconvert(mv.FunctionDate);
                    mv.Action = "Insert";
                    var emplog = ApiCall.PostApi("AdvanceBookingInsUpd", Newtonsoft.Json.JsonConvert.SerializeObject(mv));
                    mv = JsonConvert.DeserializeObject<AdvanceBookingViewModel>(emplog);
                    string msg = mv.Result;
                    if (msg.Contains("successfully"))
                    {
                        var data = new { Message = msg, Type = "success" };
                        TempData["SweetAlert"] = data;
                        return RedirectToAction("AddAdvanceBooking", "BookingManagement");
                    }
                    else
                    {
                        var data = new { Message = msg, Type = "error" };
                        TempData["SweetAlert"] = data;
                        return RedirectToAction("AddAdvanceBooking", "BookingManagement");
                    }
                }
                else
                {
                    mv.CompanyCode = LoggedUserDetails.CompanyCode;
                    mv.UpdateDate = generalFunctions.getTimeZoneDatetimedb();
                    mv.UpdateUser = User.Identity.Name;
                    mv.FunctionDate = generalFunctions.dateconvert(mv.FunctionDate);
                    mv.Action = "Update";
                    var emplog = ApiCall.PostApi("AdvanceBookingInsUpd", Newtonsoft.Json.JsonConvert.SerializeObject(mv));
                    mv = JsonConvert.DeserializeObject<AdvanceBookingViewModel>(emplog);
                    string msg = mv.Result;
                    if (msg.Contains("successfully"))
                    {
                        var data = new { Message = msg, Type = "success" };
                        TempData["SweetAlert"] = data;
                        return RedirectToAction("ViewAdvanceBooking", "BookingManagement");
                    }
                    else
                    {
                        var data = new { Message = msg, Type = "error" };
                        TempData["SweetAlert"] = data;
                        return RedirectToAction("AddAdvanceBooking", "BookingManagement");
                    }
                }
            }
            catch (Exception ex)
            {
                var data = new { Message = ex.Message.ToString(), Type = "error" };
                TempData["SweetAlert"] = data;
                return RedirectToAction("AddAdvanceBooking", "BookingManagement");
            }
        }

        public ActionResult EditAdvanceBooking(int id)
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    FormsAuthentication.RedirectToLoginPage();
                }
                AdvanceBookingViewModel samv = new AdvanceBookingViewModel();
                samv.Action = "Details";
                samv.AdvanceBookingId = id;
                samv.CompanyCode = LoggedUserDetails.CompanyCode;
                ViewBag.action = "Update";
                var AdvanceBookingServicesList = ApiCall.PostApi("AdvanceBookingRtr", Newtonsoft.Json.JsonConvert.SerializeObject(samv));
                samv = JsonConvert.DeserializeObject<AdvanceBookingViewModel>(AdvanceBookingServicesList);

                //samv.Action = "active";
                //DepartmentMasterViewModel dm = new DepartmentMasterViewModel();
                //dm.Action = samv.Action;
                //dm.CompanyCode = samv.CompanyCode;
                //var DeptList = ApiCall.PostApi("DepartmentMasterRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(dm));
                //dm = JsonConvert.DeserializeObject<DepartmentMasterViewModel>(DeptList);
                //samv.DepartmentList = dm.DepartmentList; 
                samv.AdvanceBookingId = samv.AdvanceBookingList.FirstOrDefault().AdvanceBookingId;
                samv.Name = samv.AdvanceBookingList.FirstOrDefault().Name;
                samv.SPersonName = samv.AdvanceBookingList.FirstOrDefault().ServicePersonName;
                samv.ContactNo = samv.AdvanceBookingList.FirstOrDefault().ContactNo;
                samv.SPersonContact = samv.AdvanceBookingList.FirstOrDefault().ServicePersonContact;
                samv.Address = samv.AdvanceBookingList.FirstOrDefault().Address;
                samv.SPersonAddress = samv.AdvanceBookingList.FirstOrDefault().SpecialPersonAddress;
                samv.SidersDeposit = samv.AdvanceBookingList.FirstOrDefault().SidersDeposit.ToString();
                samv.BridalDeposit = samv.AdvanceBookingList.FirstOrDefault().BridalDeposit.ToString();
                samv.TotalDeposit = samv.AdvanceBookingList.FirstOrDefault().totaldeposit.ToString();
                samv.NumOfSiders = samv.AdvanceBookingList.FirstOrDefault().NumOfSiders.ToString();
                samv.BeforeRemark = samv.AdvanceBookingList.FirstOrDefault().BeforeRemark;
                samv.FunctionDate = samv.AdvanceBookingList.FirstOrDefault().fdate;
                samv.Action = "update";

                return View("AddAdvanceBooking", samv);
            }
            catch (Exception ex)
            {
                //Danger(ex.Message.ToString(), true);
                //return RedirectToAction("Dashboard", "Home");
                var data = new { Message = ex.Message.ToString(), Type = "error" };
                TempData["SweetAlert"] = data;
                return RedirectToAction("ViewAdvanceBooking", "BookingManagement");
            }
        }
        public ActionResult AdvanceServiceBooking(int AdvanceBookingId)
        {
            if (AdvanceBookingId != null || AdvanceBookingId != 0)
            {
                AdvanceBookingViewModel samv = new AdvanceBookingViewModel();
                BookAppointmentViewModel mv = new BookAppointmentViewModel();
                samv.Action = "Details";
                samv.AdvanceBookingId = AdvanceBookingId;
                samv.CompanyCode = LoggedUserDetails.CompanyCode;
                ViewBag.action = "Update";
                var AdvanceBookingServicesList = ApiCall.PostApi("AdvanceBookingRtr", Newtonsoft.Json.JsonConvert.SerializeObject(samv));
                samv = JsonConvert.DeserializeObject<AdvanceBookingViewModel>(AdvanceBookingServicesList);
                mv.AdvanceBookingId = samv.AdvanceBookingList.FirstOrDefault().AdvanceBookingId;
                samv.Name = samv.AdvanceBookingList.FirstOrDefault().Name;
                mv.CustomerName = samv.AdvanceBookingList.FirstOrDefault().ServicePersonName;
                samv.ContactNo = samv.AdvanceBookingList.FirstOrDefault().ContactNo;
                mv.ContactNo = samv.AdvanceBookingList.FirstOrDefault().ServicePersonContact;
                samv.Address = samv.AdvanceBookingList.FirstOrDefault().Address;
                mv.Address = samv.AdvanceBookingList.FirstOrDefault().SpecialPersonAddress;
                //samv.SidersDeposit = samv.AdvanceBookingList.FirstOrDefault().SidersDeposit.ToString();
                //samv.BridalDeposit = samv.AdvanceBookingList.FirstOrDefault().BridalDeposit.ToString();
                //samv.TotalDeposit = samv.AdvanceBookingList.FirstOrDefault().totaldeposit.ToString();
                //samv.NumOfSiders = samv.AdvanceBookingList.FirstOrDefault().NumOfSiders.ToString();
                //samv.BeforeRemark = samv.AdvanceBookingList.FirstOrDefault().BeforeRemark;
                mv.FunctionDate = samv.AdvanceBookingList.FirstOrDefault().fdate;
                return View("BookAppointment", mv);
            }
            return View("ViewAdvanceBooking");
        }
        #endregion


        #region==> Booking Edit
        [HttpPost]
        public ActionResult EditBookAppointment(int? id1)
        {
            BookAppointmentViewModel mv = new BookAppointmentViewModel();
            mv.Action = "active";
            mv.CompanyCode = LoggedUserDetails.CompanyCode;
            mv.DayInterval = null;
            mv.NoOfSitting = null;
            mv.IntePackageServiceId = 0;

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
                SubCategoryId = c.SubCategoryId.ToString(),
                SubCategoryName = c.SubCategoryName
            }).ToList();
            DepartmentMasterViewModel dm = new DepartmentMasterViewModel();
            dm.Action = mv.Action;
            dm.CompanyCode = mv.CompanyCode;
            var DeptList = ApiCall.PostApi("DepartmentMasterRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(dm));
            dm = JsonConvert.DeserializeObject<DepartmentMasterViewModel>(DeptList);
            mv.DeptList = dm.DepartmentList;
            if (id1 != 0 && id1 != null)
            {
                BookingHeaderViewModel mv1 = new BookingHeaderViewModel();
                mv1.Action = "Details";
                mv1.BookingId = id1.ToString();
                mv1.CompanyCode = LoggedUserDetails.CompanyCode;
                var BookingHeaderList = ApiCall.PostApi("BookingHeaderRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(mv1));
                mv1 = JsonConvert.DeserializeObject<BookingHeaderViewModel>(BookingHeaderList);
                mv.BookingId = mv1.BookingHeaderList.FirstOrDefault().BookingId;
                mv.CustomerName = mv1.BookingHeaderList.FirstOrDefault().CustomerName;
                mv.ContactNo = mv1.BookingHeaderList.FirstOrDefault().ContactNo;
                mv.Address = mv1.BookingHeaderList.FirstOrDefault().Address;
                mv.FunctionDate = mv1.BookingHeaderList.FirstOrDefault().BDate;
                mv.DepartmentId = mv1.BookingHeaderList.FirstOrDefault().DepartmentId.ToString();
                mv.BookingAmount = mv1.BookingHeaderList.FirstOrDefault().TotalAmount.ToString();
                ViewBag.Amount = mv1.BookingHeaderList.FirstOrDefault().TotalAmount.ToString();
                mv.ReadyTime = mv1.BookingHeaderList.FirstOrDefault().ReadyTime;
                mv.AppointmentType = mv1.BookingHeaderList.FirstOrDefault().AppointmentType;

                // BookingDetailViewModel bd = new BookingDetailViewModel();

                //COMMENT ON 02-10-2023
                //bd.BookingId = mv.BookingId.ToString();
                //var BookingDetailsList = ApiCall.PostApi("BookingDetailRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(bd));
                //bd = JsonConvert.DeserializeObject<BookingDetailViewModel>(BookingDetailsList);
                //List<BookAppointmentDetailViewModel> sittingList = new List<BookAppointmentDetailViewModel>();
                //if (bd.BookingDetailList.Count > 0)
                //{
                //    foreach (var serviceResult in bd.BookingDetailList)
                //    {

                //        BookAppointmentDetailViewModel PackageDetail = new BookAppointmentDetailViewModel
                //        {
                //            NoOfSitting = serviceResult.NoOfSitting.ToString(),
                //            IntePackageServiceId = serviceResult.IntePackageServiceId.ToString(),
                //            CategoryId = serviceResult.CategoryId.ToString(),
                //            CategoryName = serviceResult.CategoryName.ToString(),
                //            DayInterval = serviceResult.DayInterval,
                //            SubCategoryId = serviceResult.SubCategoryId.ToString(),
                //            SubCategoryName = serviceResult.SubCategoryName.ToString(),
                //            Amount = serviceResult.Amount.ToString(),
                //            Remark = "",
                //            BookAppID = serviceResult.BookingId,
                //        };
                //        //for (int i = 0; i < noOfSitting; i++)
                //        //{
                //        //    BookAppointmentDetailViewModel PackageDetail = new BookAppointmentDetailViewModel
                //        //    {
                //        //        IntePackageServiceId = IntePackageServiceId,
                //        //        CategoryId = CategoryId,
                //        //        CategoryName = CategoryName,
                //        //        DayInterval = DayInterval,
                //        //        PackageId = PackageId2.ToString(),
                //        //        SubCategoryId = ServiceId,
                //        //        SubCategoryName = SubCategoryName

                //        //    };

                //        // Add the current PackageDetail to the sittingList

                //        sittingList.Add(PackageDetail);
                //        // }
                //    }
                //}
                ////var data = new List<object>
                ////    {
                ////        new
                ////        {
                ////         CategoryId = bd.BookingDetailList.FirstOrDefault().CategoryId,
                ////        CategoryName = bd.BookingDetailList.FirstOrDefault().CategoryName,
                ////        SubCategoryId = bd.BookingDetailList.FirstOrDefault().SubCategoryId,
                ////        SubCategoryName = bd.BookingDetailList.FirstOrDefault().SubCategoryName,
                ////        NoOfSitting = bd.BookingDetailList.FirstOrDefault().NoOfSitting,
                ////        DayInterval = bd.BookingDetailList.FirstOrDefault().DayInterval,
                ////        Remark = bd.BookingDetailList.FirstOrDefault().Remark,
                ////        IntePackageServiceId = bd.BookingDetailList.FirstOrDefault().IntePackageServiceId
                ////        }
                ////    };
                ////string jsonString = JsonConvert.SerializeObject(data);
                ////ViewBag.JsonData = jsonString;
                ////[{ "CategoryId":"31","CategoryName":"Extra Ready","SubCategoryId":"328","SubCategoryName":"BASIC","NoOfSitting":"1","DayInterval":"1","Remark":"","IntePackageServiceId":0}] 

                ////END COMMENT 02-10-2023
            }
            return View(mv);
        }
        [HttpPost]
        public ActionResult UpdateBookAppointment(BookAppointmentViewModel mv)
        {
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            string jsonData = Request.Form["bookServicesTableJson"];
            List<BookAppointmentDetailViewModel> bookServicesList = JsonConvert.DeserializeObject<List<BookAppointmentDetailViewModel>>(jsonData);
            mv.BookServicesTable = bookServicesList;
            dt.Columns.AddRange(
                     new DataColumn[11] {
                       new DataColumn("CategoryId", typeof(string)),
                         new DataColumn("IntePackageServiceId", typeof(string)) { AllowDBNull = true },
                          new DataColumn("SubCategoryId", typeof(string)),
                          new DataColumn("AppointmentDate", typeof(string)),
                          new DataColumn("DoneBy", typeof(string)) { AllowDBNull = true },
                          new DataColumn("DoneDate", typeof(string)) { AllowDBNull = true },
                          new DataColumn("Amount", typeof(string)){ AllowDBNull = true },
                          new DataColumn("Discount", typeof(string)) { AllowDBNull = true },
                          new DataColumn("FinalAmount", typeof(string)){ AllowDBNull = true },
                          new DataColumn("Remark", typeof(string)) { AllowDBNull = true },
                          new DataColumn("TimeSlotId", typeof(string)) { AllowDBNull = true }
                       });
            if (mv.BookAppointmentTable != null)
            {
                foreach (BookAppointmentDetailViewModel item in mv.BookAppointmentTable)
                {
                    string CategoryId = item.CategoryId.Trim();
                    string IntePSId = item.IntePackageServiceId;
                    string SubCategoryId = item.SubCategoryId.Trim();
                    string AppointmentDate = generalFunctions.dateconvert(item.AppointmentDateTime.Trim());
                    string Amount = item.Amount;
                    string Discount = item.Discount;
                    string FinalAmount = item.FinalAmount;
                    string Remark = item.Remark;
                    string DoneBy = item.DoneBy;
                    string DoneDate = item.DoneDate;
                    string TimeSlotId = item.TimeSlotId;
                    dt.Rows.Add(CategoryId, IntePSId, SubCategoryId, AppointmentDate, DoneBy, DoneDate, Amount, Discount, FinalAmount, Remark, TimeSlotId);
                }
            }
            dt1.Columns.AddRange(
                   new DataColumn[5] {
                       new DataColumn("CategoryId", typeof(string)),
                         new DataColumn("IntePackageServiceId", typeof(string)) { AllowDBNull = true },
                          new DataColumn("SubCategoryId", typeof(string)),
                          new DataColumn("Amount", typeof(string)) { AllowDBNull = true },
                          new DataColumn("Remark", typeof(string)) { AllowDBNull = true }
                     });
            if (mv.BookServicesTable != null)
            {
                foreach (BookAppointmentDetailViewModel item in mv.BookServicesTable)
                {
                    string CategoryId = item.CategoryId.Trim();
                    string IntePSId = item.IntePackageServiceId;
                    string SubCategoryId = item.SubCategoryId.Trim();
                    string Amount = item.Amount;
                    string Remark = item.Remark;
                    dt1.Rows.Add(CategoryId, IntePSId, SubCategoryId, Amount, Remark);
                }
            }
            mv.mytable = dt;
            mv.mytable1 = dt1;
            mv.CompanyCode = LoggedUserDetails.CompanyCode;
            mv.CreateDate = generalFunctions.getTimeZoneDatetimedb();
            mv.CreateUser = LoggedUserDetails.UserName;
            mv.FunctionDate = generalFunctions.dateconvert(mv.FunctionDate);
            mv.Action = "Insert";
            if (dt.Rows.Count > 0 && dt1.Rows.Count > 0)
            {
                var AttLog = ApiCall.PostApi("BookingAppointmentInsUpd", Newtonsoft.Json.JsonConvert.SerializeObject(mv));
                mv = JsonConvert.DeserializeObject<BookAppointmentViewModel>(AttLog);
                string msg = mv.Result;
                bool containsOnlyNumbers = Regex.IsMatch(msg, "^[0-9]+$");
                if (containsOnlyNumbers)
                {
                    var data = new { Message = "Appointment Booked Successfully", Type = "success" };
                    TempData["SweetAlert"] = data;
                    return RedirectToAction("OrderBook", "BookingManagement", new { msg = msg });
                }
                else
                {
                    return View("AddBookAppointment", mv);
                }
            }
            else
            {
                var data = new { Message = "Please try Again", Type = "error" };
                TempData["SweetAlert"] = data;
                return View("AddBookAppointment", mv);
            }
        }

        public ActionResult ViewBookedAppointment()
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
            BookingHeaderViewModel mv = new BookingHeaderViewModel();
            mv.Action = "Pending";
            if (mv.FromDate == null)
            {
                mv.NewFromDate = generalFunctions.getDate();
                mv.FromDate = generalFunctions.dateconvert(generalFunctions.getDate());
            }
            else
            {
                mv.NewFromDate = generalFunctions.dateconvert(mv.FromDate);
            }
            if (mv.ToDate == null)
            {
                mv.NewToDate = generalFunctions.getDate();
                mv.ToDate = generalFunctions.dateconvert(generalFunctions.getDate());
            }
            else
            {
                mv.NewToDate = generalFunctions.dateconvert(mv.ToDate);
            }

            var BookingList = ApiCall.PostApi("BookingHeaderRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(mv));
            mv = JsonConvert.DeserializeObject<BookingHeaderViewModel>(BookingList);
            return View(mv);
        }
        [HttpPost]
        public ActionResult ViewBookedAppointment(BookingHeaderViewModel mv)
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
            //if (mv1.MenuRightsList.Count > 0)
            //{
            //    //ViewBag.ViewRight = mv1.MenuRightsList.FirstOrDefault().ViewRight;
            //    //ViewBag.InsertRight = mv1.MenuRightsList.FirstOrDefault().InsertRight;
            //    //ViewBag.UpdateRight = mv1.MenuRightsList.FirstOrDefault().UpdateRight;
            //    //ViewBag.DeleteRight = mv1.MenuRightsList.FirstOrDefault().DeleteRight;
            //    //TempData["ViewRight"] = mv1.MenuRightsList.FirstOrDefault().ViewRight;
            //    //TempData["InsertRight"] = mv1.MenuRightsList.FirstOrDefault().InsertRight;
            //    //TempData["UpdateRight"] = mv1.MenuRightsList.FirstOrDefault().UpdateRight;
            //    //TempData["DeleteRight"] = mv1.MenuRightsList.FirstOrDefault().DeleteRight;
            //}
            //else
            //{
            //    var data = new { Message = "Sorry,You have no rights to access this page", Type = "error" };
            //    TempData["SweetAlert"] = data;
            //    return RedirectToAction("Dashboard", "Home");
            //}
            TempData["ViewRight"] = 1;
            TempData["InsertRight"] = 1;
            TempData["UpdateRight"] = 1;
            TempData["DeleteRight"] = 1;
            mv.Action = "Pending";
            if (mv.FromDate == null)
            {
                mv.NewFromDate = generalFunctions.getDate();
                mv.FromDate = generalFunctions.dateconvert(generalFunctions.getDate());
            }
            else
            {
                mv.NewFromDate = generalFunctions.dateconvert(mv.FromDate);
            }
            if (mv.ToDate == null)
            {
                mv.NewToDate = generalFunctions.getDate();
                mv.ToDate = generalFunctions.dateconvert(generalFunctions.getDate());
            }
            else
            {
                mv.NewToDate = generalFunctions.dateconvert(mv.ToDate);
            }

            var BookingList = ApiCall.PostApi("BookingHeaderRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(mv));
            mv = JsonConvert.DeserializeObject<BookingHeaderViewModel>(BookingList);
            return View(mv);
        }

        public ActionResult GetBookedAppointmentDetails(int BookAppId)
        {
            BookingServicesViewModel bd = new BookingServicesViewModel();
            bd.BookingId = BookAppId.ToString();
            bd.Action = "Getdetails";
            bd.CompanyCode = LoggedUserDetails.CompanyCode;
            var BookingDetailsList = ApiCall.PostApi("BookingServicesRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(bd));
            bd = JsonConvert.DeserializeObject<BookingServicesViewModel>(BookingDetailsList);
            List<BookAppointmentDetailViewModel> sittingList = new List<BookAppointmentDetailViewModel>();
            if (bd.BookingServicesList.Count > 0)
            {
                foreach (var serviceResult in bd.BookingServicesList)
                {
                    BookAppointmentDetailViewModel PackageDetail = new BookAppointmentDetailViewModel
                    {
                        NoOfSitting = serviceResult.noofsitting.ToString(),
                        IntePackageServiceId = serviceResult.IntePackageServiceId.ToString(),
                        CategoryId = serviceResult.CategoryId.ToString(),
                        CategoryName = serviceResult.CategoryName.ToString(),
                        DayInterval = serviceResult.dayinterval,
                        SubCategoryId = serviceResult.SubCategoryId.ToString(),
                        SubCategoryName = serviceResult.SubCategoryName.ToString(),
                        Amount = serviceResult.Amount.ToString(),
                        Remark = "",
                        BookAppID = Convert.ToInt32(serviceResult.BookingId),
                        BookServiceID = serviceResult.BookingServiceId,
                        DeleteStatus = serviceResult.DeleteStatus,
                    };
                    sittingList.Add(PackageDetail);
                }
            }
            var obj = new { sittingList };
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteBookedService(int id, int id1)
        {
            BookAppointmentViewModel bd = new BookAppointmentViewModel();
            bd.Action = "delete";
            bd.BookingServiceId = id;
            bd.SubCategoryId = id1;
            var BookingServiceList = ApiCall.PostApi("BookingServiceDelete", Newtonsoft.Json.JsonConvert.SerializeObject(bd));
            bd = JsonConvert.DeserializeObject<BookAppointmentViewModel>(BookingServiceList);
            return Json(bd.Result, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}