using System;
using System.Windows.Forms;
using ApplicationConfiguration;
using CommonControlLibrary;
using CommonUserControls.CommonViewers;
using CommonUserControls.PEMRCommonViewers.PEMR_InternalViewers;
using CommonUserControls.PEMRCommonViewers.PEMR_InternalViewers.Ophthalmology;
using CommonUserControls.Reports;
using CommonUserControls.SettingsViewers.MedicationViewers;
using CommonUserControls.SettingsViewers.PatientViewers;
using DevExpress.Utils;
using DevExpress.XtraBars.Docking;
using DevExpress.XtraEditors;
using DevExpress.XtraLayout.Utils;
using DevExpress.XtraTab;
using MerkDataBaseBusinessLogicProject;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MVCBusinessLogicLibrary.BaseViewers;
using MVCBusinessLogicLibrary.MVCFactories;

namespace CommonUserControls.PEMRCommonViewers
{
	public partial class PEMR_ServingContainer_UC : UserControl
	{
		public PEMRObject ActivePEMR { get; set; }
		public GetBriefQueue_Result QueueResult { get; set; }
		public VisitTiming ActiveVisitTiming { get; set; }
		private PatientEditorViewer_UC _patientEditorViewer;
		private PEMRContainer PEMRContainer { get; set; }
		private PEMR_SocialHistory_UC _socialHistory;
		private PEMR_MedicalHistory _medicationHistory;
		private PEMR_TreatmentPlan_UC _treatmentPlan;
		private PEMR_MedicalRecordContainer _medicalRecordContainer;
		private ScanFiles_UC _scanFilesUc;
		private Medication_EditorViewer _medicationEditorViewer;
		private PEMR_Medication_UC _visitTimingMedication;
		private PEMR_InvestigationContainer _investigationContainer;
		private PEMR_LabContainer _labContainer;
		private PEMR_SurgeryContainer _surgeryContainer;
		private PEMR_VitalSigns_UC _vitalSigns;
		private PEMR_Diagnosis_UC _diagnosis;
		private PEMR_Symptoms_UC _pemrSymptoms;
		private PaintViewer_UC _paintViewer;
		private PEMR_VisionRefraction_UC _visionRefraction;
		private PEMR_AnteriorSegment_UC _anteriorSegment;
		private PEMR_PosteriorSegment_UC _posteriorSegment;
		private PEMR_AdnexaSegment_UC _adnexaSegment;
		private PEMR_ExtraocularMuscles_UC _extraOcularMuscles;
		private PEMR_Pupillary_UC _pupillary;

		public PEMR_ServingContainer_UC()
		{
			InitializeComponent();

			if (dockPanel2.Visibility != DockVisibility.Hidden)
				dockPanel2.Visibility = DockVisibility.Hidden;

			foreach (XtraTabPage tabPage in tabSubServices.TabPages)
				tabPage.Text = string.Empty;

			CommonViewsActions.ShowUserControl(ref _medicationHistory, tabGeneralHistory);

			switch (ApplicationStaticConfiguration.Organization)
			{
				case DB_Organization.Ophthalmology:
					tabOpth_Adnexa.PageVisible = true;
					tabOpth_AnteriorSegment.PageVisible = true;
					tabOpth_PosteriorSegment.PageVisible = true;
					tabOpth_Vision.PageVisible = true;
					break;
			}
		}

		private void chkMenu_CheckedChanged(object sender, EventArgs e)
		{
			lytmenu.Visibility = chkMenu.Checked ? LayoutVisibility.Always : LayoutVisibility.Never;
		}

