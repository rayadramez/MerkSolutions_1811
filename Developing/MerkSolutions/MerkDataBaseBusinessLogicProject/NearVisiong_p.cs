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
    
    public partial class NearVisiong_p
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NearVisiong_p()
        {
            this.VisitTiming_VisionRefractionReading = new HashSet<VisitTiming_VisionRefractionReading>();
            this.VisitTiming_VisionRefractionReading1 = new HashSet<VisitTiming_VisionRefractionReading>();
            this.VisitTiming_VisionRefractionReading2 = new HashSet<VisitTiming_VisionRefractionReading>();
        }
    
        public int ID { get; set; }
        public string Name_P { get; set; }
        public string Name_S { get; set; }
        public string Description { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VisitTiming_VisionRefractionReading> VisitTiming_VisionRefractionReading { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VisitTiming_VisionRefractionReading> VisitTiming_VisionRefractionReading1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VisitTiming_VisionRefractionReading> VisitTiming_VisionRefractionReading2 { get; set; }
    }
}