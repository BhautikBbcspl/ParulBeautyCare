using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParulBeautyCareDbClasses.DataModels;

namespace ParulBeautyCareViewModel.ViewModel.StockMgmtViewModel
{
    public class StockTransferDetalViewModel
    {
        public string StockDetailId { get; set; }

        public string StockHeaderId { get; set; }
        public string ProductId { get; set; }
        public string Qty { get; set; }
        public string TotalPerson { get; set; }
        public string PersonAvailable { get; set; }
        public string CreateDate { get; set; }
        public string CreateUser { get; set; }
        public string UpdateDate { get; set; }
        public string UpdateUser { get; set; }

        public string CompanyCode { get; set; }
        public string Action { get; set; }
        public string result { get; set; }
        public string success { get; set; }
        public string message { get; set; }


    }
}