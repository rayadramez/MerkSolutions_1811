using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CommonUserControls.PEMRCommonViewers.PEMR_Interfaces;
using DevExpress.XtraEditors;
using DevExpress.XtraLayout;
using DevExpress.XtraLayout.HitInfo;
using DevExpress.XtraLayout.Utils;
using MerkDataBaseBusinessLogicProject;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.PEMRBusinessLogic;

namespace CommonUserControls.PEMRCommonViewers.PEMR_InternalViewers
{
	public partial class PEMR_SocialHistory_UC : UserControl, IPEMR_SocialHistory, IPEMR_Viewer
	{
		public VisitTiming_SocialHistory Active_VisitTiming_SocialHistory { get; set; }

		public PEMR_SocialHistory_UC()
		{
			InitializeComponent();
		}

		private void PEMR_SocialHistory_UC_Load(object sender, EventArgs e)
		{
			ClearControls(true);
			txtGeneralDescription.EnterMoveNextControl = false;
		}

		public void Initialize()
		{

		}

		public void ClearControls(bool clearAll)
		{
			NegativeSocialHistory = null;
			GeneralDescription = null;
			DidYouEverSmoke = null;
			NumberOfPacks = null;
			NumberOfYears = null;
			SmokeFurtherDetails = null;
			QuitingSmokeLessThan = null;
			QuitingSmokeBetween = null;
			QuitingSmokeMoreThan = null;
			QuitingSmokeFurtherDetails = null;
			DrinkAlcohol = null;
			HowMuchAlcohol = null;
			AlcoholFurtherDetails = null;
			HadProblemWithAlcohol = null;
			HadProblemWithAlcoholFurtherDetails = null;
			Addicted = null;
			AddictionFurtherDetails = null;
			HadProblemWithAddiction = null;
			HadProblemWithAddictionFurtherDetails = null;
			UseRecreationalDrugs = null;
			UseRecreationalDrugsFurtherDetails = null;
			WhenHadProblemWIthAlcohol = null;
		}

		public void FillControls()
		{

		}

		#region Controls Events

		#region CheckEdit Events

		#region Check Further Details

		private void txtGeneralDescription_EditValueChanged(object sender, EventArgs e)
		{
		}

		private void chkSmoke_DetailsButton_CheckedChanged(object sender, EventArgs e)
		{
			lytSmoke_Details.Visibility =
				chkSmoke_DetailsButton.Checked ? LayoutVisibility.Always : LayoutVisibility.Never;
		}

		private void btnQuitSmoke_DetailsButton_CheckedChanged(object sender, EventArgs e)
		{
			lytQuitSmoking_Details.Visibility = btnQuitSmoke_DetailsButton.Checked ? LayoutVisibility.Always : LayoutVisibility.Never;
		}

		private void chkAlcohol_DetailsButton_CheckedChanged(object sender, EventArgs e)
		{
			lytQuitAlcohol_Details.Visibility = chkAlcohol_DetailsButton.Checked ? LayoutVisibility.Always : LayoutVisibility.Never;
		}

		private void chkProblemAlcohol_DetailsButton_CheckedChanged(object sender, EventArgs e)
		{
			lytProblemAlcohol_Details.Visibility = chkProblemAlcohol_DetailsButton.Checked ? LayoutVisibility.Always : LayoutVisibility.Never;
		}

		private void chkAddiction_DetailsButton_CheckedChanged(object sender, EventArgs e)
		{
			lytAddiction_Details.Visibility = chkAddiction_DetailsButton.Checked ? LayoutVisibility.Always : LayoutVisibility.Never;
		}

		private void btnProblemAddiction_DetailsButton_CheckedChanged(object sender, EventArgs e)
		{
			lytProblemAddiction_Details.Visibility = btnProblemAddiction_DetailsButton.Checked ? LayoutVisibility.Always : LayoutVisibility.Never;
		}

