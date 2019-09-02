//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MerkDataBaseBusinessLogicProject
{
    using System;
    
    public partial class GetInventoryItemCostsDetails_Result
    {
        public int ItemID { get; set; }
        public string ItemName { get; set; }
        public string ItemInternalCode { get; set; }
        public Nullable<double> ItemWidth { get; set; }
        public Nullable<double> ItemHeight { get; set; }
        public Nullable<double> ItemDepth { get; set; }
        public int PrintingID { get; set; }
        public System.DateTime PrintingDate { get; set; }
        public double PrintingMinutes { get; set; }
        public double PrintingUnitCostFactor { get; set; }
        public double PrintingCalculatedCost { get; set; }
        public Nullable<double> PrintingAddedMinutes { get; set; }
        public Nullable<double> PrintingTotalCalculatedCost { get; set; }
        public bool PrintingUseRealCost { get; set; }
        public Nullable<double> PrintingReadCost { get; set; }
        public Nullable<double> TotalPartsArea { get; set; }
        public Nullable<double> TotalPartsCount { get; set; }
        public int RawID { get; set; }
        public string RawName { get; set; }
        public string ColorName { get; set; }
        public double RawThickness { get; set; }
        public Nullable<double> RawUnitPriceInPounds { get; set; }
        public Nullable<double> PrintingUnitCost { get; set; }
        public Nullable<double> TotalUnitCost { get; set; }
        public Nullable<double> NetCostPrice { get; set; }
        public Nullable<int> AdditionalCost { get; set; }
        public Nullable<double> AdditionalNetCostPrice { get; set; }
    }
}