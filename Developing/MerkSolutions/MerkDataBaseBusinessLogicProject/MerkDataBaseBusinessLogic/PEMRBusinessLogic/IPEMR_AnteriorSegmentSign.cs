using System.Collections.Generic;

namespace MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.PEMRBusinessLogic
{
	public interface IPEMR_AnteriorSegmentSign
	{
		object FurtherDetails_OD { get; set; }
		object FurtherDetails_OS { get; set; }
		List<SegmentSign_cu> AddedAnteriorSegmentSign_OD { get; set; }
		List<SegmentSign_cu> AddedAnteriorSegmentSign_OS { get; set; }
	}
}
