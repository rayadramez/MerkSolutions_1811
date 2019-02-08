using System;
using System.Collections.Generic;
using CommonControlLibrary;
using CommonUserControls.SettingsViewers.ChartOfAccountViewers;
using MerkDataBaseBusinessLogicProject;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MVCBusinessLogicLibrary.BaseViewers;
using MVCBusinessLogicLibrary.MVCFactories;
using MVCBusinessLogicLibrary.Viewers;

namespace CommonUserControls.SettingsViewers.PersonType_ChartOfAccount_Viewers
{
	public partial class PersonType_ChartOfAccount_EditorViewer : 
		//UserControl
		CommonAbstractEditorViewer<PersonType_ChartOfAccount_cu>,
		IPersonType_ChartOfAccount_Viewer
	{
		private List<ChartOfAccount_cu> lastUsed = new List<ChartOfAccount_cu>();
		private ChartOfAccount_EditorViewer _chartOfAccountEditorViewer;

		public PersonType_ChartOfAccount_EditorViewer()
		{
			InitializeComponent();
		}

		private void PersonType_ChartOfAccount_EditorViewer_Load(object sender, System.EventArgs e)
		{
			CommonViewsActions.LoadXMLFromString(layoutControl1, Resources.LocalizedRes.lyt_PersonType_ChartOfAccount_EditorViewer);
			CommonViewsActions.SetupSyle(this);
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

		private void chkCustomer_CheckedChanged(object sender, System.EventArgs e)
		{

		}

		private void chkSupplier_CheckedChanged(object sender, System.EventArgs e)
		{

		}

		private void chkPatient_CheckedChanged(object sender, System.EventArgs e)
		{

		}

		#region Overrides of CommonAbstractViewer<PersonType_ChartOfAccount_cu>

		public override object ViewerID
		{
			get { return (int)ViewerName.PersonType_ChartOfAccount_Viewer; }
		}

		public override string HeaderTitle
		{
			get { return "ربــط الحسـابـــات بنــوع الشخــــص"; }
		}

		public override void FillControls()
		{
			CommonViewsActions.FillGridlookupEdit(lkePersonChartOfAccountType, PersonChartOtAccountType_p.ItemsList);
			CommonViewsActions.FillGridlookupEdit(lkeChartOfAccount,
												  ChartOfAccount_cu.ItemsList.FindAll(
													  item =>
														  Convert.ToInt32(item.ChartOfAccountCodeMargin_P_ID)
															  .Equals(Convert.ToInt32(
																		  DB_ChartOfAccountCodeMargin.FirstMargin))));
		}

		public override void ClearControls()
		{
			lkeChartOfAccount.EditValue = null;
			lkePersonChartOfAccountType.EditValue = null;
			chkCustomer.Checked = true;
		}

		#endregion

		#region Implementation of IPersonType_ChartOfAccount_Viewer

		public object PersonType_P_ID
		{
			get
			{
				if (chkCustomer.Checked)
					return (int) DB_PersonType.Customer;
				if (chkPatient.Checked)
					return (int)DB_PersonType.Patient;
				if (chkSupplier.Checked)
					return (int)DB_PersonType.Supplier;
				return (int)DB_PersonType.None;
			}
			set
			{
				DB_PersonType persontype = (DB_PersonType) value;
				switch (persontype)
				{
					case DB_PersonType.Customer:
						chkCustomer.Checked = true;
						break;
					case DB_PersonType.Supplier:
						chkSupplier.Checked = true;
						break;
					case DB_PersonType.Patient:
						chkPatient.Checked = true;
						break;
				}
			}
		}

		public object ChartOfAccount_CU_ID
		{
			get { return lkeChartOfAccount.EditValue; }
			set { lkeChartOfAccount.EditValue = value; }
		}

		public object PersonChartOfAccountType_P_ID
		{
			get { return lkePersonChartOfAccountType.EditValue; }
			set { lkePersonChartOfAccountType.EditValue = value; }
		}

		#endregion

		private void btnChartOfAccount_Click(object sender, EventArgs e)
		{
			BaseController<Location_cu>.ShowEditorControl(ref _chartOfAccountEditorViewer, this, null, null,
														  EditorContainerType.Regular, ViewerName.ChartOfAccountViewer, DB_CommonTransactionType.CreateNew,
														  "الحسـابــــات", true);
			CommonViewsActions.FillGridlookupEdit(lkeChartOfAccount,
												  ChartOfAccount_cu.ItemsList.FindAll(
													  item =>
														  Convert.ToInt32(item.ChartOfAccountCodeMargin_P_ID)
															  .Equals(Convert.ToInt32(DB_ChartOfAccountCodeMargin.FifthMargin))));
		}
	}
}
