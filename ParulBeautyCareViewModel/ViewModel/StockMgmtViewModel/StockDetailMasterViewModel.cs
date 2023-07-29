using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParulBeautyCareDbClasses.DataModels;

namespace ParulBeautyCareViewModel.ViewModel.StockMgmtViewModel
{
    public class StockDetailMasterViewModel
    {
        public string StockDetailId { get; set; }

        [Required(ErrorMessage = "*")]
        public string ProductId { get; set; }

        [Required(ErrorMessage = "*")]
        public string Staffid { get; set; }
        public string StockAllocated { get; set; }
        public string StockUsed { get; set; }

        [Required(ErrorMessage = "*")]
        public string Barcode { get; set; }
        public string CreateDate { get; set; }
        public string CreateUser { get; set; }
        public string UpdateDate { get; set; }
        public string UpdateUser { get; set; }
        public string NoOfPerson { get; set; }
        public string PersonUsed { get; set; }
        public string StockAllocationId { get; set; }
        public string Action { get; set; }
        public string result { get; set; }
        public string success { get; set; }
        public string message { get; set; }
    }
}
