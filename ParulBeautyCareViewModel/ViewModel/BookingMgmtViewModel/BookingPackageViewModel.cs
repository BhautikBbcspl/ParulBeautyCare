using ParulBeautyCareDbClasses.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParulBeautyCareViewModel.ViewModel.BookingMgmtViewModel
{
 public class BookingPackageViewModel
    {
        public List<PBIntePackageServiceRtr_Result> PBIntePackageServiceList { get; set; }
        public List<BookAppointmentDetailViewModel> PBPackingTableList { get; set; }
        public string Action { get; set; }
        public string PackageId { get; set; }
        public string success { get; set; }
        public string message { get; set; }
    }


}