		private void chkDrugs_Details_CheckedChanged(object sender, EventArgs e)
		{
			lytDrugs_Details.Visibility = chkDrugs_Details.Checked ? LayoutVisibility.Always : LayoutVisibility.Never;
		}

		#endregion

		#region Check Yes

		private void chkSmoke_Yes_CheckedChanged(object sender, EventArgs e)
		{
			lytSmoke_NumberOfPacks.Visibility = DidYouEverSmoke != null && Convert.ToBoolean(DidYouEverSmoke)
				? LayoutVisibility.Always
				: LayoutVisibility.Never;
			lytSmoke_NumberOfYears.Visibility = DidYouEverSmoke != null && Convert.ToBoolean(DidYouEverSmoke)
				? LayoutVisibility.Always
				: LayoutVisibility.Never;
			lytSmoke_DetailsButton.Visibility = DidYouEverSmoke != null && Convert.ToBoolean(DidYouEverSmoke)
				? LayoutVisibility.Always
				: LayoutVisibility.Never;
			lytContainer_QuitSmoke.Visibility = DidYouEverSmoke != null && Convert.ToBoolean(DidYouEverSmoke)
				? LayoutVisibility.Always
				: LayoutVisibility.Never;
			lytContainer_Smoke.CaptionImage = DidYouEverSmoke != null && Convert.ToBoolean(DidYouEverSmoke)
				? Properties.Resources.AcceptIcon_16_02
				: Properties.Resources.RoundedPoint_6_01;
			NumberOfPacks = null;
			NumberOfYears = null;
		}

		private void chkAlcohol_Yes_CheckedChanged(object sender, EventArgs e)
		{
			lytAlcohol_HowMuch.Visibility = DrinkAlcohol != null && Convert.ToBoolean(DrinkAlcohol)
				? LayoutVisibility.Always
				: LayoutVisibility.Never;
			lytAlcohol_DetailsButton.Visibility = DrinkAlcohol != null && Convert.ToBoolean(DrinkAlcohol)
				? LayoutVisibility.Always
				: LayoutVisibility.Never;
			lytContainer_ProblemAlcohol.Visibility = DrinkAlcohol != null && Convert.ToBoolean(DrinkAlcohol)
				? LayoutVisibility.Always
				: LayoutVisibility.Never;
			lytContainer_Alcohol.CaptionImage = DrinkAlcohol != null && Convert.ToBoolean(DrinkAlcohol)
				? Properties.Resources.AcceptIcon_16_02
				: Properties.Resources.RoundedPoint_6_01;
		}

		private void chkProblemAlcohol_Yes_CheckedChanged(object sender, EventArgs e)
		{
			lytProblemAlcohol_When.Visibility =
				HadProblemWithAlcohol != null && Convert.ToBoolean(HadProblemWithAlcohol)
					? LayoutVisibility.Always
					: LayoutVisibility.Never;
			lytProblemAlcohol_DetailsButton.Visibility =
				HadProblemWithAlcohol != null && Convert.ToBoolean(HadProblemWithAlcohol)
					? LayoutVisibility.Always
					: LayoutVisibility.Never;
			lytContainer_ProblemAlcohol.CaptionImage =
				HadProblemWithAlcohol != null && Convert.ToBoolean(HadProblemWithAlcohol)
					? Properties.Resources.AcceptIcon_16_02
					: Properties.Resources.RoundedPoint_6_01;
		}

		private void chkAddiction_Yes_CheckedChanged(object sender, EventArgs e)
		{
			lytAddiction_DetailsButton.Visibility = Addicted != null && Convert.ToBoolean(Addicted)
				? LayoutVisibility.Always
				: LayoutVisibility.Never;
			lytContainer_AddictionProblem.Visibility = Addicted != null && Convert.ToBoolean(Addicted)
				? LayoutVisibility.Always
				: LayoutVisibility.Never;
			lytContainer_Addiction.CaptionImage = Addicted != null && Convert.ToBoolean(Addicted)
				? Properties.Resources.AcceptIcon_16_02
				: Properties.Resources.RoundedPoint_6_01;
		}

