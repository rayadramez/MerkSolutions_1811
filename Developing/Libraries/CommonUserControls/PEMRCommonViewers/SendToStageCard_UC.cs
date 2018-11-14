using System.Windows.Forms;
using MerkDataBaseBusinessLogicProject;

namespace CommonUserControls.PEMRCommonViewers
{
	public partial class SendToStageCard_UC : UserControl
	{
		private StationPointStage_cu ActiveStage { get; set; }

		public SendToStageCard_UC()
		{
			InitializeComponent();
		}

		public void Initialize(StationPointStage_cu activeStage)
		{
			if (activeStage == null)
				return;

			ActiveStage = activeStage;
			btnStage.Text = ActiveStage.Name_S;
		}

		private void btnStage_Click(object sender, System.EventArgs e)
		{

		}
	}
}
