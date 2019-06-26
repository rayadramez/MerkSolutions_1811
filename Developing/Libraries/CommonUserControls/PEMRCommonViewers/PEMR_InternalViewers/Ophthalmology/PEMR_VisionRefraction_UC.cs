using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ApplicationConfiguration;
using CommonControlLibrary;
using CommonUserControls.PEMRCommonViewers.PEMR_Interfaces;
using DevExpress.XtraEditors;
using DevExpress.XtraLayout.Utils;
using MerkDataBaseBusinessLogicProject;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.PEMRBusinessLogic;
using MVCBusinessLogicLibrary.BaseViewers;

namespace CommonUserControls.PEMRCommonViewers.PEMR_InternalViewers.Ophthalmology
{
	public enum ReadingsMode
	{
		None = 0,
		CreateNewReading = 1,
		RemovingReading = 2,
		EdiitingExistingReading = 3,
		ViewingActiveAllReadings = 4,
		ViewingPreviousReadings = 5
	}

	public partial class PEMR_VisionRefraction_UC : UserControl, IPEMR_Viewer, IPEMR_VisionRefractionReading
	{
		public ReadingsMode ReadingsMode { get; set; }
		public GetPreviousVisitTiming_VisionRefractionReading_Result Selected_VisitTiming_VisionRefractionReading { get; set; }

		public PEMR_VisionRefraction_UC()
		{
			InitializeComponent();

			CommonViewsActions.SetupGridControl(grdControl, Resources.LocalizedRes.grd_PEMR_VisionRefraction, true);
			ClearControls(true);
			FillControls();

			CommonViewsActions.Decorate(dtSearchFrom, dtSearchTo, takenDate, takesTime, spnDistance_OD, spnDistance_OS,
				lkeNVA_OD, lkeNVA_OS, spnNVAAmount_OD, spnNVAAmount_OS, spnSPH_OD, spnSPH_OS, spnCYL_OD, spnCYL_OS,
				spnAxis_OD, spnAxis_OS, lkeUVA_OD, lkeUVA_OS, spnAdd_OD, spnAdd_OS, lkeUVA_OU, lkeNVA_OU,
				spnNVAAmount_OU);
		}

		private void PEMR_VisionRefraction_Load(object sender, EventArgs e)
		{

		}

		public void Initialize(ReadingsMode readingMode,
			GetPreviousVisitTiming_VisionRefractionReading_Result visionRefractionToLoad)
		{
			ReadingsMode = readingMode;
			switch (readingMode)
			{
				case ReadingsMode.ViewingActiveAllReadings:
					lytGroup_AllReadings.Visibility = LayoutVisibility.Always;
					lytGroup_AllReadings.Expanded = true;
					lytGroup_ReadingParent.Visibility = lytGroup_ReadingDetails.Visibility = lyt_Cancel.Visibility =
						emptySpaceItem4.Visibility =
							lyt_Add.Visibility = emptySpaceItem15.Visibility = LayoutVisibility.Never;
					emptySpaceItem2.Visibility = lytNewReadings.Visibility = LayoutVisibility.Always;
					btnAllReadings_Click(null, null);
					break;
				case ReadingsMode.ViewingPreviousReadings:
					Selected_VisitTiming_VisionRefractionReading = visionRefractionToLoad;
					btnNewReading.Text = "Active Reading";
					btnAllReadings.Text = "Previous Readings";
					emptySpaceItem2.Visibility = lytNewReadings.Visibility = LayoutVisibility.Never;
					ReadyForNewOrEditing(false);
					SetControlsValues();
					lyt_Cancel.Visibility = lyt_Add.Visibility = emptySpaceItem4.Visibility =
						lytViewAllReadings.Visibility = LayoutVisibility.Never;
					break;
			}

			if (PEMRBusinessLogic.ActivePEMRObject != null)
			{
				List<GetPreviousVisitTiming_VisionRefractionReading_Result> list =
					PEMRBusinessLogic.GetPrevious_VisitTiming_VisionRefractionReading(
						PEMRBusinessLogic.ActivePEMRObject.Active_Patient.ID, dtSearchFrom.EditValue,
						dtSearchTo.EditValue);
				if (list != null)
					list = list.OrderByDescending(item => item.TakenDateTime).ToList();
				grdControl.DataSource = list;
				grdControl.RefreshDataSource();
				SetToolTip(list);
			}
		}

