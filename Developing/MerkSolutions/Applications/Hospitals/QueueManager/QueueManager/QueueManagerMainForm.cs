using System;
using CommonControlLibrary;
using CommonUserControls.QueueManagerViewers;

namespace QueueManager
{
	public partial class QueueManagerMainForm : DevExpress.XtraEditors.XtraForm
	{
		private QueueManagerContainer_UC _queueManagerContainer;

		public QueueManagerMainForm()
		{
			InitializeComponent();
		}

		private void btnDoctorQueues_Click(object sender, EventArgs e)
		{
			CommonViewsActions.ShowUserControl(ref _queueManagerContainer, this);
		}
	}
}