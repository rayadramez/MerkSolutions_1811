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

namespace CommonUserControls.SettingsViewers.CashBoxTranasctionType_GeneralChartOfAccountType_Viewers
{
	public partial class CashBoxTranasctionType_GeneralChartOfAccountType_EditorViewer : 
		//UserControl
		CommonAbstractEditorViewer<CashBoxTransactionType_GeneralChartOfAccountType_cu>,
		ICashBoxTransactionType_GeneralChartOfAccountType_Viewer
	{
		private GeneralChartOfAccountType_cu SelectedGeneralChartOfAccountTypeFromGrid = null;
		private List<GeneralChartOfAccountType_cu> List_SelectedGeneralChartOfAccountTypeToBedAdded = null;

		public CashBoxTranasctionType_GeneralChartOfAccountType_EditorViewer()
		{
			InitializeComponent();

			CommonViewsActions.LoadXMLFromString(layoutControl1,
				Resources.LocalizedRes.lyt_CashBoxTransactionType_GeneralChartOfAccountType_EditorViewer);
			CommonViewsActions.SetupSyle(this);
			CommonViewsActions.SetupGridControl(grdAllChartOfAccountTypes, Resources.LocalizedRes.grd_GeneralChartOfAccountType_Internal,
				false);
		}

		#region Overrides of CommonAbstractViewer<CashBoxTransactionType_GeneralChartOfAccountType_cu>

		public override object ViewerID
		{
			get { return (int)ViewerName.CashBoxTransactionType_GeneralChartOfAccountType_Viewer; }
		}

		public override string HeaderTitle
		{
			get { return "ربـــط المعـامـــلات الماليـــة بعمليـــات الخزائـــن"; }
		}

		public override void FillControls()
		{
			grdAllChartOfAccountTypes.DataSource = GeneralChartOfAccountType_cu.ItemsList;
			CommonViewsActions.FillGridlookupEdit(lkeCashBoxTranasctionType, CashBoxTransactionType_p.ItemsList);
		}

		public override void ClearControls()
		{
			lkeCashBoxTranasctionType.EditValue = null;
		}

		#endregion

		#region Implementation of IInventoryItem_UnitMeasurment_Viewer

		public List<CashBoxTransactionType_GeneralChartOfAccountType_cu> List_CashBoxTransactionType_GeneralChartOfAccountType { get; set; }

		#endregion

		private void btnAddList_Click(object sender, EventArgs e)
		{
			if (lkeCashBoxTranasctionType.EditValue == null)
			{
				XtraMessageBox.Show("يجـب إختيــار عمليــة الخزينــة الرئيسيــة", "تنبيــــــــــه", MessageBoxButtons.OK,
					MessageBoxIcon.Hand);
				return;
			}

			if (SelectedGeneralChartOfAccountTypeFromGrid == null)
			{
				XtraMessageBox.Show("يجب إختيــار مجمـوعــة المستخـدميـــن", "تنبيــــــــــه", MessageBoxButtons.OK,
					MessageBoxIcon.Hand);
				return;
			}

			if (List_SelectedGeneralChartOfAccountTypeToBedAdded == null)
				List_SelectedGeneralChartOfAccountTypeToBedAdded = new List<GeneralChartOfAccountType_cu>();

			if (List_SelectedGeneralChartOfAccountTypeToBedAdded.Count > 0)
				if (List_SelectedGeneralChartOfAccountTypeToBedAdded.Exists(
						item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(SelectedGeneralChartOfAccountTypeFromGrid.ID))))
				{
					XtraMessageBox.Show("تمــت إضافتـــه مـن قبـــل", "تنبيــــــــــه", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					return;
				}

			List_SelectedGeneralChartOfAccountTypeToBedAdded.Add(SelectedGeneralChartOfAccountTypeFromGrid);
			CommonViewsActions.FillListBoxControl(lst_GeneralChartOfAccountTypes, List_SelectedGeneralChartOfAccountTypeToBedAdded);
			lst_GeneralChartOfAccountTypes.Refresh();

			CashBoxTransactionType_GeneralChartOfAccountType_cu itemToBeAdded = new CashBoxTransactionType_GeneralChartOfAccountType_cu();
			itemToBeAdded.GeneralChartOfAccountType_CU_ID = SelectedGeneralChartOfAccountTypeFromGrid.ID;
			itemToBeAdded.CashBoxTransactionType_P_ID = Convert.ToInt32(lkeCashBoxTranasctionType.EditValue);
			if (List_CashBoxTransactionType_GeneralChartOfAccountType == null)
				List_CashBoxTransactionType_GeneralChartOfAccountType = new List<CashBoxTransactionType_GeneralChartOfAccountType_cu>();
			List_CashBoxTransactionType_GeneralChartOfAccountType.Add(itemToBeAdded);
		}

		private void btnRemoveFromList_Click(object sender, EventArgs e)
		{
			if (lst_GeneralChartOfAccountTypes.SelectedItems.Count == 0 || List_CashBoxTransactionType_GeneralChartOfAccountType == null)
				return;

			GeneralChartOfAccountType_cu selectedGerneralChartOfAccountType =
				(GeneralChartOfAccountType_cu) lst_GeneralChartOfAccountTypes.SelectedItem;
			if (selectedGerneralChartOfAccountType == null)
				return;
			if (List_SelectedGeneralChartOfAccountTypeToBedAdded.Exists(
				item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(selectedGerneralChartOfAccountType.ID))))
				List_SelectedGeneralChartOfAccountTypeToBedAdded.Remove(selectedGerneralChartOfAccountType);

			CommonViewsActions.FillListBoxControl(lst_GeneralChartOfAccountTypes,
				List_SelectedGeneralChartOfAccountTypeToBedAdded);
			lst_GeneralChartOfAccountTypes.Refresh();

			CashBoxTransactionType_GeneralChartOfAccountType_cu inventoryItemUnitMeasurment =
				List_CashBoxTransactionType_GeneralChartOfAccountType.Find(
					item =>
						Convert.ToInt32(item.GeneralChartOfAccountType_CU_ID)
							.Equals(Convert.ToInt32(selectedGerneralChartOfAccountType.ID)));
			if (inventoryItemUnitMeasurment == null)
				return;
			List_CashBoxTransactionType_GeneralChartOfAccountType.Remove(inventoryItemUnitMeasurment);
		}

		private void gridView2_MouseUp(object sender, MouseEventArgs e)
		{
			SelectedGeneralChartOfAccountTypeFromGrid =
				CommonViewsActions.GetSelectedRowObject<GeneralChartOfAccountType_cu>((GridView) sender);
		}

	}
}
