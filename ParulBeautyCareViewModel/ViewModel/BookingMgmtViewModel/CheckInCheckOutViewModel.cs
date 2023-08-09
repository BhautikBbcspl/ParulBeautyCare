using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParulBeautyCareDbClasses.DataModels;

namespace ParulBeautyCareViewModel.ViewModel.BookingMgmtViewModel
{
    public class CheckInCheckOutViewModel
    {
        public string CheckId { get; set; }

        [Required(ErrorMessage = "*")]
        public string CustomerName { get; set; }

        [Required(ErrorMessage = "*")]
        public string ContactNo { get; set; }
        public string HouseNoSociety { get; set; }
        public string Landmark { get; set; }

        [Required(ErrorMessage = "*")]
        public string City { get; set; }

        [Required(ErrorMessage = "*")]
        public string Pincode { get; set; }


        public string CheckinDateTime { get; set; }
        public string CheckoutDateTime { get; set; }
        public string WaitingTime { get; set; }
        public string BookingId { get; set; }
        public string Note { get; set; }


        public bool IsBooking { get; set; }
        public List<PBBookingHeaderRtr_Result> BookingList { get; set; }
        public List<PBCheckInCheckOutRetrieve_Result> CheckInCheckOutList { get; set; }

        public string CompanyCode { get; set; }
        public string CreateDate { get; set; }
        public string CreateUser { get; set; }
        public string UpdateDate { get; set; }
        public string UpdateUser { get; set; }
        public string Action { get; set; }
        public string PageName { get; set; }
        public string success { get; set; }
        public string message { get; set; }
        public string result { get; set; }
    }
}
