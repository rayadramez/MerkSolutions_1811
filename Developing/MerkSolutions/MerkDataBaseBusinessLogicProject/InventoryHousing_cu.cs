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
    
    public partial class InventoryHousing_cu
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public InventoryHousing_cu()
        {
            this.FinanceInvoiceDetails = new HashSet<FinanceInvoiceDetail>();
            this.InventoryItem_cu = new HashSet<InventoryItem_cu>();
            this.InventoryItemTransactions = new HashSet<InventoryItemTransaction>();
            this.InvoiceDetail_Inventory = new HashSet<InvoiceDetail_Inventory>();
        }
    
        public int ID { get; set; }
        public string Name_P { get; set; }
        public string Name_S { get; set; }
        public Nullable<int> Floor_CU_ID { get; set; }
        public bool IsMain { get; set; }
        public string InternalCode { get; set; }
        public Nullable<int> ChartOfAccount_CU_ID { get; set; }
        public string Description { get; set; }
        public bool IsOnDuty { get; set; }
        public Nullable<int> InsertedBy { get; set; }
    
        public virtual ChartOfAccount_cu ChartOfAccount_cu { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FinanceInvoiceDetail> FinanceInvoiceDetails { get; set; }
        public virtual Floor_cu Floor_cu { get; set; }
        public virtual User_cu User_cu { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InventoryItem_cu> InventoryItem_cu { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InventoryItemTransaction> InventoryItemTransactions { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InvoiceDetail_Inventory> InvoiceDetail_Inventory { get; set; }
    }
}
