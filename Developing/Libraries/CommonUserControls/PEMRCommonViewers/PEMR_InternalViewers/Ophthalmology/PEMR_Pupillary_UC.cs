using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CommonControlLibrary;
using CommonUserControls.PEMRCommonViewers.PEMR_Interfaces;
using DevExpress.XtraEditors;
using DevExpress.XtraLayout;
using DevExpress.XtraLayout.HitInfo;
using DevExpress.XtraLayout.Utils;
using MerkDataBaseBusinessLogicProject;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.PEMRBusinessLogic;

namespace CommonUserControls.PEMRCommonViewers.PEMR_InternalViewers.Ophthalmology
{
	public partial class PEMR_Pupillary_UC : UserControl, IPEMR_Viewer, IPEMR_Pupillary
	{
		public VisitTiming_Pupillary Active_VisitTiming_Pupillary { get; set; }

		public PEMR_Pupillary_UC()
		{
			InitializeComponent();

			CommonViewsActions.Decorate(lkeAbnormalitiesCauses_OD, lkeAbnormalitiesCauses_OS, lkeRAPDCauses_OD,
				lkeRAPDCauses_OS, spnScotopic_OD, spnScotopic_OS, spnHighPhotopic_OD, spnHighPhotopic_OS,
				spnLowPhotopic_OD, spnLowPhotopic_OS, spnHighMesopic_OD, spnHighMesopic_OS, spnLowMesopic_OD,
				spnLowMesopic_OS);
		}

		public void Initialize()
		{
			ClearControls(true);
			FillControls();

			PEMRBusinessLogic.PEMR_Pupillary = this;
		}

		#region Implementation of IPEMR_Viewer

		public void ClearControls(bool clearAll)
		{
			if(clearAll)
			{
				chkNoAbnormalitiesFound_OD.Checked = false;
				chkNoAbnormalitiesFound_OS.Checked = false;
			}

			PupillaryAbnormalities_CU_ID_OD = null;
			PupillaryAbnormalities_CU_ID_OS = null;
			IsRAPD_OD = null;
			IsRAPD_OS = null;
			PupillaryRAPDGradingScale_P_ID_OD = null;
			PupillaryRAPDGradingScale_P_ID_OS = null;
			PupillaryRAPDCauses_CU_ID_OD = null;
			PupillaryRAPDCauses_CU_ID_OS = null;
			PupillarySize_P_ID_OD = null;
			PupillarySize_P_ID_OS = null;
			PupillaryShape_P_OD = null;
			PupillaryShape_P_OS = null;
			Scotopic_OD = null;
			Scotopic_OS = null;
			HighPhotopic_OD = null;
			HighPhotopic_OS = null;
			LowPhotopic_OD = null;
			LowPhotopic_OS = null;
			HighMesopic_OD = null;
			HighMesopic_OS = null;
			LowMesopic_OD = null;
			LowMesopic_OS = null;
			FurtherDetails_OD = null;
			FurtherDetails_OS = null;
		}

		public void FillControls()
		{
			CommonViewsActions.FillGridlookupEdit(lkeAbnormalitiesCauses_OD, PupillaryAbnormalities_cu.ItemsList);
			CommonViewsActions.FillGridlookupEdit(lkeAbnormalitiesCauses_OS, PupillaryAbnormalities_cu.ItemsList);
			CommonViewsActions.FillGridlookupEdit(lkeRAPDCauses_OD, PupillaryRAPDCauses_cu.ItemsList);
			CommonViewsActions.FillGridlookupEdit(lkeRAPDCauses_OS, PupillaryRAPDCauses_cu.ItemsList);
		}

		#endregion

