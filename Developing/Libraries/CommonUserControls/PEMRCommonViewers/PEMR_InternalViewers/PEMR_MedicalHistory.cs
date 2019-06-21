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

namespace CommonUserControls.PEMRCommonViewers.PEMR_InternalViewers
{
	public partial class PEMR_MedicalHistory : UserControl, IPEMR_Viewer, IPEMR_MedicalHistory
	{
		private VisitTiming_MedicalHistory Active_VisitTiming_MedicalHistory { get; set; }

		public PEMR_MedicalHistory()
		{
			InitializeComponent();
		}

		public void Initialize()
		{
			lytGroup_Diabetes.Expanded = false;
			lytGroup_Hypertension.Expanded = false;
			ClearControls(true);
			FillControls();
		}

		#region Implementation of IPEMR_Viewer

		public void ClearControls(bool clearAll)
		{
			HasDiabetes = null;
			DiabetesType = null;
			HbA1C = null;
			IsDiabetesControlled = null;
			DiabetesMedicationType = null;
			DiabetesMedicationDuration = null;
			DiabetesMedicationDurationType = null;
			DiabetesMedication = null;
			DiabetesDosage = null;
			IsHypertension = null;
			IsHypertensionControlled = null;
			HypertensionMedicationDurationType = null;
			HypertensionMedicationDuration = null;
			HypertensionMedication = null;
			HypertensionDosage = null;
			HasDrugAllergies = null;
			TriggersDrugAllergies = null;
			HasHepatitis = null;
			HasAsthma = null;
		}

		public void FillControls()
		{
			CommonViewsActions.FillGridlookupEdit(lkeDiabetes_Medications, Medication_cu.ItemsList);
			CommonViewsActions.FillGridlookupEdit(lkeDiabetes_Doses, Dose_cu.ItemsList);
			CommonViewsActions.FillGridlookupEdit(lkeHypertension_Medications, Medication_cu.ItemsList);
			CommonViewsActions.FillGridlookupEdit(lkeHypertension_Doses, Dose_cu.ItemsList);
		}

		#endregion

		#region Controls Events

		#region CheckEdit Events

		#region Check Yes

		private void chkDiabetes_Yes_CheckedChanged(object sender, EventArgs e)
		{
			lytGroup_DiabetesDetails.Visibility = HasDiabetes != null && Convert.ToBoolean(HasDiabetes)
				? LayoutVisibility.Always
				: LayoutVisibility.Never;
			emptySpaceItem6.Visibility = HasDiabetes != null && Convert.ToBoolean(HasDiabetes)
				? LayoutVisibility.Always
				: LayoutVisibility.Never;
			lytGroup_Diabetes.CaptionImage = HasDiabetes != null && Convert.ToBoolean(HasDiabetes)
				? Properties.Resources.AcceptIcon_16_02
				: Properties.Resources.RoundedPoint_6_01;
			if (HasDiabetes == null || !Convert.ToBoolean(HasDiabetes))
			{
				DiabetesType = null;
				HbA1C = null;
				IsDiabetesControlled = null;
				DiabetesMedicationType = null;
				DiabetesMedicationDuration = null;
				DiabetesMedicationDurationType = null;
				DiabetesMedication = null;
				DiabetesDosage = null;
			}
		}

		private void chkHypertension_Yes_CheckedChanged(object sender, EventArgs e)
		{
			lytGroup_HypertensionDetails.Visibility = IsHypertension != null && Convert.ToBoolean(IsHypertension)
				? LayoutVisibility.Always
				: LayoutVisibility.Never;
			emptySpaceItem13.Visibility = IsHypertension != null && Convert.ToBoolean(IsHypertension)
				? LayoutVisibility.Always
				: LayoutVisibility.Never;
			lytGroup_Hypertension.CaptionImage = IsHypertension != null && Convert.ToBoolean(IsHypertension)
				? Properties.Resources.AcceptIcon_16_02
				: Properties.Resources.RoundedPoint_6_01;
			if (IsHypertension == null || !Convert.ToBoolean(IsHypertension))
			{
				IsHypertensionControlled = null;
				HypertensionMedicationDurationType = null;
				HypertensionMedicationDuration = null;
				HypertensionMedication = null;
				HypertensionDosage = null;
			}
		}