		public void ReadyForNewOrEditing(bool enableControls)
		{
			lytGroup_AllReadings.Visibility = LayoutVisibility.Never;
			lytGroup_AllReadings.Expanded = false;
			lytGroup_ReadingParent.Visibility = LayoutVisibility.Always;
			ClearControls(false);
			chkAutoRef_CheckedChanged(null, null);
			if (ReadingsMode != ReadingsMode.ViewingPreviousReadings)
				ReadingsMode = ReadingsMode.CreateNewReading;
			FillControls();
			takenDate.Properties.ReadOnly = takesTime.Properties.ReadOnly = rating_OD.Properties.ReadOnly =
				spnDistance_OD.Properties.ReadOnly = lkeNVA_OD.Properties.ReadOnly =
					spnNVAAmount_OD.Properties.ReadOnly = spnSPH_OD.Properties.ReadOnly =
						spnCYL_OD.Properties.ReadOnly = spnAxis_OD.Properties.ReadOnly = lkeUVA_OD.Properties.ReadOnly =
							spnAdd_OD.Properties.ReadOnly = txtRemarks_OD.Properties.ReadOnly =
								rating_OD.Properties.ReadOnly = spnDistance_OS.Properties.ReadOnly =
									lkeNVA_OS.Properties.ReadOnly = spnNVAAmount_OS.Properties.ReadOnly =
										spnSPH_OS.Properties.ReadOnly = spnCYL_OS.Properties.ReadOnly =
											spnAxis_OS.Properties.ReadOnly = lkeUVA_OS.Properties.ReadOnly =
												spnAdd_OS.Properties.ReadOnly = txtRemarks_OS.Properties.ReadOnly =
													lkeUVA_OU.Properties.ReadOnly = lkeNVA_OU.Properties.ReadOnly =
														spnNVAAmount_OU.Properties.ReadOnly = !enableControls;
		}

		public void SetControlsValues()
		{
			switch (ReadingsMode)
			{
				case ReadingsMode.ViewingPreviousReadings:
					if (Selected_VisitTiming_VisionRefractionReading == null)
						return;
					TakenDate = Selected_VisitTiming_VisionRefractionReading.TakenDateTime.Date;
					TakenTime = Selected_VisitTiming_VisionRefractionReading.TakenDateTime;
					VisionRefractionReadingTypeID =
						Selected_VisitTiming_VisionRefractionReading.VisionRefractionReadingType_P_ID;
					UVA_OU = Selected_VisitTiming_VisionRefractionReading.UVA_OU;
					NVA_OU = Selected_VisitTiming_VisionRefractionReading.NVA_OU;
					NVAAmount_OU = Selected_VisitTiming_VisionRefractionReading.NVAAmount_OU;
					Distance_OD = Selected_VisitTiming_VisionRefractionReading.Distance_OD;
					Distance_OS = Selected_VisitTiming_VisionRefractionReading.Distance_OS;
					NVA_OD = Selected_VisitTiming_VisionRefractionReading.NVA_OD;
					NVA_OS = Selected_VisitTiming_VisionRefractionReading.NVA_OS;
					NVAAmount_OD = Selected_VisitTiming_VisionRefractionReading.NVAAmount_OD;
					NVAAmount_OS = Selected_VisitTiming_VisionRefractionReading.NVAAmount_OS;
					IsIgnored_OD = Selected_VisitTiming_VisionRefractionReading.IsIgnored_OD;
					IsIgnored_OS = Selected_VisitTiming_VisionRefractionReading.IsIgnored_OS;
					IsError_OD = Selected_VisitTiming_VisionRefractionReading.IsError_OD;
					IsError_OS = Selected_VisitTiming_VisionRefractionReading.IsError_OS;
					RatingAmount_OD = Selected_VisitTiming_VisionRefractionReading.Rating_OD;
					RatingAmount_OS = Selected_VisitTiming_VisionRefractionReading.Rating_OS;
					SphereAmount_OD = Selected_VisitTiming_VisionRefractionReading.Sph_OD;
					SphereAmount_OS = Selected_VisitTiming_VisionRefractionReading.Sph_OS;
					CylinderAmount_OD = Selected_VisitTiming_VisionRefractionReading.Cyl_OD;
					CylinderAmount_OS = Selected_VisitTiming_VisionRefractionReading.Cyl_OS;
					AxisAmount_OD = Selected_VisitTiming_VisionRefractionReading.Axis_OD;
					AxisAmount_OS = Selected_VisitTiming_VisionRefractionReading.Axis_OS;
					Add_OD = Selected_VisitTiming_VisionRefractionReading.Add_OD;
					Add_OS = Selected_VisitTiming_VisionRefractionReading.Add_OS;
					UVA_OD = Selected_VisitTiming_VisionRefractionReading.UVA_OD;
					UVA_OS = Selected_VisitTiming_VisionRefractionReading.UVA_OS;
					break;
			}
		}