		public void Clear_OD()
		{
			lkeAbnormalitiesCauses_OD.EditValue = null;
			chkPositiveRAPD_OD.Checked = false;
			chkNegativeRAPD_OD.Checked = false;
			chkGrade1_OD.Checked = false;
			chkGrade2_OD.Checked = false;
			chkGrade3_OD.Checked = false;
			chkGrade4_OD.Checked = false;
			chkGrade5_OD.Checked = false;
			lkeRAPDCauses_OD.EditValue = null;
			chkSize_Miosis_OD.Checked = false;
			chkSize_Mediuim_OD.Checked = false;
			chkSize_Mydrisasis_OD.Checked = false;
			chkShape_Rounded_OD.Checked = false;
			chkShape_Oval_OD.Checked = false;
			chkShape_Sector_OD.Checked = false;
			chkShape_Irruglar_OD.Checked = false;
			chkShape_Peaked_OD.Checked = false;
			spnScotopic_OD.EditValue = null;
			spnHighPhotopic_OD.EditValue = null;
			spnLowPhotopic_OD.EditValue = null;
			spnHighMesopic_OD.EditValue = null;
			spnLowMesopic_OD.EditValue = null;
		}

		public void Clear_OS()
		{
			lkeAbnormalitiesCauses_OS.EditValue = null;
			chkPositiveRAPD_OS.Checked = false;
			chkNegativeRAPD_OS.Checked = false;
			chkGrade1_OS.Checked = false;
			chkGrade2_OS.Checked = false;
			chkGrade3_OS.Checked = false;
			chkGrade4_OS.Checked = false;
			chkGrade5_OS.Checked = false;
			lkeRAPDCauses_OS.EditValue = null;
			chkSize_Miosis_OS.Checked = false;
			chkSize_Mediuim_OS.Checked = false;
			chkSize_Mydrisasis_OS.Checked = false;
			chkShape_Rounded_OS.Checked = false;
			chkShape_Oval_OS.Checked = false;
			chkShape_Sector_OS.Checked = false;
			chkShape_Irruglar_OS.Checked = false;
			chkShape_Peaked_OS.Checked = false;
			spnScotopic_OS.EditValue = null;
			spnHighPhotopic_OS.EditValue = null;
			spnLowPhotopic_OS.EditValue = null;
			spnHighMesopic_OS.EditValue = null;
			spnLowMesopic_OS.EditValue = null;
		}

		#region Controls Events

		#region CheckEdit Events

		private void chkNoAbnormalitiesFound_OD_CheckedChanged(object sender, EventArgs e)
		{
			lytAbnormalitiesCasuses_OD.Enabled = lytGroup_RAPD_OD.Enabled = lytGroup_Size_OD.Enabled =
				lytGroup_Shape_OD.Enabled = lytGroup_Pupillometry_OD.Enabled = !chkNoAbnormalitiesFound_OD.Checked;
			Clear_OD();
		}

		private void chkNoAbnormalitiesFound_OS_CheckedChanged(object sender, EventArgs e)
		{
			lytAbnormalitiesCasuses_OS.Enabled = lytGroup_RAPD_OS.Enabled = lytGroup_Size_OS.Enabled =
				lytGroup_Shape_OS.Enabled = lytGroup_Pupillometry_OS.Enabled = !chkNoAbnormalitiesFound_OS.Checked;
			Clear_OS();
		}

		private void chkNegativeRAPD_OD_CheckedChanged(object sender, EventArgs e)
		{
			lytGroup_RAPDGradingScale_OD.Visibility = lytRAPDCauses_OD.Visibility = emptySpaceItem11.Visibility = 
				!chkNegativeRAPD_OD.Checked ? LayoutVisibility.Always : LayoutVisibility.Never;
			if (chkNegativeRAPD_OD.Checked)
			{
				PupillaryRAPDGradingScale_P_ID_OD = null;
				PupillaryRAPDCauses_CU_ID_OD = null;
			}
		}

		private void chkPositiveRAPD_OD_CheckedChanged(object sender, EventArgs e)
		{
			lytGroup_RAPDGradingScale_OD.Visibility = lytRAPDCauses_OD.Visibility = emptySpaceItem12.Visibility = 
				chkPositiveRAPD_OD.Checked ? LayoutVisibility.Always : LayoutVisibility.Never;
			if (chkNegativeRAPD_OS.Checked)
			{
				PupillaryRAPDGradingScale_P_ID_OS = null;
				PupillaryRAPDCauses_CU_ID_OS = null;
			}
		}

		private void chkNegativeRAPD_OS_CheckedChanged(object sender, EventArgs e)
		{
			lytGroup_RAPDGradingScale_OS.Visibility = lytRAPDCauses_OS.Visibility =
				!chkNegativeRAPD_OS.Checked ? LayoutVisibility.Always : LayoutVisibility.Never;
		}

