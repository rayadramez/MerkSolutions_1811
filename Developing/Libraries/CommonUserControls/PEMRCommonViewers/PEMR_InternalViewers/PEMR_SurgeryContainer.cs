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
	public partial class PEMR_SurgeryContainer : UserControl, IPEMR_Viewer, IPEMRDiagnosisServicesRequest
	{
		public VisitTiming_SurgeryReservation Selected_VisitTiming_SurgeryReservation { get; set; }
		private PatientAttachment_cu SelectedItem { get; set; }
		public List<PatientAttachment_cu> ImagesList { get; set; }
		public bool IsEditingMode { get; set; }
		private string errorMessage = "";
		
		public PEMR_SurgeryContainer()
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

			if (PEMRBusinessLogic.ActivePEMRObject != null && PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_SurgeryReservation != null &&
				PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_SurgeryReservation.Count > 0)
				grdTreatmentPlans.DataSource =
					PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_SurgeryReservation.FindAll(
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
					Convert.ToInt32(item.ServiceType_P_ID).Equals(Convert.ToInt32(DB_ServiceType.SurgeryService))));
		}

		private void lkeServiceCategories_EditValueChanged(object sender, EventArgs e)
		{
			if (lkeServiceCategories.EditValue == null)
			{
				CommonViewsActions.FillGridlookupEdit(lkeServices,
					Service_cu.ItemsList.FindAll(item =>
						Convert.ToInt32(item.ServiceType_P_ID)
							.Equals(Convert.ToInt32(DB_ServiceType.SurgeryService))));
				return;
			}

			CommonViewsActions.FillGridlookupEdit(lkeServices,
				Service_cu.ItemsList.FindAll(item =>
					Convert.ToInt32(item.ServiceType_P_ID)
						.Equals(Convert.ToInt32(DB_ServiceType.SurgeryService)) && Convert
						.ToInt32(item.ServiceCategory_CU_ID).Equals(Convert.ToInt32(lkeServiceCategories.EditValue))));
		}

		private void gridView2_DoubleClick(object sender, EventArgs e)
		{
			if (Selected_VisitTiming_SurgeryReservation == null)
				return;

			PEMR_DiagnosisServicesActions diagnosisActions = new PEMR_DiagnosisServicesActions();
			diagnosisActions.Initialize(DB_ServiceType.SurgeryService, Selected_VisitTiming_SurgeryReservation, this);
			PopupBaseForm.ShowAsPopup(diagnosisActions, this);
		}

		private void gridView2_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
		{
			Selected_VisitTiming_SurgeryReservation =
				CommonViewsActions.GetSelectedRowObject<VisitTiming_SurgeryReservation>(gridView2);
		}

		private void gridView2_MouseUp(object sender, MouseEventArgs e)
		{
			Selected_VisitTiming_SurgeryReservation =
				CommonViewsActions.GetSelectedRowObject<VisitTiming_SurgeryReservation>(gridView2);
		}

		private void btnAddToList_Click(object sender, EventArgs e)
		{
			if (lkeServices.EditValue == null)
			{
				XtraMessageBox.Show("You select Surgery before adding", "Notice",
					MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1,
					DefaultBoolean.Default);
				return;
			}

			if (PEMRBusinessLogic.ActivePEMRObject == null)
				return;

			if (PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_SurgeryReservation == null)
				PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_SurgeryReservation =
					new List<VisitTiming_SurgeryReservation>();

			VisitTiming_SurgeryReservation surgeryReservation =
				PEMRBusinessLogic.CreateNew_VisitTiming_SurgeryReservation(lkeServices.EditValue,
					dtRequestedDate.EditValue, txtDescription.EditValue,
					ApplicationStaticConfiguration.PEMRSavingMode);
			if (surgeryReservation != null)
				PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_SurgeryReservation.Add(surgeryReservation);
			grdTreatmentPlans.DataSource = PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_SurgeryReservation.FindAll(
				item => !Convert.ToInt32(item.PEMRElementStatus)
					.Equals(Convert.ToInt32(PEMRElementStatus.Removed)));
			grdTreatmentPlans.RefreshDataSource();
			ClearControls(false);
		}

		private void btnRemove_Click(object sender, EventArgs e)
		{
			if (Selected_VisitTiming_SurgeryReservation == null)
			{
				XtraMessageBox.Show("You should select Lab before Removing", "Notice",
					MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1,
					DefaultBoolean.Default);
				return;
			}

			if (PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_SurgeryReservation == null
				|| PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_SurgeryReservation.Count == 0)
			{
				XtraMessageBox.Show("The list has no Labs", "Notice",
					MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1,
					DefaultBoolean.Default);
				return;
			}

			Selected_VisitTiming_SurgeryReservation.PEMRElementStatus = PEMRElementStatus.Removed;
			grdTreatmentPlans.DataSource = PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_SurgeryReservation.FindAll(
				item => !Convert.ToInt32(item.PEMRElementStatus)
					.Equals(Convert.ToInt32(PEMRElementStatus.Removed)));
			grdTreatmentPlans.RefreshDataSource();
			ClearControls(false);
		}

		private void tabMainLab_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
		{
			if (tabMainSurgery.SelectedTabPage.Name == "tabReservation")
			{
				lstImageNames.DataSource = null;
				pictureEdit2.Image = null;
			}
		}

		public void ShowResult(List<PatientAttachment_cu> patientAttachmentsList)
		{
			ImagesList = patientAttachmentsList;
			tabMainSurgery.SelectedTabPage = tabResults;
			CommonViewsActions.FillListBoxControl(lstImageNames, ImagesList, "ImageName");
			lstImageNames.Refresh();

			CommonViewsActions.FillGridlookupEdit(lkeResultServices,
				Service_cu.ItemsList.FindAll(item =>
					Convert.ToInt32(item.ServiceType_P_ID)
						.Equals(Convert.ToInt32(DB_ServiceType.SurgeryService))));
			lkeResultServices.EditValue = Selected_VisitTiming_SurgeryReservation.Service_CU_ID;
			txtResultInstructions.EditValue = Selected_VisitTiming_SurgeryReservation.Description;

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
