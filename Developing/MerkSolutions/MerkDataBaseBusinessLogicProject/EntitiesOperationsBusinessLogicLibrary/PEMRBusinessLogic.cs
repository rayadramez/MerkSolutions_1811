using System;
using System.Collections.Generic;
using System.Linq;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.PEMRBusinessLogic;

namespace MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary
{
	public class PEMRBusinessLogic
	{
		public static InvoiceDetail ActiveInvoiceDetail { get; set; }
		public static PEMRObject ActivePEMRObject { get; set; }
		public static VisitTiming ActiveVisitTimming { get; set; }
		public static User_cu ActiveLoggedInUser { get; set; }
		public static IPEMR_Adnexa PEMR_Adnexa { get; set; }
		public static IPEMR_AnteriorSegmentSign PEMR_AnteriorSegmentSign { get; set; }
		public static IPEMR_EOMSign PEMR_EOMSign { get; set; }
		public static IPEMR_MedicalHistory PEMR_MedicalHistory { get; set; }
		public static IPEMR_PosteriorSegment PEMR_PosteriorSegment { get; set; }
		public static IPEMR_Diagnosis PEMR_Diagnosis { get; set; }
		public static IPEMR_Pupillary PEMR_Pupillary { get; set; }
		public static IPEMR_SocialHistory PEMR_SocialHistory { get; set; }
		public static IPEMR_VitalSign PEMR_VitalSign { get; set; }

		public static PEMRObject GetPEMRObject(int invoiceDetailId)
		{
			InvoiceDetail invoiceDetail = DBCommon.GetEntity<InvoiceDetail>(invoiceDetailId);
			if (invoiceDetail == null)
				return null;

			Invoice invoice = invoiceDetail.Invoice;
			if (invoice == null)
				return null;

			Patient_cu patient = invoice.Patient_cu;
			invoice.PatientObject = patient;
			patient.PersonObject = patient.Person_cu;
			invoice.List_InvoiceDetails = new List<InvoiceDetail>();
			invoice.List_InvoiceDetails.Add(invoiceDetail);

			PEMRObject newPEMR = new PEMRObject();
			newPEMR.Active_Invoice = invoice;
			newPEMR.Active_InvoiceDetail = ActiveInvoiceDetail = invoiceDetail;
			newPEMR.Active_Patient = patient;
			newPEMR.Active_VisitTiming = ActiveVisitTimming;
			ActivePEMRObject = newPEMR;

			return newPEMR;
		}

		public static PEMRObject GetVisitFullTree(GetPreviousMedicalVisits_Result medicalVisitsResult)
		{
			VisitTiming visitTiming = DBCommon.GetEntity<VisitTiming>(medicalVisitsResult.VisitTimingID);
			if (visitTiming == null)
				return null;

			Patient_cu patient = DBCommon.GetEntity<Patient_cu>(medicalVisitsResult.PatientID);
			if (patient == null)
				return null;

			InvoiceDetail invoiceDetail = DBCommon.GetEntity<InvoiceDetail>(medicalVisitsResult.InvoiceDetailID);
			if (invoiceDetail == null)
				return null;

			Invoice invoice = DBCommon.GetEntity<Invoice>(medicalVisitsResult.InvoiceID);
			if (invoice == null)
				return null;

			PEMRObject newPEMR = new PEMRObject();
			newPEMR.Active_VisitTiming = visitTiming;

			if (newPEMR.List_VisitTiming == null)
				newPEMR.List_VisitTiming = new List<VisitTiming>();
			newPEMR.List_VisitTiming.Add(visitTiming);

			newPEMR.PEMRStatus = PEMRStatus.UpdateExistingVisit;

			#region VisitTiming_Pupillary

			List<VisitTiming_Pupillary> list_VisitTiming_Pupillary = DBCommon.DBContext_External.VisitTiming_Pupillary
				.Where(item =>
					item.VisitTimingID.Equals(visitTiming.ID) && item.IsOnDuty).ToList();
			if (list_VisitTiming_Pupillary != null && list_VisitTiming_Pupillary.Count > 0)
			{
				if (newPEMR.List_VisitTiming_Pupillary == null)
					newPEMR.List_VisitTiming_Pupillary = new List<VisitTiming_Pupillary>();
				newPEMR.List_VisitTiming_Pupillary.AddRange(list_VisitTiming_Pupillary);

				if (visitTiming.List_VisitTiming_Pupillary == null)
					visitTiming.List_VisitTiming_Pupillary = new List<VisitTiming_Pupillary>();
				visitTiming.List_VisitTiming_Pupillary.AddRange(list_VisitTiming_Pupillary);

				foreach (VisitTiming_Pupillary visitTiming_Pupillary in newPEMR.List_VisitTiming_Pupillary)
					visitTiming_Pupillary.PEMRElementStatus = PEMRElementStatus.AlreadyExists;
			}

			#endregion

			#region VisitTiming_MainAdnexaSegmentSign

			List<VisitTiming_MainAdnexaSegmentSign> list_VisitTiming_MainAdnexaSegmentSign = DBCommon.DBContext_External
				.VisitTiming_MainAdnexaSegmentSign
				.Where(item => item.VisitTimingID.Equals(visitTiming.ID) && item.IsOnDuty).ToList();
			if (list_VisitTiming_MainAdnexaSegmentSign != null && list_VisitTiming_MainAdnexaSegmentSign.Count > 0)
			{
				if (newPEMR.List_VisitTiming_MainAdnexaSegmentSign == null)
					newPEMR.List_VisitTiming_MainAdnexaSegmentSign = new List<VisitTiming_MainAdnexaSegmentSign>();
				newPEMR.List_VisitTiming_MainAdnexaSegmentSign.AddRange(list_VisitTiming_MainAdnexaSegmentSign);

				if (visitTiming.List_VisitTiming_MainAdnexaSegmentSign == null)
					visitTiming.List_VisitTiming_MainAdnexaSegmentSign = new List<VisitTiming_MainAdnexaSegmentSign>();
				visitTiming.List_VisitTiming_MainAdnexaSegmentSign.AddRange(list_VisitTiming_MainAdnexaSegmentSign);

				foreach (VisitTiming_MainAdnexaSegmentSign visitTiming_MainAdnexaSegmentSign in newPEMR
					.List_VisitTiming_MainAdnexaSegmentSign)
					visitTiming_MainAdnexaSegmentSign.PEMRElementStatus = PEMRElementStatus.AlreadyExists;
			}

			#endregion

			#region VisitTiming_AdnexaSegmentSign

			if (newPEMR.List_VisitTiming_MainAdnexaSegmentSign != null &&
				newPEMR.List_VisitTiming_MainAdnexaSegmentSign.Count > 0)
			{
				List<VisitTiming_AdnexaSegmentSign> list_VisitTiming_AdnexaSegmentSign =
					new List<VisitTiming_AdnexaSegmentSign>();
				foreach (VisitTiming_MainAdnexaSegmentSign visitTimingMainAdnexaSegmentSign in newPEMR
					.List_VisitTiming_MainAdnexaSegmentSign)
				{
					if (visitTimingMainAdnexaSegmentSign.List_VisitTiming_AdnexaSegmentSign == null)
						visitTimingMainAdnexaSegmentSign.List_VisitTiming_AdnexaSegmentSign =
							new List<VisitTiming_AdnexaSegmentSign>();
					visitTimingMainAdnexaSegmentSign.List_VisitTiming_AdnexaSegmentSign.AddRange(
						visitTimingMainAdnexaSegmentSign.VisitTiming_AdnexaSegmentSign);
					list_VisitTiming_AdnexaSegmentSign.AddRange(visitTimingMainAdnexaSegmentSign
						.VisitTiming_AdnexaSegmentSign);
				}
			}

			#endregion

			#region VisitTiming_MainAnteriorSegmentSign

			List<VisitTiming_MainAnteriorSegmentSign> list_VisitTiming_MainAnteriorSegmentSign = DBCommon.DBContext_External
				.VisitTiming_MainAnteriorSegmentSign
				.Where(item => item.VisitTimingID.Equals(visitTiming.ID) && item.IsOnDuty).ToList();
			if (list_VisitTiming_MainAnteriorSegmentSign != null && list_VisitTiming_MainAnteriorSegmentSign.Count > 0)
			{
				if (newPEMR.List_VisitTiming_MainAnteriorSegmentSign == null)
					newPEMR.List_VisitTiming_MainAnteriorSegmentSign = new List<VisitTiming_MainAnteriorSegmentSign>();
				newPEMR.List_VisitTiming_MainAnteriorSegmentSign.AddRange(list_VisitTiming_MainAnteriorSegmentSign);

				if (visitTiming.List_VisitTiming_MainAnteriorSegmentSign == null)
					visitTiming.List_VisitTiming_MainAnteriorSegmentSign = new List<VisitTiming_MainAnteriorSegmentSign>();
				visitTiming.List_VisitTiming_MainAnteriorSegmentSign.AddRange(list_VisitTiming_MainAnteriorSegmentSign);

				foreach (VisitTiming_MainAnteriorSegmentSign visitTiming_MainAnteriorSegmentSign in newPEMR
					.List_VisitTiming_MainAnteriorSegmentSign)
					visitTiming_MainAnteriorSegmentSign.PEMRElementStatus = PEMRElementStatus.AlreadyExists;
			}

			#endregion

			#region VisitTiming_AdnexaSegmentSign

			if (newPEMR.List_VisitTiming_MainAnteriorSegmentSign != null &&
				newPEMR.List_VisitTiming_MainAnteriorSegmentSign.Count > 0)
			{
				List<VisitTiming_AnteriorSegmentSign> list_VisitTiming_AnteriorSegmentSign =
					new List<VisitTiming_AnteriorSegmentSign>();
				foreach (VisitTiming_MainAnteriorSegmentSign visitTiming_MainAnteriorSegmentSign in newPEMR
					.List_VisitTiming_MainAnteriorSegmentSign)
				{
					if (visitTiming_MainAnteriorSegmentSign.List_VisitTiming_AnteriorSegmentSign == null)
						visitTiming_MainAnteriorSegmentSign.List_VisitTiming_AnteriorSegmentSign =
							new List<VisitTiming_AnteriorSegmentSign>();
					visitTiming_MainAnteriorSegmentSign.List_VisitTiming_AnteriorSegmentSign.AddRange(
						visitTiming_MainAnteriorSegmentSign.VisitTiming_AnteriorSegmentSign);
					list_VisitTiming_AnteriorSegmentSign.AddRange(visitTiming_MainAnteriorSegmentSign
						.VisitTiming_AnteriorSegmentSign);
				}

				if (list_VisitTiming_AnteriorSegmentSign.Count > 0)
				{
					if (newPEMR.List_VisitTiming_AnteriorSegmentSign == null)
						newPEMR.List_VisitTiming_AnteriorSegmentSign = new List<VisitTiming_AnteriorSegmentSign>();
					newPEMR.List_VisitTiming_AnteriorSegmentSign.AddRange(list_VisitTiming_AnteriorSegmentSign);

					foreach (VisitTiming_AnteriorSegmentSign visitTimingAnteriorSegment in newPEMR.List_VisitTiming_AnteriorSegmentSign)
						visitTimingAnteriorSegment.PEMRElementStatus = PEMRElementStatus.AlreadyExists;
				}
			}

			#endregion

			#region VisitTiming_MainPosteriorSegmentSign

			List<VisitTiming_MainPosteriorSegmentSign> list_VisitTiming_MainPosteriorSegmentSign = DBCommon.DBContext_External
				.VisitTiming_MainPosteriorSegmentSign
				.Where(item => item.VisitTimingID.Equals(visitTiming.ID) && item.IsOnDuty).ToList();
			if (list_VisitTiming_MainPosteriorSegmentSign != null && list_VisitTiming_MainPosteriorSegmentSign.Count > 0)
			{
				if (newPEMR.List_VisitTiming_MainPosteriorSegmentSign == null)
					newPEMR.List_VisitTiming_MainPosteriorSegmentSign = new List<VisitTiming_MainPosteriorSegmentSign>();
				newPEMR.List_VisitTiming_MainPosteriorSegmentSign.AddRange(list_VisitTiming_MainPosteriorSegmentSign);

				if (visitTiming.List_VisitTiming_MainPosteriorSegmentSign == null)
					visitTiming.List_VisitTiming_MainPosteriorSegmentSign = new List<VisitTiming_MainPosteriorSegmentSign>();
				visitTiming.List_VisitTiming_MainPosteriorSegmentSign.AddRange(list_VisitTiming_MainPosteriorSegmentSign);

				foreach (VisitTiming_MainPosteriorSegmentSign visitTiming_MainPosteriorSegmentSign in newPEMR
					.List_VisitTiming_MainPosteriorSegmentSign)
					visitTiming_MainPosteriorSegmentSign.PEMRElementStatus = PEMRElementStatus.AlreadyExists;
			}

			#endregion

			#region VisitTiming_PosteriorSegmentSign

			if (newPEMR.List_VisitTiming_MainPosteriorSegmentSign != null &&
				newPEMR.List_VisitTiming_MainPosteriorSegmentSign.Count > 0)
			{
				List<VisitTiming_PosteriorSegmentSign> list_VisitTiming_PosteriorSegmentSign =
					new List<VisitTiming_PosteriorSegmentSign>();
				foreach (VisitTiming_MainPosteriorSegmentSign visitTiming_MainPosteriorSegmentSign in newPEMR
					.List_VisitTiming_MainPosteriorSegmentSign)
				{
					if (visitTiming_MainPosteriorSegmentSign.List_VisitTiming_PosteriorSegmentSign == null)
						visitTiming_MainPosteriorSegmentSign.List_VisitTiming_PosteriorSegmentSign =
							new List<VisitTiming_PosteriorSegmentSign>();
					visitTiming_MainPosteriorSegmentSign.List_VisitTiming_PosteriorSegmentSign.AddRange(
						visitTiming_MainPosteriorSegmentSign.VisitTiming_PosteriorSegmentSign);
					list_VisitTiming_PosteriorSegmentSign.AddRange(visitTiming_MainPosteriorSegmentSign
						.VisitTiming_PosteriorSegmentSign);
				}

				if (list_VisitTiming_PosteriorSegmentSign.Count > 0)
				{
					if (newPEMR.List_VisitTiming_PosteriorSegmentSign == null)
						newPEMR.List_VisitTiming_PosteriorSegmentSign = new List<VisitTiming_PosteriorSegmentSign>();
					newPEMR.List_VisitTiming_PosteriorSegmentSign.AddRange(list_VisitTiming_PosteriorSegmentSign);

					foreach (VisitTiming_PosteriorSegmentSign visitTimingPosteriorSegment in newPEMR.List_VisitTiming_PosteriorSegmentSign)
						visitTimingPosteriorSegment.PEMRElementStatus = PEMRElementStatus.AlreadyExists;
				}
			}

			#endregion

			#region VisitTiming_VisionRefractionReading

			List<VisitTiming_VisionRefractionReading> list_VisitTiming_VisionRefractionReading = DBCommon.DBContext_External
				.VisitTiming_VisionRefractionReading
				.Where(item => item.VisitTimingID.Equals(visitTiming.ID) && item.IsOnDuty).ToList();
			if (list_VisitTiming_VisionRefractionReading != null && list_VisitTiming_VisionRefractionReading.Count > 0)
			{
				if (newPEMR.List_VisitTiming_VisionRefractionReading == null)
					newPEMR.List_VisitTiming_VisionRefractionReading = new List<VisitTiming_VisionRefractionReading>();
				newPEMR.List_VisitTiming_VisionRefractionReading.AddRange(list_VisitTiming_VisionRefractionReading);

				if (visitTiming.List_VisitTiming_VisionRefractionReading == null)
					visitTiming.List_VisitTiming_VisionRefractionReading = new List<VisitTiming_VisionRefractionReading>();
				visitTiming.List_VisitTiming_VisionRefractionReading.AddRange(list_VisitTiming_VisionRefractionReading);

				foreach (VisitTiming_VisionRefractionReading visitTiming_VisionRefractionReading in newPEMR
					.List_VisitTiming_VisionRefractionReading)
					visitTiming_VisionRefractionReading.PEMRElementStatus = PEMRElementStatus.AlreadyExists;
			}

			#endregion

			#region VisitTiming_MainEOMSign

			List<VisitTiming_MainEOMSign> list_VisitTiming_MainEOMSign = DBCommon.DBContext_External
				.VisitTiming_MainEOMSign
				.Where(item => item.VisitTimingID.Equals(visitTiming.ID) && item.IsOnDuty).ToList();
			if (list_VisitTiming_MainEOMSign != null && list_VisitTiming_MainEOMSign.Count > 0)
			{
				if (newPEMR.List_VisitTiming_MainEOMSign == null)
					newPEMR.List_VisitTiming_MainEOMSign = new List<VisitTiming_MainEOMSign>();
				newPEMR.List_VisitTiming_MainEOMSign.AddRange(list_VisitTiming_MainEOMSign);

				if (visitTiming.List_VisitTiming_MainEOMSign == null)
					visitTiming.List_VisitTiming_MainEOMSign = new List<VisitTiming_MainEOMSign>();
				visitTiming.List_VisitTiming_MainEOMSign.AddRange(list_VisitTiming_MainEOMSign);

				foreach (VisitTiming_MainEOMSign visitTiming_MainEOMSign in newPEMR
					.List_VisitTiming_MainEOMSign)
					visitTiming_MainEOMSign.PEMRElementStatus = PEMRElementStatus.AlreadyExists;
			}

			#endregion

			#region VisitTiming_EOMSign

			if (newPEMR.List_VisitTiming_MainEOMSign != null &&
				newPEMR.List_VisitTiming_MainEOMSign.Count > 0)
			{
				List<VisitTiming_EOMSign> list_VisitTiming_EOMSign = new List<VisitTiming_EOMSign>();
				foreach (VisitTiming_MainEOMSign visitTiming_MainEOMSign in newPEMR
					.List_VisitTiming_MainEOMSign)
				{
					if (visitTiming_MainEOMSign.List_VisitTiming_EOMSign == null)
						visitTiming_MainEOMSign.List_VisitTiming_EOMSign = new List<VisitTiming_EOMSign>();
					visitTiming_MainEOMSign.List_VisitTiming_EOMSign.AddRange(visitTiming_MainEOMSign.VisitTiming_EOMSign);
					list_VisitTiming_EOMSign.AddRange(visitTiming_MainEOMSign.VisitTiming_EOMSign);
				}

				if (list_VisitTiming_EOMSign.Count > 0)
				{
					if (newPEMR.List_VisitTiming_EOMSign == null)
						newPEMR.List_VisitTiming_EOMSign = new List<VisitTiming_EOMSign>();
					newPEMR.List_VisitTiming_EOMSign.AddRange(list_VisitTiming_EOMSign);

					foreach (VisitTiming_EOMSign visitTimingEomSign in newPEMR.List_VisitTiming_EOMSign)
						visitTimingEomSign.PEMRElementStatus = PEMRElementStatus.AlreadyExists;
				}
			}

			#endregion

			#region VisitTiming_EOMReading

			List<VisitTiming_EOMReading> list_VisitTiming_EOMReading = DBCommon.DBContext_External
				.VisitTiming_EOMReading
				.Where(item => item.VisitTimingID.Equals(visitTiming.ID) && item.IsOnDuty).ToList();
			if (list_VisitTiming_EOMReading != null && list_VisitTiming_EOMReading.Count > 0)
			{
				if (newPEMR.List_VisitTiming_EOMReading == null)
					newPEMR.List_VisitTiming_EOMReading = new List<VisitTiming_EOMReading>();
				newPEMR.List_VisitTiming_EOMReading.AddRange(list_VisitTiming_EOMReading);

				if (visitTiming.List_VisitTiming_EOMReading == null)
					visitTiming.List_VisitTiming_EOMReading = new List<VisitTiming_EOMReading>();
				visitTiming.List_VisitTiming_EOMReading.AddRange(list_VisitTiming_EOMReading);

				foreach (VisitTiming_EOMReading visitTiming_EOMReading in newPEMR.List_VisitTiming_EOMReading)
					visitTiming_EOMReading.PEMRElementStatus = PEMRElementStatus.AlreadyExists;
			}

			#endregion

			#region VisitTiming_Medication
			List<VisitTiming_Medication> visitTimingMedicationsList = DBCommon.DBContext_External.VisitTiming_Medication
				.Where(item => item.VisitTimingID.Equals(visitTiming.ID) && item.IsOnDuty).ToList();
			if (visitTimingMedicationsList != null && visitTimingMedicationsList.Count > 0)
			{
				if (newPEMR.List_VisitTiming_Medication == null)
					newPEMR.List_VisitTiming_Medication = new List<VisitTiming_Medication>();
				newPEMR.List_VisitTiming_Medication.AddRange(visitTimingMedicationsList);

				if (visitTiming.List_VisitTiming_Medication == null)
					visitTiming.List_VisitTiming_Medication = new List<VisitTiming_Medication>();
				visitTiming.List_VisitTiming_Medication.AddRange(visitTimingMedicationsList);

				foreach (VisitTiming_Medication visitTimingMedication in newPEMR.List_VisitTiming_Medication)
					visitTimingMedication.PEMRElementStatus = PEMRElementStatus.AlreadyExists;
			}
			#endregion

			#region VisitTiming_MainDiagnosis
			List<VisitTiming_MainDiagnosis> visitTimingMainDiagnosisList = DBCommon.DBContext_External.VisitTiming_MainDiagnosis
				.Where(item => item.VisitTimingID.Equals(visitTiming.ID) && item.IsOnDuty).ToList();
			if (visitTimingMainDiagnosisList != null && visitTimingMainDiagnosisList.Count > 0)
			{
				if (newPEMR.List_VisitTiming_MainDiagnosis == null)
					newPEMR.List_VisitTiming_MainDiagnosis = new List<VisitTiming_MainDiagnosis>();
				newPEMR.List_VisitTiming_MainDiagnosis.AddRange(visitTimingMainDiagnosisList);

				if (visitTiming.List_VisitTiming_MainDiagnosis == null)
					visitTiming.List_VisitTiming_MainDiagnosis = new List<VisitTiming_MainDiagnosis>();
				visitTiming.List_VisitTiming_MainDiagnosis.AddRange(visitTimingMainDiagnosisList);

				foreach (VisitTiming_MainDiagnosis visitTimingMainDiagnosis in newPEMR.List_VisitTiming_MainDiagnosis)
					visitTimingMainDiagnosis.PEMRElementStatus = PEMRElementStatus.AlreadyExists;
			}
			#endregion

			#region VisitTiming_Diagnosis
			List<VisitTiming_Diagnosis> visitTimingDiagnosisList = null;
			if (visitTimingMainDiagnosisList != null && visitTimingMainDiagnosisList.Count > 0)
			{
				foreach (VisitTiming_MainDiagnosis visitTimingMainDiagnosis in visitTimingMainDiagnosisList.Where(
					item => item.IsOnDuty))
				{
					if (visitTimingDiagnosisList == null)
						visitTimingDiagnosisList = new List<VisitTiming_Diagnosis>();
					visitTimingDiagnosisList.AddRange(visitTimingMainDiagnosis.VisitTiming_Diagnosis);
				}
			}

			if (visitTimingDiagnosisList != null && visitTimingDiagnosisList.Count > 0)
			{
				if (newPEMR.List_VisitTiming_Diagnosis == null)
					newPEMR.List_VisitTiming_Diagnosis = new List<VisitTiming_Diagnosis>();
				newPEMR.List_VisitTiming_Diagnosis.AddRange(visitTimingDiagnosisList);

				foreach (VisitTiming_Diagnosis visitTimingDiagnosis in newPEMR.List_VisitTiming_Diagnosis)
					visitTimingDiagnosis.PEMRElementStatus = PEMRElementStatus.AlreadyExists;
			}
			#endregion

			#region VisitTiming_InvestigationReservation
			List<VisitTiming_InvestigationReservation> visitTimingInvestigationReservations =
				DBCommon.DBContext_External.VisitTiming_InvestigationReservation
					.Where(item => item.VisitTimingID.Equals(visitTiming.ID) && item.IsOnDuty).ToList();
			if (visitTimingInvestigationReservations != null && visitTimingInvestigationReservations.Count > 0)
			{
				if (newPEMR.List_VisitTiming_InvestigationReservation == null)
					newPEMR.List_VisitTiming_InvestigationReservation =
						new List<VisitTiming_InvestigationReservation>();
				newPEMR.List_VisitTiming_InvestigationReservation.AddRange(visitTimingInvestigationReservations);

				if (visitTiming.List_VisitTiming_InvestigationReservation == null)
					visitTiming.List_VisitTiming_InvestigationReservation = new List<VisitTiming_InvestigationReservation>();
				visitTiming.List_VisitTiming_InvestigationReservation.AddRange(visitTimingInvestigationReservations);

				foreach (VisitTiming_InvestigationReservation visitTimingInvestigationReservation in newPEMR
					.List_VisitTiming_InvestigationReservation)
					visitTimingInvestigationReservation.PEMRElementStatus = PEMRElementStatus.AlreadyExists;
			}
			#endregion

			#region VisitTiming_LabReservation
			List<VisitTiming_LabReservation> visitTimingLabReservations =
				DBCommon.DBContext_External.VisitTiming_LabReservation
					.Where(item => item.VisitTimingID.Equals(visitTiming.ID) && item.IsOnDuty).ToList();
			if (visitTimingLabReservations != null && visitTimingLabReservations.Count > 0)
			{
				if (newPEMR.List_VisitTiming_LabReservation == null)
					newPEMR.List_VisitTiming_LabReservation =
						new List<VisitTiming_LabReservation>();
				newPEMR.List_VisitTiming_LabReservation.AddRange(visitTimingLabReservations);

				if (visitTiming.List_VisitTiming_LabReservation == null)
					visitTiming.List_VisitTiming_LabReservation = new List<VisitTiming_LabReservation>();
				visitTiming.List_VisitTiming_LabReservation.AddRange(visitTimingLabReservations);

				foreach (VisitTiming_LabReservation visitTimingLabReservation in newPEMR
					.List_VisitTiming_LabReservation)
					visitTimingLabReservation.PEMRElementStatus = PEMRElementStatus.AlreadyExists;
			}
			#endregion

			#region VisitTiming_SurgeryReservation
			List<VisitTiming_SurgeryReservation> visitTimingSurgeryReservations =
				DBCommon.DBContext_External.VisitTiming_SurgeryReservation
					.Where(item => item.VisitTimingID.Equals(visitTiming.ID) && item.IsOnDuty).ToList();
			if (visitTimingSurgeryReservations != null && visitTimingSurgeryReservations.Count > 0)
			{
				if (newPEMR.List_VisitTiming_SurgeryReservation == null)
					newPEMR.List_VisitTiming_SurgeryReservation = new List<VisitTiming_SurgeryReservation>();
				newPEMR.List_VisitTiming_SurgeryReservation.AddRange(visitTimingSurgeryReservations);

				if (visitTiming.List_VisitTiming_SurgeryReservation == null)
					visitTiming.List_VisitTiming_SurgeryReservation = new List<VisitTiming_SurgeryReservation>();
				visitTiming.List_VisitTiming_SurgeryReservation.AddRange(visitTimingSurgeryReservations);

				foreach (VisitTiming_SurgeryReservation visitTimingSurgeryReservation in newPEMR
					.List_VisitTiming_SurgeryReservation)
					visitTimingSurgeryReservation.PEMRElementStatus = PEMRElementStatus.AlreadyExists;
			}
			#endregion

			#region VisitTiming_TreatmentPlan

			List<VisitTiming_TreatmentPlan> visitTimingTreatmentPlans = DBCommon.DBContext_External
				.VisitTiming_TreatmentPlan.Where(item => item.VisitTimingID.Equals(visitTiming.ID) && item.IsOnDuty)
				.ToList();
			if (visitTimingTreatmentPlans != null && visitTimingTreatmentPlans.Count > 0)
			{
				if (newPEMR.List_VisitTiming_TreatmentPlan == null)
					newPEMR.List_VisitTiming_TreatmentPlan = new List<VisitTiming_TreatmentPlan>();
				newPEMR.List_VisitTiming_TreatmentPlan.AddRange(visitTimingTreatmentPlans);

				if (visitTiming.List_VisitTiming_TreatmentPlan == null)
					visitTiming.List_VisitTiming_TreatmentPlan = new List<VisitTiming_TreatmentPlan>();
				visitTiming.List_VisitTiming_TreatmentPlan.AddRange(visitTimingTreatmentPlans);

				foreach (VisitTiming_TreatmentPlan visitTimingTreatmentPlan in newPEMR.List_VisitTiming_TreatmentPlan)
					visitTimingTreatmentPlan.PEMRElementStatus = PEMRElementStatus.AlreadyExists;
			}

			#endregion

			#region VisitTiming_SocialHistory

			List<VisitTiming_SocialHistory> list_VisitTiming_SocialHistory = DBCommon.DBContext_External.VisitTiming_SocialHistory
				.Where(item =>
					item.VisitTimingID.Equals(visitTiming.ID) && item.IsOnDuty).ToList();
			if (list_VisitTiming_SocialHistory != null && list_VisitTiming_SocialHistory.Count > 0)
			{
				if (newPEMR.List_VisitTiming_SocialHistory == null)
					newPEMR.List_VisitTiming_SocialHistory = new List<VisitTiming_SocialHistory>();
				newPEMR.List_VisitTiming_SocialHistory.AddRange(list_VisitTiming_SocialHistory);

				if (visitTiming.List_VisitTiming_SocialHistory == null)
					visitTiming.List_VisitTiming_SocialHistory = new List<VisitTiming_SocialHistory>();
				visitTiming.List_VisitTiming_SocialHistory.AddRange(list_VisitTiming_SocialHistory);

				foreach (VisitTiming_SocialHistory visitTiming_SocialHistory in newPEMR.List_VisitTiming_SocialHistory)
					visitTiming_SocialHistory.PEMRElementStatus = PEMRElementStatus.AlreadyExists;
			}

			#endregion

			#region VisitTiming_MedicalHistory

			List<VisitTiming_MedicalHistory> list_VisitTiming_MedicalHistory = DBCommon.DBContext_External.VisitTiming_MedicalHistory
				.Where(item =>
					item.VisitTimingID.Equals(visitTiming.ID) && item.IsOnDuty).ToList();
			if (list_VisitTiming_MedicalHistory != null && list_VisitTiming_MedicalHistory.Count > 0)
			{
				if (newPEMR.List_VisitTiming_MedicalHistory == null)
					newPEMR.List_VisitTiming_MedicalHistory = new List<VisitTiming_MedicalHistory>();
				newPEMR.List_VisitTiming_MedicalHistory.AddRange(list_VisitTiming_MedicalHistory);

				if (visitTiming.List_VisitTiming_MedicalHistory == null)
					visitTiming.List_VisitTiming_MedicalHistory = new List<VisitTiming_MedicalHistory>();
				visitTiming.List_VisitTiming_MedicalHistory.AddRange(list_VisitTiming_MedicalHistory);

				foreach (VisitTiming_MedicalHistory visitTiming_MedicalHistory in newPEMR.List_VisitTiming_MedicalHistory)
					visitTiming_MedicalHistory.PEMRElementStatus = PEMRElementStatus.AlreadyExists;
			}

			#endregion

			#region VisitTiming_Attachment
			List<VisitTiming_Attachment> visitTimingAttachementsList = DBCommon.DBContext_External
				.VisitTiming_Attachment.Where(item => item.VisitTimingID.Equals(visitTiming.ID) && item.IsOnDuty).ToList();
			if (visitTimingAttachementsList != null && visitTimingAttachementsList.Count > 0)
			{
				if (newPEMR.List_VisitTiming_Attachment == null)
					newPEMR.List_VisitTiming_Attachment = new List<VisitTiming_Attachment>();
				newPEMR.List_VisitTiming_Attachment.AddRange(visitTimingAttachementsList);
				foreach (VisitTiming_Attachment VisitTimingAttachment in newPEMR.List_VisitTiming_Attachment)
					VisitTimingAttachment.PEMRElementStatus = PEMRElementStatus.AlreadyExists;
			}
			#endregion

			#region VisitTiming_InvestigationResult
			List<VisitTiming_InvestigationResult> visitTimingInvestigationResults = null;
			if (visitTimingInvestigationReservations.Count > 0)
			{
				foreach (VisitTiming_InvestigationReservation visitTimingInvestigationReservation in
					visitTimingInvestigationReservations.Where(item => item.IsOnDuty))
				{
					if (visitTimingInvestigationResults == null)
						visitTimingInvestigationResults = new List<VisitTiming_InvestigationResult>();
					visitTimingInvestigationResults.AddRange(visitTimingInvestigationReservation
						.VisitTiming_InvestigationResult.Where(item => item.IsOnDuty));
				}
			}

			if (visitTimingInvestigationResults != null && visitTimingInvestigationResults.Count > 0)
			{
				if (newPEMR.List_VisitTiming_InvestigationResult == null)
					newPEMR.List_VisitTiming_InvestigationResult = new List<VisitTiming_InvestigationResult>();
				newPEMR.List_VisitTiming_InvestigationResult.AddRange(visitTimingInvestigationResults);

				foreach (VisitTiming_InvestigationResult visitTimingInvestigationResult in newPEMR.List_VisitTiming_InvestigationResult)
					visitTimingInvestigationResult.PEMRElementStatus = PEMRElementStatus.AlreadyExists;
			}
			#endregion

			#region VisitTiming_LabResult
			List<VisitTiming_LabResult> visitTimingLabsResults = null;
			if (visitTimingLabReservations.Count > 0)
			{
				foreach (VisitTiming_LabReservation visitTimingLabReservation in
					visitTimingLabReservations.Where(item => item.IsOnDuty))
				{
					if (visitTimingLabsResults == null)
						visitTimingLabsResults = new List<VisitTiming_LabResult>();
					visitTimingLabsResults.AddRange(visitTimingLabReservation
						.VisitTiming_LabResult.Where(item => item.IsOnDuty));
				}
			}

			if (visitTimingLabsResults != null && visitTimingLabsResults.Count > 0)
			{
				if (newPEMR.List_VisitTiming_LabResult == null)
					newPEMR.List_VisitTiming_LabResult = new List<VisitTiming_LabResult>();
				newPEMR.List_VisitTiming_LabResult.AddRange(visitTimingLabsResults);

				foreach (VisitTiming_LabResult visitTimingLabResult in newPEMR.List_VisitTiming_LabResult)
					visitTimingLabResult.PEMRElementStatus = PEMRElementStatus.AlreadyExists;
			}
			#endregion

			#region VisitTiming_SurgeryResult
			List<VisitTiming_SurgeryResult> visitTimingSurgerysResults = null;
			if (visitTimingLabReservations.Count > 0)
			{
				foreach (VisitTiming_SurgeryReservation visitTimingSurgeryReservation in
					visitTimingSurgeryReservations.Where(item => item.IsOnDuty))
				{
					if (visitTimingSurgerysResults == null)
						visitTimingSurgerysResults = new List<VisitTiming_SurgeryResult>();
					visitTimingSurgerysResults.AddRange(visitTimingSurgeryReservation
						.VisitTiming_SurgeryResult.Where(item => item.IsOnDuty));
				}
			}

			if (visitTimingSurgerysResults != null && visitTimingSurgerysResults.Count > 0)
			{
				if (newPEMR.List_VisitTiming_SurgeryResult == null)
					newPEMR.List_VisitTiming_SurgeryResult = new List<VisitTiming_SurgeryResult>();
				newPEMR.List_VisitTiming_SurgeryResult.AddRange(visitTimingSurgerysResults);

				foreach (VisitTiming_SurgeryResult visitTimingSurgeryResult in newPEMR.List_VisitTiming_SurgeryResult)
					visitTimingSurgeryResult.PEMRElementStatus = PEMRElementStatus.AlreadyExists;
			}
			#endregion

			newPEMR.Active_Patient = patient;
			newPEMR.Active_Invoice = invoice;
			newPEMR.Active_InvoiceDetail = invoiceDetail;
			ActiveVisitTimming = visitTiming;
			ActivePEMRObject = newPEMR;

			return newPEMR;
		}

