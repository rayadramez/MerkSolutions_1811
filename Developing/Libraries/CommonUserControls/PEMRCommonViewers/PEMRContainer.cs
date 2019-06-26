using ApplicationConfiguration;
using CommonControlLibrary;
using CommonUserControls.CommonViewers;
using CommonUserControls.PEMRCommonViewers.PEMR_InternalViewers;
using CommonUserControls.Reports;
using DevExpress.XtraEditors;
using DevExpress.XtraLayout.Utils;
using MerkDataBaseBusinessLogicProject;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;

namespace CommonUserControls.PEMRCommonViewers
{
	public partial class PEMRContainer : XtraUserControl
	{
		private PEMRContainer _pemrContainer;
		private PEMR_PatientCardContainer_UC _pemrQueueContainer;
		private PEMR_PreviousVisitCardContainer_UC _previousVisitCardContainer;
		private PEMR_ServingContainer_UC _pemrServingContainer;
		public GetBriefQueue_Result QueueResult { get; set; }
		public VisitTiming ActiveVisitTiming { get; set; }
		private PEMR_MedicalRecordContainer _medicalRecordContainer;
		private PEMR_InvestigationContainer _investigationContainer;
		private ScanFiles_UC _scanFiles;

		public int InvoiceDetailID { get; set; }

		public PEMRContainer()
		{
			InitializeComponent();
			PEMR_PatientCardContainer_UC.ParentContainer = this;
			if (ApplicationStaticConfiguration.ActiveLoginUser != null)
				PEMRBusinessLogic.ActiveLoggedInUser = ApplicationStaticConfiguration.ActiveLoginUser;
			lytContainer.Visibility = LayoutVisibility.Always;
			CommonViewsActions.ShowUserControl(ref _pemrQueueContainer, splitContainerControl1.Panel1);
			if (ApplicationStaticConfiguration.ActiveLoginUser != null &&
				ApplicationStaticConfiguration.ActiveLoginUser.FullName != null)
				btnUserDropDown.Text = PEMRBusinessLogic.ActiveLoggedInUser.FullName.ToString();
		}

		public void ShowLeftQueuePanel(bool doCollapse)
		{
			if (ApplicationStaticConfiguration.ActiveLoginUser != null &&
				ApplicationStaticConfiguration.ActiveLoginUser.FullName != null)
				btnUserDropDown.Text = PEMRBusinessLogic.ActiveLoggedInUser.FullName.ToString();
			splitContainerControl1.PanelVisibility =
				doCollapse ? SplitPanelVisibility.Panel2 : SplitPanelVisibility.Both;
		}

		public void ShowPEMRHistoryContainer(GetBriefQueue_Result queueResult, VisitTiming visitTiming, bool doShow)
		{
			if (doShow)
			{
				QueueResult = queueResult;
				ActiveVisitTiming = visitTiming;
				InvoiceDetailID = QueueResult.InvoiceDetailID;
				switch (ApplicationStaticConfiguration.Application)
				{
					case DB_Application.PEMR:
						CommonViewsActions.ShowUserControl(ref _pemrServingContainer, splitContainerControl1.Panel2);
						if (_pemrServingContainer != null)
						{
							DB_QueueManagerStatus queueManagerStatus = (DB_QueueManagerStatus)queueResult.QueueStatusID;
							switch (queueManagerStatus)
							{
								case DB_QueueManagerStatus.Waiting:
									PEMRBusinessLogic.ActivePEMRObject = PEMRBusinessLogic.GetPEMRObject(InvoiceDetailID);
									PEMRBusinessLogic.ActivePEMRObject.PEMRStatus = PEMRStatus.CreateNewVisit;
									break;
							}

							_pemrServingContainer.InitializePatientInfo(this, QueueResult, ActiveVisitTiming,
								PEMRBusinessLogic.ActivePEMRObject);
							MerkDBBusinessLogicEngine.UpdateAndSave_QueueManagerStatus(queueResult.QueueManagerID,
								DB_QueueManagerStatus.Serving);
							lytPatientQueue.Visibility = LayoutVisibility.Never;
							lytPreviousVisits.Visibility = LayoutVisibility.Never;
							emptySpaceItem1.Visibility = LayoutVisibility.Never;
							if (ApplicationStaticConfiguration.ActiveLoginUser != null &&
							    ApplicationStaticConfiguration.ActiveLoginUser.FullName != null)
								btnUserDropDown.Text = PEMRBusinessLogic.ActiveLoggedInUser.FullName.ToString();
						}
						break;
					case DB_Application.OphalmologySurgeryApplication:

						break;
				}
			}
			else
			{
				splitContainerControl1.Panel2.Controls.Clear();
				_pemrQueueContainer.Initialize(MerkDBBusinessLogicEngine.ActiveStationPointStage);
				lytPatientQueue.Visibility = LayoutVisibility.Always;
				lytPreviousVisits.Visibility = LayoutVisibility.Always;
				emptySpaceItem1.Visibility = LayoutVisibility.Always;
			}
		}

