using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ApplicationConfiguration;
using CommonControlLibrary;
using CommonUserControls.PEMRCommonViewers.PEMR_Interfaces;
using CommonUserControls.Reports;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraLayout.Utils;
using DevExpress.XtraReports.UI;
using MerkDataBaseBusinessLogicProject;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;

namespace CommonUserControls.PEMRCommonViewers.PEMR_InternalViewers
{
	public partial class PEMR_Medication_UC : UserControl, IPEMR_Viewer
	{
		public VisitTiming_Medication Selected_VisitTiming_Medication { get; set; }
		public bool isDateInterval = true;

		public PEMR_Medication_UC()
		{
			InitializeComponent();

			CommonViewsActions.SetupGridControl(grdTreatmentPlans, Resources.LocalizedRes.grd_VisitTiming_Medications,
				true);
		}

		public void Initialize(bool isEditingMode)
		{
			txtDescription.EnterMoveNextControl = false;
			ClearControls(true);

			CommonViewsActions.FillGridlookupEdit(lkeMedications, Medication_cu.ItemsList);
			CommonViewsActions.FillGridlookupEdit(lkeDoses, Dose_cu.ItemsList);

			lytFromDate.Visibility = lytToDate.Visibility =
				chkDateInterval.Checked ? LayoutVisibility.Always : LayoutVisibility.Never;
			lytTimePerDay.Visibility = lytIntervalDuration.Visibility = layoutControlItem24.Visibility = layoutControlItem25.Visibility =
				!chkDateInterval.Checked ? LayoutVisibility.Always : LayoutVisibility.Never;
			chkDateInterval.Checked = true;

			if (PEMRBusinessLogic.ActivePEMRObject == null ||
			    PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_Medication == null ||
			    PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_Medication.Count == 0)
				return;
			grdTreatmentPlans.DataSource = PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_Medication.FindAll(
				item => !Convert.ToInt32(item.PEMRElementStatus).Equals(Convert.ToInt32(PEMRElementStatus.Removed)));
		}

		public void ClearControls(bool clearAll)
		{
			lkeMedications.EditValue = null;
			lkeDoses.EditValue = null;
			dtFrom_DateInterval.EditValue = null;
			dtTo_DateInterval.EditValue = null;
			spnTimePerDay.EditValue = null;
			chkAllDoses.Checked = false;
			chkDateInterval.Checked = true;
		}

		public void FillControls()
		{

		}

		private void lkeMedications_EditValueChanged(object sender, EventArgs e)
		{
			if (lkeMedications.EditValue == null)
			{
				CommonViewsActions.FillGridlookupEdit(lkeDoses, Dose_cu.ItemsList);
				return;
			}

			if (chkAllDoses.Checked)
			{
				CommonViewsActions.FillGridlookupEdit(lkeDoses, Dose_cu.ItemsList);
				return;
			}

			List<Medication_Dose_cu> bridgeList = Medication_Dose_cu.ItemsList.FindAll(item =>
				Convert.ToInt32(item.Medication_CU_ID).Equals(Convert.ToInt32(lkeMedications.EditValue)));
			if(bridgeList.Count == 0)
			{
				CommonViewsActions.FillGridlookupEdit(lkeDoses, Dose_cu.ItemsList);
				return;
			}

			List<Dose_cu> dosesList = new List<Dose_cu>();
			foreach (Medication_Dose_cu bridge in bridgeList)
			{
				Dose_cu dose = Dose_cu.ItemsList.Find(item =>
					Convert.ToInt32(item.ID).Equals(Convert.ToInt32(bridge.Dose_CU_ID)));
				if(dose == null)
					continue;
				dosesList.Add(dose);
			}

			CommonViewsActions.FillGridlookupEdit(lkeDoses, dosesList);
		}

		private void chkDateInterval_CheckedChanged(object sender, EventArgs e)
		{
			lytFromDate.Visibility = lytToDate.Visibility = 
				chkDateInterval.Checked ? LayoutVisibility.Always : LayoutVisibility.Never;
			lytTimePerDay.Visibility = lytIntervalDuration.Visibility = layoutControlItem24.Visibility = layoutControlItem25.Visibility =
				!chkDateInterval.Checked ? LayoutVisibility.Always : LayoutVisibility.Never;
			isDateInterval = true;
			spnTimePerDay.EditValue = null;
		}

		private void chkTimDuration_CheckedChanged(object sender, EventArgs e)
		{
			lytTimePerDay.Visibility = lytIntervalDuration.Visibility = layoutControlItem24.Visibility  = layoutControlItem25.Visibility =
				chkTimDuration.Checked ? LayoutVisibility.Always : LayoutVisibility.Never;
			lytFromDate.Visibility = lytToDate.Visibility = 
				!chkTimDuration.Checked ? LayoutVisibility.Always : LayoutVisibility.Never;
			isDateInterval = false;
			dtFrom_DateInterval.EditValue = dtTo_DateInterval.EditValue = null;
		}

