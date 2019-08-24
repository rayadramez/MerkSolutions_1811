using System.Collections.Generic;
using MerkDataBaseBusinessLogicProject;

namespace MVCBusinessLogicLibrary.Viewers
{
	public interface IInventoryItemGroup_InventoryItem_Viewer : IViewer
	{
		List<InventoryItemGroup_InventoryItem_cu> List_InventoryItemGroup_InventoryItem { get; set; }
	}
}
