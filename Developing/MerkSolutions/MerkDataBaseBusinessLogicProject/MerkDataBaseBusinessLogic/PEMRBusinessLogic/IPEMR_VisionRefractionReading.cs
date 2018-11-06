namespace MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.PEMRBusinessLogic
{
	public interface IPEMR_VisionRefractionReading
	{
		object TakenDate { get; set; }
		object TakenTime { get; set; }
		object VisionRefractionReadingTypeID { get; set; }
		object UVA_OU { get; set; }
		object NVA_OU { get; set; }
		object NVAAmount_OU { get; set; }
		object Distance_OD { get; set; }
		object Distance_OS { get; set; }
		object NVA_OD { get; set; }
		object NVA_OS { get; set; }
		object NVAAmount_OD { get; set; }
		object NVAAmount_OS { get; set; }
		object IsIgnored_OD { get; set; }
		object IsIgnored_OS { get; set; }
		object IsError_OD { get; set; }
		object IsError_OS { get; set; }
		object RatingAmount_OD { get; set; }
		object RatingAmount_OS { get; set; }
		object SphereAmount_OD { get; set; }
		object SphereAmount_OS { get; set; }
		object CylinderAmount_OD { get; set; }
		object CylinderAmount_OS { get; set; }
		object AxisAmount_OD { get; set; }
		object AxisAmount_OS { get; set; }
		object UVA_OD { get; set; }
		object UVA_OS { get; set; }
		object Add_OD { get; set; }
		object Add_OS { get; set; }
		object Remarks_OD { get; set; }
		object Remarks_OS { get; set; }
	}
}
