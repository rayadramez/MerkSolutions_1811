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
    
    public partial class StationPoint_cu
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public StationPoint_cu()
        {
            this.OrganizationMachine_StationPoint_cu = new HashSet<OrganizationMachine_StationPoint_cu>();
            this.QueueManagers = new HashSet<QueueManager>();
            this.Service_StationPoint_cu = new HashSet<Service_StationPoint_cu>();
            this.ServiceCategory_StationPoint_cu = new HashSet<ServiceCategory_StationPoint_cu>();
            this.ServiceType_StationPoint_cu = new HashSet<ServiceType_StationPoint_cu>();
            this.VisitTimings = new HashSet<VisitTiming>();
            this.StationPointStage_cu = new HashSet<StationPointStage_cu>();
        }
    
        public int ID { get; set; }
        public string Name_P { get; set; }
        public string Name_S { get; set; }
        public int Station_P_ID { get; set; }
        public Nullable<double> MaxAdmissionCount { get; set; }
        public bool IsOnDuty { get; set; }
        public string InternalCode { get; set; }
        public string Description { get; set; }
        public Nullable<int> InsertedBy { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrganizationMachine_StationPoint_cu> OrganizationMachine_StationPoint_cu { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<QueueManager> QueueManagers { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Service_StationPoint_cu> Service_StationPoint_cu { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ServiceCategory_StationPoint_cu> ServiceCategory_StationPoint_cu { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ServiceType_StationPoint_cu> ServiceType_StationPoint_cu { get; set; }
        public virtual Station_p Station_p { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VisitTiming> VisitTimings { get; set; }
        public virtual User_cu User_cu { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StationPointStage_cu> StationPointStage_cu { get; set; }
    }
}
