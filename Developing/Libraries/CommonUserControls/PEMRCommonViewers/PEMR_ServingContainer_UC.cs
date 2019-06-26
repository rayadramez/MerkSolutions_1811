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
using DevExpress.XtraGauges.Core.Model;
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
		private PEMR_SocialHistory_UC _pemr_SocialHistory;
		private PEMR_MedicalHistory _medicationHistory;
		private PEMR_TreatmentPlan_UC _pemr_treatmentPlan;
		private PEMR_MedicalRecordContainer _medicalRecordContainer;
		private ScanFiles_UC _scanFilesUc;
		private Medication_EditorViewer _medicationEditorViewer;
		private PEMR_Medication_UC _pemr_Medication;
		private PEMR_InvestigationContainer _pemr_InvestigationContainer;
		private PEMR_LabContainer _pemr_LabContainer;
		private PEMR_SurgeryContainer _pemr_SurgeryContainer;
		private PEMR_VitalSigns_UC _pemr_VitalSigns;
		private PEMR_Diagnosis_UC _pmer_diagnosis;
		private PEMR_Symptoms_UC _pemr_Symptoms;
		private PaintViewer_UC _paintViewer;
		private PEMR_VisionRefraction_UC _pemr_VisionRefraction;
		private PEMR_AnteriorSegment_UC _pemr_Anterior;
		private PEMR_PosteriorSegment_UC _pemr_Posterior;
		private PEMR_AdnexaSegment_UC _pemr_Adnexa;
		private PEMR_ExtraocularMuscles_UC _pemr_EOM;
		private PEMR_Pupillary_UC _pemr_Pupillary;
		private int lockTimerCounter = 0;
		private short _hours, _minutes, _seconds;
		private string timerStr = "";

		public PEMR_ServingContainer_UC()
		{
			InitializeComponent();

			if (ApplicationStaticConfiguration.ActiveLoginUser != null)
				PEMRBusinessLogic.ActiveLoggedInUser = ApplicationStaticConfiguration.ActiveLoginUser;
			digitalGauge1.DisplayMode = DigitalGaugeDisplayMode.SevenSegment;
			digitalGauge1.LetterSpacing = 30;
			digitalGauge1.DigitCount = 6;
			digitalGauge1.Text = DateTime.Now.ToString();

			timer.Enabled = true;
			timer.Start();
			SetupTabs();
		}

		private int GetStringLength(string str)
		{
			int counter = 0;
			int pos = 0;
			while (pos < str.Length)
			{
				if (str[pos] != ':')
					counter++;
				pos++;
			}
			return counter;
		}

		private void OnTimerTick(object sender, EventArgs e)
		{
			if (lockTimerCounter == 0)
			{
				lockTimerCounter++;
				UpdateTime();
				lockTimerCounter--;
			}
		}

		private void UpdateTime()
		{
			string time = DateTime.Now.ToLongTimeString();
			if (GetStringLength(time) > digitalGauge1.DigitCount)
				digitalGauge1.DigitCount = GetStringLength(time);
			digitalGauge1.Text = time;
		}

		private void timer_Tick(object sender, EventArgs e)
		{
			IncreaseSeconds();
			ShowTimer();
		}

		private void ResetTimers()
		{
			_hours = 0;
			_minutes = 0;
			_seconds = 0;

			ShowTimer();
		}

		private void ShowTimer()
		{
			timerStr = _hours.ToString("00");
			timerStr += " : ";
			timerStr += _minutes.ToString("00");
			timerStr += " : ";
			timerStr += _seconds.ToString("00");

			digitalGauge1.Text = timerStr;
		}

		private void IncreaseSeconds()
		{
			if (_seconds == 59)
			{
				_seconds = 0;
				IncreaeMinutes();
			}
			else
				_seconds++;
		}

		private void IncreaeMinutes()
		{
			if (_minutes == 59)
			{
				_minutes = 0;
				IncreaseHours();
			}
			else
				_minutes++;
		}

		private void IncreaseHours()
		{
			_hours++;
		}

		public void SetupTabs()
		{
			if (dockPanel2.Visibility != DockVisibility.Hidden)
				dockPanel2.Visibility = DockVisibility.Hidden;

			foreach (XtraTabPage tabPage in tabSubServices.TabPages)
				tabPage.Text = string.Empty;

			tabMain.SelectedTabPageIndex = 0;
			tabSubServices.SelectedTabPageIndex = 0;
			tabMedicalHistory_Sub.SelectedTabPageIndex = tabMedicalHistory_Sub.TabPages.Count - 1;
			tabExaminationMain.SelectedTabPageIndex = tabExaminationMain.TabPages.Count - 1;
			tabDiagnosis_Main.SelectedTabPageIndex = tabDiagnosis_Main.TabPages.Count - 1;
			tabDiagnosis_Main.SelectedTabPageIndex = tabDiagnosis_Main.TabPages.Count - 1;

			CommonViewsActions.ShowUserControl(ref _pemr_SocialHistory, tabSocialHistory);
			if (_pemr_SocialHistory != null)
				_pemr_SocialHistory.Initialize();

			CommonViewsActions.ShowUserControl(ref _medicationHistory, tabMedicalHistory);
			if (_medicationHistory != null)
				_medicationHistory.Initialize();

			CommonViewsActions.ShowUserControl(ref _pmer_diagnosis, tabDiagnosis);
			if (_pmer_diagnosis != null)
				_pmer_diagnosis.Initialize();

			CommonViewsActions.ShowUserControl(ref _pemr_treatmentPlan, tabTreatmentPLan);
			if (_pemr_treatmentPlan != null)
				_pemr_treatmentPlan.Initialize(true);

			CommonViewsActions.ShowUserControl(ref _pemr_Medication, tabMedication);
			if (_pemr_Medication != null)
				_pemr_Medication.Initialize(true);

			CommonViewsActions.ShowUserControl(ref _pemr_InvestigationContainer, tabInvestigation);
			if (_pemr_InvestigationContainer != null)
				_pemr_InvestigationContainer.Initialize(true);

			CommonViewsActions.ShowUserControl(ref _pemr_LabContainer, tabLab);
			if (_pemr_LabContainer != null)
				_pemr_LabContainer.Initialize(true);

			CommonViewsActions.ShowUserControl(ref _pemr_SurgeryContainer, tabSurgeries);
			if (_pemr_SurgeryContainer != null)
				_pemr_SurgeryContainer.Initialize(true);

			CommonViewsActions.ShowUserControl(ref _scanFilesUc, tabScannedFiles);
			if (_scanFilesUc != null && PEMRBusinessLogic.ActivePEMRObject != null)
				_scanFilesUc.Initialize(PEMRBusinessLogic.ActivePEMRObject.Active_Patient,
					ScanningMode.MedicalVisit,
					MedicalType.None);

			CommonViewsActions.ShowUserControl(ref _pemr_VitalSigns, tabVitalSigns);
			if (_pemr_VitalSigns != null)
				_pemr_VitalSigns.Initialize();

			CommonViewsActions.ShowUserControl(ref _pmer_diagnosis, tabDiagnosis);
			if (_pmer_diagnosis != null)
				_pmer_diagnosis.Initialize();

			CommonViewsActions.ShowUserControl(ref _paintViewer, tabPainting);
			if (_paintViewer != null)
				_paintViewer.Initialize(PaintMode.PatientMedicalPictures, Properties.Resources._01_Heart);

			CommonViewsActions.ShowUserControl(ref _pemr_Symptoms, tabSymptoms);
			if (_pemr_Symptoms != null)
				_pemr_Symptoms.Initialize();

			CommonViewsActions.ShowUserControl(ref _pemr_VisionRefraction, tabOpth_Vision);
			if (_pemr_VisionRefraction != null)
				_pemr_VisionRefraction.Initialize(ReadingsMode.ViewingActiveAllReadings, null);

			CommonViewsActions.ShowUserControl(ref _pemr_Anterior, tabOpth_AnteriorSegment);
			if (_pemr_Anterior != null)
				_pemr_Anterior.Initialize();

			CommonViewsActions.ShowUserControl(ref _pemr_Posterior, tabOpth_PosteriorSegment);
			if (_pemr_Posterior != null)
				_pemr_Posterior.Initialize();

			CommonViewsActions.ShowUserControl(ref _pemr_Adnexa, tabOpth_Adnexa);
			if (_pemr_Adnexa != null)
				_pemr_Adnexa.Initialize();

			CommonViewsActions.ShowUserControl(ref _pemr_EOM, tabOpth_EOM);
			if (_pemr_EOM != null)
				_pemr_EOM.Initialize(ReadingsMode.ViewingActiveAllReadings, null);

			CommonViewsActions.ShowUserControl(ref _pemr_Pupillary, tabOpth_Pupil);
			if (_pemr_Pupillary != null)
				_pemr_Pupillary.Initialize();

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

		public void ClearControls(bool clearAll)
		{
			ClearPEMRObjects();

			if (_pemr_Adnexa != null)
				_pemr_Adnexa.ClearControls(clearAll);

			if (_pemr_Anterior != null)
				_pemr_Anterior.ClearControls(clearAll);

			if (_pemr_EOM != null)
				_pemr_EOM.ClearControls(clearAll);

			if (_pemr_Posterior != null)
				_pemr_Posterior.ClearControls(clearAll);

			if (_pemr_Pupillary != null)
				_pemr_Pupillary.ClearControls(clearAll);

			if (_pemr_VisionRefraction != null)
				_pemr_VisionRefraction.ClearControls(clearAll);

			if (_pemr_InvestigationContainer != null)
				_pemr_InvestigationContainer.ClearControls(clearAll);

			if (_pmer_diagnosis != null)
				_pmer_diagnosis.ClearControls(clearAll);

			if (_pemr_LabContainer != null)
				_pemr_LabContainer.ClearControls(clearAll);

			if (_pemr_Medication != null)
				_pemr_Medication.ClearControls(clearAll);

			if (_pemr_Medication != null)
				_pemr_Medication.ClearControls(clearAll);

			if (_medicalRecordContainer != null)
				_medicalRecordContainer.ClearControls(clearAll);

			if (_pemr_SocialHistory != null)
				_pemr_SocialHistory.ClearControls(clearAll);

			if (_pemr_Symptoms != null)
				_pemr_Symptoms.ClearControls(clearAll);

			if (_pemr_VitalSigns != null)
				_pemr_VitalSigns.ClearControls(clearAll);

			if (_pemr_treatmentPlan != null)
				_pemr_treatmentPlan.ClearControls(clearAll);

			lytmenu.Visibility = LayoutVisibility.Never;
			chkMenu.Checked = false;
		}

		public void ClearPEMRObjects()
		{
			PEMRBusinessLogic.ActivePEMRObject.Active_Invoice = null;
			PEMRBusinessLogic.ActivePEMRObject.Active_InvoiceDetail = null;
			PEMRBusinessLogic.ActivePEMRObject.Active_Patient = null;
			PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming = null;
			PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_SocialHistory = null;
			PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_MedicalHistory = null;
			PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_TreatmentPlan = null;
			PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_Attachment = null;
			PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_InvestigationReservation = null;
			PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_InvestigationResult = null;
			PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_LabReservation = null;
			PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_LabResult = null;
			PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_SurgeryReservation = null;
			PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_SurgeryResult = null;
			PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_Medication = null;
			PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_VitalSign = null;
			PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_MainDiagnosis = null;
			PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_Diagnosis = null;
			PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_MainAnteriorSegmentSign = null;
			PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_AnteriorSegmentSign = null;
			PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_MainPosteriorSegmentSign = null;
			PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_PosteriorSegmentSign = null;
			PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_MainAdnexaSegmentSign = null;
			PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_AdnexaSegmentSign = null;
			PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_VisionRefractionReading = null;
			PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_Pupillary = null;
			PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_MainEOMSign = null;
			PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_EOMSign = null;
			PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_EOMReading = null;
			PEMRBusinessLogic.ActivePEMRObject = null;
		}

		private void chkMenu_CheckedChanged(object sender, EventArgs e)
		{
			lytmenu.Visibility = chkMenu.Checked ? LayoutVisibility.Always : LayoutVisibility.Never;
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
					PEMRBusinessLogic.UpdateAll(PEMRBusinessLogic.ActivePEMRObject);
					MerkDBBusinessLogicEngine.UpdateAndSave_QueueManagerStatus(QueueResult.QueueManagerID,
						DB_QueueManagerStatus.Waiting);
					PEMRContainer.ShowLeftQueuePanel(false);
					PEMRContainer.ShowPEMRHistoryContainer(QueueResult, ActiveVisitTiming, false);
					ClearControls(true);
					SetupTabs();
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
					PEMRBusinessLogic.UpdateAll(PEMRBusinessLogic.ActivePEMRObject);
					MerkDBBusinessLogicEngine.UpdateAndSave_QueueManagerStatus(QueueResult.QueueManagerID,
						DB_QueueManagerStatus.Served);
					PEMRContainer.ShowLeftQueuePanel(false);
					PEMRContainer.ShowPEMRHistoryContainer(QueueResult, ActiveVisitTiming, false);
					ClearControls(true);
					SetupTabs();
					break;
			}
		}

		private void btnReturnToPausedQueue_Click(object sender, EventArgs e)
		{
			DialogResult result = XtraMessageBox.Show("Do you put this Patient to the Paused Queue ?", "Note",
				MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, DefaultBoolean.Default);
			switch (result)
			{
				case DialogResult.Yes:
					PEMRBusinessLogic.UpdateAll(PEMRBusinessLogic.ActivePEMRObject);
					MerkDBBusinessLogicEngine.UpdateAndSave_QueueManagerStatus(QueueResult.QueueManagerID,
						DB_QueueManagerStatus.Paused);
					PEMRContainer.ShowLeftQueuePanel(false);
					PEMRContainer.ShowPEMRHistoryContainer(QueueResult, ActiveVisitTiming, false);
					ClearControls(true);
					SetupTabs();
					break;
			}
		}

		private void btnSendToStage_Click(object sender, EventArgs e)
		{
			PEMR_SendToStage_UC sendToStage = new PEMR_SendToStage_UC();
			sendToStage.Initialize(MerkDBBusinessLogicEngine.ActiveStationPointStage, QueueResult);
			PopupBaseForm.ShowAsPopup(sendToStage, this);
		}

		private void tabSocialHistory_DoubleClick(object sender, EventArgs e)
		{

		}

		private void tabSubControl_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
		{
			if (tabMedicalHistory_Sub.SelectedTabPage.Name == "tabSocialHistory")
			{
				CommonViewsActions.ShowUserControl(ref _pemr_SocialHistory, tabSocialHistory);
				if (_pemr_SocialHistory != null)
					_pemr_SocialHistory.Initialize();
			}

			if (tabMedicalHistory_Sub.SelectedTabPage.Name == "tabGeneralHistory")
			{
				CommonViewsActions.ShowUserControl(ref _medicationHistory, tabMedicalHistory);
				if (_medicationHistory != null)
					_medicationHistory.Initialize();
			}
		}

		private void tabTreatment_Sub_SelectedPageChanged(object sender, TabPageChangedEventArgs e)
		{
			if (tabDiagnosis_Main.SelectedTabPage.Name == "tabDiagnosis")
			{
				CommonViewsActions.ShowUserControl(ref _pmer_diagnosis, tabDiagnosis);
				if (_pmer_diagnosis != null)
					_pmer_diagnosis.Initialize();
			}

			if (tabDiagnosis_Main.SelectedTabPage.Name == "tabTreatmentPLan")
			{
				CommonViewsActions.ShowUserControl(ref _pemr_treatmentPlan, tabTreatmentPLan);
				if (_pemr_treatmentPlan != null)
					_pemr_treatmentPlan.Initialize(true);
			}

			if (tabDiagnosis_Main.SelectedTabPage.Name == "tabMedication")
			{
				CommonViewsActions.ShowUserControl(ref _pemr_Medication, tabMedication);
				if (_pemr_Medication != null)
					_pemr_Medication.Initialize(true);
			}

			if (tabDiagnosis_Main.SelectedTabPage.Name == "tabInvestigation")
			{
				CommonViewsActions.ShowUserControl(ref _pemr_InvestigationContainer, tabInvestigation);
				if (_pemr_InvestigationContainer != null)
					_pemr_InvestigationContainer.Initialize(true);
			}

			if (tabDiagnosis_Main.SelectedTabPage.Name == "tabLab")
			{
				CommonViewsActions.ShowUserControl(ref _pemr_LabContainer, tabLab);
				if (_pemr_LabContainer != null)
					_pemr_LabContainer.Initialize(true);
			}

			if (tabDiagnosis_Main.SelectedTabPage.Name == "tabSurgeries")
			{
				CommonViewsActions.ShowUserControl(ref _pemr_SurgeryContainer, tabSurgeries);
				if (_pemr_SurgeryContainer != null)
					_pemr_SurgeryContainer.Initialize(true);
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
				if (_scanFilesUc != null && PEMRBusinessLogic.ActivePEMRObject != null)
					_scanFilesUc.Initialize(PEMRBusinessLogic.ActivePEMRObject.Active_Patient,
						ScanningMode.MedicalVisit,
						MedicalType.None);
			}

			if (tabSubServices.SelectedTabPage.Name == "tabExamination")
			{
				CommonViewsActions.ShowUserControl(ref _pemr_VitalSigns, tabVitalSigns);
				if (_pemr_VitalSigns != null)
					_pemr_VitalSigns.Initialize();
			}

			if (tabSubServices.SelectedTabPage.Name == "tabDiagnosisMain")
			{
				CommonViewsActions.ShowUserControl(ref _pmer_diagnosis, tabDiagnosis);
				if (_pmer_diagnosis != null)
					_pmer_diagnosis.Initialize();
			}

			if (tabSubServices.SelectedTabPage.Name == "tabPainting")
			{
				CommonViewsActions.ShowUserControl(ref _paintViewer, tabPainting);
				if (_paintViewer != null)
					_paintViewer.Initialize(PaintMode.PatientMedicalPictures, Properties.Resources._01_Heart);
			}
		}

		private void tabExaminationMain_SelectedPageChanged(object sender, TabPageChangedEventArgs e)
		{
			if (tabExaminationMain.SelectedTabPage.Name == "tabVitalSigns")
			{
				CommonViewsActions.ShowUserControl(ref _pemr_VitalSigns, tabVitalSigns);
				if (_pemr_VitalSigns != null)
					_pemr_VitalSigns.Initialize();
			}

			if (tabExaminationMain.SelectedTabPage.Name == "tabSymptoms")
			{
				CommonViewsActions.ShowUserControl(ref _pemr_Symptoms, tabSymptoms);
				if (_pemr_Symptoms != null)
					_pemr_Symptoms.Initialize();
			}

			if (tabExaminationMain.SelectedTabPage.Name == "tabOpth_Vision")
			{
				CommonViewsActions.ShowUserControl(ref _pemr_VisionRefraction, tabOpth_Vision);
				if (_pemr_VisionRefraction != null)
					_pemr_VisionRefraction.Initialize(ReadingsMode.ViewingActiveAllReadings, null);
			}

			if (tabExaminationMain.SelectedTabPage.Name == "tabOpth_AnteriorSegment")
			{
				CommonViewsActions.ShowUserControl(ref _pemr_Anterior, tabOpth_AnteriorSegment);
				if (_pemr_Anterior != null)
					_pemr_Anterior.Initialize();
			}

			if (tabExaminationMain.SelectedTabPage.Name == "tabOpth_PosteriorSegment")
			{
				CommonViewsActions.ShowUserControl(ref _pemr_Posterior, tabOpth_PosteriorSegment);
				if (_pemr_Posterior != null)
					_pemr_Posterior.Initialize();
			}

			if (tabExaminationMain.SelectedTabPage.Name == "tabOpth_Adnexa")
			{
				CommonViewsActions.ShowUserControl(ref _pemr_Adnexa, tabOpth_Adnexa);
				if (_pemr_Adnexa != null)
					_pemr_Adnexa.Initialize();
			}

			if (tabExaminationMain.SelectedTabPage.Name == "tabOpth_EOM")
			{
				CommonViewsActions.ShowUserControl(ref _pemr_EOM, tabOpth_EOM);
				if (_pemr_EOM != null)
					_pemr_EOM.Initialize(ReadingsMode.ViewingActiveAllReadings, null);
			}

			if (tabExaminationMain.SelectedTabPage.Name == "tabOpth_Pupil")
			{
				CommonViewsActions.ShowUserControl(ref _pemr_Pupillary, tabOpth_Pupil);
				if (_pemr_Pupillary != null)
					_pemr_Pupillary.Initialize();
			}
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