		#region Create PEMR VisitTiming Objects

		public static VisitTiming CreateNewVisitTiming(object invoiceDetailID, object stationPointID,
			object stationPointStageID, object doctorID, object signInDateTime, DB_PEMRSavingMode savingMode)
		{
			if (invoiceDetailID == null || stationPointID == null || doctorID == null || signInDateTime == null)
				return null;

			VisitTiming visitTiming = DBCommon.CreateNewDBEntity<VisitTiming>();
			visitTiming.InvoiceDetailID = Convert.ToInt32(invoiceDetailID);
			visitTiming.StationPoint_CU_ID = Convert.ToInt32(stationPointID);
			if (stationPointStageID != null)
				visitTiming.StationPointStage_CU_ID = Convert.ToInt32(stationPointStageID);
			visitTiming.Doctor_CU_ID = Convert.ToInt32(doctorID);
			visitTiming.SignInDateTime = Convert.ToDateTime(signInDateTime);
			visitTiming.InsertedBy = Convert.ToInt32(doctorID);
			visitTiming.IsOnDuty = true;
			if (savingMode == DB_PEMRSavingMode.SaveImmediately)
				if (!Save_VisitTiming(visitTiming))
					return null;
			return visitTiming;
		}

		#region Create Anterior Segment

		public static VisitTiming_MainAnteriorSegmentSign CreateNew_VisitTiming_MainAnteriorSegmentSign(
			object generalDescription_OD, object generalDescription_OS, DB_PEMRSavingMode savingMode)
		{
			VisitTiming_MainAnteriorSegmentSign mainSegmentSign = null;
			switch (savingMode)
			{
				case DB_PEMRSavingMode.PostponeSaving:
					return CreateNew_VisitTiming_MainAnteriorSegmentSign(generalDescription_OD, generalDescription_OS);
				case DB_PEMRSavingMode.SaveImmediately:
					mainSegmentSign = CreateNew_VisitTiming_MainAnteriorSegmentSign(ActivePEMRObject.Active_VisitTiming,
						generalDescription_OD, generalDescription_OS);
					if (mainSegmentSign == null || !Save_VisitTiming_MainAnteriorSegmentSign(mainSegmentSign))
						return null;
					return mainSegmentSign;
			}

			return null;
		}

		public static VisitTiming_MainAnteriorSegmentSign CreateNew_VisitTiming_MainAnteriorSegmentSign(VisitTiming visitTiming,
			object generalDescription_OD, object generalDescription_OS)
		{
			if (visitTiming == null)
				return null;
			VisitTiming_MainAnteriorSegmentSign mainSegmentSign =
				CreateNew_VisitTiming_MainAnteriorSegmentSign(generalDescription_OD, generalDescription_OS);
			if (mainSegmentSign == null)
				return null;
			mainSegmentSign.VisitTimingID = visitTiming.ID;
			return mainSegmentSign;
		}

		public static VisitTiming_MainAnteriorSegmentSign CreateNew_VisitTiming_MainAnteriorSegmentSign(
			object generalDescription_OD, object generalDescription_OS)
		{
			VisitTiming_MainAnteriorSegmentSign mainDiagnosis = DBCommon.CreateNewDBEntity<VisitTiming_MainAnteriorSegmentSign>();
			if (generalDescription_OD != null)
				mainDiagnosis.GeneralDescription_OD = generalDescription_OD.ToString();
			if (generalDescription_OS != null)
				mainDiagnosis.GeneralDescription_OS = generalDescription_OS.ToString();
			mainDiagnosis.IsOnDuty = true;
			mainDiagnosis.InsertedBy = ActiveLoggedInUser.ID;
			mainDiagnosis.PEMRElementStatus = PEMRElementStatus.NewelyAdded;
			return mainDiagnosis;
		}

		public static VisitTiming_AnteriorSegmentSign CreateNew_VisitTiming_AnteriorSegmentSign(
			VisitTiming_MainAnteriorSegmentSign mainSegmentSign,
			SegmentSign_cu segmentSign, DB_EyeType_p eyeType, DB_PEMRSavingMode savingMode)
		{
			if (mainSegmentSign == null || segmentSign == null)
				return null;
			VisitTiming_AnteriorSegmentSign visitSegmentSign =
				DBCommon.CreateNewDBEntity<VisitTiming_AnteriorSegmentSign>();
			switch (savingMode)
			{
				case DB_PEMRSavingMode.PostponeSaving:
					mainSegmentSign.VisitTiming_AnteriorSegmentSign.Add(visitSegmentSign);
					break;
				case DB_PEMRSavingMode.SaveImmediately:
					visitSegmentSign.VisitTiming_AnteriorSegmentSignID = mainSegmentSign.ID;
					break;
			}

			visitSegmentSign.SegmentSignID = segmentSign.ID;
			visitSegmentSign.Eye_P_ID = (int)eyeType;
			visitSegmentSign.IsOnDuty = true;
			visitSegmentSign.InsertedBy = ActiveLoggedInUser.ID;
			visitSegmentSign.PEMRElementStatus = PEMRElementStatus.NewelyAdded;
			if (savingMode == DB_PEMRSavingMode.SaveImmediately)
				if (!Save_VisitTiming_AnteriorSegmentSign(visitSegmentSign))
					return null;
			return visitSegmentSign;
		}

		#endregion

		#region Create Posterior Segment

		public static VisitTiming_MainPosteriorSegmentSign CreateNew_VisitTiming_MainPosteriorSegmentSign(
			object generalDescription_OD, object generalDescription_OS, DB_PEMRSavingMode savingMode)
		{
			VisitTiming_MainPosteriorSegmentSign mainSegmentSign = null;
			switch (savingMode)
			{
				case DB_PEMRSavingMode.PostponeSaving:
					return CreateNew_VisitTiming_MainPosteriorSegmentSign(generalDescription_OD, generalDescription_OS);
				case DB_PEMRSavingMode.SaveImmediately:
					mainSegmentSign = CreateNew_VisitTiming_MainPosteriorSegmentSign(
						ActivePEMRObject.Active_VisitTiming, generalDescription_OD, generalDescription_OS);
					if (mainSegmentSign == null || !Save_VisitTiming_MainPosteriorSegmentSign(mainSegmentSign))
						return null;
					return mainSegmentSign;
			}

			return null;
		}

		public static VisitTiming_MainPosteriorSegmentSign CreateNew_VisitTiming_MainPosteriorSegmentSign(VisitTiming visitTiming,
			object generalDescription_OD, object generalDescription_OS)
		{
			if (visitTiming == null)
				return null;
			VisitTiming_MainPosteriorSegmentSign mainSegmentSign =
				CreateNew_VisitTiming_MainPosteriorSegmentSign(generalDescription_OD, generalDescription_OS);
			if (mainSegmentSign == null)
				return null;
			mainSegmentSign.VisitTimingID = visitTiming.ID;
			return mainSegmentSign;
		}

		public static VisitTiming_MainPosteriorSegmentSign CreateNew_VisitTiming_MainPosteriorSegmentSign(
			object generalDescription_OD, object generalDescription_OS)
		{
			VisitTiming_MainPosteriorSegmentSign mainDiagnosis = DBCommon.CreateNewDBEntity<VisitTiming_MainPosteriorSegmentSign>();
			if (generalDescription_OD != null)
				mainDiagnosis.GeneralDescription_OD = generalDescription_OD.ToString();
			if (generalDescription_OS != null)
				mainDiagnosis.GeneralDescription_OS = generalDescription_OS.ToString();
			mainDiagnosis.IsOnDuty = true;
			mainDiagnosis.InsertedBy = ActiveLoggedInUser.ID;
			mainDiagnosis.PEMRElementStatus = PEMRElementStatus.NewelyAdded;
			return mainDiagnosis;
		}

		public static VisitTiming_PosteriorSegmentSign CreateNew_VisitTiming_PosteriorSegmentSign(
			VisitTiming_MainPosteriorSegmentSign mainSegmentSign,
			SegmentSign_cu segmentSign, DB_EyeType_p eyeType, DB_PEMRSavingMode savingMode)
		{
			if (mainSegmentSign == null || segmentSign == null)
				return null;
			VisitTiming_PosteriorSegmentSign visitSegmentSign =
				DBCommon.CreateNewDBEntity<VisitTiming_PosteriorSegmentSign>();
			switch (savingMode)
			{
				case DB_PEMRSavingMode.PostponeSaving:
					mainSegmentSign.VisitTiming_PosteriorSegmentSign.Add(visitSegmentSign);
					break;
				case DB_PEMRSavingMode.SaveImmediately:
					visitSegmentSign.VisitTiming_MainPsoteriorSegmentSignID = mainSegmentSign.ID;
					break;
			}

			visitSegmentSign.SegmentSign_CU_ID = segmentSign.ID;
			visitSegmentSign.Eye_P_ID = (int)eyeType;
			visitSegmentSign.IsOnDuty = true;
			visitSegmentSign.InsertedBy = ActiveLoggedInUser.ID;
			visitSegmentSign.PEMRElementStatus = PEMRElementStatus.NewelyAdded;
			if (savingMode == DB_PEMRSavingMode.SaveImmediately)
				if (!Save_VisitTiming_PosteriorSegmentSign(visitSegmentSign))
					return null;
			return visitSegmentSign;
		}

		#endregion

		#region Create Adnexa Segment

		public static VisitTiming_MainAdnexaSegmentSign CreateNew_VisitTiming_MainAdnexaSegmentSign(
			object generalDescription_OD, object generalDescription_OS, DB_PEMRSavingMode savingMode)
		{
			VisitTiming_MainAdnexaSegmentSign mainSegmentSign = null;
			switch (savingMode)
			{
				case DB_PEMRSavingMode.PostponeSaving:
					return CreateNew_VisitTiming_MainAdnexaSegmentSign(generalDescription_OD, generalDescription_OS);
				case DB_PEMRSavingMode.SaveImmediately:
					mainSegmentSign = CreateNew_VisitTiming_MainAdnexaSegmentSign(ActivePEMRObject.Active_VisitTiming,
						generalDescription_OD, generalDescription_OS);
					if (mainSegmentSign == null || !Save_VisitTiming_MainAdnexaSegmentSign(mainSegmentSign))
						return null;
					return mainSegmentSign;
			}

			return null;
		}

		public static VisitTiming_MainAdnexaSegmentSign CreateNew_VisitTiming_MainAdnexaSegmentSign(VisitTiming visitTiming,
			object generalDescription_OD, object generalDescription_OS)
		{
			if (visitTiming == null)
				return null;
			VisitTiming_MainAdnexaSegmentSign mainSegmentSign =
				CreateNew_VisitTiming_MainAdnexaSegmentSign(generalDescription_OD, generalDescription_OS);
			if (mainSegmentSign == null)
				return null;
			mainSegmentSign.VisitTimingID = visitTiming.ID;
			return mainSegmentSign;
		}

		public static VisitTiming_MainAdnexaSegmentSign CreateNew_VisitTiming_MainAdnexaSegmentSign(
			object generalDescription_OD, object generalDescription_OS)
		{
			VisitTiming_MainAdnexaSegmentSign mainDiagnosis = DBCommon.CreateNewDBEntity<VisitTiming_MainAdnexaSegmentSign>();
			if (generalDescription_OD != null)
				mainDiagnosis.GeneralDescription_OD = generalDescription_OD.ToString();
			if (generalDescription_OS != null)
				mainDiagnosis.GeneralDescription_OS = generalDescription_OS.ToString();
			mainDiagnosis.IsOnDuty = true;
			mainDiagnosis.InsertedBy = ActiveLoggedInUser.ID;
			mainDiagnosis.PEMRElementStatus = PEMRElementStatus.NewelyAdded;
			return mainDiagnosis;
		}

		public static VisitTiming_AdnexaSegmentSign CreateNew_VisitTiming_AdnexaSegmentSign(
			VisitTiming_MainAdnexaSegmentSign mainSegmentSign,
			SegmentSign_cu segmentSign, DB_EyeType_p eyeType, DB_PEMRSavingMode savingMode)
		{
			if (mainSegmentSign == null || segmentSign == null)
				return null;
			VisitTiming_AdnexaSegmentSign visitSegmentSign =
				DBCommon.CreateNewDBEntity<VisitTiming_AdnexaSegmentSign>();
			switch (savingMode)
			{
				case DB_PEMRSavingMode.PostponeSaving:
					mainSegmentSign.VisitTiming_AdnexaSegmentSign.Add(visitSegmentSign);
					break;
				case DB_PEMRSavingMode.SaveImmediately:
					visitSegmentSign.VisitTiming_MainAdnexaSegmentSignID = mainSegmentSign.ID;
					break;
			}

			visitSegmentSign.SegmentSign_CU_ID = segmentSign.ID;
			visitSegmentSign.Eye_P_ID = (int)eyeType;
			visitSegmentSign.IsOnDuty = true;
			visitSegmentSign.InsertedBy = ActiveLoggedInUser.ID;
			visitSegmentSign.PEMRElementStatus = PEMRElementStatus.NewelyAdded;
			if (savingMode == DB_PEMRSavingMode.SaveImmediately)
				if (!Save_VisitTiming_AdnexaSegmentSign(visitSegmentSign))
					return null;
			return visitSegmentSign;
		}

		#endregion

		#region Create EOM Sign

		public static VisitTiming_MainEOMSign CreateNew_VisitTiming_MainEOMSign(
			object generalDescription_OD, object generalDescription_OS, DB_PEMRSavingMode savingMode)
		{
			VisitTiming_MainEOMSign mainSegmentSign = null;
			switch (savingMode)
			{
				case DB_PEMRSavingMode.PostponeSaving:
					return CreateNew_VisitTiming_MainEOMSign(generalDescription_OD, generalDescription_OS);
				case DB_PEMRSavingMode.SaveImmediately:
					mainSegmentSign = CreateNew_VisitTiming_MainEOMSign(ActivePEMRObject.Active_VisitTiming,
						generalDescription_OD, generalDescription_OS);
					if (mainSegmentSign == null || !Save_VisitTiming_MainEOMSign(mainSegmentSign))
						return null;
					return mainSegmentSign;
			}

			return null;
		}

		public static VisitTiming_MainEOMSign CreateNew_VisitTiming_MainEOMSign(VisitTiming visitTiming,
			object generalDescription_OD, object generalDescription_OS)
		{
			if (visitTiming == null)
				return null;
			VisitTiming_MainEOMSign mainSegmentSign =
				CreateNew_VisitTiming_MainEOMSign(generalDescription_OD, generalDescription_OS);
			if (mainSegmentSign == null)
				return null;
			mainSegmentSign.VisitTimingID = visitTiming.ID;
			return mainSegmentSign;
		}

		public static VisitTiming_MainEOMSign CreateNew_VisitTiming_MainEOMSign(
			object generalDescription_OD, object generalDescription_OS)
		{
			VisitTiming_MainEOMSign mainDiagnosis = DBCommon.CreateNewDBEntity<VisitTiming_MainEOMSign>();
			if (generalDescription_OD != null)
				mainDiagnosis.GeneralDescription_OD = generalDescription_OD.ToString();
			if (generalDescription_OS != null)
				mainDiagnosis.GeneralDescription_OS = generalDescription_OS.ToString();
			mainDiagnosis.IsOnDuty = true;
			mainDiagnosis.InsertedBy = ActiveLoggedInUser.ID;
			mainDiagnosis.PEMRElementStatus = PEMRElementStatus.NewelyAdded;
			return mainDiagnosis;
		}

		public static VisitTiming_EOMSign CreateNew_VisitTiming_EOMSign(
			VisitTiming_MainEOMSign mainSegmentSign,
			SegmentSign_cu segmentSign, DB_EyeType_p eyeType, int userID, DB_PEMRSavingMode savingMode)
		{
			if (mainSegmentSign == null || segmentSign == null)
				return null;
			VisitTiming_EOMSign visitSegmentSign = DBCommon.CreateNewDBEntity<VisitTiming_EOMSign>();
			switch (savingMode)
			{
				case DB_PEMRSavingMode.PostponeSaving:
					mainSegmentSign.VisitTiming_EOMSign.Add(visitSegmentSign);
					break;
				case DB_PEMRSavingMode.SaveImmediately:
					visitSegmentSign.VisitTiming_EOMMainSignID = mainSegmentSign.ID;
					break;
			}

			visitSegmentSign.SegmentSign_CU_ID = segmentSign.ID;
			visitSegmentSign.Eye_P_ID = (int)eyeType;
			visitSegmentSign.IsOnDuty = true;
			visitSegmentSign.InsertedBy = userID;
			visitSegmentSign.PEMRElementStatus = PEMRElementStatus.NewelyAdded;
			if (savingMode == DB_PEMRSavingMode.SaveImmediately)
				if (!Save_VisitTiming_EOMSign(visitSegmentSign))
					return null;
			return visitSegmentSign;
		}

		#endregion

		#region VisitTiming_MainDiagnosis

		public static VisitTiming_MainDiagnosis CreateNew_VisitTiming_MainDiagnosis(object generalDescription,
			DB_DiagnosisType diagnosisType, DB_PEMRSavingMode savingMode)
		{
			VisitTiming_MainDiagnosis mainDiagnosis = null;
			switch (savingMode)
			{
				case DB_PEMRSavingMode.PostponeSaving:
					return CreateNew_VisitTiming_MainDiagnosis(generalDescription, diagnosisType);
				case DB_PEMRSavingMode.SaveImmediately:
					mainDiagnosis = CreateNew_VisitTiming_MainDiagnosis(ActivePEMRObject.Active_VisitTiming,
						generalDescription, diagnosisType);
					if (mainDiagnosis == null || !Save_VisitTiming_MainDiagnosis(mainDiagnosis))
						return null;
					return mainDiagnosis;
			}

			return null;
		}

		public static VisitTiming_MainDiagnosis CreateNew_VisitTiming_MainDiagnosis(VisitTiming visitTiming,
			object generalDescription, DB_DiagnosisType diagnosisType)
		{
			if (visitTiming == null)
				return null;
			VisitTiming_MainDiagnosis mainDiagnosis =
				CreateNew_VisitTiming_MainDiagnosis(generalDescription, diagnosisType);
			if (mainDiagnosis == null)
				return null;
			mainDiagnosis.VisitTimingID = visitTiming.ID;
			return mainDiagnosis;
		}

		public static VisitTiming_MainDiagnosis CreateNew_VisitTiming_MainDiagnosis(object generalDescription,
			DB_DiagnosisType diagnosisType)
		{
			VisitTiming_MainDiagnosis mainDiagnosis = DBCommon.CreateNewDBEntity<VisitTiming_MainDiagnosis>();
			if (generalDescription != null)
				mainDiagnosis.GeneralDescription = generalDescription.ToString();

			mainDiagnosis.DiagnosisType_P_ID = (int)diagnosisType;
			mainDiagnosis.IsOnDuty = true;
			mainDiagnosis.InsertedBy = ActiveLoggedInUser.ID;
			mainDiagnosis.PEMRElementStatus = PEMRElementStatus.NewelyAdded;
			return mainDiagnosis;
		}

		public static VisitTiming_Diagnosis CreateNew_VisitTiming_Diagnosis(VisitTiming_MainDiagnosis mainDiagnosis,
			Diagnosis_cu diagnosis, object eyeType, DB_PEMRSavingMode savingMode)
		{
			if (mainDiagnosis == null || diagnosis == null)
				return null;
			VisitTiming_Diagnosis visit_Diagnosis = DBCommon.CreateNewDBEntity<VisitTiming_Diagnosis>();
			switch (savingMode)
			{
				case DB_PEMRSavingMode.PostponeSaving:
					mainDiagnosis.VisitTiming_Diagnosis.Add(visit_Diagnosis);
					break;
				case DB_PEMRSavingMode.SaveImmediately:
					visit_Diagnosis.VisitTiming_MainDiagnosisID = mainDiagnosis.ID;
					break;
			}

			visit_Diagnosis.Diagnosis_CU_ID = diagnosis.ID;
			if (eyeType != null)
				visit_Diagnosis.Eye_P_ID = (int)eyeType;
			visit_Diagnosis.IsOnDuty = true;
			visit_Diagnosis.InsertedBy = ActiveLoggedInUser.ID;
			visit_Diagnosis.PEMRElementStatus = PEMRElementStatus.NewelyAdded;
			if (savingMode == DB_PEMRSavingMode.SaveImmediately)
				if (!Save_VisitTiming_Diagnosis(visit_Diagnosis))
					return null;
			return visit_Diagnosis;
		}

		#endregion

		#region Create EOM Reading

		public static VisitTiming_EOMReading CreateNew_VisitTiming_EOMReading(
			IPEMR_EOMReading eomReadingObject, DB_PEMRSavingMode savingMode)
		{
			if (eomReadingObject == null || eomReadingObject.TakenDateTime == null)
				return null;
			VisitTiming_EOMReading visitTiming_EOMReading = null;
			switch (savingMode)
			{
				case DB_PEMRSavingMode.PostponeSaving:
					return CreateNew_VisitTiming_EOMReading(eomReadingObject);
				case DB_PEMRSavingMode.SaveImmediately:
					visitTiming_EOMReading = CreateNew_VisitTiming_EOMReading(
						ActivePEMRObject.Active_VisitTiming, eomReadingObject);
					if (visitTiming_EOMReading == null || !Save_VisitTiming_EOMReading(visitTiming_EOMReading))
						return null;
					return visitTiming_EOMReading;
			}
			return null;
		}

		public static VisitTiming_EOMReading CreateNew_VisitTiming_EOMReading(
			VisitTiming visitTiming, IPEMR_EOMReading eomReadingObject)
		{
			if (visitTiming == null || eomReadingObject == null || eomReadingObject.TakenDateTime == null)
				return null;
			VisitTiming_EOMReading visitTIming_EOMReading = CreateNew_VisitTiming_EOMReading(eomReadingObject);
			if (visitTIming_EOMReading == null)
				return null;
			visitTIming_EOMReading.VisitTimingID = visitTiming.ID;
			return visitTIming_EOMReading;
		}

		public static VisitTiming_EOMReading CreateNew_VisitTiming_EOMReading(IPEMR_EOMReading eomReadingObject)
		{
			if (eomReadingObject == null || eomReadingObject.TakenDateTime == null)
				return null;

			VisitTiming_EOMReading visitTiming_EOMReading = DBCommon.CreateNewDBEntity<VisitTiming_EOMReading>();
			visitTiming_EOMReading.TakenDateTime = Convert.ToDateTime(eomReadingObject.TakenDateTime);

			if (eomReadingObject.SR_OD != null)
				visitTiming_EOMReading.SR_OD = Convert.ToInt32(eomReadingObject.SR_OD);
			if (eomReadingObject.SR_OS != null)
				visitTiming_EOMReading.SR_OS = Convert.ToInt32(eomReadingObject.SR_OS);
			if (eomReadingObject.LR_OD != null)
				visitTiming_EOMReading.LR_OD = Convert.ToInt32(eomReadingObject.LR_OD);
			if (eomReadingObject.LR_OS != null)
				visitTiming_EOMReading.LR_OS = Convert.ToInt32(eomReadingObject.LR_OS);
			if (eomReadingObject.IR_OD != null)
				visitTiming_EOMReading.IR_OD = Convert.ToInt32(eomReadingObject.IR_OD);
			if (eomReadingObject.IR_OS != null)
				visitTiming_EOMReading.IR_OS = Convert.ToInt32(eomReadingObject.IR_OS);
			if (eomReadingObject.IO_OD != null)
				visitTiming_EOMReading.IO_OD = Convert.ToInt32(eomReadingObject.IO_OD);
			if (eomReadingObject.IO_OS != null)
				visitTiming_EOMReading.IO_OS = Convert.ToInt32(eomReadingObject.IO_OS);
			if (eomReadingObject.MR_OD != null)
				visitTiming_EOMReading.MR_OD = Convert.ToInt32(eomReadingObject.MR_OD);
			if (eomReadingObject.MR_OS != null)
				visitTiming_EOMReading.MR_OS = Convert.ToInt32(eomReadingObject.MR_OS);
			if (eomReadingObject.SO_OD != null)
				visitTiming_EOMReading.SO_OD = Convert.ToInt32(eomReadingObject.SO_OD);
			if (eomReadingObject.SO_OS != null)
				visitTiming_EOMReading.SO_OS = Convert.ToInt32(eomReadingObject.SO_OS);

			visitTiming_EOMReading.IsOnDuty = true;
			visitTiming_EOMReading.InsertedBy = ActiveLoggedInUser.ID;
			visitTiming_EOMReading.PEMRElementStatus = PEMRElementStatus.NewelyAdded;
			return visitTiming_EOMReading;
		}

		#endregion

		#region VisitTiming_Pupillary

		public static VisitTiming_Pupillary CreateNew_VisitTiming_Pupillary(
			IPEMR_Pupillary visitTimingPupillary, DB_PEMRSavingMode savingMode)
		{
			if (visitTimingPupillary == null)
				return null;
			VisitTiming_Pupillary visit_Pupilary = null;
			switch (savingMode)
			{
				case DB_PEMRSavingMode.PostponeSaving:
					return CreateNew_VisitTiming_Pupillary(visitTimingPupillary);
				case DB_PEMRSavingMode.SaveImmediately:
					visit_Pupilary =
						CreateNew_VisitTiming_Pupillary(ActivePEMRObject.Active_VisitTiming, visitTimingPupillary);
					if (visit_Pupilary == null ||
						!Save_VisitTiming_Pupillary(visit_Pupilary))
						return null;
					return visit_Pupilary;
			}
			return null;
		}

		public static VisitTiming_Pupillary CreateNew_VisitTiming_Pupillary(
			VisitTiming visitTiming, IPEMR_Pupillary visitTimingPupillary)
		{
			if (visitTiming == null || visitTimingPupillary == null)
				return null;
			VisitTiming_Pupillary visit_Pupilary = CreateNew_VisitTiming_Pupillary(visitTimingPupillary);
			if (visit_Pupilary == null)
				return null;
			visit_Pupilary.VisitTimingID = visitTiming.ID;
			return visit_Pupilary;
		}

