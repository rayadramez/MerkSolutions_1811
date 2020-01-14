using CommonControlLibrary;
using MerkDataBaseBusinessLogicProject;
using MVCBusinessLogicLibrary.BaseViewers;
using MVCBusinessLogicLibrary.MVCFactories;
using MVCBusinessLogicLibrary.Viewers;

namespace CommonUserControls.SettingsViewers.InventoryItem_Area_Viewers
{
	public partial class InventoryItem_Area_SearchViewer : 
		//UserControl
		CommonAbstractSearchViewer<InventoryItem_Area>,
		IInventoryItem_Area_Viewer
	{
		public InventoryItem_Area_SearchViewer()
		{
			InitializeComponent();

			CommonViewsActions.LoadXMLFromString(layoutControl1, Resources.LocalizedRes.lyt_Inventoryitem_Area_SearchViewer);
			CommonViewsActions.SetupSyle(this);
		}

		#region Overrides of CommonAbstractViewer<InventoryItem_Area>

		public override object ViewerID
		{
			get { return (int)ViewerName.InventoryItem_Area_Viewer; }
		}

		public override string HeaderTitle
		{
			get { return "المنتجــــات"; }
		}

		public override string GridXML
		{
			get { return Resources.LocalizedRes.grd_InventoryItem_Area_SearchViewer; }
		}

		#endregion

		#region Implementation of IInventoryItem_Area_Viewer

		public object InventoryItemID
		{
			get { return lkeInventoryItems.EditValue; }
			set { lkeInventoryItems.EditValue = value; }
		}

		public object Width
		{
			get { return spnWidth.EditValue; }
			set { spnWidth.EditValue = value; }
		}

		public object Height
		{
			get { return spnHeight.EditValue; }
			set { spnHeight.EditValue = value; }
		}

		public object Count
		{
			get { return spnCount.EditValue; }
			set { spnCount.EditValue = value; }
		}

		public object InternalCode
		{
			get { return txtInternalCode.EditValue; }
			set { txtInternalCode.EditValue = value; }
		}

		public object SizeUnitMeasure_P_ID { get; set; }

		#endregion
	}
}
