using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ApplicationConfiguration;
using CommonControlLibrary;
using CommonUserControls.InvoiceViewers;
using CommonUserControls.SettingsViewers.PatientViewers;
using DevExpress.XtraEditors;
using DevExpress.XtraLayout.Utils;
using MerkDataBaseBusinessLogicProject;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;
using MVCBusinessLogicLibrary.BaseViewers;
using MVCBusinessLogicLibrary.MVCFactories;

namespace CommonUserControls.CommonViewers
{
	public partial class MainPatientInvoiceActions : XtraUserControl
	{
		private PatientEditorViewer_UC _patientEditorViewer;
		private InvoiceContainer_UC _invoiceContainer;
		private MedicalAdmissionInvoiceCreationContainer_UC _medicalAdmissionInvoiceCreationContainer;
		private InvoicePayment_UC _invoicePayment;
		public Control ParentControl { get; set; }

		private Patient_cu ActiveSelectedPatient { get; set; }
		private InvoiceObject InvoiceObject { get; set; }
		private InvoiceManagerQueueContainerWithHeaderIcons_UC _invoiceManagerQueueContainer;
		private PatientNotCompletedPreviousVisits_UC _patientNotCompletedPreviousVisits;
		private ScanFiles_UC _scanFiles;

		public MainPatientInvoiceActions()
		{
			InitializeComponent();

			CommonViewsActions.LoadXMLFromString(layoutControl1, Resources.LocalizedRes.lyt_MainPatientInvoiceActions);
			CommonViewsActions.SetupSyle(this);
			BringToFront();
		}

