using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using CommonUserControls.InvoiceViewers;
using MerkDataBaseBusinessLogicProject;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;

namespace CommonUserControls.QueueManagerViewers
{
	public interface IDoctorQueueSelection
	{
		object DoctorID { get; set; }
	}

	public partial class QueueManagerContainer_UC : UserControl, IDoctorQueueSelection
	{
		private QueueManager_DoctorCard_UC _checkedDoctor;
		private Dictionary<int, QueueManager_DoctorCard_UC> CheckButtonDictionaryList { get; set; }

		public QueueManagerContainer_UC()
		{
			InitializeComponent();

			InitializeDoctorList();
		}

		public void Initialize()
		{
			tabWaitingPatients.Controls.Clear();
			tabServedPatients.Controls.Clear();
			tabPausedPatients.Controls.Clear();

			List<GetBriefQueue_Result> allQueues = null;
			allQueues = MerkDBBusinessLogicEngine.GetBriefQueue((int?)DoctorID, null, null, null);

			if (allQueues == null || allQueues.Count == 0)
				return;

			List<GetBriefQueue_Result> waitingQueue =
				allQueues.FindAll(item => Convert.ToInt32(item.QueueStatusID).Equals((int)DB_QueueManagerStatus.Waiting));
			List<GetBriefQueue_Result> pausedQueue =
				allQueues.FindAll(item => Convert.ToInt32(item.QueueStatusID).Equals((int)DB_QueueManagerStatus.Paused));
			List<GetBriefQueue_Result> servedQueue =
				allQueues.FindAll(item => Convert.ToInt32(item.QueueStatusID).Equals((int)DB_QueueManagerStatus.Served));

			#region waitingQueue
			if (waitingQueue.Count > 0)
			{
				foreach (GetBriefQueue_Result queue in waitingQueue)
				{
					InvoiceManagerPatientQueueCard_UC card = new InvoiceManagerPatientQueueCard_UC();
					card.Dock = DockStyle.Top;

					Invoice invoice = DBCommon.GetEntity<Invoice>(queue.InvoiceID);
					if (invoice == null)
						continue;
					Patient_cu patient = DBCommon.GetEntity<Patient_cu>(invoice.Patient_CU_ID);
					if (patient == null)
						continue;
					InvoiceDetail invoiceDetail = DBCommon.GetEntity<InvoiceDetail>(queue.InvoiceDetailID);
					if (invoiceDetail == null)
						continue;
					QueueManager queueManager = DBCommon.GetEntity<QueueManager>(queue.QueueManagerID);
					if(queueManager == null)
						continue;
					card.Initialize(this, invoice, patient, invoiceDetail, queueManager, queue.PatientID.ToString(), queue.PatientFullName,
						queue.DoctorID.ToString(), queue.DoctorFullName, queue.ServiceName, queue.ReservationTime.ToString());
					tabWaitingPatients.Controls.Add(card);
				}
			}
			#endregion

			#region PausedQueue
			if (pausedQueue.Count > 0)
			{
				foreach (GetBriefQueue_Result queue in pausedQueue)
				{
					InvoiceManagerPatientQueueCard_UC card = new InvoiceManagerPatientQueueCard_UC();
					card.Dock = DockStyle.Top;

					Invoice invoice = DBCommon.GetEntity<Invoice>(queue.InvoiceID);
					if (invoice == null)
						continue;
					Patient_cu patient = DBCommon.GetEntity<Patient_cu>(invoice.Patient_CU_ID);
					if (patient == null)
						continue;
					InvoiceDetail invoiceDetail = DBCommon.GetEntity<InvoiceDetail>(queue.InvoiceDetailID);
					if (invoiceDetail == null)
						continue;
					QueueManager queueManager = DBCommon.GetEntity<QueueManager>(queue.QueueManagerID);
					if (queueManager == null)
						continue;
					card.Initialize(this, invoice, patient, invoiceDetail, queueManager, queue.PatientID.ToString(), queue.PatientFullName,
						queue.DoctorID.ToString(), queue.DoctorFullName, queue.ServiceName, queue.ReservationTime.ToString());
					tabPausedPatients.Controls.Add(card);
				}
			}
			#endregion

			#region servedQueue
			if (servedQueue.Count > 0)
			{
				foreach (GetBriefQueue_Result queue in servedQueue)
				{
					InvoiceManagerPatientQueueCard_UC card = new InvoiceManagerPatientQueueCard_UC();
					card.Dock = DockStyle.Top;

					Invoice invoice = DBCommon.GetEntity<Invoice>(queue.InvoiceID);
					if (invoice == null)
						continue;
					Patient_cu patient = DBCommon.GetEntity<Patient_cu>(invoice.Patient_CU_ID);
					if (patient == null)
						continue;
					InvoiceDetail invoiceDetail = DBCommon.GetEntity<InvoiceDetail>(queue.InvoiceDetailID);
					if(invoiceDetail == null)
						continue;
					QueueManager queueManager = DBCommon.GetEntity<QueueManager>(queue.QueueManagerID);
					if (queueManager == null)
						continue;
					card.Initialize(this, invoice, patient, invoiceDetail, queueManager, queue.PatientID.ToString(), queue.PatientFullName,
						queue.DoctorID.ToString(), queue.DoctorFullName, queue.ServiceName, queue.ReservationTime.ToString());
					tabServedPatients.Controls.Add(card);
				}
			}
			#endregion

			if (DoctorID != null)
			{
				Doctor_cu doctor =
					Doctor_cu.ItemsList.Find(item => Convert.ToInt32(item.Person_CU_ID).Equals(Convert.ToInt32(DoctorID)));
				if (doctor != null)
					tabDoctorName.Text = doctor.Name_P;
			}
		}

