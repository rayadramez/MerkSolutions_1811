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
    
    public partial class GetInvoiceForAddmission_Result
    {
        public int PatientID { get; set; }
        public string PatientFullName { get; set; }
        public int InvoiceID { get; set; }
        public System.DateTime InvoiceCreationDate { get; set; }
        public string InvoiceSerial { get; set; }
        public Nullable<System.DateTime> InvoicePrintingDate { get; set; }
        public int DoctorID { get; set; }
        public string DoctorName { get; set; }
        public bool IsPaymentEnough { get; set; }
    }
}
