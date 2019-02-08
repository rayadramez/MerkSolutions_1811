using System;
using System.Collections.Generic;
using CommonControlLibrary;
using CommonUserControls.SettingsViewers.ChartOfAccountViewers;
using CommonUserControls.SettingsViewers.FloorViewers;
using MerkDataBaseBusinessLogicProject;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MVCBusinessLogicLibrary.BaseViewers;
using MVCBusinessLogicLibrary.MVCFactories;
using MVCBusinessLogicLibrary.Viewers;

namespace CommonUserControls.SettingsViewers.CashBoxViewers
{
	public partial class CashBox_EditorViewer : 
		//UserControl
		CommonAbstractEditorViewer<CashBox_cu>,
		ICashBoxViewer
	{
		private List<ChartOfAccount_cu> lastUsed = new List<ChartOfAccount_cu>();
		private Floor_EditorViewer _floorEditorViewer;
		private ChartOfAccount_EditorViewer _chartOfAccountEditorViewer;

		public CashBox_EditorViewer()
		{
			InitializeComponent();

			CommonViewsActions.LoadXMLFromString(layoutControl1, Resources.LocalizedRes.lyt_CashBox_EditorViewer);
			CommonViewsActions.SetupSyle(this);
		}

		#region Overrides of CommonAbstractViewer<CashBox_cu>

		public override object ViewerID
		{
			get { return (int)ViewerName.CashBoxViewer; }
		}

		public override string HeaderTitle
		{
			get { return "الخـزائــــن"; }
		}

		public override void FillControls()
		{
			CommonViewsActions.FillGridlookupEdit(lkeFloor, Floor_cu.ItemsList, "FloorFullName");
			CommonViewsActions.FillGridlookupEdit(lkeChartOfAccount,
			                                      ChartOfAccount_cu.ItemsList.FindAll(
				                                      item =>
					                                      Convert.ToInt32(item.ChartOfAccountCodeMargin_P_ID)
						                                      .Equals(Convert.ToInt32(
							                                              DB_ChartOfAccountCodeMargin.FirstMargin))));
		}

		public override void ClearControls()
		{
			txtNameP.EditValue = null;
			txtNameS.EditValue = null;
			txtInternalCode.EditValue = null;
			txtDescription.EditValue = null;
			chkIsMan.Checked = false;
			lkeFloor.EditValue = null;
			lkeChartOfAccount.EditValue = null;
		}

		#endregion

		#region Implementation of ICashBoxViewer

		public object Name_P
		{
			get { return txtNameP.EditValue; }
			set { txtNameP.EditValue = value; }
		}

		public object Name_S
		{
			get { return txtNameS.EditValue; }
			set { txtNameS.EditValue = value; }
		}

		public object Floor_CU_ID
		{
			get { return lkeFloor.EditValue; }
			set { lkeFloor.EditValue = value; }
		}

		public object IsMain
		{
			get { return chkIsMan.Checked; }
			set { chkIsMan.Checked = Convert.ToBoolean(value); }
		}

		public object ChartOfAccount_CU_ID
		{
			get { return lkeChartOfAccount.EditValue; }
			set { lkeChartOfAccount.EditValue = value; }
		}

		public object InternalCode
		{
			get { return txtInternalCode.EditValue; }
			set { txtInternalCode.EditValue = value; }
		}

		public object Description
		{
			get { return txtDescription.EditValue; }
			set { txtDescription.EditValue = value; }
		}

		#endregion

		private void lkeFloor_EditValueChanged(object sender, EventArgs e)
		{
			if (lkeFloor.EditValue == null)
				return;

			chkIsMan.Enabled = !FinancialBusinessLogicLibrary.IsFloorOfLocationHasMainCashBox(Convert.ToInt32(lkeFloor.EditValue));
		}

		private void btnNewChartOfAccount_Click(object sender, EventArgs e)
		{
			BaseController<Location_cu>.ShowEditorControl(ref _chartOfAccountEditorViewer, this, null, null,
					EditorContainerType.Regular, ViewerName.ChartOfAccountViewer, DB_CommonTransactionType.CreateNew,
					"الحسـابــــات", true);
			CommonViewsActions.FillGridlookupEdit(lkeChartOfAccount,
				ChartOfAccount_cu.ItemsList.FindAll(
					item =>
						Convert.ToInt32(item.ChartOfAccountCodeMargin_P_ID)
							.Equals(Convert.ToInt32(DB_ChartOfAccountCodeMargin.FirstMargin))));
		}

		private void btnAddFloor_Click(object sender, EventArgs e)
		{
			BaseController<Location_cu>.ShowEditorControl(ref _floorEditorViewer, this, null, null,
					EditorContainerType.Regular, ViewerName.FloorViewer, DB_CommonTransactionType.CreateNew,
					"أدوار المنظمـــــة", true);
			CommonViewsActions.FillGridlookupEdit(lkeFloor, Floor_cu.ItemsList, "FloorFullName");
		}

		private void lkeChartOfAccount_EditValueChanged(object sender, EventArgs e)
		{
			if (lkeChartOfAccount.EditValue == null)
			{
				CommonViewsActions.FillGridlookupEdit(lkeChartOfAccount,
													  ChartOfAccount_cu.ItemsList.FindAll(
														  item => Convert.ToInt32(item.ChartOfAccountCodeMargin_P_ID)
															  .Equals(Convert.ToInt32(
																		  DB_ChartOfAccountCodeMargin.FirstMargin))));
				return;
			}

			ChartOfAccount_cu chartOfAccount =
				ChartOfAccount_cu.ItemsList.Find(item => Convert.ToInt32(item.ID)
													 .Equals(Convert.ToInt32(lkeChartOfAccount.EditValue)));
			if (chartOfAccount == null)
			{
				CommonViewsActions.FillGridlookupEdit(lkeChartOfAccount,
													  ChartOfAccount_cu.ItemsList.FindAll(
														  item => Convert.ToInt32(item.ChartOfAccountCodeMargin_P_ID)
															  .Equals(Convert.ToInt32(
																		  DB_ChartOfAccountCodeMargin.FirstMargin))));
				return;
			}

			List<ChartOfAccount_cu> list = ChartOfAccount_cu.ItemsList.FindAll(
				item => Convert.ToInt32(item.ParentChartOfAccount_CU_ID).Equals(Convert.ToInt32(chartOfAccount.ID)));
			if (list.Count > 0)
			{
				CommonViewsActions.FillGridlookupEdit(lkeChartOfAccount, list);
				lastUsed = list;
			}
			else
			{
				CommonViewsActions.FillGridlookupEdit(lkeChartOfAccount, lastUsed);
			}
		}
	}
}