		public void Initialize(Patient_cu patient, Control parentControl)
		{
			if (patient == null)
				return;
			ActiveSelectedPatient = patient;
			ParentControl = parentControl;

			switch (ApplicationStaticConfiguration.Application)
			{
				case DB_Application.ClinicReception:
					lytInPatientActions.Visibility = LayoutVisibility.Never;
					lytOutpatientActions.Visibility = LayoutVisibility.Always;
					break;
				case DB_Application.AdmissionReception:
					lytInPatientActions.Visibility = LayoutVisibility.Always;
					lytOutpatientActions.Visibility = LayoutVisibility.Never;
					break;
			}

			BackColor = Color.SlateGray;

			if (!string.IsNullOrEmpty(ApplicationStaticConfiguration.SkinName) &&
			    !string.IsNullOrWhiteSpace(ApplicationStaticConfiguration.SkinName))
				if (ApplicationStaticConfiguration.SkinName.Equals("McSkin"))
				{
					lytOutpatientActions.AppearanceGroup.ForeColor =
						lytInPatientActions.AppearanceGroup.ForeColor = Color.DarkBlue;
					lblPatientID.ForeColor = Color.DarkOrange;
					lblPatientName.ForeColor = lblInsuranceCarrierName.ForeColor = lblInsuranceLevelName.ForeColor =
						lblInsurancePercentage.ForeColor = Color.DarkBlue;
					labelControl1.ForeColor = labelControl3.ForeColor =
						labelControl5.ForeColor = labelControl7.ForeColor = Color.Black;
					btnClose.Image = Properties.Resources.ExitIcon_8;
				}
				else
				{
					lytOutpatientActions.AppearanceGroup.ForeColor =
						lytInPatientActions.AppearanceGroup.ForeColor = Color.OldLace;
					lblPatientID.ForeColor = Color.OrangeRed;
					lblPatientName.ForeColor = lblInsuranceCarrierName.ForeColor = lblInsuranceLevelName.ForeColor =
						lblInsurancePercentage.ForeColor = Color.Ivory;
					labelControl1.ForeColor = labelControl3.ForeColor =
						labelControl5.ForeColor = labelControl7.ForeColor = Color.OldLace;
					btnClose.Image = Properties.Resources.Exit_1_16;
				}

			lblPatientID.Text = ActiveSelectedPatient.Person_CU_ID.ToString();
			lblPatientName.Text = ActiveSelectedPatient.PatientFullName;
			if (ActiveSelectedPatient.InsuranceCarrier_InsuranceLevel_CU_ID != null)
			{
				InsuranceCarrier_InsuranceLevel_cu insuranceBridge =
					InsuranceCarrier_InsuranceLevel_cu.ItemsList.Find(
						item =>
							Convert.ToInt32(item.ID)
								.Equals(Convert.ToInt32(ActiveSelectedPatient.InsuranceCarrier_InsuranceLevel_CU_ID)));
				if (insuranceBridge != null)
				{
					InsuranceCarrier_cu insuranceCarrier =
						InsuranceCarrier_cu.ItemsList.Find(
							item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(insuranceBridge.InsuranceCarrier_CU_ID)));
					if (insuranceCarrier != null)
						lblInsuranceCarrierName.Text = insuranceCarrier.Name_P;
					InsuranceLevel_cu insurancelevel =
						InsuranceLevel_cu.ItemsList.Find(
							item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(insuranceBridge.InsuranceLevel_CU_ID)));
					if (insurancelevel != null)
						lblInsuranceLevelName.Text = insurancelevel.Name_P;
					lblInsurancePercentage.Text = Convert.ToString(insuranceBridge.InsurancePercentage * 100);
				}
			}
			BringToFront();
		}

		public void Initialize(InvoiceObject invoiceObject)
		{
			if (invoiceObject == null)
				return;

			InvoiceObject = invoiceObject;
			BringToFront();
		}

		private void simpleButton1_Click(object sender, System.EventArgs e)
		{
			if (ActiveSelectedPatient != null)
				BaseController<Person_cu>.ShowEditorControl(ref _patientEditorViewer, this, null, ActiveSelectedPatient.Person_cu,
					EditorContainerType.Regular, ViewerName.PatientViewer, DB_CommonTransactionType.UpdateExisting,
					"بيانـــات المرضــى", true);
			if (ParentForm != null)
				ParentForm.Close();
		}

		private void btnOutPatientInvoiceCreation_Click(object sender, System.EventArgs e)
		{
			ApplicationStaticConfiguration.InternalReceptionApplication = DB_Application.ClinicReception;
			List<GetPatientPreviousInvoices_Result> list = DBCommon.DBContext_External
				.GetPatientPreviousInvoices(ActiveSelectedPatient.Person_CU_ID, false, true, DateTime.Now.Date,
					DateTime.Now.Date).OrderBy(item => item.InvoiceCreationDate).ToList();
			if (list.Count > 0)
			{
				PatientMedicalServicesActions_UC patientMedicalServiceActions = new PatientMedicalServicesActions_UC();
				patientMedicalServiceActions.Initialize(ActiveSelectedPatient, this, ParentControl);
				if (ParentForm != null)
					ParentForm.Close();
				PopupBaseForm.ShowAsPopup(patientMedicalServiceActions, this);
			}
			else
			{
				BaseController<Invoice>.ShowEditorControl(ref _medicalAdmissionInvoiceCreationContainer, this,
					ActiveSelectedPatient, null, EditorContainerType.Regular,
					ViewerName.PatientInvoiceCreation,
					DB_CommonTransactionType.CreateNew,
					".... الخـدمــــات الطبيــــــة ....",
					true);
				if (ParentForm != null)
					ParentForm.Close();
			}
		}

		private void btnOneDaySurgery_Click(object sender, EventArgs e)
		{
			ApplicationStaticConfiguration.InternalReceptionApplication = DB_Application.OneDaySurgeryReception;
			List<GetPatientPreviousInvoices_Result> list = DBCommon.DBContext_External
				.GetPatientPreviousInvoices(ActiveSelectedPatient.Person_CU_ID, false, true, DateTime.Now.Date,
					DateTime.Now.Date).OrderBy(item => item.InvoiceCreationDate).ToList();
			if (list.Count > 0)
			{
				PatientMedicalServicesActions_UC patientMedicalServiceActions = new PatientMedicalServicesActions_UC();
				patientMedicalServiceActions.Initialize(ActiveSelectedPatient, this, ParentControl);
				if (ParentForm != null)
					ParentForm.Close();
				PopupBaseForm.ShowAsPopup(patientMedicalServiceActions, this);
			}
			else
			{
				BaseController<Invoice>.ShowEditorControl(ref _medicalAdmissionInvoiceCreationContainer, this,
					ActiveSelectedPatient, null, EditorContainerType.Regular,
					ViewerName.PatientInvoiceCreation,
					DB_CommonTransactionType.CreateNew,
					".... الخـدمــــات الطبيــــــة ....",
					true);
				if (ParentForm != null)
					ParentForm.Close();
			}
		}

		private void btnOutPayments_Click(object sender, System.EventArgs e)
		{
			PatientNotCompletedPreviousVisits_UC patientPreviousInvoices = new PatientNotCompletedPreviousVisits_UC();
			patientPreviousInvoices.Initialize(ActiveSelectedPatient, ParentControl, this,
				PatientNotCompletedPreviousVisitsAction.Payment);
			if (ParentForm != null)
				ParentForm.Close();
			PopupBaseForm.ShowAsPopup(patientPreviousInvoices, this);
		}

		private void btnInvoiceReviewingOutPatient_Click(object sender, System.EventArgs e)
		{
			PatientNotCompletedPreviousVisits_UC patientPreviousInvoices = new PatientNotCompletedPreviousVisits_UC();
			patientPreviousInvoices.Initialize(ActiveSelectedPatient, ParentControl, this,
				PatientNotCompletedPreviousVisitsAction.InvoiceManager);
			if (ParentForm != null)
				ParentForm.Close();
			PopupBaseForm.ShowAsPopup(patientPreviousInvoices, this);
		}

		private void btnStartAccommodation_Click(object sender, System.EventArgs e)
		{
		}

		private void btnTransferAccommodation_Click(object sender, System.EventArgs e)
		{

		}

		private void btnEndAccommodation_Click(object sender, System.EventArgs e)
		{

		}

		private void btnAddServicesToInPatient_Click(object sender, System.EventArgs e)
		{

		}

		private void btnInvoiceReviewingInPatient_Click(object sender, System.EventArgs e)
		{
			BaseController<Invoice>.ShowEditorControl(ref _invoiceContainer, this, null, null, EditorContainerType.Settings,
				ViewerName.PatientInvoice, DB_CommonTransactionType.CreateNew, ".... بيانـــات المرضــى ....", true);

			if (ParentForm != null)
				ParentForm.Close();
		}

		private void btnInPatientPayments_Click(object sender, System.EventArgs e)
		{
			BaseController<InvoicePayment>.ShowEditorControl(ref _invoicePayment, this, null, null, EditorContainerType.Settings,
				ViewerName.InvoicePayment_Viewer, DB_CommonTransactionType.CreateNew, ".... الخـزينـــــة ....", true);
			if (_invoicePayment != null)
				_invoicePayment.MedicalInitialize(InvoiceObject.ActiveInvoice);

			if (ParentForm != null)
				ParentForm.Close();
		}

		private void btnClose_Click(object sender, System.EventArgs e)
		{
			if (ParentForm != null)
				ParentForm.Close();
		}

		private void btnScanner_Click(object sender, EventArgs e)
		{
			ScanFiles_UC scanFiles = new ScanFiles_UC();
			scanFiles.Initialize(ActiveSelectedPatient, ScanningMode.Regular, MedicalType.None);
			if (ParentForm != null)
				ParentForm.Close();
			PopupBaseForm.ShowAsPopup(scanFiles, this);
		}

		private void btnPatientDeposite_Click(object sender, EventArgs e)
		{
			BaseController<InvoicePayment>.ShowEditorControl(
				ref _invoicePayment, this, ActiveSelectedPatient, null, EditorContainerType.Regular,
				ViewerName.InvoicePayment_Viewer, DB_CommonTransactionType.CreateNew,
				".... الخـزينـــــة ....", true);
			if (ParentForm != null)
				ParentForm.Close();
		}
	}
}
