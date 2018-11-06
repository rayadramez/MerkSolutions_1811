using System;
using System.Windows.Forms;
using MerkDataBaseBusinessLogicProject;
using MVCBusinessLogicLibrary.BaseViewers;

namespace CommonUserControls.QueueManagerViewers
{
	public partial class QueueManager_Actions_UC : UserControl
	{
		public Invoice ActiveInvoice { get; set; }
		public Patient_cu ActivePatient { get; set; }
		public InvoiceDetail ActiveInvoiceDetail { get; set; }
		public QueueManager ActiveQueueManager { get; set; }

		public QueueManager_Actions_UC()
		{
			InitializeComponent();
		}

		public void Initialize(Invoice activeInvoice, Patient_cu activePatient, InvoiceDetail invoiceDetail, QueueManager queueManager)
		{
			ActiveInvoice = activeInvoice;
			ActivePatient = activePatient;
			ActiveInvoiceDetail = invoiceDetail;
			ActiveQueueManager = queueManager;
		}

		private void btnChangeDoctor_Click(object sender, EventArgs e)
		{
			QueueManager_DoctorSelection_UC doctorSelection = new QueueManager_DoctorSelection_UC();
			doctorSelection.Initialize(ActiveInvoiceDetail, ActiveQueueManager);
			PopupBaseForm.ShowAsPopup(doctorSelection, this);
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			if(ParentForm != null)
				ParentForm.Close();
		}
	}
}
