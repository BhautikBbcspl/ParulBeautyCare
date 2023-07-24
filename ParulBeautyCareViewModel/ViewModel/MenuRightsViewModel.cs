using ParulBeautyCareDbClasses.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParulBeautyCareViewModel.ViewModel
{
    public class MenuRightsViewModel
    {
        public string ViewRight { get; set; }
        public string InsertRight { get; set; }
        public string UpdateRight { get; set; }
        public string DeleteRight { get; set; }
        public int RoleID { get; set; }
        public string Usercode { get; set; }
        public string PageName { get; set; }
        public List<PBMenuRightsRtr_Result> MenuRightsList { get; set; }
        public string success { get; set; }
        public string message { get; set; }
        public string result { get; set; }

    }
}
