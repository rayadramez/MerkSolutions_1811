﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using System.Windows.Forms;
using ApplicationConfiguration;
using CommonControlLibrary;
using MerkDataBaseBusinessLogicProject;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;
using Timer = System.Windows.Forms.Timer;

namespace CommonUserControls.PEMRCommonViewers
{
	public partial class PEMR_PatientCardContainer_UC : DevExpress.XtraEditors.XtraUserControl
	{
		public static PEMRContainer ParentContainer { get; set; }
		private readonly System.Timers.Timer _timer;
		private readonly Timer _activeTimer;

		public PEMR_PatientCardContainer_UC()
		{
			InitializeComponent();
			Initialize();
			_timer = new System.Timers.Timer();
			_activeTimer = new Timer();
		}

		public void Initialize()
		{
			if (ApplicationStaticConfiguration.ActiveLoginUser == null)
				return;

			MerkDBBusinessLogicEngine.LoadDefaultStationPointStage();

			List<StationPointStage_cu> list = MerkDBBusinessLogicEngine.GetAllStationPointStages();
			CommonViewsActions.FillGridlookupEdit(lkeStationPointStages,
				MerkDBBusinessLogicEngine.GetOrganizationMachineStationPointStages(
					ApplicationStaticConfiguration.OrganizationMachine,
					ApplicationStaticConfiguration.Application), "Name_S");
			if (MerkDBBusinessLogicEngine.ActiveStationPointStage != null)
				lkeStationPointStages.EditValue = MerkDBBusinessLogicEngine.ActiveStationPointStage.ID;
		}

		public void Initialize(StationPointStage_cu stationPointStage)
		{
			if (stationPointStage == null)
				return;

			tabWaiting.Controls.Clear();
			tabPaused.Controls.Clear();
			tabServed.Controls.Clear();

			List<GetBriefQueue_Result> allQueues =
				MerkDBBusinessLogicEngine.GetBriefQueue(ApplicationStaticConfiguration.ActiveLoginUser.Person_CU_ID,
					stationPointStage.ID, DateTime.Now.Date, null);

			if (allQueues == null || allQueues.Count == 0)
				return;

			List<GetBriefQueue_Result> waitingQueue =
				allQueues.FindAll(item => Convert.ToInt32(item.QueueStatusID).Equals((int)DB_QueueManagerStatus.Waiting));

			List<GetBriefQueue_Result> pausedQueue =
				allQueues.FindAll(item => Convert.ToInt32(item.QueueStatusID).Equals((int)DB_QueueManagerStatus.Paused));

			List<GetBriefQueue_Result> servedQueue =
				allQueues.FindAll(item => Convert.ToInt32(item.QueueStatusID).Equals((int)DB_QueueManagerStatus.Served));

			if (waitingQueue.Count > 0)
			{
				foreach (GetBriefQueue_Result queueResult in waitingQueue.OrderByDescending(item => item.ReservationTime))
				{
					PEMRPatientQueueCard card = new PEMRPatientQueueCard();
					card.Dock = DockStyle.Top;
					card.Initialize(ParentContainer, queueResult);
					tabWaiting.Controls.Add(card);
				}
			}

			if (pausedQueue.Count > 0)
			{
				foreach (GetBriefQueue_Result queue in pausedQueue.OrderByDescending(item => item.ReservationTime))
				{
					PEMRPatientQueueCard card = new PEMRPatientQueueCard();
					card.Dock = DockStyle.Top;
					card.Initialize(ParentContainer, queue);
					tabPaused.Controls.Add(card);
				}
			}

			if (servedQueue.Count > 0)
			{
				foreach (GetBriefQueue_Result queue in servedQueue.OrderByDescending(item => item.ReservationTime))
				{
					PEMRPatientQueueCard card = new PEMRPatientQueueCard();
					card.Dock = DockStyle.Top;
					card.Initialize(ParentContainer, queue);
					tabServed.Controls.Add(card);
				}
			}
		}

		private void Reload(object sender, EventArgs e)
		{
			_activeTimer.Stop();

			_activeTimer.Start();
		}

		private void ActiveTimerOnElapsed(object sender, ElapsedEventArgs elapsedEventArgs)
		{
			Initialize(MerkDBBusinessLogicEngine.ActiveStationPointStage);
			MerkDBBusinessLogicEngine.ActiveTimer.Interval = GetInterval();
			MerkDBBusinessLogicEngine.ActiveTimer.Start();
		}

		public static double GetInterval()
		{
			DateTime now = DateTime.Now;
			return ((60 - now.Second) * 1000 - now.Millisecond);
		}

		private void lkeStationPointStages_EditValueChanged(object sender, EventArgs e)
		{
			if (lkeStationPointStages.EditValue == null)
				return;

			tabWaiting.Controls.Clear();
			tabPaused.Controls.Clear();
			tabServed.Controls.Clear();

			StationPointStage_cu stationPointStage =
				StationPointStage_cu.ItemsList.Find(
					item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(lkeStationPointStages.EditValue)));
			if (stationPointStage == null)
				return;

			MerkDBBusinessLogicEngine.ActiveStationPointStage = stationPointStage;

			Initialize(MerkDBBusinessLogicEngine.ActiveStationPointStage);
		}

		private void btnRefresh_Click(object sender, EventArgs e)
		{
			tabWaiting.Controls.Clear();
			tabPaused.Controls.Clear();
			tabServed.Controls.Clear();

			Initialize(MerkDBBusinessLogicEngine.ActiveStationPointStage);
		}
	}
}