		public void InitializePatientInfo(PEMRContainer pemrContainer, GetBriefQueue_Result queueResult,
			VisitTiming visitTiming, PEMRObject pemrObject)
		{
			ActivePEMR = pemrObject;
			QueueResult = queueResult;
			ActiveVisitTiming = visitTiming;
			PEMRContainer = pemrContainer;

			if (pemrObject == null)
				return;

			lblTitlePatientID.Text = pemrObject.Active_Patient.ID.ToString();
			lblTitlePatientName.Text = pemrObject.Active_Patient.PatientFullName;

			Service_cu service =
				Service_cu.ItemsList.Find(
					item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(pemrObject.Active_InvoiceDetail.Service_CU_ID)));
			if (service != null)
				lblTitleServiceName.Text = service.Name_S;
			lblTitleReservationTime.Text = Convert.ToDateTime(pemrObject.Active_InvoiceDetail.Date)
				.ConvertDateTimeToString(true, true, true);
			lblIsMale.Text = pemrObject.Active_Patient.IsMale ? "(M)" : "(F)";
			if (pemrObject.Active_Patient.BirthDate != null)
			{
				int numberOfYears =
					Convert.ToInt32(CommonActions.CommonActions.CalculateYears(pemrObject.Active_Patient.BirthDate));
				lblBD.Text = numberOfYears.ToString();
			}

			DB_QueueManagerStatus queueManagerStatus = (DB_QueueManagerStatus)queueResult.QueueStatusID;
			switch (queueManagerStatus)
			{
				case DB_QueueManagerStatus.Waiting:

					break;
				case DB_QueueManagerStatus.Paused:
				case DB_QueueManagerStatus.Served:

					break;
			}
		}

		private void btnPatientPersonalInfo_Click(object sender, EventArgs e)
		{
			BaseController<Person_cu>.ShowEditorControl(ref _patientEditorViewer, this, null, ActivePEMR.Active_Patient.PersonObject,
				EditorContainerType.Regular, ViewerName.PatientViewer, DB_CommonTransactionType.UpdateExisting,
				"بيـانــــات المريــــض", true);
		}

		private void btnReturnToWaitingQueue_Click(object sender, EventArgs e)
		{
			DialogResult result = XtraMessageBox.Show("Do you put this Patient to the Waiting Queue ?", "Note",
				MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, DefaultBoolean.Default);
			switch (result)
			{
				case DialogResult.Yes:
					PEMRBusinessLogic.SavePEMRObject();
					MerkDBBusinessLogicEngine.UpdateAndSave_QueueManagerStatus(QueueResult.QueueManagerID,
						DB_QueueManagerStatus.Waiting);
					PEMRContainer.ShowLeftQueuePanel(false);
					PEMRContainer.ShowPEMRHistoryContainer(QueueResult, ActiveVisitTiming, false);
					break;
			}
		}

		private void btnDoneServing_Click(object sender, EventArgs e)
		{
			DialogResult result = XtraMessageBox.Show("Do you Done Serving this Patient ?", "Note",
				MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, DefaultBoolean.Default);
			switch (result)
			{
				case DialogResult.Yes:
					PEMRBusinessLogic.SavePEMRObject();
					MerkDBBusinessLogicEngine.UpdateAndSave_QueueManagerStatus(QueueResult.QueueManagerID,
						DB_QueueManagerStatus.Served);
					PEMRContainer.ShowLeftQueuePanel(false);
					PEMRContainer.ShowPEMRHistoryContainer(QueueResult, ActiveVisitTiming, false);
					ClearControls(true);
					break;
			}
		}

		private void btnSendToStage_Click(object sender, EventArgs e)
		{

		}

		public void ClearControls(bool clearAll)
		{
			if (_socialHistory != null)
				_socialHistory.ClearControls(clearAll);

			if (_treatmentPlan != null)
				_treatmentPlan.ClearControls(clearAll);

			if (_medicalRecordContainer != null)
				_medicalRecordContainer.ClearControls(clearAll);

			if (_scanFilesUc != null)
				_scanFilesUc.ClearControls();

			if (_visitTimingMedication != null)
				_visitTimingMedication.ClearControls(clearAll);

			if (_investigationContainer != null)
				_investigationContainer.ClearControls(clearAll);

			if (_labContainer != null)
				_labContainer.ClearControls(clearAll);

			if (_surgeryContainer != null)
				_surgeryContainer.ClearControls(clearAll);

			if (_vitalSigns != null)
				_vitalSigns.ClearControls(clearAll);

			if (_diagnosis != null)
				_diagnosis.ClearControls(clearAll);

			if (_pemrSymptoms != null)
				_pemrSymptoms.ClearControls(clearAll);

			lytmenu.Visibility = LayoutVisibility.Never;
			chkMenu.Checked = false;
		}

