namespace MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.PEMRBusinessLogic
{
	public interface IPEMR_MedicalHistory
	{
		object FurtherDetails { get; set; }
		object HasDiabetes { get; set; }
		object DiabetesType { get; set; }
		object HbA1C { get; set; }
		object IsDiabetesControlled { get; set; }
		object DiabetesMedicationType { get; set; }
		object DiabetesMedicationDuration { get; set; }
		object DiabetesMedicationDurationType { get; set; }
		object DiabetesMedication { get; set; }
		object DiabetesDosage { get; set; }
		object IsHypertension { get; set; }
		object IsHypertensionControlled { get; set; }
		object HypertensionMedicationDurationType { get; set; }
		object HypertensionMedicationDuration { get; set; }
		object HypertensionMedication { get; set; }
		object HypertensionDosage { get; set; }
		object HasDrugAllergies { get; set; }
		object TriggersDrugAllergies { get; set; }
		object HasHepatitis { get; set; }
		object HasAsthma { get; set; }
	}
}
