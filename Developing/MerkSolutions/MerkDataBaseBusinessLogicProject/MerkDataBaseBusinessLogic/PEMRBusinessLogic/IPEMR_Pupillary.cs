namespace MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.PEMRBusinessLogic
{
	public interface IPEMR_Pupillary
	{
		object IsNoAbnormalitiesFound_OD { get; set; }
		object IsNoAbnormalitiesFound_OS { get; set; }
		object PupillaryAbnormalities_CU_ID_OD { get; set; }
		object PupillaryAbnormalities_CU_ID_OS { get; set; }
		object IsRAPD_OD { get; set; }
		object IsRAPD_OS { get; set; }
		object PupillaryRAPDGradingScale_P_ID_OD { get; set; }
		object PupillaryRAPDGradingScale_P_ID_OS { get; set; }
		object PupillaryRAPDCauses_CU_ID_OD { get; set; }
		object PupillaryRAPDCauses_CU_ID_OS { get; set; }
		object PupillarySize_P_ID_OD { get; set; }
		object PupillarySize_P_ID_OS { get; set; }
		object PupillaryShape_P_OD { get; set; }
		object PupillaryShape_P_OS { get; set; }
		object Scotopic_OD { get; set; }
		object HighPhotopic_OD { get; set; }
		object LowPhotopic_OD { get; set; }
		object HighMesopic_OD { get; set; }
		object LowMesopic_OD { get; set; }
		object Scotopic_OS { get; set; }
		object HighPhotopic_OS { get; set; }
		object LowPhotopic_OS { get; set; }
		object HighMesopic_OS { get; set; }
		object LowMesopic_OS { get; set; }
		object FurtherDetails_OD { get; set; }
		object FurtherDetails_OS { get; set; }
	}
}
