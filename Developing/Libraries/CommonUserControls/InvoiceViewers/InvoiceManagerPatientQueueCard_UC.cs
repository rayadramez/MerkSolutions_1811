using System;
using System.Drawing;
using CommonUserControls.QueueManagerViewers;
using DevExpress.XtraLayout.Utils;
using MerkDataBaseBusinessLogicProject;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MVCBusinessLogicLibrary.BaseViewers;

namespace CommonUserControls.InvoiceViewers
{
	public partial class InvoiceManagerPatientQueueCard_UC : DevExpress.XtraEditors.XtraUserControl
	{
		private InvoiceManagerQueueContainerWithHeaderIcons_UC ParentInvoiceManagerQueueContainerWithHeaderIcons_UC { get;
			set; }
		private QueueManagerContainer_UC ParentQueueManagerContainer_UC { get; set; }

		public int PatientID { get; set; }
		public string PatientName { get; set; }
		public int DoctorID { get; set; }
		public string ServiceName { get; set; }
		public DB_InvoiceType InvoiceType { get; set; }

		public Invoice ActiveInvoice { get; set; }
		public Patient_cu ActivePatient { get; set; }
		public InvoiceDetail ActiveInvoiceDetail { get; set; }
		public QueueManager ActiveQueueManager { get; set; }

		public InvoiceManagerPatientQueueCard_UC()
		{
			InitializeComponent();
		}

		public void Initialize(InvoiceManagerQueueContainerWithHeaderIcons_UC parentContainer,
			ReadyInvoicesForAction readyInvoice)
		{
			ParentInvoiceManagerQueueContainerWithHeaderIcons_UC = parentContainer;
			lblPatientID.Text = readyInvoice.PatientID.ToString();
			lblPatientName.Text = readyInvoice.PatientFullName.ToString();
			lblDoctorID.Text = readyInvoice.DoctorID.ToString();
			lblDoctorName.Text = readyInvoice.DoctorName.ToString();
			lblServiceName.Text = "------------------------------";
			lyt_lblStationPointName.Visibility = LayoutVisibility.Never;
			lblReservationTime.Text =
				Convert.ToDateTime(readyInvoice.InvoiceCreationDate).ConvertDateTimeToString(true, true);
			InvoiceType = readyInvoice.InvoiceType;
			ActiveInvoice = readyInvoice.ActiveInvoice;

			BackColor = Color.FromArgb(80, 80, 85);
		}

		public void Initialize(QueueManagerContainer_UC parentContainer, Invoice invoice, Patient_cu patient,
			InvoiceDetail invoiceDetail, QueueManager queueManager, string patientID,
			string patientName, string doctorID,
			string doctorName, string serviceName, string reservationDate)
		{
			ParentQueueManagerContainer_UC = parentContainer;
			lblPatientID.Text = patientID;
			lblPatientName.Text = patientName;
			lblDoctorID.Text = doctorID;
			lblDoctorName.Text = doctorName;
			lblServiceName.Text = serviceName;
			lblReservationTime.Text = Convert.ToDateTime(invoice.InvoiceCreationDate).ConvertDateTimeToString(true, true);
			if (queueManager != null)
			{
				StationPoint_cu stPoint =
								StationPoint_cu.ItemsList.Find(
									item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(queueManager.StationPoint_CU_ID)));
				if (stPoint != null)
					lblStationPointName.Text = "العيــادة : " + stPoint.Name_P;

				lyt_lblStationPointName.Visibility = LayoutVisibility.Always;
			}

			ActiveInvoice = invoice;
			ActivePatient = patient;
			ActiveInvoiceDetail = invoiceDetail;
			ActiveQueueManager = queueManager;

			BackColor = Color.FromArgb(80, 80, 85);
		}

		private void btnPlay_Click(object sender, System.EventArgs e)
		{
			if (ParentInvoiceManagerQueueContainerWithHeaderIcons_UC != null)
			{
				ParentInvoiceManagerQueueContainerWithHeaderIcons_UC.PassInvoice(ActiveInvoice);
				ParentInvoiceManagerQueueContainerWithHeaderIcons_UC.CollapseLeftPanel(true);
				ParentInvoiceManagerQueueContainerWithHeaderIcons_UC.ShowInvoiceContainer(true);
				return;
			}
			if (ParentQueueManagerContainer_UC != null)
			{
				QueueManager_Actions_UC patientActions = new QueueManager_Actions_UC();
				patientActions.Initialize(ActiveInvoice, ActivePatient, ActiveInvoiceDetail, ActiveQueueManager);
				PopupBaseForm.ShowAsPopup(patientActions, this);
			}
		}
	}
}
