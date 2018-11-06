using System.Collections.Generic;
using MerkDataBaseBusinessLogicProject;

namespace MVCBusinessLogicLibrary.Viewers
{
	public interface IInventoryItem_UnitMeasurment_Viewer : IViewer
	{
		List<InventoryItem_UnitMeasurment_cu> List_InventoryItem_UnitMeasurment { get; set; }
	}
}
