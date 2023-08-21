using ParulBeautyCareDbClasses.DataModels;
using ParulBeautyCareViewModel.ViewModel.BookingMgmtViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ParulBeautyCareViewModel.ViewModel
{
    public class BookAppointmentViewModel
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public string Date { get; set; }
        [Required(ErrorMessage = "Please enter Customer Name.")]
        public string CustomerName { get; set; }
        [Required(ErrorMessage = "Please enter ContactNo.")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Invalid Contact number.")]
        public string ContactNo { get; set; }
       
        [Required(ErrorMessage = "Please enter Address.")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Please enter City.")]
        public string FunctionDate { get; set; }
        public string BookingAmount { get; set; }
        public string CompanyCode { get; set; }
        public string ReadyTime { get; set; }
        public string AppointmentType { get; set; }
        public string Action { get; set; }
        public decimal DepositAmount { get; set; }
        public int CategoryId { get; set;}
        public int PackageId { get; set;}
        public int SubCategoryId { get; set;}
        public string AppointmentDateTime { get; set; }
        public string[] SelectedServices { get; set; }
        public List<BookCategoryViewModel> CategoryList { get; set; }
        public List<BookSubCategoryViewModel> SubCategoryList { get; set; }
        public List<PBPackageMasterRtr_Result> PackageList { get; set; }
        public List<BookAppointmentDetailViewModel> BookAppointmentTable { get; set; }

    }
    public class BookSubCategoryViewModel
    {
        public string SubcategoryId { get; set; }
        public string SubcategoryName { get; set; }
        public string Amount { get; set; }
    }
    public class BookCategoryViewModel
    {
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
    public class BookAppointmentDetailViewModel
    {
        public string IntePackageServiceId { get; set; }
        public string CategoryId { get; set; }
        public string SubcategoryId { get; set; }
        public string PackageId { get; set; }
        public string AppointmentDateTime { get; set; }
        public string DayInterval { get; set; }
        public string ServiceId { get; set; }
        public string SubCategoryName { get; set; }
        public string CategoryName { get; set; }

    }
}