		private void chkDrugAllergies_Yes_CheckedChanged(object sender, EventArgs e)
		{
			lytGroup_TriggersOfDrugAllergies.Visibility = HasDrugAllergies != null && Convert.ToBoolean(HasDrugAllergies)
				? LayoutVisibility.Always
				: LayoutVisibility.Never;
			lytGroup_Allergies.CaptionImage = HasDrugAllergies != null && Convert.ToBoolean(HasDrugAllergies)
				? Properties.Resources.AcceptIcon_16_02
				: Properties.Resources.RoundedPoint_6_01;
			if (HasDrugAllergies == null || !Convert.ToBoolean(HasDrugAllergies))
				TriggersDrugAllergies = null;
		}

		private void chkHepatitis_Yes_CheckedChanged(object sender, EventArgs e)
		{
			lytGroup_Hepatitis.CaptionImage = HasHepatitis != null && Convert.ToBoolean(HasHepatitis)
				? Properties.Resources.AcceptIcon_16_02
				: Properties.Resources.RoundedPoint_6_01;
		}

		private void chkAsthma_Yes_CheckedChanged(object sender, EventArgs e)
		{
			lytGroup_Asthma.CaptionImage = HasAsthma != null && Convert.ToBoolean(HasAsthma)
				? Properties.Resources.AcceptIcon_16_02
				: Properties.Resources.RoundedPoint_6_01;
		}

		#endregion

		#region Check No

		private void chkDiabetes_No_CheckedChanged(object sender, EventArgs e)
		{
			lytGroup_DiabetesDetails.Visibility = HasDiabetes != null && Convert.ToBoolean(HasDiabetes)
				? LayoutVisibility.Always
				: LayoutVisibility.Never;
			emptySpaceItem6.Visibility = HasDiabetes != null && Convert.ToBoolean(HasDiabetes)
				? LayoutVisibility.Always
				: LayoutVisibility.Never;
			lytGroup_Diabetes.CaptionImage = HasDiabetes != null && !Convert.ToBoolean(HasDiabetes)
				? Properties.Resources.RejectIcon_16_01
				: Properties.Resources.RoundedPoint_6_01;
			if (HasDiabetes == null || !Convert.ToBoolean(HasDiabetes))
			{
				DiabetesType = null;
				HbA1C = null;
				IsDiabetesControlled = null;
				DiabetesMedicationType = null;
				DiabetesMedicationDuration = null;
				DiabetesMedicationDurationType = null;
				DiabetesMedication = null;
				DiabetesDosage = null;
			}
		}

		private void chkHypertension_No_CheckedChanged(object sender, EventArgs e)
		{
			lytGroup_HypertensionDetails.Visibility = HasDiabetes != null && Convert.ToBoolean(HasDiabetes)
				? LayoutVisibility.Always
				: LayoutVisibility.Never;
			emptySpaceItem13.Visibility = HasDiabetes != null && Convert.ToBoolean(HasDiabetes)
				? LayoutVisibility.Always
				: LayoutVisibility.Never;
			lytGroup_Hypertension.CaptionImage = HasDiabetes != null && !Convert.ToBoolean(HasDiabetes)
				? Properties.Resources.RejectIcon_16_01
				: Properties.Resources.RoundedPoint_6_01;
			if (IsHypertension == null || !Convert.ToBoolean(IsHypertension))
			{
				IsHypertensionControlled = null;
				HypertensionMedicationDurationType = null;
				HypertensionMedicationDuration = null;
				HypertensionMedication = null;
				HypertensionDosage = null;
			}
		}

