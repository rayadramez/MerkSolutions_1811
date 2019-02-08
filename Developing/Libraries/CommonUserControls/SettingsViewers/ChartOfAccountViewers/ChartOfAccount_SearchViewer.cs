using System;
using CommonControlLibrary;
using MerkDataBaseBusinessLogicProject;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MVCBusinessLogicLibrary.BaseViewers;
using MVCBusinessLogicLibrary.MVCFactories;
using MVCBusinessLogicLibrary.Viewers;

namespace CommonUserControls.SettingsViewers.ChartOfAccountViewers
{
	public partial class ChartOfAccount_SearchViewer :
		//DevExpress.XtraEditors.XtraUserControl
		CommonAbstractSearchViewer<ChartOfAccount_cu>,
		IChartOfAccountViewer
	{
		public ChartOfAccount_SearchViewer()
		{
			InitializeComponent();

			CommonViewsActions.LoadXMLFromString(layoutControl1, Resources.LocalizedRes.lyt_ChartOfAccount_SearchViewer);
			CommonViewsActions.SetupSyle(this);
		}

		#region Overrides of CommonAbstractViewer<ChartOfAccount_cu>

		public override object ViewerID
		{
			get { return (int)ViewerName.ChartOfAccountViewer; }
		}

		public override string HeaderTitle
		{
			get { return "الحســـابات"; }
		}

		public override void FillControls()
		{
			CommonViewsActions.FillGridlookupEdit(lkeChartOfAccountCodeMargin, ChartOfAccountCodeMargin_p.ItemsList);
		}

		public override void ClearControls()
		{
			txtNameP.EditValue = null;
			txtNameS.EditValue = null;
			lkeParentChartOfAccount.EditValue = null;
			txtDescription.EditValue = null;
			lkeChartOfAccountCodeMargin.EditValue = null;
			chkDebit.Checked = true;
			txtSerial.EditValue = null;
		}

		public override string GridXML
		{
			get { return Resources.LocalizedRes.grd_ChartOfAccount_SearchViewer; }
		}

		#endregion

		#region Implementation of IChartOfAccountViewer

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

		public object ParentChartOfAccount_CU_ID
		{
			get { return lkeParentChartOfAccount.EditValue; }
			set { lkeParentChartOfAccount.EditValue = value; }
		}

		public object Serial
		{
			get { return txtSerial.EditValue; }
			set { txtSerial.EditValue = value; }
		}

		public object ChartOfAccountCodeMargin_P_ID
		{
			get { return lkeChartOfAccountCodeMargin.EditValue; }
			set { lkeChartOfAccountCodeMargin.EditValue = value; }
		}

		public object IsDebit
		{
			get { return chkDebit.Checked; }
			set { chkDebit.Checked = Convert.ToBoolean(value); }
		}

		public object Description
		{
			get { return txtDescription.EditValue; }
			set { txtDescription.EditValue = value; }
		}

		#endregion

		private void lkeChartOfAccountCodeMargin_EditValueChanged(object sender, EventArgs e)
		{
			CommonViewsActions.FillGridlookupEdit(lkeParentChartOfAccount,
				AccountingBusinessLogicEngine.GetChartOfAccountOfPreviousCodeMargin(lkeChartOfAccountCodeMargin.EditValue,
					chkDebit.Checked));
		}

		private void chkDebit_CheckedChanged(object sender, EventArgs e)
		{
			CommonViewsActions.FillGridlookupEdit(lkeParentChartOfAccount,
				AccountingBusinessLogicEngine.GetChartOfAccountOfPreviousCodeMargin(lkeChartOfAccountCodeMargin.EditValue,
					chkDebit.Checked));
		}

		private void chkCredit_CheckedChanged(object sender, EventArgs e)
		{
			CommonViewsActions.FillGridlookupEdit(lkeParentChartOfAccount,
				AccountingBusinessLogicEngine.GetChartOfAccountOfPreviousCodeMargin(lkeChartOfAccountCodeMargin.EditValue,
					chkDebit.Checked));
		}
	}
}
