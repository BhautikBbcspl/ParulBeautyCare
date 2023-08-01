using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParulBeautyCareDbClasses.DataModels;

namespace ParulBeautyCareViewModel.ViewModel.StockMgmtViewModel
{
    public class AvailableStockViewModel
    {
        public string StockDetailId { get; set; }

        public string ProductId { get; set; }
        public string availstock { get; set; }
        public string AutoSrNo { get; set; }
        public string Barcode { get; set; }
        public string NoOfPerson { get; set; }
        public string availperson { get; set; }
        public string StockAllocationId { get; set; }
       

        public string CompanyCode { get; set; }
        public string Action { get; set; }
        public string result { get; set; }
        public string success { get; set; }
        public string message { get; set; }


    }
}