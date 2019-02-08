using System;
using CommonControlLibrary;
using MerkDataBaseBusinessLogicProject;
using MVCBusinessLogicLibrary.BaseViewers;
using MVCBusinessLogicLibrary.MVCFactories;
using MVCBusinessLogicLibrary.Viewers;

namespace CommonUserControls.SettingsViewers.InventoryItem_InventoryHousingViewers
{
	public partial class InventoryItem_InventoryHousing_SearchViewer : 
		//UserControl
		CommonAbstractSearchViewer<InventoryItemTransaction>,
		IInventoryItem_InventoryHousing_Viewer
	{
		public InventoryItem_InventoryHousing_SearchViewer()
		{
			InitializeComponent();

			CommonViewsActions.LoadXMLFromString(layoutControl1,
				Resources.LocalizedRes.lyt_InventoryItem_InventoryHousing_SearchViewer);
			CommonViewsActions.SetupSyle(this);
		}

		#region Overrides of CommonAbstractViewer<InventoryItemTransaction>

		public override object ViewerID
		{
			get { return (int)ViewerName.InventoryItem_InventoryHousing_Viewer; }
		}

		public override string HeaderTitle
		{
			get { return "ربــط المنتجــــات بالمخــــازن"; }
		}

		public override void FillControls()
		{
			CommonViewsActions.FillGridlookupEdit(lkeInventoryItem, InventoryItem_cu.ItemsList);
			CommonViewsActions.FillGridlookupEdit(lkeInventoryHousing, InventoryHousing_cu.ItemsList, "InventoryHousingFullName");

			dtEntryDate.DateTime = DateTime.Now;
			spnQuantity.EditValue = 0;
		}

		public override void ClearControls()
		{
			lkeInventoryHousing.EditValue = null;
			lkeInventoryItem.EditValue = null;
			lkeUnitMeasurment.EditValue = null;
			spnQuantity.EditValue = null;
			dtEntryDate.EditValue = DateTime.Now;
			dtExpirationDate.EditValue = DateTime.Now;
		}

		public override string GridXML
		{
			get { return Resources.LocalizedRes.grd_InventoryItem_InventoryHousing_SearchViewer; }
		}

		#endregion

		#region Implementation of IInventoryItem_InventoryHousing_Viewer

		public object InventoryItem_CU_ID
		{
			get { return lkeInventoryItem.EditValue; }
			set { lkeInventoryItem.EditValue = value; }
		}

		public object InventoryHousing_CU_ID
		{
			get { return lkeInventoryHousing.EditValue; }
			set { lkeInventoryHousing.EditValue = value; }
		}

		public object Quantity
		{
			get { return spnQuantity.EditValue; }
			set { spnQuantity.EditValue = value; }
		}

		public object UnitMeasurment_CU_ID
		{
			get { return lkeUnitMeasurment.EditValue; }
			set { lkeUnitMeasurment.EditValue = value; }
		}

		public object InventoryItemTransactionType { get; set; }

		public object ExpirationDate
		{
			get { return dtExpirationDate.EditValue; }
			set { dtExpirationDate.EditValue = value; }
		}

		public object Date
		{
			get { return dtEntryDate.EditValue; }
			set { dtEntryDate.EditValue = value; }
		}

		#endregion
	}
}
