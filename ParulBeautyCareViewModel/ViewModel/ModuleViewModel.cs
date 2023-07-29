﻿using ParulBeautyCareDbClasses.DataModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParulBeautyCareViewModel.ViewModel
{
    public class ModuleViewModel
    {
        public string Action { get; set; }
        public string ModuleId { get; set; }
        [StringLength(50, ErrorMessage = "The Module Name cannot exceed 50 characters.")]
        [Required(ErrorMessage = "*")]
        public string ModuleName { get; set; }
        public string FaIcon { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Only Greater > 0 number allowed.")]
        [Required(ErrorMessage = "*")]
        public string ModulePriority { get; set; }
        public string IsActive { get; set; }
        public string CreateUser { get; set; }  
        public string CreateDate { get; set; }
        public string UpdateDate { get; set; }
        public bool selfPage { get; set; }
        public string UpdateUser { get; set; }
        public List<PBModuleMasterRtr_Result> ModuleViewList { get; set; }
        public List<MenuRightsViewModel> MenuRightsList { get; set; }

        public string success { get; set; }
        public string message { get; set; }
        public string result { get; set; }

    }
}