		private void chkProblemAddiction_Yes_CheckedChanged(object sender, EventArgs e)
		{
			lytProblemAddiction_DetailsButton.Visibility = HadProblemWithAddiction != null && Convert.ToBoolean(HadProblemWithAddiction)
				? LayoutVisibility.Always
				: LayoutVisibility.Never;
			lytContainer_AddictionProblem.CaptionImage = HadProblemWithAddiction != null && Convert.ToBoolean(HadProblemWithAddiction)
				? Properties.Resources.AcceptIcon_16_02
				: Properties.Resources.RoundedPoint_6_01;
		}

		private void chkDrugs_Yes_CheckedChanged(object sender, EventArgs e)
		{
			lytDrugs_DetailsButtons.Visibility = UseRecreationalDrugs != null && Convert.ToBoolean(UseRecreationalDrugs)
				? LayoutVisibility.Always
				: LayoutVisibility.Never;
			lytContainer_RecreationalDrugs.CaptionImage = UseRecreationalDrugs != null && Convert.ToBoolean(UseRecreationalDrugs)
				? Properties.Resources.AcceptIcon_16_02
				: Properties.Resources.RoundedPoint_6_01;
		}

		#endregion

		#region Check No

		private void chkSmoke_No_CheckedChanged(object sender, EventArgs e)
		{
			lytSmoke_NumberOfPacks.Visibility = DidYouEverSmoke != null && Convert.ToBoolean(DidYouEverSmoke)
				? LayoutVisibility.Always
				: LayoutVisibility.Never;
			lytSmoke_NumberOfYears.Visibility = DidYouEverSmoke != null && Convert.ToBoolean(DidYouEverSmoke)
				? LayoutVisibility.Always
				: LayoutVisibility.Never;
			lytSmoke_DetailsButton.Visibility = DidYouEverSmoke != null && Convert.ToBoolean(DidYouEverSmoke)
				? LayoutVisibility.Always
				: LayoutVisibility.Never;
			lytContainer_QuitSmoke.Visibility = DidYouEverSmoke != null && Convert.ToBoolean(DidYouEverSmoke)
				? LayoutVisibility.Always
				: LayoutVisibility.Never;
			chkSmoke_DetailsButton.Checked = false;
			btnQuitSmoke_DetailsButton.Checked = false;
			spnNumberOfDailyPacks.EditValue = null;
			spnNumberOfYearsSmoke.EditValue = null;
			lytContainer_Smoke.CaptionImage = DidYouEverSmoke != null && !Convert.ToBoolean(DidYouEverSmoke)
				? Properties.Resources.RejectIcon_16_01
				: Properties.Resources.RoundedPoint_6_01;
		}

		private void chkAlcohol_No_CheckedChanged(object sender, EventArgs e)
		{
			lytAlcohol_HowMuch.Visibility = DrinkAlcohol != null && Convert.ToBoolean(DrinkAlcohol)
				? LayoutVisibility.Always
				: LayoutVisibility.Never;
			lytAlcohol_DetailsButton.Visibility = DrinkAlcohol != null && Convert.ToBoolean(DrinkAlcohol)
				? LayoutVisibility.Always
				: LayoutVisibility.Never;
			lytContainer_ProblemAlcohol.Visibility = DrinkAlcohol != null && Convert.ToBoolean(DrinkAlcohol)
				? LayoutVisibility.Always
				: LayoutVisibility.Never;
			lytContainer_Alcohol.CaptionImage = DrinkAlcohol != null && !Convert.ToBoolean(DrinkAlcohol)
				? Properties.Resources.RejectIcon_16_01
				: Properties.Resources.RoundedPoint_6_01;
			chkAlcohol_DetailsButton.Checked = false;
		}

