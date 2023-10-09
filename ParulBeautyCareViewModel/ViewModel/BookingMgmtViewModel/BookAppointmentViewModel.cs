using ParulBeautyCareDbClasses.DataModels;
using ParulBeautyCareViewModel.ViewModel.Master;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ParulBeautyCareViewModel.ViewModel
{
    public class BookAppointmentViewModel
    {
        public int BookingId { get; set; }
        public int BookingServiceId { get; set; }
        public int count { get; set; }
        public int CustomerId { get; set; }
        public string Number { get; set; }
        public string Date { get; set; }
        [Required(ErrorMessage = "*")]
        public string CustomerName { get; set; }
        [Required(ErrorMessage = "*")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Invalid Contact.")]
        public string ContactNo { get; set; }

        [Required(ErrorMessage = "*")]
        public string Address { get; set; }
        [Required(ErrorMessage = "*")]
        public string FunctionDate { get; set; }
        public string BookingAmount { get; set; }
        public string Amount { get; set; }
        public string CompanyCode { get; set; }
        public string CreateDate { get; set; }
        public string BookDate { get; set; }
        [Required(ErrorMessage = "*")]
        public string DepartmentId { get; set; }
        public string CreateUser { get; set; }
        public string UpdateDate { get; set; }
        public string UpdateUser { get; set; }
        public string success { get; set; }
        public string Message { get; set; }
        [Required(ErrorMessage = "*")]
        public string ReadyTime { get; set; }
        [Required(ErrorMessage = "*")]
        public string AppointmentType { get; set; }
        public string Action { get; set; }
        public string Result { get; set; }
        public string Remark { get; set; }
        [Required(ErrorMessage = "*")]
        public string DepositAmount { get; set; }
        [Required(ErrorMessage = "*")]

        public int TimeSlotId { get; set; }
        public int CategoryId { get; set; }
        public int? NoOfSitting { get; set; }
        public int? DayInterval { get; set; }
        public int PackageId { get; set; }
        public bool IsEMI { get; set; }
        public int IntePackageServiceId { get; set; }
        public int SubCategoryId { get; set; }
        public int AdvanceBookingId { get; set; }
        //public string AdvanceName { get; set; }
        //public string AdvanceContact { get; set; }
        //public string AdvanceAddress { get; set; }
        public string AppointmentDateTime { get; set; }
        public string[] SelectedServices { get; set; }
        public List<BookCategoryViewModel> CategoryList { get; set; }
        public List<BookSubCategoryViewModel> SubCategoryList { get; set; }
        public List<BookPackageViewModel> PackageList { get; set; }
        public List<BookAppointmentDetailViewModel> BookAppointmentTable { get; set; }
        public List<BookAppointmentDetailViewModel> BookServicesTable { get; set; }
        public List<PBDepartmentMasterRetrieve_Result> DeptList { get; set; }
        public List<PBTimeSlotMasterRetrieve_Result> TimeSlotMasterList { get; set; }
        public System.Data.DataTable mytable { get; set; }
        public System.Data.DataTable mytable1 { get; set; }
    }
    public class BookSubCategoryViewModel
    {
        public string SubCategoryId { get; set; }
        public string SubCategoryName { get; set; }
        public string Amount { get; set; }
    }
    public class BookCategoryViewModel
    {
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
    public class BookPackageViewModel
    {
        public string PackageId { get; set; }
        public string PackageName { get; set; }
    }
    public class BookAppointmentDetailViewModel
    {
        public string IntePackageServiceId { get; set; }
        public string CategoryId { get; set; }
        public string PackageId { get; set; }
        public string count { get; set; }
        public int BookAppID { get; set; }
        public int BookServiceID { get; set; }
        [Required(ErrorMessage = "*")]
        public string TimeSlotId { get; set; }
        public string AppointmentDateTime { get; set; }
        public string NoOfSitting { get; set; }
        public string DayInterval { get; set; }
        public string DoneBy { get; set; }
        public string DoneDate { get; set; }
        public string Amount { get; set; }
        public string DeleteStatus { get; set; }
        public string Discount { get; set; }
        public string Remark { get; set; }
        public string FinalAmount { get; set; }
        public string CustomerName { get; set; }
        public string SubCategoryId { get; set; }
        public string SubCategoryName { get; set; }
        public string CategoryName { get; set; }
        public List<PBTimeSlotMasterRetrieve_Result> TimeSlotMasterList { get; set; }

    }
}