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
    
    public partial class Medication_cu
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Medication_cu()
        {
            this.Medication_Dose_cu = new HashSet<Medication_Dose_cu>();
            this.VisitTiming_Medication = new HashSet<VisitTiming_Medication>();
        }
    
        public int ID { get; set; }
        public int MedicationCategory_CU_ID { get; set; }
        public string Name_P { get; set; }
        public string Name_S { get; set; }
        public bool IsOnDuty { get; set; }
        public int InsertedBy { get; set; }
        public string Description { get; set; }
    
        public virtual MedicationCategory_cu MedicationCategory_cu { get; set; }
        public virtual User_cu User_cu { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Medication_Dose_cu> Medication_Dose_cu { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VisitTiming_Medication> VisitTiming_Medication { get; set; }
    }
}