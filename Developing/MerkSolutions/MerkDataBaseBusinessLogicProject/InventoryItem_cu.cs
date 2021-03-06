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
    
    public partial class InventoryItem_cu
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public InventoryItem_cu()
        {
            this.FinanceInvoiceDetails = new HashSet<FinanceInvoiceDetail>();
            this.InventoryItem_Area = new HashSet<InventoryItem_Area>();
            this.InventoryItem_Color_cu = new HashSet<InventoryItem_Color_cu>();
            this.InventoryItemTransactions = new HashSet<InventoryItemTransaction>();
            this.InventoryItem_Printing_cu = new HashSet<InventoryItem_Printing_cu>();
            this.InventoryItem_RawMaterial_cu = new HashSet<InventoryItem_RawMaterial_cu>();
            this.InventoryItem_UnitMeasurment_cu = new HashSet<InventoryItem_UnitMeasurment_cu>();
            this.InventoryItemGroup_InventoryItem_cu = new HashSet<InventoryItemGroup_InventoryItem_cu>();
            this.InventoryItemPrice_cu = new HashSet<InventoryItemPrice_cu>();
            this.InvoiceDetail_Inventory = new HashSet<InvoiceDetail_Inventory>();
        }
    
        public int ID { get; set; }
        public string Name_P { get; set; }
        public string Name_S { get; set; }
        public Nullable<int> InventoryHousing_CU_ID { get; set; }
        public Nullable<int> InventoryItemCategory_CU_ID { get; set; }
        public Nullable<int> InventoryItemBrand_CU_ID { get; set; }
        public Nullable<int> InventoryTrackingUnitMeasurment_CU_ID { get; set; }
        public Nullable<int> InventoryItemType_P_ID { get; set; }
        public string InternalCode { get; set; }
        public string DefaultBarcode { get; set; }
        public Nullable<double> DefaultSellingPrice { get; set; }
        public Nullable<double> DefaultCost { get; set; }
        public Nullable<double> RorderedPoint { get; set; }
        public Nullable<double> StockMinLevel { get; set; }
        public Nullable<double> StockMaxLevel { get; set; }
        public Nullable<bool> AcceptOverrideMinAmount { get; set; }
        public Nullable<bool> CanBeSold { get; set; }
        public Nullable<bool> IsAvailable { get; set; }
        public Nullable<bool> AcceptPartingSelling { get; set; }
        public Nullable<bool> IsCountable { get; set; }
        public Nullable<System.DateTime> SellingStartDate { get; set; }
        public Nullable<System.DateTime> SellingEndDate { get; set; }
        public Nullable<System.DateTime> ExpirationDate { get; set; }
        public Nullable<int> ChartOfAccount_CU_ID { get; set; }
        public bool IsOnDuty { get; set; }
        public string Description { get; set; }
        public Nullable<int> InsertedBy { get; set; }
        public Nullable<bool> IsTaxable { get; set; }
        public Nullable<bool> IsSurcharge { get; set; }
        public Nullable<double> Width { get; set; }
        public Nullable<double> Height { get; set; }
        public Nullable<double> Depth { get; set; }
        public Nullable<int> Count { get; set; }
        public Nullable<int> Color_CU_ID { get; set; }
    
        public virtual ChartOfAccount_cu ChartOfAccount_cu { get; set; }
        public virtual Color_cu Color_cu { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FinanceInvoiceDetail> FinanceInvoiceDetails { get; set; }
        public virtual InventoryHousing_cu InventoryHousing_cu { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InventoryItem_Area> InventoryItem_Area { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InventoryItem_Color_cu> InventoryItem_Color_cu { get; set; }
        public virtual InventoryItemBrand_cu InventoryItemBrand_cu { get; set; }
        public virtual InventoryItemCategory_cu InventoryItemCategory_cu { get; set; }
        public virtual InventoryItemType_p InventoryItemType_p { get; set; }
        public virtual UnitMeasurment_cu UnitMeasurment_cu { get; set; }
        public virtual User_cu User_cu { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InventoryItemTransaction> InventoryItemTransactions { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InventoryItem_Printing_cu> InventoryItem_Printing_cu { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InventoryItem_RawMaterial_cu> InventoryItem_RawMaterial_cu { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InventoryItem_UnitMeasurment_cu> InventoryItem_UnitMeasurment_cu { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InventoryItemGroup_InventoryItem_cu> InventoryItemGroup_InventoryItem_cu { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InventoryItemPrice_cu> InventoryItemPrice_cu { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InvoiceDetail_Inventory> InvoiceDetail_Inventory { get; set; }
    }
}
