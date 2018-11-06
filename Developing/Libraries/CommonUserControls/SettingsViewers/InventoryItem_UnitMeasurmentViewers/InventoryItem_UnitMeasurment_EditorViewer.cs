using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CommonControlLibrary;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using MerkDataBaseBusinessLogicProject;
using MVCBusinessLogicLibrary.BaseViewers;
using MVCBusinessLogicLibrary.Controller;
using MVCBusinessLogicLibrary.MVCFactories;
using MVCBusinessLogicLibrary.Viewers;

namespace CommonUserControls.SettingsViewers.InventoryItem_UnitMeasurmentViewers
{
	public partial class InventoryItem_UnitMeasurment_EditorViewer :
		//UserControl
		CommonAbstractEditorViewer<InventoryItem_UnitMeasurment_cu>,
		IInventoryItem_UnitMeasurment_Viewer
	{
		private UnitMeasurment_cu SelectedUnitMeasurmentFromGrid = null;
		private List<UnitMeasurment_cu> List_SelectedUnitMeasurmentToBedAdded = null;

		public InventoryItem_UnitMeasurment_EditorViewer()
		{
			InitializeComponent();
		}

		public override IMVCController<InventoryItem_UnitMeasurment_cu> ViewerCollector { get; set; }

		public override object ViewerID
		{
			get { return (int)ViewerName.InventoryItem_UnitMeasurment_Viewer; }
		}

		public override string HeaderTitle
		{
			get { return "ربـــط المنتجــــات بوحـــدات القيـــاس"; }
		}

		public override void FillControls()
		{
			grdAllInventoryitems.DataSource = UnitMeasurment_cu.ItemsList;
			CommonViewsActions.FillGridlookupEdit(lkeInventoryItems, InventoryItem_cu.ItemsList);
		}

		public override void ClearControls()
		{
			txtUnitMeasurmentInternalCode.EditValue = null;
			txtUnitMeasurmentName.EditValue = null;
			lst_UnitMeasurments.DataSource = null;
			lkeInventoryItems.EditValue = null;
		}

		private void btnAddList_Click(object sender, EventArgs e)
		{
			if (lkeInventoryItems.EditValue == null)
			{
				XtraMessageBox.Show("يجـب إختيــار البـرنـامج", "تنبيــــــــــه", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return;
			}

			if (SelectedUnitMeasurmentFromGrid == null)
			{
				XtraMessageBox.Show("يجب إختيــار مجمـوعــة المستخـدميـــن", "تنبيــــــــــه", MessageBoxButtons.OK,
					MessageBoxIcon.Hand);
				return;
			}

			if (List_SelectedUnitMeasurmentToBedAdded == null)
				List_SelectedUnitMeasurmentToBedAdded = new List<UnitMeasurment_cu>();

			if (List_SelectedUnitMeasurmentToBedAdded.Count > 0)
				if (List_SelectedUnitMeasurmentToBedAdded.Exists(
						item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(SelectedUnitMeasurmentFromGrid.ID))))
				{
					XtraMessageBox.Show("تمــت إضافتـــه مـن قبـــل", "تنبيــــــــــه", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					return;
				}

			List_SelectedUnitMeasurmentToBedAdded.Add(SelectedUnitMeasurmentFromGrid);
			CommonViewsActions.FillListBoxControl(lst_UnitMeasurments, List_SelectedUnitMeasurmentToBedAdded);
			lst_UnitMeasurments.Refresh();

			InventoryItem_UnitMeasurment_cu inventoryItemUnitMeasurment = new InventoryItem_UnitMeasurment_cu();
			inventoryItemUnitMeasurment.UnitMeasurment_CU_ID = SelectedUnitMeasurmentFromGrid.ID;
			inventoryItemUnitMeasurment.InventoryItem_CU_ID = Convert.ToInt32(lkeInventoryItems.EditValue);
			if (List_InventoryItem_UnitMeasurment == null)
				List_InventoryItem_UnitMeasurment = new List<InventoryItem_UnitMeasurment_cu>();
			List_InventoryItem_UnitMeasurment.Add(inventoryItemUnitMeasurment);
		}

		private void btnRemoveFromList_Click(object sender, EventArgs e)
		{
			if (lst_UnitMeasurments.SelectedItems.Count == 0 || List_InventoryItem_UnitMeasurment == null)
				return;

			UnitMeasurment_cu selectedUnitMeasurment = (UnitMeasurment_cu)lst_UnitMeasurments.SelectedItem;
			if (selectedUnitMeasurment == null)
				return;
			if (List_SelectedUnitMeasurmentToBedAdded.Exists(
				item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(selectedUnitMeasurment.ID))))
				List_SelectedUnitMeasurmentToBedAdded.Remove(selectedUnitMeasurment);

			CommonViewsActions.FillListBoxControl(lst_UnitMeasurments, List_SelectedUnitMeasurmentToBedAdded);
			lst_UnitMeasurments.Refresh();

			InventoryItem_UnitMeasurment_cu inventoryItemUnitMeasurment =
				List_InventoryItem_UnitMeasurment.Find(
					item => Convert.ToInt32(item.InventoryItem_CU_ID).Equals(Convert.ToInt32(selectedUnitMeasurment.ID)));
			if (inventoryItemUnitMeasurment == null)
				return;
			List_InventoryItem_UnitMeasurment.Remove(inventoryItemUnitMeasurment);
		}

		private void gridView2_GotFocus(object sender, EventArgs e)
		{
			SelectedUnitMeasurmentFromGrid = CommonViewsActions.GetSelectedRowObject<UnitMeasurment_cu>((GridView)sender);
		}

		private void gridView2_MouseUp(object sender, MouseEventArgs e)
		{
			SelectedUnitMeasurmentFromGrid = CommonViewsActions.GetSelectedRowObject<UnitMeasurment_cu>((GridView)sender);
		}

		#region Implementation of IInventoryItem_UnitMeasurment_Viewer

		public List<InventoryItem_UnitMeasurment_cu> List_InventoryItem_UnitMeasurment { get; set; }

		#endregion

		private void txtUnitMeasurmentName_EditValueChanged(object sender, EventArgs e)
		{
			if (txtUnitMeasurmentName.EditValue == null || string.IsNullOrEmpty(txtUnitMeasurmentName.Text) ||
				string.IsNullOrWhiteSpace(txtUnitMeasurmentName.Text))
			{
				grdAllInventoryitems.DataSource = UnitMeasurment_cu.ItemsList;
				return;
			}

			grdAllInventoryitems.DataSource =
				UnitMeasurment_cu.ItemsList.FindAll(
					item => item.Name_P != null && Convert.ToString(item.Name_P).Contains(Convert.ToString(txtUnitMeasurmentName.Text)));
		}

		private void txtUnitMeasurmentInternalCode_EditValueChanged(object sender, EventArgs e)
		{
			if (txtUnitMeasurmentInternalCode.EditValue == null || string.IsNullOrEmpty(txtUnitMeasurmentInternalCode.Text) ||
					string.IsNullOrWhiteSpace(txtUnitMeasurmentInternalCode.Text))
			{
				grdAllInventoryitems.DataSource = UnitMeasurment_cu.ItemsList;
				return;
			}

			grdAllInventoryitems.DataSource =
				UnitMeasurment_cu.ItemsList.FindAll(
					item =>
						item.InternalCode != null &&
						Convert.ToString(item.InternalCode).Contains(Convert.ToString(txtUnitMeasurmentInternalCode.Text)));
		}

		private void gridView2_DoubleClick(object sender, EventArgs e)
		{
			SelectedUnitMeasurmentFromGrid = CommonViewsActions.GetSelectedRowObject<UnitMeasurment_cu>((GridView)sender);
			btnAddList_Click(null, null);
		}
	}
}
