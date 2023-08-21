using ParulBeautyCareDbClasses.DataModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParulBeautyCareViewModel.ViewModel.Master
{
   public  class PackageMasterViewModel
    {
        [Required(ErrorMessage = "*")]
        public string PackageName { get; set; }
        public string IsActive { get; set; }
        public string CompanyCode { get; set; }
        public string PackageId { get; set; }
        [Required(ErrorMessage = "*")]
        public string PackageAmount { get; set; }
        public string CreateDate { get; set; }
        public string CreateUser { get; set; }
        public string UpdateDate { get; set; }
        public string UpdateUser { get; set; }
        public string message { get; set; }
        public string Action { get; set; }
        public string result { get; set; }
        public string success { get; set; }
        public List<PBPackageMasterRtr_Result> PackageList { get; set; }
    }
}
