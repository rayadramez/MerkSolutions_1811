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
    
    public partial class InventoryItem_Area
    {
        public int ID { get; set; }
        public int InventoryItemID { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public double Count { get; set; }
        public bool IsOnDuty { get; set; }
        public Nullable<int> InsertedBy { get; set; }
    }
}