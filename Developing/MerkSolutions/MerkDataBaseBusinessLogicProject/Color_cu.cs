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
    
    public partial class Color_cu
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Color_cu()
        {
            this.RawMaterials_cu = new HashSet<RawMaterials_cu>();
        }
    
        public int ID { get; set; }
        public string Name_P { get; set; }
        public string Name_S { get; set; }
        public string InternalCode { get; set; }
        public string Description { get; set; }
        public Nullable<int> InsertedBy { get; set; }
        public bool IsOnDuty { get; set; }
    
        public virtual User_cu User_cu { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RawMaterials_cu> RawMaterials_cu { get; set; }
    }
}
