using System;
using ParulBeautyCareDbClasses.DataModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParulBeautyCareViewModel.ViewModel.StockMgmtViewModel
{
    public class ProductTypeMasterViewModel
    {
        public string ProductTypeId { get; set; }

        [Required(ErrorMessage = "*")]
        public string ProductTName { get; set; }
        public string IsActive { get; set; }
        public string CompanyCode { get; set; }
        public string CreateDate { get; set; }
        public string CreateUser { get; set; }
        public string UpdateDate { get; set; }
        public string UpdateUser { get; set; }
        public List<PBProductTypeMasterRetrieve_Result> ProductTypeMasterList { get; set; }
        public string Action { get; set; }
        public string PageName { get; set; }
        public string success { get; set; }
        public string message { get; set; }
        public string result { get; set; }

    }
}