		public static VisitTiming_Pupillary CreateNew_VisitTiming_Pupillary(IPEMR_Pupillary visitTimingPupillary)
		{
			if (visitTimingPupillary == null)
				return null;
			VisitTiming_Pupillary visit_Pupilary =
				DBCommon.CreateNewDBEntity<VisitTiming_Pupillary>();
			if (visitTimingPupillary.IsNoAbnormalitiesFound_OD != null)
				visit_Pupilary.IsNoAbnormalitiesFound_OD =
					Convert.ToBoolean(visitTimingPupillary.IsNoAbnormalitiesFound_OD);
			if (visitTimingPupillary.IsNoAbnormalitiesFound_OS != null)
				visit_Pupilary.IsNoAbnormalitiesFound_OS =
					Convert.ToBoolean(visitTimingPupillary.IsNoAbnormalitiesFound_OS);
			if (visitTimingPupillary.PupillaryAbnormalities_CU_ID_OD != null)
				visit_Pupilary.PupillaryAbnormalities_CU_ID_OD =
					Convert.ToInt32(visitTimingPupillary.PupillaryAbnormalities_CU_ID_OD);
			if (visitTimingPupillary.PupillaryAbnormalities_CU_ID_OS != null)
				visit_Pupilary.PupillaryAbnormalities_CU_ID_OS =
					Convert.ToInt32(visitTimingPupillary.PupillaryAbnormalities_CU_ID_OS);
			if (visitTimingPupillary.IsRAPD_OD != null)
				visit_Pupilary.IsRAPD_OD =
					Convert.ToBoolean(visitTimingPupillary.IsRAPD_OD);
			if (visitTimingPupillary.IsRAPD_OS != null)
				visit_Pupilary.IsRAPD_OS =
					Convert.ToBoolean(visitTimingPupillary.IsRAPD_OS);
			if (visitTimingPupillary.PupillaryRAPDGradingScale_P_ID_OD != null)
				visit_Pupilary.PupillaryRAPDGradingScale_P_ID_OD =
					Convert.ToInt32(visitTimingPupillary.PupillaryRAPDGradingScale_P_ID_OD);
			if (visitTimingPupillary.PupillaryRAPDGradingScale_P_ID_OS != null)
				visit_Pupilary.PupillaryRAPDGradingScale_P_ID_OS =
					Convert.ToInt32(visitTimingPupillary.PupillaryRAPDGradingScale_P_ID_OS);
			if (visitTimingPupillary.PupillaryRAPDCauses_CU_ID_OD != null)
				visit_Pupilary.PupillaryRAPDCauses_CU_ID_OD =
					Convert.ToInt32(visitTimingPupillary.PupillaryRAPDCauses_CU_ID_OD);
			if (visitTimingPupillary.PupillaryRAPDCauses_CU_ID_OS != null)
				visit_Pupilary.PupillaryRAPDCauses_CU_ID_OS =
					Convert.ToInt32(visitTimingPupillary.PupillaryRAPDCauses_CU_ID_OS);
			if (visitTimingPupillary.PupillarySize_P_ID_OD != null)
				visit_Pupilary.PupillarySize_P_ID_OD =
					Convert.ToInt32(visitTimingPupillary.PupillarySize_P_ID_OD);
			if (visitTimingPupillary.PupillarySize_P_ID_OS != null)
				visit_Pupilary.PupillarySize_P_ID_OS =
					Convert.ToInt32(visitTimingPupillary.PupillarySize_P_ID_OS);
			if (visitTimingPupillary.PupillaryShape_P_OD != null)
				visit_Pupilary.PupillaryShape_P_OD =
					Convert.ToInt32(visitTimingPupillary.PupillaryShape_P_OD);
			if (visitTimingPupillary.PupillaryShape_P_OS != null)
				visit_Pupilary.PupillaryShape_P_OS =
					Convert.ToInt32(visitTimingPupillary.PupillaryShape_P_OS);
			if (visitTimingPupillary.Scotopic_OD != null)
				visit_Pupilary.Scotopic_OD =
					Convert.ToDouble(visitTimingPupillary.Scotopic_OD);
			if (visitTimingPupillary.HighPhotopic_OD != null)
				visit_Pupilary.HighPhotopic_OD =
					Convert.ToDouble(visitTimingPupillary.HighPhotopic_OD);
			if (visitTimingPupillary.LowPhotopic_OD != null)
				visit_Pupilary.LowPhotopic_OD =
					Convert.ToDouble(visitTimingPupillary.LowPhotopic_OD);
			if (visitTimingPupillary.HighMesopic_OD != null)
				visit_Pupilary.HighMesopic_OD =
					Convert.ToDouble(visitTimingPupillary.HighMesopic_OD);
			if (visitTimingPupillary.LowMesopic_OD != null)
				visit_Pupilary.LowMesopic_OD =
					Convert.ToDouble(visitTimingPupillary.LowMesopic_OD);
			if (visitTimingPupillary.Scotopic_OS != null)
				visit_Pupilary.Scotopic_OS =
					Convert.ToDouble(visitTimingPupillary.Scotopic_OS);
			if (visitTimingPupillary.HighPhotopic_OS != null)
				visit_Pupilary.HighPhotopic_OS =
					Convert.ToDouble(visitTimingPupillary.HighPhotopic_OS);
			if (visitTimingPupillary.LowPhotopic_OS != null)
				visit_Pupilary.LowPhotopic_OS =
					Convert.ToDouble(visitTimingPupillary.LowPhotopic_OS);
			if (visitTimingPupillary.HighMesopic_OS != null)
				visit_Pupilary.HighMesopic_OS =
					Convert.ToDouble(visitTimingPupillary.HighMesopic_OS);
			if (visitTimingPupillary.LowMesopic_OS != null)
				visit_Pupilary.LowMesopic_OS =
					Convert.ToDouble(visitTimingPupillary.LowMesopic_OS);
			if (visitTimingPupillary.FurtherDetails_OD != null)
				visit_Pupilary.FurtherDetails_OD = visitTimingPupillary.FurtherDetails_OD.ToString();
			if (visitTimingPupillary.FurtherDetails_OS != null)
				visit_Pupilary.FurtherDetails_OS = visitTimingPupillary.FurtherDetails_OS.ToString();
			visit_Pupilary.IsOnDuty = true;
			visit_Pupilary.InsertedBy = ActiveLoggedInUser.ID;
			visit_Pupilary.PEMRElementStatus = PEMRElementStatus.NewelyAdded;
			return visit_Pupilary;
		}

		#endregion

		#region VisitTiming_SocialHistory

		public static VisitTiming_SocialHistory CreateNew_VisitTiming_SocialHistory(
			IPEMR_SocialHistory pemrSocialHistory, DB_PEMRSavingMode savingMode)
		{
			if (pemrSocialHistory == null)
				return null;
			VisitTiming_SocialHistory visit_SOcialHistory = null;
			switch (savingMode)
			{
				case DB_PEMRSavingMode.PostponeSaving:
					return CreateNew_VisitTiming_SocialHistory(pemrSocialHistory);
				case DB_PEMRSavingMode.SaveImmediately:
					visit_SOcialHistory = CreateNew_VisitTiming_SocialHistory(
						ActivePEMRObject.Active_VisitTiming, pemrSocialHistory);
					if (visit_SOcialHistory == null ||
						!Save_VisitTiming_SocialHistory(visit_SOcialHistory))
						return null;
					return visit_SOcialHistory;
			}
			return null;
		}

		public static VisitTiming_SocialHistory CreateNew_VisitTiming_SocialHistory(
			VisitTiming visitTiming, IPEMR_SocialHistory pemrSocialHistory)
		{
			if (visitTiming == null || pemrSocialHistory == null)
				return null;
			VisitTiming_SocialHistory visit_SOcialHistory = CreateNew_VisitTiming_SocialHistory(pemrSocialHistory);
			if (visit_SOcialHistory == null)
				return null;
			visit_SOcialHistory.VisitTimingID = visitTiming.ID;
			return visit_SOcialHistory;
		}

		public static VisitTiming_SocialHistory CreateNew_VisitTiming_SocialHistory(IPEMR_SocialHistory pemrSocialHistory)
		{
			if (pemrSocialHistory == null)
				return null;
			VisitTiming_SocialHistory visitTimmingSocialHistory =
				DBCommon.CreateNewDBEntity<VisitTiming_SocialHistory>();

			if (pemrSocialHistory.GeneralDescription != null &&
				!string.IsNullOrEmpty(pemrSocialHistory.GeneralDescription.ToString()) &&
				!string.IsNullOrWhiteSpace(pemrSocialHistory.GeneralDescription.ToString()))
				visitTimmingSocialHistory.GeneralDescription =
					pemrSocialHistory.GeneralDescription.ToString();
			if (pemrSocialHistory.NegativeSocialHistory != null &&
				Convert.ToBoolean(pemrSocialHistory.NegativeSocialHistory))
				visitTimmingSocialHistory.NegativeSocialHistory =
					Convert.ToBoolean(pemrSocialHistory.NegativeSocialHistory);
			if (pemrSocialHistory.DidYouEverSmoke != null)
				visitTimmingSocialHistory.DidYouEverSmoke =
					Convert.ToBoolean(pemrSocialHistory.DidYouEverSmoke);
			if (pemrSocialHistory.NumberOfPacks != null)
				visitTimmingSocialHistory.NumberOfPacks =
					Convert.ToDouble(pemrSocialHistory.NumberOfPacks);
			if (pemrSocialHistory.NumberOfYears != null)
				visitTimmingSocialHistory.NumberOfYears =
					Convert.ToDouble(pemrSocialHistory.NumberOfYears);
			if (pemrSocialHistory.SmokeFurtherDetails != null &&
				!string.IsNullOrEmpty(pemrSocialHistory.SmokeFurtherDetails.ToString()) &&
				!string.IsNullOrWhiteSpace(pemrSocialHistory.SmokeFurtherDetails.ToString()))
				visitTimmingSocialHistory.SmokeFurtherDetails =
					pemrSocialHistory.SmokeFurtherDetails.ToString();
			if (pemrSocialHistory.QuitingSmokeLessThan != null)
				visitTimmingSocialHistory.QuitingSmokeLessThan =
					Convert.ToBoolean(pemrSocialHistory.QuitingSmokeLessThan);
			if (pemrSocialHistory.QuitingSmokeBetween != null)
				visitTimmingSocialHistory.QuitingSmokeBetween =
					Convert.ToBoolean(pemrSocialHistory.QuitingSmokeBetween);
			if (pemrSocialHistory.QuitingSmokeMoreThan != null)
				visitTimmingSocialHistory.QuitingSmokeMoreThan =
					Convert.ToBoolean(pemrSocialHistory.QuitingSmokeMoreThan);
			if (pemrSocialHistory.QuitingSmokeFurtherDetails != null &&
				!string.IsNullOrEmpty(pemrSocialHistory.QuitingSmokeFurtherDetails.ToString()) &&
				!string.IsNullOrWhiteSpace(pemrSocialHistory.QuitingSmokeFurtherDetails.ToString()))
				visitTimmingSocialHistory.QuitingSmokeFurtherDetails =
					pemrSocialHistory.QuitingSmokeFurtherDetails.ToString();
			if (pemrSocialHistory.DrinkAlcohol != null)
				visitTimmingSocialHistory.DrinkAlcohol =
					Convert.ToBoolean(pemrSocialHistory.DrinkAlcohol);
			if (pemrSocialHistory.HowMuchAlcohol != null)
				visitTimmingSocialHistory.HowMuchAlcohol =
					Convert.ToDouble(pemrSocialHistory.HowMuchAlcohol);
			if (pemrSocialHistory.AlcoholFurtherDetails != null &&
				!string.IsNullOrEmpty(pemrSocialHistory.AlcoholFurtherDetails.ToString()) &&
				!string.IsNullOrWhiteSpace(pemrSocialHistory.AlcoholFurtherDetails.ToString()))
				visitTimmingSocialHistory.AlcoholFurtherDetails =
					pemrSocialHistory.AlcoholFurtherDetails.ToString();
			if (pemrSocialHistory.HadProblemWithAlcohol != null)
				visitTimmingSocialHistory.HadProblemWithAlcohol =
					Convert.ToBoolean(pemrSocialHistory.HadProblemWithAlcohol);
			if (pemrSocialHistory.WhenHadProblemWIthAlcohol != null)
				visitTimmingSocialHistory.WhenHadProblemWIthAlcohol =
					Convert.ToDouble(pemrSocialHistory.WhenHadProblemWIthAlcohol);
			if (pemrSocialHistory.HadProblemWithAlcoholFurtherDetails != null &&
				!string.IsNullOrEmpty(pemrSocialHistory.HadProblemWithAlcoholFurtherDetails.ToString()) &&
				!string.IsNullOrWhiteSpace(pemrSocialHistory.HadProblemWithAlcoholFurtherDetails.ToString()))
				visitTimmingSocialHistory.HadProblemWithAlcoholFurtherDetails =
					pemrSocialHistory.HadProblemWithAlcoholFurtherDetails.ToString();
			if (pemrSocialHistory.Addicted != null)
				visitTimmingSocialHistory.Addicted =
					Convert.ToBoolean(pemrSocialHistory.Addicted);
			if (pemrSocialHistory.AddictionFurtherDetails != null &&
				!string.IsNullOrEmpty(pemrSocialHistory.AddictionFurtherDetails.ToString()) &&
				!string.IsNullOrWhiteSpace(pemrSocialHistory.AddictionFurtherDetails.ToString()))
				visitTimmingSocialHistory.AddictionFurtherDetails =
					pemrSocialHistory.AddictionFurtherDetails.ToString();
			if (pemrSocialHistory.HadProblemWithAddiction != null)
				visitTimmingSocialHistory.HadProblemWithAddiction =
					Convert.ToBoolean(pemrSocialHistory.HadProblemWithAddiction);
			if (pemrSocialHistory.HadProblemWithAddictionFurtherDetails != null &&
				!string.IsNullOrEmpty(pemrSocialHistory.HadProblemWithAddictionFurtherDetails.ToString()) &&
				!string.IsNullOrWhiteSpace(pemrSocialHistory.HadProblemWithAddictionFurtherDetails.ToString()))
				visitTimmingSocialHistory.HadProblemWithAddictionFurtherDetails =
					pemrSocialHistory.HadProblemWithAddictionFurtherDetails.ToString();
			if (pemrSocialHistory.UseRecreationalDrugs != null)
				visitTimmingSocialHistory.UseRecreationalDrugs =
					Convert.ToBoolean(pemrSocialHistory.UseRecreationalDrugs);

			if (pemrSocialHistory.UseRecreationalDrugsFurtherDetails != null &&
				!string.IsNullOrEmpty(pemrSocialHistory.UseRecreationalDrugsFurtherDetails.ToString()) &&
				!string.IsNullOrWhiteSpace(pemrSocialHistory.UseRecreationalDrugsFurtherDetails.ToString()))
				visitTimmingSocialHistory.UseRecreationalDrugsFurtherDetails =
					pemrSocialHistory.UseRecreationalDrugsFurtherDetails.ToString();

			visitTimmingSocialHistory.IsOnDuty = true;
			visitTimmingSocialHistory.InsertedBy = ActiveLoggedInUser.ID;
			visitTimmingSocialHistory.PEMRElementStatus = PEMRElementStatus.NewelyAdded;
			return visitTimmingSocialHistory;
		}

		#endregion

		#region VisitTiming_MedicalHistory

		public static VisitTiming_MedicalHistory CreateNew_VisitTiming_MedicalHistory(
			IPEMR_MedicalHistory pemrMedicalHistory, DB_PEMRSavingMode savingMode)
		{
			if (pemrMedicalHistory == null)
				return null;
			VisitTiming_MedicalHistory visit_MedicalHistory = null;
			switch (savingMode)
			{
				case DB_PEMRSavingMode.PostponeSaving:
					return CreateNew_VisitTiming_MedicalHistory(pemrMedicalHistory);
				case DB_PEMRSavingMode.SaveImmediately:
					visit_MedicalHistory = CreateNew_VisitTiming_MedicalHistory(
						ActivePEMRObject.Active_VisitTiming, pemrMedicalHistory);
					if (visit_MedicalHistory == null ||
						!Save_VisitTiming_MedicalHistory(visit_MedicalHistory))
						return null;
					return visit_MedicalHistory;
			}
			return null;
		}

		public static VisitTiming_MedicalHistory CreateNew_VisitTiming_MedicalHistory(
			VisitTiming visitTiming, IPEMR_MedicalHistory pemrMedicalHistory)
		{
			if (visitTiming == null || pemrMedicalHistory == null)
				return null;
			VisitTiming_MedicalHistory visit_MedicalHistory = CreateNew_VisitTiming_MedicalHistory(pemrMedicalHistory);
			if (visit_MedicalHistory == null)
				return null;
			visit_MedicalHistory.VisitTimingID = visitTiming.ID;
			return visit_MedicalHistory;
		}

		public static VisitTiming_MedicalHistory CreateNew_VisitTiming_MedicalHistory(
			IPEMR_MedicalHistory pemrMedicalHistory)
		{
			if (pemrMedicalHistory == null)
				return null;
			VisitTiming_MedicalHistory visitTiming_MedicalHistory =
				DBCommon.CreateNewDBEntity<VisitTiming_MedicalHistory>();

			if (pemrMedicalHistory.FurtherDetails != null &&
				!string.IsNullOrEmpty(pemrMedicalHistory.FurtherDetails.ToString()) &&
				!string.IsNullOrWhiteSpace(pemrMedicalHistory.FurtherDetails.ToString()))
				visitTiming_MedicalHistory.FurtherDetails = pemrMedicalHistory.FurtherDetails.ToString();
			if (pemrMedicalHistory.HasDiabetes != null)
				visitTiming_MedicalHistory.HasDiabetes = Convert.ToBoolean(pemrMedicalHistory.HasDiabetes);
			else
				visitTiming_MedicalHistory.HasDiabetes = null;
			if (pemrMedicalHistory.DiabetesType != null)
				visitTiming_MedicalHistory.DiabetesType_P_ID = Convert.ToInt32(pemrMedicalHistory.DiabetesType);
			if (pemrMedicalHistory.IsDiabetesControlled != null)
				visitTiming_MedicalHistory.IsDiabetesControlled = Convert.ToBoolean(pemrMedicalHistory.IsDiabetesControlled);
			if (pemrMedicalHistory.HbA1C != null)
				visitTiming_MedicalHistory.HbA1c = Convert.ToInt32(pemrMedicalHistory.HbA1C);
			if (pemrMedicalHistory.DiabetesMedicationType != null)
				visitTiming_MedicalHistory.DiabetesMedicationType_P_ID = Convert.ToInt32(pemrMedicalHistory.DiabetesMedicationType);
			if (pemrMedicalHistory.DiabetesMedication != null)
				visitTiming_MedicalHistory.DiabetesMedication_CU_ID = Convert.ToInt32(pemrMedicalHistory.DiabetesMedication);
			if (pemrMedicalHistory.DiabetesDosage != null)
				visitTiming_MedicalHistory.DiabetesDose_CU_ID = Convert.ToInt32(pemrMedicalHistory.DiabetesDosage);
			if (pemrMedicalHistory.DiabetesMedicationDuration != null)
				visitTiming_MedicalHistory.DiabetesMedicationDuration = Convert.ToInt32(pemrMedicalHistory.DiabetesMedicationDuration);
			if (pemrMedicalHistory.DiabetesMedicationDurationType != null)
				visitTiming_MedicalHistory.DiabetesTimeDurationType_P_ID = Convert.ToInt32(pemrMedicalHistory.DiabetesMedicationDurationType);

			if (pemrMedicalHistory.IsHypertension != null)
				visitTiming_MedicalHistory.IsHypertension = Convert.ToBoolean(pemrMedicalHistory.IsHypertension);
			else
				visitTiming_MedicalHistory.IsHypertension = null;
			if (pemrMedicalHistory.IsHypertensionControlled != null)
				visitTiming_MedicalHistory.IsHypertensionControlled = Convert.ToBoolean(pemrMedicalHistory.IsHypertensionControlled);
			if (pemrMedicalHistory.HypertensionMedication != null)
				visitTiming_MedicalHistory.HypertensionMedication_CU_ID = Convert.ToInt32(pemrMedicalHistory.HypertensionMedication);
			if (pemrMedicalHistory.HypertensionDosage != null)
				visitTiming_MedicalHistory.HypertensionDose_CU_ID = Convert.ToInt32(pemrMedicalHistory.HypertensionDosage);
			if (pemrMedicalHistory.HypertensionMedicationDuration != null)
				visitTiming_MedicalHistory.HypertensionMedicationDuration = Convert.ToInt32(pemrMedicalHistory.HypertensionMedicationDuration);
			if (pemrMedicalHistory.HypertensionMedicationDurationType != null)
				visitTiming_MedicalHistory.HypertensionTimeDurationType_P_ID = Convert.ToInt32(pemrMedicalHistory.HypertensionMedicationDurationType);

			if (pemrMedicalHistory.HasDrugAllergies != null)
				visitTiming_MedicalHistory.IsDrugAllergy = Convert.ToBoolean(pemrMedicalHistory.HasDrugAllergies);
			else
				pemrMedicalHistory.HasDrugAllergies = null;
			if (pemrMedicalHistory.TriggersDrugAllergies != null)
				visitTiming_MedicalHistory.TriggerOfDrugAllergy_CU_ID = Convert.ToInt32(pemrMedicalHistory.TriggersDrugAllergies);

			if (pemrMedicalHistory.HasHepatitis != null)
				visitTiming_MedicalHistory.IsHepatitis = Convert.ToBoolean(pemrMedicalHistory.HasHepatitis);
			else
				visitTiming_MedicalHistory.IsHepatitis = null;

			if (pemrMedicalHistory.HasAsthma != null)
				visitTiming_MedicalHistory.IsAsthma = Convert.ToBoolean(pemrMedicalHistory.HasAsthma);
			else
				pemrMedicalHistory.HasAsthma = null;

			visitTiming_MedicalHistory.IsOnDuty = true;
			visitTiming_MedicalHistory.InsertedBy = ActiveLoggedInUser.ID;
			visitTiming_MedicalHistory.PEMRElementStatus = PEMRElementStatus.NewelyAdded;
			return visitTiming_MedicalHistory;
		}

		#endregion

		#region Create VisionRefractionReadings

		public static VisitTiming_VisionRefractionReading CreateNew_VisitTiming_VisionRefractionReading(
			IPEMR_VisionRefractionReading visitonRefractionReading, DB_PEMRSavingMode savingMode)
		{
			if (visitonRefractionReading == null)
				return null;
			VisitTiming_VisionRefractionReading visionRefraction = null;
			switch (savingMode)
			{
				case DB_PEMRSavingMode.PostponeSaving:
					return CreateNew_VisitTiming_VisionRefractionReading(visitonRefractionReading);
				case DB_PEMRSavingMode.SaveImmediately:
					visionRefraction = CreateNew_VisitTiming_VisionRefractionReading(
						ActivePEMRObject.Active_VisitTiming, visitonRefractionReading);
					if (visionRefraction == null ||
						!Save_VisitTiming_VisionRefractionReading(visionRefraction))
						return null;
					return visionRefraction;
			}
			return null;
		}

		public static VisitTiming_VisionRefractionReading CreateNew_VisitTiming_VisionRefractionReading(
			VisitTiming visitTiming, IPEMR_VisionRefractionReading visitonRefractionReading)
		{
			if (visitTiming == null || visitonRefractionReading == null)
				return null;
			VisitTiming_VisionRefractionReading visionRefractionObject =
				CreateNew_VisitTiming_VisionRefractionReading(visitonRefractionReading);
			if (visionRefractionObject == null)
				return null;
			visionRefractionObject.VisitTimingID = visitTiming.ID;
			return visionRefractionObject;
		}

		public static VisitTiming_VisionRefractionReading CreateNew_VisitTiming_VisionRefractionReading(
			IPEMR_VisionRefractionReading visitonRefractionReading)
		{
			if (visitonRefractionReading == null || visitonRefractionReading.TakenDate == null ||
				visitonRefractionReading.TakenTime == null ||
				visitonRefractionReading.VisionRefractionReadingTypeID == null)
				return null;

			VisitTiming_VisionRefractionReading visionRefractionObject =
				DBCommon.CreateNewDBEntity<VisitTiming_VisionRefractionReading>();

			TimeSpan time = Convert.ToDateTime(visitonRefractionReading.TakenTime).TimeOfDay;
			DateTime takenDateTime = Convert.ToDateTime(visitonRefractionReading.TakenDate);
			takenDateTime = takenDateTime.Add(time);
			visionRefractionObject.TakenDateTime = takenDateTime;
			visionRefractionObject.VisionRefractionReadingType_P_ID =
				Convert.ToInt32(visitonRefractionReading.VisionRefractionReadingTypeID);
			//OU
			if (visitonRefractionReading.UVA_OU != null)
				visionRefractionObject.UVA_OU = Convert.ToInt32(visitonRefractionReading.UVA_OU);
			if (visitonRefractionReading.NVA_OU != null)
				visionRefractionObject.NVA_OU = Convert.ToInt32(visitonRefractionReading.NVA_OU);
			if (visitonRefractionReading.NVAAmount_OU != null)
				visionRefractionObject.NVAAmount_OU = Convert.ToInt32(visitonRefractionReading.NVAAmount_OU);
			//OD
			if (visitonRefractionReading.Distance_OD != null)
				visionRefractionObject.Distance_OD = Convert.ToInt32(visitonRefractionReading.Distance_OD);
			if (visitonRefractionReading.NVA_OD != null)
				visionRefractionObject.NVA_OD = Convert.ToInt32(visitonRefractionReading.NVA_OD);
			if (visitonRefractionReading.NVAAmount_OD != null)
				visionRefractionObject.NVAAmount_OD = Convert.ToInt32(visitonRefractionReading.NVAAmount_OD);
			if (visitonRefractionReading.SphereAmount_OD != null)
				visionRefractionObject.Sph_OD = Convert.ToInt32(visitonRefractionReading.SphereAmount_OD);
			if (visitonRefractionReading.CylinderAmount_OD != null)
				visionRefractionObject.Cyl_OD = Convert.ToInt32(visitonRefractionReading.CylinderAmount_OD);
			if (visitonRefractionReading.AxisAmount_OD != null)
				visionRefractionObject.Axis_OD = Convert.ToInt32(visitonRefractionReading.AxisAmount_OD);
			if (visitonRefractionReading.UVA_OD != null)
				visionRefractionObject.UVA_OD = Convert.ToInt32(visitonRefractionReading.UVA_OD);
			if (visitonRefractionReading.Add_OD != null)
				visionRefractionObject.Add_OD = Convert.ToInt32(visitonRefractionReading.Add_OD);
			if (visitonRefractionReading.Remarks_OD != null)
				visionRefractionObject.Remarks_OD = visitonRefractionReading.Remarks_OD.ToString();
			if (visitonRefractionReading.IsError_OD != null)
				visionRefractionObject.IsError_OD = Convert.ToBoolean(visitonRefractionReading.IsError_OD);
			if (visitonRefractionReading.IsIgnored_OD != null)
				visionRefractionObject.IsIgnored_OD = Convert.ToBoolean(visitonRefractionReading.IsIgnored_OD);
			if (visitonRefractionReading.RatingAmount_OD != null)
				visionRefractionObject.Rating_OD = Convert.ToInt32(visitonRefractionReading.RatingAmount_OD);
			//OS
			if (visitonRefractionReading.Distance_OS != null)
				visionRefractionObject.Distance_OS = Convert.ToInt32(visitonRefractionReading.Distance_OS);
			if (visitonRefractionReading.NVA_OS != null)
				visionRefractionObject.NVA_OS = Convert.ToInt32(visitonRefractionReading.NVA_OS);
			if (visitonRefractionReading.NVAAmount_OS != null)
				visionRefractionObject.NVAAmount_OS = Convert.ToInt32(visitonRefractionReading.NVAAmount_OS);
			if (visitonRefractionReading.SphereAmount_OS != null)
				visionRefractionObject.Sph_OS = Convert.ToInt32(visitonRefractionReading.SphereAmount_OS);
			if (visitonRefractionReading.CylinderAmount_OS != null)
				visionRefractionObject.Cyl_OS = Convert.ToInt32(visitonRefractionReading.CylinderAmount_OS);
			if (visitonRefractionReading.AxisAmount_OS != null)
				visionRefractionObject.Axis_OS = Convert.ToInt32(visitonRefractionReading.AxisAmount_OS);
			if (visitonRefractionReading.UVA_OS != null)
				visionRefractionObject.UVA_OS = Convert.ToInt32(visitonRefractionReading.UVA_OS);
			if (visitonRefractionReading.Add_OS != null)
				visionRefractionObject.Add_OS = Convert.ToInt32(visitonRefractionReading.Add_OS);
			if (visitonRefractionReading.Remarks_OS != null)
				visionRefractionObject.Remarks_OS = visitonRefractionReading.Remarks_OS.ToString();
			if (visitonRefractionReading.IsError_OS != null)
				visionRefractionObject.IsError_OS = Convert.ToBoolean(visitonRefractionReading.IsError_OS);
			if (visitonRefractionReading.IsIgnored_OS != null)
				visionRefractionObject.IsIgnored_OS = Convert.ToBoolean(visitonRefractionReading.IsIgnored_OS);
			if (visitonRefractionReading.RatingAmount_OS != null)
				visionRefractionObject.Rating_OS = Convert.ToInt32(visitonRefractionReading.RatingAmount_OS);

			visionRefractionObject.IsOnDuty = true;
			visionRefractionObject.InsertedBy = ActiveLoggedInUser.ID;
			visionRefractionObject.PEMRElementStatus = PEMRElementStatus.NewelyAdded;
			return visionRefractionObject;
		}

		#endregion

		#region Create Medications

		public static VisitTiming_Medication CreateNew_VisitTiming_Medication(object medicationID, object dosageID,
			object timesperDuration, object timeDurationID, object startDate, object endDate, object description,
			DB_PEMRSavingMode savingMode)
		{
			if (medicationID == null || dosageID == null)
				return null;

			VisitTiming_Medication medication = null;
			switch (savingMode)
			{
				case DB_PEMRSavingMode.PostponeSaving:
					return CreateNew_VisitTiming_Medication(medicationID, dosageID, timesperDuration, timeDurationID,
						startDate, endDate, description);
				case DB_PEMRSavingMode.SaveImmediately:
					medication = CreateNew_VisitTiming_Medication(ActivePEMRObject.Active_VisitTiming, medicationID, dosageID,
						timesperDuration, timeDurationID, startDate, endDate, description);
					if (medication == null || !Save_VisitTiming_Medication(medication))
						return null;
					return medication;
			}

			return null;
		}

		public static VisitTiming_Medication CreateNew_VisitTiming_Medication(VisitTiming visitTiming,
			object medicationID, object dosageID, object timesperDuration, object timeDurationID, object startDate,
			object endDate, object description)
		{
			if (visitTiming == null)
				return null;
			VisitTiming_Medication medication = CreateNew_VisitTiming_Medication(medicationID, dosageID,
				timesperDuration, timeDurationID, startDate, endDate, description);
			if (medication == null)
				return null;
			medication.VisitTimingID = visitTiming.ID;
			return medication;
		}

		public static VisitTiming_Medication CreateNew_VisitTiming_Medication(object medicationID, object dosageID,
			object timesperDuration, object timeDurationID, object startDate, object endDate, object description)
		{
			VisitTiming_Medication medication = DBCommon.CreateNewDBEntity<VisitTiming_Medication>();
			medication.Medication_CU_ID = Convert.ToInt32(medicationID);
			medication.Dose_CU_ID = Convert.ToInt32(dosageID);
			if (timeDurationID != null)
				medication.TimeDuration_P_ID = Convert.ToInt32(timeDurationID);
			if (timesperDuration != null)
				medication.TimesPerDuration = Convert.ToInt32(timesperDuration);
			if (startDate != null)
				medication.StartDate = Convert.ToDateTime(startDate);
			if (endDate != null)
				medication.EndDate = Convert.ToDateTime(endDate);
			if (description != null)
				medication.Description = description.ToString();
			medication.IsOnDuty = true;
			medication.InsertedBy = ActiveLoggedInUser.ID;
			medication.PEMRElementStatus = PEMRElementStatus.NewelyAdded;
			return medication;
		}

		#endregion

		#region Create Investiagations

		public static VisitTiming_InvestigationReservation CreateNew_VisitTiming_InvestigationReservation(
			 object investigationServiceID, object date, object description, DB_PEMRSavingMode savingMode)
		{
			if (investigationServiceID == null)
				return null;
			VisitTiming_InvestigationReservation investigationReservation = null;
			switch (savingMode)
			{
				case DB_PEMRSavingMode.PostponeSaving:
					return CreateNew_VisitTiming_InvestigationReservation(investigationServiceID, date, description);
				case DB_PEMRSavingMode.SaveImmediately:
					investigationReservation = CreateNew_VisitTiming_InvestigationReservation(
						ActivePEMRObject.Active_VisitTiming, investigationServiceID, date, description);
					if (investigationReservation == null ||
						!Save_VisitTiming_InvestigationReservation(investigationReservation))
						return null;
					return investigationReservation;
			}
			return null;
		}

		public static VisitTiming_InvestigationReservation CreateNew_VisitTiming_InvestigationReservation(
			VisitTiming visitTiming, object investigationServiceID, object date, object description)
		{
			if (investigationServiceID == null)
				return null;
			VisitTiming_InvestigationReservation investigationReservation =
				CreateNew_VisitTiming_InvestigationReservation(investigationServiceID, date, description);
			if (investigationReservation == null)
				return null;
			investigationReservation.VisitTimingID = visitTiming.ID;
			return investigationReservation;
		}

		public static VisitTiming_InvestigationReservation CreateNew_VisitTiming_InvestigationReservation(
			object investigationServiceID, object date, object description)
		{
			if (investigationServiceID == null)
				return null;

			VisitTiming_InvestigationReservation investigationReservation =
				DBCommon.CreateNewDBEntity<VisitTiming_InvestigationReservation>();
			investigationReservation.Service_CU_ID = Convert.ToInt32(investigationServiceID);
			if (date != null)
				investigationReservation.Date = Convert.ToDateTime(date);
			investigationReservation.InsertedBy = ActiveLoggedInUser.ID;
			investigationReservation.IsOnDuty = true;
			if (description != null)
				investigationReservation.Description = description.ToString();
			investigationReservation.PEMRElementStatus = PEMRElementStatus.NewelyAdded;
			return investigationReservation;
		}

		#endregion

		#region Create Lab

