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
    
    public partial class InvoiceDetail
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public InvoiceDetail()
        {
            this.InvoiceDetail_Accommodation = new HashSet<InvoiceDetail_Accommodation>();
            this.InvoiceDetail_DoctorFees = new HashSet<InvoiceDetail_DoctorFees>();
            this.InvoiceDetail_Inventory = new HashSet<InvoiceDetail_Inventory>();
            this.InvoiceDetail1 = new HashSet<InvoiceDetail>();
            this.VisitTimings = new HashSet<VisitTiming>();
            this.QueueManagers = new HashSet<QueueManager>();
        }
    
        public int ID { get; set; }
        public int InvoiceID { get; set; }
        public Nullable<int> ParentInvoiceDetailID { get; set; }
        public Nullable<int> Service_CU_ID { get; set; }
        public System.DateTime Date { get; set; }
        public Nullable<int> Doctor_CU_ID { get; set; }
        public double Count { get; set; }
        public double PatientShare { get; set; }
        public double InsuranceShare { get; set; }
        public Nullable<int> DiscountType_P_ID { get; set; }
        public Nullable<double> PatientShareDiscount { get; set; }
        public bool IsInsuranceApplied { get; set; }
        public bool IsOnDuty { get; set; }
        public bool IsSurchargeApplied { get; set; }
        public Nullable<double> SurchargeAmount { get; set; }
        public string Description { get; set; }
        public Nullable<int> InsertedBy { get; set; }
    
        public virtual DiscountType_p DiscountType_p { get; set; }
        public virtual Doctor_cu Doctor_cu { get; set; }
        public virtual Invoice Invoice { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InvoiceDetail_Accommodation> InvoiceDetail_Accommodation { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InvoiceDetail_DoctorFees> InvoiceDetail_DoctorFees { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InvoiceDetail_Inventory> InvoiceDetail_Inventory { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InvoiceDetail> InvoiceDetail1 { get; set; }
        public virtual InvoiceDetail InvoiceDetail2 { get; set; }
        public virtual Service_cu Service_cu { get; set; }
        public virtual User_cu User_cu { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VisitTiming> VisitTimings { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<QueueManager> QueueManagers { get; set; }
    }
}
