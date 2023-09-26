using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParulBeautyCareDbClasses.DataModels;

namespace ParulBeautyCareViewModel.ViewModel.InvoiceMgmtViewModel
{
    public class InvoiceDetailViewModel
    {
        public string BookingContactNo { get; set; }
        public string BookingCode { get; set; }
        public string BillId { get; set; }
        [Required(ErrorMessage = "*")]
        public string BillCode { get; set; }
        public string CustomerId { get; set; }
        public string ContactNo { get; set; }
        public string CustomerName { get; set; }
        public string Address { get; set; }
        [Required(ErrorMessage = "*")]
        public string BillDate { get; set; }
        public decimal? GSTPerc { get; set; }
        public decimal? GSTAmount { get; set; }
        public decimal? BaseAmount { get; set; }
        public decimal? DiscountPerc { get; set; }
        public decimal? Discount { get; set; }
        public decimal? FinalAmount { get; set; }
        public decimal? AdvanceAmount { get; set; }
        public decimal? RemainingAmount { get; set; }
        public string Remark { get; set; }


        public string CompanyCode { get; set; }
        public string CreateDate { get; set; }
        public string CreateUser { get; set; }
        public string UpdateDate { get; set; }
        public string UpdateUser { get; set; }
        public string Action { get; set; }
        [Required(ErrorMessage = "*")]
        public string FromDate { get; set; }
        [Required(ErrorMessage = "*")]
        public string ToDate { get; set; }

        public string result { get; set; }
        public string success { get; set; }
        public string message { get; set; }
        public List<PBDepartmentMasterRetrieve_Result> DeptList { get; set; }
        public List<PBBookingHeaderRtr_Result> DoneBookingsList { get; set; }
        public List<PBBillingRetrieve_Result> BillRtr { get; set; }
        public List<PBItemMasterRetrieve_Result> ItemRetrieve { get; set; }
        public List<PB_DateWiseBillReportRtr_Result> DateWiseBillReportList { get; set; }
        [Required(ErrorMessage = "*")]
        public string DepartmentId { get; set; }
        [Required(ErrorMessage = "*")]
        public string BookingId { get; set; }
        public string DeptAbbr { get; set; }
        public string NewBillingCode { get; set; }
        public string ItemId { get; set; }
        public string Qty { get; set; }
        public string BillDetailId { get; set; }
        public string CategoryId { get; set; }
        public string SubCategoryId { get; set; }
        public decimal? Amount { get; set; }
        public List<BillingDetailsDataTable> BillingDetailsTable { get; set; }

        public string ReportDepartmentId { get; set; }
        public string ReportBookingId { get; set; }
    }
    public class BillingHeaderDataTable
    {
        public string BillId { get; set; }
        public string BookingId { get; set; }
        public string BookingCode { get; set; }
        public string DepartmentId { get; set; }
        public string BillCode { get; set; }
        public string CustomerId { get; set; }
        public string ContactNo { get; set; }
        public string CustomerName { get; set; }
        public string Address { get; set; }
        public string BillDate { get; set; }
        public decimal? GSTPerc { get; set; }
        public decimal? GSTAmount { get; set; }
        public decimal? BaseAmount { get; set; }
        public decimal? DiscountPerc { get; set; }
        public decimal? Discount { get; set; }
        public decimal? FinalAmount { get; set; }
    }
    public class BillingDetailsDataTable
    {
        public string BillingDetailId { get; set; }
        public string CategoryId { get; set; }
        public string SubCategoryId { get; set; }
        public string ItemId { get; set; }
        public string Qty { get; set; }
        public string Remark { get; set; }
        public decimal? Amount { get; set; }
        public decimal? FinalAmount { get; set; }
    }
}