		private void chkDrugAllergies_No_CheckedChanged(object sender, EventArgs e)
		{
			lytGroup_TriggersOfDrugAllergies.Visibility = HasDrugAllergies != null && Convert.ToBoolean(HasDrugAllergies)
				? LayoutVisibility.Always
				: LayoutVisibility.Never;
			lytGroup_Hypertension.CaptionImage = HasDrugAllergies != null && !Convert.ToBoolean(HasDrugAllergies)
				? Properties.Resources.RejectIcon_16_01
				: Properties.Resources.RoundedPoint_6_01;
			if (HasDrugAllergies == null || !Convert.ToBoolean(HasDrugAllergies))
				TriggersDrugAllergies = null;
		}

		private void chkHepatitis_No_CheckedChanged(object sender, EventArgs e)
		{
			lytGroup_Hepatitis.CaptionImage = HasHepatitis != null && !Convert.ToBoolean(HasHepatitis)
				? Properties.Resources.AcceptIcon_16_02
				: Properties.Resources.RoundedPoint_6_01;
		}

		private void chkAsthma_No_CheckedChanged(object sender, EventArgs e)
		{
			lytGroup_Asthma.CaptionImage = HasAsthma != null && !Convert.ToBoolean(HasAsthma)
				? Properties.Resources.AcceptIcon_16_02
				: Properties.Resources.RoundedPoint_6_01;
		}

		#endregion

		#endregion

		#region Layout Events

