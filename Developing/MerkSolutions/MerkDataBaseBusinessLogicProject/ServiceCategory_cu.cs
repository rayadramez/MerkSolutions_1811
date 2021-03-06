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
    
    public partial class ServiceCategory_cu
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ServiceCategory_cu()
        {
            this.Doctor_Service_cu = new HashSet<Doctor_Service_cu>();
            this.Service_cu = new HashSet<Service_cu>();
            this.ServiceCategory_StationPoint_cu = new HashSet<ServiceCategory_StationPoint_cu>();
            this.ServiceCategory_StationPointStage_cu = new HashSet<ServiceCategory_StationPointStage_cu>();
            this.ServiceCategory_Surcharge_cu = new HashSet<ServiceCategory_Surcharge_cu>();
            this.ServicePrice_cu = new HashSet<ServicePrice_cu>();
        }
    
        public int ID { get; set; }
        public string Name_P { get; set; }
        public string Name_S { get; set; }
        public int ServiceType_P_ID { get; set; }
        public Nullable<bool> AllowAdmission { get; set; }
        public Nullable<int> ChartOfAccount_CU_ID { get; set; }
        public bool IsOnDuty { get; set; }
        public string Description { get; set; }
        public Nullable<bool> IsMedical { get; set; }
        public string InternalCode { get; set; }
        public Nullable<int> InsertedBy { get; set; }
    
        public virtual ChartOfAccount_cu ChartOfAccount_cu { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Doctor_Service_cu> Doctor_Service_cu { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Service_cu> Service_cu { get; set; }
        public virtual ServiceType_p ServiceType_p { get; set; }
        public virtual User_cu User_cu { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ServiceCategory_StationPoint_cu> ServiceCategory_StationPoint_cu { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ServiceCategory_StationPointStage_cu> ServiceCategory_StationPointStage_cu { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ServiceCategory_Surcharge_cu> ServiceCategory_Surcharge_cu { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ServicePrice_cu> ServicePrice_cu { get; set; }
    }
}