		public static VisitTiming_LabReservation CreateNew_VisitTiming_LabReservation(
			 object labServiceID, object date, object description, DB_PEMRSavingMode savingMode)
		{
			if (labServiceID == null)
				return null;
			VisitTiming_LabReservation labReservation = null;
			switch (savingMode)
			{
				case DB_PEMRSavingMode.PostponeSaving:
					return CreateNew_VisitTiming_LabReservation(labServiceID, date, description);
				case DB_PEMRSavingMode.SaveImmediately:
					labReservation = CreateNew_VisitTiming_LabReservation(
						ActivePEMRObject.Active_VisitTiming, labServiceID, date, description);
					if (labReservation == null ||
						!Save_VisitTiming_LabReservation(labReservation))
						return null;
					return labReservation;
			}
			return null;
		}

		public static VisitTiming_LabReservation CreateNew_VisitTiming_LabReservation(
			VisitTiming visitTiming, object labServiceID, object date, object description)
		{
			if (labServiceID == null)
				return null;
			VisitTiming_LabReservation labReservation =
				CreateNew_VisitTiming_LabReservation(labServiceID, date, description);
			if (labReservation == null)
				return null;
			labReservation.VisitTimingID = visitTiming.ID;
			return labReservation;
		}

		public static VisitTiming_LabReservation CreateNew_VisitTiming_LabReservation(
			object labServiceID, object date, object description)
		{
			if (labServiceID == null)
				return null;

			VisitTiming_LabReservation labReservation =
				DBCommon.CreateNewDBEntity<VisitTiming_LabReservation>();
			labReservation.Service_CU_ID = Convert.ToInt32(labServiceID);
			if (date != null)
				labReservation.Date = Convert.ToDateTime(date);
			labReservation.InsertedBy = ActiveLoggedInUser.ID;
			labReservation.IsOnDuty = true;
			if (description != null)
				labReservation.Description = description.ToString();
			labReservation.PEMRElementStatus = PEMRElementStatus.NewelyAdded;
			return labReservation;
		}

		#endregion

		#region Create Surgery

		public static VisitTiming_SurgeryReservation CreateNew_VisitTiming_SurgeryReservation(
			 object SurgeryServiceID, object date, object description, DB_PEMRSavingMode savingMode)
		{
			if (SurgeryServiceID == null)
				return null;
			VisitTiming_SurgeryReservation SurgeryReservation = null;
			switch (savingMode)
			{
				case DB_PEMRSavingMode.PostponeSaving:
					return CreateNew_VisitTiming_SurgeryReservation(SurgeryServiceID, date, description);
				case DB_PEMRSavingMode.SaveImmediately:
					SurgeryReservation = CreateNew_VisitTiming_SurgeryReservation(
						ActivePEMRObject.Active_VisitTiming, SurgeryServiceID, date, description);
					if (SurgeryReservation == null ||
						!Save_VisitTiming_SurgeryReservation(SurgeryReservation))
						return null;
					return SurgeryReservation;
			}
			return null;
		}

		public static VisitTiming_SurgeryReservation CreateNew_VisitTiming_SurgeryReservation(
			VisitTiming visitTiming, object SurgeryServiceID, object date, object description)
		{
			if (SurgeryServiceID == null)
				return null;
			VisitTiming_SurgeryReservation SurgeryReservation =
				CreateNew_VisitTiming_SurgeryReservation(SurgeryServiceID, date, description);
			if (SurgeryReservation == null)
				return null;
			SurgeryReservation.VisitTimingID = visitTiming.ID;
			return SurgeryReservation;
		}

		public static VisitTiming_SurgeryReservation CreateNew_VisitTiming_SurgeryReservation(
			object SurgeryServiceID, object date, object description)
		{
			if (SurgeryServiceID == null)
				return null;

			VisitTiming_SurgeryReservation SurgeryReservation =
				DBCommon.CreateNewDBEntity<VisitTiming_SurgeryReservation>();
			SurgeryReservation.Service_CU_ID = Convert.ToInt32(SurgeryServiceID);
			if (date != null)
				SurgeryReservation.Date = Convert.ToDateTime(date);
			SurgeryReservation.InsertedBy = ActiveLoggedInUser.ID;
			SurgeryReservation.IsOnDuty = true;
			if (description != null)
				SurgeryReservation.Description = description.ToString();
			SurgeryReservation.PEMRElementStatus = PEMRElementStatus.NewelyAdded;
			return SurgeryReservation;
		}

		#endregion

		#region VisitTiming_TreatmentPlan

		public static VisitTiming_TreatmentPlan CreateNew_VisitTiming_TreatmentPlan(
			IPEMR_TreatmentPlan treatmentPlanObject, DB_PEMRSavingMode savingMode)
		{
			if (treatmentPlanObject == null || treatmentPlanObject.TreatmentDetails == null ||
			    treatmentPlanObject.StepOrderIndex == null)
				return null;
			VisitTiming_TreatmentPlan visitTiming_TreatmentPLan = null;
			switch (savingMode)
			{
				case DB_PEMRSavingMode.PostponeSaving:
					return CreateNew_VisitTiming_TreatmentPlan(treatmentPlanObject);
				case DB_PEMRSavingMode.SaveImmediately:
					visitTiming_TreatmentPLan = CreateNew_VisitTiming_TreatmentPlan(
						ActivePEMRObject.Active_VisitTiming, treatmentPlanObject);
					if (visitTiming_TreatmentPLan == null || !Save_VisitTiming_TreatmentPlan(visitTiming_TreatmentPLan))
						return null;
					return visitTiming_TreatmentPLan;
			}
			return null;
		}

		public static VisitTiming_TreatmentPlan CreateNew_VisitTiming_TreatmentPlan(
			VisitTiming visitTiming, IPEMR_TreatmentPlan treatmentPlanObject)
		{
			if (visitTiming == null || treatmentPlanObject.TreatmentDetails == null ||
				treatmentPlanObject.StepOrderIndex == null)
				return null;
			VisitTiming_TreatmentPlan visitTiming_TreatmentPLan = CreateNew_VisitTiming_TreatmentPlan(treatmentPlanObject);
			if (visitTiming_TreatmentPLan == null)
				return null;
			visitTiming_TreatmentPLan.VisitTimingID = visitTiming.ID;
			return visitTiming_TreatmentPLan;
		}

		public static VisitTiming_TreatmentPlan CreateNew_VisitTiming_TreatmentPlan(IPEMR_TreatmentPlan treatmentPlanObject)
		{
			if (treatmentPlanObject.TreatmentDetails == null ||
				treatmentPlanObject.StepOrderIndex == null)
				return null;

			VisitTiming_TreatmentPlan visitTreatmentPlan = DBCommon.CreateNewDBEntity<VisitTiming_TreatmentPlan>();
			visitTreatmentPlan.Treatment = treatmentPlanObject.TreatmentDetails.ToString();
			visitTreatmentPlan.StepOrderIndex = Convert.ToInt32(treatmentPlanObject.StepOrderIndex);

			visitTreatmentPlan.IsOnDuty = true;
			visitTreatmentPlan.InsertedBy = ActiveLoggedInUser.ID;
			visitTreatmentPlan.PEMRElementStatus = PEMRElementStatus.NewelyAdded;
			return visitTreatmentPlan;
		}

		#endregion

		public static VisitTiming_Attachment CreateNewVisitTimingAttachement(int patientID, string imageName,
			string imagePath, DB_ImageType imageType, object description, int userID)
		{
			PatientAttachment_cu patientAttachment =
				MerkDBBusinessLogicEngine.CreateNewPatientAttachement(patientID, imageName, imagePath, imageType,
					description, userID);
			if (patientAttachment == null)
				return null;
			VisitTiming_Attachment visitTimingAttachement = CreateNew_VisitTiming_Attachment(patientAttachment, userID);
			return visitTimingAttachement;
		}

		public static VisitTiming_Attachment CreateNew_VisitTiming_Attachment(PatientAttachment_cu patientAttachment,
			int userID)
		{
			if (patientAttachment == null)
				return null;

			VisitTiming_Attachment visitTimingAttachement = DBCommon.CreateNewDBEntity<VisitTiming_Attachment>();
			visitTimingAttachement.IsOnDuty = true;
			visitTimingAttachement.PatientAttachment_cu = patientAttachment;
			visitTimingAttachement.InsertedBy = userID;
			visitTimingAttachement.PEMRElementStatus = PEMRElementStatus.NewelyAdded;
			return visitTimingAttachement;
		}

		public static VisitTiming_InvestigationResult CreateNew_VisitTiming_InvestigationResult(
			VisitTiming_Attachment visitTimingAttachement, VisitTiming_InvestigationReservation visitTimingReservation,
			int userID)
		{
			if (visitTimingAttachement == null)
				return null;

			VisitTiming_InvestigationResult investigationResult =
				DBCommon.CreateNewDBEntity<VisitTiming_InvestigationResult>();
			investigationResult.VisitTiming_Attachment = visitTimingAttachement;
			investigationResult.IsOnDuty = true;
			investigationResult.Description = null;
			investigationResult.InsertedBy = userID;
			visitTimingReservation.VisitTiming_InvestigationResult.Add(investigationResult);
			visitTimingReservation.PEMRElementStatus = PEMRElementStatus.NewelyAdded;
			return investigationResult;
		}

		public static VisitTiming_VitalSign CreateNew_VisitTiming_VitalSign(
			IPEMR_VitalSign vitalSingViewer, DB_PEMRSavingMode savingMode)
		{
			if (vitalSingViewer == null)
				return null;
			VisitTiming_VitalSign vitalSign = null;
			switch (savingMode)
			{
				case DB_PEMRSavingMode.PostponeSaving:
					return CreateNew_VisitTiming_VitalSign(vitalSingViewer);
				case DB_PEMRSavingMode.SaveImmediately:
					vitalSign =
						CreateNew_VisitTiming_VitalSign(ActivePEMRObject.Active_VisitTiming, vitalSingViewer);
					if (vitalSign == null ||
						!Save_VisitTiming_VitalSign(vitalSign))
						return null;
					return vitalSign;
			}
			return null;
		}

		public static VisitTiming_VitalSign CreateNew_VisitTiming_VitalSign(
			VisitTiming visitTiming, IPEMR_VitalSign vitalSingViewer)
		{
			if (visitTiming == null || vitalSingViewer == null)
				return null;
			VisitTiming_VitalSign vitalSign =
				CreateNew_VisitTiming_VitalSign(vitalSingViewer);
			if (vitalSign == null)
				return null;
			vitalSign.VisitTimingID = visitTiming.ID;
			return vitalSign;
		}

		public static VisitTiming_VitalSign CreateNew_VisitTiming_VitalSign(IPEMR_VitalSign vitalSingViewer)
		{
			if (vitalSingViewer.TakenDate == null || vitalSingViewer.TakenTime == null)
				return null;
			VisitTiming_VitalSign vitalSign = DBCommon.CreateNewDBEntity<VisitTiming_VitalSign>();
			vitalSign.TakenDate = Convert.ToDateTime(vitalSingViewer.TakenDate).Date;
			vitalSign.TakenTime = Convert.ToDateTime(vitalSingViewer.TakenTime);
			if (vitalSingViewer.GeneralDescription != null)
				vitalSign.GeneralDescription = vitalSingViewer.GeneralDescription.ToString();
			if (vitalSingViewer.Weight_Unit != null)
				vitalSign.WeightUnit_P_ID = Convert.ToInt32(vitalSingViewer.Weight_Unit);
			if (vitalSingViewer.Weight_Amount != null)
				vitalSign.WeightAmount = Convert.ToInt32(vitalSingViewer.Weight_Amount);
			if (vitalSingViewer.Height_Unit != null)
				vitalSign.HeightUnit_P_ID = Convert.ToInt32(vitalSingViewer.Height_Unit);
			if (vitalSingViewer.HeightAmount != null)
				vitalSign.HeightAmount = Convert.ToInt32(vitalSingViewer.HeightAmount);
			if (vitalSingViewer.Weight_Description != null)
				vitalSign.WeightDescription = vitalSingViewer.Weight_Description.ToString();
			if (vitalSingViewer.Temperature_Unit != null)
				vitalSign.TemperatureUnit_P_ID = Convert.ToInt32(vitalSingViewer.Temperature_Unit);
			if (vitalSingViewer.Temperature_Amount != null)
				vitalSign.TemperatureAmount = Convert.ToInt32(vitalSingViewer.Temperature_Amount);
			if (vitalSingViewer.Temperature_Description != null)
				vitalSign.TemperatureDescription = vitalSingViewer.Temperature_Description.ToString();
			if (vitalSingViewer.BloodPressure_AmountHigh != null)
				vitalSign.BloodPressureAmountHigh = Convert.ToInt32(vitalSingViewer.BloodPressure_AmountHigh);
			if (vitalSingViewer.BloodPressure_AmountLow != null)
				vitalSign.BloodPressureAmountLow = Convert.ToInt32(vitalSingViewer.BloodPressure_AmountLow);
			if (vitalSingViewer.Pulse_Amount != null)
				vitalSign.PulseAmount = Convert.ToInt32(vitalSingViewer.Pulse_Amount);
			if (vitalSingViewer.Pulse_Reg != null)
				vitalSign.PulseReg = Convert.ToInt32(vitalSingViewer.Pulse_Reg);
			if (vitalSingViewer.BloodPressure_Description != null)
				vitalSign.BloodPressureDescription = vitalSingViewer.BloodPressure_Description.ToString();
			if (vitalSingViewer.Respiration_Amount != null)
				vitalSign.RespirationAmount = Convert.ToInt32(vitalSingViewer.Respiration_Amount);
			if (vitalSingViewer.Oxygen_Amount != null)
				vitalSign.OxygenAmount = Convert.ToInt32(vitalSingViewer.Oxygen_Amount);
			if (vitalSingViewer.FIO2 != null)
				vitalSign.FIO2 = Convert.ToInt32(vitalSingViewer.FIO2);
			if (vitalSingViewer.SPO2_Amount != null)
				vitalSign.SPO2Amount = Convert.ToInt32(vitalSingViewer.SPO2_Amount);
			if (vitalSingViewer.Respiration_Description != null)
				vitalSign.RespirationDescription = vitalSingViewer.Respiration_Description.ToString();

			vitalSign.IsOnDuty = true;
			vitalSign.InsertedBy = ActiveLoggedInUser.ID;
			vitalSign.PEMRElementStatus = PEMRElementStatus.NewelyAdded;
			return vitalSign;
		}

		#endregion

		#region Add PEMR Object to VisitTiming

		public static bool AddVisitTiming_TreatmentPlan(VisitTiming visitTiming,
			List<VisitTiming_TreatmentPlan> listToBeAdded)
		{
			if (visitTiming == null || listToBeAdded == null || listToBeAdded.Count == 0)
				return false;
			foreach (VisitTiming_TreatmentPlan visitTimingTreatmentPlan in listToBeAdded.FindAll(item =>
				Convert.ToInt32(item.PEMRElementStatus).Equals(Convert.ToInt32(PEMRElementStatus.NewelyAdded))))
				visitTiming.VisitTiming_TreatmentPlan.Add(visitTimingTreatmentPlan);
			foreach (VisitTiming_TreatmentPlan visitTimingTreatmentPlan in listToBeAdded.FindAll(item =>
				Convert.ToInt32(item.PEMRElementStatus).Equals(Convert.ToInt32(PEMRElementStatus.Removed))))
			{
				visitTimingTreatmentPlan.DBCommonTransactionType = DB_CommonTransactionType.DeleteExisting;
				visitTiming.VisitTiming_TreatmentPlan.Remove(visitTimingTreatmentPlan);
			}
			return true;
		}

		public static bool AddVisitTiming_InvestigationReservation(VisitTiming visitTiming,
			List<VisitTiming_InvestigationReservation> listToBeAdded)
		{
			if (visitTiming == null || listToBeAdded == null || listToBeAdded.Count == 0)
				return false;
			foreach (VisitTiming_InvestigationReservation visitTimingInvestigationReservation in listToBeAdded.FindAll(
				item => Convert.ToInt32(item.PEMRElementStatus).Equals(Convert.ToInt32(PEMRElementStatus.NewelyAdded))))
				visitTiming.VisitTiming_InvestigationReservation.Add(visitTimingInvestigationReservation);
			return true;
		}

		public static bool AddVisitTiming_LabReservation(VisitTiming visitTiming,
			List<VisitTiming_LabReservation> listToBeAdded)
		{
			if (visitTiming == null || listToBeAdded == null || listToBeAdded.Count == 0)
				return false;
			foreach (VisitTiming_LabReservation visitTimingLabReservation in listToBeAdded.FindAll(item =>
				Convert.ToInt32(item.PEMRElementStatus).Equals(Convert.ToInt32(PEMRElementStatus.NewelyAdded))))
				visitTiming.VisitTiming_LabReservation.Add(visitTimingLabReservation);
			return true;
		}

		public static bool AddVisitTiming_SurgeryReservation(VisitTiming visitTiming,
			List<VisitTiming_SurgeryReservation> listToBeAdded)
		{
			if (visitTiming == null || listToBeAdded == null || listToBeAdded.Count == 0)
				return false;
			foreach (VisitTiming_SurgeryReservation visitTimingSurgeryReservation in listToBeAdded.FindAll(item =>
				Convert.ToInt32(item.PEMRElementStatus).Equals(Convert.ToInt32(PEMRElementStatus.NewelyAdded))))
				visitTiming.VisitTiming_SurgeryReservation.Add(visitTimingSurgeryReservation);
			return true;
		}

		public static bool AddVisitTiming_Medication(VisitTiming visitTiming,
			List<VisitTiming_Medication> listToBeAdded)
		{
			if (visitTiming == null || listToBeAdded == null || listToBeAdded.Count == 0)
				return false;
			foreach (VisitTiming_Medication visitTimingMedication in listToBeAdded.FindAll(item =>
				Convert.ToInt32(item.PEMRElementStatus).Equals(Convert.ToInt32(PEMRElementStatus.NewelyAdded))))
			{
				visitTimingMedication.VisitTimingID = visitTiming.ID;
				visitTiming.VisitTiming_Medication.Add(visitTimingMedication);
			}

			foreach (VisitTiming_Medication visitTimingMedication in listToBeAdded.FindAll(item =>
				Convert.ToInt32(item.PEMRElementStatus).Equals(Convert.ToInt32(PEMRElementStatus.Removed))))
			{
				visitTimingMedication.DBCommonTransactionType = DB_CommonTransactionType.DeleteExisting;
				visitTiming.VisitTiming_Medication.Remove(visitTimingMedication);
			}
			return true;
		}

		public static bool AddVisitTiming_Attachment(VisitTiming visitTiming, List<VisitTiming_Attachment> visitTimingList)
		{
			if (visitTiming == null || visitTimingList == null || visitTimingList.Count == 0)
				return false;
			foreach (VisitTiming_Attachment attachement in visitTimingList.FindAll(item =>
				Convert.ToInt32(item.PEMRElementStatus).Equals(Convert.ToInt32(PEMRElementStatus.NewelyAdded))))
				visitTiming.VisitTiming_Attachment.Add(attachement);
			return true;
		}

		public static bool AddVisitTiming_MainAnteriorSegmentSign(VisitTiming visitTiming,
			List<VisitTiming_MainAnteriorSegmentSign> visitTimingMainDiagnosis)
		{
			if (visitTiming == null || visitTimingMainDiagnosis == null || visitTimingMainDiagnosis.Count == 0)
				return false;
			foreach (VisitTiming_MainAnteriorSegmentSign mainDiagnosis in visitTimingMainDiagnosis.FindAll(item =>
				Convert.ToInt32(item.PEMRElementStatus).Equals(Convert.ToInt32(PEMRElementStatus.NewelyAdded))))
				visitTiming.VisitTiming_MainAnteriorSegmentSign.Add(mainDiagnosis);
			return true;
		}

		public static bool AddVisitTiming_VitalSign(VisitTiming visitTiming,
			List<VisitTiming_VitalSign> visitTimingVitalSigns)
		{
			if (visitTiming == null || visitTimingVitalSigns == null || visitTimingVitalSigns.Count == 0)
				return false;
			foreach (VisitTiming_VitalSign vitalSign in visitTimingVitalSigns.FindAll(item =>
				Convert.ToInt32(item.PEMRElementStatus).Equals(Convert.ToInt32(PEMRElementStatus.NewelyAdded))))
				visitTiming.VisitTiming_VitalSign.Add(vitalSign);
			return true;
		}

		#endregion

		#region Save PEMR VisitTiming Objects

		public static bool Save_VisitTiming(VisitTiming itemToBeSaved)
		{
			if (itemToBeSaved == null)
				return false;
			return itemToBeSaved.SaveChanges();
		}

		public static bool Save_VisitTiming_MainAnteriorSegmentSign(VisitTiming_MainAnteriorSegmentSign itemToBeSaved)
		{
			if (itemToBeSaved == null)
				return false;
			return itemToBeSaved.SaveChanges();
		}

		public static bool Save_VisitTiming_AnteriorSegmentSign(VisitTiming_AnteriorSegmentSign itemToBeSaved)
		{
			if (itemToBeSaved == null)
				return false;
			return itemToBeSaved.SaveChanges();
		}

		public static bool Save_VisitTiming_MainPosteriorSegmentSign(VisitTiming_MainPosteriorSegmentSign itemToBeSaved)
		{
			if (itemToBeSaved == null)
				return false;
			return itemToBeSaved.SaveChanges();
		}

		public static bool Save_VisitTiming_PosteriorSegmentSign(VisitTiming_PosteriorSegmentSign itemToBeSaved)
		{
			if (itemToBeSaved == null)
				return false;
			return itemToBeSaved.SaveChanges();
		}

		public static bool Save_VisitTiming_MainAdnexaSegmentSign(VisitTiming_MainAdnexaSegmentSign itemToBeSaved)
		{
			if (itemToBeSaved == null)
				return false;
			return itemToBeSaved.SaveChanges();
		}

		public static bool Save_VisitTiming_AdnexaSegmentSign(VisitTiming_AdnexaSegmentSign itemToBeSaved)
		{
			if (itemToBeSaved == null)
				return false;
			return itemToBeSaved.SaveChanges();
		}

		public static bool Save_VisitTiming_MainEOMSign(VisitTiming_MainEOMSign itemToBeSaved)
		{
			if (itemToBeSaved == null)
				return false;
			return itemToBeSaved.SaveChanges();
		}

		public static bool Save_VisitTiming_EOMSign(VisitTiming_EOMSign itemToBeSaved)
		{
			if (itemToBeSaved == null)
				return false;
			return itemToBeSaved.SaveChanges();
		}

		public static bool Save_VisitTiming_EOMReading(VisitTiming_EOMReading itemToBeSaved)
		{
			if (itemToBeSaved == null)
				return false;
			return itemToBeSaved.SaveChanges();
		}

		public static bool Save_VisitTiming_Attachment(VisitTiming_Attachment itemToBeSaved)
		{
			if (itemToBeSaved == null)
				return false;
			return itemToBeSaved.SaveChanges();
		}

		public static bool Save_VisitTiming_MainDiagnosis(VisitTiming_MainDiagnosis itemToBeSaved)
		{
			if (itemToBeSaved == null)
				return false;
			return itemToBeSaved.SaveChanges();
		}

		public static bool Save_VisitTiming_Diagnosis(VisitTiming_Diagnosis itemToBeSaved)
		{
			if (itemToBeSaved == null)
				return false;
			return itemToBeSaved.SaveChanges();
		}

		public static bool Save_VisitTiming_MainSymptoms(VisitTiming_MainSymptoms itemToBeSaved)
		{
			if (itemToBeSaved == null)
				return false;
			return itemToBeSaved.SaveChanges();
		}

		public static bool Save_VisitTiming_Symptoms(VisitTiming_Symptoms itemToBeSaved)
		{
			if (itemToBeSaved == null)
				return false;
			return itemToBeSaved.SaveChanges();
		}

		public static bool Save_VisitTiming_InvestigationReservation(VisitTiming_InvestigationReservation itemToBeSaved)
		{
			if (itemToBeSaved == null)
				return false;
			return itemToBeSaved.SaveChanges();
		}

		public static bool Save_VisitTiming_InvestigationResult(VisitTiming_InvestigationResult itemToBeSaved)
		{
			if (itemToBeSaved == null)
				return false;
			return itemToBeSaved.SaveChanges();
		}

		public static bool Save_VisitTiming_LabReservation(VisitTiming_LabReservation itemToBeSaved)
		{
			if (itemToBeSaved == null)
				return false;
			return itemToBeSaved.SaveChanges();
		}

		public static bool Save_VisitTiming_VisitTiming_LabResult(VisitTiming_LabResult itemToBeSaved)
		{
			if (itemToBeSaved == null)
				return false;
			return itemToBeSaved.SaveChanges();
		}

		public static bool Save_VisitTiming_SurgeryReservation(VisitTiming_SurgeryReservation itemToBeSaved)
		{
			if (itemToBeSaved == null)
				return false;
			return itemToBeSaved.SaveChanges();
		}

		public static bool Save_VisitTiming_SurgeryResult(VisitTiming_SurgeryResult itemToBeSaved)
		{
			if (itemToBeSaved == null)
				return false;
			return itemToBeSaved.SaveChanges();
		}

		public static bool Save_VisitTiming_SocialHistory(VisitTiming_SocialHistory itemToBeSaved)
		{
			if (itemToBeSaved == null)
				return false;
			return itemToBeSaved.SaveChanges();
		}

		public static bool Save_VisitTiming_MedicalHistory(VisitTiming_MedicalHistory itemToBeSaved)
		{
			if (itemToBeSaved == null)
				return false;
			return itemToBeSaved.SaveChanges();
		}

		public static bool Save_VisitTiming_TreatmentPlan(VisitTiming_TreatmentPlan itemToBeSaved)
		{
			if (itemToBeSaved == null)
				return false;
			return itemToBeSaved.SaveChanges();
		}

		public static bool Save_VisitTiming_VitalSign(VisitTiming_VitalSign itemToBeSaved)
		{
			if (itemToBeSaved == null)
				return false;
			return itemToBeSaved.SaveChanges();
		}

		public static bool Save_VisitTiming_VisionRefractionReading(VisitTiming_VisionRefractionReading itemToBeSaved)
		{
			if (itemToBeSaved == null)
				return false;
			return itemToBeSaved.SaveChanges();
		}

		public static bool Save_VisitTiming_Pupillary(VisitTiming_Pupillary itemToBeSaved)
		{
			if (itemToBeSaved == null)
				return false;
			return itemToBeSaved.SaveChanges();
		}

		public static bool Save_VisitTiming_Medication(VisitTiming_Medication itemToBeSaved)
		{
			if (itemToBeSaved == null)
				return false;
			return itemToBeSaved.SaveChanges();
		}

		#endregion

		#region Update PEMR VisitTiming Objects

		public static bool UpdateAll(PEMRObject pemrObject)
		{
			if (pemrObject == null)
				return false;

			if (pemrObject.List_VisitTiming_MainAdnexaSegmentSign != null && pemrObject.List_VisitTiming_MainAdnexaSegmentSign.Count > 0)
				foreach (VisitTiming_MainAdnexaSegmentSign itemToBeSaved in pemrObject.List_VisitTiming_MainAdnexaSegmentSign)
					Update_VisitTiming_MainAdnexaSegmentSign(PEMR_Adnexa, itemToBeSaved);

			if (pemrObject.List_VisitTiming_MainAnteriorSegmentSign != null && pemrObject.List_VisitTiming_MainAnteriorSegmentSign.Count > 0)
				foreach (VisitTiming_MainAnteriorSegmentSign itemToBeSaved in pemrObject.List_VisitTiming_MainAnteriorSegmentSign)
					Update_VisitTiming_MainAnteriorSegmentSign(PEMR_AnteriorSegmentSign, itemToBeSaved);

			if (pemrObject.List_VisitTiming_MainEOMSign != null && pemrObject.List_VisitTiming_MainEOMSign.Count > 0)
				foreach (VisitTiming_MainEOMSign itemToBeSaved in pemrObject.List_VisitTiming_MainEOMSign)
					Update_VisitTiming_MainEOMSign(PEMR_EOMSign, itemToBeSaved);

			if (pemrObject.List_VisitTiming_MedicalHistory != null && pemrObject.List_VisitTiming_MedicalHistory.Count > 0)
				foreach (VisitTiming_MedicalHistory itemToBeSaved in pemrObject.List_VisitTiming_MedicalHistory)
					Update_VisitTiming_MedicalHistory(PEMR_MedicalHistory, itemToBeSaved);

			if (pemrObject.List_VisitTiming_SocialHistory != null && pemrObject.List_VisitTiming_SocialHistory.Count > 0)
				foreach (VisitTiming_SocialHistory itemToBeSaved in pemrObject.List_VisitTiming_SocialHistory)
					Update_VisitTiming_SocialHistory(PEMR_SocialHistory, itemToBeSaved);

			UpdateVisitTimming(ActivePEMRObject.Active_VisitTiming, DateTime.Now);

			return true;
		}

		public static bool UpdateVisitTimming(VisitTiming visitTiming, DateTime signOutDateTime)
		{
			if (visitTiming == null)
				return false;

			visitTiming.SignOutDateTime = signOutDateTime;
			visitTiming.InsertedBy = ActiveLoggedInUser.ID;
			visitTiming.DBCommonTransactionType = DB_CommonTransactionType.UpdateExisting;

			return visitTiming.SaveChanges();
		}

		#region Update Anterior Segment

		public static bool Update_VisitTiming_MainAnteriorSegmentSign(List<VisitTiming_MainAnteriorSegmentSign> list)
		{
			if (list == null || list.Count == 0)
				return false;

			foreach (VisitTiming_MainAnteriorSegmentSign visitTimingMainDiagnosis in list.FindAll(item =>
				Convert.ToInt32(item.PEMRElementStatus).Equals(Convert.ToInt32(PEMRElementStatus.NewelyAdded))))
				visitTimingMainDiagnosis.SaveChanges();
			foreach (VisitTiming_MainAnteriorSegmentSign visitTimingMainDiagnosis in list.FindAll(item =>
				Convert.ToInt32(item.PEMRElementStatus).Equals(Convert.ToInt32(PEMRElementStatus.Removed))))
			{
				visitTimingMainDiagnosis.DBCommonTransactionType = DB_CommonTransactionType.DeleteExisting;
				visitTimingMainDiagnosis.SaveChanges();
			}
			return true;
		}

		public static bool Update_VisitTiming_MainAnteriorSegmentSign(
			VisitTiming_MainAnteriorSegmentSign visitTimingMainDiagnosis)
		{
			if (visitTimingMainDiagnosis == null)
				return false;

			visitTimingMainDiagnosis.DBCommonTransactionType = DB_CommonTransactionType.UpdateExisting;
			return visitTimingMainDiagnosis.SaveChanges();
		}

		public static bool Update_VisitTiming_MainAnteriorSegmentSign(IPEMR_AnteriorSegmentSign anteriorSegmentObject,
			VisitTiming_MainAnteriorSegmentSign visitTimingMainanterior)
		{
			if (visitTimingMainanterior == null || anteriorSegmentObject == null)
				return false;
			if (anteriorSegmentObject.FurtherDetails_OD != null)
				visitTimingMainanterior.GeneralDescription_OD = anteriorSegmentObject.FurtherDetails_OD.ToString();
			if (anteriorSegmentObject.FurtherDetails_OS != null)
				visitTimingMainanterior.GeneralDescription_OS = anteriorSegmentObject.FurtherDetails_OS.ToString();
			visitTimingMainanterior.IsOnDuty = true;
			visitTimingMainanterior.InsertedBy = ActiveLoggedInUser.ID;
			visitTimingMainanterior.PEMRElementStatus = PEMRElementStatus.NewelyAdded;
			visitTimingMainanterior.DBCommonTransactionType = DB_CommonTransactionType.UpdateExisting;
			return visitTimingMainanterior.SaveChanges();
		}

		#endregion

		#region VisitTiming_AnteriorSegmentSign

