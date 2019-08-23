using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using CommonControlLibrary;
using CommonUserControls.PEMRCommonViewers.PEMR_Interfaces;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using MerkDataBaseBusinessLogicProject;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.PEMRBusinessLogic;

namespace CommonUserControls.PEMRCommonViewers.PEMR_InternalViewers
{
	public partial class PEMR_TreatmentPlan_UC : UserControl, IPEMR_Viewer, IPEMR_TreatmentPlan
	{
		public VisitTiming_TreatmentPlan Active_VisitTiming_TreatmentPlan { get; set; }
		public VisitTiming_TreatmentPlan Selected_VisitTiming_TreatmentPlan { get; set; }

		public PEMR_TreatmentPlan_UC()
		{
			InitializeComponent();

			CommonViewsActions.SetupGridControl(grdTreatmentPlans,
				Resources.LocalizedRes.grd_VisitTiming_TreatmentPlan_Internal, true);
		}

		public void Initialize(bool isEditingMode)
		{
			txtTreatmentPlanDetails.EnterMoveNextControl = false;

			if (PEMRBusinessLogic.ActivePEMRObject != null &&
				PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_TreatmentPlan != null &&
				PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_TreatmentPlan.Count > 0)
			{
				grdTreatmentPlans.DataSource =
					PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_TreatmentPlan.FindAll(
						item => !Convert.ToInt32(item.PEMRElementStatus)
							.Equals(Convert.ToInt32(PEMRElementStatus.Removed))).OrderBy(item => item.StepOrderIndex);
				grdTreatmentPlans.RefreshDataSource();
				ClearControls(false);
			}
		}

		public void FillControls()
		{

		}

		public void ClearControls(bool clearAll)
		{
			txtTreatmentPlanDetails.EditValue = null;
			if (PEMRBusinessLogic.ActivePEMRObject != null)
				if (PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_TreatmentPlan == null
					|| PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_TreatmentPlan.Count == 0)
					spnOrderIndex.EditValue = 1;
				else
					spnOrderIndex.EditValue = PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_TreatmentPlan.Count + 1;
			if (clearAll)
				grdTreatmentPlans.DataSource = null;
			txtTreatmentPlanDetails.Focus();
		}

		private void gridView1_MouseUp(object sender, MouseEventArgs e)
		{

		}

		private void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
		{
			Selected_VisitTiming_TreatmentPlan =
				CommonViewsActions.GetSelectedRowObject<VisitTiming_TreatmentPlan>(gridView1);
		}

		private void btnAddToList_Click(object sender, EventArgs e)
		{
			if (txtTreatmentPlanDetails.EditValue == null || string.IsNullOrEmpty(txtTreatmentPlanDetails.Text)
														  || string.IsNullOrWhiteSpace(txtTreatmentPlanDetails.Text))
			{
				XtraMessageBox.Show("You should enter the Treatment Plan Details before adding", "Notice",
					MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1,
					DefaultBoolean.Default);
				return;
			}

			if (spnOrderIndex.EditValue == null)
			{
				XtraMessageBox.Show("You should enter the Order Index before adding", "Notice",
									MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1,
									DefaultBoolean.Default);
				return;
			}

			if (PEMRBusinessLogic.ActivePEMRObject == null)
				return;

			if (PEMRBusinessLogic.ActivePEMRObject != null)
			{
				Active_VisitTiming_TreatmentPlan =
					PEMRBusinessLogic.CreateNew_VisitTiming_TreatmentPlan(this, DB_PEMRSavingMode.SaveImmediately);
				if (Active_VisitTiming_TreatmentPlan == null)
					return;
				if (PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_TreatmentPlan == null)
				{
					PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_TreatmentPlan =
						new List<VisitTiming_TreatmentPlan>();
				}
				PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_TreatmentPlan.Add(
					Active_VisitTiming_TreatmentPlan);
			}

			grdTreatmentPlans.DataSource =
				PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_TreatmentPlan.FindAll(
					item => !Convert.ToInt32(item.PEMRElementStatus)
						.Equals(Convert.ToInt32(PEMRElementStatus.Removed))).OrderBy(item => item.StepOrderIndex);
			grdTreatmentPlans.RefreshDataSource();
			ClearControls(false);
		}

