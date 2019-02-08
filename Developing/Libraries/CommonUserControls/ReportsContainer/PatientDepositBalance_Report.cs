using System.Drawing;
using ApplicationConfiguration;
using CommonControlLibrary;
using MerkDataBaseBusinessLogicProject;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MVCBusinessLogicLibrary.BaseViewers;
using MVCBusinessLogicLibrary.MVCFactories;
using MVCBusinessLogicLibrary.Viewers;

namespace CommonUserControls.ReportsContainer
{
	public partial class PatientDepositBalance_Report : 
		//UserControl
		CommonAbstractSearchViewer<GetPatientDepositBalance_Result>,
		IPatientDepositBalance_Report_Viewer
	{
		public PatientDepositBalance_Report()
		{
			InitializeComponent();

			CommonViewsActions.LoadXMLFromString(layoutControl1, Resources.LocalizedRes.lyt_PatientDeposite_Report);
			CommonViewsActions.SetupSyle(this);

			if (!string.IsNullOrEmpty(ApplicationStaticConfiguration.SkinName) &&
				!string.IsNullOrWhiteSpace(ApplicationStaticConfiguration.SkinName))
				if (!ApplicationStaticConfiguration.SkinName.Equals("McSkin"))
				{
					layoutControlGroup2.AppearanceGroup.ForeColor = Color.OldLace;
					layoutControlGroup3.AppearanceGroup.ForeColor = Color.OldLace;
				}
				else
				{
					layoutControlGroup2.AppearanceGroup.ForeColor = Color.Navy;
					layoutControlGroup3.AppearanceGroup.ForeColor = Color.Navy;
				}
		}

		#region Overrides of CommonAbstractViewer<GetPatientDepositBalance_Result>

		public override object ViewerID
		{
			get { return (int)ViewerName.PatientDepositBalance_Report_Viewer; }
		}

		public override string HeaderTitle
		{
			get { return string.Empty; }
		}

		public override string GridXML
		{
			get { return Resources.LocalizedRes.grd_PatientDepositBalance_Report; }
		}

		public override void FillControls()
		{
			CommonViewsActions.FillGridlookupEdit(lkeUsers, User_cu.ItemsList, "FullName", "Person_CU_ID");
			CommonViewsActions.FillGridlookupEdit(lkePatient, Patient_cu.ItemsList, "FullName", "Person_CU_ID");
			CommonViewsActions.FillGridlookupEdit(lkeServiceCategories, ServiceCategory_cu.ItemsList);
			CommonViewsActions.FillGridlookupEdit(lkeSerivces, Service_cu.ItemsList);
		}

		#endregion

		#region Implementation of IPatientDepositBalance_Report_Viewer

		public object PatientID
		{
			get { return lkePatient.EditValue; }
		}

		public object ServiceID
		{
			get { return lkeSerivces.EditValue; }
		}

		public object ServiceTypeID
		{
			get
			{
				if (chkExamination.Checked)
					return (int)DB_ServiceType.ExaminationService;
				if (chkInvestigation.Checked)
					return (int)DB_ServiceType.InvestigationServices;
				if (chkServiceTypeALl.Checked)
					return null;
				return null;
			}
		}

		public object ServiceCategoryID
		{
			get { return lkeServiceCategories.EditValue; }
		}

		public object IsOnDuty
		{
			get
			{
				if (chkIsOnDuty.Checked)
					return true;
				if (chkIsNotOnDuty.Checked)
					return false;
				if (chkIsOnDuty_All.Checked)
					return null;
				return null;
			}
		}

		#endregion

		private void btnGetPatient_Click(object sender, System.EventArgs e)
		{
			
		}
	}
}
