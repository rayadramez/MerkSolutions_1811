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
    
    public partial class VisitTiming_PosteriorSegmentSign
    {
        public int ID { get; set; }
        public int VisitTiming_MainPsoteriorSegmentSignID { get; set; }
        public int SegmentSign_CU_ID { get; set; }
        public int Eye_P_ID { get; set; }
        public bool IsOnDuty { get; set; }
        public Nullable<int> InsertedBy { get; set; }
    
        public virtual Eye_p Eye_p { get; set; }
        public virtual SegmentSign_cu SegmentSign_cu { get; set; }
        public virtual User_cu User_cu { get; set; }
        public virtual VisitTiming_MainPosteriorSegmentSign VisitTiming_MainPosteriorSegmentSign { get; set; }
    }
}