		private void lytGroup_Diabetes_MouseUp(object sender, MouseEventArgs e)
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
					if (lytGroup_Hypertension.Expanded)
						lytGroup_Hypertension.Expanded = false;
					if (lytGroup_Allergies.Expanded)
						lytGroup_Allergies.Expanded = false;
					if (lytGroup_Hepatitis.Expanded)
						lytGroup_Hepatitis.Expanded = false;
					if (lytGroup_Asthma.Expanded)
						lytGroup_Asthma.Expanded = false;
				}
			}
		}

		private void lytGroup_Hypertension_MouseUp(object sender, MouseEventArgs e)
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
					if (lytGroup_Diabetes.Expanded)
						lytGroup_Diabetes.Expanded = false;
					if (lytGroup_Allergies.Expanded)
						lytGroup_Allergies.Expanded = false;
					if (lytGroup_Hepatitis.Expanded)
						lytGroup_Hepatitis.Expanded = false;
					if (lytGroup_Asthma.Expanded)
						lytGroup_Asthma.Expanded = false;
				}
			}
		}

		private void lytGroup_Allergies_MouseUp(object sender, MouseEventArgs e)
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
					lytGroup_Hepatitis.Expanded = group.Expanded;
					lytGroup_Asthma.Expanded = group.Expanded;

					if (lytGroup_Diabetes.Expanded)
						lytGroup_Diabetes.Expanded = false;
					if (lytGroup_Hypertension.Expanded)
						lytGroup_Hypertension.Expanded = false;
				}
			}
		}

		private void lytGroup_Hepatitis_MouseUp(object sender, MouseEventArgs e)
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
					lytGroup_Hepatitis.Expanded = group.Expanded;
					lytGroup_Asthma.Expanded = group.Expanded;

					if (lytGroup_Diabetes.Expanded)
						lytGroup_Diabetes.Expanded = false;
					if (lytGroup_Hypertension.Expanded)
						lytGroup_Hypertension.Expanded = false;
				}
			}
		}

		private void lytGroup_Asthma_MouseUp(object sender, MouseEventArgs e)
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
					lytGroup_Hepatitis.Expanded = group.Expanded;
					lytGroup_Asthma.Expanded = group.Expanded;

					if (lytGroup_Diabetes.Expanded)
						lytGroup_Diabetes.Expanded = false;
					if (lytGroup_Hypertension.Expanded)
						lytGroup_Hypertension.Expanded = false;
				}
			}
		}

		#endregion

		#region Button Events

		private void btnSave_Click(object sender, EventArgs e)
		{
			if (PEMRBusinessLogic.ActivePEMRObject != null)
				if (PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_MedicalHistory == null)
				{
					Active_VisitTiming_MedicalHistory = PEMRBusinessLogic.CreateNew_VisitTiming_MedicalHistory(this, DB_PEMRSavingMode.SaveImmediately);
					if (Active_VisitTiming_MedicalHistory == null)
						return;
					if (PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_MedicalHistory == null)
						PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_MedicalHistory = new List<VisitTiming_MedicalHistory>();
					PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_MedicalHistory.Add(Active_VisitTiming_MedicalHistory);
					XtraMessageBox.Show("Saved Successfully", "Saved", MessageBoxButtons.OK,
						MessageBoxIcon.Information);
				}
				else
				{
					if (Active_VisitTiming_MedicalHistory == null)
						return;
					if(PEMRBusinessLogic.Update_VisitTiming_MedicalHistory(this, Active_VisitTiming_MedicalHistory))
						XtraMessageBox.Show("Saved Successfully", "Saved", MessageBoxButtons.OK,
							MessageBoxIcon.Information);
				}
		}

		#endregion

		#endregion

		#region Implementation of IPEMR_MedicalHistory

		public object FurtherDetails
		{
			get { return txtTreatmentPlanDetails.EditValue; }
			set { txtTreatmentPlanDetails.EditValue = value; }
		}

		public object HasDiabetes
		{
			get
			{
				if (chkDiabetes_Yes.Checked)
					return true;
				if (chkDiabetes_No.Checked)
					return false;
				return null;
			}
			set
			{
				if (value == null)
				{
					chkDiabetes_Yes.Checked = false;
					chkDiabetes_No.Checked = false;
					return;
				}

				if (Convert.ToBoolean(value))
					chkDiabetes_Yes.Checked = true;
				else
					chkDiabetes_No.Checked = true;
			}
		}

		public object DiabetesType
		{
			get
			{
				if (chkDiabetes_Type1.Checked)
					return (int) DB_DiabetesType.Type1;
				if (chkDiabetes_Type2.Checked)
					return (int)DB_DiabetesType.Type2;
				return null;
			}
			set
			{
				if (value == null)
				{
					chkDiabetes_Type1.Checked = false;
					chkDiabetes_Type2.Checked = false;
					return;
				}
				DB_DiabetesType type = (DB_DiabetesType) value;
				switch (type)
				{
					case DB_DiabetesType.Type1:
						chkDiabetes_Type1.Checked = true;
						break;
					case DB_DiabetesType.Type2:
						chkDiabetes_Type2.Checked = true;
						break;
				}
			}
		}

		public object HbA1C
		{
			get { return spnHbA1C.EditValue; }
			set { spnHbA1C.EditValue = value; }
		}

		public object IsDiabetesControlled
		{
			get
			{
				if (chkDiabetesControlled_Yes.Checked)
					return true;
				if (chkDiabetesControlled_No.Checked)
					return true;
				return null;
			}
			set
			{
				if (value == null)
				{
					chkDiabetesControlled_Yes.Checked = false;
					chkDiabetesControlled_No.Checked = false;
					return;
				}
				if(Convert.ToBoolean(value))
					chkDiabetesControlled_Yes.Checked = true;
				else
					chkDiabetesControlled_No.Checked = true;
			}
		}

		public object DiabetesMedicationType
		{
			get
			{
				if (chkDiabetesMedication_Tables.Checked)
					return (int)DB_DiabetedMedication.Tablets;
				if (chkDiabetesMedication_Insulin.Checked)
					return (int)DB_DiabetedMedication.Insulin;
				if (chkDiabetesMedication_Both.Checked)
					return (int)DB_DiabetedMedication.Both;
				return null;
			}
			set
			{
				if (value == null)
				{
					chkDiabetesMedication_Tables.Checked = false;
					chkDiabetesMedication_Insulin.Checked = false;
					chkDiabetesMedication_Both.Checked = false;
					return;
				}
				DB_DiabetedMedication type = (DB_DiabetedMedication)value;
				switch (type)
				{
					case DB_DiabetedMedication.Tablets:
						chkDiabetesMedication_Tables.Checked = true;
						break;
					case DB_DiabetedMedication.Insulin:
						chkDiabetesMedication_Insulin.Checked = true;
						break;
					case DB_DiabetedMedication.Both:
						chkDiabetesMedication_Both.Checked = true;
						break;
				}
			}
		}

		public object DiabetesMedicationDuration
		{
			get { return spnDiabetes_Duration.EditValue; }
			set { spnDiabetes_Duration.EditValue = value; }
		}

		public object DiabetesMedicationDurationType
		{
			get
			{
				if (spnDiabetes_Duration.EditValue == null)
					return null;
				if (chkDiabetes_Hourly.Checked)
					return (int)DB_TimeDuration.Hourly;
				if (chkDiabetes_Daily.Checked)
					return (int)DB_TimeDuration.Daily;
				if (chkDiabetes_Weekly.Checked)
					return (int)DB_TimeDuration.Weekly;
				if (chkDiabetes_Monthly.Checked)
					return (int)DB_TimeDuration.Monthly;
				if (chkDiabetes_Yearly.Checked)
					return (int)DB_TimeDuration.Yearly;
				return null;
			}
			set
			{
				if (value == null)
				{
					chkDiabetes_Hourly.Checked = false;
					chkDiabetes_Daily.Checked = false;
					chkDiabetes_Weekly.Checked = false;
					chkDiabetes_Monthly.Checked = false;
					chkDiabetes_Yearly.Checked = false;
					return;
				}
				DB_TimeDuration type = (DB_TimeDuration)value;
				switch (type)
				{
					case DB_TimeDuration.Hourly:
						chkDiabetes_Hourly.Checked = true;
						break;
					case DB_TimeDuration.Daily:
						chkDiabetes_Daily.Checked = true;
						break;
					case DB_TimeDuration.Weekly:
						chkDiabetes_Weekly.Checked = true;
						break;
					case DB_TimeDuration.Monthly:
						chkDiabetes_Monthly.Checked = true;
						break;
					case DB_TimeDuration.Yearly:
						chkDiabetes_Yearly.Checked = true;
						break;
				}
			}
		}

		public object DiabetesMedication
		{
			get { return lkeDiabetes_Medications.EditValue; }
			set { lkeDiabetes_Medications.EditValue = value; }
		}

		public object DiabetesDosage
		{
			get { return lkeDiabetes_Doses.EditValue; }
			set { lkeDiabetes_Doses.EditValue = value; }
		}

		public object IsHypertension
		{
			get
			{
				if (chkHypertension_Yes.Checked)
					return true;
				if (chkHypertension_No.Checked)
					return false;
				return null;
			}
			set
			{
				if (value == null)
				{
					chkHypertension_Yes.Checked = false;
					chkHypertension_No.Checked = false;
					return;
				}

				if (Convert.ToBoolean(value))
					chkHypertension_Yes.Checked = true;
				else
					chkHypertension_No.Checked = true;
			}
		}

		public object IsHypertensionControlled
		{
			get
			{
				if (chkHypertensionControlled_Yes.Checked)
					return true;
				if (chkHypertensionControlled_No.Checked)
					return true;
				return null;
			}
			set
			{
				if (value == null)
				{
					chkHypertensionControlled_Yes.Checked = false;
					chkHypertensionControlled_No.Checked = false;
					return;
				}
				if (Convert.ToBoolean(value))
					chkHypertensionControlled_Yes.Checked = true;
				else
					chkHypertensionControlled_No.Checked = true;
			}
		}

		public object HypertensionMedicationDurationType
		{
			get
			{
				if (spnHypertension_Duration.EditValue == null)
					return null;
				if (chkHypertension_Hours.Checked)
					return (int)DB_TimeDuration.Hourly;
				if (chkHypertension_Days.Checked)
					return (int)DB_TimeDuration.Daily;
				if (chkHypertension_Weeks.Checked)
					return (int)DB_TimeDuration.Weekly;
				if (chkHypertension_Months.Checked)
					return (int)DB_TimeDuration.Monthly;
				if (chkHypertension_Years.Checked)
					return (int)DB_TimeDuration.Yearly;
				return null;
			}
			set
			{
				if (value == null)
				{
					chkHypertension_Hours.Checked = false;
					chkHypertension_Days.Checked = false;
					chkHypertension_Weeks.Checked = false;
					chkHypertension_Months.Checked = false;
					chkHypertension_Years.Checked = false;
					return;
				}
				DB_TimeDuration type = (DB_TimeDuration)value;
				switch (type)
				{
					case DB_TimeDuration.Hourly:
						chkHypertension_Hours.Checked = true;
						break;
					case DB_TimeDuration.Daily:
						chkHypertension_Days.Checked = true;
						break;
					case DB_TimeDuration.Weekly:
						chkHypertension_Weeks.Checked = true;
						break;
					case DB_TimeDuration.Monthly:
						chkHypertension_Months.Checked = true;
						break;
					case DB_TimeDuration.Yearly:
						chkHypertension_Years.Checked = true;
						break;
				}
			}
		}

		public object HypertensionMedicationDuration
		{
			get { return spnHypertension_Duration.EditValue; }
			set { spnHypertension_Duration.EditValue = value; }
		}

		public object HypertensionMedication
		{
			get { return lkeHypertension_Medications.EditValue; }
			set { lkeHypertension_Medications.EditValue = value; }
		}

		public object HypertensionDosage
		{
			get { return lkeHypertension_Doses.EditValue; }
			set { lkeHypertension_Doses.EditValue = value; }
		}

		public object HasDrugAllergies
		{
			get
			{
				if (chkDrugAllergies_Yes.Checked)
					return true;
				if (chkDrugAllergies_No.Checked)
					return false;
				return null;
			}
			set
			{
				if (value == null)
				{
					chkDrugAllergies_Yes.Checked = false;
					chkDrugAllergies_No.Checked = false;
					return;
				}

				if (Convert.ToBoolean(value))
					chkDrugAllergies_Yes.Checked = true;
				else
					chkDrugAllergies_No.Checked = true;
			}
		}

		public object TriggersDrugAllergies
		{
			get { return lkeTriggersOfDrugAllergies.EditValue; }
			set { lkeTriggersOfDrugAllergies.EditValue = value; }
		}

		public object HasHepatitis
		{
			get
			{
				if (chkHepatitis_Yes.Checked)
					return true;
				if (chkHepatitis_No.Checked)
					return false;
				return null;
			}
			set
			{
				if (value == null)
				{
					chkHepatitis_Yes.Checked = false;
					chkHepatitis_No.Checked = false;
					return;
				}

				if (Convert.ToBoolean(value))
					chkHepatitis_Yes.Checked = true;
				else
					chkHepatitis_No.Checked = true;
			}
		}

		public object HasAsthma
		{
			get
			{
				if (chkAsthma_Yes.Checked)
					return true;
				if (chkAsthma_No.Checked)
					return false;
				return null;
			}
			set
			{
				if (value == null)
				{
					chkAsthma_Yes.Checked = false;
					chkAsthma_No.Checked = false;
					return;
				}

				if (Convert.ToBoolean(value))
					chkAsthma_Yes.Checked = true;
				else
					chkAsthma_No.Checked = true;
			}
		}

		#endregion

	}
}
