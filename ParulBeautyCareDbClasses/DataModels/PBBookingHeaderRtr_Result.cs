//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ParulBeautyCareDbClasses.DataModels
{
    using System;
    
    public partial class PBBookingHeaderRtr_Result
    {
        public int BookingId { get; set; }
        public string BookingCode { get; set; }
        public Nullable<int> DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string DeptAbrv { get; set; }
        public string DeptAddress { get; set; }
        public string GSTNo { get; set; }
        public Nullable<int> CustomerId { get; set; }
        public string ContactNo { get; set; }
        public string CustomerName { get; set; }
        public string Address { get; set; }
        public Nullable<System.DateTime> BookDate { get; set; }
        public string bookingdate { get; set; }
        public string BDate { get; set; }
        public Nullable<int> NoOfPerson { get; set; }
        public Nullable<decimal> TotalAmount { get; set; }
        public Nullable<decimal> DiscountPerc { get; set; }
        public Nullable<decimal> Discount { get; set; }
        public Nullable<decimal> FinalAmount { get; set; }
        public Nullable<decimal> AdvanceBookingCharge { get; set; }
        public Nullable<decimal> PaidAmount { get; set; }
        public string CompanyCode { get; set; }
        public Nullable<System.DateTime> FunctionDate { get; set; }
        public string ReadyTime { get; set; }
        public Nullable<int> Status { get; set; }
        public string UpdateDate { get; set; }
        public string UpdateUser { get; set; }
        public string CreateDate { get; set; }
        public string CreateUser { get; set; }
        public string AppointmentType { get; set; }
    }
}
