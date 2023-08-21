using ParulBeautyCareDbClasses.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParulBeautyCareViewModel.ViewModel.Master
{
    public class IntePackageServiceMasterViewModel
    {
        public string IntePackageServiceId { get; set; }
        public string PackageId { get; set; }
        public string ProductId { get; set; }
        public string ServiceId { get; set; }
        public string PackageName { get; set; }
        public string ProductName { get; set; }
        public string SubCategoryName { get; set; }
        public string IsActive { get; set; }
        public string CompanyCode { get; set; }
        public string CreateDate { get; set; }
        public string CreateUser { get; set; }
        public string UpdateDate { get; set; }
        public string UpdateUser { get; set; }
        public string Action { get; set; }
        public string success { get; set; }
        public string message { get; set; }
        public string result { get; set; }
        public List<PBIntePackageServiceMasterRetrieve_Result> IntePackageServiceMasterList { get; set; }
        public List<PBPackageMasterRtr_Result> PackageMasterList { get; set; }
        public List<PBSubCategoryMasterRetrieve_Result> SubCategoryMasterList { get; set; }
        public List<PBProductMasterRtr_Result> ProductMasterList { get; set; }
    }
}
