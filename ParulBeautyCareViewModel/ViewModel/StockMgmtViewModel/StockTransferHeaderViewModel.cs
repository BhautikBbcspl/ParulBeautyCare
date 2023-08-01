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
        public List<PBAvailableStockRtr_Result> AvailableStockList { get; set; }
        public List<StockTransferTypeViewModel> StockTransferTypeTable { get; set; }

        //public DataTable StockTransferTypeDataTable
        //{
        //    get
        //    {
        //        DataTable dt = new DataTable();
        //        dt.Columns.AddRange(
        //            new DataColumn[8] {
        //            new DataColumn("StockDetailId", typeof(string)),
        //            new DataColumn("StockHeaderId", typeof(string)),
        //            new DataColumn("ProductId", typeof(string)),
        //            new DataColumn("Qty", typeof(string)),
        //            new DataColumn("TotalPerson", typeof(string)),
        //            new DataColumn("PersonAvailable", typeof(string)),
        //            new DataColumn("AutoSrNo", typeof(string)),
        //            new DataColumn("Barcode", typeof(string))
        //            });

        //        if (StockTransferTypeTable != null)
        //        {
        //            foreach (var item in StockTransferTypeTable)
        //            {
        //                dt.Rows.Add(
        //                    item.StockDetailId,
        //                    item.StockHeaderId,
        //                    item.ProductId,
        //                    item.Qty,
        //                    item.TotalPerson,
        //                    item.PersonAvailable,
        //                    item.AutoSrNo,
        //                    item.Barcode
        //                );
        //            }
        //        }

        //        return dt;
        //    }
        //}
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
    }
}