		private void btnReturnToPausedQueue_Click(object sender, EventArgs e)
		{
			DialogResult result = XtraMessageBox.Show("Do you put this Patient to the Paused Queue ?", "Note",
				MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, DefaultBoolean.Default);
			switch (result)
			{
				case DialogResult.Yes:
					PEMRBusinessLogic.SavePEMRObject();
					MerkDBBusinessLogicEngine.UpdateAndSave_QueueManagerStatus(QueueResult.QueueManagerID,
						DB_QueueManagerStatus.Paused);
					PEMRContainer.ShowLeftQueuePanel(false);
					PEMRContainer.ShowPEMRHistoryContainer(QueueResult, ActiveVisitTiming, false);
					break;
			}
		}

		private void tabSocialHistory_DoubleClick(object sender, EventArgs e)
		{

		}

		private void tabSubControl_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
		{
			if (tabMedicalHistory_Sub.SelectedTabPage.Name == "tabSocialHistory")
				CommonViewsActions.ShowUserControl(ref _socialHistory, tabSocialHistory);

			if (tabMedicalHistory_Sub.SelectedTabPage.Name == "tabGeneralHistory")
				CommonViewsActions.ShowUserControl(ref _medicationHistory, tabGeneralHistory);
		}

		private void tabTreatment_Sub_SelectedPageChanged(object sender, TabPageChangedEventArgs e)
		{
			if (tabDiagnosis_Main.SelectedTabPage.Name == "tabDiagnosis")
			{
				CommonViewsActions.ShowUserControl(ref _diagnosis, tabDiagnosis);
				_diagnosis.Initialize();
			}

			if (tabDiagnosis_Main.SelectedTabPage.Name == "tabTreatmentPLan")
			{
				CommonViewsActions.ShowUserControl(ref _treatmentPlan, tabTreatmentPLan);
				_treatmentPlan.Initialize(true);
			}

			if (tabDiagnosis_Main.SelectedTabPage.Name == "tabMedication")
			{
				CommonViewsActions.ShowUserControl(ref _visitTimingMedication, tabMedication);
				_visitTimingMedication.Initialize(true);
			}

			if (tabDiagnosis_Main.SelectedTabPage.Name == "tabInvestigation")
			{
				CommonViewsActions.ShowUserControl(ref _investigationContainer, tabInvestigation);
				_investigationContainer.Initialize(true);
			}

			if (tabDiagnosis_Main.SelectedTabPage.Name == "tabLab")
			{
				CommonViewsActions.ShowUserControl(ref _labContainer, tabLab);
				_labContainer.Initialize(true);
			}

			if (tabDiagnosis_Main.SelectedTabPage.Name == "tabSurgeries")
			{
				CommonViewsActions.ShowUserControl(ref _surgeryContainer, tabSurgeries);
				_surgeryContainer.Initialize(true);
			}
		}

