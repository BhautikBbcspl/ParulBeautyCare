using ParulBeautyCareDbClasses.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ParulBeautyCareViewModel.ViewModel.BookingMgmtViewModel
{
    public class BookingServicesViewModel
    {
        public string BookingServiceId { get; set; }
        public string BookingId { get; set; }
        public string BookingNo { get; set; }
        public string CategoryId { get; set; }
        public string SubCategoryId { get; set; }
        public string IntePackageServiceId { get; set; }
        public string Amount { get; set; }
        public string CreateDate { get; set; }
        public string CreateUser { get; set; }
        public string UpdateDate { get; set; }
        public string UpdateUser { get; set; }
        public string Categoryame { get; set; }
        public string SubCategoryName { get; set; }
        public List<PBBookingServicesRtr_Result> BookingServicesList { get; set; }
      
        public string success { get; set; }
        public string message { get; set; }
        public string Action { get; set; }
        public string CompanyCode { get; set; }
        public string Remark { get; set; }
        public string result { get; set; }
    }
}