		private void chkProblemAlcohol_No_CheckedChanged(object sender, EventArgs e)
		{
			lytProblemAlcohol_When.Visibility = HadProblemWithAlcohol != null && Convert.ToBoolean(HadProblemWithAlcohol)
				? LayoutVisibility.Always
				: LayoutVisibility.Never;
			lytProblemAlcohol_DetailsButton.Visibility = HadProblemWithAlcohol != null && Convert.ToBoolean(HadProblemWithAlcohol)
				? LayoutVisibility.Always
				: LayoutVisibility.Never;
			lytContainer_ProblemAlcohol.CaptionImage = HadProblemWithAlcohol != null && !Convert.ToBoolean(HadProblemWithAlcohol)
				? Properties.Resources.RejectIcon_16_01
				: Properties.Resources.RoundedPoint_6_01;
			chkProblemAlcohol_DetailsButton.Checked = false;
		}

		private void chkAddiction_No_CheckedChanged(object sender, EventArgs e)
		{
			lytAddiction_DetailsButton.Visibility = Addicted != null && Convert.ToBoolean(Addicted)
				? LayoutVisibility.Always
				: LayoutVisibility.Never;
			lytContainer_AddictionProblem.Visibility = Addicted != null && Convert.ToBoolean(Addicted)
				? LayoutVisibility.Always
				: LayoutVisibility.Never;
			lytContainer_Addiction.CaptionImage = Addicted != null && !Convert.ToBoolean(Addicted)
				? Properties.Resources.RejectIcon_16_01
				: Properties.Resources.RoundedPoint_6_01;
			chkAddiction_DetailsButton.Checked = false;
		}

		private void chkProblemAddiction_No_CheckedChanged(object sender, EventArgs e)
		{
			lytProblemAddiction_DetailsButton.Visibility = HadProblemWithAddiction != null && Convert.ToBoolean(HadProblemWithAddiction)
				? LayoutVisibility.Always
				: LayoutVisibility.Never;
			lytContainer_AddictionProblem.CaptionImage = HadProblemWithAddiction != null && Convert.ToBoolean(HadProblemWithAddiction)
				? Properties.Resources.RejectIcon_16_01
				: Properties.Resources.RoundedPoint_6_01;
			btnProblemAddiction_DetailsButton.Checked = false;
		}

		private void chkDrugs_No_CheckedChanged(object sender, EventArgs e)
		{
			lytDrugs_DetailsButtons.Visibility = UseRecreationalDrugs != null && Convert.ToBoolean(UseRecreationalDrugs)
				? LayoutVisibility.Always
				: LayoutVisibility.Never;
			lytContainer_RecreationalDrugs.CaptionImage = UseRecreationalDrugs != null && !Convert.ToBoolean(UseRecreationalDrugs)
				? Properties.Resources.RejectIcon_16_01
				: Properties.Resources.RoundedPoint_6_01;
			chkDrugs_Details.Checked = false;
		}

		#endregion

		#region Check Other
		private void chkNoSocialHistory_CheckedChanged(object sender, EventArgs e)
		{
			lytContainer_Smoke.Enabled = lytContainer_Alcohol.Enabled = lytContainer_Addiction.Enabled =
				lytContainer_RecreationalDrugs.Enabled = !chkNoSocialHistory.Checked;

			lytContainer_Smoke.Expanded = lytContainer_Alcohol.Expanded = lytContainer_Addiction.Expanded =
				lytContainer_RecreationalDrugs.Expanded = false;

			DidYouEverSmoke = null;
			NumberOfPacks = null;
			NumberOfYears = null;
			SmokeFurtherDetails = null;
			QuitingSmokeLessThan = null;
			QuitingSmokeBetween = null;
			QuitingSmokeMoreThan = null;
			QuitingSmokeFurtherDetails = null;
			DrinkAlcohol = null;
			HowMuchAlcohol = null;
			AlcoholFurtherDetails = null;
			HadProblemWithAlcohol = null;
			WhenHadProblemWIthAlcohol = null;
			HadProblemWithAlcoholFurtherDetails = null;
			Addicted = null;
			AddictionFurtherDetails = null;
			HadProblemWithAddiction = null;
			HadProblemWithAddictionFurtherDetails = null;
			UseRecreationalDrugs = null;
			UseRecreationalDrugsFurtherDetails = null;

			lytContainer_Smoke.CaptionImage = Properties.Resources.RoundedPoint_6_01;
			lytContainer_Alcohol.CaptionImage = Properties.Resources.RoundedPoint_6_01;
			lytContainer_ProblemAlcohol.CaptionImage = Properties.Resources.RoundedPoint_6_01;
			lytContainer_Addiction.CaptionImage = Properties.Resources.RoundedPoint_6_01;
			lytContainer_AddictionProblem.CaptionImage = Properties.Resources.RoundedPoint_6_01;
			lytContainer_RecreationalDrugs.CaptionImage = Properties.Resources.RoundedPoint_6_01;
		}