		public void SetToolTip(List<GetPreviousVisitTiming_VisionRefractionReading_Result> list)
		{
			string distance_OD = "";
			string distance_OS = "";
			string nva_OD = "";
			string nva_OS = "";
			string nvaAmount_OD = "";
			string nvaAmount_ODS = "";
			string sph_OD = "";
			string sph_OS = "";
			string cyl_OD = "";
			string cyl_OS = "";
			string axis_OD = "";
			string axis_OS = "";
			string uva_OD = "";
			string uva_OS = "";
			string add_OD = "";
			string add_OS = "";
			foreach (GetPreviousVisitTiming_VisionRefractionReading_Result result in list)
			{
				distance_OD += result.Distance_OD + " - " + result.TakenDateTime.Date.ConvertDateTimeToString(false, false, true) + "\r\n";
				distance_OS += result.Distance_OS + " - " + result.TakenDateTime.Date.ConvertDateTimeToString(false, false, true) + "\r\n";
				nva_OD += result.Distance_OD + " - " + result.TakenDateTime.Date.ConvertDateTimeToString(false, false, true) + "\r\n";
				nva_OS += result.Distance_OS + " - " + result.TakenDateTime.Date.ConvertDateTimeToString(false, false, true) + "\r\n";
				nvaAmount_OD += result.NVAAmount_OD + " - " + result.TakenDateTime.Date.ConvertDateTimeToString(false, false, true) + "\r\n";
				nvaAmount_ODS += result.NVAAmount_OS + " - " + result.TakenDateTime.Date.ConvertDateTimeToString(false, false, true) + "\r\n";
				sph_OD += result.Sph_OD + " - " + result.TakenDateTime.Date.ConvertDateTimeToString(false, false, true) + "\r\n";
				sph_OS += result.Sph_OS + " - " + result.TakenDateTime.Date.ConvertDateTimeToString(false, false, true) + "\r\n";
				cyl_OD += result.Cyl_OD + " - " + result.TakenDateTime.Date.ConvertDateTimeToString(false, false, true) + "\r\n";
				cyl_OS += result.Cyl_OS + " - " + result.TakenDateTime.Date.ConvertDateTimeToString(false, false, true) + "\r\n";
				axis_OD += result.Axis_OD + " - " + result.TakenDateTime.Date.ConvertDateTimeToString(false, false, true) + "\r\n";
				axis_OS += result.Axis_OS + " - " + result.TakenDateTime.Date.ConvertDateTimeToString(false, false, true) + "\r\n";
				uva_OD += result.UVA_OD + " - " + result.TakenDateTime.Date.ConvertDateTimeToString(false, false, true) + "\r\n";
				uva_OS += result.UVA_OS + " - " + result.TakenDateTime.Date.ConvertDateTimeToString(false, false, true) + "\r\n";
				add_OD += result.Add_OD + " - " + result.TakenDateTime.Date.ConvertDateTimeToString(false, false, true) + "\r\n";
				add_OS += result.Add_OS + " - " + result.TakenDateTime.Date.ConvertDateTimeToString(false, false, true) + "\r\n";
			}
			spnDistance_OD.ToolTip = distance_OD;
			spnDistance_OS.ToolTip = distance_OS;
			lkeNVA_OD.ToolTip = nva_OD;
			lkeNVA_OS.ToolTip = nva_OS;
			spnNVAAmount_OD.ToolTip = nvaAmount_OD;
			spnNVAAmount_OS.ToolTip = nvaAmount_ODS;
			spnSPH_OD.ToolTip = sph_OD;
			spnSPH_OS.ToolTip = sph_OS;
			spnCYL_OD.ToolTip = cyl_OD;
			spnCYL_OS.ToolTip = cyl_OS;
			spnAxis_OD.ToolTip = axis_OD;
			spnAxis_OS.ToolTip = axis_OS;
			lkeUVA_OD.ToolTip = uva_OD;
			lkeUVA_OS.ToolTip = uva_OS;
			spnAdd_OD.ToolTip = add_OD;
			spnAdd_OS.ToolTip = add_OS;
		}

		#region Implementation of IPEMR_Viewer

