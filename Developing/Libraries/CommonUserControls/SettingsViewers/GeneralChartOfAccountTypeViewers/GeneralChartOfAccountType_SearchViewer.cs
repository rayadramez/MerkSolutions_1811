using CommonControlLibrary;
using MerkDataBaseBusinessLogicProject;
using MVCBusinessLogicLibrary.BaseViewers;
using MVCBusinessLogicLibrary.MVCFactories;
using MVCBusinessLogicLibrary.Viewers;

namespace CommonUserControls.SettingsViewers.GeneralChartOfAccountTypeViewers
{
	public partial class GeneralChartOfAccountType_SearchViewer : 
		//UserControl
		CommonAbstractSearchViewer<GeneralChartOfAccountType_cu>,
		IGeneralChartOfAccountTypeViewer
	{
		public GeneralChartOfAccountType_SearchViewer()
		{
			InitializeComponent();

			CommonViewsActions.LoadXMLFromString(layoutControl1,
				Resources.LocalizedRes.lyt_GeneralChartOfAccountType_SearchViewer);
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
		}

		public override void ClearControls()
		{
			txtNameP.EditValue = null;
			txtNameS.EditValue = null;
			txtDescription.EditValue = null;
			lkeGeneralChartOfAccountType.EditValue = null;
		}

		public override string GridXML
		{
			get { return Resources.LocalizedRes.grd_GeneralChartOfAccountType_SearchViewer; }
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

		public object ChartOfAccount_CU_ID { get; set; }

		#endregion
	}
}