		private void btnPatientQueue_Click(object sender, System.EventArgs e)
		{
			splitContainerControl1.Panel1.Controls.Clear();
			splitContainerControl1.Panel2.Controls.Clear();
			splitContainerControl1.PanelVisibility = SplitPanelVisibility.Both;
			lytContainer.Visibility = LayoutVisibility.Always;
			CommonViewsActions.ShowUserControl(ref _pemrQueueContainer, splitContainerControl1.Panel1);
		}

		private void btnPreviousVisit_Click(object sender, System.EventArgs e)
		{
			splitContainerControl1.Panel1.Controls.Clear();
			splitContainerControl1.Panel2.Controls.Clear();
			splitContainerControl1.PanelVisibility = SplitPanelVisibility.Both;
			lytContainer.Visibility = LayoutVisibility.Always;
			CommonViewsActions.ShowUserControl(ref _previousVisitCardContainer, splitContainerControl1.Panel1);
			PEMR_PreviousVisitCardContainer_UC.ParentContainer = this;
		}

		public void ShowPreviousVisitPEMR()
		{
			splitContainerControl1.Panel2.Controls.Clear();
			CommonViewsActions.ShowUserControl(ref _medicalRecordContainer, splitContainerControl1.Panel2);
			if (_medicalRecordContainer != null)
			{
				PEMR_HeaderAndFooterTemplate_A4_rpt templateReport = new PEMR_HeaderAndFooterTemplate_A4_rpt();
				PEMR_PatientMedicalRecordReportContainer_rpt patientMedicalRecordReportContainer =
					new PEMR_PatientMedicalRecordReportContainer_rpt();
				patientMedicalRecordReportContainer.Initialize(PEMRBusinessLogic.ActivePEMRObject);
				templateReport =
					PEMR_HeaderAndFooterTemplate_A4_rpt.Initialize(patientMedicalRecordReportContainer, true);
				_medicalRecordContainer.Initialize(templateReport, patientMedicalRecordReportContainer);
			}
		}

		public void ShowPreviousInvestigation()
		{
			splitContainerControl1.Panel2.Controls.Clear();
			CommonViewsActions.ShowUserControl(ref _investigationContainer, splitContainerControl1.Panel2);
			_investigationContainer.Initialize(false);
		}

		public void ShowPreviousAttachments()
		{
			splitContainerControl1.Panel2.Controls.Clear();
			CommonViewsActions.ShowUserControl(ref _scanFiles, splitContainerControl1.Panel2);
			_scanFiles.Initialize(PEMRBusinessLogic.ActivePEMRObject.Active_Patient, ScanningMode.MedicalVisit,
				MedicalType.None);
			_scanFiles.EnableTopMenu(false);
		}
	}
}
