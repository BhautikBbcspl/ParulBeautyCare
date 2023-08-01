using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParulBeautyCareDbClasses.DataModels;

namespace ParulBeautyCareViewModel.ViewModel.StockMgmtViewModel
{
    public class ProductMasterViewModel
    {
        public string ProductId { get; set; }

        [Required(ErrorMessage = "*")]
        public string ProductCode { get; set; }

        [Required(ErrorMessage = "*")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "*")]  
        public string NumberOfPerson { get; set; }

        [Required(ErrorMessage = "*")]
        public string ProductTypeId { get; set; }
        public string IsActive { get; set; }
        public string CompanyCode { get; set; }
        public string CreateDate { get; set; }
        public string CreateUser { get; set; }
        public string UpdateDate { get; set; }
        public string UpdateUser { get; set; }
        public List<PBProductTypeMasterRetrieve_Result> ProductTypeMasterList { get; set; }
        public List<PBProductMasterRtr_Result> ProductMasterList { get; set; }
        public string Action { get; set; }
        public string PageName { get; set; }
        public string success { get; set; }
        public string message { get; set; }
        public string result { get; set; }
    }
}
