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
    
    public partial class InventoryItemGroup_InventoryItem_cu
    {
        public int ID { get; set; }
        public int InvetoryItemGroup_CU_ID { get; set; }
        public int InventoryItem_CU_ID { get; set; }
        public bool IsOnDuty { get; set; }
    
        public virtual InventoryItem_cu InventoryItem_cu { get; set; }
        public virtual InventoryItemGroup_cu InventoryItemGroup_cu { get; set; }
    }
}