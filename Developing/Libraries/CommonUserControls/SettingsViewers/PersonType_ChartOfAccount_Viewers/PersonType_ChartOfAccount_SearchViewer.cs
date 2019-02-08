using System;
using CommonControlLibrary;
using MerkDataBaseBusinessLogicProject;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MVCBusinessLogicLibrary.BaseViewers;
using MVCBusinessLogicLibrary.MVCFactories;
using MVCBusinessLogicLibrary.Viewers;

namespace CommonUserControls.SettingsViewers.PersonType_ChartOfAccount_Viewers
{
	public partial class PersonType_ChartOfAccount_SearchViewer : 
		//UserControl
		CommonAbstractSearchViewer<PersonType_ChartOfAccount_cu>,
		IPersonType_ChartOfAccount_Viewer
	{
		public PersonType_ChartOfAccount_SearchViewer()
		{
			InitializeComponent();
		}

		private void PersonType_ChartOfAccount_SearchViewer_Load(object sender, EventArgs e)
		{
			CommonViewsActions.LoadXMLFromString(layoutControl1, Resources.LocalizedRes.lyt_PersonType_ChartOfAccount_SearchViewer);
			CommonViewsActions.SetupSyle(this);
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

		public override string GridXML
		{
			get { return Resources.LocalizedRes.grd_PersonType_ChartOfAccount_SearchViewer; }
		}

		#endregion

		#region Implementation of IPersonType_ChartOfAccount_Viewer

		public object PersonType_P_ID { get; set; }
		public object ChartOfAccount_CU_ID { get; set; }
		public object PersonChartOfAccountType_P_ID { get; set; }

		#endregion
	}
}