		private void chkQuitSmoke_LessThan_CheckedChanged(object sender, EventArgs e)
		{
		}

		private void chkQuitSmoke_Bet_CheckedChanged(object sender, EventArgs e)
		{
		}

		private void chkQuitSmoke_MoreThan_CheckedChanged(object sender, EventArgs e)
		{
		}
		#endregion

		#endregion

		#region TextEdit Events

		private void txtSmoke_Details_EditValueChanged(object sender, EventArgs e)
		{
		}

		private void txtQuitSmoke_Details_EditValueChanged(object sender, EventArgs e)
		{
		}

		private void txtAlcohol_Details_EditValueChanged(object sender, EventArgs e)
		{
		}

		private void txtProblemAlcohol_Details_EditValueChanged(object sender, EventArgs e)
		{
		}

		private void txtAddiction_Details_EditValueChanged(object sender, EventArgs e)
		{
		}

		private void txtProblemAddiction_Details_EditValueChanged(object sender, EventArgs e)
		{
		}

		private void txtDrugs_Details_EditValueChanged(object sender, EventArgs e)
		{
		}

		#endregion

		#region SpinEdit Events

		private void spnNumberOfYearsSmoke_EditValueChanged(object sender, EventArgs e)
		{
		}

		private void spnNumberOfDailyPacks_EditValueChanged(object sender, EventArgs e)
		{
		}

		private void spnAlcohol_HowMuch_EditValueChanged(object sender, EventArgs e)
		{
		}

		private void spnProblemAlcohol_When_EditValueChanged(object sender, EventArgs e)
		{
		}

		#endregion

		#region Layout Events

		private void lytContainer_Smoke_MouseUp(object sender, MouseEventArgs e)
		{
			BaseLayoutItemHitInfo hitInfo = layoutControl1.CalcHitInfo(e.Location);
			if (hitInfo.HitType != LayoutItemHitTest.Item || e.Clicks != 1)
				return;
			LayoutGroup group = hitInfo.Item as LayoutGroup;
			if (group != null)
			{
				if (group.ViewInfo.BorderInfo.CaptionBounds.Contains(e.Location))
				{
					group.Expanded = !group.Expanded;
					if (lytContainer_Alcohol.Expanded)
						lytContainer_Alcohol.Expanded = false;
					if (lytContainer_Addiction.Expanded)
						lytContainer_Addiction.Expanded = false;
					if (lytContainer_RecreationalDrugs.Expanded)
						lytContainer_RecreationalDrugs.Expanded = false;
				}
			}
		}

