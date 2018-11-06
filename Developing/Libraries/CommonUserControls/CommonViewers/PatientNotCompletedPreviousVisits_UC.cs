using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ApplicationConfiguration;
using CommonControlLibrary;
using CommonUserControls.InvoiceViewers;
using DevExpress.XtraEditors;
using MerkDataBaseBusinessLogicProject;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;
using MVCBusinessLogicLibrary.BaseViewers;
using MVCBusinessLogicLibrary.MVCFactories;

namespace CommonUserControls.CommonViewers
{
	public enum PatientNotCompletedPreviousVisitsAction
	{
		InvoiceManager = 1,
		Payment = 2,
		InvoiceCreation = 3
	}

	public partial class PatientNotCompletedPreviousVisits_UC : XtraUserControl
	{
		public Control TopMaxParentControl { get; set; }
		public MainPatientInvoiceActions MainPatientInvoiceActionsControl { get; set; }
		public Invoice ActiveInvoice { get; set; }
		public InvoiceContainer_UC _invoiceContainer;
		private PatientNotCompletedPreviousVisitsAction PatientNotCompletedPreviousVisitsAction { get; set; }
		private InvoicePayment_UC _invoicePayment;
		private MedicalAdmissionInvoiceCreationContainer_UC _medicalAdmissionInvoiceCreationContainer;

		public PatientNotCompletedPreviousVisits_UC()
		{
			InitializeComponent();

			CommonViewsActions.SetupGridControl(grdPatientsList, Resources.LocalizedRes.grd_PatientPrviousInvoices, false);
			CommonViewsActions.SetupSyle(this);

			if (!string.IsNullOrEmpty(ApplicationStaticConfiguration.SkinName) &&
				!string.IsNullOrWhiteSpace(ApplicationStaticConfiguration.SkinName))
				if (!ApplicationStaticConfiguration.SkinName.Equals("McSkin"))
					layoutControlItem1.AppearanceItemCaption.ForeColor = Color.OldLace;
				else
					layoutControlItem1.AppearanceItemCaption.ForeColor = Color.Navy;
		}

		public void Initialize(Patient_cu patient, Control parentControl,
			MainPatientInvoiceActions mainPatientInvoiceActionsControl,
			PatientNotCompletedPreviousVisitsAction patientNotCompletedPreviousVisitsAction)
		{
			TopMaxParentControl = parentControl;
			MainPatientInvoiceActionsControl = mainPatientInvoiceActionsControl;
			List<GetPatientPreviousInvoices_Result> list =
				DBCommon.DBContext_External.GetPatientPreviousInvoices(patient.Person_CU_ID, false, true, null, null)
					.OrderBy(item => item.InvoiceCreationDate)
					.ToList();

			grdPatientsList.DataSource = list;
			PatientNotCompletedPreviousVisitsAction = patientNotCompletedPreviousVisitsAction;
		}

		private void gridView1_DoubleClick(object sender, System.EventArgs e)
		{
			GetPatientPreviousInvoices_Result selected =
				CommonViewsActions.GetSelectedRowObject<GetPatientPreviousInvoices_Result>(gridView1);
			if (selected == null)
				return;

			int invoiceID = selected.InvoiceID;
			ActiveInvoice = DBCommon.GetEntity<Invoice>(invoiceID);

			if (ActiveInvoice == null)
				return;

			DialogResult result =
				XtraMessageBox.Show(
					"هـل تـرغــب فـي إختيــار فـاتـــورة المـريــض ؟" + "\r\n" + "تـاريــخ الفـاتـــورة : " +
					selected.InvoiceCreationDate.Date.ToShortDateString(), "تنيـــه", MessageBoxButtons.YesNo,
					MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
			switch (result)
			{
				case DialogResult.Yes:
					switch (PatientNotCompletedPreviousVisitsAction)
					{
						case PatientNotCompletedPreviousVisitsAction.InvoiceManager:
							CommonViewsActions.ShowUserControl(ref _invoiceContainer, TopMaxParentControl);
							if(_invoiceContainer != null)
								_invoiceContainer.Initialize(TopMaxParentControl, ActiveInvoice);
							break;
						case PatientNotCompletedPreviousVisitsAction.Payment:
							BaseController<InvoicePayment>.ShowEditorControl(
								ref _invoicePayment, this, ActiveInvoice, null, EditorContainerType.Regular,
								ViewerName.InvoicePayment_Viewer, DB_CommonTransactionType.CreateNew,
								"مـدفـوعــــات المـريـــض", true);
							break;
						case PatientNotCompletedPreviousVisitsAction.InvoiceCreation:
							BaseController<Invoice>.ShowEditorControl(ref _medicalAdmissionInvoiceCreationContainer,
								this, ActiveInvoice.PatientObject, ActiveInvoice,
								EditorContainerType.Regular,
								ViewerName.PatientInvoiceCreation,
								DB_CommonTransactionType.UpdateExisting,
								"الخـدمــــات الطبيــــــة", true);
							break;
					}
					break;
			}

			if (this.ParentForm != null)
			{
				if (MainPatientInvoiceActionsControl != null)
					if (MainPatientInvoiceActionsControl.ParentForm != null)
						MainPatientInvoiceActionsControl.ParentForm.Close();
				ParentForm.Close();
			}
		}

		private void btnClose_Click(object sender, System.EventArgs e)
		{
			if (this.ParentForm != null)
				ParentForm.Close();
		}
	}
}
