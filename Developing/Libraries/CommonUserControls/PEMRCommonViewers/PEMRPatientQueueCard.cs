using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ApplicationConfiguration;
using DevExpress.XtraEditors;
using MerkDataBaseBusinessLogicProject;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;

namespace CommonUserControls.PEMRCommonViewers
{
	public partial class PEMRPatientQueueCard : XtraUserControl
	{
		private PEMRContainer ParentPEMRContainer { get; set; }
		public GetBriefQueue_Result ActiveQueueResult { get; set; }
		public QueueManager ActiveQueuemanager { get; set; }
		public int InvoiceDetailID { get; set; }
		public int PatientID { get; set; }
		public string PatientName { get; set; }
		public int DoctorID { get; set; }
		public string DoctorName { get; set; }
		public string ServiceName { get; set; }
		public string ReservationDateTime { get; set; }

		public PEMRPatientQueueCard()
		{
			InitializeComponent();
		}

		public void Initialize(PEMRContainer parentContainer, GetBriefQueue_Result queueResult)
		{
			if (queueResult == null || queueResult.PatientID == null)
				return;

			ParentPEMRContainer = parentContainer;
			ActiveQueueResult = queueResult;
			InvoiceDetailID = queueResult.InvoiceDetailID;
			PatientID = Convert.ToInt32(queueResult.PatientID);
			PatientName = queueResult.PatientFullName;
			DoctorID = Convert.ToInt32(queueResult.DoctorID);
			DoctorName = queueResult.DoctorFullName;
			ServiceName = queueResult.ServiceName;
			ReservationDateTime = Convert.ToDateTime(queueResult.ReservationTime)
				.ConvertDateTimeToString(true, true, true);

			lblPatientID.Text = PatientID.ToString();
			lblPatientName.Text = PatientName;
			lblServiceName.Text = ServiceName;
			lblReservationTime.Text = ReservationDateTime;

			if (queueResult.ServiceID == null)
				return;

			Service_cu service = Service_cu.ItemsList.Find(item =>
				Convert.ToInt32(item.ID).Equals(Convert.ToInt32(queueResult.ServiceID)));
			if (service == null)
				return;

			ServiceCategory_cu serviceCategory = ServiceCategory_cu.ItemsList.Find(item =>
				Convert.ToInt32(item.ID).Equals(Convert.ToInt32(service.ServiceCategory_CU_ID)));
			if(serviceCategory == null)
				return;
			
			//TODO :: just add DisplayingColor to ServiceCategoryTable and it will work
			//if(serviceCategory.DisplayingColor == null)
			//	if(service.DisplayingColor == null)
			//		return;
			//	else
			//	{
			//		object backColor = ApplicationStaticConfiguration.GetSkinColor(service.DisplayingColor);
			//		if (backColor != null)
			//			this.BackColor = Color.FromArgb(((Color) backColor).R, ((Color) backColor).G,
			//				((Color) backColor).B);
			//	}
			//else
			//{
			//	object backColor = ApplicationStaticConfiguration.GetSkinColor(serviceCategory.DisplayingColor);
			//	if (backColor != null)
			//		this.BackColor = Color.FromArgb(((Color)backColor).R, ((Color)backColor).G,
			//			((Color)backColor).B);
			//}
		}

		private void btnPlay_Click(object sender, System.EventArgs e)
		{
			DialogResult result = XtraMessageBox.Show("Do you want to run serving this patient ?", "Notice",
				MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
			switch (result)
			{
				case DialogResult.Yes:
					if (ActiveQueueResult == null || MerkDBBusinessLogicEngine.ActiveStationPoint == null ||
					    MerkDBBusinessLogicEngine.ActiveStationPointStage == null)
						return;

					if (ActiveQueueResult == null)
						return;

					DB_QueueManagerStatus queueManagerStatus = (DB_QueueManagerStatus) ActiveQueueResult.QueueStatusID;
					switch (queueManagerStatus)
					{
						case DB_QueueManagerStatus.Paused:
						case DB_QueueManagerStatus.Served:
							List<GetPreviousMedicalVisits_Result> previousVisitsList =
								MerkDBBusinessLogicEngine.GetPreviousMedicalVisitsList(PatientID, true,
									null, DateTime.Now.Date, DateTime.Now.Date, DoctorID);
							if (previousVisitsList.Count > 0)
								PEMRBusinessLogic.ActivePEMRObject =
									PEMRBusinessLogic.GetVisitFullTree(previousVisitsList[0]);
							break;
						case DB_QueueManagerStatus.Waiting:
							PEMRBusinessLogic.ActiveVisitTimming = PEMRBusinessLogic.CreateNewVisitTiming(
								ActiveQueueResult.InvoiceDetailID, MerkDBBusinessLogicEngine.ActiveStationPoint.ID,
								MerkDBBusinessLogicEngine.ActiveStationPointStage.ID, DoctorID, DateTime.Now,
								ApplicationStaticConfiguration.PEMRSavingMode);
							break;
					}

					if (PEMRBusinessLogic.ActiveVisitTimming == null)
						return;

					ParentPEMRContainer.ShowLeftQueuePanel(true);
					ParentPEMRContainer.ShowPEMRHistoryContainer(ActiveQueueResult, PEMRBusinessLogic.ActiveVisitTimming, true);
					break;
			}
		}

		private void btnPause_Click(object sender, System.EventArgs e)
		{

		}

		private void btnStop_Click(object sender, System.EventArgs e)
		{
			
		}
	}
}
