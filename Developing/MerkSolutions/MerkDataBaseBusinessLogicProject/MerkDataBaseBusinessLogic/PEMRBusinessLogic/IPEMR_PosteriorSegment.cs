using System.Collections.Generic;

namespace MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.PEMRBusinessLogic
{
	public interface IPEMR_PosteriorSegment
	{
		object FurtherDetails_OD { get; set; }
		object FurtherDetails_OS { get; set; }
		List<SegmentSign_cu> AddedPosteriorSegmentSign_OD { get; set; }
		List<SegmentSign_cu> AddedPosteriorSegmentSign_OS { get; set; }
	}
}
