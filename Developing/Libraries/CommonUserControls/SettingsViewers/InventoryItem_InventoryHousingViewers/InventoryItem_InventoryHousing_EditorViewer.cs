using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CommonControlLibrary;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using MerkDataBaseBusinessLogicProject;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MVCBusinessLogicLibrary.BaseViewers;
using MVCBusinessLogicLibrary.Controller;
using MVCBusinessLogicLibrary.MVCFactories;
using MVCBusinessLogicLibrary.Viewers;

namespace CommonUserControls.SettingsViewers.InventoryItem_InventoryHousingViewers
{
	public partial class InventoryItem_InventoryHousing_EditorViewer : 
		//DevExpress.XtraEditors.XtraUserControl
		CommonAbstractEditorViewer<InventoryItemTransaction>,
		IInventoryItem_InventoryHousing_Viewer
	{
		public InventoryItem_InventoryHousing_EditorViewer()
		{
			InitializeComponent();

			CommonViewsActions.LoadXMLFromString(layoutControl1,
				Resources.LocalizedRes.lyt_InventoryItem_InventoryHousing_EditorViewer);
			CommonViewsActions.SetupSyle(this);
		}

		#region Overrides of CommonAbstractViewer<InventoryItemTransaction>

		public override IMVCController<InventoryItemTransaction> ViewerCollector { get; set; }

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

			List<InventoryItemTransactionType_p> list =
				InventoryItemTransactionType_p.ItemsList.FindAll(
					item =>
						Convert.ToInt32(item.ID).Equals(Convert.ToInt32(DB_InventoryItemTransactionType.StartingInventoryBalance)) /*||
						Convert.ToInt32(item.ID).Equals(Convert.ToInt32(DB_InventoryItemTransactionType.EndingInventoryBalance)) ||
						Convert.ToInt32(item.ID).Equals(Convert.ToInt32(DB_InventoryItemTransactionType.InventorySettlement))*/);

			CommonViewsActions.FillGridlookupEdit(lkeInventoryItemTransactionType, list);
			lkeInventoryItemTransactionType.EditValue = list.Find(item =>
				Convert.ToInt32(item.ID).Equals(Convert.ToInt32(DB_InventoryItemTransactionType.StartingInventoryBalance))).ID;
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

		public object InventoryItemTransactionType
		{
			get { return lkeInventoryItemTransactionType.EditValue; }
			set { lkeInventoryItemTransactionType.EditValue = value; }
		}

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

		private void lkeInventoryItem_EditValueChanged(object sender, EventArgs e)
		{
			if (lkeInventoryItem.EditValue == null)
			{
				lkeInventoryItem.Properties.DataSource = null;
				return;
			}

			List<UnitMeasurment_cu> unitMeasurmentList =
				InventoryBusinessLogicEngine.GetInventoryItemRegisteredUnitMeasurments(Convert.ToInt32(lkeInventoryItem.EditValue));
			if (unitMeasurmentList.Count == 0)
				XtraMessageBox.Show("لا يـوجـــد وحـدات قيــــاس مـربـوطــــة مـع هــذا المنتــــج", "تنبيـــــه",
					MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, DefaultBoolean.Default);

			UnitMeasurment_cu inventoryTrackingUnit =
				InventoryBusinessLogicEngine.GetInventoryTrackingUnitMeasurment(lkeInventoryItem.EditValue);
			if (inventoryTrackingUnit == null)
				return;

			CommonViewsActions.FillGridlookupEdit(lkeUnitMeasurment,
				unitMeasurmentList.FindAll(item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(inventoryTrackingUnit.ID))));
			lkeUnitMeasurment.EditValue = inventoryTrackingUnit.ID;
		}

		private void btnNewInventoryHousing_Click(object sender, EventArgs e)
		{
		
		}

		private void btnNewInventoryItem_Click(object sender, EventArgs e)
		{
		
		}
	}
}
