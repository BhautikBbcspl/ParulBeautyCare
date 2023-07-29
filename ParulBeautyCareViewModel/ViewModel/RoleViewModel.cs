using ParulBeautyCareDbClasses.DataModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParulBeautyCareViewModel.ViewModel
{
    public class RoleViewModel
    {
        public string Action { get; set; }
        public string RoleId { get; set; }
        [StringLength(50, ErrorMessage = "The Role Name cannot exceed 50 characters.")]
        [Required(ErrorMessage = "*")]
        public string RoleName { get; set; }      
        public string IsActive { get; set; }
        public string CreateUser { get; set; }  
        public string CreateDate { get; set; }
        public string UpdateDate { get; set; }
        public string UpdateUser { get; set; }
        public List<PBRoleMasterRetrieve_Result> RoleViewList { get; set; }
        public string success { get; set; }
        public string message { get; set; }
        public string result { get; set; }
        public string CompanyCode { get; set; }

    }
}
