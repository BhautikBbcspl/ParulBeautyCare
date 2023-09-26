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
    public class BillReportViewModel
    {
        public string Action { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string BookingId { get; set; }
        public string DepartmentId { get; set; }
        public string result { get; set; }
        public string success { get; set; }
        public string message { get; set; }
        public List<PBDepartmentMasterRetrieve_Result> DeptList { get; set; }
        public List<PBBookingHeaderRtr_Result> DoneBookingsList { get; set; }
        public List<PBBillingDetailRetrieve_Result> BillDetailList { get; set; }
        public List<PB_DateWiseBillReportRtr_Result> DateWiseBillReportList { get; set; }
    }
    public class BillDetailViewModel
    {
        public string BookingId { get; set; }
        public  string BillingDetailId { get; set; }
        public  string BillingId { get; set; }
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string SubCategoryId { get; set; }
        public string SubCategoryName { get; set; }
        public string ItemId { get; set; }
        public string ItemName { get; set; }
        public string Qty { get; set; }
        public  string Amount { get; set; }
        public  string Discount { get; set; }
        public  string FinalAmount { get; set; }
        public  string CreateDate { get; set; }
        public string CreateUser { get; set; }
        public  string UpdateDate { get; set; }
        public string UpdateUser { get; set; }
        public string Remark { get; set; }
        public string Action { get; set; }
        public string result { get; set; }
        public string success { get; set; }
        public string message { get; set; }
        public List<PBBillingDetailRetrieve_Result> BillDetailList { get; set; }
    }
}
