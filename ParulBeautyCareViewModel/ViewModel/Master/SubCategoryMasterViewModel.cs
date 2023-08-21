using System;
using ParulBeautyCareDbClasses.DataModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParulBeautyCareViewModel.ViewModel.Master
{
    public class SubCategoryMasterViewModel
    {
        public string SubCategoryId { get; set; }

        [Required(ErrorMessage = "*")]
        public string CategoryId { get; set; }

        [Required(ErrorMessage = "*")]
        public string SubCategoryName { get; set; }

        //[Required(ErrorMessage = "*")]
        public string IsMultiPerson { get; set; }
        public string NumberOfPerson { get; set; }

        //[Required(ErrorMessage = "*")]
        public string YearId { get; set; }

        [Required(ErrorMessage = "*")]
        public string NoOfSitting { get; set; }

        public string DayInterval { get; set; }
        public string DayPlaceholder { get; set; }
        public string Incentive { get; set; }
        public string GSTPercentage { get; set; }
        public string IsActive { get; set; }
        public string CompanyCode { get; set; }
        public string CreateDate { get; set; }
        public string CreateUser { get; set; }
        public string UpdateDate { get; set; }
        public string UpdateUser { get; set; }

        [Required(ErrorMessage = "*")]
        public string TimeDuraion { get; set; }

        [Required(ErrorMessage = "*")]
        public string Amount { get; set; }
        public List<PBSubCategoryMasterRetrieve_Result> SubCategoryMasterList { get; set; }
        public List<PBYearMasterRetrieve_Result> YearMasterList { get; set; }
        public List<PBCategoryMasterRetrieve_Result> CategoryMasterList { get; set; }     
        public string Action { get; set; }
        public string PageName { get; set; }
        public string success { get; set; }
        public string message { get; set; }
        public string result { get; set; }
    }
}