		private void lytContainer_Alcohol_MouseUp(object sender, MouseEventArgs e)
		{
			BaseLayoutItemHitInfo hitInfo = layoutControl1.CalcHitInfo(e.Location);
			if (hitInfo.HitType != LayoutItemHitTest.Item || e.Clicks != 1)
				return;
			LayoutGroup group = hitInfo.Item as LayoutGroup;
			if (group != null)
			{
				if (group.ViewInfo.BorderInfo.CaptionBounds.Contains(e.Location))
				{
					group.Expanded = !group.Expanded;
					if (lytContainer_Smoke.Expanded)
						lytContainer_Smoke.Expanded = false;
					if (lytContainer_Addiction.Expanded)
						lytContainer_Addiction.Expanded = false;
					if (lytContainer_RecreationalDrugs.Expanded)
						lytContainer_RecreationalDrugs.Expanded = false;
				}
			}
		}

		private void lytContainer_Addiction_MouseUp(object sender, MouseEventArgs e)
		{
			BaseLayoutItemHitInfo hitInfo = layoutControl1.CalcHitInfo(e.Location);
			if (hitInfo.HitType != LayoutItemHitTest.Item || e.Clicks != 1)
				return;
			LayoutGroup group = hitInfo.Item as LayoutGroup;
			if (group != null)
			{
				if (group.ViewInfo.BorderInfo.CaptionBounds.Contains(e.Location))
				{
					group.Expanded = !group.Expanded;
					if (lytContainer_Smoke.Expanded)
						lytContainer_Smoke.Expanded = false;
					if (lytContainer_Alcohol.Expanded)
						lytContainer_Alcohol.Expanded = false;
					if (lytContainer_RecreationalDrugs.Expanded)
						lytContainer_RecreationalDrugs.Expanded = false;
				}
			}
		}

		private void layoutControlGroup2_MouseUp(object sender, MouseEventArgs e)
		{
			BaseLayoutItemHitInfo hitInfo = layoutControl1.CalcHitInfo(e.Location);
			if (hitInfo.HitType != LayoutItemHitTest.Item || e.Clicks != 1)
				return;
			LayoutGroup group = hitInfo.Item as LayoutGroup;
			if (group != null)
			{
				if (group.ViewInfo.BorderInfo.CaptionBounds.Contains(e.Location))
				{
					group.Expanded = !group.Expanded;
					if (lytContainer_Smoke.Expanded)
						lytContainer_Smoke.Expanded = false;
					if (lytContainer_Alcohol.Expanded)
						lytContainer_Alcohol.Expanded = false;
					if (lytContainer_Addiction.Expanded)
						lytContainer_Addiction.Expanded = false;
				}
			}
		}

		private void lytContainer_QuitSmoke_MouseUp(object sender, MouseEventArgs e)
		{
			BaseLayoutItemHitInfo hitInfo = layoutControl1.CalcHitInfo(e.Location);
			if (hitInfo.HitType != LayoutItemHitTest.Item || e.Clicks != 1)
				return;
			LayoutGroup group = hitInfo.Item as LayoutGroup;
			if (group != null)
			{
				if (group.ViewInfo.BorderInfo.CaptionBounds.Contains(e.Location))
					group.Expanded = !group.Expanded;
			}
		}

		private void lytContainer_ProblemAlcohol_MouseUp(object sender, MouseEventArgs e)
		{
			BaseLayoutItemHitInfo hitInfo = layoutControl1.CalcHitInfo(e.Location);
			if (hitInfo.HitType != LayoutItemHitTest.Item || e.Clicks != 1)
				return;
			LayoutGroup group = hitInfo.Item as LayoutGroup;
			if (group != null)
			{
				if (group.ViewInfo.BorderInfo.CaptionBounds.Contains(e.Location))
					group.Expanded = !group.Expanded;
			}
		}

		private void lytContainer_AddictionProblem_MouseUp(object sender, MouseEventArgs e)
		{
			BaseLayoutItemHitInfo hitInfo = layoutControl1.CalcHitInfo(e.Location);
			if (hitInfo.HitType != LayoutItemHitTest.Item || e.Clicks != 1)
				return;
			LayoutGroup group = hitInfo.Item as LayoutGroup;
			if (group != null)
			{
				if (group.ViewInfo.BorderInfo.CaptionBounds.Contains(e.Location))
					group.Expanded = !group.Expanded;
			}
		}

