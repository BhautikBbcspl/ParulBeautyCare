using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ClosedXML.Excel;
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

        #region==> Incoice (Bhautik)
        public ActionResult Invoice()
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
            InvoiceDetailViewModel invm = new InvoiceDetailViewModel();
            invm.CompanyCode = LoggedUserDetails.CompanyCode;
            invm.Action = "active";
            invm.CreateDate = generalFunctions.getTimeZoneDatetimedb();
            invm.CreateUser = User.Identity.Name;

            ////Department List Bind
            //invm.Action = "active";
            //DepartmentMasterViewModel dm = new DepartmentMasterViewModel();
            //var DeptList = ApiCall.PostApi("DepartmentMasterRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(invm));
            //dm = JsonConvert.DeserializeObject<DepartmentMasterViewModel>(DeptList);
            //invm.DeptList = dm.DepartmentList;
            ////

            //// Done Bookings List Bind
            //invm.Action = "all";
            //BookingHeaderViewModel bh = new BookingHeaderViewModel();
            //var donebookinglist = ApiCall.PostApi("BookingHeaderRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(invm));
            //bh = JsonConvert.DeserializeObject<BookingHeaderViewModel>(donebookinglist);
            //invm.DoneBookingsList = bh.BookingHeaderList.Where(x => x.Status == 0).ToList();
            //invm.DoneBookingsList.ForEach(e =>
            //{
            //    e.CustomerName = $"{e.BookingCode} - {e.CustomerName}";
            //});
            ////

            invm.Action = "active";
            ItemMasterViewModel itm = new ItemMasterViewModel();
            var itmList = ApiCall.PostApi("ItemMasterRtr", Newtonsoft.Json.JsonConvert.SerializeObject(invm));
            itm = JsonConvert.DeserializeObject<ItemMasterViewModel>(itmList);
            invm.ItemRetrieve = itm.ItemMasterList;

            return View(invm);
        }
        [HttpPost]
        public ActionResult FetchBooking(string BookingContactNo, string BookingCode)
        {
            BookingHeaderViewModel bhm = new BookingHeaderViewModel();
            bhm.CompanyCode = LoggedUserDetails.CompanyCode;
            bhm.Action = "all";
            var BookingHeaderList = ApiCall.PostApi("BookingHeaderRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(bhm));
            bhm = JsonConvert.DeserializeObject<BookingHeaderViewModel>(BookingHeaderList);
            List<PBBookingHeaderRtr_Result> bhv;
            List<PBBookingHeaderRtr_Result> pbhv;
            if (!string.IsNullOrEmpty(BookingContactNo))
            {
                bhv = bhm.BookingHeaderList.Where(x => x.ContactNo == BookingContactNo && x.Status == 2).ToList();
                pbhv = bhm.BookingHeaderList.Where(x => x.ContactNo == BookingContactNo && x.Status == 4).ToList();

                var obj = new { bhv, pbhv };
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            else if (!string.IsNullOrEmpty(BookingCode))
            {
                bhv = bhm.BookingHeaderList.Where(x => x.BookingCode == BookingCode && x.Status == 2).ToList();
                pbhv = bhm.BookingHeaderList.Where(x => x.BookingCode == BookingCode && x.Status == 4).ToList();
                var obj = new { bhv, pbhv };
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            else
            {
                bhv = new List<PBBookingHeaderRtr_Result>();
                pbhv = new List<PBBookingHeaderRtr_Result>();
                var obj = new { bhv, pbhv };
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public ActionResult BookingDetailRTR(string BookingHeaderId)
        {
            //BookingDetailViewModel bvm = new BookingDetailViewModel();
            //bvm.BookingId = BookingHeaderId;
            //bvm.CompanyCode = LoggedUserDetails.CompanyCode;
            //bvm.Action = "details";
            //var BookingDetailList = ApiCall.PostApi("BookingDetailRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(bvm));
            //bvm = JsonConvert.DeserializeObject<BookingDetailViewModel>(BookingDetailList);
            //List<PBBookingDetailRtr_Result> bdv = bvm.BookingDetailList;

            BookingServicesViewModel bvm = new BookingServicesViewModel();
            bvm.BookingId = BookingHeaderId;
            bvm.CompanyCode = LoggedUserDetails.CompanyCode;
            bvm.Action = "details";
            var BookingServiceList = ApiCall.PostApi("BookingServicesRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(bvm));
            bvm = JsonConvert.DeserializeObject<BookingServicesViewModel>(BookingServiceList);
            List<PBBookingServicesRtr_Result> bdv = bvm.BookingServicesList;

            BookingHeaderViewModel bhm = new BookingHeaderViewModel();
            bhm.CompanyCode = LoggedUserDetails.CompanyCode;
            bhm.Action = "all";
            var BookingHeaderList = ApiCall.PostApi("BookingHeaderRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(bhm));
            bhm = JsonConvert.DeserializeObject<BookingHeaderViewModel>(BookingHeaderList);
            List<PBBookingHeaderRtr_Result> bhv = bhm.BookingHeaderList.Where(x => x.BookingId.ToString() == BookingHeaderId).ToList();

            GSTMasterViewModel gsv = new GSTMasterViewModel();
            gsv.Action = "all";
            gsv.CompanyCode = LoggedUserDetails.CompanyCode;
            var TimeslotList = ApiCall.PostApi("GSTMasterRtr", Newtonsoft.Json.JsonConvert.SerializeObject(gsv));
            gsv = JsonConvert.DeserializeObject<GSTMasterViewModel>(TimeslotList);
            PBGSTMasterRetrieve_Result gsmv = gsv.GSTMasterList.Where(x => x.IsActive == true).FirstOrDefault();

            var obj = new { bdv, bhv, gsmv };
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult GetDepartment(string DepartmentId)
        {
            DepartmentMasterViewModel dept = new DepartmentMasterViewModel();
            dept.DepartmentId = DepartmentId;
            dept.CompanyCode = LoggedUserDetails.CompanyCode;
            dept.Action = "details";
            var DeptList = ApiCall.PostApi("DepartmentMasterRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(dept));
            dept = JsonConvert.DeserializeObject<DepartmentMasterViewModel>(DeptList);
            dept.DepartmentList = dept.DepartmentList;

            InvoiceDetailViewModel inv = new InvoiceDetailViewModel();
            inv.Action = "getAbrv";
            inv.DeptAbbr = dept.DepartmentList.FirstOrDefault().DeptAbrv;
            inv.CompanyCode = LoggedUserDetails.CompanyCode;
            var billList = ApiCall.PostApi("GenBillingCode", Newtonsoft.Json.JsonConvert.SerializeObject(inv));
            inv = JsonConvert.DeserializeObject<InvoiceDetailViewModel>(billList);

            var obj = new { dept.DepartmentList, inv.NewBillingCode };
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult AddInvoice(List<BillingHeaderDataTable> BillingHeaderData, List<BillingDetailsDataTable> BillingDetailsData)
        {
            try
            {
                InvoiceDetailViewModel idvm = new InvoiceDetailViewModel();

                foreach (var TextboxItem in BillingHeaderData)
                {
                    idvm.BookingId = TextboxItem.BookingId;
                    idvm.BookingCode = TextboxItem.BookingCode;
                    idvm.DepartmentId = TextboxItem.DepartmentId;
                    idvm.BillCode = TextboxItem.BillCode;
                    idvm.CustomerId = TextboxItem.CustomerId;
                    idvm.ContactNo = TextboxItem.ContactNo;
                    idvm.CustomerName = TextboxItem.CustomerName;
                    idvm.Address = TextboxItem.Address;
                    idvm.BillDate = generalFunctions.dateconvert(TextboxItem.BillDate);
                    idvm.GSTPerc = TextboxItem.GSTPerc;
                    idvm.GSTAmount = TextboxItem.GSTAmount;
                    idvm.BaseAmount = TextboxItem.BaseAmount;
                    idvm.DiscountPerc = TextboxItem.DiscountPerc;
                    idvm.Discount = TextboxItem.Discount;
                    idvm.FinalAmount = TextboxItem.FinalAmount;
                    idvm.CreateDate = generalFunctions.getTimeZoneDatetimedb();
                    idvm.CreateUser = User.Identity.Name;
                    idvm.CompanyCode = LoggedUserDetails.CompanyCode;

                    int cnt = 0;
                    idvm.BillingDetailsTable = new List<BillingDetailsDataTable>(); // Initialize the list

                    foreach (var item in BillingDetailsData)
                    {
                        BillingDetailsDataTable billDetails = new BillingDetailsDataTable
                        {
                            BillingDetailId = item.BillingDetailId,
                            CategoryId = item.CategoryId,
                            SubCategoryId = item.SubCategoryId,
                            ItemId = item.ItemId,
                            Qty = item.Qty,
                            Remark = item.Remark,
                            Amount = item.Amount,
                            FinalAmount = item.FinalAmount
                        };
                        idvm.BillingDetailsTable.Add(billDetails);
                        cnt++;
                    }
                }

                idvm.Action = "insert";

                var apiResponse = ApiCall.PostApi("InvoiceInsUpd", Newtonsoft.Json.JsonConvert.SerializeObject(idvm));
                idvm = JsonConvert.DeserializeObject<InvoiceDetailViewModel>(apiResponse);
                string msg = idvm.result;
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var data = new { Message = ex.Message.ToString(), Type = "error" };
                TempData["SweetAlert"] = data;
                return RedirectToAction("Invoice", "Invoice");
            }
        }
        #endregion

        #region==> Invoice Edit (Bhautik)
        public ActionResult InvoiceRegister()
        {
            InvoiceDetailViewModel invm = new InvoiceDetailViewModel();
            invm.CompanyCode = LoggedUserDetails.CompanyCode;
            invm.Action = "active";
            invm.CreateDate = generalFunctions.getTimeZoneDatetimedb();
            invm.CreateUser = User.Identity.Name;

            ItemMasterViewModel itm = new ItemMasterViewModel();
            var itmList = ApiCall.PostApi("ItemMasterRtr", Newtonsoft.Json.JsonConvert.SerializeObject(invm));
            itm = JsonConvert.DeserializeObject<ItemMasterViewModel>(itmList);
            invm.ItemRetrieve = itm.ItemMasterList;
            ViewBag.ItemList = new SelectList(invm.ItemRetrieve, "ItemId", "ItemName");

            return View(invm);
        }
        [HttpPost]
        public ActionResult UpdateInvoice(List<BillingHeaderDataTable> BillingHeaderData, List<BillingDetailsDataTable> BillingDetailsData)
        {
            try
            {
                InvoiceDetailViewModel idvm = new InvoiceDetailViewModel();

                foreach (var TextboxItem in BillingHeaderData)
                {
                   
                    idvm.BillId = TextboxItem.BillId;
                    idvm.BillCode = TextboxItem.BillCode;
                    idvm.GSTPerc = TextboxItem.GSTPerc;
                    idvm.GSTAmount = TextboxItem.GSTAmount;
                    idvm.BaseAmount = TextboxItem.BaseAmount;
                    idvm.DiscountPerc = TextboxItem.DiscountPerc;
                    idvm.Discount = TextboxItem.Discount;
                    idvm.FinalAmount = TextboxItem.FinalAmount;
                    idvm.UpdateDate = generalFunctions.getTimeZoneDatetimedb();
                    idvm.UpdateUser = User.Identity.Name;

                    int cnt = 0;
                    idvm.BillingDetailsTable = new List<BillingDetailsDataTable>(); // Initialize the list

                    foreach (var item in BillingDetailsData)
                    {
                        BillingDetailsDataTable billDetails = new BillingDetailsDataTable
                        {
                            BillingDetailId = item.BillingDetailId,
                            CategoryId = item.CategoryId,
                            SubCategoryId = item.SubCategoryId,
                            ItemId = item.ItemId,
                            Qty = item.Qty,
                            Remark = item.Remark,
                            Amount = item.Amount,
                            FinalAmount = item.FinalAmount
                        };
                        idvm.BillingDetailsTable.Add(billDetails);
                        cnt++;
                    }
                }

                idvm.Action = "update";

                var apiResponse = ApiCall.PostApi("InvoiceInsUpd", Newtonsoft.Json.JsonConvert.SerializeObject(idvm));
                idvm = JsonConvert.DeserializeObject<InvoiceDetailViewModel>(apiResponse);
                string msg = idvm.result;
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var data = new { Message = ex.Message.ToString(), Type = "error" };
                TempData["SweetAlert"] = data;
                return RedirectToAction("Invoice", "Invoice");
            }
        }
        [HttpPost]
        public ActionResult FetchInvoice(string BookingContactNo, string BookingCode)
        {
            BookingHeaderViewModel bhm = new BookingHeaderViewModel();
            bhm.CompanyCode = LoggedUserDetails.CompanyCode;
            bhm.Action = "all";
            var BookingHeaderList = ApiCall.PostApi("BookingHeaderRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(bhm));
            bhm = JsonConvert.DeserializeObject<BookingHeaderViewModel>(BookingHeaderList);
            List<PBBookingHeaderRtr_Result> bhv;
            if (!string.IsNullOrEmpty(BookingContactNo))
            {
                bhv = bhm.BookingHeaderList.Where(x => x.ContactNo == BookingContactNo && x.Status == 4).ToList();
                var obj = new { bhv };
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            else if (!string.IsNullOrEmpty(BookingCode))
            {
                bhv = bhm.BookingHeaderList.Where(x => x.BookingCode == BookingCode && x.Status == 4).ToList();
                var obj = new { bhv };
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            else
            {
                bhv = new List<PBBookingHeaderRtr_Result>();
                var obj = new { bhv };
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult BillingDetailRTR(string BookingHeaderId)
        {
            BillDetailViewModel bdvm = new BillDetailViewModel();
            bdvm.Action = "details";
            bdvm.BookingId = BookingHeaderId;
            var billDetaillist = ApiCall.PostApi("BillingDetailRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(bdvm));
            bdvm = JsonConvert.DeserializeObject<BillDetailViewModel>(billDetaillist);
            List<PBBillingDetailRetrieve_Result> bdr = bdvm.BillDetailList;

            InvoiceDetailViewModel idvm = new InvoiceDetailViewModel();
            idvm.Action = "all";
            idvm.CompanyCode = LoggedUserDetails.CompanyCode;
            var billingHeaderlist = ApiCall.PostApi("BillingRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(idvm));
            idvm = JsonConvert.DeserializeObject<InvoiceDetailViewModel>(billingHeaderlist);
            List<PBBillingRetrieve_Result> bhdt = idvm.BillRtr;

            BookingServicesViewModel bvm = new BookingServicesViewModel();
            bvm.BookingId = BookingHeaderId;
            bvm.CompanyCode = LoggedUserDetails.CompanyCode;
            bvm.Action = "details";
            var BookingServiceList = ApiCall.PostApi("BookingServicesRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(bvm));
            bvm = JsonConvert.DeserializeObject<BookingServicesViewModel>(BookingServiceList);
            List<PBBookingServicesRtr_Result> bdv = bvm.BookingServicesList;

            BookingHeaderViewModel bhm = new BookingHeaderViewModel();
            bhm.CompanyCode = LoggedUserDetails.CompanyCode;
            bhm.Action = "all";
            var BookingHeaderList = ApiCall.PostApi("BookingHeaderRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(bhm));
            bhm = JsonConvert.DeserializeObject<BookingHeaderViewModel>(BookingHeaderList);
            List<PBBookingHeaderRtr_Result> bhv = bhm.BookingHeaderList.Where(x => x.BookingId.ToString() == BookingHeaderId).ToList();

            GSTMasterViewModel gsv = new GSTMasterViewModel();
            gsv.Action = "all";
            gsv.CompanyCode = LoggedUserDetails.CompanyCode;
            var TimeslotList = ApiCall.PostApi("GSTMasterRtr", Newtonsoft.Json.JsonConvert.SerializeObject(gsv));
            gsv = JsonConvert.DeserializeObject<GSTMasterViewModel>(TimeslotList);
            PBGSTMasterRetrieve_Result gsmv = gsv.GSTMasterList.Where(x => x.IsActive == true).FirstOrDefault();

            var obj = new { bdr,bhdt,bdv, bhv, gsmv };
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        #region==> When click on (-)button delete row from table in Billing Detail
        [HttpPost]
        public ActionResult DeleteBillingDetail(string BillingDetailId,string BillingHeaderId)
        {
            BillDetailViewModel bdvm = new BillDetailViewModel();
            bdvm.Action = "delete";
            bdvm.BillingDetailId = BillingDetailId;
            var billDetaillist = ApiCall.PostApi("BillingDetailInsUpd", Newtonsoft.Json.JsonConvert.SerializeObject(bdvm));
            bdvm = JsonConvert.DeserializeObject<BillDetailViewModel>(billDetaillist);

            InvoiceDetailViewModel bvm = new InvoiceDetailViewModel();
            bvm.Action = "all";
            bvm.CompanyCode = LoggedUserDetails.CompanyCode;
            var billingHeaderlist = ApiCall.PostApi("BillingRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(bvm));
            bvm = JsonConvert.DeserializeObject<InvoiceDetailViewModel>(billingHeaderlist);
            var bookingId = bvm.BillRtr.Where(x => x.BillId.ToString() == BillingHeaderId).FirstOrDefault().BookingId;

            string msg = bdvm.result;
            if (msg.Contains("Successfully"))
            {
                var data = new { Message = msg, Type = "success", BookingId=bookingId };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var data = new { Message = msg, Type = "error" , BookingId = bookingId };
                return Json(data, JsonRequestBehavior.AllowGet);

            }
        }
        #endregion

        #endregion

        #region==> Invoice Report (Bhautik)
        public ActionResult BillReport()
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
            InvoiceDetailViewModel invm = new InvoiceDetailViewModel();
            invm.CompanyCode = LoggedUserDetails.CompanyCode;

            if (!User.Identity.IsAuthenticated)
            {
                FormsAuthentication.RedirectToLoginPage();
            }
            BillReportViewModel brm = new BillReportViewModel();
            brm.Action = "All";
            var BillReportList = ApiCall.PostApi("InvoiceReport", Newtonsoft.Json.JsonConvert.SerializeObject(brm));
            brm = JsonConvert.DeserializeObject<BillReportViewModel>(BillReportList);
            invm.DateWiseBillReportList = brm.DateWiseBillReportList;

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
            invm.DoneBookingsList = bh.BookingHeaderList.Where(x => x.Status == 0).ToList();
            invm.DoneBookingsList.ForEach(e =>
            {
                e.CustomerName = $"{e.BookingCode} - {e.CustomerName}";
            });
            return View(invm);
        }
        [HttpPost]
        public ActionResult BillReport(InvoiceDetailViewModel invm)
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
            invm.CompanyCode = LoggedUserDetails.CompanyCode;

            if (!User.Identity.IsAuthenticated)
            {
                FormsAuthentication.RedirectToLoginPage();
            }
            BillReportViewModel brm = new BillReportViewModel();
            brm.Action = "Report";
            brm.DepartmentId = invm.ReportDepartmentId;
            brm.BookingId = invm.ReportBookingId;
            brm.FromDate = generalFunctions.dateconvert(invm.FromDate);
            brm.ToDate = generalFunctions.dateconvert(invm.ToDate);
            var BillReportList = ApiCall.PostApi("InvoiceReport", Newtonsoft.Json.JsonConvert.SerializeObject(brm));
            brm = JsonConvert.DeserializeObject<BillReportViewModel>(BillReportList);
            invm.DateWiseBillReportList = brm.DateWiseBillReportList;

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
            invm.DoneBookingsList = bh.BookingHeaderList.Where(x => x.Status == 0).ToList();
            invm.DoneBookingsList.ForEach(e =>
            {
                e.CustomerName = $"{e.BookingCode} - {e.CustomerName}";
            });
            return View(invm);
        }
        public ActionResult SelectBillIdWiseDetailJson(string billId)
        {
            BillReportViewModel brm = new BillReportViewModel();

            BillDetailViewModel model = new BillDetailViewModel();
            model.Action = "all";
            var billDetaillist = ApiCall.PostApi("BillingDetailRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(model));
            model = JsonConvert.DeserializeObject<BillDetailViewModel>(billDetaillist);
            brm.BillDetailList = model.BillDetailList.Where(x => x.BillingId.ToString() == billId).ToList();

            var obj = new { brm.BillDetailList };
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult ExportBillReport(string ReportDepartmentId, string ReportBookingId, string FromDate, string ToDate)
        {
            BillReportViewModel brm = new BillReportViewModel();
            brm.Action = "Report";
            brm.DepartmentId = ReportDepartmentId;
            brm.BookingId = ReportBookingId;
            brm.FromDate = generalFunctions.dateconvert(FromDate);
            brm.ToDate = generalFunctions.dateconvert(ToDate);
            var BillReportList = ApiCall.PostApi("InvoiceReport", Newtonsoft.Json.JsonConvert.SerializeObject(brm));
            brm = JsonConvert.DeserializeObject<BillReportViewModel>(BillReportList);
            var re1 = brm.DateWiseBillReportList;

            using (XLWorkbook workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Invoice Report Sheet");

                string title = "Invoice Report " + brm.FromDate + " to " + brm.ToDate;


                worksheet.Range("A1:L1").Merge().Value = title;

                worksheet.Row(1).Height = 45;
                worksheet.Row(1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                string[] TitlecellNames = { "A1" };

                foreach (string TitlecellName in TitlecellNames)
                {
                    worksheet.Cell(TitlecellName).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    worksheet.Cell(TitlecellName).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    worksheet.Cell(TitlecellName).Style.Font.Bold = true;
                    worksheet.Cell(TitlecellName).Style.Font.FontSize = 20;
                    worksheet.Cell(TitlecellName).Style.Font.FontColor = XLColor.White;
                    worksheet.Cell(TitlecellName).Style.Fill.BackgroundColor = XLColor.FromHtml("#D81B60");
                }

                // Add header row
                worksheet.Cell("A2").Value = "Company";
                worksheet.Cell("B2").Value = "Bill Code";
                worksheet.Cell("C2").Value = "Bill Date";
                worksheet.Cell("D2").Value = "Customer";
                worksheet.Cell("E2").Value = "Customer Contact No";
                worksheet.Cell("F2").Value = "Base Amount";
                worksheet.Cell("G2").Value = "Discount(%)";
                worksheet.Cell("H2").Value = "Discount(Rs)";
                worksheet.Cell("I2").Value = "GST(%)";
                worksheet.Cell("J2").Value = "GST(Rs)";
                worksheet.Cell("K2").Value = "Final Amount";
                worksheet.Cell("L2").Value = "Paid Amount";

                string[] cellNames = { "A2", "B2", "C2", "D2", "E2", "F2", "G2", "H2", "I2", "J2" ,"K2","L2"};

                foreach (string cellName in cellNames)
                {
                    worksheet.Cell(cellName).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    worksheet.Cell(cellName).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    worksheet.Cell(cellName).Style.Font.Bold = true;
                    worksheet.Cell(cellName).Style.Font.FontColor = XLColor.White;
                    worksheet.Cell(cellName).Style.Fill.BackgroundColor = XLColor.FromHtml("#fe7bab");
                }

                string[] columnNames = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J","K","L"};

                foreach (string columnName in columnNames)
                {
                    worksheet.Column(columnName).Style.Alignment.WrapText = true;
                    worksheet.Column(columnName).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    worksheet.Column(columnName).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    worksheet.Column(columnName).Width = 25;
                }
                int rowIndex = 3;
                foreach (var item in re1)
                {
                    worksheet.Cell("A" + rowIndex).Value = item.DepartmentName;
                    worksheet.Cell("B" + rowIndex).Value = item.BillCode;
                    worksheet.Cell("C" + rowIndex).Value = item.BillDate;
                    worksheet.Cell("D" + rowIndex).Value = item.CustomerName;
                    worksheet.Cell("E" + rowIndex).Value = item.ContactNo;
                    worksheet.Cell("F" + rowIndex).Value = item.BaseAmount;
                    worksheet.Cell("G" + rowIndex).Value = item.DiscountPerc;
                    worksheet.Cell("H" + rowIndex).Value = item.Discount;
                    worksheet.Cell("I" + rowIndex).Value = item.GSTPerc;
                    worksheet.Cell("J" + rowIndex).Value = item.GSTAmount;
                    worksheet.Cell("K" + rowIndex).Value = item.FinalAmount;
                    worksheet.Cell("L" + rowIndex).Value = item.PaidAmount;

                    BillDetailViewModel model = new BillDetailViewModel();
                    model.Action = "all";
                    var billDetaillist = ApiCall.PostApi("BillingDetailRetrieve", Newtonsoft.Json.JsonConvert.SerializeObject(model));
                    model = JsonConvert.DeserializeObject<BillDetailViewModel>(billDetaillist);
                    var re2 = model.BillDetailList.Where(x => x.BillingId == item.BillId).ToList();

                    if (re2.Count > 0)
                    {
                        rowIndex++;
                        worksheet.Cell("B" + rowIndex).Value = "Invoice " + item.BillCode + " Details";
                        worksheet.Range("B" + rowIndex + ":K" + rowIndex).Merge().Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        worksheet.Range("B" + rowIndex + ":K" + rowIndex).Merge().Style.Font.FontSize = 14;
                        worksheet.Range("B" + rowIndex + ":K" + rowIndex).Merge().Style.Font.FontColor = XLColor.White;
                        worksheet.Range("B" + rowIndex + ":K" + rowIndex).Style.Fill.BackgroundColor = XLColor.FromHtml("#17c964");

                        rowIndex++;
                        worksheet.Range("B" + rowIndex + ":D" + rowIndex).Merge();
                        worksheet.Cell("B" + rowIndex).Value = "Particulars";
                        worksheet.Cell("E" + rowIndex).Value = "Rate";
                        worksheet.Cell("F" + rowIndex).Value = "Quantity";
                        worksheet.Cell("G" + rowIndex).Value = "Final Amount";
                        worksheet.Range("H" + rowIndex + ":I" + rowIndex).Merge();
                        worksheet.Cell("H" + rowIndex).Value = "Remark";
                        worksheet.Cell("J" + rowIndex).Value = "CreateDate";
                        worksheet.Cell("K" + rowIndex).Value = "CreateUser";

                        // Apply formatting to child table header row
                        string[] childHeaderCellNames = { "B" + rowIndex, "C" + rowIndex, "D" + rowIndex, "E" + rowIndex, "F" + rowIndex, "G" + rowIndex, "H" + rowIndex, "I" + rowIndex, "J" + rowIndex ,"K" + rowIndex };
                        foreach (string cellName in childHeaderCellNames)
                        {
                            worksheet.Cell(cellName).Style.Alignment.WrapText = true;
                            worksheet.Cell(cellName).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            worksheet.Cell(cellName).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                            worksheet.Cell(cellName).Style.Font.Bold = true;
                            worksheet.Cell(cellName).Style.Font.FontColor = XLColor.Black;
                            worksheet.Cell(cellName).Style.Fill.BackgroundColor = XLColor.FromHtml("a7f4deb8");
                        }

                        rowIndex++;
                        foreach (var item2 in re2)
                        {
                            worksheet.Range("B" + rowIndex + ":D" + rowIndex).Merge();
                            if(item2.ItemName==null || item2.ItemName=="")
                            {
                                worksheet.Cell("B" + rowIndex).Value = item2.CategoryName+"("+ item2.SubCategoryName+")";
                            }
                            else
                            {
                                worksheet.Cell("B" + rowIndex).Value = item2.ItemName;
                            }
                            worksheet.Cell("E" + rowIndex).Value = item2.Amount;
                            worksheet.Cell("F" + rowIndex).Value = item2.Qty;
                            worksheet.Cell("G" + rowIndex).Value = item2.FinalAmount;
                            worksheet.Range("H" + rowIndex + ":I" + rowIndex).Merge();
                            worksheet.Cell("H" + rowIndex).Value = item2.Remark;
                            worksheet.Cell("J" + rowIndex).Value = item2.CreateDate;
                            worksheet.Cell("K" + rowIndex).Value = item2.CreateUser;

                            rowIndex++;
                        }
                    }
                    rowIndex++;
                }

                var tableRange = worksheet.Range("A3:J" + (rowIndex - 1));

                using (MemoryStream ms = new MemoryStream())
                {
                    workbook.SaveAs(ms);
                    ms.Position = 0;

                    string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

                    return File(ms.ToArray(), contentType, "InvoiceReport.xlsx");

                }
            }

        }
        #endregion 
    }
}