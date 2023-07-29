using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParulBeautyCareDbClasses.DataModels;

namespace ParulBeautyCareViewModel.ViewModel.StockMgmtViewModel
{
    public class StockAllocationMasterViewModel
    {
        public string StockAllocationId { get; set; }

        [Required(ErrorMessage = "*")]
        public string ProductId { get; set; }

        [Required(ErrorMessage = "*")]
        public int Qty { get; set; }

        public string AllocationDate { get; set; }

        public string AllocateUser { get; set; }


        [Required(ErrorMessage = "*")]

        public string StaffId { get; set; }


        [Required(ErrorMessage = "*")]
        public string UpdateDate { get; set; }


        [Required(ErrorMessage = "*")]
        public string UpdateUser { get; set; }

        public string CompanyCode { get; set; }
        public string Action { get; set; }
        public string result { get; set; }
        public string success { get; set; }
        public string message { get; set; }

        public List<PBStaffMasterRtr_Result> StaffList { get; set; }
        public List<PBProductMasterRtr_Result> ProductList { get; set; }
        public List<PBStockAllocationRetrieve_Result> StockAllocationList { get; set; }
    }
}
