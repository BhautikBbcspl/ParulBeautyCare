using ParulBeautyCareDbClasses.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParulBeautyCareViewModel.ViewModel.BookingMgmtViewModel
{
    public class BookingHeaderViewModel
    {
        public string BookingId { get; set; }
        public string DepartmentId { get; set; }
        public string BookingCode { get; set; }
        public string CustomerName { get; set; }
        public string CompanyCode { get; set; }
        public string Action { get; set; }
        public string CustomerId { get; set; }
        public string ContactNo { get; set; }
        public string Address { get; set; }
        public string BookDate { get; set; }
        public string NoOfPerson { get; set; }
        public string TotalAmount { get; set; }
        public string DiscountPerc { get; set; }
        public string Discount { get; set; }
        public string FinalAmount { get; set; }
        public string AdvanceBookingCharge { get; set; }
        public string PaidAmount { get; set; }
        public string FunctionDate { get; set; }
        public string ReadyTime { get; set; }
        public string Status { get; set; }
        public string UpdateDate { get; set; }
        public string UpdateUser { get; set; }
        public string CreateDate { get; set; }
        public string CreateUser { get; set; }
        public List<PBBookingHeaderRtr_Result> BookingHeaderList { get; set; }
        public List<PBBookingDetailRtr_Result> BookingDetailList { get; set; }
        public string success { get; set; }
        public string message { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }

    }
}
