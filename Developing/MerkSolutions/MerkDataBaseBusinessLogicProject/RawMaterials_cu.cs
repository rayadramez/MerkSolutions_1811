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
    
    public partial class RawMaterials_cu
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RawMaterials_cu()
        {
            this.InventoryItem_RawMaterial_cu = new HashSet<InventoryItem_RawMaterial_cu>();
            this.RawMaterialTranasctions = new HashSet<RawMaterialTranasction>();
        }
    
        public int ID { get; set; }
        public string Name_P { get; set; }
        public string Name_S { get; set; }
        public int RawTypeID { get; set; }
        public double Thickness { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
        public Nullable<System.DateTime> ExpirationDate { get; set; }
        public string InternalCode { get; set; }
        public Nullable<int> Color_CU_ID { get; set; }
        public Nullable<bool> IsCountable { get; set; }
        public Nullable<bool> IsStockAvailable { get; set; }
        public string Description { get; set; }
        public Nullable<int> InsertedBy { get; set; }
        public bool IsOnDuty { get; set; }
    
        public virtual Color_cu Color_cu { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InventoryItem_RawMaterial_cu> InventoryItem_RawMaterial_cu { get; set; }
        public virtual RawMaterialType_p RawMaterialType_p { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RawMaterialTranasction> RawMaterialTranasctions { get; set; }
        public virtual User_cu User_cu { get; set; }
    }
}