		private void btnRemove_Click(object sender, EventArgs e)
		{
			if (Selected_VisitTiming_TreatmentPlan == null)
			{
				XtraMessageBox.Show("You should select the Treatment before Removing", "Notice",
									MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1,
									DefaultBoolean.Default);
				return;
			}

			if (PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_TreatmentPlan == null
				|| PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_TreatmentPlan.Count == 0)
			{
				XtraMessageBox.Show("The list has no Treatment Plan", "Notice",
									MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1,
									DefaultBoolean.Default);
				return;
			}

			Selected_VisitTiming_TreatmentPlan.PEMRElementStatus = PEMRElementStatus.Removed;

			int index = 1;
			foreach (VisitTiming_TreatmentPlan timingTreatmentPlan in PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_TreatmentPlan.FindAll(
				item => !Convert.ToInt32(item.PEMRElementStatus)
					.Equals(Convert.ToInt32(PEMRElementStatus.Removed))).OrderBy(item => item.StepOrderIndex))
			{
				timingTreatmentPlan.StepOrderIndex = index;
				index++;
			}

			grdTreatmentPlans.DataSource =
				PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_TreatmentPlan.FindAll(
					item => !Convert.ToInt32(item.PEMRElementStatus)
						.Equals(Convert.ToInt32(PEMRElementStatus.Removed))).OrderBy(item => item.StepOrderIndex);
			grdTreatmentPlans.RefreshDataSource();
			ClearControls(false);
		}

		private void btnUp_Click(object sender, EventArgs e)
		{
			if (Selected_VisitTiming_TreatmentPlan == null)
			{
				XtraMessageBox.Show("You should select the Treatment before Moving Up", "Notice",
									MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1,
									DefaultBoolean.Default);
				return;
			}

			if (PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_TreatmentPlan == null
				|| PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_TreatmentPlan.Count == 0)
			{
				XtraMessageBox.Show("The list has no Treatment Plan", "Notice",
									MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1,
									DefaultBoolean.Default);
				return;
			}

			VisitTiming_TreatmentPlan visitTimingTreatmentPlan =
				PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_TreatmentPlan.Find(
					item => Convert.ToInt32(item.StepOrderIndex)
						.Equals(Convert.ToInt32(Selected_VisitTiming_TreatmentPlan.StepOrderIndex)));
			if (visitTimingTreatmentPlan == null)
				return;

			VisitTiming_TreatmentPlan previousVisitTimingTreatmentPlan = null;
			foreach (VisitTiming_TreatmentPlan timingTreatmentPlan in PEMRBusinessLogic.ActivePEMRObject
				.List_VisitTiming_TreatmentPlan.FindAll(
					item => !Convert.ToInt32(item.PEMRElementStatus)
						.Equals(Convert.ToInt32(PEMRElementStatus.Removed))).OrderBy(item => item.StepOrderIndex))
			{
				if (timingTreatmentPlan.Equals(visitTimingTreatmentPlan))
					break;

				previousVisitTimingTreatmentPlan = timingTreatmentPlan;
			}

			if (previousVisitTimingTreatmentPlan != null)
			{
				if (previousVisitTimingTreatmentPlan.StepOrderIndex <= 0)
					return;
				previousVisitTimingTreatmentPlan.StepOrderIndex = previousVisitTimingTreatmentPlan.StepOrderIndex + 1;
				previousVisitTimingTreatmentPlan.PEMRElementStatus = PEMRElementStatus.Updated;
			}
			else
				return;

			visitTimingTreatmentPlan.StepOrderIndex = visitTimingTreatmentPlan.StepOrderIndex - 1;
			visitTimingTreatmentPlan.PEMRElementStatus = PEMRElementStatus.Updated;
			StepOrderIndex = visitTimingTreatmentPlan.StepOrderIndex;

			if (!PEMRBusinessLogic.Update_VisitTiming_TreatmentPlan(this, Active_VisitTiming_TreatmentPlan))
				return;

			if (!PEMRBusinessLogic.Update_VisitTiming_TreatmentPlan(this, previousVisitTimingTreatmentPlan))
				return;

			grdTreatmentPlans.DataSource =
				PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_TreatmentPlan.FindAll(
					item => !Convert.ToInt32(item.PEMRElementStatus)
						.Equals(Convert.ToInt32(PEMRElementStatus.Removed))).OrderBy(item => item.StepOrderIndex);
			grdTreatmentPlans.RefreshDataSource();
			ClearControls(false);

			StepOrderIndex = PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_TreatmentPlan
				                 .FindAll(item =>
					                 !Convert.ToInt32(item.PEMRElementStatus)
						                 .Equals(Convert.ToInt32(PEMRElementStatus.Removed)))
				                 .OrderBy(item => item.StepOrderIndex).Count() + 1;
		}