		private void chkPositiveRAPD_OS_CheckedChanged(object sender, EventArgs e)
		{
			lytGroup_RAPDGradingScale_OS.Visibility = lytRAPDCauses_OS.Visibility =
				chkPositiveRAPD_OS.Checked ? LayoutVisibility.Always : LayoutVisibility.Never;
		}

		#endregion

		#region Button Events

		private void btnSave_Click(object sender, EventArgs e)
		{
			if(PEMRBusinessLogic.ActivePEMRObject != null )
				if (PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_Pupillary == null ||
				    PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_Pupillary.Count == 0)
				{
					Active_VisitTiming_Pupillary = PEMRBusinessLogic.CreateNew_VisitTiming_Pupillary(this, DB_PEMRSavingMode.SaveImmediately);
					if (Active_VisitTiming_Pupillary == null)
						return;
					if(PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_Pupillary == null)
						PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_Pupillary = new List<VisitTiming_Pupillary>();
					PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_Pupillary.Add(Active_VisitTiming_Pupillary);
					XtraMessageBox.Show("Saved Successfully", "Saved", MessageBoxButtons.OK,
						MessageBoxIcon.Information);
				}
				else
				{
					if (Active_VisitTiming_Pupillary == null)
						return;
					if (PEMRBusinessLogic.Update_VisitTiming_Pupillary(this, Active_VisitTiming_Pupillary))
						XtraMessageBox.Show("Saved Successfully", "Saved", MessageBoxButtons.OK,
							MessageBoxIcon.Information);
				}
		}

		#endregion

		#region Layout Events