		public static bool Update_VisitTiming_AnteriorSegmentSign(List<VisitTiming_AnteriorSegmentSign> list)
		{
			if (list == null || list.Count == 0)
				return false;

			foreach (VisitTiming_AnteriorSegmentSign visitTiming_AnteriorSegment in list.FindAll(item =>
				Convert.ToInt32(item.PEMRElementStatus).Equals(Convert.ToInt32(PEMRElementStatus.NewelyAdded))))
			{
				if (ActivePEMRObject != null && ActivePEMRObject.List_VisitTiming_MainAnteriorSegmentSign != null &&
					ActivePEMRObject.List_VisitTiming_MainAnteriorSegmentSign.Count > 0)
					visitTiming_AnteriorSegment.VisitTiming_AnteriorSegmentSignID =
						ActivePEMRObject.List_VisitTiming_MainAnteriorSegmentSign[0].ID;
				visitTiming_AnteriorSegment.SaveChanges();
			}
			foreach (VisitTiming_AnteriorSegmentSign visitTiming_AnteriorSegment in list.FindAll(item =>
				Convert.ToInt32(item.PEMRElementStatus).Equals(Convert.ToInt32(PEMRElementStatus.Removed))))
			{
				visitTiming_AnteriorSegment.DBCommonTransactionType = DB_CommonTransactionType.DeleteExisting;
				visitTiming_AnteriorSegment.SaveChanges();
			}
			return true;
		}

		#endregion

		#region Update Posterior Segment

		public static bool Update_VisitTiming_MainPosteriorSegmentSign(IPEMR_PosteriorSegment posteriorSegmentObject,
			VisitTiming_MainPosteriorSegmentSign visitTimingMainPosterior)
		{
			if (visitTimingMainPosterior == null || posteriorSegmentObject == null)
				return false;
			if (posteriorSegmentObject.FurtherDetails_OD != null)
				visitTimingMainPosterior.GeneralDescription_OD = posteriorSegmentObject.FurtherDetails_OD.ToString();
			if (posteriorSegmentObject.FurtherDetails_OS != null)
				visitTimingMainPosterior.GeneralDescription_OS = posteriorSegmentObject.FurtherDetails_OS.ToString();
			visitTimingMainPosterior.IsOnDuty = true;
			visitTimingMainPosterior.InsertedBy = ActiveLoggedInUser.ID;
			visitTimingMainPosterior.PEMRElementStatus = PEMRElementStatus.NewelyAdded;
			visitTimingMainPosterior.DBCommonTransactionType = DB_CommonTransactionType.UpdateExisting;
			return visitTimingMainPosterior.SaveChanges();
		}

		#endregion

		#region VisitTiming_PosteriorSegmentSign

		public static bool Update_VisitTiming_PosteriorSegmentSign(List<VisitTiming_PosteriorSegmentSign> list)
		{
			if (list == null || list.Count == 0)
				return false;

			foreach (VisitTiming_PosteriorSegmentSign visitTiming_PosteriorSegment in list.FindAll(item =>
				Convert.ToInt32(item.PEMRElementStatus).Equals(Convert.ToInt32(PEMRElementStatus.NewelyAdded))))
			{
				if (ActivePEMRObject != null && ActivePEMRObject.List_VisitTiming_MainPosteriorSegmentSign != null &&
					ActivePEMRObject.List_VisitTiming_MainPosteriorSegmentSign.Count > 0)
					visitTiming_PosteriorSegment.VisitTiming_MainPsoteriorSegmentSignID =
						ActivePEMRObject.List_VisitTiming_MainPosteriorSegmentSign[0].ID;
				visitTiming_PosteriorSegment.SaveChanges();
			}
			foreach (VisitTiming_PosteriorSegmentSign visitTiming_PosteriorSegment in list.FindAll(item =>
				Convert.ToInt32(item.PEMRElementStatus).Equals(Convert.ToInt32(PEMRElementStatus.Removed))))
			{
				visitTiming_PosteriorSegment.DBCommonTransactionType = DB_CommonTransactionType.DeleteExisting;
				visitTiming_PosteriorSegment.SaveChanges();
			}
			return true;
		}

		#endregion

		#region Update Adnexa

		public static bool Update_VisitTiming_MainAdnexaSegmentSign(IPEMR_Adnexa adnexaObject,
			VisitTiming_MainAdnexaSegmentSign visitTimingAdnexa)
		{
			if (visitTimingAdnexa == null || adnexaObject == null)
				return false;
			if (adnexaObject.FurtherDetails_OD != null)
				visitTimingAdnexa.GeneralDescription_OD = adnexaObject.FurtherDetails_OD.ToString();
			if (adnexaObject.FurtherDetails_OS != null)
				visitTimingAdnexa.GeneralDescription_OS = adnexaObject.FurtherDetails_OS.ToString();
			visitTimingAdnexa.IsOnDuty = true;
			visitTimingAdnexa.InsertedBy = ActiveLoggedInUser.ID;
			visitTimingAdnexa.PEMRElementStatus = PEMRElementStatus.NewelyAdded;
			visitTimingAdnexa.DBCommonTransactionType = DB_CommonTransactionType.UpdateExisting;
			return visitTimingAdnexa.SaveChanges();
		}

		#endregion

		#region VisitTiming_AdnexaSegmentSign

		public static bool Update_VisitTiming_AdnexaSegmentSign(List<VisitTiming_AdnexaSegmentSign> list)
		{
			if (list == null || list.Count == 0)
				return false;

			foreach (VisitTiming_AdnexaSegmentSign visitTiming_AdenxaSegment in list.FindAll(item =>
				Convert.ToInt32(item.PEMRElementStatus).Equals(Convert.ToInt32(PEMRElementStatus.NewelyAdded))))
			{
				if (ActivePEMRObject != null && ActivePEMRObject.List_VisitTiming_MainAdnexaSegmentSign != null &&
					ActivePEMRObject.List_VisitTiming_MainAdnexaSegmentSign.Count > 0)
					visitTiming_AdenxaSegment.VisitTiming_MainAdnexaSegmentSignID =
						ActivePEMRObject.List_VisitTiming_MainAdnexaSegmentSign[0].ID;
				visitTiming_AdenxaSegment.SaveChanges();
			}
			foreach (VisitTiming_AdnexaSegmentSign visitTiming_AdenxaSegment in list.FindAll(item =>
				Convert.ToInt32(item.PEMRElementStatus).Equals(Convert.ToInt32(PEMRElementStatus.Removed))))
			{
				visitTiming_AdenxaSegment.DBCommonTransactionType = DB_CommonTransactionType.DeleteExisting;
				visitTiming_AdenxaSegment.SaveChanges();
			}
			return true;
		}

		#endregion

		#region VisitTiming_Pupillary

		public static bool Update_VisitTiming_Pupillary(IPEMR_Pupillary visitTimingPupillary,
			VisitTiming_Pupillary visitTiming_Pupillary)
		{
			if (visitTimingPupillary == null || visitTimingPupillary == null)
				return false;
			if (visitTimingPupillary.IsNoAbnormalitiesFound_OD != null)
				visitTiming_Pupillary.IsNoAbnormalitiesFound_OD =
					Convert.ToBoolean(visitTimingPupillary.IsNoAbnormalitiesFound_OD);
			if (visitTimingPupillary.IsNoAbnormalitiesFound_OS != null)
				visitTiming_Pupillary.IsNoAbnormalitiesFound_OS =
					Convert.ToBoolean(visitTimingPupillary.IsNoAbnormalitiesFound_OS);
			if (visitTimingPupillary.PupillaryAbnormalities_CU_ID_OD != null)
				visitTiming_Pupillary.PupillaryAbnormalities_CU_ID_OD =
					Convert.ToInt32(visitTimingPupillary.PupillaryAbnormalities_CU_ID_OD);
			if (visitTimingPupillary.PupillaryAbnormalities_CU_ID_OS != null)
				visitTiming_Pupillary.PupillaryAbnormalities_CU_ID_OS =
					Convert.ToInt32(visitTimingPupillary.PupillaryAbnormalities_CU_ID_OS);
			if (visitTimingPupillary.IsRAPD_OD != null)
				visitTiming_Pupillary.IsRAPD_OD =
					Convert.ToBoolean(visitTimingPupillary.IsRAPD_OD);
			if (visitTimingPupillary.IsRAPD_OS != null)
				visitTiming_Pupillary.IsRAPD_OS =
					Convert.ToBoolean(visitTimingPupillary.IsRAPD_OS);
			if (visitTimingPupillary.PupillaryRAPDGradingScale_P_ID_OD != null)
				visitTiming_Pupillary.PupillaryRAPDGradingScale_P_ID_OD =
					Convert.ToInt32(visitTimingPupillary.PupillaryRAPDGradingScale_P_ID_OD);
			if (visitTimingPupillary.PupillaryRAPDGradingScale_P_ID_OS != null)
				visitTiming_Pupillary.PupillaryRAPDGradingScale_P_ID_OS =
					Convert.ToInt32(visitTimingPupillary.PupillaryRAPDGradingScale_P_ID_OS);
			if (visitTimingPupillary.PupillaryRAPDCauses_CU_ID_OD != null)
				visitTiming_Pupillary.PupillaryRAPDCauses_CU_ID_OD =
					Convert.ToInt32(visitTimingPupillary.PupillaryRAPDCauses_CU_ID_OD);
			if (visitTimingPupillary.PupillaryRAPDCauses_CU_ID_OS != null)
				visitTiming_Pupillary.PupillaryRAPDCauses_CU_ID_OS =
					Convert.ToInt32(visitTimingPupillary.PupillaryRAPDCauses_CU_ID_OS);
			if (visitTimingPupillary.PupillarySize_P_ID_OD != null)
				visitTiming_Pupillary.PupillarySize_P_ID_OD =
					Convert.ToInt32(visitTimingPupillary.PupillarySize_P_ID_OD);
			if (visitTimingPupillary.PupillarySize_P_ID_OS != null)
				visitTiming_Pupillary.PupillarySize_P_ID_OS =
					Convert.ToInt32(visitTimingPupillary.PupillarySize_P_ID_OS);
			if (visitTimingPupillary.PupillaryShape_P_OD != null)
				visitTiming_Pupillary.PupillaryShape_P_OD =
					Convert.ToInt32(visitTimingPupillary.PupillaryShape_P_OD);
			if (visitTimingPupillary.PupillaryShape_P_OS != null)
				visitTiming_Pupillary.PupillaryShape_P_OS =
					Convert.ToInt32(visitTimingPupillary.PupillaryShape_P_OS);
			if (visitTimingPupillary.Scotopic_OD != null)
				visitTiming_Pupillary.Scotopic_OD =
					Convert.ToDouble(visitTimingPupillary.Scotopic_OD);
			if (visitTimingPupillary.HighPhotopic_OD != null)
				visitTiming_Pupillary.HighPhotopic_OD =
					Convert.ToDouble(visitTimingPupillary.HighPhotopic_OD);
			if (visitTimingPupillary.LowPhotopic_OD != null)
				visitTiming_Pupillary.LowPhotopic_OD =
					Convert.ToDouble(visitTimingPupillary.LowPhotopic_OD);
			if (visitTimingPupillary.HighMesopic_OD != null)
				visitTiming_Pupillary.HighMesopic_OD =
					Convert.ToDouble(visitTimingPupillary.HighMesopic_OD);
			if (visitTimingPupillary.LowMesopic_OD != null)
				visitTiming_Pupillary.LowMesopic_OD =
					Convert.ToDouble(visitTimingPupillary.LowMesopic_OD);
			if (visitTimingPupillary.Scotopic_OS != null)
				visitTiming_Pupillary.Scotopic_OS =
					Convert.ToDouble(visitTimingPupillary.Scotopic_OS);
			if (visitTimingPupillary.HighPhotopic_OS != null)
				visitTiming_Pupillary.HighPhotopic_OS =
					Convert.ToDouble(visitTimingPupillary.HighPhotopic_OS);
			if (visitTimingPupillary.LowPhotopic_OS != null)
				visitTiming_Pupillary.LowPhotopic_OS =
					Convert.ToDouble(visitTimingPupillary.LowPhotopic_OS);
			if (visitTimingPupillary.HighMesopic_OS != null)
				visitTiming_Pupillary.HighMesopic_OS =
					Convert.ToDouble(visitTimingPupillary.HighMesopic_OS);
			if (visitTimingPupillary.LowMesopic_OS != null)
				visitTiming_Pupillary.LowMesopic_OS =
					Convert.ToDouble(visitTimingPupillary.LowMesopic_OS);
			if (visitTimingPupillary.FurtherDetails_OD != null)
				visitTiming_Pupillary.FurtherDetails_OD = visitTimingPupillary.FurtherDetails_OD.ToString();
			if (visitTimingPupillary.FurtherDetails_OS != null)
				visitTiming_Pupillary.FurtherDetails_OS = visitTimingPupillary.FurtherDetails_OS.ToString();
			visitTiming_Pupillary.IsOnDuty = true;
			visitTiming_Pupillary.InsertedBy = ActiveLoggedInUser.ID;
			visitTiming_Pupillary.PEMRElementStatus = PEMRElementStatus.NewelyAdded;
			visitTiming_Pupillary.DBCommonTransactionType = DB_CommonTransactionType.UpdateExisting;
			return visitTiming_Pupillary.SaveChanges();
		}

		#endregion

		#region VisitTiming_Pupillary

		public static bool Update_VisitTiming_Pupillary(VisitTiming_Pupillary visitTimingPupillary)
		{
			if (visitTimingPupillary == null)
				return false;

			visitTimingPupillary.DBCommonTransactionType = DB_CommonTransactionType.UpdateExisting;
			return visitTimingPupillary.SaveChanges();
		}

		#endregion

		#region VisitTiming_MainEOMSign

		public static bool Update_VisitTiming_MainEOMSign(IPEMR_EOMSign eomSign,
			VisitTiming_MainEOMSign visitTiming_MainEOMSign)
		{
			if (visitTiming_MainEOMSign == null || eomSign == null)
				return false;
			if (eomSign.FurtherDetails_OD != null)
				visitTiming_MainEOMSign.GeneralDescription_OD = eomSign.FurtherDetails_OD.ToString();
			if (eomSign.FurtherDetails_OS != null)
				visitTiming_MainEOMSign.GeneralDescription_OS = eomSign.FurtherDetails_OS.ToString();
			visitTiming_MainEOMSign.IsOnDuty = true;
			visitTiming_MainEOMSign.InsertedBy = ActiveLoggedInUser.ID;
			visitTiming_MainEOMSign.PEMRElementStatus = PEMRElementStatus.NewelyAdded;
			visitTiming_MainEOMSign.DBCommonTransactionType = DB_CommonTransactionType.UpdateExisting;
			return visitTiming_MainEOMSign.SaveChanges();
		}

		#endregion

		#region VisitTiming_EOMSign

		public static bool Update_VisitTiming_EOMSign(List<VisitTiming_EOMSign> list)
		{
			if (list == null || list.Count == 0)
				return false;

			foreach (VisitTiming_EOMSign visitTiming_EOMSign in list.FindAll(item =>
				Convert.ToInt32(item.PEMRElementStatus).Equals(Convert.ToInt32(PEMRElementStatus.NewelyAdded))))
			{
				if (ActivePEMRObject != null && ActivePEMRObject.List_VisitTiming_MainEOMSign != null &&
					ActivePEMRObject.List_VisitTiming_MainEOMSign.Count > 0)
					visitTiming_EOMSign.VisitTiming_EOMMainSignID = ActivePEMRObject.List_VisitTiming_MainEOMSign[0].ID;
				visitTiming_EOMSign.SaveChanges();
			}

			foreach (VisitTiming_EOMSign visitTiming_EOMSign in list.FindAll(item =>
				Convert.ToInt32(item.PEMRElementStatus).Equals(Convert.ToInt32(PEMRElementStatus.Removed))))
			{
				visitTiming_EOMSign.DBCommonTransactionType = DB_CommonTransactionType.DeleteExisting;
				visitTiming_EOMSign.SaveChanges();
			}
			return true;
		}

		#endregion

		#region VisitTiming_MainDiagnosis

		public static bool Update_VisitTiming_MainDiagnosis(IPEMR_Diagnosis pemrDiagnosis,
			VisitTiming_MainDiagnosis visitTiming_MainDiagnosis)
		{
			if (visitTiming_MainDiagnosis == null || pemrDiagnosis == null)
				return false;
			if (pemrDiagnosis.FurtherDetails != null)
				visitTiming_MainDiagnosis.GeneralDescription = pemrDiagnosis.FurtherDetails.ToString();

			visitTiming_MainDiagnosis.DiagnosisType_P_ID = (int)pemrDiagnosis.DiagnosisType;
			visitTiming_MainDiagnosis.IsOnDuty = true;
			visitTiming_MainDiagnosis.InsertedBy = ActiveLoggedInUser.ID;
			visitTiming_MainDiagnosis.PEMRElementStatus = PEMRElementStatus.NewelyAdded;
			visitTiming_MainDiagnosis.DBCommonTransactionType = DB_CommonTransactionType.UpdateExisting;
			return visitTiming_MainDiagnosis.SaveChanges();
		}

		#endregion

		#region VisitTiming_SocialHistory

		public static bool Update_VisitTiming_SocialHistory(IPEMR_SocialHistory socialHistory,
			VisitTiming_SocialHistory visitTiming_SocialHistory)
		{
			if (socialHistory == null || visitTiming_SocialHistory == null)
				return false;

			if (socialHistory.NegativeSocialHistory != null)
				visitTiming_SocialHistory.NegativeSocialHistory =
					Convert.ToBoolean(socialHistory.NegativeSocialHistory);
			if (socialHistory.GeneralDescription != null)
				visitTiming_SocialHistory.GeneralDescription = socialHistory.GeneralDescription.ToString();
			if (socialHistory.DidYouEverSmoke != null)
				visitTiming_SocialHistory.DidYouEverSmoke = Convert.ToBoolean(socialHistory.DidYouEverSmoke);
			if (socialHistory.NumberOfPacks != null)
				visitTiming_SocialHistory.NumberOfPacks = Convert.ToInt32(socialHistory.NumberOfPacks);
			if (socialHistory.NumberOfYears != null)
				visitTiming_SocialHistory.NumberOfYears = Convert.ToInt32(socialHistory.NumberOfYears);
			if (socialHistory.SmokeFurtherDetails != null)
				visitTiming_SocialHistory.SmokeFurtherDetails = socialHistory.SmokeFurtherDetails.ToString();
			if (socialHistory.QuitingSmokeLessThan != null)
				visitTiming_SocialHistory.QuitingSmokeLessThan = Convert.ToBoolean(socialHistory.QuitingSmokeLessThan);
			if (socialHistory.QuitingSmokeBetween != null)
				visitTiming_SocialHistory.QuitingSmokeBetween = Convert.ToBoolean(socialHistory.QuitingSmokeBetween);
			if (socialHistory.QuitingSmokeMoreThan != null)
				visitTiming_SocialHistory.QuitingSmokeMoreThan = Convert.ToBoolean(socialHistory.QuitingSmokeMoreThan);
			if (socialHistory.QuitingSmokeFurtherDetails != null)
				visitTiming_SocialHistory.QuitingSmokeFurtherDetails =
					socialHistory.QuitingSmokeFurtherDetails.ToString();
			if (socialHistory.DrinkAlcohol != null)
				visitTiming_SocialHistory.DrinkAlcohol = Convert.ToBoolean(socialHistory.DrinkAlcohol);
			if (socialHistory.HowMuchAlcohol != null)
				visitTiming_SocialHistory.HowMuchAlcohol = Convert.ToInt32(socialHistory.HowMuchAlcohol);
			if (socialHistory.AlcoholFurtherDetails != null)
				visitTiming_SocialHistory.AlcoholFurtherDetails = socialHistory.AlcoholFurtherDetails.ToString();
			if (socialHistory.HadProblemWithAlcohol != null)
				visitTiming_SocialHistory.HadProblemWithAlcohol =
					Convert.ToBoolean(socialHistory.HadProblemWithAlcohol);
			if (socialHistory.WhenHadProblemWIthAlcohol != null)
				visitTiming_SocialHistory.WhenHadProblemWIthAlcohol =
					Convert.ToInt32(socialHistory.WhenHadProblemWIthAlcohol);
			if (socialHistory.HadProblemWithAlcoholFurtherDetails != null)
				visitTiming_SocialHistory.HadProblemWithAlcoholFurtherDetails =
					socialHistory.HadProblemWithAlcoholFurtherDetails.ToString();
			if (socialHistory.Addicted != null)
				visitTiming_SocialHistory.Addicted = Convert.ToBoolean(socialHistory.Addicted);
			if (socialHistory.AddictionFurtherDetails != null)
				visitTiming_SocialHistory.AddictionFurtherDetails = socialHistory.AddictionFurtherDetails.ToString();
			if (socialHistory.HadProblemWithAddiction != null)
				visitTiming_SocialHistory.HadProblemWithAddiction =
					Convert.ToBoolean(socialHistory.HadProblemWithAddiction);
			if (socialHistory.HadProblemWithAddictionFurtherDetails != null)
				visitTiming_SocialHistory.HadProblemWithAddictionFurtherDetails =
					socialHistory.HadProblemWithAddictionFurtherDetails.ToString();
			if (socialHistory.UseRecreationalDrugs != null)
				visitTiming_SocialHistory.UseRecreationalDrugs = Convert.ToBoolean(socialHistory.UseRecreationalDrugs);
			if (socialHistory.UseRecreationalDrugsFurtherDetails != null)
				visitTiming_SocialHistory.UseRecreationalDrugsFurtherDetails =
					socialHistory.UseRecreationalDrugsFurtherDetails.ToString();
			visitTiming_SocialHistory.IsOnDuty = true;
			visitTiming_SocialHistory.InsertedBy = ActiveLoggedInUser.ID;
			visitTiming_SocialHistory.PEMRElementStatus = PEMRElementStatus.NewelyAdded;
			visitTiming_SocialHistory.DBCommonTransactionType = DB_CommonTransactionType.UpdateExisting;
			return visitTiming_SocialHistory.SaveChanges();
		}

		#endregion

		#region VisitTiming_MedicalHistory

		public static bool Update_VisitTiming_MedicalHistory(IPEMR_MedicalHistory pemrMedicalHistory,
			VisitTiming_MedicalHistory visitTiming_MedicalHistory)
		{
			if (pemrMedicalHistory == null || visitTiming_MedicalHistory == null)
				return false;

			if (pemrMedicalHistory.FurtherDetails != null &&
				!string.IsNullOrEmpty(pemrMedicalHistory.FurtherDetails.ToString()) &&
				!string.IsNullOrWhiteSpace(pemrMedicalHistory.FurtherDetails.ToString()))
				visitTiming_MedicalHistory.FurtherDetails = pemrMedicalHistory.FurtherDetails.ToString();
			if (pemrMedicalHistory.HasDiabetes != null)
				visitTiming_MedicalHistory.HasDiabetes = Convert.ToBoolean(pemrMedicalHistory.HasDiabetes);
			else
				visitTiming_MedicalHistory.HasDiabetes = null;

			if (pemrMedicalHistory.DiabetesType != null)
				visitTiming_MedicalHistory.DiabetesType_P_ID = Convert.ToInt32(pemrMedicalHistory.DiabetesType);
			else
				visitTiming_MedicalHistory.DiabetesType_P_ID = null;

			if (pemrMedicalHistory.IsDiabetesControlled != null)
				visitTiming_MedicalHistory.IsDiabetesControlled =
					Convert.ToBoolean(pemrMedicalHistory.IsDiabetesControlled);
			else
				visitTiming_MedicalHistory.IsDiabetesControlled = null;

			if (pemrMedicalHistory.HbA1C != null)
				visitTiming_MedicalHistory.HbA1c = Convert.ToInt32(pemrMedicalHistory.HbA1C);
			else
				visitTiming_MedicalHistory.HbA1c = null;

			if (pemrMedicalHistory.DiabetesMedicationType != null)
				visitTiming_MedicalHistory.DiabetesMedicationType_P_ID =
					Convert.ToInt32(pemrMedicalHistory.DiabetesMedicationType);
			else
				visitTiming_MedicalHistory.DiabetesMedicationType_P_ID = null;

			if (pemrMedicalHistory.DiabetesMedication != null)
				visitTiming_MedicalHistory.DiabetesMedication_CU_ID =
					Convert.ToInt32(pemrMedicalHistory.DiabetesMedication);
			else
				visitTiming_MedicalHistory.DiabetesMedication_CU_ID = null;

			if (pemrMedicalHistory.DiabetesDosage != null)
				visitTiming_MedicalHistory.DiabetesDose_CU_ID = Convert.ToInt32(pemrMedicalHistory.DiabetesDosage);
			else
				visitTiming_MedicalHistory.DiabetesDose_CU_ID = null;

			if (pemrMedicalHistory.DiabetesMedicationDuration != null)
				visitTiming_MedicalHistory.DiabetesMedicationDuration =
					Convert.ToInt32(pemrMedicalHistory.DiabetesMedicationDuration);
			else
				visitTiming_MedicalHistory.DiabetesMedicationDuration = null;

			if (pemrMedicalHistory.DiabetesMedicationDurationType != null)
				visitTiming_MedicalHistory.DiabetesTimeDurationType_P_ID =
					Convert.ToInt32(pemrMedicalHistory.DiabetesMedicationDurationType);
			else
				visitTiming_MedicalHistory.DiabetesTimeDurationType_P_ID = null;

			if (pemrMedicalHistory.IsHypertension != null)
				visitTiming_MedicalHistory.IsHypertension = Convert.ToBoolean(pemrMedicalHistory.IsHypertension);
			else
				visitTiming_MedicalHistory.IsHypertension = null;

			if (pemrMedicalHistory.IsHypertensionControlled != null)
				visitTiming_MedicalHistory.IsHypertensionControlled =
					Convert.ToBoolean(pemrMedicalHistory.IsHypertensionControlled);
			else
				visitTiming_MedicalHistory.IsHypertensionControlled = null;

			if (pemrMedicalHistory.HypertensionMedication != null)
				visitTiming_MedicalHistory.HypertensionMedication_CU_ID =
					Convert.ToInt32(pemrMedicalHistory.HypertensionMedication);
			else
				visitTiming_MedicalHistory.HypertensionMedication_CU_ID = null;

			if (pemrMedicalHistory.HypertensionDosage != null)
				visitTiming_MedicalHistory.HypertensionDose_CU_ID =
					Convert.ToInt32(pemrMedicalHistory.HypertensionDosage);
			else
				visitTiming_MedicalHistory.HypertensionDose_CU_ID = null;

			if (pemrMedicalHistory.HypertensionMedicationDuration != null)
				visitTiming_MedicalHistory.HypertensionMedicationDuration =
					Convert.ToInt32(pemrMedicalHistory.HypertensionMedicationDuration);
			else
				visitTiming_MedicalHistory.HypertensionMedicationDuration = null;

			if (pemrMedicalHistory.HypertensionMedicationDurationType != null)
				visitTiming_MedicalHistory.HypertensionTimeDurationType_P_ID =
					Convert.ToInt32(pemrMedicalHistory.HypertensionMedicationDurationType);
			else
				visitTiming_MedicalHistory.HypertensionTimeDurationType_P_ID = null;

			if (pemrMedicalHistory.HasDrugAllergies != null)
				visitTiming_MedicalHistory.IsDrugAllergy = Convert.ToBoolean(pemrMedicalHistory.HasDrugAllergies);
			else
				visitTiming_MedicalHistory.IsDrugAllergy = null;

			if (pemrMedicalHistory.TriggersDrugAllergies != null)
				visitTiming_MedicalHistory.TriggerOfDrugAllergy_CU_ID =
					Convert.ToInt32(pemrMedicalHistory.TriggersDrugAllergies);
			else
				visitTiming_MedicalHistory.TriggerOfDrugAllergy_CU_ID = null;

			if (pemrMedicalHistory.HasHepatitis != null)
				visitTiming_MedicalHistory.IsHepatitis = Convert.ToBoolean(pemrMedicalHistory.HasHepatitis);
			else
				visitTiming_MedicalHistory.IsHepatitis = null;

			if (pemrMedicalHistory.HasAsthma != null)
				visitTiming_MedicalHistory.IsAsthma = Convert.ToBoolean(pemrMedicalHistory.HasAsthma);
			else
				visitTiming_MedicalHistory.IsAsthma = null;

			visitTiming_MedicalHistory.IsOnDuty = true;
			visitTiming_MedicalHistory.InsertedBy = ActiveLoggedInUser.ID;
			visitTiming_MedicalHistory.PEMRElementStatus = PEMRElementStatus.NewelyAdded;
			visitTiming_MedicalHistory.DBCommonTransactionType = DB_CommonTransactionType.UpdateExisting;
			return visitTiming_MedicalHistory.SaveChanges();
		}

		#endregion

		#region VisitTiming_TreatmentPlan

		public static bool Update_VisitTiming_TreatmentPlan(IPEMR_TreatmentPlan treatmentPLanObject,
			VisitTiming_TreatmentPlan visitTiming_TreatmentPlan)
		{
			if (visitTiming_TreatmentPlan == null || treatmentPLanObject == null)
				return false;
			if (treatmentPLanObject.TreatmentDetails != null)
				visitTiming_TreatmentPlan.Treatment = treatmentPLanObject.TreatmentDetails.ToString();
			if (treatmentPLanObject.StepOrderIndex != null)
				visitTiming_TreatmentPlan.StepOrderIndex = Convert.ToInt32(treatmentPLanObject.StepOrderIndex);
			visitTiming_TreatmentPlan.IsOnDuty = true;
			visitTiming_TreatmentPlan.InsertedBy = ActiveLoggedInUser.ID;
			visitTiming_TreatmentPlan.PEMRElementStatus = PEMRElementStatus.NewelyAdded;
			visitTiming_TreatmentPlan.DBCommonTransactionType = DB_CommonTransactionType.UpdateExisting;
			return visitTiming_TreatmentPlan.SaveChanges();
		}

		#endregion

		public static bool Update_VisitTiming_Diagnosis(List<VisitTiming_Diagnosis> list)
		{
			if (list == null || list.Count == 0)
				return false;

			foreach (VisitTiming_Diagnosis visitTimingDiagnosis in list.FindAll(item =>
				Convert.ToInt32(item.PEMRElementStatus).Equals(Convert.ToInt32(PEMRElementStatus.NewelyAdded))))
			{
				if (ActivePEMRObject != null && ActivePEMRObject.List_VisitTiming_MainDiagnosis != null &&
					ActivePEMRObject.List_VisitTiming_MainDiagnosis.Count > 0)
					visitTimingDiagnosis.VisitTiming_MainDiagnosisID =
						ActivePEMRObject.List_VisitTiming_MainDiagnosis[0].ID;
				visitTimingDiagnosis.SaveChanges();
			}
			foreach (VisitTiming_Diagnosis visitTimingDiagnosis in list.FindAll(item =>
				Convert.ToInt32(item.PEMRElementStatus).Equals(Convert.ToInt32(PEMRElementStatus.Removed))))
			{
				visitTimingDiagnosis.DBCommonTransactionType = DB_CommonTransactionType.DeleteExisting;
				visitTimingDiagnosis.SaveChanges();
			}
			return true;
		}

		public static bool Update_VisitTiming_Medication(List<VisitTiming_Medication> list)
		{
			if (list == null || list.Count == 0)
				return false;

			foreach (VisitTiming_Medication visitTimingMedication in list.FindAll(item =>
				Convert.ToInt32(item.PEMRElementStatus).Equals(Convert.ToInt32(PEMRElementStatus.NewelyAdded))))
				visitTimingMedication.SaveChanges();
			foreach (VisitTiming_Medication visitTimingMedication in list.FindAll(item =>
				Convert.ToInt32(item.PEMRElementStatus).Equals(Convert.ToInt32(PEMRElementStatus.Removed))))
			{
				visitTimingMedication.DBCommonTransactionType = DB_CommonTransactionType.DeleteExisting;
				visitTimingMedication.SaveChanges();
			}

			return true;
		}

		public static bool Update_VisitTiming_InvestigationReservation(List<VisitTiming_InvestigationReservation> list)
		{
			if (list == null || list.Count == 0)
				return false;

			foreach (VisitTiming_InvestigationReservation visitTimingInvestigationReservation in list.FindAll(item =>
				Convert.ToInt32(item.PEMRElementStatus).Equals(Convert.ToInt32(PEMRElementStatus.NewelyAdded))))
			{
				if (ActivePEMRObject.Active_VisitTiming != null)
					visitTimingInvestigationReservation.VisitTimingID = ActivePEMRObject.Active_VisitTiming.ID;
				visitTimingInvestigationReservation.SaveChanges();
			}
			foreach (VisitTiming_InvestigationReservation visitTimingInvestigationReservation in list.FindAll(item =>
				Convert.ToInt32(item.PEMRElementStatus).Equals(Convert.ToInt32(PEMRElementStatus.Removed))))
			{
				visitTimingInvestigationReservation.DBCommonTransactionType = DB_CommonTransactionType.DeleteExisting;
				visitTimingInvestigationReservation.SaveChanges();
			}

			return true;
		}