		private void btnDown_Click(object sender, EventArgs e)
		{
			if (Selected_VisitTiming_TreatmentPlan == null)
			{
				XtraMessageBox.Show("You should select the Treatment before Moving Up", "Notice",
									MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1,
									DefaultBoolean.Default);
				return;
			}

			if (PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_TreatmentPlan == null
				|| PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_TreatmentPlan.Count == 0)
			{
				XtraMessageBox.Show("The list has no Treatment Plan", "Notice",
									MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1,
									DefaultBoolean.Default);
				return;
			}

			VisitTiming_TreatmentPlan visitTimingTreatmentPlan =
				PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_TreatmentPlan.Find(
					item => Convert.ToInt32(item.StepOrderIndex)
						.Equals(Convert.ToInt32(Selected_VisitTiming_TreatmentPlan.StepOrderIndex)));
			if (visitTimingTreatmentPlan == null)
				return;

			VisitTiming_TreatmentPlan previousVisitTimingTreatmentPlan = null;
			foreach (VisitTiming_TreatmentPlan timingTreatmentPlan in PEMRBusinessLogic.ActivePEMRObject
				.List_VisitTiming_TreatmentPlan.FindAll(
					item => !Convert.ToInt32(item.PEMRElementStatus)
						.Equals(Convert.ToInt32(PEMRElementStatus.Removed))).OrderByDescending(item => item.StepOrderIndex))
			{
				if (timingTreatmentPlan.Equals(visitTimingTreatmentPlan))
					break;

				previousVisitTimingTreatmentPlan = timingTreatmentPlan;
			}

			if (previousVisitTimingTreatmentPlan != null)
			{
				previousVisitTimingTreatmentPlan.StepOrderIndex = previousVisitTimingTreatmentPlan.StepOrderIndex - 1;
				previousVisitTimingTreatmentPlan.PEMRElementStatus = PEMRElementStatus.Updated;
			}
			else
				return;

			visitTimingTreatmentPlan.StepOrderIndex = visitTimingTreatmentPlan.StepOrderIndex + 1;
			visitTimingTreatmentPlan.PEMRElementStatus = PEMRElementStatus.Updated;

			if (!PEMRBusinessLogic.Update_VisitTiming_TreatmentPlan(this, Active_VisitTiming_TreatmentPlan))
				return;

			grdTreatmentPlans.DataSource =
				PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_TreatmentPlan.FindAll(
					item => !Convert.ToInt32(item.PEMRElementStatus)
						.Equals(Convert.ToInt32(PEMRElementStatus.Removed))).OrderBy(item => item.StepOrderIndex);
			grdTreatmentPlans.RefreshDataSource();
			ClearControls(false);

			StepOrderIndex = PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_TreatmentPlan
								 .FindAll(item =>
									 !Convert.ToInt32(item.PEMRElementStatus)
										 .Equals(Convert.ToInt32(PEMRElementStatus.Removed)))
								 .OrderBy(item => item.StepOrderIndex).Count() + 1;
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			if (PEMRBusinessLogic.ActivePEMRObject != null)
				if (PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_TreatmentPlan == null)
				{
					Active_VisitTiming_TreatmentPlan =
						PEMRBusinessLogic.CreateNew_VisitTiming_TreatmentPlan(this, DB_PEMRSavingMode.SaveImmediately);
					if (Active_VisitTiming_TreatmentPlan == null)
						return;
					if (PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_TreatmentPlan == null)
						PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_TreatmentPlan = new List<VisitTiming_TreatmentPlan>();
					PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_TreatmentPlan.Add(Active_VisitTiming_TreatmentPlan);
					XtraMessageBox.Show("Saved Successfully", "Saved", MessageBoxButtons.OK,
						MessageBoxIcon.Information);
				}
				else
				{
					if (Active_VisitTiming_TreatmentPlan == null)
						return;
					if (PEMRBusinessLogic.Update_VisitTiming_TreatmentPlan(this, Active_VisitTiming_TreatmentPlan))
						XtraMessageBox.Show("Saved Successfully", "Saved", MessageBoxButtons.OK,
							MessageBoxIcon.Information);
				}
		}

		#region Implementation of IPEMR_TreatmentPlan

		public object TreatmentDetails
		{
			get { return txtTreatmentPlanDetails.EditValue; }
			set { txtTreatmentPlanDetails.EditValue = value; }
		}

		public object StepOrderIndex
		{
			get { return spnOrderIndex.EditValue; }
			set { spnOrderIndex.EditValue = value; }
		}

		#endregion
	}
}
