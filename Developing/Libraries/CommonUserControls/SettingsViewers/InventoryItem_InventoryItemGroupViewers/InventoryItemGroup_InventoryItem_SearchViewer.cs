using System.Collections.Generic;
using MerkDataBaseBusinessLogicProject;
using MVCBusinessLogicLibrary.BaseViewers;
using MVCBusinessLogicLibrary.MVCFactories;
using MVCBusinessLogicLibrary.Viewers;

namespace CommonUserControls.SettingsViewers.InventoryItem_InventoryItemGroupViewers
{
	public partial class InventoryItemGroup_InventoryItem_SearchViewer : 
		//UserControl
		CommonAbstractSearchViewer<InventoryItemGroup_InventoryItem_cu>,
		IInventoryItemGroup_InventoryItem_Viewer
	{
		public InventoryItemGroup_InventoryItem_SearchViewer()
		{
			InitializeComponent();
		}

		#region Overrides of CommonAbstractViewer<InventoryItemGroup_InventoryItem_cu>

		public override object ViewerID
		{
			get { return (int)ViewerName.InventoryItemGroup_InventoryItem_Viewer; }
		}

		public override string HeaderTitle
		{
			get { return "ربـــط مجمـوعــــة المنتجــــات بالمنتجــــات"; }
		}

		public override string GridXML
		{
			get { return Resources.LocalizedRes.grd_InventoryItemGroup_InventoryItem_SearchViewer; }
		}

		#endregion

		#region Implementation of IInventoryItemGroup_InventoryItem_Viewer

		public List<InventoryItemGroup_InventoryItem_cu> List_InventoryItemGroup_InventoryItem { get; set; }

		#endregion
	}
}
