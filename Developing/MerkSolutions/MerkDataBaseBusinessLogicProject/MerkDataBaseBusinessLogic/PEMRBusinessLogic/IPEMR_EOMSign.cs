using System.Collections.Generic;

namespace MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.PEMRBusinessLogic
{
	public interface IPEMR_EOMSign
	{
		object FurtherDetails_OD { get; set; }
		object FurtherDetails_OS { get; set; }
		List<SegmentSign_cu> AddedEOMSign_OD { get; set; }
		List<SegmentSign_cu> AddedEOMSign_OS { get; set; }
	}
}
