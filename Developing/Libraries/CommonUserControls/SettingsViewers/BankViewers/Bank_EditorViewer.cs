using System;
using System.Collections.Generic;
using CommonControlLibrary;
using MerkDataBaseBusinessLogicProject;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MVCBusinessLogicLibrary.BaseViewers;
using MVCBusinessLogicLibrary.MVCFactories;
using MVCBusinessLogicLibrary.Viewers;

namespace CommonUserControls.SettingsViewers.BankViewers
{
	public partial class Bank_EditorViewer : 
		//DevExpress.XtraEditors.XtraUserControl
		CommonAbstractEditorViewer<Bank_cu>,
		IBankViewer
	{
		private List<ChartOfAccount_cu> lastUsed = new List<ChartOfAccount_cu>();

		public Bank_EditorViewer()
		{
			InitializeComponent();
		}

		private void Bank_EditorViewer_Load(object sender, EventArgs e)
		{
			CommonViewsActions.LoadXMLFromString(layoutControl1, Resources.LocalizedRes.lyt_Bank_EditorViewer);
			CommonViewsActions.SetupSyle(this);
		}

		#region Overrides of CommonAbstractViewer<Bank_cu>

		public override object ViewerID
		{
			get { return (int)ViewerName.Bank_Viewer; }
		}

		public override string HeaderTitle
		{
			get { return "البنــــوك"; }
		}

		public override void FillControls()
		{
			CommonViewsActions.FillGridlookupEdit(lkeChartOfAccount,
			                                      ChartOfAccount_cu.ItemsList.FindAll(
				                                      item => Convert.ToInt32(item.ChartOfAccountCodeMargin_P_ID)
					                                      .Equals(Convert.ToInt32(
						                                              DB_ChartOfAccountCodeMargin.FirstMargin))));
		}

		public override void ClearControls()
		{
			txtNameP.EditValue = null;
			txtNameS.EditValue = null;
			lkeChartOfAccount.EditValue = null;
			txtDescription.EditValue = null;
		}

		#endregion

		#region Implementation of IBankViewer

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

		public object Description
		{
			get { return txtDescription.EditValue; }
			set { txtDescription.EditValue = value; }
		}

		public object ChartOfAccount_CU_ID
		{
			get { return lkeChartOfAccount.EditValue; }
			set { lkeChartOfAccount.EditValue = value; }
		}

		#endregion

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
			if(chartOfAccount == null)
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
