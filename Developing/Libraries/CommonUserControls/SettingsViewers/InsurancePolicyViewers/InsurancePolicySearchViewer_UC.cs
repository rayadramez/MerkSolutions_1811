using CommonControlLibrary;
using MerkDataBaseBusinessLogicProject;
using MVCBusinessLogicLibrary.BaseViewers;
using MVCBusinessLogicLibrary.MVCFactories;
using MVCBusinessLogicLibrary.Viewers;

namespace CommonUserControls.SettingsViewers.InsurancePolicyViewers
{
	public partial class InsurancePolicySearchViewer_UC : 
		//UserControl
		CommonAbstractSearchViewer<InsuranceCarrier_InsuranceLevel_cu>,
		IInsurancePolicyViewer
	{
		public InsurancePolicySearchViewer_UC()
		{
			InitializeComponent();
			CommonViewsActions.SetupSyle(this);
			CommonViewsActions.LoadXMLFromString(layoutControl1, Resources.LocalizedRes.lyt_InsurancePolicySearchViewer_UC);
		}

		public override void FillControls()
		{
			CommonViewsActions.FillGridlookupEdit(lkeInsuranceCarrier, InsuranceCarrier_cu.ItemsList);
			CommonViewsActions.FillGridlookupEdit(lkeInsuranceLevel, InsuranceLevel_cu.ItemsList);
			spnInsurancePercentage.EditValue = 0;
			spnPatientMaxAmount.EditValue = null;
		}

		#region Overrides of CommonAbstractViewer<InsuranceCarrier_InsuranceLevel_cu>

		public override object ViewerID
		{
			get { return (int)ViewerName.InsurancePolicy_Viewer; }
		}

		public override string HeaderTitle
		{
			get { return "سياســات جهــات التأميـــن"; }
		}

		public override string GridXML
		{
			get { return Resources.LocalizedRes.grd_InsurancePolicySearchViewer; }
		}

		#endregion

		#region Implementation of IInsurancePolicyViewer

		public object InsuranceCarrierID
		{
			get { return lkeInsuranceCarrier.EditValue; }
			set { lkeInsuranceCarrier.EditValue = value; }
		}

		public object InsuranceLevelID
		{
			get { return lkeInsuranceLevel.EditValue; }
			set { lkeInsuranceLevel.EditValue = value; }
		}

		public object InsurancePercetnage
		{
			get { return spnInsurancePercentage.EditValue; }
			set { spnInsurancePercentage.EditValue = value; }
		}

		public object PatientMaxAmount
		{
			get { return spnPatientMaxAmount.EditValue; }
			set { spnPatientMaxAmount.EditValue = value; }
		}

		#endregion
	}
}
