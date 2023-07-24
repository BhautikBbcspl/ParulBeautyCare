using ParulBeautyCareDbClasses.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParulBeautyCareViewModel.ViewModel
{
    public class LoginViewModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string CompanyCode { get; set; }
        public int RoleId { get; set; }
        public string message { get; set; }
        public string result { get; set; }
        public string success { get; set; }
        public List<PBLoginRtr_Result> LoginUserList { get; set; }

    }
}
