using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CommonControlLibrary;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using MerkDataBaseBusinessLogicProject;
using MVCBusinessLogicLibrary.BaseViewers;
using MVCBusinessLogicLibrary.MVCFactories;
using MVCBusinessLogicLibrary.Viewers;

namespace CommonUserControls.SettingsViewers.InventoryItem_InventoryItemGroupViewers
{
	public partial class InventoryItemGroup_InventoryItem_EditorViewer :
		//DevExpress.XtraEditors.XtraUserControl
		CommonAbstractEditorViewer<InventoryItemGroup_InventoryItem_cu>,
		IInventoryItemGroup_InventoryItem_Viewer
	{
		private InventoryItemGroup_cu SelectedInventoryGroupFromGrid = null;
		private List<InventoryItemGroup_cu> List_SelectedUInventoryGroupsToBedAdded = null;

		public InventoryItemGroup_InventoryItem_EditorViewer()
		{
			InitializeComponent();

			CommonViewsActions.LoadXMLFromString(layoutControl1,
				Resources.LocalizedRes.lyt_InventoryItemGroup_InventoryItem_EditorViewer);
			CommonViewsActions.SetupGridControl(grdInventoryGroupItems,
				Resources.LocalizedRes.grd_InventoryItemGroup_Internal, true);
			CommonViewsActions.SetupSyle(this);
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

		public override void FillControls()
		{
			grdInventoryGroupItems.DataSource = InventoryItemGroup_cu.ItemsList;
			CommonViewsActions.FillGridlookupEdit(lkeInventoryItems, InventoryItem_cu.ItemsList);
		}

		public override void ClearControls()
		{
			txtInternalCode.EditValue = null;
			txtInventoryGroupName.EditValue = null;
			lst_InventoryGroups.DataSource = null;
			lkeInventoryItems.EditValue = null;
			SelectedInventoryGroupFromGrid = null;
			List_SelectedUInventoryGroupsToBedAdded = null;
			List_InventoryItemGroup_InventoryItem = null;
		}

		#endregion

		#region Implementation of IInventoryItemGroup_InventoryItem_Viewer

		public List<InventoryItemGroup_InventoryItem_cu> List_InventoryItemGroup_InventoryItem { get; set; }

		#endregion

		private void txtInternalCode_EditValueChanged(object sender, System.EventArgs e)
		{
			if (txtInternalCode.EditValue == null || string.IsNullOrEmpty(txtInternalCode.Text) ||
			    string.IsNullOrWhiteSpace(txtInternalCode.Text))
			{
				grdInventoryGroupItems.DataSource = InventoryItemGroup_cu.ItemsList;
				return;
			}

			grdInventoryGroupItems.DataSource =
				InventoryItemGroup_cu.ItemsList.FindAll(
					item =>
						item.InternalCode != null &&
						Convert.ToString(item.InternalCode).Contains(Convert.ToString(txtInternalCode.Text)));
		}

		private void txtInventoryGroupName_EditValueChanged(object sender, System.EventArgs e)
		{
			if (txtInventoryGroupName.EditValue == null || string.IsNullOrEmpty(txtInventoryGroupName.Text) ||
				string.IsNullOrWhiteSpace(txtInventoryGroupName.Text))
			{
				grdInventoryGroupItems.DataSource = InventoryItemGroup_cu.ItemsList;
				return;
			}

			grdInventoryGroupItems.DataSource =
				InventoryItemGroup_cu.ItemsList.FindAll(
					item => item.Name_P != null && Convert.ToString(item.Name_P)
						        .Contains(Convert.ToString(txtInventoryGroupName.Text)));
		}

		private void btnAddList_Click(object sender, EventArgs e)
		{
			if (lkeInventoryItems.EditValue == null)
			{
				XtraMessageBox.Show("يجـب إختيــار المنتــــــج", "تنبيــــــــــه", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return;
			}

			if (SelectedInventoryGroupFromGrid == null)
			{
				XtraMessageBox.Show("يجب إختيــار مجمـوعــة المستخـدميـــن", "تنبيــــــــــه", MessageBoxButtons.OK,
					MessageBoxIcon.Hand);
				return;
			}

			if (List_SelectedUInventoryGroupsToBedAdded == null)
				List_SelectedUInventoryGroupsToBedAdded = new List<InventoryItemGroup_cu>();

			if (List_SelectedUInventoryGroupsToBedAdded.Count > 0)
				if (List_SelectedUInventoryGroupsToBedAdded.Exists(
					item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(SelectedInventoryGroupFromGrid.ID))))
				{
					XtraMessageBox.Show("تمــت إضافتـــه مـن قبـــل", "تنبيــــــــــه", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					return;
				}

			List_SelectedUInventoryGroupsToBedAdded.Add(SelectedInventoryGroupFromGrid);
			CommonViewsActions.FillListBoxControl(lst_InventoryGroups, List_SelectedUInventoryGroupsToBedAdded);
			lst_InventoryGroups.Refresh();

			InventoryItemGroup_InventoryItem_cu groupBridge = new InventoryItemGroup_InventoryItem_cu();
			groupBridge.InvetoryItemGroup_CU_ID = SelectedInventoryGroupFromGrid.ID;
			groupBridge.InventoryItem_CU_ID = Convert.ToInt32(lkeInventoryItems.EditValue);
			if (List_InventoryItemGroup_InventoryItem == null)
				List_InventoryItemGroup_InventoryItem = new List<InventoryItemGroup_InventoryItem_cu>();
			List_InventoryItemGroup_InventoryItem.Add(groupBridge);
		}

		private void btnRemoveFromList_Click(object sender, EventArgs e)
		{
			if (lst_InventoryGroups.SelectedItems.Count == 0 || List_InventoryItemGroup_InventoryItem == null)
				return;

			InventoryItemGroup_cu selecteduserGroup = (InventoryItemGroup_cu)lst_InventoryGroups.SelectedItem;
			if (selecteduserGroup == null)
				return;
			if (List_SelectedUInventoryGroupsToBedAdded.Exists(
				item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(selecteduserGroup.ID))))
				List_SelectedUInventoryGroupsToBedAdded.Remove(selecteduserGroup);

			CommonViewsActions.FillListBoxControl(lst_InventoryGroups, List_SelectedUInventoryGroupsToBedAdded);
			lst_InventoryGroups.Refresh();

			InventoryItemGroup_InventoryItem_cu groupBridge = List_InventoryItemGroup_InventoryItem.Find(item =>
				Convert.ToInt32(item.InvetoryItemGroup_CU_ID).Equals(Convert.ToInt32(selecteduserGroup.ID)));

			if (groupBridge == null)
				return;

			List_InventoryItemGroup_InventoryItem.Remove(groupBridge);
		}

		private void gridView1_MouseUp(object sender, MouseEventArgs e)
		{
			SelectedInventoryGroupFromGrid =
				CommonViewsActions.GetSelectedRowObject<InventoryItemGroup_cu>((GridView) sender);
		}

		private void gridView1_GotFocus(object sender, EventArgs e)
		{
			SelectedInventoryGroupFromGrid =
				CommonViewsActions.GetSelectedRowObject<InventoryItemGroup_cu>((GridView) sender);
		}

		private void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
		{
			SelectedInventoryGroupFromGrid =
				CommonViewsActions.GetSelectedRowObject<InventoryItemGroup_cu>((GridView) sender);
		}

		private void gridView1_DoubleClick(object sender, EventArgs e)
		{
			SelectedInventoryGroupFromGrid =
				CommonViewsActions.GetSelectedRowObject<InventoryItemGroup_cu>((GridView) sender);
		}
	}
}
