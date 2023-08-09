﻿using ParulBeautyCareDbClasses.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParulBeautyCareViewModel.ViewModel.BookingMgmtViewModel
{
    public class BookingDetailViewModel
    {
        public string BookingDetailId { get; set; }
        public string BookingId { get; set; }
        public string CategoryId { get; set; }
        public string SubCategoryId { get; set; }
        public string AllocatedTo { get; set; }
        public string AppointmentDate { get; set; }
        public string AppointmentTime { get; set; }
        public string DoneBy { get; set; }
        public string DoneDate { get; set; }
        public string Amount { get; set; }
        public string Discount { get; set; }
        public string FinalAmount { get; set; }
        public string Status { get; set; }
        public string CreateDate { get; set; }
        public string CreateUser { get; set; }
        public string UpdateDate { get; set; }
        public string UpdateUser { get; set; }
        public string CustomerName { get; set; }
        public List<PBBookingDetailRtr_Result> BookingDetailList { get; set; }
        public string success { get; set; }
        public string message { get; set; }
        public string Action { get; set; }
        public string CompanyCode { get; set; }
    }
}
