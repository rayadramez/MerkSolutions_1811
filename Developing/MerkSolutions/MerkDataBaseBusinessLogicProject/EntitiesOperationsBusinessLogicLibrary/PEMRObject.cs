using System.Collections.Generic;

namespace MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary
{
	public enum PEMRStatus
	{
		None = 0,
		CreateNewVisit = 1,
		UpdateExistingVisit = 2
	}

	public class PEMRObject
	{
		public PEMRStatus PEMRStatus { get; set; }
		public Invoice Active_Invoice { get; set; }
		public InvoiceDetail Active_InvoiceDetail { get; set; }
		public Patient_cu Active_Patient { get; set; }
		public VisitTiming Active_VisitTiming { get; set; }
		public List<VisitTiming> List_VisitTiming { get; set; }
		public List<VisitTiming_SocialHistory> List_VisitTiming_SocialHistory { get; set; }
		public List<VisitTiming_MedicalHistory> List_VisitTiming_MedicalHistory { get; set; }
		public List<VisitTiming_TreatmentPlan> List_VisitTiming_TreatmentPlan { get; set; }
		public List<VisitTiming_Attachment> List_VisitTiming_Attachment { get; set; }
		public List<VisitTiming_InvestigationReservation> List_VisitTiming_InvestigationReservation { get; set; }
		public List<VisitTiming_InvestigationResult> List_VisitTiming_InvestigationResult { get; set; }
		public List<VisitTiming_LabReservation> List_VisitTiming_LabReservation { get; set; }
		public List<VisitTiming_LabResult> List_VisitTiming_LabResult { get; set; }
		public List<VisitTiming_SurgeryReservation> List_VisitTiming_SurgeryReservation { get; set; }
		public List<VisitTiming_SurgeryResult> List_VisitTiming_SurgeryResult { get; set; }
		public List<VisitTiming_Medication> List_VisitTiming_Medication { get; set; }
		public List<VisitTiming_VitalSign> List_VisitTiming_VitalSign { get; set; }
		public List<VisitTiming_MainDiagnosis> List_VisitTiming_MainDiagnosis { get; set; }
		public List<VisitTiming_Diagnosis> List_VisitTiming_Diagnosis { get; set; }
		public List<VisitTiming_MainAnteriorSegmentSign> List_VisitTiming_MainAnteriorSegmentSign { get; set; }
		public List<VisitTiming_AnteriorSegmentSign> List_VisitTiming_AnteriorSegmentSign { get; set; }
		public List<VisitTiming_MainPosteriorSegmentSign> List_VisitTiming_MainPosteriorSegmentSign { get; set; }
		public List<VisitTiming_PosteriorSegmentSign> List_VisitTiming_PosteriorSegmentSign { get; set; }
		public List<VisitTiming_MainAdnexaSegmentSign> List_VisitTiming_MainAdnexaSegmentSign { get; set; }
		public List<VisitTiming_AdnexaSegmentSign> List_VisitTiming_AdnexaSegmentSign { get; set; }
		public List<VisitTiming_VisionRefractionReading> List_VisitTiming_VisionRefractionReading { get; set; }
		public List<VisitTiming_Pupillary> List_VisitTiming_Pupillary { get; set; }
		public List<VisitTiming_MainEOMSign> List_VisitTiming_MainEOMSign { get; set; }
		public List<VisitTiming_EOMSign> List_VisitTiming_EOMSign { get; set; }
		public List<VisitTiming_EOMReading> List_VisitTiming_EOMReading { get; set; }
	}
}