		private void tabMain_SelectedPageChanged(object sender, TabPageChangedEventArgs e)
		{
			if (tabMain.SelectedTabPage.Name == "tabPEMR")
			{
				CommonViewsActions.ShowUserControl(ref _medicalRecordContainer, tabPEMR);
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
		}

		private void tabSubServices_SelectedPageChanged(object sender, TabPageChangedEventArgs e)
		{
			if (tabSubServices.SelectedTabPage.Name == "tabScannedFiles")
			{
				CommonViewsActions.ShowUserControl(ref _scanFilesUc, tabScannedFiles);
				_scanFilesUc.Initialize(PEMRBusinessLogic.ActivePEMRObject.Active_Patient, ScanningMode.MedicalVisit,
					MedicalType.None);
			}

			if (tabSubServices.SelectedTabPage.Name == "tabExamination")
				CommonViewsActions.ShowUserControl(ref _vitalSigns, tabVitalSigns);

			if (tabSubServices.SelectedTabPage.Name == "tabDiagnosisMain")
			{
				CommonViewsActions.ShowUserControl(ref _diagnosis, tabDiagnosis);
				_diagnosis.Initialize();
			}

			if (tabSubServices.SelectedTabPage.Name == "tabPainting")
			{
				CommonViewsActions.ShowUserControl(ref _paintViewer, tabPainting);
				_paintViewer.Initialize(PaintMode.PatientMedicalPictures, Properties.Resources._01_Heart);
			}
		}

		private void tabExaminationMain_SelectedPageChanged(object sender, TabPageChangedEventArgs e)
		{
			if (tabExaminationMain.SelectedTabPage.Name == "tabVitalSigns")
				CommonViewsActions.ShowUserControl(ref _vitalSigns, tabVitalSigns);
			if (tabExaminationMain.SelectedTabPage.Name == "tabSymptoms")
				CommonViewsActions.ShowUserControl(ref _pemrSymptoms, tabSymptoms);

			if (tabExaminationMain.SelectedTabPage.Name == "tabOpth_Vision")
			{
				CommonViewsActions.ShowUserControl(ref _visionRefraction, tabOpth_Vision);
				if (_visionRefraction != null)
					_visionRefraction.Initialize(ReadingsMode.ViewingActiveAllReadings, null);
			}

			if (tabExaminationMain.SelectedTabPage.Name == "tabOpth_AnteriorSegment")
			{
				CommonViewsActions.ShowUserControl(ref _anteriorSegment, tabOpth_AnteriorSegment);
				if (_anteriorSegment != null)
					_anteriorSegment.Initialize();
			}

			if (tabExaminationMain.SelectedTabPage.Name == "tabOpth_PosteriorSegment")
			{
				CommonViewsActions.ShowUserControl(ref _posteriorSegment, tabOpth_PosteriorSegment);
				if (_posteriorSegment != null)
					_posteriorSegment.Initialize();
			}

			if (tabExaminationMain.SelectedTabPage.Name == "tabOpth_Adnexa")
			{
				CommonViewsActions.ShowUserControl(ref _adnexaSegment, tabOpth_Adnexa);
				if (_adnexaSegment != null)
					_adnexaSegment.Initialize();
			}

			if (tabExaminationMain.SelectedTabPage.Name == "tabOpth_EOM")
			{
				CommonViewsActions.ShowUserControl(ref _extraOcularMuscles, tabOpth_EOM);
				_extraOcularMuscles.Initialize(ReadingsMode.ViewingActiveAllReadings, null);
			}

			if (tabExaminationMain.SelectedTabPage.Name == "tabOpth_Pupil")
				CommonViewsActions.ShowUserControl(ref _pupillary, tabOpth_Pupil);
		}

		private void tabSubServices_MouseHover(object sender, EventArgs e)
		{
			foreach (XtraTabPage tabPage in tabSubServices.TabPages)
			{
				if (tabPage.Name == "tabMedicalHistory")
					tabPage.Text = "Medical History";
				if (tabPage.Name == "tabExamination")
					tabPage.Text = "Examination";
				if (tabPage.Name == "tabDiagnosisMain")
					tabPage.Text = "Diagnosis";
				if (tabPage.Name == "tabFollowUp")
					tabPage.Text = "Follow Up";
				if (tabPage.Name == "tabNotes")
					tabPage.Text = "Notes";
				if (tabPage.Name == "tabReferral")
					tabPage.Text = "Referral";
				if (tabPage.Name == "tabPainting")
					tabPage.Text = "Painting";
				if (tabPage.Name == "tabScannedFiles")
					tabPage.Text = "Scanned Files";
			}
		}

		private void tabSubServices_MouseLeave(object sender, EventArgs e)
		{
			foreach (XtraTabPage tabPage in tabSubServices.TabPages)
				tabPage.Text = string.Empty;
		}
	}
}
