using System.Windows.Forms;
using DevExpress.XtraEditors;
using MerkDataBaseBusinessLogicProject;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;

namespace CommonUserControls.PEMRCommonViewers
{
	public partial class SendToStageCard_UC : UserControl
	{
		private StationPointStage_cu ActiveStage { get; set; }
		private GetBriefQueue_Result QueueResult { get; set; }

		public SendToStageCard_UC()
		{
			InitializeComponent();
		}

		public void Initialize(StationPointStage_cu activeStage, GetBriefQueue_Result queueResult)
		{
			if (activeStage == null)
				return;

			ActiveStage = activeStage;
			QueueResult = queueResult;
			btnStage.Text = ActiveStage.Name_S;
		}

		private void btnStage_Click(object sender, System.EventArgs e)
		{
			if (MerkDBBusinessLogicEngine.UpdateAndSave_QueueManagerStatus(QueueResult.QueueManagerID,
				DB_QueueManagerStatus.Waiting))
				XtraMessageBox.Show("Sent Successfully");
			else
				XtraMessageBox.Show("Not Sent");
		}
	}
}
