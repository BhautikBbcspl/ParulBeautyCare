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
    
    public partial class PBAvailableStockRtr_Result
    {
        public int StockDetailId { get; set; }
        public Nullable<int> ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal availstock { get; set; }
        public string AutoSrNo { get; set; }
        public string Barcode { get; set; }
        public int NoOfPerson { get; set; }
        public int availperson { get; set; }
        public Nullable<int> StockAllocationId { get; set; }
        public Nullable<decimal> Price { get; set; }
        public string ExpDate { get; set; }
    }
}
