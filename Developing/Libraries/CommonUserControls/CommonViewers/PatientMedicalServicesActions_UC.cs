using System;
using System.Windows.Forms;
using ApplicationConfiguration;
using CommonControlLibrary;
using CommonUserControls.InvoiceViewers;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using MerkDataBaseBusinessLogicProject;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MVCBusinessLogicLibrary.BaseViewers;
using MVCBusinessLogicLibrary.MVCFactories;

namespace CommonUserControls.CommonViewers
{
	public partial class PatientMedicalServicesActions_UC : XtraUserControl
	{
		public Patient_cu ActivePatient { get; set; }
		public Invoice ActiveInvoice { get; set; }
		private MedicalAdmissionInvoiceCreationContainer_UC _medicalAdmissionInvoiceCreationContainer;
		private MainPatientInvoiceActions MainPatientInvoiceActions { get; set; }
		public Control ParentControl { get; set; }

		public PatientMedicalServicesActions_UC()
		{
			InitializeComponent();

			CommonViewsActions.SetupSyle(this);
			if (!string.IsNullOrEmpty(ApplicationStaticConfiguration.SkinName) &&
			    !string.IsNullOrWhiteSpace(ApplicationStaticConfiguration.SkinName))
				if (ApplicationStaticConfiguration.SkinName.Equals("McSkin"))
					btnClose.Image = Properties.Resources.ExitIcon_8;
				else
					btnClose.Image = Properties.Resources.Exit_1_16;
		}

		public void Initialize(Patient_cu patient, MainPatientInvoiceActions mainPatientInvoiceActions,
			Control parentControl)
		{
			ActivePatient = patient;
			MainPatientInvoiceActions = mainPatientInvoiceActions;
			ParentControl = parentControl;
		}

		private void btnClose_Click(object sender, System.EventArgs e)
		{
			if (ParentForm != null)
				ParentForm.Close();
		}

		private void btnCreateNewInvoice_Click(object sender, System.EventArgs e)
		{
			DialogResult result;
			BaseController<Invoice>.ShowEditorControl(ref _medicalAdmissionInvoiceCreationContainer, this,
			                                          ActivePatient, null, EditorContainerType.Regular,
			                                          ViewerName.PatientInvoiceCreation,
			                                          DB_CommonTransactionType.CreateNew, "الخـدمــــات الطبيــــــة",
			                                          true);
			if (BaseController<Invoice>.ActiveDBEntity != null)
				if (Convert.ToBoolean(_medicalAdmissionInvoiceCreationContainer.IsPaymentAttached))
				{
					result = XtraMessageBox.Show("هـل تـريـــد الطباعـــة ؟", "تنبيــــه", MessageBoxButtons.YesNo,
					                             MessageBoxIcon.Question, MessageBoxDefaultButton.Button1,
					                             DefaultBoolean.Default);
					switch (result)
					{
						case DialogResult.Yes:

							break;
					}
				}

			if (ParentForm != null)
				ParentForm.Close();
		}

		private void btnContinueOnPreviousInvoice_Click(object sender, EventArgs e)
		{
			PatientNotCompletedPreviousVisits_UC patientPreviousInvoices = new PatientNotCompletedPreviousVisits_UC();
			patientPreviousInvoices.Initialize(ActivePatient, ParentControl, MainPatientInvoiceActions,
			                                   PatientNotCompletedPreviousVisitsAction.InvoiceCreation);
			if (ParentForm != null)
				ParentForm.Close();
			PopupBaseForm.ShowAsPopup(patientPreviousInvoices, this);
		}
	}
}