		public static bool Update_VisitTiming_LabReservation(List<VisitTiming_LabReservation> list)
		{
			if (list == null || list.Count == 0)
				return false;

			foreach (VisitTiming_LabReservation visitTimingLabReservation in list.FindAll(item =>
				Convert.ToInt32(item.PEMRElementStatus).Equals(Convert.ToInt32(PEMRElementStatus.NewelyAdded))))
			{
				if (ActivePEMRObject.Active_VisitTiming != null)
					visitTimingLabReservation.VisitTimingID = ActivePEMRObject.Active_VisitTiming.ID;
				visitTimingLabReservation.SaveChanges();
			}
			foreach (VisitTiming_LabReservation visitTimingLabReservation in list.FindAll(item =>
				Convert.ToInt32(item.PEMRElementStatus).Equals(Convert.ToInt32(PEMRElementStatus.Removed))))
			{
				visitTimingLabReservation.DBCommonTransactionType = DB_CommonTransactionType.DeleteExisting;
				visitTimingLabReservation.SaveChanges();
			}

			return true;
		}

		public static bool Update_VisitTiming_SurgeryReservation(List<VisitTiming_SurgeryReservation> list)
		{
			if (list == null || list.Count == 0)
				return false;

			foreach (VisitTiming_SurgeryReservation visitTimingSurgeryReservation in list.FindAll(item =>
				Convert.ToInt32(item.PEMRElementStatus).Equals(Convert.ToInt32(PEMRElementStatus.NewelyAdded))))
			{
				if (ActivePEMRObject.Active_VisitTiming != null)
					visitTimingSurgeryReservation.VisitTimingID = ActivePEMRObject.Active_VisitTiming.ID;
				visitTimingSurgeryReservation.SaveChanges();
			}
			foreach (VisitTiming_SurgeryReservation visitTimingSurgeryReservation in list.FindAll(item =>
				Convert.ToInt32(item.PEMRElementStatus).Equals(Convert.ToInt32(PEMRElementStatus.Removed))))
			{
				visitTimingSurgeryReservation.DBCommonTransactionType = DB_CommonTransactionType.DeleteExisting;
				visitTimingSurgeryReservation.SaveChanges();
			}

			return true;
		}

		public static bool Update_VisitTiming_TreatmentPlan(List<VisitTiming_TreatmentPlan> list)
		{
			if (list == null || list.Count == 0)
				return false;

			foreach (VisitTiming_TreatmentPlan visitTimingTreatmentPlan in list.FindAll(item =>
				Convert.ToInt32(item.PEMRElementStatus).Equals(Convert.ToInt32(PEMRElementStatus.NewelyAdded))))
			{
				if (ActivePEMRObject.Active_VisitTiming != null)
					visitTimingTreatmentPlan.VisitTimingID = ActivePEMRObject.Active_VisitTiming.ID;
				visitTimingTreatmentPlan.SaveChanges();
			}
			foreach (VisitTiming_TreatmentPlan visitTimingTreatmentPlan in list.FindAll(item =>
				Convert.ToInt32(item.PEMRElementStatus).Equals(Convert.ToInt32(PEMRElementStatus.Removed))))
			{
				visitTimingTreatmentPlan.DBCommonTransactionType = DB_CommonTransactionType.DeleteExisting;
				visitTimingTreatmentPlan.SaveChanges();
			}
			foreach (VisitTiming_TreatmentPlan visitTimingTreatmentPlan in list.FindAll(item =>
				Convert.ToInt32(item.PEMRElementStatus).Equals(Convert.ToInt32(PEMRElementStatus.Updated))))
			{
				visitTimingTreatmentPlan.DBCommonTransactionType = DB_CommonTransactionType.UpdateExisting;
				visitTimingTreatmentPlan.SaveChanges();
			}
			return true;
		}

		#endregion

		#region Remove PEMR Object from VisitTiming

		public static bool Remove_VisitTiming_MainAnteriorSegmentSign(VisitTiming_MainAnteriorSegmentSign itemToBeRemoved)
		{
			if (itemToBeRemoved == null)
				return false;
			itemToBeRemoved.DBCommonTransactionType = DB_CommonTransactionType.DeleteExisting;
			return itemToBeRemoved.SaveChanges();
		}

		public static bool Remove_VisitTiming_AnteriorSegmentSign(VisitTiming_AnteriorSegmentSign itemToBeRemoved)
		{
			if (itemToBeRemoved == null)
				return false;
			itemToBeRemoved.DBCommonTransactionType = DB_CommonTransactionType.DeleteExisting;
			return itemToBeRemoved.SaveChanges();
		}

		public static bool Remove_VisitTiming_EOMSign(VisitTiming_EOMSign itemToBeRemoved)
		{
			if (itemToBeRemoved == null)
				return false;
			itemToBeRemoved.DBCommonTransactionType = DB_CommonTransactionType.DeleteExisting;
			return itemToBeRemoved.SaveChanges();
		}

		public static bool Remove_VisitTiming_MainPosteriorSegmentSign(VisitTiming_MainPosteriorSegmentSign itemToBeRemoved)
		{
			if (itemToBeRemoved == null)
				return false;
			itemToBeRemoved.DBCommonTransactionType = DB_CommonTransactionType.DeleteExisting;
			return itemToBeRemoved.SaveChanges();
		}

		public static bool Remove_VisitTiming_PosteriorSegmentSign(VisitTiming_PosteriorSegmentSign itemToBeRemoved)
		{
			if (itemToBeRemoved == null)
				return false;
			itemToBeRemoved.DBCommonTransactionType = DB_CommonTransactionType.DeleteExisting;
			return itemToBeRemoved.SaveChanges();
		}

		public static bool Remove_VisitTiming_MainAdnexaSegmentSign(VisitTiming_MainAdnexaSegmentSign itemToBeRemoved)
		{
			if (itemToBeRemoved == null)
				return false;
			itemToBeRemoved.DBCommonTransactionType = DB_CommonTransactionType.DeleteExisting;
			return itemToBeRemoved.SaveChanges();
		}

		public static bool Remove_VisitTiming_AdnexaSegmentSign(VisitTiming_AdnexaSegmentSign itemToBeRemoved)
		{
			if (itemToBeRemoved == null)
				return false;
			itemToBeRemoved.DBCommonTransactionType = DB_CommonTransactionType.DeleteExisting;
			return itemToBeRemoved.SaveChanges();
		}

		#endregion

		public static bool SavePEMRObject()
		{
			if (ActivePEMRObject == null)
				return false;

			if (ActivePEMRObject.Active_VisitTiming != null)
			{
				bool savingNew = false;
				AddVisitTiming_VitalSign(ActivePEMRObject.Active_VisitTiming,
					ActivePEMRObject.List_VisitTiming_VitalSign);
				AddVisitTiming_MainAnteriorSegmentSign(ActiveVisitTimming, ActivePEMRObject.List_VisitTiming_MainAnteriorSegmentSign);
				AddVisitTiming_Medication(ActivePEMRObject.Active_VisitTiming,
					ActivePEMRObject.List_VisitTiming_Medication);
				AddVisitTiming_Attachment(ActivePEMRObject.Active_VisitTiming,
					ActivePEMRObject.List_VisitTiming_Attachment);
				AddVisitTiming_InvestigationReservation(ActivePEMRObject.Active_VisitTiming,
					ActivePEMRObject.List_VisitTiming_InvestigationReservation);
				AddVisitTiming_LabReservation(ActivePEMRObject.Active_VisitTiming,
					ActivePEMRObject.List_VisitTiming_LabReservation);
				AddVisitTiming_SurgeryReservation(ActivePEMRObject.Active_VisitTiming,
					ActivePEMRObject.List_VisitTiming_SurgeryReservation);
				AddVisitTiming_TreatmentPlan(ActivePEMRObject.Active_VisitTiming,
					ActivePEMRObject.List_VisitTiming_TreatmentPlan);

				switch (ActivePEMRObject.PEMRStatus)
				{
					case PEMRStatus.CreateNewVisit:
						if ((ActivePEMRObject.List_VisitTiming_TreatmentPlan != null &&
							 ActivePEMRObject.List_VisitTiming_TreatmentPlan.Count > 0) ||
							(ActivePEMRObject.List_VisitTiming_Attachment != null &&
							 ActivePEMRObject.List_VisitTiming_Attachment.Count > 0) ||
							(ActivePEMRObject.List_VisitTiming_InvestigationReservation != null &&
							 ActivePEMRObject.List_VisitTiming_InvestigationReservation.Count > 0) ||
							(ActivePEMRObject.List_VisitTiming_LabReservation != null &&
							 ActivePEMRObject.List_VisitTiming_LabReservation.Count > 0) ||
							(ActivePEMRObject.List_VisitTiming_SurgeryReservation != null &&
							 ActivePEMRObject.List_VisitTiming_SurgeryReservation.Count > 0) ||
							(ActivePEMRObject.List_VisitTiming_Medication != null &&
							 ActivePEMRObject.List_VisitTiming_Medication.Count > 0) ||
							(ActivePEMRObject.List_VisitTiming_VitalSign != null &&
							 ActivePEMRObject.List_VisitTiming_VitalSign.Count > 0) ||
							(ActivePEMRObject.List_VisitTiming_MainAnteriorSegmentSign != null &&
							 ActivePEMRObject.List_VisitTiming_MainAnteriorSegmentSign.Count > 0))
							savingNew = true;

						if (savingNew)
							if (ActivePEMRObject.Active_VisitTiming.SaveChanges())
							{
								ActivePEMRObject.List_VisitTiming_TreatmentPlan = null;
								ActivePEMRObject.List_VisitTiming_Attachment = null;
								ActivePEMRObject.List_VisitTiming_InvestigationReservation = null;
								ActivePEMRObject.List_VisitTiming_LabReservation = null;
								ActivePEMRObject.List_VisitTiming_SurgeryReservation = null;
								ActivePEMRObject.List_VisitTiming_Medication = null;
								ActivePEMRObject.List_VisitTiming_Diagnosis = null;
								ActivePEMRObject.List_VisitTiming_VitalSign = null;
							}
							else
								return false;
						break;
					case PEMRStatus.UpdateExistingVisit:
						Update_VisitTiming_Medication(ActivePEMRObject.List_VisitTiming_Medication);
						Update_VisitTiming_MainAnteriorSegmentSign(ActivePEMRObject.List_VisitTiming_MainAnteriorSegmentSign[0]);
						Update_VisitTiming_Diagnosis(ActivePEMRObject.List_VisitTiming_Diagnosis);
						Update_VisitTiming_InvestigationReservation(ActivePEMRObject
							.List_VisitTiming_InvestigationReservation);
						Update_VisitTiming_LabReservation(ActivePEMRObject.List_VisitTiming_LabReservation);
						Update_VisitTiming_SurgeryReservation(ActivePEMRObject.List_VisitTiming_SurgeryReservation);
						Update_VisitTiming_TreatmentPlan(ActivePEMRObject.List_VisitTiming_TreatmentPlan);
						break;
				}
			}

			return true;
		}

		public static List<VisitTiming_InvestigationResult> Get_VisitTiming_InvestigationResultsList(
			int VisitTiming_InvestigationReservationId)
		{
			if (ActivePEMRObject != null &&
				ActivePEMRObject.List_VisitTiming_InvestigationResult != null &&
				ActivePEMRObject.List_VisitTiming_InvestigationResult.Count > 0)
				return ActivePEMRObject.List_VisitTiming_InvestigationResult.FindAll(item =>
					Convert.ToInt32(item.VisitTiming_InvestigationReservationID)
						.Equals(Convert.ToInt32(VisitTiming_InvestigationReservationId)));
			return null;
		}

		public static List<VisitTiming_LabResult> Get_VisitTiming_LabResultsList(
			int elementID)
		{
			if (ActivePEMRObject != null &&
				ActivePEMRObject.List_VisitTiming_LabResult != null &&
				ActivePEMRObject.List_VisitTiming_LabResult.Count > 0)
				return ActivePEMRObject.List_VisitTiming_LabResult.FindAll(item =>
					Convert.ToInt32(item.VisitTiming_LabReservationID)
						.Equals(Convert.ToInt32(elementID)));
			return null;
		}

		public static List<VisitTiming_SurgeryResult> Get_VisitTiming_SurgeryResultsList(
			int elementID)
		{
			if (ActivePEMRObject != null &&
				ActivePEMRObject.List_VisitTiming_SurgeryResult != null &&
				ActivePEMRObject.List_VisitTiming_SurgeryResult.Count > 0)
				return ActivePEMRObject.List_VisitTiming_SurgeryResult.FindAll(item =>
					Convert.ToInt32(item.VisitTiming_SurgeryReservationID)
						.Equals(Convert.ToInt32(elementID)));
			return null;
		}

		public static VisitTiming_InvestigationResult GetVisitTiming_InvestigationReservation(
			int VisitTiming_InvestigationReservationId)
		{
			if (ActivePEMRObject != null &&
				ActivePEMRObject.List_VisitTiming_InvestigationResult != null &&
				ActivePEMRObject.List_VisitTiming_InvestigationResult.Count > 0)
				return ActivePEMRObject.List_VisitTiming_InvestigationResult.Find(item =>
					Convert.ToInt32(item.ID).Equals(Convert.ToInt32(VisitTiming_InvestigationReservationId)));
			return null;
		}

		public static List<VisitTiming_Attachment> GetVisitTiming_AttachmentsList(
			DB_ServiceType serviceType, List<IPEMR_Element> visitTimingInvestigationResultsList)
		{
			List<VisitTiming_Attachment> list = new List<VisitTiming_Attachment>();
			List<VisitTiming_Attachment> tempList = null;

			if (visitTimingInvestigationResultsList != null && visitTimingInvestigationResultsList.Count > 0 &&
				ActivePEMRObject != null && ActivePEMRObject.List_VisitTiming_Attachment != null &&
				ActivePEMRObject.List_VisitTiming_Attachment.Count > 0)
				foreach (IPEMR_Element elementResult in
					visitTimingInvestigationResultsList)
				{

					switch (serviceType)
					{
						case DB_ServiceType.InvestigationServices:
							tempList = ActivePEMRObject.List_VisitTiming_Attachment.FindAll(item =>
								Convert.ToInt32(item.ID).Equals(Convert.ToInt32(
									((VisitTiming_InvestigationResult)elementResult)
									.VisitTiming_AttachmentID)));
							break;
						case DB_ServiceType.LabServices:
							tempList = ActivePEMRObject.List_VisitTiming_Attachment.FindAll(item =>
								Convert.ToInt32(item.ID).Equals(Convert.ToInt32(
									((VisitTiming_LabResult)elementResult).VisitTiming_AttachmentID)));
							break;
						case DB_ServiceType.SurgeryService:
							tempList = ActivePEMRObject.List_VisitTiming_Attachment.FindAll(item =>
								Convert.ToInt32(item.ID).Equals(Convert.ToInt32(
									((VisitTiming_SurgeryResult)elementResult)
									.VisitTiming_AttachmentID)));
							break;
					}

					if (tempList != null && tempList.Count > 0)
						list.AddRange(tempList);
				}

			return list;
		}

		public static List<PatientAttachment_cu> GetPatientAttachmetsList(
			List<VisitTiming_Attachment> visitTimingAttachementsList)
		{
			List<PatientAttachment_cu> list = new List<PatientAttachment_cu>();
			if (visitTimingAttachementsList != null && visitTimingAttachementsList.Count > 0 &&
				PatientAttachment_cu.ItemsList.Count > 0)
				foreach (VisitTiming_Attachment visitTimingAttachement in visitTimingAttachementsList)
				{
					PatientAttachment_cu temp = PatientAttachment_cu.ItemsList.Find(item =>
						Convert.ToInt32(item.ID)
							.Equals(Convert.ToInt32(visitTimingAttachement.PatientAttachement_CU_ID)));
					if (temp != null)
						list.Add(temp);
				}

			return list;
		}

		public static List<IPEMR_Element> ReflectToInterface(List<VisitTiming_InvestigationResult> list)
		{
			if (list == null || list.Count == 0)
				return null;

			List<IPEMR_Element> reflectedList = new List<IPEMR_Element>();
			foreach (VisitTiming_InvestigationResult visitTimingInvestigationResult in list)
			{
				reflectedList.Add(visitTimingInvestigationResult);
			}

			return reflectedList;
		}

		public static List<IPEMR_Element> ReflectToInterface(List<VisitTiming_LabResult> list)
		{
			if (list == null || list.Count == 0)
				return null;

			List<IPEMR_Element> reflectedList = new List<IPEMR_Element>();
			foreach (VisitTiming_LabResult visitTimingLabResult in list)
			{
				reflectedList.Add(visitTimingLabResult);
			}

			return reflectedList;
		}

		public static List<IPEMR_Element> ReflectToInterface(List<VisitTiming_SurgeryResult> list)
		{
			if (list == null || list.Count == 0)
				return null;

			List<IPEMR_Element> reflectedList = new List<IPEMR_Element>();
			foreach (VisitTiming_SurgeryResult visitTimingSurgeryResult in list)
			{
				reflectedList.Add(visitTimingSurgeryResult);
			}

			return reflectedList;
		}

		public static List<PatientAttachment_cu> GetPatientAttachmetsList(DB_ServiceType serviceType,
			int reservationElementID)
		{
			List<VisitTiming_Attachment> attachementsList = null;
			List<IPEMR_Element> reflectedList = null;
			switch (serviceType)
			{
				case DB_ServiceType.InvestigationServices:
					List<VisitTiming_InvestigationResult> investigationResultsList =
						Get_VisitTiming_InvestigationResultsList(reservationElementID);
					if (investigationResultsList == null || investigationResultsList.Count == 0)
						return null;
					reflectedList = ReflectToInterface(investigationResultsList);
					attachementsList = GetVisitTiming_AttachmentsList(serviceType, reflectedList);
					break;
				case DB_ServiceType.LabServices:
					List<VisitTiming_LabResult> labbResultsList =
						Get_VisitTiming_LabResultsList(reservationElementID);
					if (labbResultsList == null || labbResultsList.Count == 0)
						return null;
					reflectedList = ReflectToInterface(labbResultsList);
					attachementsList = GetVisitTiming_AttachmentsList(serviceType, reflectedList);
					break;
				case DB_ServiceType.SurgeryService:
					List<VisitTiming_SurgeryResult> surgeryResultsList =
						Get_VisitTiming_SurgeryResultsList(reservationElementID);
					if (surgeryResultsList == null || surgeryResultsList.Count == 0)
						return null;
					reflectedList = ReflectToInterface(surgeryResultsList);
					attachementsList = GetVisitTiming_AttachmentsList(serviceType, reflectedList);
					break;
			}

			if (attachementsList == null || attachementsList.Count == 0)
				return null;
			return GetPatientAttachmetsList(attachementsList);
		}

