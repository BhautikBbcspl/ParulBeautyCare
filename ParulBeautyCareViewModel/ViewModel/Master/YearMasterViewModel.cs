using ParulBeautyCareDbClasses.DataModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParulBeautyCareViewModel.ViewModel.Master
{
    public class YearMasterViewModel
    {
        public string Action { get; set; }
        public string YearId { get; set; }
        [Required(ErrorMessage = "*")]
        public string FromDate { get; set; }
        [Required(ErrorMessage = "*")]
        public string ToDate { get; set; }
        [Required(ErrorMessage = "*")]
        public string Yearperiod { get; set; }
        public string IsActive { get; set; }
        public string CreateUser { get; set; }
        public string CreateDate { get; set; }
        public string UpdateDate { get; set; }
        public string UpdateUser { get; set; }
        public List<PBYearMasterRetrieve_Result> YearMasterList { get; set; }
        public string success { get; set; }
        public string message { get; set; }
        public string result { get; set; }
        public string CompanyCode { get; set; }
    }
}
