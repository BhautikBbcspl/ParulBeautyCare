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
    
    public partial class PBBookingDetailRtr_Result
    {
        public int BookingDetailId { get; set; }
        public Nullable<int> BookingId { get; set; }
        public Nullable<int> CategoryId { get; set; }
        public string CategoryName { get; set; }
        public Nullable<int> SubCategoryId { get; set; }
        public string SubCategoryName { get; set; }
        public Nullable<int> AllocatedTo { get; set; }
        public string StaffCode { get; set; }
        public string StaffName { get; set; }
        public Nullable<System.DateTime> AllocationDate { get; set; }
        public string AppointmentDate { get; set; }
        public Nullable<int> AppointmentTime { get; set; }
        public string SlotName { get; set; }
        public Nullable<int> DoneBy { get; set; }
        public Nullable<System.DateTime> DoneDate { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public Nullable<decimal> Discount { get; set; }
        public Nullable<decimal> FinalAmount { get; set; }
        public Nullable<int> Status { get; set; }
        public string UpdateDate { get; set; }
        public string UpdateUser { get; set; }
        public string CustomerName { get; set; }
        public string CreateDate { get; set; }
        public string CreateUser { get; set; }
    }
}