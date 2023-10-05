using ParulBeautyCareDbClasses.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParulBeautyCareViewModel.ViewModel
{
    public class AppointmentsViewModel
    {
        public string TodayDate { get; set; }
        public string Action { get; set; }
        public string CompanyCode { get; set; }
        public List<PBDashboardAppointmentsRtr_Result> AppointmentsList { get; set; }
        public string success { get; set; }
        public string message { get; set; }
        public string result { get; set; }
    }
    public class StaffServicesViewModel
    {
        public string TodayDate { get; set; }
        public string Action { get; set; }
        public string CompanyCode { get; set; }
        public List<PBDashboardStaffTodayServicesRtr_Result> StaffTodayServicesList { get; set; }
        public string success { get; set; }
        public string message { get; set; }
        public string result { get; set; }
    }
    public class DashboardCountViewModel
    {
        public string TodayDate { get; set; }
        public string Action { get; set; }
        public string CompanyCode { get; set; }
        public List<PBDashboardCountRtr_Result> DashboardCountList { get; set; }
        public string success { get; set; }
        public string message { get; set; }
        public string result { get; set; }
    }
    public class DashboardWeeklyChartViewModel
    {
        public string Action { get; set; }
        public string CompanyCode { get; set; }
        public List<PBDashboardWeeklyAppointmentChart_Result> DashboardWeeklyAppointmentChartList { get; set; }
        public string success { get; set; }
        public string message { get; set; }
        public string result { get; set; }
    }
    public class DashboardViewModel
    {
        public string TotalCustomers { get; set; }
        public string TotalStaff { get; set; }
        public string TodaysBookings { get; set; }
        public string TodayAppointments { get; set; }
        public List<PBDashboardWeeklyAppointmentChart_Result> DashboardPreviousWeekAppointmentChartList { get; set; }
        public List<PBDashboardWeeklyAppointmentChart_Result> DashboardCurrentWeekAppointmentChartList { get; set; }
        public List<PBDashboardStaffTodayServicesRtr_Result> StaffTodayServicesList { get; set; }
        public List<PBDashboardAppointmentsRtr_Result> AppointmentsList { get; set; }
        public List<PBDashboardAppointmentsRtr_Result> CompletedAppointmentsList { get; set; }
    }
}
