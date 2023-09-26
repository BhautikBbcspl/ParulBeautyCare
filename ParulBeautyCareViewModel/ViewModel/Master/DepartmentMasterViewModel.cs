using System;
using ParulBeautyCareDbClasses.DataModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParulBeautyCareViewModel.ViewModel.Master
{
    public class DepartmentMasterViewModel
    {
        public string DepartmentId { get; set; }

        [Required(ErrorMessage = "*")]
        public string DepartmentName { get; set; }
        public string DeptAbrv { get; set; }
        public string GSTNo { get; set; }
        public string DeptAddress { get; set; }
        public string IsActive { get; set; }
        public string CompanyCode { get; set; }
        public string CreateDate { get; set; }
        public string CreateUser { get; set; }
        public string UpdateDate { get; set; }
        public string UpdateUser { get; set; }
        public string Action { get; set; }
        public string success { get; set; }
        public string message { get; set; }
        public string result { get; set; }

        public List<PBDepartmentMasterRetrieve_Result> DepartmentList { get; set; }
    }
}
