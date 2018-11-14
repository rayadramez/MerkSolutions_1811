using System.Drawing;
using System.Windows.Forms;
using CommonUserControls.PEMRCommonViewers.PEMR_Interfaces;
using DevExpress.XtraLayout;
using DevExpress.XtraLayout.HitInfo;
using DevExpress.XtraLayout.Utils;

namespace CommonUserControls.PEMRCommonViewers.PEMR_InternalViewers
{
	public partial class PEMR_Symptoms_UC : UserControl, IPEMR_Viewer
	{
		public PEMR_Symptoms_UC()
		{
			InitializeComponent();

			txtReccommednations.EnterMoveNextControl = false;
		}

		public void Initialize()
		{

		}

		public void ClearControls(bool clearAll)
		{

		}

		public void FillControls()
		{

		}

		private void lytGroup_PainLevel_MouseUp(object sender, MouseEventArgs e)
		{
			BaseLayoutItemHitInfo hitInfo = layoutControl1.CalcHitInfo(e.Location);
			if (hitInfo.HitType != LayoutItemHitTest.Item || e.Clicks != 1)
				return;
			LayoutGroup group = hitInfo.Item as LayoutGroup;
			if (group != null)
				if (group.ViewInfo.BorderInfo.CaptionBounds.Contains(e.Location))
				{
					group.Expanded = !group.Expanded;
					if (group.Expanded)
					{
						group.MinSize = new Size(300, 200);
						group.MaxSize = new Size(300, 200);
						group.Size = new Size(300, 200);
						group.CaptionImage = Properties.Resources.Expanded_06;
					}
					else
						group.CaptionImage = Properties.Resources.NotExpanded_06;
				}
		}

		private void lytGroup_DoctorSymptomsCategories_MouseUp(object sender, MouseEventArgs e)
		{
			BaseLayoutItemHitInfo hitInfo = layoutControl1.CalcHitInfo(e.Location);
			if (hitInfo.HitType != LayoutItemHitTest.Item || e.Clicks != 1)
				return;
			LayoutGroup group = hitInfo.Item as LayoutGroup;
			if (group != null)
				if (group.ViewInfo.BorderInfo.CaptionBounds.Contains(e.Location))
				{
					group.Expanded = !group.Expanded;
					if (group.Expanded)
						group.CaptionImage = Properties.Resources.Expanded_06;
					else
						group.CaptionImage = Properties.Resources.NotExpanded_06;
				}
		}

		private void lytGroup_DoctorSymptoms_MouseUp(object sender, MouseEventArgs e)
		{
			BaseLayoutItemHitInfo hitInfo = layoutControl1.CalcHitInfo(e.Location);
			if (hitInfo.HitType != LayoutItemHitTest.Item || e.Clicks != 1)
				return;
			LayoutGroup group = hitInfo.Item as LayoutGroup;
			if (group != null)
				if (group.ViewInfo.BorderInfo.CaptionBounds.Contains(e.Location))
				{
					group.Expanded = !group.Expanded;
					if (group.Expanded)
						group.CaptionImage = Properties.Resources.Expanded_06;
					else
						group.CaptionImage = Properties.Resources.NotExpanded_06;
				}
		}

		private void chkPainDescription_CheckedChanged(object sender, System.EventArgs e)
		{
			lytPainLevelDescription.Visibility =
				chkPainDescription.Checked ? LayoutVisibility.Always : LayoutVisibility.Never;
		}
	}
}