		#endregion

		#region Button Events

		private void btnSave_Click(object sender, EventArgs e)
		{
			if (PEMRBusinessLogic.ActivePEMRObject != null)
				if (PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_SocialHistory == null)
				{
					Active_VisitTiming_SocialHistory =
						PEMRBusinessLogic.CreateNew_VisitTiming_SocialHistory(this, DB_PEMRSavingMode.SaveImmediately);
					if (Active_VisitTiming_SocialHistory == null)
						return;
					if (PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_SocialHistory == null)
						PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_SocialHistory = new List<VisitTiming_SocialHistory>();
					PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_SocialHistory.Add(Active_VisitTiming_SocialHistory);
					XtraMessageBox.Show("Saved Successfully", "Saved", MessageBoxButtons.OK,
						MessageBoxIcon.Information);
				}
				else
				{
					if (Active_VisitTiming_SocialHistory == null)
						return;
					if(PEMRBusinessLogic.Update_VisitTiming_SocialHistory(this, Active_VisitTiming_SocialHistory))
						XtraMessageBox.Show("Saved Successfully", "Saved", MessageBoxButtons.OK,
							MessageBoxIcon.Information);
				}
		}

		#endregion

		#endregion

		#region Implementation of IPEMR_SocialHistory

		public object NegativeSocialHistory
		{
			get
			{
				if (chkNoSocialHistory.Checked)
					return chkNoSocialHistory.Checked;
				return null;
			}
			set { chkNoSocialHistory.Checked = Convert.ToBoolean(value); }
		}

		public object GeneralDescription
		{
			get { return txtGeneralDescription.EditValue; }
			set { txtGeneralDescription.EditValue = value; }
		}

		public object DidYouEverSmoke
		{
			get
			{
				if (chkSmoke_Yes.Checked)
					return true;
				if (chkSmoke_No.Checked)
					return false;
				return null;
			}
			set
			{
				if (value == null)
				{
					chkSmoke_Yes.Checked = false;
					chkSmoke_No.Checked = false;
					return;
				}

				if (Convert.ToBoolean(value))
					chkSmoke_Yes.Checked = true;
				else
					chkSmoke_No.Checked = true;
			}
		}

		public object NumberOfPacks
		{
			get { return spnNumberOfDailyPacks.EditValue; }
			set { spnNumberOfDailyPacks.EditValue = value; }
		}

		public object NumberOfYears
		{
			get { return spnNumberOfYearsSmoke.EditValue; }
			set { spnNumberOfYearsSmoke.EditValue = value; }
		}

		public object SmokeFurtherDetails
		{
			get { return txtSmoke_Details.EditValue; }
			set { txtSmoke_Details.EditValue = value; }
		}

		public object QuitingSmokeLessThan
		{
			get
			{
				if (chkQuitSmoke_LessThan.Checked)
					return chkQuitSmoke_LessThan.Checked;
				return null;
			}
			set { chkQuitSmoke_LessThan.Checked = Convert.ToBoolean(value); }
		}

		public object QuitingSmokeBetween
		{
			get
			{
				if (chkQuitSmoke_Bet.Checked)
					return chkQuitSmoke_Bet.Checked;
				return null;
			}
			set { chkQuitSmoke_Bet.Checked = Convert.ToBoolean(value); }
		}

		public object QuitingSmokeMoreThan
		{
			get
			{
				if (chkQuitSmoke_MoreThan.Checked)
					return chkQuitSmoke_MoreThan.Checked;
				return null;
			}
			set { chkQuitSmoke_MoreThan.Checked = Convert.ToBoolean(value); }
		}

		public object QuitingSmokeFurtherDetails
		{
			get { return txtQuitSmoke_Details.EditValue; }
			set { txtQuitSmoke_Details.EditValue = value; }
		}

