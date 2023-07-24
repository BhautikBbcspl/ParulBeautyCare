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
        [Required(ErrorMessage = "Please enter Name.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please enter PhoneNo.")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Invalid PhoneNumber number.")]
        public string ContactNo { get; set; }
       
        [Required(ErrorMessage = "Please enter Address.")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Please enter City.")]
        public string FunctionDate { get; set; }
        public string BookingAmount { get; set; }
        public string Action { get; set; }

        public string Time { get; set; }
        public string[] SelectedServices { get; set; }
    }
}