		public void ClearControls(bool clearAll)
		{
			takenDate.EditValue = null;
			takesTime.EditValue = null;
			VisionRefractionReadingTypeID = (int)DB_VisionRefractionReadingType.AutoRef;

			UVA_OU = null;
			NVA_OU = null;
			NVAAmount_OU = null;

			Distance_OD = null;
			NVA_OD = null;
			NVAAmount_OD = null;
			SphereAmount_OD = null;
			CylinderAmount_OD = null;
			AxisAmount_OD = null;
			UVA_OD = null;
			Add_OD = null;
			Remarks_OD = null;
			IsIgnored_OD = false;
			IsError_OD = false;
			RatingAmount_OD = 0;

			Distance_OS = null;
			NVA_OS = null;
			NVAAmount_OS = null;
			SphereAmount_OS = null;
			CylinderAmount_OS = null;
			AxisAmount_OS = null;
			UVA_OS = null;
			Add_OS = null;
			Remarks_OS = null;
			IsIgnored_OS = false;
			IsError_OS = false;
			RatingAmount_OS = 0;

			if (clearAll)
			{
				ReadingsMode = ReadingsMode.ViewingActiveAllReadings;
				lytGroup_AllReadings.Visibility = LayoutVisibility.Always;
				lytGroup_AllReadings.Expanded = true;
				lytGroup_ReadingParent.Visibility = lytGroup_ReadingDetails.Visibility = lyt_Cancel.Visibility =
					emptySpaceItem4.Visibility =
						lyt_Add.Visibility = emptySpaceItem15.Visibility = LayoutVisibility.Never;
				btnAllReadings_Click(null, null);
			}
		}

		public void FillControls()
		{
			CommonViewsActions.FillGridlookupEdit(lkeUVA_OD,
				UncorrectedDistanceVisualAcuity_cu.ItemsList.FindAll(item =>
					Convert.ToInt32(item.UncorrectedDistanceVisualAcuityUnit_P_ID)
						.Equals(Convert.ToInt32(DB_UncorrectedDistanceVisualAcuityUnit.Decimal))));
			CommonViewsActions.FillGridlookupEdit(lkeUVA_OS,
				UncorrectedDistanceVisualAcuity_cu.ItemsList.FindAll(item =>
					Convert.ToInt32(item.UncorrectedDistanceVisualAcuityUnit_P_ID)
						.Equals(Convert.ToInt32(DB_UncorrectedDistanceVisualAcuityUnit.Decimal))));
			CommonViewsActions.FillGridlookupEdit(lkeUVA_OU,
				UncorrectedDistanceVisualAcuity_cu.ItemsList.FindAll(item =>
					Convert.ToInt32(item.UncorrectedDistanceVisualAcuityUnit_P_ID)
						.Equals(Convert.ToInt32(DB_UncorrectedDistanceVisualAcuityUnit.Decimal))));
			CommonViewsActions.FillGridlookupEdit(lkeNVA_OD, NearVisiong_p.ItemsList);
			CommonViewsActions.FillGridlookupEdit(lkeNVA_OS, NearVisiong_p.ItemsList);
			CommonViewsActions.FillGridlookupEdit(lkeNVA_OU, NearVisiong_p.ItemsList);
			TakenDate = DateTime.Now.Date;
			TakenTime = DateTime.Now;
			dtSearchFrom.EditValue = dtSearchTo.EditValue = null;
		}

		#endregion

		#region Controls Events

		#region CheckEdit Controls

		private void chkAutoRef_CheckedChanged(object sender, EventArgs e)
		{
			lytGroup_ReadingDetails.Visibility = emptySpaceItem4.Visibility = lyt_Cancel.Visibility =
				lyt_Add.Visibility = emptySpaceItem15.Visibility =
					chkAutoRef.Checked ? LayoutVisibility.Always : LayoutVisibility.Never;
			lytGroup_ReadingParent.Text = "AutoRef Readings";
		}

		private void chkAutoRefAfterCyclo_CheckedChanged(object sender, EventArgs e)
		{
			lytGroup_ReadingDetails.Visibility = emptySpaceItem4.Visibility = lyt_Cancel.Visibility =
				lyt_Add.Visibility = emptySpaceItem15.Visibility =
					chkAutoRefAfterCyclo.Checked ? LayoutVisibility.Always : LayoutVisibility.Never;
			lytGroup_ReadingParent.Text = "AutoRef After Cyclo Readings";
		}

