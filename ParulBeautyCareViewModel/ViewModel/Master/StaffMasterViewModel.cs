using System;
using ParulBeautyCareDbClasses.DataModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParulBeautyCareViewModel.ViewModel.Master
{
    public class StaffMasterViewModel
    {
        public string StaffId { get; set; }

        [Required(ErrorMessage = "*")]
        public string StaffCode { get; set; }

        [Required(ErrorMessage = "*")]
        public string StaffName { get; set; }

        [Required(ErrorMessage = "*")]
       
        public string Contact { get; set; }

        [Required(ErrorMessage = "*")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "*")]
        public string Password { get; set; }

        [Required(ErrorMessage = "*")]
        public string RoleId { get; set; }
        public string IsActive { get; set; }
        public string CompanyCode { get; set; }
        public string CreateDate { get; set; }
        public string CreateUser { get; set; }
        public string UpdateDate { get; set; }
        public string UpdateUser { get; set; }
        public List<PBRoleMasterRetrieve_Result> RoleMasterList { get; set; }
        public List<PBStaffMasterRtr_Result> StaffMasterList { get; set; }
        public string Action { get; set; }
        public string PageName { get; set; }
        public string success { get; set; }
        public string message { get; set; }
        public string result { get; set; }
    }
}
