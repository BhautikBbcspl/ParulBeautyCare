using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParulBeautyCareDbClasses.DataModels;

namespace ParulBeautyCareViewModel.ViewModel.StockMgmtViewModel
{
    public class StockPurchaseViewModel
    {
        public string PurchaseId { get; set; }
        [Required(ErrorMessage = "*")]
        public string ProductId { get; set; }
        [Required(ErrorMessage = "*")]
        public string DepartmentId { get; set; }
        public string ProductTypeId { get; set; }

        [Required(ErrorMessage = "*")]
        public string Quantity { get; set; }

        [Required(ErrorMessage = "*")]
        public string MfgDate { get; set; }

        [Required(ErrorMessage = "*")]
        public string ExpDate { get; set; }

        [Required(ErrorMessage = "*")]
        public string PurchaseDate { get; set; }

        [Required(ErrorMessage = "*")]
        public string VendorId { get; set; }
        [Required(ErrorMessage = "*")]
        public string GSTId { get; set; }
        public string VendorName { get; set; }
        public string CompanyCode { get; set; }
        public string CreateDate { get; set; }
        public string CreateUser { get; set; }
        public string UpdateDate { get; set; }
        public string UpdateUser { get; set; }
        public string Action { get; set; }
        public string result { get; set; }
        public string success { get; set; }
        public string message { get; set; }
        public List<PBProductMasterRtr_Result> ProductList { get; set; }
        public List<PBStockPurchaseRetrieve_Result> StockPurchaseList { get; set; }
        public List<PBDepartmentMasterRetrieve_Result> DeptList { get; set; }
        public List<PBVendorMasterRetrieve_Result> VendorList { get; set; }
        public List<PBGSTMasterRetrieve_Result> GSTMasterList { get; set; }
        [Required(ErrorMessage = "*")]
        public string Price { get; set; }
    }
}