		private void chkOldGlasses_CheckedChanged(object sender, EventArgs e)
		{
			lytGroup_ReadingDetails.Visibility = emptySpaceItem4.Visibility = lyt_Cancel.Visibility =
				lyt_Add.Visibility = emptySpaceItem15.Visibility =
					chkOldGlasses.Checked ? LayoutVisibility.Always : LayoutVisibility.Never;
			lytGroup_ReadingParent.Text = "Old Glasses Readings";
		}

		private void chkOldGlassesNear_CheckedChanged(object sender, EventArgs e)
		{
			lytGroup_ReadingDetails.Visibility = emptySpaceItem4.Visibility = lyt_Cancel.Visibility =
				lyt_Add.Visibility = emptySpaceItem15.Visibility =
					chkOldGlassesNear.Checked ? LayoutVisibility.Always : LayoutVisibility.Never;
			lytGroup_ReadingParent.Text = "Old Glasses (NEAR) Readings";
		}

		private void chkSubjectivePrescription_CheckedChanged(object sender, EventArgs e)
		{
			lytGroup_ReadingDetails.Visibility = emptySpaceItem4.Visibility = lyt_Cancel.Visibility =
				lyt_Add.Visibility = emptySpaceItem15.Visibility =
					chkSubjectivePrescription.Checked ? LayoutVisibility.Always : LayoutVisibility.Never;
			lytGroup_ReadingParent.Text = "Subjective Refraction Readings";
		}

		private void chkError_OD_CheckedChanged(object sender, EventArgs e)
		{
			rating_OD.Enabled = spnDistance_OD.Enabled = lkeNVA_OD.Enabled = spnNVAAmount_OD.Enabled = spnSPH_OD.Enabled =
				spnCYL_OD.Enabled = spnAxis_OD.Enabled =
					lkeUVA_OD.Enabled = spnAdd_OD.Enabled = !Convert.ToBoolean(IsError_OD);
			if (Convert.ToBoolean(IsError_OD))
				RatingAmount_OD = Distance_OD = NVA_OD = NVAAmount_OD = SphereAmount_OD =
					CylinderAmount_OD = AxisAmount_OD = UVA_OD = Add_OD = null;
		}

		private void chkError_OS_CheckedChanged(object sender, EventArgs e)
		{
			rating_OS.Enabled = spnDistance_OS.Enabled = lkeNVA_OS.Enabled = spnNVAAmount_OS.Enabled = spnSPH_OS.Enabled =
				spnCYL_OS.Enabled = spnAxis_OS.Enabled =
					lkeUVA_OS.Enabled = spnAdd_OS.Enabled = !Convert.ToBoolean(IsError_OS);
			if (Convert.ToBoolean(IsError_OS))
				RatingAmount_OS = Distance_OS = NVA_OS = NVAAmount_OS = SphereAmount_OS =
					CylinderAmount_OS = AxisAmount_OS = UVA_OS = Add_OS = null;
		}

		#endregion

		#region Button Events

		private void btnNewReading_Click(object sender, EventArgs e)
		{
			switch (ReadingsMode)
			{
				case ReadingsMode.ViewingActiveAllReadings:
					ReadyForNewOrEditing(true);
					break;
				case ReadingsMode.ViewingPreviousReadings:
					ReadyForNewOrEditing(false);
					break;
			}
		}

		private void btnAllReadings_Click(object sender, EventArgs e)
		{
			DialogResult result;
			switch (ReadingsMode)
			{
				case ReadingsMode.CreateNewReading:
					result = XtraMessageBox.Show("Do you want to Cancel this Reading ?", "Note", MessageBoxButtons.YesNo,
						MessageBoxIcon.Exclamation);
					switch (result)
					{
						case DialogResult.Yes:
							ReadingsMode = ReadingsMode.ViewingActiveAllReadings;
							lytGroup_AllReadings.Visibility = LayoutVisibility.Always;
							lytGroup_AllReadings.Expanded = true;
							lytGroup_ReadingParent.Visibility = lytGroup_ReadingDetails.Visibility = lyt_Cancel.Visibility =
								emptySpaceItem4.Visibility =
									lyt_Add.Visibility = emptySpaceItem15.Visibility = LayoutVisibility.Never;

							List<GetPreviousVisitTiming_VisionRefractionReading_Result> list =
								PEMRBusinessLogic.GetPrevious_VisitTiming_VisionRefractionReading(
									PEMRBusinessLogic.ActivePEMRObject.Active_Patient.ID, dtSearchFrom.EditValue,
									dtSearchTo.EditValue);
							grdControl.DataSource = list;
							grdControl.RefreshDataSource();
							SetToolTip(list);
							break;
					}
					break;
			}
		}

