using ParulBeautyCareDbClasses.DataModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParulBeautyCareViewModel.ViewModel
{
    public class PageModuleViewModel
    {
        public string InteId { get; set; }
        [Required(ErrorMessage = "*")]
        public string ModuleId { get; set; }
        [Required(ErrorMessage = "*")]
        public string PageId { get; set; }
        public string CreateDate { get; set; }
        public string CreateUser { get; set; }
        public string IsActive { get; set; }
        public string ModuleName { get; set; }
        public string PageName { get; set; }
        public string Action { get; set; }
        public string UpdateDate { get; set; }
        public string UpdateUser { get; set; }
        public List<PBModuleMasterRtr_Result> ModulesList { get; set; }
        public List<PBPageMasterRtr_Result> PagesList { get; set; }
        public List<PBIntePageModuleRtr_Result> PageModuleInteList { get; set; }
        public string success { get; set; }
        public string message { get; set; }
        public string result { get; set; }
    }
}
