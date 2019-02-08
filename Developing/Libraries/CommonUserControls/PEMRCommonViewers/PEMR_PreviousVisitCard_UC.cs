using System;
using System.Windows.Forms;
using DevExpress.XtraLayout;
using DevExpress.XtraLayout.HitInfo;
using MerkDataBaseBusinessLogicProject;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;

namespace CommonUserControls.PEMRCommonViewers
{
	public partial class PEMR_PreviousVisitCard_UC : UserControl
	{
		private PEMRContainer ParentPEMRContainer { get; set; }
		private PEMRObject ActivePEMR { get; set; }
		private GetPreviousMedicalVisits_Result ActivePreviousMedicalVisit { get; set; }

		public PEMR_PreviousVisitCard_UC()
		{
			InitializeComponent();
		}

		public void Initialize(PEMRContainer parentContainer, PEMRObject pemrObject,
			GetPreviousMedicalVisits_Result visitResult)
		{
			ParentPEMRContainer = parentContainer;
			ActivePreviousMedicalVisit = visitResult;
			ActivePEMR = pemrObject;

			lblPatientID.Text = visitResult.PatientID.ToString();
			lblPatientName.Text = visitResult.PatientName;
			lblServiceName.Text = visitResult.ServiceName;
			lblReservationTime.Text = "[" + visitResult.SignOutDateString + " - " + visitResult.SignInDateString + "]";
			lytGroup.Text = visitResult.PatientName + " [" +
			                Convert.ToDateTime(visitResult.SignInDate).ConvertDateTimeToString(false, true, true) +
			                " - " + visitResult.SignOutDateString + "]";
			lytGroup.Expanded = false;
			ParentPEMRContainer.ShowPreviousVisitPEMR();
		}

		private void chkPEMR_CheckedChanged(object sender, EventArgs e)
		{
			ParentPEMRContainer.ShowPreviousVisitPEMR();
		}

		private void chkInvestigations_CheckedChanged(object sender, EventArgs e)
		{
			ParentPEMRContainer.ShowPreviousInvestigation();
		}

		private void chkLabs_CheckedChanged(object sender, EventArgs e)
		{

		}

		private void chkAttachments_CheckedChanged(object sender, EventArgs e)
		{
			ParentPEMRContainer.ShowPreviousAttachments();
		}

		private void lytGroup_MouseUp(object sender, MouseEventArgs e)
		{
			BaseLayoutItemHitInfo hitInfo = layoutControl1.CalcHitInfo(e.Location);
			if (hitInfo.HitType != LayoutItemHitTest.Item || e.Clicks != 1)
				return;
			LayoutGroup group = hitInfo.Item as LayoutGroup;
			if (group.ViewInfo.BorderInfo.CaptionBounds.Contains(e.Location))
			{
				group.Expanded = !group.Expanded;
				if (group.Expanded)
					Height = 292;
				else
					Height = 35;
			}
		}
	}
}
