using System.Collections.Generic;
using MerkDataBaseBusinessLogicProject;

namespace MVCBusinessLogicLibrary.Viewers
{
	public interface IInventoryItem_RawMaterial_Viewer : IViewer
	{
		object InventoryItemID { get; set; }
		object RawMaterialID { get; set; }
		object Width { get; set; }
		object Height { get; set; }
		object Count { get; set; }
		object HasDimensions { get; set; }
	}
}
