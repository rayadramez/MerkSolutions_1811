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
    
    public partial class BankAccount_cu
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BankAccount_cu()
        {
            this.CashBoxInOutTransactions = new HashSet<CashBoxInOutTransaction>();
            this.InvoicePayment_Check = new HashSet<InvoicePayment_Check>();
            this.InvoicePayment_Visa = new HashSet<InvoicePayment_Visa>();
        }
    
        public int ID { get; set; }
        public int Bank_CU_ID { get; set; }
        public string Name_P { get; set; }
        public string Name_S { get; set; }
        public bool IsOnDuty { get; set; }
        public string Description { get; set; }
        public Nullable<int> InsertedBy { get; set; }
        public Nullable<int> ChartOfAccount_CU_ID { get; set; }
    
        public virtual Bank_cu Bank_cu { get; set; }
        public virtual ChartOfAccount_cu ChartOfAccount_cu { get; set; }
        public virtual User_cu User_cu { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CashBoxInOutTransaction> CashBoxInOutTransactions { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InvoicePayment_Check> InvoicePayment_Check { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InvoicePayment_Visa> InvoicePayment_Visa { get; set; }
    }
}
