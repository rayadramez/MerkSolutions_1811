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
    
    public partial class VisitTiming_EOMReading
    {
        public int ID { get; set; }
        public int VisitTimingID { get; set; }
        public System.DateTime TakenDateTime { get; set; }
        public Nullable<int> SR_OD { get; set; }
        public Nullable<int> LR_OD { get; set; }
        public Nullable<int> IR_OD { get; set; }
        public Nullable<int> IO_OD { get; set; }
        public Nullable<int> MR_OD { get; set; }
        public Nullable<int> SO_OD { get; set; }
        public Nullable<int> SR_OS { get; set; }
        public Nullable<int> LR_OS { get; set; }
        public Nullable<int> IR_OS { get; set; }
        public Nullable<int> IO_OS { get; set; }
        public Nullable<int> MR_OS { get; set; }
        public Nullable<int> SO_OS { get; set; }
        public bool IsOnDuty { get; set; }
        public Nullable<int> InsertedBy { get; set; }
    
        public virtual User_cu User_cu { get; set; }
        public virtual VisitTiming VisitTiming { get; set; }
    }
}
