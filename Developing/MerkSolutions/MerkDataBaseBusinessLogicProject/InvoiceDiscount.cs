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
    
    public partial class InvoiceDiscount
    {
        public int ID { get; set; }
        public int InvoiceID { get; set; }
        public int DisountType_P_ID { get; set; }
        public double DiscountAmount { get; set; }
        public bool IsOnDuty { get; set; }
        public string Description { get; set; }
        public Nullable<int> InsertedBy { get; set; }
    
        public virtual DiscountType_p DiscountType_p { get; set; }
        public virtual Invoice Invoice { get; set; }
        public virtual User_cu User_cu { get; set; }
    }
}