		private void btnCopyToOS_Click(object sender, EventArgs e)
		{
			DialogResult result = XtraMessageBox.Show("Do you want to Copy To OS ?", "Note", MessageBoxButtons.YesNo,
				MessageBoxIcon.Exclamation);
			switch (result)
			{
				case DialogResult.Yes:
					Distance_OS = Distance_OD;
					NVA_OS = NVAAmount_OD;
					NVAAmount_OS = NVAAmount_OD;
					SphereAmount_OS = SphereAmount_OD;
					CylinderAmount_OS = CylinderAmount_OD;
					AxisAmount_OS = AxisAmount_OD;
					UVA_OS = UVA_OD;
					Add_OS = Add_OD;
					break;
			}
		}

		private void btnCopyToOD_Click(object sender, EventArgs e)
		{
			DialogResult result = XtraMessageBox.Show("Do you want to Copy To OD ?", "Note", MessageBoxButtons.YesNo,
				MessageBoxIcon.Exclamation);
			switch (result)
			{
				case DialogResult.Yes:
					Distance_OD = Distance_OS;
					NVA_OD = NVA_OS;
					NVAAmount_OD = NVAAmount_OS;
					SphereAmount_OD = SphereAmount_OS;
					CylinderAmount_OD = CylinderAmount_OS;
					AxisAmount_OD = AxisAmount_OS;
					UVA_OD = UVA_OS;
					Add_OD = Add_OS;
					break;
			}
		}

		private void btnAdd_Click(object sender, EventArgs e)
		{
			DialogResult result = XtraMessageBox.Show("Do you to add this Readings?", "Note", MessageBoxButtons.YesNo,
				MessageBoxIcon.Exclamation);
			switch (result)
			{
				case DialogResult.Yes:
					VisitTiming_VisionRefractionReading visitTimingVisionRefraction =
						PEMRBusinessLogic.CreateNew_VisitTiming_VisionRefractionReading(this,
							ApplicationStaticConfiguration.PEMRSavingMode);
					if (visitTimingVisionRefraction != null)
					{
						if (PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_VisionRefractionReading == null)
							PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_VisionRefractionReading =
								new List<VisitTiming_VisionRefractionReading>();
						PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_VisionRefractionReading.Add(
							visitTimingVisionRefraction);
						List<GetPreviousVisitTiming_VisionRefractionReading_Result> list =
							PEMRBusinessLogic.GetPrevious_VisitTiming_VisionRefractionReading(
								PEMRBusinessLogic.ActivePEMRObject.Active_Patient.ID, dtSearchFrom.EditValue,
								dtSearchTo.EditValue);
						if (list != null)
							list = list.OrderByDescending(item => item.TakenDateTime).ToList();
						grdControl.DataSource = list;
						grdControl.RefreshDataSource();
						SetToolTip(list);
						ClearControls(true);
					}
					break;
			}
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			//if (PEMRBusinessLogic.ActivePEMRObject != null)
			//	if (PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_SocialHistory == null)
			//	{
			//		Active_VisitTiming_SocialHistory = PEMRBusinessLogic.CreateNew_VisitTiming_SocialHistory(this, DB_PEMRSavingMode.SaveImmediately);
			//		if (Active_VisitTiming_SocialHistory == null)
			//			return;
			//		if (PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_SocialHistory == null)
			//			PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_SocialHistory = new List<VisitTiming_SocialHistory>();
			//		PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_SocialHistory.Add(Active_VisitTiming_SocialHistory);
			//		XtraMessageBox.Show("Saved Successfully", "Saved", MessageBoxButtons.OK,
			//			MessageBoxIcon.Information);
			//	}
			//	else
			//	{
			//		if (Active_VisitTiming_SocialHistory == null)
			//			return;
			//		if (PEMRBusinessLogic.Update_VisitTiming_SocialHistory(this, Active_VisitTiming_SocialHistory))
			//			XtraMessageBox.Show("Saved Successfully", "Saved", MessageBoxButtons.OK,
			//				MessageBoxIcon.Information);
			//	}
		}

		private void btnSearch_Click(object sender, EventArgs e)
		{
			List<GetPreviousVisitTiming_VisionRefractionReading_Result> list =
				PEMRBusinessLogic.GetPrevious_VisitTiming_VisionRefractionReading(
					PEMRBusinessLogic.ActivePEMRObject.Active_Patient.ID, dtSearchFrom.EditValue,
					dtSearchTo.EditValue);
			if (list != null)
				list = list.OrderByDescending(item => item.TakenDateTime).ToList();
			grdControl.DataSource = list;
			grdControl.RefreshDataSource();
			SetToolTip(list);
		}

