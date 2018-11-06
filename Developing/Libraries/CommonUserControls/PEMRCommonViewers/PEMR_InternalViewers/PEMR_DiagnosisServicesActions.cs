using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CommonUserControls.CommonViewers;
using CommonUserControls.PEMRCommonViewers.PEMR_Interfaces;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using MerkDataBaseBusinessLogicProject;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;
using MVCBusinessLogicLibrary.BaseViewers;

namespace CommonUserControls.PEMRCommonViewers.PEMR_InternalViewers
{
	public partial class PEMR_DiagnosisServicesActions : UserControl
	{
		public IPEMR_Element Selected_PEMR_Element { get; set; }
		private IPEMRDiagnosisServicesRequest ParentContainer { get; set; }
		private DB_ServiceType ServiceType { get; set; }

		public PEMR_DiagnosisServicesActions()
		{
			InitializeComponent();
		}

		public void Initialize(DB_ServiceType serviceType, IPEMR_Element element,
			IPEMRDiagnosisServicesRequest diagnosisServicesRequest)
		{
			ServiceType = serviceType;
			Selected_PEMR_Element = element;
			ParentContainer = diagnosisServicesRequest;
		}

		private void btnScanPicture_Click(object sender, EventArgs e)
		{
			ScanFiles_UC _scanFilesUc = new ScanFiles_UC();
			switch (ServiceType)
			{
				case DB_ServiceType.InvestigationServices:
					_scanFilesUc.Initialize(PEMRBusinessLogic.ActivePEMRObject.Active_Patient,
						ScanningMode.MedicalVisit, MedicalType.InvestigationResult);
					_scanFilesUc.Pass_VisitTiming_InvestigationReservation(
						(VisitTiming_InvestigationReservation)Selected_PEMR_Element, true);
					break;
				case DB_ServiceType.LabServices:
					_scanFilesUc.Initialize(PEMRBusinessLogic.ActivePEMRObject.Active_Patient,
						ScanningMode.MedicalVisit, MedicalType.LabResult);
					_scanFilesUc.Pass_VisitTiming_LabReservation(
						(VisitTiming_LabReservation)Selected_PEMR_Element, true);
					break;
				case DB_ServiceType.SurgeryService:
					_scanFilesUc.Initialize(PEMRBusinessLogic.ActivePEMRObject.Active_Patient,
						ScanningMode.MedicalVisit, MedicalType.SurgeryResult);
					_scanFilesUc.Pass_VisitTiming_SurgeryReservation(
						(VisitTiming_SurgeryReservation)Selected_PEMR_Element, true);
					break;
			}
			
			PopupBaseForm.ShowAsPopup(_scanFilesUc, this);

			if(ParentForm != null)
				ParentForm.Close();
		}

		private void btnCheckResult_Click(object sender, EventArgs e)
		{
			if (PEMRBusinessLogic.ActivePEMRObject != null &&
			    PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_InvestigationResult != null &&
			    PEMRBusinessLogic.ActivePEMRObject.List_VisitTiming_InvestigationResult.Count > 0)
			{
				List<PatientAttachment_cu> patientAttachementsList =
					PEMRBusinessLogic.GetPatientAttachmetsList(ServiceType,
						((VisitTiming_InvestigationReservation) Selected_PEMR_Element).ID);
				ParentContainer.ShowResult(patientAttachementsList);
				btnExit_Click(null, null);
			}
			else
				XtraMessageBox.Show("There is no results are recorded", "No Result", MessageBoxButtons.OK,
					MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1, DefaultBoolean.Default);
		}

		private void btnExit_Click(object sender, EventArgs e)
		{
			if (ParentForm != null)
				ParentForm.Close();
		}
	}
}
