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
    
    public partial class RawMaterialTranasction
    {
        public int ID { get; set; }
        public int RawMaterial_CU_ID { get; set; }
        public int Count { get; set; }
        public int RawMaterialTransactionType_P_ID { get; set; }
        public System.DateTime Date { get; set; }
        public Nullable<int> InsertedBy { get; set; }
        public bool IsOnDuty { get; set; }
    
        public virtual RawMaterialTranasctionType_p RawMaterialTranasctionType_p { get; set; }
        public virtual User_cu User_cu { get; set; }
    }
}
