using System;
using System.Windows.Forms;
using MerkDataBaseBusinessLogicProject;
using CheckButton = DevExpress.XtraEditors.CheckButton;

namespace CommonUserControls.QueueManagerViewers
{
	public partial class QueueManager_DoctorCard_UC : UserControl, IDoctorQueueSelection
	{
		private Doctor_cu ActiveDoctor { get; set; }
		public bool IsCheckChanged { get; set; }
		private QueueManagerContainer_UC ParentQueueManagerContainer { get; set; }

		public QueueManager_DoctorCard_UC()
		{
			InitializeComponent();
		}

		public void Initiaize(Doctor_cu doctor, QueueManagerContainer_UC parentQueueManagerContainer)
		{
			if (doctor == null)
				return;

			ActiveDoctor = doctor;

			if (ActiveDoctor != null)
				DoctorID = ActiveDoctor.ID;

			chkDoctor.Text = ActiveDoctor.Name_P;
			ParentQueueManagerContainer = parentQueueManagerContainer;
			chkDoctor.CheckedChanged += ChkDoctorOnCheckedChanged;

			IsCheckChanged = false;
		}

		private void ChkDoctorOnCheckedChanged(object sender, EventArgs eventArgs)
		{
			if (ParentQueueManagerContainer != null)
			{
				ParentQueueManagerContainer.DoctorID = DoctorID;
				ParentQueueManagerContainer.Initialize();
				if (!IsCheckChanged)
					ParentQueueManagerContainer.SetDoctorCheckState(true, Convert.ToInt32(DoctorID));
			}
		}

		private void chkDoctor_CheckedChanged(object sender, System.EventArgs e)
		{
			if (ActiveDoctor != null)
				DoctorID = ActiveDoctor.ID;
		}

		public CheckButton GetCheckButton()
		{
			return chkDoctor;
		}

		public void SetCheckState(bool isCheck)
		{
			GetCheckButton().Checked = isCheck;
		}

		#region Implementation of IDoctorQueueSelection

		public object DoctorID { get; set; }

		#endregion
	}
}
