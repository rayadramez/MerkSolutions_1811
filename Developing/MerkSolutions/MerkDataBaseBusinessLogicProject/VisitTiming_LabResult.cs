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
    
    public partial class VisitTiming_LabResult
    {
        public int ID { get; set; }
        public int VisitTiming_AttachmentID { get; set; }
        public Nullable<int> VisitTiming_LabReservationID { get; set; }
        public string Description { get; set; }
        public bool IsOnDuty { get; set; }
        public Nullable<int> InsertedBy { get; set; }
    
        public virtual User_cu User_cu { get; set; }
        public virtual VisitTiming_Attachment VisitTiming_Attachment { get; set; }
        public virtual VisitTiming_LabReservation VisitTiming_LabReservation { get; set; }
    }
}
