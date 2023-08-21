using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParulBeautyCareViewModel.ViewModel
{
    public class EnquiryViewModel
    {
        public int EnqId { get; set; }

        //[Required(ErrorMessage = "*")]
        //public string PersonName { get; set; }

        //[Required(ErrorMessage = "*")]
        //public string PersonContactNo { get; set; }
        public string PersonName { get; set; }
        public string PersonContactNo { get; set; }
        public string PersonEmailId { get; set; }
        public string PersonAddress { get; set; }
        public string PersonEnquiryFor { get; set; }
        public string CreateDate { get; set; }
        public string CreateUser { get; set; }

        public string Action { get; set; }

        public string success { get; set; }
        public string message { get; set; }
        public string result { get; set; }
    }
}
