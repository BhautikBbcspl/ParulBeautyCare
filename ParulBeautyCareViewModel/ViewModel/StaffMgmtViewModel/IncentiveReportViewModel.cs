using ParulBeautyCareDbClasses.DataModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParulBeautyCareViewModel.ViewModel.StaffMgmtViewModel
{
    public class IncentiveReportViewModel
    {
        public string Action { get; set; }
        [Required(ErrorMessage = "*")]
        public string FromDate { get; set; }
        [Required(ErrorMessage = "*")]
        public string ToDate { get; set; }
        public string StaffCode { get; set; }
        public string result { get; set; }
        public string success { get; set; }
        public string message { get; set; }
        public List<PBStaffIncentiveReport_Result> StaffIncentiveReportList { get; set; }
    }
}
