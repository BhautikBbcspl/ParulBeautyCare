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
    
    public partial class PBBookingServicesRtr_Result
    {
        public int BookingServiceId { get; set; }
        public Nullable<int> BookingId { get; set; }
        public string BookingNo { get; set; }
        public Nullable<int> CategoryId { get; set; }
        public string CategoryName { get; set; }
        public Nullable<int> SubCategoryId { get; set; }
        public string SubCategoryName { get; set; }
        public Nullable<int> IntePackageServiceId { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public string UpdateDate { get; set; }
        public string UpdateUser { get; set; }
        public string CreateDate { get; set; }
        public string CreateUser { get; set; }
        public string Remark { get; set; }
        public string CompanyCode { get; set; }
    }
}
