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
    
    public partial class Department_cu
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Department_cu()
        {
            this.Department_cu1 = new HashSet<Department_cu>();
            this.Department_JobTitle_cu = new HashSet<Department_JobTitle_cu>();
            this.Department_JobTitle_WorkingShiftTitle_cu = new HashSet<Department_JobTitle_WorkingShiftTitle_cu>();
            this.Employee_Department_JobTitle_cu = new HashSet<Employee_Department_JobTitle_cu>();
        }
    
        public int ID { get; set; }
        public string Name_P { get; set; }
        public string Name_S { get; set; }
        public Nullable<int> ParentDepartment_CU_ID { get; set; }
        public Nullable<int> Department_P_ID { get; set; }
        public Nullable<int> Manager_CU_ID { get; set; }
        public Nullable<int> Location_CU_ID { get; set; }
        public string Description { get; set; }
        public Nullable<int> InsertedBy { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Department_cu> Department_cu1 { get; set; }
        public virtual Department_cu Department_cu2 { get; set; }
        public virtual Department_cu Department_cu11 { get; set; }
        public virtual Department_cu Department_cu3 { get; set; }
        public virtual DepartmentType_p DepartmentType_p { get; set; }
        public virtual Location_cu Location_cu { get; set; }
        public virtual Manager_cu Manager_cu { get; set; }
        public virtual User_cu User_cu { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Department_JobTitle_cu> Department_JobTitle_cu { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Department_JobTitle_WorkingShiftTitle_cu> Department_JobTitle_WorkingShiftTitle_cu { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Employee_Department_JobTitle_cu> Employee_Department_JobTitle_cu { get; set; }
    }
}
