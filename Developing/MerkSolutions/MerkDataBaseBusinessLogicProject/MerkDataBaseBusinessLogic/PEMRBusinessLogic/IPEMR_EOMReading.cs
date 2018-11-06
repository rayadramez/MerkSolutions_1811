namespace MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.PEMRBusinessLogic
{
	public interface IPEMR_EOMReading
	{
		object TakenDateTime { get; set; }
		object SR_OD { get; set; }
		object SR_OS { get; set; }
		object LR_OD { get; set; }
		object LR_OS { get; set; }
		object IR_OD { get; set; }
		object IR_OS { get; set; }
		object IO_OD { get; set; }
		object IO_OS { get; set; }
		object MR_OD { get; set; }
		object MR_OS { get; set; }
		object SO_OD { get; set; }
		object SO_OS { get; set; }
	}
}
