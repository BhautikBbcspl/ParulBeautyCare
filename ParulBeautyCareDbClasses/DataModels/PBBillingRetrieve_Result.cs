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
    
    public partial class PBBillingRetrieve_Result
    {
        public int BillId { get; set; }
        public Nullable<int> BookingId { get; set; }
        public string BookingCode { get; set; }
        public Nullable<int> DepartmentId { get; set; }
        public string BillCode { get; set; }
        public Nullable<int> CustomerId { get; set; }
        public string ContactNo { get; set; }
        public string CustomerName { get; set; }
        public string Address { get; set; }
        public string BillDate { get; set; }
        public string CreateDate { get; set; }
        public string CreateUser { get; set; }
        public string UpdateDate { get; set; }
        public string UpdateUser { get; set; }
        public Nullable<decimal> BaseAmount { get; set; }
        public Nullable<decimal> DiscountPerc { get; set; }
        public string Discount { get; set; }
        public Nullable<decimal> GSTPerc { get; set; }
        public Nullable<decimal> GSTAmount { get; set; }
        public Nullable<decimal> FinalAmount { get; set; }
        public Nullable<decimal> PaidAmount { get; set; }
        public string CompanyCode { get; set; }
    }
}