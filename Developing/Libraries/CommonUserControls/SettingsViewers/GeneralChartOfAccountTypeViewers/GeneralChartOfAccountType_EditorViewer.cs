using System;
using CommonControlLibrary;
using CommonUserControls.SettingsViewers.ChartOfAccountViewers;
using MerkDataBaseBusinessLogicProject;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MVCBusinessLogicLibrary.BaseViewers;
using MVCBusinessLogicLibrary.MVCFactories;
using MVCBusinessLogicLibrary.Viewers;

namespace CommonUserControls.SettingsViewers.GeneralChartOfAccountTypeViewers
{
	public partial class GeneralChartOfAccountType_EditorViewer : 
		//UserControl
		CommonAbstractEditorViewer<GeneralChartOfAccountType_cu>,
		IGeneralChartOfAccountTypeViewer
	{
		private ChartOfAccount_EditorViewer _chartOfAccountEditorViewer;

		public GeneralChartOfAccountType_EditorViewer()
		{
			InitializeComponent();

			CommonViewsActions.LoadXMLFromString(layoutControl1,
				Resources.LocalizedRes.lyt_GeneralChartOfAccountType_EditorViewer);
			CommonViewsActions.SetupSyle(this);
		}

		#region Overrides of CommonAbstractViewer<GeneralChartOfAccountType_cu>

		public override object ViewerID
		{
			get { return (int)ViewerName.GeneralChartOfAccountType_Viewer; }
		}

		public override string HeaderTitle
		{
			get { return "أنــواع المعـامــــلات المـاليــــة"; }
		}

		public override void FillControls()
		{
			CommonViewsActions.FillGridlookupEdit(lkeGeneralChartOfAccountType, GeneralChartOfAccountType_p.ItemsList);
			CommonViewsActions.FillGridlookupEdit(lkeChartOfAccount,
				ChartOfAccount_cu.ItemsList.FindAll(
					item =>
						Convert.ToInt32(item.ChartOfAccountCodeMargin_P_ID)
							.Equals(Convert.ToInt32(DB_ChartOfAccountCodeMargin.FifthMargin))));
		}

		public override void ClearControls()
		{
			txtNameP.EditValue = null;
			txtNameS.EditValue = null;
			txtDescription.EditValue = null;
			lkeGeneralChartOfAccountType.EditValue = null;
		}

		#endregion

		#region Implementation of IGeneralChartOfAccountTypeViewer

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

		public object GeneralChartOfAccountType_P_ID
		{
			get { return lkeGeneralChartOfAccountType.EditValue; }
			set { lkeGeneralChartOfAccountType.EditValue = value; }
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

		private void btnNewChartOfAccount_Click(object sender, EventArgs e)
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
