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
    
    public partial class PupillaryRAPDCauses_cu
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PupillaryRAPDCauses_cu()
        {
            this.VisitTiming_Pupillary = new HashSet<VisitTiming_Pupillary>();
            this.VisitTiming_Pupillary1 = new HashSet<VisitTiming_Pupillary>();
        }
    
        public int ID { get; set; }
        public string Name_P { get; set; }
        public string Name_S { get; set; }
        public Nullable<bool> IsOnDuty { get; set; }
        public Nullable<int> InsertedBy { get; set; }
    
        public virtual User_cu User_cu { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VisitTiming_Pupillary> VisitTiming_Pupillary { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VisitTiming_Pupillary> VisitTiming_Pupillary1 { get; set; }
    }
}