		private void btnPreviousReading_Click(object sender, EventArgs e)
		{
			dtSearchFrom.EditValue = null;
			dtSearchTo.EditValue = null;
			lytGroup_AllReadings.Visibility = LayoutVisibility.Always;
			lytGroup_AllReadings.Expanded = true;
			lytGroup_ReadingParent.Visibility = lytGroup_ReadingDetails.Visibility = lyt_Cancel.Visibility =
				emptySpaceItem4.Visibility =
					lyt_Add.Visibility = emptySpaceItem15.Visibility = LayoutVisibility.Never;

			List<GetPreviousVisitTiming_VisionRefractionReading_Result> list =
				PEMRBusinessLogic.GetPrevious_VisitTiming_VisionRefractionReading(
					PEMRBusinessLogic.ActivePEMRObject.Active_Patient.ID, dtSearchFrom.EditValue,
					dtSearchTo.EditValue);
			if (list != null)
				list = list.OrderByDescending(item => item.TakenDateTime).ToList();
			grdControl.DataSource = list;
			grdControl.RefreshDataSource();
			SetToolTip(list);
		}

		private void btnEdit_Click(object sender, EventArgs e)
		{

		}

		private void btnRemove_Click(object sender, EventArgs e)
		{

		}

		private void btnGraph_Click(object sender, EventArgs e)
		{
			PEMR_VisionRefractionGraph_UC graph = new PEMR_VisionRefractionGraph_UC();
			graph.Initialize(ReadingsType.VisionAndRefractionRading,
				PEMRBusinessLogic.ActivePEMRObject.Active_Patient.ID, dtSearchFrom.EditValue, dtSearchTo.EditValue);
			PopupBaseForm.ShowAsPopup(graph, this);
		}

		#endregion

		#region Grid Control Events

		private void gridView2_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
		{
			Selected_VisitTiming_VisionRefractionReading =
				CommonViewsActions.GetSelectedRowObject<GetPreviousVisitTiming_VisionRefractionReading_Result>(gridView2);
		}

		private void gridView2_DoubleClick(object sender, EventArgs e)
		{
			if (Selected_VisitTiming_VisionRefractionReading == null)
				return;

			PEMR_VisionRefractionDetails_UC details = new PEMR_VisionRefractionDetails_UC();
			details.Initialize(Selected_VisitTiming_VisionRefractionReading);
			ReadingsMode = ReadingsMode.ViewingPreviousReadings;
			PopupBaseForm.ShowAsPopup(details, this);
			ReadingsMode = ReadingsMode.ViewingActiveAllReadings;
		}

		#endregion

		#endregion

		#region IPEMR_VisionRefractionReading

		public object TakenDate
		{
			get { return takenDate.EditValue; }
			set { takenDate.EditValue = value; }
		}

		public object TakenTime
		{
			get { return takesTime.EditValue; }
			set { takesTime.EditValue = value; }
		}

		public object VisionRefractionReadingTypeID
		{
			get
			{
				if (chkAutoRef.Checked)
					return (int)DB_VisionRefractionReadingType.AutoRef;
				if (chkAutoRefAfterCyclo.Checked)
					return (int)DB_VisionRefractionReadingType.AutoRefAfterCyclo;
				if (chkOldGlasses.Checked)
					return (int)DB_VisionRefractionReadingType.AutoRef;
				if (chkOldGlassesNear.Checked)
					return (int)DB_VisionRefractionReadingType.OldGlassesNear;
				if (chkSubjectivePrescription.Checked)
					return (int)DB_VisionRefractionReadingType.SubjectiveRefraction;
				return DB_VisionRefractionReadingType.None;
			}
			set
			{
				if (value == null)
					return;
				DB_VisionRefractionReadingType readingType = (DB_VisionRefractionReadingType)value;
				switch (readingType)
				{
					case DB_VisionRefractionReadingType.AutoRef:
						chkAutoRef.Checked = true;
						break;
					case DB_VisionRefractionReadingType.AutoRefAfterCyclo:
						chkAutoRefAfterCyclo.Checked = true;
						break;
					case DB_VisionRefractionReadingType.OldGlasses:
						chkOldGlasses.Checked = true;
						break;
					case DB_VisionRefractionReadingType.OldGlassesNear:
						chkOldGlassesNear.Checked = true;
						break;
					case DB_VisionRefractionReadingType.SubjectiveRefraction:
						chkSubjectivePrescription.Checked = true;
						break;
				}
			}
		}

