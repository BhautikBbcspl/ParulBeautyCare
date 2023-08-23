using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParulBeautyCareDbClasses.DataModels;

namespace ParulBeautyCareViewModel.ViewModel.StockMgmtViewModel
{
    public class StockTransferHeaderViewModel
    {
        public string StockHeaderId { get; set; }

        public string TransferDate { get; set; }

        //[Required(ErrorMessage = "*")]
        public string FromStaffId { get; set; }

        //[Required(ErrorMessage = "*")]
        public string ToStaffId { get; set; }
        public string CreateDate { get; set; }
        public string CreateUser { get; set; }
        public string UpdateDate { get; set; }
        public string UpdateUser { get; set; }
        public string CompanyCode { get; set; }
        public string Action { get; set; }
        public string result { get; set; }
        public string success { get; set; }
        public string message { get; set; }

        public List<PBStaffMasterRtr_Result> StaffList { get; set; }
        public int Qty { get; set; }
        public string ProductName { get; set; }
        public List<PBAvailableStockRtr_Result> AvailableStockList { get; set; }
        public List<StockTransferTypeViewModel> StockTransferTypeTable { get; set; }
        public string StaffId { get; set; }
        public string ProductId { get; set; }
        public List<PBAllocatedStockToStaffRtr_Result> StockAllocatedToStaffList { get; set; }
    }

    public class StockTransferTypeViewModel
    {
        public string StockDetailId { get; set; }
        public string StockHeaderId { get; set; }
        public string ProductId { get; set; }
        public string Qty { get; set; }
        public string TotalPerson { get; set; }
        public string PersonAvailable { get; set; }
        public string AutoSrNo { get; set; }
        public string Barcode { get; set; }
        public string ExpDate { get; set; }
        public string Price { get; set; }
    }
}