		public object DrinkAlcohol
		{
			get
			{
				if (chkAlcohol_Yes.Checked)
					return true;
				if (chkAlcohol_No.Checked)
					return false;
				return null;
			}
			set
			{
				if (value == null)
				{
					chkAlcohol_Yes.Checked = false;
					chkAlcohol_No.Checked = false;
					return;
				}

				if (Convert.ToBoolean(value))
					chkAlcohol_Yes.Checked = true;
				else
					chkAlcohol_No.Checked = true;
			}
		}

		public object HowMuchAlcohol
		{
			get { return spnAlcohol_HowMuch.EditValue; }
			set { spnAlcohol_HowMuch.EditValue = value; }
		}

		public object AlcoholFurtherDetails
		{
			get { return txtAlcohol_Details.EditValue; }
			set { txtAlcohol_Details.EditValue = value; }
		}

		public object HadProblemWithAlcohol
		{
			get
			{
				if (chkProblemAlcohol_Yes.Checked)
					return true;
				if (chkProblemAlcohol_No.Checked)
					return false;
				return null;
			}
			set
			{
				if (value == null)
				{
					chkProblemAlcohol_Yes.Checked = false;
					chkProblemAlcohol_No.Checked = false;
					return;
				}

				if (Convert.ToBoolean(value))
					chkProblemAlcohol_Yes.Checked = true;
				else
					chkProblemAlcohol_No.Checked = true;
			}
		}

		public object WhenHadProblemWIthAlcohol
		{
			get { return spnProblemAlcohol_When.EditValue; }
			set { spnProblemAlcohol_When.EditValue = value; }
		}

		public object HadProblemWithAlcoholFurtherDetails
		{
			get { return txtProblemAlcohol_Details.EditValue; }
			set { txtProblemAlcohol_Details.EditValue = value; }
		}

		public object Addicted
		{
			get
			{
				if (chkAddiction_Yes.Checked)
					return true;
				if (chkAddiction_No.Checked)
					return false;
				return null;
			}
			set
			{
				if (value == null)
				{
					chkAddiction_Yes.Checked = false;
					chkAddiction_No.Checked = false;
					return;
				}

				if (Convert.ToBoolean(value))
					chkAddiction_Yes.Checked = true;
				else
					chkAddiction_No.Checked = true;
			}
		}

		public object AddictionFurtherDetails
		{
			get { return txtAddiction_Details.EditValue; }
			set { txtAddiction_Details.EditValue = value; }
		}

		public object HadProblemWithAddiction
		{
			get
			{
				if (chkProblemAddiction_Yes.Checked)
					return true;
				if (chkProblemAddiction_No.Checked)
					return false;
				return null;
			}
			set
			{
				if (value == null)
				{
					chkProblemAddiction_Yes.Checked = false;
					chkProblemAddiction_No.Checked = false;
					return;
				}

				if (Convert.ToBoolean(value))
					chkProblemAddiction_Yes.Checked = true;
				else
					chkProblemAddiction_No.Checked = true;
			}
		}

		public object HadProblemWithAddictionFurtherDetails
		{
			get { return txtProblemAddiction_Details.EditValue; }
			set { txtProblemAddiction_Details.EditValue = value; }
		}

		public object UseRecreationalDrugs
		{
			get
			{
				if (chkDrugs_Yes.Checked)
					return true;
				if (chkDrugs_No.Checked)
					return false;
				return null;
			}
			set
			{
				if (value == null)
				{
					chkDrugs_Yes.Checked = false;
					chkDrugs_No.Checked = false;
					return;
				}

				if (Convert.ToBoolean(value))
					chkDrugs_Yes.Checked = true;
				else
					chkDrugs_No.Checked = true;
			}
		}

		public object UseRecreationalDrugsFurtherDetails
		{
			get { return txtDrugs_Details.EditValue; }
			set { txtDrugs_Details.EditValue = value; }
		}

		#endregion
	}
}
