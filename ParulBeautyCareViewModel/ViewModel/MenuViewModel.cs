using ParulBeautyCareDbClasses.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParulBeautyCareViewModel.ViewModel
{
    public class MenuViewModel
    {
        public string success { get; set; }
        public string message { get; set; }
        public string result { get; set; }
        public int RoleId { get; set; }
        public List<PBMenuRtr_Result> MenuList { get; set; }
    }
}