		private void lytGroup_RAPD_OD_MouseUp(object sender, MouseEventArgs e)
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
					lytGroup_RAPD_OS.Expanded = group.Expanded;
					if (group.Expanded)
					{
						if (lytGroup_Size_OD.Expanded)
							lytGroup_Size_OD.Expanded = false;
						if (lytGroup_Size_OS.Expanded)
							lytGroup_Size_OS.Expanded = false;
						if (lytGroup_Shape_OD.Expanded)
							lytGroup_Shape_OD.Expanded = false;
						if (lytGroup_Shape_OS.Expanded)
							lytGroup_Shape_OS.Expanded = false;
						if (lytGroup_Pupillometry_OD.Expanded)
							lytGroup_Pupillometry_OD.Expanded = false;
						if (lytGroup_Pupillometry_OS.Expanded)
							lytGroup_Pupillometry_OS.Expanded = false;
					}
				}
			}
		}

		private void lytGroup_RAPD_OS_MouseUp(object sender, MouseEventArgs e)
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
					lytGroup_RAPD_OD.Expanded = group.Expanded;
					if (group.Expanded)
					{
						if (lytGroup_Size_OD.Expanded)
							lytGroup_Size_OD.Expanded = false;
						if (lytGroup_Size_OS.Expanded)
							lytGroup_Size_OS.Expanded = false;
						if (lytGroup_Shape_OD.Expanded)
							lytGroup_Shape_OD.Expanded = false;
						if (lytGroup_Shape_OS.Expanded)
							lytGroup_Shape_OS.Expanded = false;
						if (lytGroup_Pupillometry_OD.Expanded)
							lytGroup_Pupillometry_OD.Expanded = false;
						if (lytGroup_Pupillometry_OS.Expanded)
							lytGroup_Pupillometry_OS.Expanded = false;
					}
				}
			}
		}

		private void lytGroup_Size_OD_MouseUp(object sender, MouseEventArgs e)
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
					lytGroup_Size_OS.Expanded = group.Expanded;
					if (group.Expanded)
					{
						if (lytGroup_RAPD_OD.Expanded)
							lytGroup_RAPD_OD.Expanded = false;
						if (lytGroup_RAPD_OS.Expanded)
							lytGroup_RAPD_OS.Expanded = false;
						if (lytGroup_Shape_OD.Expanded)
							lytGroup_Shape_OD.Expanded = false;
						if (lytGroup_Shape_OS.Expanded)
							lytGroup_Shape_OS.Expanded = false;
						if (lytGroup_Pupillometry_OD.Expanded)
							lytGroup_Pupillometry_OD.Expanded = false;
						if (lytGroup_Pupillometry_OS.Expanded)
							lytGroup_Pupillometry_OS.Expanded = false;
					}
				}
			}
		}

		private void lytGroup_Size_OS_MouseUp(object sender, MouseEventArgs e)
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
					lytGroup_Size_OD.Expanded = group.Expanded;
					if (group.Expanded)
					{
						if (lytGroup_RAPD_OD.Expanded)
							lytGroup_RAPD_OD.Expanded = false;
						if (lytGroup_RAPD_OS.Expanded)
							lytGroup_RAPD_OS.Expanded = false;
						if (lytGroup_Shape_OD.Expanded)
							lytGroup_Shape_OD.Expanded = false;
						if (lytGroup_Shape_OS.Expanded)
							lytGroup_Shape_OS.Expanded = false;
						if (lytGroup_Pupillometry_OD.Expanded)
							lytGroup_Pupillometry_OD.Expanded = false;
						if (lytGroup_Pupillometry_OS.Expanded)
							lytGroup_Pupillometry_OS.Expanded = false;
					}
				}
			}
		}

		private void lytGroup_Shape_OD_MouseUp(object sender, MouseEventArgs e)
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
					lytGroup_Shape_OS.Expanded = group.Expanded;
					if (group.Expanded)
					{
						if (lytGroup_RAPD_OD.Expanded)
							lytGroup_RAPD_OD.Expanded = false;
						if (lytGroup_RAPD_OS.Expanded)
							lytGroup_RAPD_OS.Expanded = false;
						if (lytGroup_Size_OD.Expanded)
							lytGroup_Size_OD.Expanded = false;
						if (lytGroup_Size_OS.Expanded)
							lytGroup_Size_OS.Expanded = false;
						if (lytGroup_Pupillometry_OD.Expanded)
							lytGroup_Pupillometry_OD.Expanded = false;
						if (lytGroup_Pupillometry_OS.Expanded)
							lytGroup_Pupillometry_OS.Expanded = false;
					}
				}
			}
		}

		private void lytGroup_Shape_OS_MouseUp(object sender, MouseEventArgs e)
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
					lytGroup_Shape_OD.Expanded = group.Expanded;
					if (group.Expanded)
					{
						if (lytGroup_RAPD_OD.Expanded)
							lytGroup_RAPD_OD.Expanded = false;
						if (lytGroup_RAPD_OS.Expanded)
							lytGroup_RAPD_OS.Expanded = false;
						if (lytGroup_Size_OD.Expanded)
							lytGroup_Size_OD.Expanded = false;
						if (lytGroup_Size_OS.Expanded)
							lytGroup_Size_OS.Expanded = false;
						if (lytGroup_Pupillometry_OD.Expanded)
							lytGroup_Pupillometry_OD.Expanded = false;
						if (lytGroup_Pupillometry_OS.Expanded)
							lytGroup_Pupillometry_OS.Expanded = false;
					}
				}
			}
		}

		private void lytGroup_Pupillometry_OD_MouseUp(object sender, MouseEventArgs e)
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
					lytGroup_Pupillometry_OS.Expanded = group.Expanded;
					if (group.Expanded)
					{
						if (lytGroup_RAPD_OD.Expanded)
							lytGroup_RAPD_OD.Expanded = false;
						if (lytGroup_RAPD_OS.Expanded)
							lytGroup_RAPD_OS.Expanded = false;
						if (lytGroup_Size_OD.Expanded)
							lytGroup_Size_OD.Expanded = false;
						if (lytGroup_Size_OS.Expanded)
							lytGroup_Size_OS.Expanded = false;
						if (lytGroup_Shape_OD.Expanded)
							lytGroup_Shape_OD.Expanded = false;
						if (lytGroup_Shape_OS.Expanded)
							lytGroup_Shape_OS.Expanded = false;
					}
				}
			}
		}

		private void lytGroup_Pupillometry_OS_MouseUp(object sender, MouseEventArgs e)
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
					lytGroup_Pupillometry_OD.Expanded = group.Expanded;
					if (group.Expanded)
					{
						if (lytGroup_RAPD_OD.Expanded)
							lytGroup_RAPD_OD.Expanded = false;
						if (lytGroup_RAPD_OS.Expanded)
							lytGroup_RAPD_OS.Expanded = false;
						if (lytGroup_Size_OD.Expanded)
							lytGroup_Size_OD.Expanded = false;
						if (lytGroup_Size_OS.Expanded)
							lytGroup_Size_OS.Expanded = false;
						if (lytGroup_Shape_OD.Expanded)
							lytGroup_Shape_OD.Expanded = false;
						if (lytGroup_Shape_OS.Expanded)
							lytGroup_Shape_OS.Expanded = false;
					}
				}
			}
		}

		#endregion

		#endregion

		#region Implementation of PEMR_Pupillary

		public object IsNoAbnormalitiesFound_OD
		{
			get { return chkNoAbnormalitiesFound_OD.Checked; }
			set { chkNoAbnormalitiesFound_OD.Checked = Convert.ToBoolean(chkNoAbnormalitiesFound_OD.Checked); }
		}

		public object IsNoAbnormalitiesFound_OS
		{
			get { return chkNoAbnormalitiesFound_OS.Checked; }
			set { chkNoAbnormalitiesFound_OS.Checked = Convert.ToBoolean(chkNoAbnormalitiesFound_OS.Checked); }
		}

		public object PupillaryAbnormalities_CU_ID_OD
		{
			get { return lkeAbnormalitiesCauses_OD.EditValue; }
			set { lkeAbnormalitiesCauses_OD.EditValue = value; }
		}

		public object PupillaryAbnormalities_CU_ID_OS
		{
			get { return lkeAbnormalitiesCauses_OS.EditValue; }
			set { lkeAbnormalitiesCauses_OS.EditValue = value; }
		}

		public object IsRAPD_OD
		{
			get { return chkPositiveRAPD_OD.Checked; }
			set
			{
				if (value == null)
				{
					chkPositiveRAPD_OD.Checked = false;
					chkNegativeRAPD_OD.Checked = false;
					return;
				}
				if (Convert.ToBoolean(value))
					chkPositiveRAPD_OD.Checked = true;
				else
					chkNegativeRAPD_OD.Checked = true;
			}
		}

		public object IsRAPD_OS
		{
			get { return chkPositiveRAPD_OS.Checked; }
			set
			{
				if (value == null)
				{
					chkPositiveRAPD_OS.Checked = false;
					chkNegativeRAPD_OS.Checked = false;
					return;
				}

				if (Convert.ToBoolean(value))
					chkPositiveRAPD_OS.Checked = true;
				else
					chkNegativeRAPD_OS.Checked = true;
			}
		}

		public object PupillaryRAPDGradingScale_P_ID_OD
		{
			get
			{
				if (chkGrade1_OD.Checked)
					return (int) DB_PupillaryRAPDGradingScale.Grade1;
				if (chkGrade2_OD.Checked)
					return (int)DB_PupillaryRAPDGradingScale.Grade2;
				if (chkGrade3_OD.Checked)
					return (int)DB_PupillaryRAPDGradingScale.Grade3;
				if (chkGrade4_OD.Checked)
					return (int)DB_PupillaryRAPDGradingScale.Grade4;
				if (chkGrade5_OD.Checked)
					return (int)DB_PupillaryRAPDGradingScale.Grade5;
				return null;
			}
			set
			{
				if (value == null)
					return;
				DB_PupillaryRAPDGradingScale scale = (DB_PupillaryRAPDGradingScale) value;
				switch (scale)
				{
					case DB_PupillaryRAPDGradingScale.Grade1:
						chkGrade1_OD.Checked = true;
						break;
					case DB_PupillaryRAPDGradingScale.Grade2:
						chkGrade2_OD.Checked = true;
						break;
					case DB_PupillaryRAPDGradingScale.Grade3:
						chkGrade3_OD.Checked = true;
						break;
					case DB_PupillaryRAPDGradingScale.Grade4:
						chkGrade4_OD.Checked = true;
						break;
					case DB_PupillaryRAPDGradingScale.Grade5:
						chkGrade5_OD.Checked = true;
						break;
				}
			}
		}

		public object PupillaryRAPDGradingScale_P_ID_OS
		{
			get
			{
				if (chkGrade1_OS.Checked)
					return (int)DB_PupillaryRAPDGradingScale.Grade1;
				if (chkGrade2_OS.Checked)
					return (int)DB_PupillaryRAPDGradingScale.Grade2;
				if (chkGrade3_OS.Checked)
					return (int)DB_PupillaryRAPDGradingScale.Grade3;
				if (chkGrade4_OS.Checked)
					return (int)DB_PupillaryRAPDGradingScale.Grade4;
				if (chkGrade5_OS.Checked)
					return (int)DB_PupillaryRAPDGradingScale.Grade5;
				return null;
			}
			set
			{
				if (value == null)
					return;
				DB_PupillaryRAPDGradingScale scale = (DB_PupillaryRAPDGradingScale)value;
				switch (scale)
				{
					case DB_PupillaryRAPDGradingScale.Grade1:
						chkGrade1_OS.Checked = true;
						break;
					case DB_PupillaryRAPDGradingScale.Grade2:
						chkGrade2_OS.Checked = true;
						break;
					case DB_PupillaryRAPDGradingScale.Grade3:
						chkGrade3_OS.Checked = true;
						break;
					case DB_PupillaryRAPDGradingScale.Grade4:
						chkGrade4_OS.Checked = true;
						break;
					case DB_PupillaryRAPDGradingScale.Grade5:
						chkGrade5_OS.Checked = true;
						break;
				}
			}
		}

		public object PupillaryRAPDCauses_CU_ID_OD
		{
			get { return lkeRAPDCauses_OD.EditValue; }
			set { lkeRAPDCauses_OD.EditValue = value; }
		}

		public object PupillaryRAPDCauses_CU_ID_OS
		{
			get { return lkeRAPDCauses_OS.EditValue; }
			set { lkeRAPDCauses_OS.EditValue = value; }
		}

		public object PupillarySize_P_ID_OD
		{
			get
			{
				if (chkSize_Miosis_OD.Checked)
					return (int)DB_PupillarySize.Miosis;
				if (chkSize_Mediuim_OD.Checked)
					return (int)DB_PupillarySize.Medium;
				if (chkSize_Mydrisasis_OD.Checked)
					return (int)DB_PupillarySize.Mydrisasis;
				return null;
			}
			set
			{
				if (value == null)
					return;
				DB_PupillarySize size = (DB_PupillarySize)value;
				switch (size)
				{
					case DB_PupillarySize.Miosis:
						chkSize_Miosis_OD.Checked = true;
						break;
					case DB_PupillarySize.Medium:
						chkSize_Mediuim_OD.Checked = true;
						break;
					case DB_PupillarySize.Mydrisasis:
						chkSize_Mydrisasis_OD.Checked = true;
						break;
				}
			}
		}

		public object PupillarySize_P_ID_OS
		{
			get
			{
				if (chkSize_Miosis_OS.Checked)
					return (int)DB_PupillarySize.Miosis;
				if (chkSize_Mediuim_OS.Checked)
					return (int)DB_PupillarySize.Medium;
				if (chkSize_Mydrisasis_OS.Checked)
					return (int)DB_PupillarySize.Mydrisasis;
				return null;
			}
			set
			{
				if (value == null)
					return;
				DB_PupillarySize size = (DB_PupillarySize)value;
				switch (size)
				{
					case DB_PupillarySize.Miosis:
						chkSize_Miosis_OS.Checked = true;
						break;
					case DB_PupillarySize.Medium:
						chkSize_Mediuim_OS.Checked = true;
						break;
					case DB_PupillarySize.Mydrisasis:
						chkSize_Mydrisasis_OS.Checked = true;
						break;
				}
			}
		}

		public object PupillaryShape_P_OD
		{
			get
			{
				if (chkShape_Rounded_OD.Checked)
					return (int)DB_PupillaryShape.Rounded;
				if (chkShape_Oval_OD.Checked)
					return (int)DB_PupillaryShape.Oval;
				if (chkShape_Sector_OD.Checked)
					return (int)DB_PupillaryShape.Sector;
				if (chkShape_Irruglar_OD.Checked)
					return (int)DB_PupillaryShape.Irruglar;
				if (chkShape_Peaked_OD.Checked)
					return (int)DB_PupillaryShape.Peaked;
				return null;
			}
			set
			{
				if (value == null)
					return;
				DB_PupillaryShape shape = (DB_PupillaryShape)value;
				switch (shape)
				{
					case DB_PupillaryShape.Rounded:
						chkShape_Rounded_OD.Checked = true;
						break;
					case DB_PupillaryShape.Oval:
						chkShape_Oval_OD.Checked = true;
						break;
					case DB_PupillaryShape.Sector:
						chkShape_Sector_OD.Checked = true;
						break;
					case DB_PupillaryShape.Irruglar:
						chkShape_Irruglar_OD.Checked = true;
						break;
					case DB_PupillaryShape.Peaked:
						chkShape_Peaked_OD.Checked = true;
						break;
				}
			}
		}

		public object PupillaryShape_P_OS
		{
			get
			{
				if (chkShape_Rounded_OS.Checked)
					return (int)DB_PupillaryShape.Rounded;
				if (chkShape_Oval_OS.Checked)
					return (int)DB_PupillaryShape.Oval;
				if (chkShape_Sector_OS.Checked)
					return (int)DB_PupillaryShape.Sector;
				if (chkShape_Irruglar_OS.Checked)
					return (int)DB_PupillaryShape.Irruglar;
				if (chkShape_Peaked_OS.Checked)
					return (int)DB_PupillaryShape.Peaked;
				return null;
			}
			set
			{
				if (value == null)
					return;
				DB_PupillaryShape shape = (DB_PupillaryShape)value;
				switch (shape)
				{
					case DB_PupillaryShape.Rounded:
						chkShape_Rounded_OS.Checked = true;
						break;
					case DB_PupillaryShape.Oval:
						chkShape_Oval_OS.Checked = true;
						break;
					case DB_PupillaryShape.Sector:
						chkShape_Sector_OS.Checked = true;
						break;
					case DB_PupillaryShape.Irruglar:
						chkShape_Irruglar_OS.Checked = true;
						break;
					case DB_PupillaryShape.Peaked:
						chkShape_Peaked_OS.Checked = true;
						break;
				}
			}
		}

		public object Scotopic_OD
		{
			get { return spnScotopic_OD.EditValue; }
			set { spnScotopic_OD.EditValue = value; }
		}

		public object HighPhotopic_OD
		{
			get { return spnHighPhotopic_OD.EditValue; }
			set { spnHighPhotopic_OD.EditValue = value; }
		}

		public object LowPhotopic_OD
		{
			get { return spnLowPhotopic_OD.EditValue; }
			set { spnLowPhotopic_OD.EditValue = value; }
		}

		public object HighMesopic_OD
		{
			get { return spnHighMesopic_OD.EditValue; }
			set { spnHighMesopic_OD.EditValue = value; }
		}

		public object LowMesopic_OD
		{
			get { return spnLowMesopic_OD.EditValue; }
			set { spnLowMesopic_OD.EditValue = value; }
		}

		public object Scotopic_OS
		{
			get { return spnScotopic_OS.EditValue; }
			set { spnScotopic_OS.EditValue = value; }
		}

		public object HighPhotopic_OS
		{
			get { return spnHighPhotopic_OS.EditValue; }
			set { spnHighPhotopic_OS.EditValue = value; }
		}

		public object LowPhotopic_OS
		{
			get { return spnLowPhotopic_OS.EditValue; }
			set { spnLowPhotopic_OS.EditValue = value; }
		}

		public object HighMesopic_OS
		{
			get { return spnHighMesopic_OS.EditValue; }
			set { spnHighMesopic_OS.EditValue = value; }
		}

		public object LowMesopic_OS
		{
			get { return spnLowMesopic_OS.EditValue; }
			set { spnLowMesopic_OS.EditValue = value; }
		}

		public object FurtherDetails_OD
		{
			get { return txtReccommednations_OD.EditValue; }
			set { txtReccommednations_OD.EditValue = value; }
		}

		public object FurtherDetails_OS
		{
			get { return txtReccommednations_OS.EditValue; }
			set { txtReccommednations_OS.EditValue = value; }
		}

		#endregion

	}
}
