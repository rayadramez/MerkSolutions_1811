using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ApplicationConfiguration;
using CommonControlLibrary;
using DevExpress.XtraEditors;
using MerkDataBaseBusinessLogicProject;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;

namespace CommonUserControls.PEMRCommonViewers
{
	public partial class PEMR_SendToStage_UC : UserControl
	{
		private StationPointStage_cu ActiveStage { get; set; }

		public PEMR_SendToStage_UC()
		{
			InitializeComponent();
		}

		public void Initialize(StationPointStage_cu activeStage)
		{
			if(activeStage == null)
				return;
			
			ActiveStage = activeStage;

			List<StationPointStage_cu> stationPointStagesList =
				MerkDBBusinessLogicEngine.GetOrganizationMachineStationPointStages(
					ApplicationStaticConfiguration.OrganizationMachine, DB_Application.All);
			if (stationPointStagesList != null && stationPointStagesList.Count > 0)
			{
				stationPointStagesList = stationPointStagesList.OrderBy(item => item.OrderIndex).ToList();
				MerkDBBusinessLogicEngine.ActiveStationPointStage = ActiveStage;
			}

			CommonViewsActions.FillGridlookupEdit(lkeStationPointStages, stationPointStagesList, "Name_S");
			if (MerkDBBusinessLogicEngine.ActiveStationPointStage != null)
				lkeStationPointStages.EditValue = MerkDBBusinessLogicEngine.ActiveStationPointStage.ID;

			if (stationPointStagesList != null)
				foreach (StationPointStage_cu stationPointStageCu in stationPointStagesList.FindAll(item =>
					!Convert.ToInt32(item.ID).Equals(Convert.ToInt32(ActiveStage.ID))))
				{
					SendToStageCard_UC card = new SendToStageCard_UC();
					card.Dock = DockStyle.Top;
					card.Initialize(stationPointStageCu);
					tabPage.Controls.Add(card);
				}
		}

		private void lkeStationPointStages_EditValueChanged(object sender, System.EventArgs e)
		{
		}
	}
}
