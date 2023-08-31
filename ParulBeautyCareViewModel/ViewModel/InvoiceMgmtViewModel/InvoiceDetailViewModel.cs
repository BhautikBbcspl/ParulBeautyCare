using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParulBeautyCareDbClasses.DataModels;

namespace ParulBeautyCareViewModel.ViewModel.InvoiceMgmtViewModel
{
    public class InvoiceDetailViewModel
    {
        public string BillingId { get; set; }
        public string BillingCode { get; set; }
        public string CustomerId { get; set; }
        public string ContactNo { get; set; }
        public string CustomerName { get; set; }
        public string Address { get; set; }
        public string BillDate { get; set; }
        public string TotalAmount { get; set; }
        public string DiscountPerc { get; set; }
        public string Discount { get; set; }
        public string FinalAmount { get; set; }
        public string PaidAmount { get; set; }
        public string Remark { get; set; }
        


        public string CompanyCode { get; set; }
        public string CreateDate { get; set; }
        public string CreateUser { get; set; }
        public string UpdateDate { get; set; }
        public string UpdateUser { get; set; }
        public string Action { get; set; }

        public string result { get; set; }
        public string success { get; set; }
        public string message { get; set; }

      
        public List<PBDepartmentMasterRetrieve_Result> DeptList { get; set; }
        public List<PBBookingHeaderRtr_Result> DoneBookingsList { get; set; }


        public string DepartmentId { get; set; }
        public string BookingId { get; set; }
    }
}
