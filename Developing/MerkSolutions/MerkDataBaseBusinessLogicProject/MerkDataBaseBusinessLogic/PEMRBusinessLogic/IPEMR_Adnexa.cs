using System.Collections.Generic;

namespace MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.PEMRBusinessLogic
{
	public interface IPEMR_Adnexa
	{
		object FurtherDetails_OD { get; set; }
		object FurtherDetails_OS { get; set; }
		List<SegmentSign_cu> AddedAdnexaSegmentSign_OD { get; set; }
		List<SegmentSign_cu> AddedAdnexaSegmentSign_OS { get; set; }
	}
}
