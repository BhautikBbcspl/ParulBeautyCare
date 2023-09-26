using ParulBeautyCareDbClasses.DataModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParulBeautyCareViewModel.ViewModel.BookingMgmtViewModel
{
   public  class AdvanceBookingViewModel
    {
        public int AdvanceBookingId { get; set; }
        [Required(ErrorMessage = "*")]
        public string Name { get; set; }
        [Required(ErrorMessage = "*")]
        public string SPersonName { get; set; }
        public string Address { get; set; }
        public string SPersonAddress { get; set; }
        [Required(ErrorMessage = "*")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Invalid Contact.")]
        public string ContactNo { get; set; }
        [Required(ErrorMessage = "*")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Invalid Contact.")]
        public string SPersonContact { get; set; }
        [Required(ErrorMessage = "*")]
        public string NumOfSiders{ get; set; }
        public string BridalDeposit { get; set; }
        public string SidersDeposit { get; set; }
        public string TotalDeposit { get; set; }
        public string BeforeRemark { get; set; }
        public string AfterRemark { get; set; }
        public string Action { get; set; }
        public string CreateDate { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string NewFromDate { get; set; }
        public string NewToDate { get; set; }
        public string CreateUser { get; set; }
        public string UpdateDate { get; set; }
        public string UpdateUser { get; set; }
        public string Result { get; set; }
        public string Message { get; set; }
        public string Success { get; set; }
        public string CompanyCode { get; set; }
        [Required(ErrorMessage = "*")]
        public string DepartmentId { get; set; }
        [Required(ErrorMessage = "*")]
        public string FunctionDate { get; set; }
        public string TotalSidersDeposit { get; set; }
        public List<PBDepartmentMasterRetrieve_Result> DepartmentList { get; set; }
        public List<PB_AdvanceBookingRtr_Result> AdvanceBookingList { get; set; }
    }
}
