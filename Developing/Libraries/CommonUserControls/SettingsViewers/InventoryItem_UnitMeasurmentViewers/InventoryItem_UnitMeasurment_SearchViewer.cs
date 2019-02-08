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
	public partial class InventoryItem_UnitMeasurment_SearchViewer : 
		//UserControl
		CommonAbstractSearchViewer<InventoryItem_UnitMeasurment_cu>,
		IInventoryItem_UnitMeasurment_Viewer
	{
		private InventoryItem_cu SelectedInventoryItemFromGrid = null;
		private List<InventoryItem_cu> List_SelectedInventoryItemToBedAdded = null;

		public InventoryItem_UnitMeasurment_SearchViewer()
		{
			InitializeComponent();

			CommonViewsActions.LoadXMLFromString(layoutControl1, Resources.LocalizedRes.lyt_InventoryItem_UnitMeasurment_EditorViewer);
			CommonViewsActions.SetupGridControl(grdAllInventoryitems, Resources.LocalizedRes.grd_UserGroup_Application_Internal, true);
			CommonViewsActions.SetupSyle(this);
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
			grdAllInventoryitems.DataSource = InventoryItem_cu.ItemsList;
			CommonViewsActions.FillGridlookupEdit(lkeUnitMeasurments, UnitMeasurment_cu.ItemsList);
		}

		public override string GridXML
		{
			get { return Resources.LocalizedRes.grd_UserGroupSearchViewer; }
		}

		public override void ClearControls()
		{
			txtInventoryItemInternalCode.EditValue = null;
			txtInventoryitemName.EditValue = null;
			lst_InventoryItems.DataSource = null;
			lkeUnitMeasurments.EditValue = null;
		}

		private void btnAddList_Click(object sender, EventArgs e)
		{
			if (lkeUnitMeasurments.EditValue == null)
			{
				XtraMessageBox.Show("يجـب إختيــار البـرنـامج", "تنبيــــــــــه", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return;
			}

			if (SelectedInventoryItemFromGrid == null)
			{
				XtraMessageBox.Show("يجب إختيــار مجمـوعــة المستخـدميـــن", "تنبيــــــــــه", MessageBoxButtons.OK,
					MessageBoxIcon.Hand);
				return;
			}

			if (List_SelectedInventoryItemToBedAdded == null)
				List_SelectedInventoryItemToBedAdded = new List<InventoryItem_cu>();

			if (List_SelectedInventoryItemToBedAdded.Count > 0)
				if (List_SelectedInventoryItemToBedAdded.Exists(
						item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(SelectedInventoryItemFromGrid.ID))))
				{
					XtraMessageBox.Show("تمــت إضافتـــه مـن قبـــل", "تنبيــــــــــه", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					return;
				}

			List_SelectedInventoryItemToBedAdded.Add(SelectedInventoryItemFromGrid);
			CommonViewsActions.FillListBoxControl(lst_InventoryItems, List_SelectedInventoryItemToBedAdded);
			lst_InventoryItems.Refresh();

			InventoryItem_UnitMeasurment_cu inventoryItemUnitMeasurment = new InventoryItem_UnitMeasurment_cu();
			inventoryItemUnitMeasurment.InventoryItem_CU_ID = SelectedInventoryItemFromGrid.ID;
			inventoryItemUnitMeasurment.UnitMeasurment_CU_ID = Convert.ToInt32(lkeUnitMeasurments.EditValue);
			if (List_InventoryItem_UnitMeasurment == null)
				List_InventoryItem_UnitMeasurment = new List<InventoryItem_UnitMeasurment_cu>();
			List_InventoryItem_UnitMeasurment.Add(inventoryItemUnitMeasurment);
		}

		private void btnRemoveFromList_Click(object sender, EventArgs e)
		{
			if (lst_InventoryItems.SelectedItems.Count == 0 || List_InventoryItem_UnitMeasurment == null)
				return;

			InventoryItem_cu selectedInventoryItem = (InventoryItem_cu)lst_InventoryItems.SelectedItem;
			if (selectedInventoryItem == null)
				return;
			if (List_SelectedInventoryItemToBedAdded.Exists(
				item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(selectedInventoryItem.ID))))
				List_SelectedInventoryItemToBedAdded.Remove(selectedInventoryItem);

			CommonViewsActions.FillListBoxControl(lst_InventoryItems, List_SelectedInventoryItemToBedAdded);
			lst_InventoryItems.Refresh();

			InventoryItem_UnitMeasurment_cu inventoryItemUnitMeasurment =
				List_InventoryItem_UnitMeasurment.Find(
					item => Convert.ToInt32(item.InventoryItem_CU_ID).Equals(Convert.ToInt32(selectedInventoryItem.ID)));
			if (inventoryItemUnitMeasurment == null)
				return;
			List_InventoryItem_UnitMeasurment.Remove(inventoryItemUnitMeasurment);
		}

		private void gridView2_GotFocus(object sender, EventArgs e)
		{
			SelectedInventoryItemFromGrid = CommonViewsActions.GetSelectedRowObject<InventoryItem_cu>((GridView)sender);
		}

		private void gridView2_MouseUp(object sender, MouseEventArgs e)
		{
			SelectedInventoryItemFromGrid = CommonViewsActions.GetSelectedRowObject<InventoryItem_cu>((GridView)sender);
		}

		#region Implementation of IInventoryItem_UnitMeasurment_Viewer

		public List<InventoryItem_UnitMeasurment_cu> List_InventoryItem_UnitMeasurment { get; set; }

		#endregion
	}
}
