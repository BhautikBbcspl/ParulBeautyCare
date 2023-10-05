using ParulBeautyCareDbClasses.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParulBeautyCareViewModel.ViewModel.StaffMgmtViewModel
{
    public class StaffAppointmentsViewModel
    {
        public string StaffCode { get; set; }
        public string TodayDate { get; set; }
        public string Action { get; set; }
        public string CompanyCode { get; set; }
        public List<PBStaffDashboardAppointmentsRtr_Result> StaffAppointmentsList { get; set; }
        public string success { get; set; }
        public string message { get; set; }
        public string result { get; set; }
    }
    public class StaffDashboardServicesViewModel
    {
        public string StaffCode { get; set; }
        public string TodayDate { get; set; }
        public string Action { get; set; }
        public string CompanyCode { get; set; }
        public List<PBStaffDashboardTodayServicesRtr_Result> StaffDashboardTodayServicesList { get; set; }
        public string success { get; set; }
        public string message { get; set; }
        public string result { get; set; }
    }
    public class StaffDashboardViewModel
    {
        public string TodayWorks { get; set; }
        public List<PBStaffDashboardAppointmentsRtr_Result> StaffAppointmentsList { get; set; }
        public List<PBStaffDashboardAppointmentsRtr_Result> StaffCompletedAppointmentsList { get; set; }
    }
}