		public object UVA_OU
		{
			get { return lkeUVA_OU.EditValue; }
			set { lkeUVA_OU.EditValue = value; }
		}

		public object NVA_OU
		{
			get { return lkeNVA_OU.EditValue; }
			set { lkeNVA_OU.EditValue = value; }
		}

		public object NVAAmount_OU
		{
			get { return spnNVAAmount_OU.EditValue; }
			set { spnNVAAmount_OU.EditValue = value; }
		}

		public object Distance_OD
		{
			get { return spnDistance_OD.EditValue; }
			set { spnDistance_OD.EditValue = value; }
		}

		public object Distance_OS
		{
			get { return spnDistance_OS.EditValue; }
			set { spnDistance_OS.EditValue = value; }
		}

		public object NVA_OD
		{
			get { return lkeNVA_OD.EditValue; }
			set { lkeNVA_OD.EditValue = value; }
		}

		public object NVA_OS
		{
			get { return lkeNVA_OS.EditValue; }
			set { lkeNVA_OS.EditValue = value; }
		}

		public object NVAAmount_OD
		{
			get { return spnNVAAmount_OD.EditValue; }
			set { spnNVAAmount_OD.EditValue = value; }
		}

		public object NVAAmount_OS
		{
			get { return spnNVAAmount_OS.EditValue; }
			set { spnNVAAmount_OS.EditValue = value; }
		}

		public object IsIgnored_OD
		{
			get { return chkIgnore_OD.Checked; }
			set { chkIgnore_OD.Checked = Convert.ToBoolean(value); }
		}

		public object IsIgnored_OS
		{
			get { return chkIgnore_OS.Checked; }
			set { chkIgnore_OS.Checked = Convert.ToBoolean(value); }
		}

		public object IsError_OD
		{
			get { return chkError_OD.Checked; }
			set { chkError_OD.Checked = Convert.ToBoolean(value); }
		}

		public object IsError_OS
		{
			get { return chkError_OS.Checked; }
			set { chkError_OS.Checked = Convert.ToBoolean(value); }
		}

		public object RatingAmount_OD
		{
			get { return rating_OD.Rating; }
			set
			{
				if (value == null)
					return;
				rating_OD.Rating = Convert.ToInt32(value);
			}
		}

		public object RatingAmount_OS
		{
			get { return rating_OS.Rating; }
			set
			{
				if (value == null)
					return;
				rating_OS.Rating = Convert.ToInt32(value);
			}
		}

		public object SphereAmount_OD
		{
			get { return spnSPH_OD.EditValue; }
			set { spnSPH_OD.EditValue = value; }
		}

		public object SphereAmount_OS
		{
			get { return spnSPH_OS.EditValue; }
			set { spnSPH_OS.EditValue = value; }
		}

		public object CylinderAmount_OD
		{
			get { return spnCYL_OD.EditValue; }
			set { spnCYL_OD.EditValue = value; }
		}

		public object CylinderAmount_OS
		{
			get { return spnCYL_OS.EditValue; }
			set { spnCYL_OS.EditValue = value; }
		}

		public object AxisAmount_OD
		{
			get { return spnAxis_OD.EditValue; }
			set { spnAxis_OD.EditValue = value; }
		}

		public object AxisAmount_OS
		{
			get { return spnAxis_OS.EditValue; }
			set { spnAxis_OS.EditValue = value; }
		}

		public object UVA_OD
		{
			get { return lkeUVA_OD.EditValue; }
			set { lkeUVA_OD.EditValue = value; }
		}

		public object UVA_OS
		{
			get { return lkeUVA_OS.EditValue; }
			set { lkeUVA_OS.EditValue = value; }
		}

		public object Add_OD
		{
			get { return spnAdd_OD.EditValue; }
			set { spnAdd_OD.EditValue = value; }
		}

		public object Add_OS
		{
			get { return spnAdd_OS.EditValue; }
			set { spnAdd_OS.EditValue = value; }
		}

		public object Remarks_OD
		{
			get { return txtRemarks_OD.EditValue; }
			set { txtRemarks_OD.EditValue = value; }
		}

		public object Remarks_OS
		{
			get { return txtRemarks_OS.EditValue; }
			set { txtRemarks_OS.EditValue = value; }
		}

		#endregion

	}
}