		public void InitializeDoctorList()
		{
			tabDoctorCards.Controls.Clear(); 
			List<GetBriefQueue_Result> allQueues = null;
			allQueues = MerkDBBusinessLogicEngine.GetBriefQueue(null, null, null, null);

			List<IGrouping<int, GetBriefQueue_Result>> groupedByList = allQueues.GroupBy(item => item.DoctorID).ToList();

			if (groupedByList.Count == 0)
				return;

			if (CheckButtonDictionaryList == null)
				CheckButtonDictionaryList = new Dictionary<int, QueueManager_DoctorCard_UC>();

			foreach (IGrouping<int, GetBriefQueue_Result> queueResults in groupedByList)
			{
				QueueManager_DoctorCard_UC doctorCard = new QueueManager_DoctorCard_UC();
				doctorCard.Dock = DockStyle.Right;
				Doctor_cu doctor =
					Doctor_cu.ItemsList.Find(item => Convert.ToInt32(item.Person_CU_ID).Equals(Convert.ToInt32(queueResults.Key)));
				if (doctor == null)
					continue;
				doctorCard.Initiaize(doctor, this);
				tabDoctorCards.Controls.Add(doctorCard);

				CheckButtonDictionaryList.Add(doctor.Person_CU_ID, doctorCard);
			}
		}

		public void SetDoctorCheckState(bool isChecked, int checkButtonKey)
		{
			if(CheckButtonDictionaryList != null)
				if (CheckButtonDictionaryList.TryGetValue(checkButtonKey, out _checkedDoctor))
				{
					foreach (KeyValuePair<int, QueueManager_DoctorCard_UC> keyValuePair in CheckButtonDictionaryList)
					{
						QueueManager_DoctorCard_UC chkButton = keyValuePair.Value;
						chkButton.IsCheckChanged = false;
						if (keyValuePair.Key.Equals(checkButtonKey))
						{
							chkButton.IsCheckChanged = true;
							chkButton.SetCheckState(true);
							continue;
						}
						chkButton.IsCheckChanged = true;
						chkButton.SetCheckState(false);
					}
				}
		}

		private void btnRefresh_Click(object sender, EventArgs e)
		{
			InitializeDoctorList();
			Initialize();
		}

		#region Implementation of IDoctorQueueSelection

		public object DoctorID { get; set; }

		#endregion
	}
}
