namespace MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary
{
	public class UnitMeasurmentTreeLinkObject
	{
		public InventoryItem_cu InventoryItem { get; set; }
		public UnitMeasurment_cu UnitMeasurment { get; set; }
		public bool IsInventoryTracking { get; set; }
		public double EncapsulatedQuantityRelativeToSmallestUnit { get; set; }
		public double EncapsulatedQuantityRelativeToInventoryTrackingUnit { get; set; }
		public int OrderIndex { get; set; }
	}
}
