using ParulBeautyCareDbClasses.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParulBeautyCareViewModel.ViewModel.Master
{
    public class GSTMasterViewModel
    {
        public string GSTId { get; set; }
        public string GSTName { get; set; }
        public string GSTPerc { get; set; }
        public string IsActive { get; set; }
        public string CreateDate { get; set; }
        public string CreateUser { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public List<PBGSTMasterRetrieve_Result> GSTMasterList { get; set; }
        public string Action { get; set; }
        public string success { get; set; }
        public string message { get; set; }
        public string result { get; set; }
        public string CompanyCode { get; set; }
        public string updatedate { get; set; }
        public string UpdateUser { get; set; }
    }
}
