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
    
    public partial class EmploymentDate_cu
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EmploymentDate_cu()
        {
            this.Employee_cu = new HashSet<Employee_cu>();
        }
    
        public int ID { get; set; }
        public System.DateTime Date { get; set; }
        public int EmploymentDateType_P_ID { get; set; }
        public Nullable<int> InsertedBy { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Employee_cu> Employee_cu { get; set; }
        public virtual EmploymentDateType_p EmploymentDateType_p { get; set; }
        public virtual User_cu User_cu { get; set; }
    }
}