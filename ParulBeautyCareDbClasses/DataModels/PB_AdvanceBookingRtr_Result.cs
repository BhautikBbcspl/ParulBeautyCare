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
    
    public partial class PB_AdvanceBookingRtr_Result
    {
        public int AdvanceBookingId { get; set; }
        public string Name { get; set; }
        public string ContactNo { get; set; }
        public string Address { get; set; }
        public string CompanyCode { get; set; }
        public Nullable<System.DateTime> createdate { get; set; }
        public string createuser { get; set; }
        public Nullable<bool> status { get; set; }
        public Nullable<decimal> totaldeposit { get; set; }
        public Nullable<int> NumOfSiders { get; set; }
        public Nullable<decimal> SidersDeposit { get; set; }
        public Nullable<decimal> BridalDeposit { get; set; }
        public string ServicePersonName { get; set; }
        public string ServicePersonContact { get; set; }
        public string SpecialPersonAddress { get; set; }
        public Nullable<System.DateTime> FunctionDate { get; set; }
        public string fdate { get; set; }
        public string AfterRemark { get; set; }
        public string BeforeRemark { get; set; }
    }
}
