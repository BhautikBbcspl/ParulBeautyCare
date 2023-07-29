using System;
using ParulBeautyCareDbClasses.DataModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParulBeautyCareViewModel.ViewModel.Master
{
    public class CategoryMasterViewModel
    {
        public string CategoryId { get; set; }

        [Required(ErrorMessage = "*")]
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public string IsActive { get; set; }
        public string CompanyCode { get; set; }
        public string CreateDate { get; set; }
        public string CreateUser { get; set; }
        public string UpdateDate { get; set; }
        public string UpdateUser { get; set; }
        public List<PBCategoryMasterRetrieve_Result> CategoryMasterList { get; set; }
        public string Action { get; set; }
        public string PageName { get; set; }
        public string success { get; set; }
        public string message { get; set; }
        public string result { get; set; }
    }
}
