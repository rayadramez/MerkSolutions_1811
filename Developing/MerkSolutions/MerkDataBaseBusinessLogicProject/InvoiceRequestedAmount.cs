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
    
    public partial class InvoiceRequestedAmount
    {
        public int ID { get; set; }
        public Nullable<int> InvoiceID { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<double> RequestedAmount { get; set; }
        public Nullable<bool> IsOnDuty { get; set; }
        public string Description { get; set; }
        public Nullable<int> InsertedBy { get; set; }
    
        public virtual Invoice Invoice { get; set; }
        public virtual User_cu User_cu { get; set; }
    }
}