		public static List<PEMR_Translated> Translate_PEMR_Report(PEMRObject pemrObject)
		{
			if (pemrObject == null || pemrObject.Active_VisitTiming == null)
				return null;

			List<PEMR_Translated> translatedList = new List<PEMR_Translated>();

			PEMR_Translated parent = new PEMR_Translated();
			parent.ElementName = "Main Visit";
			parent.PEMR_ElementObject = pemrObject.Active_VisitTiming;

			#region VisitTiming_MedicalHistory

			if (pemrObject.List_VisitTiming_MedicalHistory != null && pemrObject.List_VisitTiming_MedicalHistory.Count > 0)
			{
				PEMR_Translated medicalHistoryParent = CreateNewPEMR_Translated(
					pemrObject.List_VisitTiming_MedicalHistory[0].OrderIndex,
					pemrObject.List_VisitTiming_MedicalHistory[0].ElementName,
					"",
					pemrObject.List_VisitTiming_MedicalHistory[0],
					pemrObject.List_VisitTiming_MedicalHistory[0].PEMR_Element);

				foreach (VisitTiming_MedicalHistory visitTiming in pemrObject.List_VisitTiming_MedicalHistory)
				{
					PEMR_Translated medicalHistory = null;

					#region FurtherDetails

					if (visitTiming.FurtherDetails != null &&
						!string.IsNullOrEmpty(visitTiming.FurtherDetails) &&
						!string.IsNullOrWhiteSpace(visitTiming.FurtherDetails))
					{
						medicalHistory = CreateNewPEMR_Translated(visitTiming.OrderIndex, "Further Details",
							visitTiming.FurtherDetails, visitTiming, visitTiming.PEMR_Element);
						if (medicalHistoryParent.List_PEMR_Element_Translated == null)
							medicalHistoryParent.List_PEMR_Element_Translated = new List<PEMR_Translated>();
						medicalHistoryParent.List_PEMR_Element_Translated.Add(medicalHistory);
					}

					#endregion

					#region HasDiabetes

					string visitTimingValue = string.Empty;
					visitTimingValue = "** Has Diabetes";

					if (visitTiming.HasDiabetes != null && Convert.ToBoolean(visitTiming.HasDiabetes))
					{
						visitTimingValue += "Yes";
						if (visitTiming.IsDiabetesControlled != null &&
							Convert.ToBoolean(visitTiming.IsDiabetesControlled))
							visitTimingValue += " --- Controlled";
						else if (visitTiming.IsDiabetesControlled != null &&
								 !Convert.ToBoolean(visitTiming.IsDiabetesControlled))
							visitTimingValue += " --- Not Controlled";
						else
							visitTimingValue += " -- Controlled VOID";

						visitTimingValue += " \r\n";
						visitTimingValue += "	--- Type : ";
						visitTimingValue += visitTiming.DiabetesType != null
							? visitTiming.DiabetesTypeName
							: " --- Type VOID";

						visitTimingValue += " --- HbA1c : ";
						visitTimingValue += visitTiming.HbA1c != null
							? visitTiming.HbA1c.ToString()
							: " --- HbA1c VOID";

						visitTimingValue += " \r\n";
						visitTimingValue += "	--- Diabetes Medication Type : ";
						visitTimingValue += visitTiming.DiabetedMedicationType != null
							? visitTiming.DiabetedMedicationTypeName
							: " --- Diabetes Medication Type VOID";

						visitTimingValue += " \r\n";
						visitTimingValue += "	--- Diabetes Medication : ";
						visitTimingValue += visitTiming.DiabetesMedication != null
							? visitTiming.DiabetesMedicationName
							: " --- Diabetes Medication VOID";

						visitTimingValue += " \r\n";
						visitTimingValue += "	--- Diabetes Medication Dosage : ";
						visitTimingValue += visitTiming.DiabetesDose != null
							? visitTiming.DiabetesDoseName
							: " --- Diabetes Medication Dosage VOID";

						visitTimingValue += " \r\n";
						visitTimingValue += "	--- Diabetes Medication Duration : ";
						visitTimingValue += visitTiming.DiabetesMedicationDuration != null
							? visitTiming.DiabetesMedicationDuration.ToString()
							: " --- Diabetes Medication Duration VOID";

						visitTimingValue += " \r\n";
						visitTimingValue += "	--- Diabetes Medication Duration Type : ";
						visitTimingValue += visitTiming.DiabetesTimeDurationType != null
							? visitTiming.DiabetesTimeDurationTypeName
							: " --- Diabetes Medication Duration Type VOID";
					}
					else if (visitTiming.HasDiabetes != null && !Convert.ToBoolean(visitTiming.HasDiabetes))
						visitTimingValue += "No";
					else
						visitTimingValue += "VOID";

					medicalHistory = CreateNewPEMR_Translated(visitTiming.OrderIndex, "Diabetes Details : ",
						visitTimingValue, visitTiming,
						visitTiming.PEMR_Element);
					if (medicalHistoryParent.List_PEMR_Element_Translated == null)
						medicalHistoryParent.List_PEMR_Element_Translated = new List<PEMR_Translated>();
					medicalHistoryParent.List_PEMR_Element_Translated.Add(medicalHistory);

					#endregion

					#region Is Hypertension

					visitTimingValue = string.Empty;
					visitTimingValue += "** Is Hypertension";

					if (visitTiming.IsHypertension != null && Convert.ToBoolean(visitTiming.IsHypertension))
					{
						visitTimingValue += "Yes";
						if (visitTiming.IsHypertensionControlled != null &&
							Convert.ToBoolean(visitTiming.IsHypertensionControlled))
							visitTimingValue += " --- Controlled";
						else if (visitTiming.IsDiabetesControlled != null &&
								 !Convert.ToBoolean(visitTiming.IsHypertensionControlled))
							visitTimingValue += " --- Not Controlled";
						else
							visitTimingValue += " --- Controlled VOID";

						visitTimingValue += " \r\n";
						visitTimingValue += "	--- Hypertension Medication : ";
						visitTimingValue += visitTiming.HypertensionMedication != null
							? visitTiming.HypertensionMedicationName
							: " --- Hypertension Medication VOID";

						visitTimingValue += " \r\n";
						visitTimingValue += "	--- Hypertension Medication Dosage : ";
						visitTimingValue += visitTiming.HypertensionDose != null
							? visitTiming.HypertensionDoseName
							: " --- Hypertension Medication Dosage VOID";

						visitTimingValue += " \r\n";
						visitTimingValue += "	--- Hypertension Medication Duration : ";
						visitTimingValue += visitTiming.HypertensionMedicationDuration != null
							? visitTiming.HypertensionMedicationDuration.ToString()
							: " --- Hypertension Medication Duration VOID";

						visitTimingValue += " \r\n";
						visitTimingValue += "	--- Hypertension Medication Duration Type : ";
						visitTimingValue += visitTiming.HypertensionTimeDurationType != null
							? visitTiming.HypertensionTimeDurationTypeName
							: " --- Hypertension Medication Duration Type VOID";
					}
					else if (visitTiming.IsHypertension != null && !Convert.ToBoolean(visitTiming.IsHypertension))
						visitTimingValue += "No";
					else
						visitTimingValue += "VOID";

					medicalHistory = CreateNewPEMR_Translated(visitTiming.OrderIndex, "Hypertension Details : ",
						visitTimingValue, visitTiming,
						visitTiming.PEMR_Element);
					if (medicalHistoryParent.List_PEMR_Element_Translated == null)
						medicalHistoryParent.List_PEMR_Element_Translated = new List<PEMR_Translated>();
					medicalHistoryParent.List_PEMR_Element_Translated.Add(medicalHistory);

					#endregion

					#region IsDrugAllergy

					visitTimingValue = string.Empty;
					visitTimingValue += "** Is DrugAllergy";

					if (visitTiming.IsDrugAllergy != null && Convert.ToBoolean(visitTiming.IsDrugAllergy))
					{
						visitTimingValue += "Yes";
					}
					else if (visitTiming.IsDrugAllergy != null && !Convert.ToBoolean(visitTiming.IsDrugAllergy))
						visitTimingValue += "No";
					else
						visitTimingValue += "VOID";

					medicalHistory = CreateNewPEMR_Translated(visitTiming.OrderIndex, "Drug Allergies Details : ",
						visitTimingValue, visitTiming,
						visitTiming.PEMR_Element);
					if (medicalHistoryParent.List_PEMR_Element_Translated == null)
						medicalHistoryParent.List_PEMR_Element_Translated = new List<PEMR_Translated>();
					medicalHistoryParent.List_PEMR_Element_Translated.Add(medicalHistory);

					#endregion

					#region IsDrugAllergy

					visitTimingValue = string.Empty;
					visitTimingValue += "** Is Hepatitis";

					if (visitTiming.IsHepatitis != null && Convert.ToBoolean(visitTiming.IsHepatitis))
					{
						visitTimingValue += "Yes";
					}
					else if (visitTiming.IsHepatitis != null && !Convert.ToBoolean(visitTiming.IsHepatitis))
						visitTimingValue += "No";
					else
						visitTimingValue += "VOID";

					medicalHistory = CreateNewPEMR_Translated(visitTiming.OrderIndex, "Hepatitis Details : ",
						visitTimingValue, visitTiming,
						visitTiming.PEMR_Element);
					if (medicalHistoryParent.List_PEMR_Element_Translated == null)
						medicalHistoryParent.List_PEMR_Element_Translated = new List<PEMR_Translated>();
					medicalHistoryParent.List_PEMR_Element_Translated.Add(medicalHistory);

					#endregion

					#region IsDrugAllergy

					visitTimingValue = string.Empty;
					visitTimingValue += "** Is Asthma";

					if (visitTiming.IsAsthma != null && Convert.ToBoolean(visitTiming.IsAsthma))
					{
						visitTimingValue += "Yes";
					}
					else if (visitTiming.IsAsthma != null && !Convert.ToBoolean(visitTiming.IsAsthma))
						visitTimingValue += "No";
					else
						visitTimingValue += "VOID";

					medicalHistory = CreateNewPEMR_Translated(visitTiming.OrderIndex, "Asthma Details : ",
						visitTimingValue, visitTiming,
						visitTiming.PEMR_Element);
					if (medicalHistoryParent.List_PEMR_Element_Translated == null)
						medicalHistoryParent.List_PEMR_Element_Translated = new List<PEMR_Translated>();
					medicalHistoryParent.List_PEMR_Element_Translated.Add(medicalHistory);

					#endregion
				}

				if (parent.List_PEMR_Element_Translated == null)
					parent.List_PEMR_Element_Translated = new List<PEMR_Translated>();
				parent.List_PEMR_Element_Translated.Add(medicalHistoryParent);
			}

			#endregion

			#region VisitTiming_SocialHistory

			if (pemrObject.List_VisitTiming_SocialHistory != null &&
				pemrObject.List_VisitTiming_SocialHistory.Count > 0)
			{
				PEMR_Translated socialHistoryParent = CreateNewPEMR_Translated(
					pemrObject.List_VisitTiming_SocialHistory[0].OrderIndex,
					pemrObject.List_VisitTiming_SocialHistory[0].ElementName,
					"",
					pemrObject.List_VisitTiming_SocialHistory[0],
					pemrObject.List_VisitTiming_SocialHistory[0].PEMR_Element);

				foreach (VisitTiming_SocialHistory visitTiming in pemrObject.List_VisitTiming_SocialHistory)
				{
					PEMR_Translated socialHistory = null;

					#region NegativeSocialHistory

					if (Convert.ToBoolean(visitTiming.NegativeSocialHistory))
					{
						socialHistory = CreateNewPEMR_Translated(visitTiming.OrderIndex, "Negative Social History",
							"Yes", visitTiming, visitTiming.PEMR_Element);
						if (socialHistoryParent.List_PEMR_Element_Translated == null)
							socialHistoryParent.List_PEMR_Element_Translated = new List<PEMR_Translated>();
						socialHistoryParent.List_PEMR_Element_Translated.Add(socialHistory);
					}
					#endregion
					#region Not NegativeSocialHistory
					else
					{
						#region Smoke

						string visitTimingValue = string.Empty;
						visitTimingValue = "** Did you ever smoke ?";

						if (visitTiming.DidYouEverSmoke != null && Convert.ToBoolean(visitTiming.DidYouEverSmoke))
						{
							visitTimingValue += " Yes";

							visitTimingValue += " \r\n";
							if (visitTiming.NumberOfPacks != null)
								visitTimingValue += "	--- Number Of Packs : " + visitTiming.NumberOfPacks + " packs";
							else
								visitTimingValue += "	-- Number Of Packs : VOID";

							visitTimingValue += " \r\n";
							if (visitTiming.NumberOfYears != null)
								visitTimingValue += "	--- Number Of Smoking Years : " + visitTiming.NumberOfYears + " years";
							else
								visitTimingValue += "	-- Number Of Smoking Years : VOID";

							if (visitTiming.SmokeFurtherDetails != null)
								visitTimingValue += " \r\n" + "	--- Smoke Further Details : " +
													visitTiming.SmokeFurtherDetails;

							visitTimingValue += " \r\n";
							if (visitTiming.QuitingSmokeLessThan != null)
								visitTimingValue += "	--- Quiting Smoke Less Than : 6 months";
							else if (visitTiming.QuitingSmokeBetween != null)
								visitTimingValue += "	--- Quiting Smoke Between : 6 and 12 months";
							else if (visitTiming.QuitingSmokeMoreThan != null)
								visitTimingValue += "	--- Quiting Smoke More Than : 12 months";
							else
								visitTimingValue += "	-- Quiting Smoke : Still Smoking";

							if (visitTiming.QuitingSmokeFurtherDetails != null)
								visitTimingValue += " \r\n" + "	--- Quitting Smoking Further Details : " +
													visitTiming.QuitingSmokeFurtherDetails;

						}
						else if (visitTiming.DidYouEverSmoke != null && !Convert.ToBoolean(visitTiming.DidYouEverSmoke))
							visitTimingValue += " No";
						else
							visitTimingValue += " VOID";

						socialHistory = CreateNewPEMR_Translated(visitTiming.OrderIndex, "Smoking Details : ",
							visitTimingValue, visitTiming,
							visitTiming.PEMR_Element);
						if (socialHistoryParent.List_PEMR_Element_Translated == null)
							socialHistoryParent.List_PEMR_Element_Translated = new List<PEMR_Translated>();
						socialHistoryParent.List_PEMR_Element_Translated.Add(socialHistory);

						#endregion

						#region DrinkAlcohol

						visitTimingValue = "** Drink Alcohol ?";

						if (visitTiming.DrinkAlcohol != null && Convert.ToBoolean(visitTiming.DrinkAlcohol))
						{
							visitTimingValue += " Yes";

							visitTimingValue += " \r\n";
							if (visitTiming.HowMuchAlcohol != null)
								visitTimingValue += "	--- How Much Alcohol : " + visitTiming.HowMuchAlcohol;
							else
								visitTimingValue += "	-- How Much Alcohol : VOID";

							if (visitTiming.AlcoholFurtherDetails != null)
								visitTimingValue += " \r\n" + "	--- Drinking Alcohol Further Details : " +
													visitTiming.AlcoholFurtherDetails;

							visitTimingValue += " \r\n";
							if (visitTiming.HadProblemWithAlcohol != null &&
								Convert.ToBoolean(visitTiming.HadProblemWithAlcohol))
							{
								visitTimingValue += "	--- Had Problems With Alcohol : Yes";
								if (visitTiming.WhenHadProblemWIthAlcohol != null)
									visitTimingValue +=
										"	-- Had Problems since " + visitTiming.WhenHadProblemWIthAlcohol +
										" years";
								if (visitTiming.HadProblemWithAlcoholFurtherDetails != null)
									visitTimingValue += " \r\n" + "	--- Had Problem With Alcohol Further Details : " +
														visitTiming.HadProblemWithAlcoholFurtherDetails;
							}
							else if (visitTiming.HadProblemWithAlcohol != null &&
								!Convert.ToBoolean(visitTiming.HadProblemWithAlcohol))
								visitTimingValue += "	--- Had Problems With Alcohol : No";
							else
								visitTimingValue += "	--- Had Problems With Alcohol : Void";
						}
						else if (visitTiming.DidYouEverSmoke != null && !Convert.ToBoolean(visitTiming.DidYouEverSmoke))
							visitTimingValue += " No";
						else
							visitTimingValue += " VOID";

						socialHistory = CreateNewPEMR_Translated(visitTiming.OrderIndex, "Drinking Alcohol Details : ",
							visitTimingValue, visitTiming,
							visitTiming.PEMR_Element);
						if (socialHistoryParent.List_PEMR_Element_Translated == null)
							socialHistoryParent.List_PEMR_Element_Translated = new List<PEMR_Translated>();
						socialHistoryParent.List_PEMR_Element_Translated.Add(socialHistory);

						#endregion

						#region Addicted

						visitTimingValue = "** Addicted ?";

						if (visitTiming.Addicted != null && Convert.ToBoolean(visitTiming.Addicted))
						{
							visitTimingValue += " Yes";

							if (visitTiming.AddictionFurtherDetails != null)
								visitTimingValue += " \r\n" + "	--- Addicted Further Details : " +
													visitTiming.AlcoholFurtherDetails;

							if (visitTiming.HadProblemWithAddiction != null &&
								Convert.ToBoolean(visitTiming.HadProblemWithAddiction))
							{
								visitTimingValue += " \r\n";
								visitTimingValue += "	---- Had Problems With Addiction : Yes";
								if (visitTiming.HadProblemWithAlcoholFurtherDetails != null)
									visitTimingValue += " \r\n" + "	--- Had Problem With Addiction Further Details : " +
														visitTiming.HadProblemWithAddictionFurtherDetails;
							}
							else if (visitTiming.HadProblemWithAddiction != null &&
									 !Convert.ToBoolean(visitTiming.HadProblemWithAddiction))
								visitTimingValue += "	---- Had Problems With Addiction : No";
							else
								visitTimingValue += "	---- Had Problems With Addiction : VOID";
						}
						else if (visitTiming.Addicted != null && !Convert.ToBoolean(visitTiming.Addicted))
							visitTimingValue += " No";
						else
							visitTimingValue += " VOID";

						socialHistory = CreateNewPEMR_Translated(visitTiming.OrderIndex, "Addicted : ",
							visitTimingValue, visitTiming,
							visitTiming.PEMR_Element);
						if (socialHistoryParent.List_PEMR_Element_Translated == null)
							socialHistoryParent.List_PEMR_Element_Translated = new List<PEMR_Translated>();
						socialHistoryParent.List_PEMR_Element_Translated.Add(socialHistory);

						#endregion

						#region UseRecreationalDrugs

						visitTimingValue = "** Used Recreational Drugs ?";

						if (visitTiming.UseRecreationalDrugs != null && Convert.ToBoolean(visitTiming.UseRecreationalDrugs))
						{
							visitTimingValue += " Yes";
							if (visitTiming.UseRecreationalDrugsFurtherDetails != null)
							{
								visitTimingValue += " \r\n";
								visitTimingValue += "	--- Use Recreational Drugs Further Details : " +
																					visitTiming.UseRecreationalDrugsFurtherDetails;
							}
						}
						else if (visitTiming.UseRecreationalDrugs != null && !Convert.ToBoolean(visitTiming.UseRecreationalDrugs))
							visitTimingValue += "  No";
						else
							visitTimingValue += " VOID";

						socialHistory = CreateNewPEMR_Translated(visitTiming.OrderIndex, "Used Recreational Drugs : ",
							visitTimingValue, visitTiming,
							visitTiming.PEMR_Element);
						if (socialHistoryParent.List_PEMR_Element_Translated == null)
							socialHistoryParent.List_PEMR_Element_Translated = new List<PEMR_Translated>();
						socialHistoryParent.List_PEMR_Element_Translated.Add(socialHistory);

						#endregion
					}
					#endregion

					#region GeneralDescription

					if (visitTiming.GeneralDescription != null)
						socialHistory = CreateNewPEMR_Translated(visitTiming.OrderIndex, "General Further Details",
							visitTiming.GeneralDescription, visitTiming, visitTiming.PEMR_Element);
					if (socialHistoryParent.List_PEMR_Element_Translated == null)
						socialHistoryParent.List_PEMR_Element_Translated = new List<PEMR_Translated>();
					socialHistoryParent.List_PEMR_Element_Translated.Add(socialHistory);

					#endregion
				}

				if (parent.List_PEMR_Element_Translated == null)
					parent.List_PEMR_Element_Translated = new List<PEMR_Translated>();
				parent.List_PEMR_Element_Translated.Add(socialHistoryParent);
			}

			#endregion

			#region VisitTiming_VitalSign

			if (pemrObject.List_VisitTiming_VitalSign != null && pemrObject.List_VisitTiming_VitalSign.Count > 0)
			{
				PEMR_Translated vitalSignParent = CreateNewPEMR_Translated(
					pemrObject.List_VisitTiming_VitalSign[0].OrderIndex,
					pemrObject.List_VisitTiming_VitalSign[0].ElementName,
					"",
					pemrObject.List_VisitTiming_VitalSign[0],
					pemrObject.List_VisitTiming_VitalSign[0].PEMR_Element);

				foreach (VisitTiming_VitalSign visitTiming in pemrObject.List_VisitTiming_VitalSign)
				{
					PEMR_Translated vitalSign = null;

					#region Taken Date / Time

					vitalSign = CreateNewPEMR_Translated(visitTiming.OrderIndex, "Taken Date / Time : ",
						Convert.ToDateTime(visitTiming.TakenTime).ToString(), visitTiming, visitTiming.PEMR_Element);
					if (vitalSignParent.List_PEMR_Element_Translated == null)
						vitalSignParent.List_PEMR_Element_Translated = new List<PEMR_Translated>();
					vitalSignParent.List_PEMR_Element_Translated.Add(vitalSign);

					#endregion

					#region GeneralDescription

					if (visitTiming.GeneralDescription != null &&
						!string.IsNullOrEmpty(visitTiming.GeneralDescription) &&
						!string.IsNullOrWhiteSpace(visitTiming.GeneralDescription))
					{
						vitalSign = CreateNewPEMR_Translated(visitTiming.OrderIndex, "General Description ",
							visitTiming.GeneralDescription, visitTiming, visitTiming.PEMR_Element);
						if (vitalSignParent.List_PEMR_Element_Translated == null)
							vitalSignParent.List_PEMR_Element_Translated = new List<PEMR_Translated>();
						vitalSignParent.List_PEMR_Element_Translated.Add(vitalSign);
					}

					#endregion

					#region IsHeightRegistered

					if (visitTiming.IsHeightRegistered)
					{
						vitalSign = CreateNewPEMR_Translated(visitTiming.OrderIndex, "Height ",
							visitTiming.HeightUnitUnitName + " [" + visitTiming.HeightAmount + "] ", visitTiming,
							visitTiming.PEMR_Element);
						if (vitalSignParent.List_PEMR_Element_Translated == null)
							vitalSignParent.List_PEMR_Element_Translated = new List<PEMR_Translated>();
						vitalSignParent.List_PEMR_Element_Translated.Add(vitalSign);
					}

					#endregion

					#region IsWeightRegistered

					if (visitTiming.IsWeightRegistered)
					{
						if (visitTiming.WeightDescription != null)
							vitalSign = CreateNewPEMR_Translated(visitTiming.OrderIndex, "Weight ",
								visitTiming.WeightUnitName + " [" + visitTiming.WeightAmount + "] Description : " +
								visitTiming.WeightDescription, visitTiming, visitTiming.PEMR_Element);
						else
							vitalSign = CreateNewPEMR_Translated(visitTiming.OrderIndex, "Weight ",
								visitTiming.WeightUnitName + " [" + visitTiming.WeightAmount + "] ", visitTiming,
								visitTiming.PEMR_Element);
						if (vitalSignParent.List_PEMR_Element_Translated == null)
							vitalSignParent.List_PEMR_Element_Translated = new List<PEMR_Translated>();
						vitalSignParent.List_PEMR_Element_Translated.Add(vitalSign);
					}

					#endregion

					#region IsTemperatureRegistered

					if (visitTiming.IsTemperatureRegistered)
					{
						if (visitTiming.WeightDescription != null)
							vitalSign = CreateNewPEMR_Translated(visitTiming.OrderIndex, "Temperature ",
								visitTiming.TemperatureUnitName + " [" + visitTiming.TemperatureAmount +
								"] Description : " +
								visitTiming.TemperatureDescription, visitTiming, visitTiming.PEMR_Element);
						else
							vitalSign = CreateNewPEMR_Translated(visitTiming.OrderIndex, "Temperature ",
								visitTiming.TemperatureUnitName + " [" + visitTiming.TemperatureAmount + "] ",
								visitTiming,
								visitTiming.PEMR_Element);
						if (vitalSignParent.List_PEMR_Element_Translated == null)
							vitalSignParent.List_PEMR_Element_Translated = new List<PEMR_Translated>();
						vitalSignParent.List_PEMR_Element_Translated.Add(vitalSign);
					}

					#endregion

					#region IsBloodPressureRegistered

					if (visitTiming.IsBloodPressureRegistered)
					{
						if (visitTiming.BloodPressureDescription != null)
							vitalSign = CreateNewPEMR_Translated(visitTiming.OrderIndex, "Blood Pressure : ",
								visitTiming.BloodPressureAmountHigh + " / " + visitTiming.BloodPressureAmountLow +
								" Description : " + visitTiming.BloodPressureDescription, visitTiming,
								visitTiming.PEMR_Element);
						else
							vitalSign = CreateNewPEMR_Translated(visitTiming.OrderIndex, "Blood Pressure : ",
								visitTiming.BloodPressureAmountHigh + " / " + visitTiming.BloodPressureAmountLow,
								visitTiming, visitTiming.PEMR_Element);
						if (vitalSignParent.List_PEMR_Element_Translated == null)
							vitalSignParent.List_PEMR_Element_Translated = new List<PEMR_Translated>();
						vitalSignParent.List_PEMR_Element_Translated.Add(vitalSign);
					}

					#endregion

					#region PulseAmount

					if (visitTiming.PulseAmount != null)
					{
						vitalSign = CreateNewPEMR_Translated(visitTiming.OrderIndex, "Pulse",
							visitTiming.PulseAmount.ToString(), visitTiming, visitTiming.PEMR_Element);
						if (vitalSignParent.List_PEMR_Element_Translated == null)
							vitalSignParent.List_PEMR_Element_Translated = new List<PEMR_Translated>();
						vitalSignParent.List_PEMR_Element_Translated.Add(vitalSign);
					}

					#endregion

					#region RespirationAmount

					if (visitTiming.PulseAmount != null)
					{
						if (visitTiming.RespirationDescription != null)
							vitalSign = CreateNewPEMR_Translated(visitTiming.OrderIndex, "Respiration ",
								visitTiming.RespirationAmount + " Description : " +
								visitTiming.RespirationDescription, visitTiming, visitTiming.PEMR_Element);
						else
							vitalSign = CreateNewPEMR_Translated(visitTiming.OrderIndex, "Respiration ",
								visitTiming.RespirationAmount.ToString(), visitTiming, visitTiming.PEMR_Element);
						if (vitalSignParent.List_PEMR_Element_Translated == null)
							vitalSignParent.List_PEMR_Element_Translated = new List<PEMR_Translated>();
						vitalSignParent.List_PEMR_Element_Translated.Add(vitalSign);
					}

					#endregion

					#region OxygenAmount

					if (visitTiming.OxygenAmount != null)
					{
						vitalSign = CreateNewPEMR_Translated(visitTiming.OrderIndex, "Oxygen ",
							visitTiming.OxygenAmount.ToString(), visitTiming, visitTiming.PEMR_Element);
						if (vitalSignParent.List_PEMR_Element_Translated == null)
							vitalSignParent.List_PEMR_Element_Translated = new List<PEMR_Translated>();
						vitalSignParent.List_PEMR_Element_Translated.Add(vitalSign);
					}

					#endregion

					#region FIO2

					if (visitTiming.FIO2 != null)
					{
						vitalSign = CreateNewPEMR_Translated(visitTiming.OrderIndex, "FIO2 ",
							visitTiming.FIO2.ToString(), visitTiming, visitTiming.PEMR_Element);
						if (vitalSignParent.List_PEMR_Element_Translated == null)
							vitalSignParent.List_PEMR_Element_Translated = new List<PEMR_Translated>();
						vitalSignParent.List_PEMR_Element_Translated.Add(vitalSign);
					}

					#endregion

					#region SPO2Amount

					if (visitTiming.SPO2Amount != null)
					{
						vitalSign = CreateNewPEMR_Translated(visitTiming.OrderIndex, "SPO2 ",
							visitTiming.SPO2Amount.ToString(), visitTiming, visitTiming.PEMR_Element);
						if (vitalSignParent.List_PEMR_Element_Translated == null)
							vitalSignParent.List_PEMR_Element_Translated = new List<PEMR_Translated>();
						vitalSignParent.List_PEMR_Element_Translated.Add(vitalSign);
					}

					#endregion
				}

				if (parent.List_PEMR_Element_Translated == null)
					parent.List_PEMR_Element_Translated = new List<PEMR_Translated>();
				parent.List_PEMR_Element_Translated.Add(vitalSignParent);
			}

			#endregion

			#region Symptoms

			#endregion

			#region VisitTiming_VisionRefractionReading

			if (pemrObject.List_VisitTiming_VisionRefractionReading != null &&
				pemrObject.List_VisitTiming_VisionRefractionReading.Count > 0)
			{
				PEMR_Translated visionRefractionParent = CreateNewPEMR_Translated(
					pemrObject.List_VisitTiming_VisionRefractionReading[0].OrderIndex,
					pemrObject.List_VisitTiming_VisionRefractionReading[0].ElementName,
					"",
					pemrObject.List_VisitTiming_VisionRefractionReading[0],
					pemrObject.List_VisitTiming_VisionRefractionReading[0].PEMR_Element);
				if (visionRefractionParent != null)
					visionRefractionParent.IsEyeRelatedType = true;

				foreach (VisitTiming_VisionRefractionReading visitTiming in pemrObject.List_VisitTiming_VisionRefractionReading)
				{
					PEMR_Translated visionRefractionReading = null;

					#region VisionRefractionReadingTypeName

					if (visitTiming.VisionRefractionReadingType != null)
					{
						visionRefractionReading = CreateNewPEMR_Translated(visitTiming.OrderIndex, "Vision Refraction Type",
							visitTiming.VisionRefractionReadingTypeName, visitTiming, visitTiming.PEMR_Element);
						if (visionRefractionReading != null)
							visionRefractionReading.IsEyeRelatedType = true;
						if (visionRefractionParent.List_PEMR_Element_Translated == null)
							visionRefractionParent.List_PEMR_Element_Translated = new List<PEMR_Translated>();
						visionRefractionParent.List_PEMR_Element_Translated.Add(visionRefractionReading);
					}

					#endregion

					#region TakenDateTime

					if (visitTiming.TakenDateTime != null)
					{
						visionRefractionReading = CreateNewPEMR_Translated(visitTiming.OrderIndex,
							"Reading Taken Date/Time",
							visitTiming.TakenDateTime.ConvertDateTimeToString(false, true, false), visitTiming,
							visitTiming.PEMR_Element);
						if (visionRefractionReading != null)
							visionRefractionReading.IsEyeRelatedType = true;
						if (visionRefractionParent.List_PEMR_Element_Translated == null)
							visionRefractionParent.List_PEMR_Element_Translated = new List<PEMR_Translated>();
						visionRefractionParent.List_PEMR_Element_Translated.Add(visionRefractionReading);
					}

					#endregion

					#region UVA_OU

					if (visitTiming.UVA_OU != null)
					{
						visionRefractionReading = CreateNewPEMR_Translated(visitTiming.OrderIndex, "UVA",
							visitTiming.UVA_OU.ToString(), visitTiming, visitTiming.PEMR_Element);
						if (visionRefractionReading != null)
							visionRefractionReading.IsEyeRelatedType = true;
						if (visionRefractionReading != null)
							visionRefractionReading.EyeType = DB_EyeType_p.OU;
						if (visionRefractionParent.List_PEMR_Element_Translated == null)
							visionRefractionParent.List_PEMR_Element_Translated = new List<PEMR_Translated>();
						visionRefractionParent.List_PEMR_Element_Translated.Add(visionRefractionReading);
					}

					#endregion

					#region NVA_OU

					if (visitTiming.NVA_OU != null)
					{
						visionRefractionReading = CreateNewPEMR_Translated(visitTiming.OrderIndex, "NVA",
							visitTiming.NVA_OU.ToString(), visitTiming, visitTiming.PEMR_Element);
						if (visionRefractionReading != null)
							visionRefractionReading.EyeType = DB_EyeType_p.OU;
						if (visionRefractionParent.List_PEMR_Element_Translated == null)
							visionRefractionParent.List_PEMR_Element_Translated = new List<PEMR_Translated>();
						visionRefractionParent.List_PEMR_Element_Translated.Add(visionRefractionReading);
					}

					#endregion

					#region NVAAmount_OU

					if (visitTiming.NVAAmount_OU != null)
					{
						visionRefractionReading = CreateNewPEMR_Translated(visitTiming.OrderIndex, "NVAAmount",
							visitTiming.NVAAmount_OU.ToString(), visitTiming, visitTiming.PEMR_Element);
						if (visionRefractionReading != null)
							visionRefractionReading.EyeType = DB_EyeType_p.OU;
						if (visionRefractionParent.List_PEMR_Element_Translated == null)
							visionRefractionParent.List_PEMR_Element_Translated = new List<PEMR_Translated>();
						visionRefractionParent.List_PEMR_Element_Translated.Add(visionRefractionReading);
					}

					#endregion

					#region Distance_OD

					if (visitTiming.Distance_OD != null)
					{
						visionRefractionReading = CreateNewPEMR_Translated(visitTiming.OrderIndex, "Distance",
							visitTiming.Distance_OD.ToString(), visitTiming, visitTiming.PEMR_Element);
						if (visionRefractionReading != null)
							visionRefractionReading.EyeType = DB_EyeType_p.OD;
						if (visionRefractionParent.List_PEMR_Element_Translated == null)
							visionRefractionParent.List_PEMR_Element_Translated = new List<PEMR_Translated>();
						visionRefractionParent.List_PEMR_Element_Translated.Add(visionRefractionReading);
					}

					#endregion

					#region NVA_OD

					if (visitTiming.NVA_OD != null)
					{
						visionRefractionReading = CreateNewPEMR_Translated(visitTiming.OrderIndex, "NVA",
							visitTiming.NVA_OD.ToString(), visitTiming, visitTiming.PEMR_Element);
						if (visionRefractionReading != null)
							visionRefractionReading.EyeType = DB_EyeType_p.OD;
						if (visionRefractionParent.List_PEMR_Element_Translated == null)
							visionRefractionParent.List_PEMR_Element_Translated = new List<PEMR_Translated>();
						visionRefractionParent.List_PEMR_Element_Translated.Add(visionRefractionReading);
					}

					#endregion

					#region NVAAmount_OD

					if (visitTiming.NVAAmount_OD != null)
					{
						visionRefractionReading = CreateNewPEMR_Translated(visitTiming.OrderIndex, "NVAAmount",
							visitTiming.NVAAmount_OD.ToString(), visitTiming, visitTiming.PEMR_Element);
						if (visionRefractionReading != null)
							visionRefractionReading.EyeType = DB_EyeType_p.OD;
						if (visionRefractionParent.List_PEMR_Element_Translated == null)
							visionRefractionParent.List_PEMR_Element_Translated = new List<PEMR_Translated>();
						visionRefractionParent.List_PEMR_Element_Translated.Add(visionRefractionReading);
					}

					#endregion
				}

				if (parent.List_PEMR_Element_Translated == null)
					parent.List_PEMR_Element_Translated = new List<PEMR_Translated>();
				parent.List_PEMR_Element_Translated.Add(visionRefractionParent);
			}

			#endregion

			#region VisitTiming_Pupillary

			#endregion

			#region VisitTiming_AnteriorSegmentSign

			if (pemrObject.List_VisitTiming_MainAnteriorSegmentSign != null &&
				pemrObject.List_VisitTiming_MainAnteriorSegmentSign.Count > 0)
			{
				PEMR_Translated mainAnteriorSegmentParent = CreateNewPEMR_Translated(
					pemrObject.List_VisitTiming_MainAnteriorSegmentSign[0].OrderIndex,
					pemrObject.List_VisitTiming_MainAnteriorSegmentSign[0].ElementName,
					"",
					pemrObject.List_VisitTiming_MainAnteriorSegmentSign[0],
					pemrObject.List_VisitTiming_MainAnteriorSegmentSign[0].PEMR_Element);
				if (mainAnteriorSegmentParent != null)
					mainAnteriorSegmentParent.IsEyeRelatedType = true;

				PEMR_Translated anteriorSegment_translated = null;
				VisitTiming_MainAnteriorSegmentSign mainAnteriorSegmentElement = null;

				if (pemrObject.List_VisitTiming_MainAnteriorSegmentSign != null)
					foreach (VisitTiming_MainAnteriorSegmentSign visitTiming in pemrObject
						.List_VisitTiming_MainAnteriorSegmentSign)
					{
						#region General Description

						if (visitTiming.GeneralDescription_OD != null)
						{
							anteriorSegment_translated = CreateNewPEMR_Translated(visitTiming.OrderIndex,
								"General Recommendations",
								visitTiming.GeneralDescription_OD, visitTiming,
								visitTiming.PEMR_Element);
							if (anteriorSegment_translated != null)
							{
								anteriorSegment_translated.EyeType = DB_EyeType_p.OD;
								anteriorSegment_translated.IsEyeRelatedType = true;
							}
							if (mainAnteriorSegmentParent.List_PEMR_Element_Translated == null)
								mainAnteriorSegmentParent.List_PEMR_Element_Translated = new List<PEMR_Translated>();
							mainAnteriorSegmentParent.List_PEMR_Element_Translated.Add(anteriorSegment_translated);
						}

						if (visitTiming.GeneralDescription_OD != null)
						{
							anteriorSegment_translated = CreateNewPEMR_Translated(visitTiming.OrderIndex,
								"General Recommendations",
								visitTiming.GeneralDescription_OS, visitTiming,
								visitTiming.PEMR_Element);
							if (anteriorSegment_translated != null)
							{
								anteriorSegment_translated.EyeType = DB_EyeType_p.OS;
								anteriorSegment_translated.IsEyeRelatedType = true;
							}
							if (mainAnteriorSegmentParent.List_PEMR_Element_Translated == null)
								mainAnteriorSegmentParent.List_PEMR_Element_Translated = new List<PEMR_Translated>();
							mainAnteriorSegmentParent.List_PEMR_Element_Translated.Add(anteriorSegment_translated);
						}

						mainAnteriorSegmentElement = visitTiming;

						#endregion
					}

				#region AnteriorSegmentSign

				if (pemrObject.List_VisitTiming_AnteriorSegmentSign != null)
				{
					int orderIndex = -1;
					string segmentString = "";
					string segmentElementName = "";

					if (pemrObject.List_VisitTiming_AnteriorSegmentSign.FindAll(
							item => item.Eye_P_ID != null &&
									Convert.ToInt32(item.Eye_P_ID).Equals(Convert.ToInt32(DB_EyeType_p.OD))).Count > 0)
					{
						segmentString = "Eye (OD) : ";
						foreach (VisitTiming_AnteriorSegmentSign anteriorSegment in
							pemrObject.List_VisitTiming_AnteriorSegmentSign.FindAll(item => Convert
								.ToInt32(item.Eye_P_ID)
								.Equals(Convert.ToInt32(DB_EyeType_p.OD))))
						{
							orderIndex = anteriorSegment.OrderIndex;
							SegmentSign_cu segmentAdded = SegmentSign_cu.ItemsList.Find(item =>
								Convert.ToInt32(item.ID).Equals(Convert.ToInt32(anteriorSegment.SegmentSignID)));
							if (segmentAdded != null)
								segmentString += segmentAdded.Name_P + ", ";
						}

						anteriorSegment_translated = CreateNewPEMR_Translated(orderIndex, "Anterior Segment - OD", segmentString,
							mainAnteriorSegmentElement, DB_PEMR_ElementType.Diagnosis);

						if (mainAnteriorSegmentParent.List_PEMR_Element_Translated == null)
							mainAnteriorSegmentParent.List_PEMR_Element_Translated = new List<PEMR_Translated>();
						mainAnteriorSegmentParent.List_PEMR_Element_Translated.Add(anteriorSegment_translated);
					}

					if (pemrObject.List_VisitTiming_AnteriorSegmentSign.FindAll(
							item => item.Eye_P_ID != null &&
									Convert.ToInt32(item.Eye_P_ID).Equals(Convert.ToInt32(DB_EyeType_p.OS))).Count > 0)
					{
						segmentString = "Eye (OS) : ";
						foreach (VisitTiming_AnteriorSegmentSign anteriorSegment in pemrObject
							.List_VisitTiming_AnteriorSegmentSign.FindAll(
								item => item.Eye_P_ID != null &&
										Convert.ToInt32(item.Eye_P_ID).Equals(Convert.ToInt32(DB_EyeType_p.OS))))
						{
							orderIndex = anteriorSegment.OrderIndex;
							SegmentSign_cu segmentAdded = SegmentSign_cu.ItemsList.Find(item =>
								Convert.ToInt32(item.ID).Equals(Convert.ToInt32(anteriorSegment.SegmentSignID)));
							if (segmentAdded != null)
								segmentString += segmentAdded.Name_P + ", ";
						}

						anteriorSegment_translated = CreateNewPEMR_Translated(orderIndex, "Anterior Segment - OS ", segmentString,
							mainAnteriorSegmentElement, DB_PEMR_ElementType.Diagnosis);

						if (mainAnteriorSegmentParent.List_PEMR_Element_Translated == null)
							mainAnteriorSegmentParent.List_PEMR_Element_Translated = new List<PEMR_Translated>();
						mainAnteriorSegmentParent.List_PEMR_Element_Translated.Add(anteriorSegment_translated);
					}

					if (pemrObject.List_VisitTiming_AnteriorSegmentSign.FindAll(
							item => item.Eye_P_ID != null &&
									Convert.ToInt32(item.Eye_P_ID).Equals(Convert.ToInt32(DB_EyeType_p.OU))).Count > 0)
					{
						segmentString = "Eye (OU) : ";
						foreach (VisitTiming_AnteriorSegmentSign visitDiagnosis in pemrObject
							.List_VisitTiming_AnteriorSegmentSign.FindAll(
								item => item.Eye_P_ID != null &&
										Convert.ToInt32(item.Eye_P_ID).Equals(Convert.ToInt32(DB_EyeType_p.OU))))
						{
							orderIndex = visitDiagnosis.OrderIndex;
							SegmentSign_cu segmentAdded = SegmentSign_cu.ItemsList.Find(item =>
								Convert.ToInt32(item.ID).Equals(Convert.ToInt32(visitDiagnosis.SegmentSignID)));
							if (segmentAdded != null)
								segmentString += segmentAdded.Name_P + ", ";
						}

						anteriorSegment_translated = CreateNewPEMR_Translated(orderIndex, "Anterior Segment - OU", segmentString,
							mainAnteriorSegmentElement, DB_PEMR_ElementType.Diagnosis);

						if (mainAnteriorSegmentParent.List_PEMR_Element_Translated == null)
							mainAnteriorSegmentParent.List_PEMR_Element_Translated = new List<PEMR_Translated>();
						mainAnteriorSegmentParent.List_PEMR_Element_Translated.Add(anteriorSegment_translated);
					}
				}
				#endregion

				if (parent.List_PEMR_Element_Translated == null)
					parent.List_PEMR_Element_Translated = new List<PEMR_Translated>();
				parent.List_PEMR_Element_Translated.Add(mainAnteriorSegmentParent);
			}

			#endregion

			#region VisitTiming_TreatmentPlan

			if (pemrObject.List_VisitTiming_TreatmentPlan != null && pemrObject.List_VisitTiming_TreatmentPlan.Count > 0)
			{
				PEMR_Translated treatmentPlanParent = new PEMR_Translated();
				treatmentPlanParent.OrderIndex = pemrObject.List_VisitTiming_TreatmentPlan[0].OrderIndex;
				treatmentPlanParent.ElementName = pemrObject.List_VisitTiming_TreatmentPlan[0].ElementName;
				treatmentPlanParent.PEMR_Element = pemrObject.List_VisitTiming_TreatmentPlan[0].PEMR_Element;
				treatmentPlanParent.PEMR_ElementObject = pemrObject.List_VisitTiming_TreatmentPlan[0];

				foreach (VisitTiming_TreatmentPlan visitTimingTreatmentPlan in pemrObject.List_VisitTiming_TreatmentPlan)
				{
					PEMR_Translated treatmentPLan = new PEMR_Translated();
					treatmentPLan.OrderIndex = visitTimingTreatmentPlan.OrderIndex;
					treatmentPLan.ElementName = "Index : " + visitTimingTreatmentPlan.StepOrderIndex;
					treatmentPLan.TranslatedItemValue = visitTimingTreatmentPlan.Treatment;
					treatmentPLan.PEMR_Element = visitTimingTreatmentPlan.PEMR_Element;
					treatmentPLan.PEMR_ElementObject = visitTimingTreatmentPlan;
					if (treatmentPlanParent.List_PEMR_Element_Translated == null)
						treatmentPlanParent.List_PEMR_Element_Translated = new List<PEMR_Translated>();
					treatmentPlanParent.List_PEMR_Element_Translated.Add(treatmentPLan);
				}

				if (parent.List_PEMR_Element_Translated == null)
					parent.List_PEMR_Element_Translated = new List<PEMR_Translated>();
				parent.List_PEMR_Element_Translated.Add(treatmentPlanParent);
			}

			#endregion

			#region VisitTiming_InvestigationReservation

			if (pemrObject.List_VisitTiming_InvestigationReservation != null &&
				pemrObject.List_VisitTiming_InvestigationReservation.Count > 0)
			{
				PEMR_Translated investigationReservationParent = null;
				investigationReservationParent = CreateNewPEMR_Translated(
					pemrObject.List_VisitTiming_InvestigationReservation[0].OrderIndex,
					pemrObject.List_VisitTiming_InvestigationReservation[0].ElementName, "",
					pemrObject.List_VisitTiming_InvestigationReservation[0],
					pemrObject.List_VisitTiming_InvestigationReservation[0].PEMR_Element);

				foreach (VisitTiming_InvestigationReservation visitTimingInvestigationReservation in pemrObject
					.List_VisitTiming_InvestigationReservation)
				{
					PEMR_Translated investigationReservation = null;

					#region Requested Investigation

					if (visitTimingInvestigationReservation.Date != null)
						investigationReservation = CreateNewPEMR_Translated(
							visitTimingInvestigationReservation.OrderIndex, "Requested Investigation",
							visitTimingInvestigationReservation.ServiceName + " [" + Convert
								.ToDateTime(visitTimingInvestigationReservation.Date)
								.ConvertDateTimeToString(false, false) + "] ", visitTimingInvestigationReservation,
							visitTimingInvestigationReservation.PEMR_Element);
					else
						investigationReservation = CreateNewPEMR_Translated(visitTimingInvestigationReservation.OrderIndex,
							"Requested Investigation", visitTimingInvestigationReservation.ServiceName,
							visitTimingInvestigationReservation, visitTimingInvestigationReservation.PEMR_Element);
					if (investigationReservationParent.List_PEMR_Element_Translated == null)
						investigationReservationParent.List_PEMR_Element_Translated = new List<PEMR_Translated>();
					investigationReservationParent.List_PEMR_Element_Translated.Add(investigationReservation);
					#endregion

					#region Description
					if (visitTimingInvestigationReservation.Description != null &&
						!string.IsNullOrEmpty(visitTimingInvestigationReservation.Description) &&
						!string.IsNullOrWhiteSpace(visitTimingInvestigationReservation.Description))
					{
						investigationReservation = CreateNewPEMR_Translated(
							visitTimingInvestigationReservation.OrderIndex, "Recommendations",
							visitTimingInvestigationReservation.Description, visitTimingInvestigationReservation,
							visitTimingInvestigationReservation.PEMR_Element);
						if (investigationReservationParent.List_PEMR_Element_Translated == null)
							investigationReservationParent.List_PEMR_Element_Translated = new List<PEMR_Translated>();
						investigationReservationParent.List_PEMR_Element_Translated.Add(investigationReservation);
					}
					#endregion
				}

				if (parent.List_PEMR_Element_Translated == null)
					parent.List_PEMR_Element_Translated = new List<PEMR_Translated>();
				parent.List_PEMR_Element_Translated.Add(investigationReservationParent);
			}

			#endregion

			#region VisitTiming_LabReservation

			if (pemrObject.List_VisitTiming_LabReservation != null &&
				pemrObject.List_VisitTiming_LabReservation.Count > 0)
			{
				PEMR_Translated labReservationParent = null;
				labReservationParent = CreateNewPEMR_Translated(
					pemrObject.List_VisitTiming_LabReservation[0].OrderIndex,
					pemrObject.List_VisitTiming_LabReservation[0].ElementName, "",
					pemrObject.List_VisitTiming_LabReservation[0],
					pemrObject.List_VisitTiming_LabReservation[0].PEMR_Element);

				foreach (VisitTiming_LabReservation visitTimingLabReservation in pemrObject
					.List_VisitTiming_LabReservation)
				{
					PEMR_Translated labReservation = null;

					#region Requested Investigation

					if (visitTimingLabReservation.Date != null)
						labReservation = CreateNewPEMR_Translated(visitTimingLabReservation.OrderIndex, "Requested Lab",
							visitTimingLabReservation.ServiceName + " [" + Convert
								.ToDateTime(visitTimingLabReservation.Date).ConvertDateTimeToString(false, false) +
							"] ", visitTimingLabReservation, visitTimingLabReservation.PEMR_Element);
					else
						labReservation = CreateNewPEMR_Translated(visitTimingLabReservation.OrderIndex,
							"Requested Lab", visitTimingLabReservation.ServiceName,
							visitTimingLabReservation, visitTimingLabReservation.PEMR_Element);
					if (labReservationParent.List_PEMR_Element_Translated == null)
						labReservationParent.List_PEMR_Element_Translated = new List<PEMR_Translated>();
					labReservationParent.List_PEMR_Element_Translated.Add(labReservation);
					#endregion

					#region Description
					if (visitTimingLabReservation.Description != null &&
						!string.IsNullOrEmpty(visitTimingLabReservation.Description) &&
						!string.IsNullOrWhiteSpace(visitTimingLabReservation.Description))
					{
						labReservation = CreateNewPEMR_Translated(
							visitTimingLabReservation.OrderIndex, "Recommendations",
							visitTimingLabReservation.Description, visitTimingLabReservation,
							visitTimingLabReservation.PEMR_Element);
						if (labReservationParent.List_PEMR_Element_Translated == null)
							labReservationParent.List_PEMR_Element_Translated = new List<PEMR_Translated>();
						labReservationParent.List_PEMR_Element_Translated.Add(labReservation);
					}
					#endregion
				}

				if (parent.List_PEMR_Element_Translated == null)
					parent.List_PEMR_Element_Translated = new List<PEMR_Translated>();
				parent.List_PEMR_Element_Translated.Add(labReservationParent);
			}

			#endregion

			#region VisitTiming_SurgeryReservation

			if (pemrObject.List_VisitTiming_SurgeryReservation != null &&
				pemrObject.List_VisitTiming_SurgeryReservation.Count > 0)
			{
				PEMR_Translated surgeryReservationParent = null;
				surgeryReservationParent = CreateNewPEMR_Translated(
					pemrObject.List_VisitTiming_SurgeryReservation[0].OrderIndex,
					pemrObject.List_VisitTiming_SurgeryReservation[0].ElementName, "",
					pemrObject.List_VisitTiming_SurgeryReservation[0],
					pemrObject.List_VisitTiming_SurgeryReservation[0].PEMR_Element);

				foreach (VisitTiming_SurgeryReservation visitTimingSurgeryReservation in pemrObject
					.List_VisitTiming_SurgeryReservation)
				{
					PEMR_Translated SurgeryReservation = null;

					#region Requested Surgery

					if (visitTimingSurgeryReservation.Date != null)
						SurgeryReservation = CreateNewPEMR_Translated(visitTimingSurgeryReservation.OrderIndex, "Requested Surgery",
							visitTimingSurgeryReservation.ServiceName + " [" + Convert
								.ToDateTime(visitTimingSurgeryReservation.Date).ConvertDateTimeToString(false, false) +
							"] ", visitTimingSurgeryReservation, visitTimingSurgeryReservation.PEMR_Element);
					else
						SurgeryReservation = CreateNewPEMR_Translated(visitTimingSurgeryReservation.OrderIndex,
							"Requested Surgery", visitTimingSurgeryReservation.ServiceName,
							visitTimingSurgeryReservation, visitTimingSurgeryReservation.PEMR_Element);
					if (surgeryReservationParent.List_PEMR_Element_Translated == null)
						surgeryReservationParent.List_PEMR_Element_Translated = new List<PEMR_Translated>();
					surgeryReservationParent.List_PEMR_Element_Translated.Add(SurgeryReservation);
					#endregion

					#region Description
					if (visitTimingSurgeryReservation.Description != null &&
						!string.IsNullOrEmpty(visitTimingSurgeryReservation.Description) &&
						!string.IsNullOrWhiteSpace(visitTimingSurgeryReservation.Description))
					{
						SurgeryReservation = CreateNewPEMR_Translated(
							visitTimingSurgeryReservation.OrderIndex, "Recommendations",
							visitTimingSurgeryReservation.Description, visitTimingSurgeryReservation,
							visitTimingSurgeryReservation.PEMR_Element);
						if (surgeryReservationParent.List_PEMR_Element_Translated == null)
							surgeryReservationParent.List_PEMR_Element_Translated = new List<PEMR_Translated>();
						surgeryReservationParent.List_PEMR_Element_Translated.Add(SurgeryReservation);
					}
					#endregion
				}

				if (parent.List_PEMR_Element_Translated == null)
					parent.List_PEMR_Element_Translated = new List<PEMR_Translated>();
				parent.List_PEMR_Element_Translated.Add(surgeryReservationParent);
			}

			#endregion

			#region VisitTiming_Medication

			if (pemrObject.List_VisitTiming_Medication != null && pemrObject.List_VisitTiming_Medication.Count > 0)
			{
				PEMR_Translated medicationsParent = CreateNewPEMR_Translated(
					pemrObject.List_VisitTiming_Medication[0].OrderIndex,
					pemrObject.List_VisitTiming_Medication[0].ElementName, "",
					pemrObject.List_VisitTiming_Medication[0], pemrObject.List_VisitTiming_Medication[0].PEMR_Element);

				foreach (VisitTiming_Medication visitTimingMedication in pemrObject.List_VisitTiming_Medication)
				{
					PEMR_Translated medication = null;

					#region Medication / Dosage
					medication = CreateNewPEMR_Translated(visitTimingMedication.OrderIndex,
						"Medication / Dosage ",
						visitTimingMedication.MedicationName_English + " / " + visitTimingMedication.DosageName_English,
						visitTimingMedication, visitTimingMedication.PEMR_Element);
					if (medicationsParent.List_PEMR_Element_Translated == null)
						medicationsParent.List_PEMR_Element_Translated = new List<PEMR_Translated>();
					medicationsParent.List_PEMR_Element_Translated.Add(medication);
					#endregion

					#region TimesPerDuration
					if (visitTimingMedication.TimesPerDuration != null)
					{
						if (visitTimingMedication.TimeDuration_P_ID != null)
							medication = CreateNewPEMR_Translated(visitTimingMedication.OrderIndex,
								"Times Per Duration ",
								visitTimingMedication.TimesPerDuration + " " +
								visitTimingMedication.TimeDurationName_English, visitTimingMedication,
								visitTimingMedication.PEMR_Element);
						else
							medication = CreateNewPEMR_Translated(visitTimingMedication.OrderIndex,
								"Times Per Duration ", visitTimingMedication.TimesPerDuration.ToString(),
								visitTimingMedication, visitTimingMedication.PEMR_Element);
						if (medicationsParent.List_PEMR_Element_Translated == null)
							medicationsParent.List_PEMR_Element_Translated = new List<PEMR_Translated>();
						medicationsParent.List_PEMR_Element_Translated.Add(medication);
					}
					#endregion

					#region StartDate
					if (visitTimingMedication.StartDate != null)
					{
						if (visitTimingMedication.EndDate != null)
							medication = CreateNewPEMR_Translated(visitTimingMedication.OrderIndex, "Start Date ",
								"From (" + visitTimingMedication.StartDateString + ")" + " - To (" +
								visitTimingMedication.EndDateString + ")", visitTimingMedication,
								visitTimingMedication.PEMR_Element);
						else
							medication = CreateNewPEMR_Translated(visitTimingMedication.OrderIndex, "Start Date ",
								"From (" + visitTimingMedication.StartDateString + ")", visitTimingMedication,
								visitTimingMedication.PEMR_Element);
						medication.PEMR_ElementObject = visitTimingMedication;
						if (medicationsParent.List_PEMR_Element_Translated == null)
							medicationsParent.List_PEMR_Element_Translated = new List<PEMR_Translated>();
						medicationsParent.List_PEMR_Element_Translated.Add(medication);
					}
					#endregion

					#region Description
					if (visitTimingMedication.Description != null &&
						!string.IsNullOrEmpty(visitTimingMedication.Description) &&
						!string.IsNullOrWhiteSpace(visitTimingMedication.Description))
					{
						medication = CreateNewPEMR_Translated(visitTimingMedication.OrderIndex, "Instructions ",
							visitTimingMedication.Description, visitTimingMedication, visitTimingMedication.PEMR_Element);
						if (medicationsParent.List_PEMR_Element_Translated == null)
							medicationsParent.List_PEMR_Element_Translated = new List<PEMR_Translated>();
						medicationsParent.List_PEMR_Element_Translated.Add(medication);
					}
					#endregion
				}

				if (parent.List_PEMR_Element_Translated == null)
					parent.List_PEMR_Element_Translated = new List<PEMR_Translated>();
				parent.List_PEMR_Element_Translated.Add(medicationsParent);
			}

			#endregion

			#region VisitTiming_Diagnosis

			if (pemrObject.List_VisitTiming_MainDiagnosis != null &&
				pemrObject.List_VisitTiming_MainDiagnosis.Count > 0)
			{
				PEMR_Translated mainDiagnosisParent = CreateNewPEMR_Translated(
					pemrObject.List_VisitTiming_MainDiagnosis[0].OrderIndex,
					pemrObject.List_VisitTiming_MainDiagnosis[0].ElementName,
					"",
					pemrObject.List_VisitTiming_MainDiagnosis[0],
					pemrObject.List_VisitTiming_MainDiagnosis[0].PEMR_Element);
				PEMR_Translated diagnosis = null;
				VisitTiming_MainDiagnosis mainDiagnosisElement = null;

				if (pemrObject.List_VisitTiming_MainDiagnosis != null)
					foreach (VisitTiming_MainDiagnosis visitMainDiagnosis in pemrObject.List_VisitTiming_MainDiagnosis)
					{
						#region Diagnosis Type
						diagnosis = CreateNewPEMR_Translated(visitMainDiagnosis.OrderIndex, "Diagnosis Type",
							visitMainDiagnosis.DiagnosisTypeName, visitMainDiagnosis, visitMainDiagnosis.PEMR_Element);
						if (mainDiagnosisParent.List_PEMR_Element_Translated == null)
							mainDiagnosisParent.List_PEMR_Element_Translated = new List<PEMR_Translated>();
						mainDiagnosisParent.List_PEMR_Element_Translated.Add(diagnosis);
						#endregion

						#region General Description
						if (visitMainDiagnosis.GeneralDescription != null)
						{
							diagnosis = CreateNewPEMR_Translated(visitMainDiagnosis.OrderIndex, "General Recommendations",
								visitMainDiagnosis.GeneralDescription, visitMainDiagnosis, visitMainDiagnosis.PEMR_Element);
							if (mainDiagnosisParent.List_PEMR_Element_Translated == null)
								mainDiagnosisParent.List_PEMR_Element_Translated = new List<PEMR_Translated>();
							mainDiagnosisParent.List_PEMR_Element_Translated.Add(diagnosis);
						}

						mainDiagnosisElement = visitMainDiagnosis;
						#endregion
					}

				#region Diagnosis

				#region Ophthalmology
				if (pemrObject.List_VisitTiming_Diagnosis != null)
					if (pemrObject.List_VisitTiming_Diagnosis.FindAll(item => item.IsForOphthalmology) != null &&
						pemrObject.List_VisitTiming_Diagnosis.FindAll(item => item.IsForOphthalmology).Count > 0)
					{
						int orderIndex = -1;
						string diagnosisString = "";
						string diagnosisElementName = "";

						if (pemrObject.List_VisitTiming_Diagnosis.FindAll(
								item => item.Eye_P_ID != null &&
										Convert.ToInt32(item.Eye_P_ID).Equals(Convert.ToInt32(DB_EyeType_p.OD))).Count > 0)
						{
							diagnosisString = "Eye (OD) : ";
							foreach (VisitTiming_Diagnosis visitDiagnosis in pemrObject.List_VisitTiming_Diagnosis.FindAll(
								item => item.Eye_P_ID != null &&
										Convert.ToInt32(item.Eye_P_ID).Equals(Convert.ToInt32(DB_EyeType_p.OD))))
							{
								orderIndex = visitDiagnosis.OrderIndex;
								Diagnosis_cu diagnosisAdded = Diagnosis_cu.ItemsList.Find(item =>
									Convert.ToInt32(item.ID).Equals(Convert.ToInt32(visitDiagnosis.Diagnosis_CU_ID)));
								if (diagnosisAdded != null)
									diagnosisString += diagnosisAdded.Name_P + ", ";
							}

							diagnosis = CreateNewPEMR_Translated(orderIndex, "Diagnosis ", diagnosisString,
								mainDiagnosisElement, DB_PEMR_ElementType.Diagnosis);

							if (mainDiagnosisParent.List_PEMR_Element_Translated == null)
								mainDiagnosisParent.List_PEMR_Element_Translated = new List<PEMR_Translated>();
							mainDiagnosisParent.List_PEMR_Element_Translated.Add(diagnosis);
						}

						if (pemrObject.List_VisitTiming_Diagnosis.FindAll(
								item => item.Eye_P_ID != null &&
										Convert.ToInt32(item.Eye_P_ID).Equals(Convert.ToInt32(DB_EyeType_p.OS))).Count > 0)
						{
							diagnosisString = "Eye (OS) : ";
							foreach (VisitTiming_Diagnosis visitDiagnosis in pemrObject.List_VisitTiming_Diagnosis.FindAll(
								item => item.Eye_P_ID != null &&
										Convert.ToInt32(item.Eye_P_ID).Equals(Convert.ToInt32(DB_EyeType_p.OS))))
							{
								orderIndex = visitDiagnosis.OrderIndex;
								Diagnosis_cu diagnosisAdded = Diagnosis_cu.ItemsList.Find(item =>
									Convert.ToInt32(item.ID).Equals(Convert.ToInt32(visitDiagnosis.Diagnosis_CU_ID)));
								if (diagnosisAdded != null)
									diagnosisString += diagnosisAdded.Name_P + ", ";
							}

							diagnosis = CreateNewPEMR_Translated(orderIndex, "Diagnosis ", diagnosisString,
								mainDiagnosisElement, DB_PEMR_ElementType.Diagnosis);

							if (mainDiagnosisParent.List_PEMR_Element_Translated == null)
								mainDiagnosisParent.List_PEMR_Element_Translated = new List<PEMR_Translated>();
							mainDiagnosisParent.List_PEMR_Element_Translated.Add(diagnosis);
						}

						if (pemrObject.List_VisitTiming_Diagnosis.FindAll(
								item => item.Eye_P_ID != null &&
										Convert.ToInt32(item.Eye_P_ID).Equals(Convert.ToInt32(DB_EyeType_p.OU))).Count > 0)
						{
							diagnosisString = "Eye (OU) : ";
							foreach (VisitTiming_Diagnosis visitDiagnosis in pemrObject.List_VisitTiming_Diagnosis.FindAll(
								item => item.Eye_P_ID != null &&
										Convert.ToInt32(item.Eye_P_ID).Equals(Convert.ToInt32(DB_EyeType_p.OU))))
							{
								orderIndex = visitDiagnosis.OrderIndex;
								Diagnosis_cu diagnosisAdded = Diagnosis_cu.ItemsList.Find(item =>
									Convert.ToInt32(item.ID).Equals(Convert.ToInt32(visitDiagnosis.Diagnosis_CU_ID)));
								if (diagnosisAdded != null)
									diagnosisString += diagnosisAdded.Name_P + ", ";
							}

							diagnosis = CreateNewPEMR_Translated(orderIndex, "Diagnosis ", diagnosisString,
								mainDiagnosisElement, DB_PEMR_ElementType.Diagnosis);

							if (mainDiagnosisParent.List_PEMR_Element_Translated == null)
								mainDiagnosisParent.List_PEMR_Element_Translated = new List<PEMR_Translated>();
							mainDiagnosisParent.List_PEMR_Element_Translated.Add(diagnosis);
						}
					}
				#endregion

				#region Not Ophthalmology
				if (pemrObject.List_VisitTiming_Diagnosis != null)
					if (pemrObject.List_VisitTiming_Diagnosis.FindAll(item => !item.IsForOphthalmology) != null &&
						pemrObject.List_VisitTiming_Diagnosis.FindAll(item => !item.IsForOphthalmology).Count > 0)
					{
						int orderIndex = -1;
						string diagnosisString = "";
						string diagnosisElementName = "";

						foreach (VisitTiming_Diagnosis visitDiagnosis in pemrObject.List_VisitTiming_Diagnosis)
						{
							orderIndex = visitDiagnosis.OrderIndex;
							Diagnosis_cu diagnosisAdded = Diagnosis_cu.ItemsList.Find(item =>
								Convert.ToInt32(item.ID).Equals(Convert.ToInt32(visitDiagnosis.Diagnosis_CU_ID)));
							if (diagnosisAdded != null)
								diagnosisString += diagnosisAdded.Name_P + ", ";
						}

						diagnosis = CreateNewPEMR_Translated(orderIndex, "Diagnosis ", diagnosisString,
							mainDiagnosisElement, DB_PEMR_ElementType.Diagnosis);

						if (mainDiagnosisParent.List_PEMR_Element_Translated == null)
							mainDiagnosisParent.List_PEMR_Element_Translated = new List<PEMR_Translated>();
						mainDiagnosisParent.List_PEMR_Element_Translated.Add(diagnosis);
					}
				#endregion

				#endregion

				if (parent.List_PEMR_Element_Translated == null)
					parent.List_PEMR_Element_Translated = new List<PEMR_Translated>();
				parent.List_PEMR_Element_Translated.Add(mainDiagnosisParent);
			}

			#endregion

			if (parent.List_PEMR_Element_Translated != null && parent.List_PEMR_Element_Translated.Count > 0)
				parent.List_PEMR_Element_Translated =
					parent.List_PEMR_Element_Translated.OrderBy(item => item.OrderIndex).ToList();

			translatedList.Add(parent);

			return translatedList;
		}

