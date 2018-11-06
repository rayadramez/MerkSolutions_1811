namespace MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.PEMRBusinessLogic
{
	public interface IPEMR_VitalSign 
	{
		object TakenDate { get; set; }
		object TakenTime { get; set; }
		object GeneralDescription { get; set; }
		object Weight_Unit { get; set; }
		object Weight_Amount { get; set; }
		object Height_Unit { get; set; }
		object HeightAmount { get; set; }
		object Weight_Description { get; set; }
		object Temperature_Unit { get; set; }
		object Temperature_Amount { get; set; }
		object Temperature_Description { get; set; }
		object BloodPressure_AmountHigh { get; set; }
		object BloodPressure_AmountLow { get; set; }
		object Pulse_Amount { get; set; }
		object Pulse_Reg { get; set; }
		object BloodPressure_Description { get; set; }
		object Respiration_Amount { get; set; }
		object Oxygen_Amount { get; set; }
		object FIO2 { get; set; }
		object SPO2_Amount { get; set; }
		object Respiration_Description { get; set; }

	}
}
