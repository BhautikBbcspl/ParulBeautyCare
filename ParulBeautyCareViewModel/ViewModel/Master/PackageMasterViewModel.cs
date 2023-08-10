using ParulBeautyCareDbClasses.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParulBeautyCareViewModel.ViewModel.Master
{
   public  class PackageMasterViewModel
    {
        public string PackageName { get; set; }
        public string IsActive { get; set; }
        public string CompanyCode { get; set; }
        public int PackageId { get; set; }
        public string message { get; set; }
        public string Action { get; set; }
        public string result { get; set; }
        public string success { get; set; }
        public List<PBPackageMasterRtr_Result> PackageList { get; set; }
    }
}