		public static List<PEMR_Translated> Translate_PEMR_Condensed(PEMRObject pemrObject, DB_PEMR_ElementType elementType)
		{
			if (pemrObject == null)
				return null;

			List<PEMR_Translated> translatedList = new List<PEMR_Translated>();

			switch (elementType)
			{
				case DB_PEMR_ElementType.VisitTiming_Medications:
					if (pemrObject.List_VisitTiming_Medication != null &&
						pemrObject.List_VisitTiming_Medication.Count > 0)
					{
						PEMR_Translated medicationsParent = CreateNewPEMR_Translated(
							pemrObject.List_VisitTiming_Medication[0].OrderIndex,
							pemrObject.List_VisitTiming_Medication[0].ElementName, "",
							pemrObject.List_VisitTiming_Medication[0],
							pemrObject.List_VisitTiming_Medication[0].PEMR_Element);

						foreach (VisitTiming_Medication visitTimingMedication in pemrObject.List_VisitTiming_Medication.OrderBy(item => item.StartDate))
						{
							PEMR_Translated translated = new PEMR_Translated();
							translated.OrderIndex = visitTimingMedication.OrderIndex;
							translated.ElementName = visitTimingMedication.ElementName;
							translated.MedicationName_EnglishValue = visitTimingMedication.MedicationName_English;
							translated.MedicationName_ArabicValue = visitTimingMedication.MedicationName_Arabic;
							if (visitTimingMedication.TimesPerDuration != null)
							{
								translated.MedicationDosageName_EnglishValue =
									"From (" + visitTimingMedication.StartDateString + ") " + "- To (" +
									visitTimingMedication.EndDateString + ") - " + "(" +
									visitTimingMedication.TimesPerDuration + " - " +
									visitTimingMedication.TimeDurationName_English + ") - " + visitTimingMedication.DosageName_English;
								translated.MedicationDosageName_ArabicValue =
									"من (" + visitTimingMedication.StartDateString + ") " + "- إلى (" +
									visitTimingMedication.EndDateString + ") - " + "(" +
									visitTimingMedication.TimesPerDuration + " - " +
									visitTimingMedication.TimeDurationName_Arabic + ") - " + visitTimingMedication.DosageName_Arabic;
							}
							else if (visitTimingMedication.StartDate != null)
							{
								translated.MedicationDosageName_EnglishValue =
									"From (" +
									visitTimingMedication.StartDateString + ") " + "- To (" +
									visitTimingMedication.EndDateString + ") - " + visitTimingMedication.DosageName_English;
								translated.MedicationDosageName_ArabicValue =
									"من (" +
									visitTimingMedication.StartDateString + ") " + "- إلى (" +
									visitTimingMedication.EndDateString + ") - " + visitTimingMedication.DosageName_Arabic;
							}
							else
							{
								translated.MedicationDosageName_ArabicValue = visitTimingMedication.DosageName_Arabic;
								translated.MedicationDosageName_EnglishValue = visitTimingMedication.DosageName_English;
							}
							if (visitTimingMedication.Description != null)
								translated.MedicationReccommendationsNameValue = visitTimingMedication.Description;
							translated.PEMR_ElementObject = visitTimingMedication;
							translated.PEMR_TranslatedObject = translated;

							if (medicationsParent.List_PEMR_Element_Translated == null)
								medicationsParent.List_PEMR_Element_Translated = new List<PEMR_Translated>();
							medicationsParent.List_PEMR_Element_Translated.Add(translated);
						}

						translatedList.Add(medicationsParent);
					}

					break;
			}

			return translatedList;
		}

		public static PEMR_Translated CreateNewPEMR_Translated(int orderIdex, string elementName, string itemValue,
			IPEMR_Element element, DB_PEMR_ElementType elementType)
		{
			PEMR_Translated translated = new PEMR_Translated();
			translated.OrderIndex = orderIdex;
			translated.ElementName = elementName;
			translated.TranslatedItemValue = itemValue;
			translated.PEMR_ElementObject = element;
			translated.PEMR_TranslatedObject = translated;
			return translated;
		}

		public static List<DiagnosisCategory_cu> GetDiagnosisCategoriesList(bool isDoctorRelated)
		{
			return DiagnosisCategory_cu.ItemsList.FindAll(item => item.IsDoctorRelated.Equals(isDoctorRelated));
		}

		public static List<Diagnosis_cu> GetDiagnosisList(bool isDoctorRelated)
		{
			return Diagnosis_cu.ItemsList.FindAll(item => item.IsDoctorRelated.Equals(isDoctorRelated));
		}

		public static List<Diagnosis_cu> GetDiagnosisList(int doctorID)
		{
			if (doctorID == null)
				return null;

			List<Doctor_Diagnosis_cu> bridgeList = Doctor_Diagnosis_cu.ItemsList.FindAll(item =>
				Convert.ToInt32(item.Doctor_CU_ID).Equals(Convert.ToInt32(doctorID)));
			if (bridgeList == null || bridgeList.Count == 0)
				return null;
			List<Diagnosis_cu> diagnosisList = new List<Diagnosis_cu>();
			foreach (Doctor_Diagnosis_cu bridge in bridgeList)
			{
				Diagnosis_cu diagnosis = Diagnosis_cu.ItemsList.Find(item =>
					Convert.ToInt32(item.ID).Equals(Convert.ToInt32(bridge.Diagnosis_CU_ID)));
				if (diagnosis != null)
					diagnosisList.Add(diagnosis);
			}
			return diagnosisList;
		}

		public static List<Diagnosis_cu> GetDiagnosisList(object diagnosisCategoryID)
		{
			if (diagnosisCategoryID == null)
				return null;

			List<DiagnosisCategory_Diagnosis_cu> bridgesList = DiagnosisCategory_Diagnosis_cu.ItemsList.FindAll(item =>
				Convert.ToInt32(item.DiagnosisCategory_CU_ID).Equals(Convert.ToInt32(diagnosisCategoryID)));
			if (bridgesList.Count == 0)
				return null;
			List<Diagnosis_cu> diagnosisLIst = new List<Diagnosis_cu>();
			foreach (DiagnosisCategory_Diagnosis_cu bridge in bridgesList)
			{
				Diagnosis_cu diagnosis = Diagnosis_cu.ItemsList.Find(item =>
					Convert.ToInt32(item.ID).Equals(Convert.ToInt32(bridge.Diagnosis_CU_ID)));
				if (diagnosis != null)
					diagnosisLIst.Add(diagnosis);
			}
			return diagnosisLIst;
		}

		public static List<Diagnosis_cu> GetDiagnosisList(List<VisitTiming_Diagnosis> list, DB_EyeType_p eyeType)
		{
			if (list == null || list.Count == 0)
				return null;
			List<Diagnosis_cu> diagnosisList = new List<Diagnosis_cu>();
			List<VisitTiming_Diagnosis> newList = new List<VisitTiming_Diagnosis>();
			newList = list.FindAll(item => item.Eye_P_ID != null && item.Eye_P_ID.Equals((int)eyeType));
			switch (eyeType)
			{
				case DB_EyeType_p.All:
					newList = list;
					break;
			}

			foreach (VisitTiming_Diagnosis visitTimingDiagnosis in newList)
			{
				Diagnosis_cu diagnosis = Diagnosis_cu.ItemsList.Find(item =>
					Convert.ToInt32(item.ID).Equals(Convert.ToInt32(visitTimingDiagnosis.Diagnosis_CU_ID)));
				if (diagnosis != null)
					diagnosisList.Add(diagnosis);
			}
			return diagnosisList;
		}

		public static List<DiagnosisCategory_Diagnosis_cu> GetDiagnosisCategory_DiagnosisList(
			object diagnosisCategoryID)
		{
			if (diagnosisCategoryID == null)
				return null;
			return DiagnosisCategory_Diagnosis_cu.ItemsList.FindAll(item =>
				Convert.ToInt32(item.DiagnosisCategory_CU_ID).Equals(Convert.ToInt32(diagnosisCategoryID)));
		}

		public static List<DiagnosisCategory_cu> GetDiagnosisCategoriesList(object doctorID)
		{
			if (doctorID == null)
				return null;

			List<Doctor_DiagnosisCategory_cu> bridgeList = Doctor_DiagnosisCategory_cu.ItemsList.FindAll(item =>
				Convert.ToInt32(item.Doctor_CU_ID).Equals(Convert.ToInt32(doctorID)));
			if (bridgeList == null || bridgeList.Count == 0)
				return null;
			List<DiagnosisCategory_cu> diagnosisCategoriesList = new List<DiagnosisCategory_cu>();
			foreach (Doctor_DiagnosisCategory_cu bridge in bridgeList)
			{
				DiagnosisCategory_cu diagnosisCategory = DiagnosisCategory_cu.ItemsList.Find(item =>
					Convert.ToInt32(item.ID).Equals(Convert.ToInt32(bridge.DiagnosisCategory_CU_ID)));
				if (diagnosisCategory != null)
					diagnosisCategoriesList.Add(diagnosisCategory);
			}
			return diagnosisCategoriesList;
		}

		public static int GetPEMRElementOrderIndex(DB_PEMR_ElementType pemrElement)
		{
			int elementIndex = 0;

			PEMR_Elemet_p element =
				PEMR_Elemet_p.ItemsList.Find(item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(pemrElement)));
			PEMR_Elemet_p parentElement = null;
			if (element != null)
			{
				parentElement = PEMR_Elemet_p.ItemsList.Find(item =>
					Convert.ToInt32(item.ID).Equals(Convert.ToInt32(element.ParentID)));
				int parentElementIndex = Convert.ToInt32(parentElement.DefaultOrderIndex);

			}

			if (PEM_ElementPrintOrder_cu.ItemsList.Count == 0)
			{

			}

			return elementIndex;
		}

		public static List<SegmentSign_cu> GetSegmentSignsList(int segmentSignCategoryID)
		{
			if (SegmentSign_cu.ItemsList.Count == 0)
				return null;

			return SegmentSign_cu.ItemsList.FindAll(item =>
				Convert.ToInt32(item.SegmentCategory_CU_ID).Equals(segmentSignCategoryID));
		}

		public static List<GetPreviousVisitTiming_VisionRefractionReading_Result>
			GetPrevious_VisitTiming_VisionRefractionReading(object patientID, object dateFrom, object dateTo)
		{
			return GetPreviousVisitTiming_VisionRefractionReading_Result.GetItemsList(patientID, dateFrom, dateTo);
		}

		public static List<GetPreviousVisitTiming_EOMReading_Result> GetPrevious_VisitTiming_EOMReading_Result(
			object patientID, object dateFrom, object dateTo)
		{
			return GetPreviousVisitTiming_EOMReading_Result.GetItemsList(patientID, dateFrom, dateTo);
		}
	}
}
