using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CommonControlLibrary;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using MerkDataBaseBusinessLogicProject;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MVCBusinessLogicLibrary.BaseViewers;
using MVCBusinessLogicLibrary.MVCFactories;

namespace CommonUserControls.InvoiceViewers
{
	public partial class ReadyInvoiceForPayments_UC : DevExpress.XtraEditors.XtraUserControl
	{
		private Patient_cu ActivePatient { get; set; }
		private List<GetInvoiceForAddmission_Result> result = new List<GetInvoiceForAddmission_Result>();
		private List<ReadyInvoicesForAction> readyInvoicesList = new List<ReadyInvoicesForAction>();
		private InvoiceObject InvoiceObject { get; set; }
		private InvoicePayment_UC _invoicePayment;

		public ReadyInvoiceForPayments_UC()
		{
			InitializeComponent();
			CommonViewsActions.SetupSyle(this);
			CommonViewsActions.SetupGridControl(gridControl1, Resources.LocalizedRes.grd_ReadyInvoicesForPayments, false);

		}

		public void Initialize(Patient_cu patient, AdmissionType admissionType)
		{
			ActivePatient = patient;
			readyInvoicesList = MerkDBBusinessLogicEngine.ReadyInvoicesForAction(admissionType, null, null, true, true, false, null,
				null, ActivePatient);

			gridControl1.DataSource = readyInvoicesList.Count > 0 ? readyInvoicesList : null;
		}

		private void btnPayments_Click(object sender, System.EventArgs e)
		{
			if (InvoiceObject == null)
			{
				XtraMessageBox.Show("يجب إختيار الخدمة", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Error, DefaultBoolean.Default);
				return;
			}

			if (ParentForm != null)
				ParentForm.Close();

			BaseController<InvoicePayment>.ShowEditorControl(ref _invoicePayment, this, null, null, EditorContainerType.Regular,
				ViewerName.InvoicePayment_Viewer, DB_CommonTransactionType.CreateNew, "الخزينــــة", true, false);
			if (_invoicePayment != null)
				_invoicePayment.MedicalInitialize(InvoiceObject.ActiveInvoice);
		}

		private void btnClose_Click(object sender, System.EventArgs e)
		{
			if(ParentForm != null)
				ParentForm.Close();
		}

		private void gridView1_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			var rowObject = CommonViewsActions.GetSelectedRowObject(layoutView1);
			if (rowObject is ReadyInvoicesForAction)
			{
				InvoiceObject =
					MerkDBBusinessLogicEngine.GetInvoiceFullTree(Convert.ToInt32((rowObject as ReadyInvoicesForAction).InvoiceID));
				btnPayments.Enabled = true;
			}
		}
	}
}
