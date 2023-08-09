using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParulBeautyCareDbClasses.DataModels;

namespace ParulBeautyCareViewModel.ViewModel.BookingMgmtViewModel
{
    public class BookingStaffAllocationViewModel
    {
        [Required(ErrorMessage = "*")]
        public string BookingId { get; set; }
        public string CustomerName { get; set; }
        public string CompanyCode { get; set; }
        public string Action { get; set; }
        public List<PBBookingHeaderRtr_Result> BookingHeaderList { get; set; }
        public string success { get; set; }
        public string message { get; set; }
    }
}
