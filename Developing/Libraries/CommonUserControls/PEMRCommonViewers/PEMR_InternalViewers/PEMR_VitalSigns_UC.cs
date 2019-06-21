using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ApplicationConfiguration;
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
	public partial class PEMR_VitalSigns_UC : UserControl, IPEMR_VitalSign, IPEMR_Viewer
	{
		public PEMR_VitalSigns_UC()
		{
			InitializeComponent();
			CommonViewsActions.SetupGridControl(grdControl, Resources.LocalizedRes.grd_VisitTiming_VitalSigns, true);
		}

		private void PEMR_VitalSigns_UC_Load(object sender, EventArgs e)
		{
			ClearControls(true);
			txtGeneralDescription.EnterMoveNextControl = false;
		}

		public void Initialize()
		{

		}

		public void ClearControls(bool clearAll)
		{
			if (clearAll)
				grdControl.DataSource = null;

			lytNewVitalSigns.Visibility = LayoutVisibility.Never;
			lytNewVitalSigns.Expanded = false;
			lytVitalSignsGrid.Visibility = LayoutVisibility.Always;
			lytVitalSignsGrid.Expanded = true;

			takenDate.DateTime = DateTime.Now.Date;
			takesTime.EditValue = DateTime.Now;
			txtGeneralDescription.EditValue = null;
			lytGroup_Weight.Expanded = false;
			chkWeightKGS.Checked = true;
			chkWeightLBS.Checked = false;
			spnWeight.EditValue = null;
			chkHeightCM.Checked = true;
			chkheightInch.Checked = false;
			chkHeightFeet.Checked = false;
			spnHeight.EditValue = null;
			txtWeightDescription.EditValue = null;
			chkWeightDescription.Checked = false;
			lytWeightDescription.Visibility = LayoutVisibility.Never;

			lytGroup_Temperature.Expanded = false;
			chkTemperatureCelesius.Checked = true;
			chkTemperatureFehrenhite.Checked = false;
			spnTemperature.EditValue = null;
			txtTemperatureDescription.EditValue = null;
			chkTemperatureDescription.Checked = false;
			lytTemperatureDescription.Visibility = LayoutVisibility.Never;

			lytGroup_BloodPressure.Expanded = false;
			spnBloodPressureHigh.EditValue = null;
			spnBloodPressureLow.EditValue = null;
			spnPulse.EditValue = null;
			lkePulseReg.EditValue = null;
			txtBloodPressureDescription.EditValue = null;
			chkBloodPressureDescription.Checked = false;
			lytBloodPressureDescription.Visibility = LayoutVisibility.Never;

			lytGroup_Respiration.Expanded = false;
			spnRespiration.EditValue = null;
			spnOxygen.EditValue = null;
			lkeFIO.EditValue = null;
			spnSPO.EditValue = null;
			chkRespirationDescription.Checked = false;
			lytResiprationDescription.Visibility = LayoutVisibility.Never;
		}

		public void FillControls()
		{
		}

		#region Layout Events

		private void lytGroup_Weight_MouseUp(object sender, MouseEventArgs e)
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
					if (lytGroup_Temperature.Expanded)
						lytGroup_Temperature.Expanded = false;
					if (lytGroup_BloodPressure.Expanded)
						lytGroup_BloodPressure.Expanded = false;
					if (lytGroup_Respiration.Expanded)
						lytGroup_Respiration.Expanded = false;
				}
			}
		}

		private void lytGroup_Temperature_MouseUp(object sender, MouseEventArgs e)
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
					if (lytGroup_Weight.Expanded)
						lytGroup_Weight.Expanded = false;
					if (lytGroup_BloodPressure.Expanded)
						lytGroup_BloodPressure.Expanded = false;
					if (lytGroup_Respiration.Expanded)
						lytGroup_Respiration.Expanded = false;
				}
			}
		}

		private void lytGroup_BloodPressure_MouseUp(object sender, MouseEventArgs e)
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
					if (lytGroup_Weight.Expanded)
						lytGroup_Weight.Expanded = false;
					if (lytGroup_Temperature.Expanded)
						lytGroup_Temperature.Expanded = false;
					if (lytGroup_Respiration.Expanded)
						lytGroup_Respiration.Expanded = false;
				}
			}
		}

		private void lytGroup_Respiration_MouseUp(object sender, MouseEventArgs e)
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
					if (lytGroup_Weight.Expanded)
						lytGroup_Weight.Expanded = false;
					if (lytGroup_Temperature.Expanded)
						lytGroup_Temperature.Expanded = false;
					if (lytGroup_BloodPressure.Expanded)
						lytGroup_BloodPressure.Expanded = false;
				}
			}
		}

		#endregion

		#region Button Events

		private void btnNewVitalSign_Click(object sender, EventArgs e)
		{
			if (IsThereNotSavedReadings())
			{
				DialogResult result = XtraMessageBox.Show(
					"There are not saved data. Do you want to clear all records and create new record ? ", "Notice",
					MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
				switch (result)
				{
					case DialogResult.Yes:
						ClearControls(false);
						break;
				}
			}
			lytNewVitalSigns.Visibility = LayoutVisibility.Always;
			lytNewVitalSigns.Expanded = true;
			lytVitalSignsGrid.Visibility = LayoutVisibility.Never;
			lytVitalSignsGrid.Expanded = false;
		}

		private void btnActiveVitalSign_Click(object sender, EventArgs e)
		{
			lytNewVitalSigns.Visibility = LayoutVisibility.Never;
			lytNewVitalSigns.Expanded = false;
			lytVitalSignsGrid.Visibility = LayoutVisibility.Always;
			lytVitalSignsGrid.Expanded = true;
			grdControl.DataSource = PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_VitalSign;
			if (!IsThereNotSavedReadings()) 
				ClearControls(false);
		}

		private void btnAllActiveSign_Click(object sender, EventArgs e)
		{
			lytNewVitalSigns.Visibility = LayoutVisibility.Never;
			lytNewVitalSigns.Expanded = false;
			lytVitalSignsGrid.Visibility = LayoutVisibility.Always;
			lytVitalSignsGrid.Expanded = true;
			grdControl.DataSource = PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_VitalSign;
			if (!IsThereNotSavedReadings())
				ClearControls(false);
		}

		private void btnAdd_Click(object sender, EventArgs e)
		{
			DialogResult result = XtraMessageBox.Show("Do you to add this Readings ?", "Adding", MessageBoxButtons.YesNo,
				MessageBoxIcon.Exclamation);

			switch (result)
			{
				case DialogResult.Yes:
					VisitTiming_VitalSign visitTiming_VitalSign =
						PEMRBusinessLogic.CreateNew_VisitTiming_VitalSign(this,
							ApplicationStaticConfiguration.PEMRSavingMode);
					if (visitTiming_VitalSign != null)
					{
						if (PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_VitalSign == null)
							PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_VitalSign =
								new List<VisitTiming_VitalSign>();
						PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_VitalSign.Add(visitTiming_VitalSign);
					}

					btnActiveVitalSign_Click(null, null);
					ClearControls(false);
					break;
			}
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			XtraMessageBox.Show("Saved Successfully", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
			ClearControls(false);
		}

		#endregion

		#region Check Events

		private void chkWeightDescription_CheckedChanged(object sender, EventArgs e)
		{
			lytWeightDescription.Visibility =
				chkWeightDescription.Checked ? LayoutVisibility.Always : LayoutVisibility.Never;
		}

		private void chkTemperatureDescription_CheckedChanged(object sender, EventArgs e)
		{
			lytTemperatureDescription.Visibility =
				chkTemperatureDescription.Checked ? LayoutVisibility.Always : LayoutVisibility.Never;
		}

		private void chkBloodPressureDescription_CheckedChanged(object sender, EventArgs e)
		{
			lytBloodPressureDescription.Visibility =
				chkBloodPressureDescription.Checked ? LayoutVisibility.Always : LayoutVisibility.Never;
		}

		private void chkRespirationDescription_CheckedChanged(object sender, EventArgs e)
		{
			lytResiprationDescription.Visibility =
				chkRespirationDescription.Checked ? LayoutVisibility.Always : LayoutVisibility.Never;
		}

		#endregion

		public bool IsThereNotSavedReadings()
		{
			return GeneralDescription != null || Weight_Amount != null ||
			       HeightAmount != null || Weight_Description != null ||
			       Temperature_Amount != null || Temperature_Description != null || BloodPressure_AmountHigh != null ||
			       BloodPressure_AmountLow != null || Pulse_Amount != null || Pulse_Reg != null ||
			       BloodPressure_Description != null || Respiration_Amount != null || Oxygen_Amount != null ||
			       FIO2 != null || SPO2_Amount != null;
		}

		#region Implementation of IPEMR_VitalSign

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

		public object GeneralDescription
		{
			get { return txtGeneralDescription.EditValue; }
			set { txtGeneralDescription.EditValue = value; }
		}

		public object Weight_Unit
		{
			get
			{
				if (chkWeightKGS.Checked)
					return (int) DB_WeightUnit.KG;
				if(chkWeightLBS.Checked)
					return (int)DB_WeightUnit.LBs;
				return null;
			}
			set
			{
				DB_WeightUnit weight = (DB_WeightUnit) value;
				switch (weight)
				{
					case DB_WeightUnit.KG:
						chkWeightKGS.Checked = true;
						break;
					case DB_WeightUnit.LBs:
						chkWeightLBS.Checked = true;
						break;
				}
			}
		}

		public object Weight_Amount
		{
			get { return spnWeight.EditValue; }
			set { spnWeight.EditValue = value; }
		}

		public object Height_Unit
		{
			get
			{
				if (chkHeightCM.Checked)
					return (int)DB_HeightUnit.CM;
				if (chkheightInch.Checked)
					return (int)DB_HeightUnit.Inch;
				if (chkHeightFeet.Checked)
					return (int)DB_HeightUnit.Feet;
				return null;
			}
			set
			{
				DB_HeightUnit height = (DB_HeightUnit)value;
				switch (height)
				{
					case DB_HeightUnit.CM:
						chkHeightCM.Checked = true;
						break;
					case DB_HeightUnit.Inch:
						chkheightInch.Checked = true;
						break;
					case DB_HeightUnit.Feet:
						chkHeightFeet.Checked = true;
						break;
				}
			}
		}

		public object HeightAmount
		{
			get { return spnHeight.EditValue; }
			set { spnHeight.EditValue = value; }
		}

		public object Weight_Description
		{
			get { return txtWeightDescription.EditValue; }
			set { txtWeightDescription.EditValue = value; }
		}

		public object Temperature_Unit
		{
			get
			{
				if (chkTemperatureCelesius.Checked)
					return (int)DB_TemperatureUnit.Celesius;
				if (chkTemperatureFehrenhite.Checked)
					return (int)DB_TemperatureUnit.Fahrenheit;
				return null;
			}
			set
			{
				DB_TemperatureUnit temperature = (DB_TemperatureUnit)value;
				switch (temperature)
				{
					case DB_TemperatureUnit.Celesius:
						chkTemperatureCelesius.Checked = true;
						break;
					case DB_TemperatureUnit.Fahrenheit:
						chkTemperatureFehrenhite.Checked = true;
						break;
				}
			}
		}

		public object Temperature_Amount
		{
			get { return spnTemperature.EditValue; }
			set { spnTemperature.EditValue = value; }
		}

		public object Temperature_Description
		{
			get { return txtTemperatureDescription.EditValue; }
			set { txtTemperatureDescription.EditValue = value; }
		}

		public object BloodPressure_AmountHigh
		{
			get { return spnBloodPressureHigh.EditValue; }
			set { spnBloodPressureHigh.EditValue = value; }
		}

		public object BloodPressure_AmountLow
		{
			get { return spnBloodPressureLow.EditValue; }
			set { spnBloodPressureLow.EditValue = value; }
		}

		public object Pulse_Amount
		{
			get { return spnPulse.EditValue; }
			set { spnPulse.EditValue = value; }
		}

		public object Pulse_Reg
		{
			get { return lkePulseReg.EditValue; }
			set { lkePulseReg.EditValue = value; }
		}

		public object BloodPressure_Description
		{
			get { return txtBloodPressureDescription.EditValue; }
			set { txtBloodPressureDescription.EditValue = value; }
		}

		public object Respiration_Amount
		{
			get { return spnRespiration.EditValue; }
			set { spnRespiration.EditValue = value; }
		}

		public object Oxygen_Amount
		{
			get { return spnOxygen.EditValue; }
			set { spnOxygen.EditValue = value; }
		}

		public object FIO2
		{
			get { return lkeFIO.EditValue; }
			set { lkeFIO.EditValue = value; }
		}

		public object SPO2_Amount
		{
			get { return spnSPO.EditValue; }
			set { spnSPO.EditValue = value; }
		}

		public object Respiration_Description
		{
			get { return txtRespirationDescription.EditValue; }
			set { txtRespirationDescription.EditValue = value; }
		}

		#endregion
	}
}
