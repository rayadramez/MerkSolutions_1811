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
	public partial class PEMR_LabContainer : UserControl, IPEMR_Viewer, IPEMRDiagnosisServicesRequest
	{
		public VisitTiming_LabReservation Selected_VisitTiming_LabReservation { get; set; }
		private PatientAttachment_cu SelectedItem { get; set; }
		public List<PatientAttachment_cu> ImagesList { get; set; }
		public bool IsEditingMode { get; set; }
		private string errorMessage = "";

		public PEMR_LabContainer()
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

			if (PEMRBusinessLogic.ActivePEMRObject != null &&  PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_LabReservation != null &&
				PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_LabReservation.Count > 0)
				grdTreatmentPlans.DataSource =
					PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_LabReservation.FindAll(
						item => !Convert.ToInt32(item.PEMRElementStatus)
							.Equals(Convert.ToInt32(PEMRElementStatus.Removed)));
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
						.Equals(Convert.ToInt32(DB_ServiceType.LabServices))));
		}

		private void lkeServiceCategories_EditValueChanged(object sender, EventArgs e)
		{
			if (lkeServiceCategories.EditValue == null)
			{
				CommonViewsActions.FillGridlookupEdit(lkeServices,
					Service_cu.ItemsList.FindAll(item =>
						Convert.ToInt32(item.ServiceType_P_ID)
							.Equals(Convert.ToInt32(DB_ServiceType.LabServices))));
				return;
			}

			CommonViewsActions.FillGridlookupEdit(lkeServices,
				Service_cu.ItemsList.FindAll(item =>
					Convert.ToInt32(item.ServiceType_P_ID)
						.Equals(Convert.ToInt32(DB_ServiceType.LabServices)) && Convert
						.ToInt32(item.ServiceCategory_CU_ID).Equals(Convert.ToInt32(lkeServiceCategories.EditValue))));
		}

		private void gridView2_DoubleClick(object sender, EventArgs e)
		{
			if (Selected_VisitTiming_LabReservation == null)
				return;

			PEMR_DiagnosisServicesActions diagnosisActions = new PEMR_DiagnosisServicesActions();
			diagnosisActions.Initialize(DB_ServiceType.LabServices, Selected_VisitTiming_LabReservation, this);
			PopupBaseForm.ShowAsPopup(diagnosisActions, this);
		}

		private void gridView2_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
		{
			Selected_VisitTiming_LabReservation =
				CommonViewsActions.GetSelectedRowObject<VisitTiming_LabReservation>(gridView2);
		}

		private void gridView2_MouseUp(object sender, MouseEventArgs e)
		{
			Selected_VisitTiming_LabReservation =
				CommonViewsActions.GetSelectedRowObject<VisitTiming_LabReservation>(gridView2);
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

			if (PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_LabReservation == null)
				PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_LabReservation =
					new List<VisitTiming_LabReservation>();

			VisitTiming_LabReservation labReservation = PEMRBusinessLogic.CreateNew_VisitTiming_LabReservation(
				lkeServices.EditValue, dtRequestedDate.EditValue, txtDescription.EditValue,
				ApplicationStaticConfiguration.PEMRSavingMode);
			if (labReservation != null)
				PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_LabReservation.Add(labReservation);
			grdTreatmentPlans.DataSource = PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_LabReservation.FindAll(
				item => !Convert.ToInt32(item.PEMRElementStatus)
					.Equals(Convert.ToInt32(PEMRElementStatus.Removed)));
			grdTreatmentPlans.RefreshDataSource();
			ClearControls(false);
		}

		private void btnRemove_Click(object sender, EventArgs e)
		{
			if (Selected_VisitTiming_LabReservation == null)
			{
				XtraMessageBox.Show("You should select Lab before Removing", "Notice",
					MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1,
					DefaultBoolean.Default);
				return;
			}

			if (PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_LabReservation == null
				|| PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_LabReservation.Count == 0)
			{
				XtraMessageBox.Show("The list has no Labs", "Notice",
					MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1,
					DefaultBoolean.Default);
				return;
			}

			Selected_VisitTiming_LabReservation.PEMRElementStatus = PEMRElementStatus.Removed;
			grdTreatmentPlans.DataSource = PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_LabReservation.FindAll(
				item => !Convert.ToInt32(item.PEMRElementStatus)
					.Equals(Convert.ToInt32(PEMRElementStatus.Removed)));
			grdTreatmentPlans.RefreshDataSource();
			ClearControls(false);
		}

		private void tabMainLab_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
		{
			if (tabMainLab.SelectedTabPage.Name == "tabReservation")
			{
				lstImageNames.DataSource = null;
				pictureEdit2.Image = null;
			}
		}

		public void ShowResult(List<PatientAttachment_cu> patientAttachmentsList)
		{
			ImagesList = patientAttachmentsList;
			tabMainLab.SelectedTabPage = tabResults;
			CommonViewsActions.FillListBoxControl(lstImageNames, ImagesList, "ImageName");
			lstImageNames.Refresh();

			CommonViewsActions.FillGridlookupEdit(lkeResultServices,
				Service_cu.ItemsList.FindAll(item =>
					Convert.ToInt32(item.ServiceType_P_ID)
						.Equals(Convert.ToInt32(DB_ServiceType.LabServices))));
			lkeResultServices.EditValue = Selected_VisitTiming_LabReservation.Service_CU_ID;
			txtResultInstructions.EditValue = Selected_VisitTiming_LabReservation.Description;

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

		private void btnPrint_Click_1(object sender, EventArgs e)
		{

		}
	}
}
