using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ApplicationConfiguration;
using CommonControlLibrary;
using CommonUserControls.PEMRCommonViewers.PEMR_Interfaces;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using MerkDataBaseBusinessLogicProject;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;
using MVCBusinessLogicLibrary.BaseViewers;

namespace CommonUserControls.PEMRCommonViewers.PEMR_InternalViewers
{
	public partial class PEMR_InvestigationContainer : UserControl, IPEMR_Viewer, IPEMRDiagnosisServicesRequest
	{
		public VisitTiming_InvestigationReservation Selected_VisitTiming_InvestigationReservation { get; set; }
		private PatientAttachment_cu SelectedItem { get; set; }
		public List<PatientAttachment_cu> ImagesList { get; set; }
		public bool IsEditingMode { get; set; }
		private string errorMessage = "";

		public PEMR_InvestigationContainer()
		{
			InitializeComponent();

			CommonViewsActions.SetupGridControl(grdTreatmentPlans,
				Resources.LocalizedRes.grd_VisitTiming_InvestigationReservation, true);
		}

		public void Initialize(bool isEditingMode)
		{
			tabResults.Enabled = false;
			txtDescription.EnterMoveNextControl = false;
			IsEditingMode = isEditingMode;

			lkeServiceCategories.Properties.Enabled = IsEditingMode;
			lkeServices.Properties.Enabled = IsEditingMode;
			txtDescription.Properties.Enabled = IsEditingMode;
			btnAddToList.Enabled = IsEditingMode;
			btnRemove.Enabled = IsEditingMode;

			if (PEMRBusinessLogic.ActivePEMRObject != null &&
			    PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_InvestigationReservation != null &&
			    PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_InvestigationReservation.Count > 0)
				grdTreatmentPlans.DataSource =
					PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_InvestigationReservation.FindAll(item =>
						!Convert.ToInt32(item.PEMRElementStatus).Equals(Convert.ToInt32(PEMRElementStatus.Removed)));
			FillControls();
		}

		public void ClearControls(bool clearAll)
		{
			lkeServices.EditValue = null;
			lkeServiceCategories.EditValue = null;
			txtDescription.EditValue = null;
			dtRequestedDate.EditValue = null;
			if (clearAll)
				grdTreatmentPlans.DataSource = null;
		}

		public void FillControls()
		{
			CommonViewsActions.FillGridlookupEdit(lkeServiceCategories,
				ServiceCategory_cu.ItemsList.FindAll(item =>
					Convert.ToInt32(item.ServiceType_P_ID)
						.Equals(Convert.ToInt32(DB_ServiceType.InvestigationServices))));
		}

		private void lkeServiceCategories_EditValueChanged(object sender, EventArgs e)
		{
			if (lkeServiceCategories.EditValue == null)
			{
				CommonViewsActions.FillGridlookupEdit(lkeServices,
					Service_cu.ItemsList.FindAll(item =>
						Convert.ToInt32(item.ServiceType_P_ID)
							.Equals(Convert.ToInt32(DB_ServiceType.InvestigationServices))));
				return;
			}

			CommonViewsActions.FillGridlookupEdit(lkeServices,
				Service_cu.ItemsList.FindAll(item =>
					Convert.ToInt32(item.ServiceType_P_ID)
						.Equals(Convert.ToInt32(DB_ServiceType.InvestigationServices)) && Convert
						.ToInt32(item.ServiceCategory_CU_ID).Equals(Convert.ToInt32(lkeServiceCategories.EditValue))));
		}

		private void gridView2_DoubleClick(object sender, EventArgs e)
		{
			if (Selected_VisitTiming_InvestigationReservation == null)
				return;

			PEMR_DiagnosisServicesActions diagnosisActions = new PEMR_DiagnosisServicesActions();
			diagnosisActions.Initialize(DB_ServiceType.InvestigationServices,
				Selected_VisitTiming_InvestigationReservation, this);
			PopupBaseForm.ShowAsPopup(diagnosisActions, this);
		}

		private void gridView2_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
		{
			Selected_VisitTiming_InvestigationReservation =
				CommonViewsActions.GetSelectedRowObject<VisitTiming_InvestigationReservation>(gridView2);
		}

		private void gridView2_MouseUp(object sender, MouseEventArgs e)
		{
			Selected_VisitTiming_InvestigationReservation =
				CommonViewsActions.GetSelectedRowObject<VisitTiming_InvestigationReservation>(gridView2);
		}

