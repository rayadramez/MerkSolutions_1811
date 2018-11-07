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
    
    public partial class CashBoxInOutTransaction
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CashBoxInOutTransaction()
        {
            this.CashBoxInOutTransaction_AccountingJournalTransaction = new HashSet<CashBoxInOutTransaction_AccountingJournalTransaction>();
        }
    
        public int ID { get; set; }
        public System.DateTime TranscationDate { get; set; }
        public int CashBoxTransactionType_P_ID { get; set; }
        public int ChartOfAccount_CU_ID { get; set; }
        public int GeneralChartOfAccountType_CU_ID { get; set; }
        public double TransactionAmount { get; set; }
        public int PaymentType_P_ID { get; set; }
        public Nullable<int> CashBox_CU_ID { get; set; }
        public Nullable<int> Bank_CU_ID { get; set; }
        public Nullable<int> BankAccount_CU_ID { get; set; }
        public int Currency_CU_ID { get; set; }
        public Nullable<double> CurrencyExchangeRate { get; set; }
        public string TranscationSerial { get; set; }
        public bool IsOnDuty { get; set; }
        public string Description { get; set; }
        public int InsertedBy { get; set; }
        public bool IsCancelled { get; set; }
        public Nullable<int> CancelledBy { get; set; }
        public Nullable<System.DateTime> CancellationDate { get; set; }
        public string CancellationReason { get; set; }
    
        public virtual Bank_cu Bank_cu { get; set; }
        public virtual BankAccount_cu BankAccount_cu { get; set; }
        public virtual CashBox_cu CashBox_cu { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CashBoxInOutTransaction_AccountingJournalTransaction> CashBoxInOutTransaction_AccountingJournalTransaction { get; set; }
        public virtual CashBoxTransactionType_p CashBoxTransactionType_p { get; set; }
        public virtual ChartOfAccount_cu ChartOfAccount_cu { get; set; }
        public virtual Currency_cu Currency_cu { get; set; }
        public virtual GeneralChartOfAccountType_cu GeneralChartOfAccountType_cu { get; set; }
        public virtual PaymentType_p PaymentType_p { get; set; }
        public virtual User_cu User_cu { get; set; }
        public virtual User_cu User_cu1 { get; set; }
    }
}