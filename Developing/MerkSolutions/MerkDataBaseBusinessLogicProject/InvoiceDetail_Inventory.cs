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
    using System.Collections.Generic;
    
    public partial class InvoiceDetail_Inventory
    {
        public int ID { get; set; }
        public int InvoiceDetailID { get; set; }
        public int InventoryHousing_CU_ID { get; set; }
        public int InventoryItem_CU_ID { get; set; }
        public Nullable<double> Count { get; set; }
        public double PatientShare { get; set; }
        public double InsuranceShare { get; set; }
        public double PatientShareDiscount { get; set; }
        public bool IsInsuranceApplied { get; set; }
        public Nullable<int> DiscountType_P_ID { get; set; }
        public bool IsOnDuty { get; set; }
        public bool IsSurchargeApplied { get; set; }
        public Nullable<double> SurchargeAmount { get; set; }
        public string Description { get; set; }
        public Nullable<int> InsertedBy { get; set; }
    
        public virtual DiscountType_p DiscountType_p { get; set; }
        public virtual InventoryHousing_cu InventoryHousing_cu { get; set; }
        public virtual InventoryItem_cu InventoryItem_cu { get; set; }
        public virtual InvoiceDetail InvoiceDetail { get; set; }
        public virtual User_cu User_cu { get; set; }
    }
}
