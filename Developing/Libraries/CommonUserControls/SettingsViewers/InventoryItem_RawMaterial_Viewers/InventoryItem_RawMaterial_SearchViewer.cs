using System.Collections.Generic;
using System.Windows.Forms;
using MerkDataBaseBusinessLogicProject;
using MVCBusinessLogicLibrary.BaseViewers;
using MVCBusinessLogicLibrary.MVCFactories;
using MVCBusinessLogicLibrary.Viewers;

namespace CommonUserControls.SettingsViewers.InventoryItem_RawMaterial_Viewers
{
	public partial class InventoryItem_RawMaterial_SearchViewer : 
		//UserControl
		CommonAbstractSearchViewer<InventoryItem_RawMaterial_cu>,
		IInventoryItem_RawMaterial_Viewer
	{
		public InventoryItem_RawMaterial_SearchViewer()
		{
			InitializeComponent();
		}

		#region Overrides of CommonAbstractViewer<InventoryItem_RawMaterial_cu>

		public override object ViewerID
		{
			get { return (int)ViewerName.InventoryItem_RawMaterial_Viewer; }
		}

		public override string HeaderTitle
		{
			get { return " ربـــط مجمـوعـــات المستخـدميـــن بالوظائـــف"; }
		}

		public override string GridXML
		{
			get { return Resources.LocalizedRes.grd_InventoryItem_RawMaterial_SearchViewer; }
		}

		#endregion

		#region Implementation of IInventoryItem_RawMaterial_Viewer

		public List<InventoryItem_RawMaterial_cu> List_InventoryItem_RawMaterial { get; set; }

		#endregion

		#region Implementation of IInventoryItem_RawMaterial_Viewer

		public object InventoryItemID { get; set; }
		public object RawMaterialID { get; set; }
		public object Width { get; set; }
		public object Height { get; set; }
		public object Count { get; set; }
		public object HasDimensions { get; set; }

		#endregion
	}
}