		private void btnAddToList_Click(object sender, EventArgs e)
		{
			if (lkeMedications.EditValue == null || lkeDoses.EditValue == null)
			{
				XtraMessageBox.Show("You select Medication and Dosage before adding", "Notice",
					MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1,
					DefaultBoolean.Default);
				return;
			}

			if (PEMRBusinessLogic.ActivePEMRObject == null)
				return;

			if (PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_Medication == null)
				PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_Medication =
					new List<VisitTiming_Medication>();

			VisitTiming_Medication medication = null;
			medication = PEMRBusinessLogic.CreateNew_VisitTiming_Medication(lkeMedications.EditValue,
				lkeDoses.EditValue, isDateInterval ? null : spnTimePerDay.EditValue,
				isDateInterval ? null : TimeDuration,
				isDateInterval ? dtFrom_DateInterval.EditValue : dtFrom_TimeInterval.EditValue,
				isDateInterval ? dtTo_DateInterval.EditValue : dtTo_TimeInterval.EditValue, txtDescription.EditValue,
				ApplicationStaticConfiguration.PEMRSavingMode);
			if (medication != null)
				PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_Medication.Add(medication);
			grdTreatmentPlans.DataSource = PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_Medication.FindAll(
				item => !Convert.ToInt32(item.PEMRElementStatus).Equals(Convert.ToInt32(PEMRElementStatus.Removed)));
			grdTreatmentPlans.RefreshDataSource();
			ClearControls(true);
		}

		private void btnRemove_Click(object sender, EventArgs e)
		{
			if (Selected_VisitTiming_Medication == null)
			{
				XtraMessageBox.Show("You should select Investigation before Removing", "Notice",
					MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1,
					DefaultBoolean.Default);
				return;
			}

			if (PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_Medication == null
				|| PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_Medication.Count == 0)
			{
				XtraMessageBox.Show("The list has no MEdication", "Notice",
					MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1,
					DefaultBoolean.Default);
				return;
			}

			Selected_VisitTiming_Medication.PEMRElementStatus = PEMRElementStatus.Removed;
			grdTreatmentPlans.DataSource = PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_Medication.FindAll(
				item => !Convert.ToInt32(item.PEMRElementStatus).Equals(Convert.ToInt32(PEMRElementStatus.Removed)));
			grdTreatmentPlans.RefreshDataSource();
			ClearControls(true);
		}

		private void chkAllDoses_CheckedChanged(object sender, EventArgs e)
		{
			if (chkAllDoses.Checked)
			{
				CommonViewsActions.FillGridlookupEdit(lkeDoses, Dose_cu.ItemsList);
				return;
			}

			if (lkeMedications.EditValue == null)
			{
				CommonViewsActions.FillGridlookupEdit(lkeDoses, Dose_cu.ItemsList);
				return;
			}

			List<Medication_Dose_cu> bridgeList = Medication_Dose_cu.ItemsList.FindAll(item =>
				Convert.ToInt32(item.Medication_CU_ID).Equals(Convert.ToInt32(lkeMedications.EditValue)));
			if (bridgeList.Count == 0)
			{
				CommonViewsActions.FillGridlookupEdit(lkeDoses, Dose_cu.ItemsList);
				return;
			}

			List<Dose_cu> dosesList = new List<Dose_cu>();
			foreach (Medication_Dose_cu bridge in bridgeList)
			{
				Dose_cu dose = Dose_cu.ItemsList.Find(item =>
					Convert.ToInt32(item.ID).Equals(Convert.ToInt32(bridge.Dose_CU_ID)));
				if (dose == null)
					continue;
				dosesList.Add(dose);
			}

			CommonViewsActions.FillGridlookupEdit(lkeDoses, dosesList);
		}

		private void btnPrint_Click(object sender, EventArgs e)
		{
			if (PEMRBusinessLogic.ActivePEMRObject == null ||
			    PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_Medication == null ||
			    PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_Medication.Count == 0)
			{
				XtraMessageBox.Show("No Medications are added", " Note", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return;
			}

			PEMR_HeaderAndFooterTemplate_A5_rpt templateReport = new PEMR_HeaderAndFooterTemplate_A5_rpt();
			PEMR_ElementContainer_A5_rpt elementContainer = new PEMR_ElementContainer_A5_rpt();
			List<PEMR_Translated> list = PEMRBusinessLogic.Translate_PEMR_Condensed(PEMRBusinessLogic.ActivePEMRObject,
				DB_PEMR_ElementType.VisitTiming_Medications);
			elementContainer.Initialize(list, DB_PEMR_ElementType.VisitTiming_Medications);
			templateReport =
				PEMR_HeaderAndFooterTemplate_A5_rpt.Initialize(elementContainer, true);
			using (ReportPrintTool reprotTool = new ReportPrintTool(templateReport))
				reprotTool.PrintDialog();
		}

		private void dtFrom_EditValueChanged(object sender, EventArgs e)
		{
			if (dtFrom_DateInterval.EditValue != null)
				dtTo_DateInterval.Properties.MinValue = dtFrom_DateInterval.DateTime;
		}

		private void dtFrom_TimeInterval_EditValueChanged(object sender, EventArgs e)
		{
			if (dtFrom_TimeInterval.EditValue != null)
				dtTo_TimeInterval.Properties.MinValue = dtFrom_TimeInterval.DateTime;
		}

		public object TimeDuration
		{
			get
			{
				if (!chkTimDuration.Checked)
					return null;

				if (chkHourly.Checked)
					return (int) DB_TimeDuration.Hourly;
				if (chkDaily.Checked)
					return (int)DB_TimeDuration.Daily;
				if (chkWeekly.Checked)
					return (int)DB_TimeDuration.Weekly;
				if (chkMonthly.Checked)
					return (int)DB_TimeDuration.Monthly;
				if (chkYearly.Checked)
					return (int)DB_TimeDuration.Yearly;
				return null;
			}
		}

		private void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
		{
			Selected_VisitTiming_Medication =
				CommonViewsActions.GetSelectedRowObject<VisitTiming_Medication>(gridView1);
		}

		private void btnSave_Click(object sender, EventArgs e)
		{

		}
	}
}