		private void btnAddToList_Click(object sender, EventArgs e)
		{
			if (lkeServices.EditValue == null)
			{
				XtraMessageBox.Show("You select Investigation before adding", "Notice",
					MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1,
					DefaultBoolean.Default);
				return;
			}

			if (PEMRBusinessLogic.ActivePEMRObject == null)
				return;

			VisitTiming_InvestigationReservation investigationReservation =
				PEMRBusinessLogic.CreateNew_VisitTiming_InvestigationReservation(lkeServices.EditValue,
					dtRequestedDate.EditValue, txtDescription.EditValue,
					ApplicationStaticConfiguration.PEMRSavingMode);
			if (investigationReservation != null)
			{
				if (PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_InvestigationReservation == null)
					PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_InvestigationReservation =
						new List<VisitTiming_InvestigationReservation>();
				PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_InvestigationReservation.Add(
					investigationReservation);
			}
			grdTreatmentPlans.DataSource = PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_InvestigationReservation.FindAll(
				item => !Convert.ToInt32(item.PEMRElementStatus).Equals(Convert.ToInt32(PEMRElementStatus.Removed)));
			grdTreatmentPlans.RefreshDataSource();
			ClearControls(false);
		}

		private void btnRemove_Click(object sender, EventArgs e)
		{
			if (Selected_VisitTiming_InvestigationReservation == null)
			{
				XtraMessageBox.Show("You should select Investigation before Removing", "Notice",
					MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1,
					DefaultBoolean.Default);
				return;
			}

			if (PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_InvestigationReservation == null
				|| PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_InvestigationReservation.Count == 0)
			{
				XtraMessageBox.Show("The list has no Investigation", "Notice",
					MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1,
					DefaultBoolean.Default);
				return;
			}

			Selected_VisitTiming_InvestigationReservation.PEMRElementStatus = PEMRElementStatus.Removed;
			grdTreatmentPlans.DataSource =
				PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_InvestigationReservation.FindAll(
					item => !Convert.ToInt32(item.PEMRElementStatus)
						.Equals(Convert.ToInt32(PEMRElementStatus.Removed)));
			grdTreatmentPlans.RefreshDataSource();
			ClearControls(false);
		}

		private void tabMainInvestigation_SelectedPageChanged(object sender,
			DevExpress.XtraTab.TabPageChangedEventArgs e)
		{
			if (tabMainInvestigation.SelectedTabPage.Name == "tabReservation")
			{
				lstImageNames.DataSource = null;
				pictureEdit2.Image = null;
			}
		}

		public void ShowResult(List<PatientAttachment_cu> patientAttachmentsList)
		{
			ImagesList = patientAttachmentsList;
			tabMainInvestigation.SelectedTabPage = tabResults;
			CommonViewsActions.FillListBoxControl(lstImageNames, ImagesList, "ImageName");
			lstImageNames.Refresh();

			CommonViewsActions.FillGridlookupEdit(lkeResultServices,
				Service_cu.ItemsList.FindAll(item =>
					Convert.ToInt32(item.ServiceType_P_ID)
						.Equals(Convert.ToInt32(DB_ServiceType.InvestigationServices))));
			lkeResultServices.EditValue = Selected_VisitTiming_InvestigationReservation.Service_CU_ID;
			txtResultInstructions.EditValue = Selected_VisitTiming_InvestigationReservation.Description;

			if (ImagesList == null || ImagesList.Count == 0)
				pictureEdit2.Image = null;
		}

		private void lstImageNames_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (lstImageNames.SelectedItem != null)
			{
				SelectedItem = (PatientAttachment_cu)lstImageNames.SelectedItem;
				if (SelectedItem != null)
					pictureEdit2.Image = FileManager.GetImageFromPath(SelectedItem.ImagePath, ref errorMessage);
			}
		}

		private void lstImageNames_MouseUp(object sender, MouseEventArgs e)
		{
			if (lstImageNames.SelectedItem != null)
			{
				SelectedItem = (PatientAttachment_cu)lstImageNames.SelectedItem;
				if (SelectedItem != null)
					pictureEdit2.Image = FileManager.GetImageFromPath(SelectedItem.ImagePath, ref errorMessage);
				if (!string.IsNullOrEmpty(errorMessage) || !string.IsNullOrWhiteSpace(errorMessage))
					XtraMessageBox.Show(errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error,
						MessageBoxDefaultButton.Button1, DefaultBoolean.Default);
				errorMessage = string.Empty;
			}
		}

		private void btnPrint_Click(object sender, EventArgs e)
		{

		}
	}
}
