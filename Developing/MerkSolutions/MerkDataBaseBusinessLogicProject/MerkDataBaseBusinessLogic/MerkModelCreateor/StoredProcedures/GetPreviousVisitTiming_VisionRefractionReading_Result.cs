using System;
using System.Collections.Generic;
using System.Linq;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;

namespace MerkDataBaseBusinessLogicProject
{
	public partial class GetPreviousVisitTiming_VisionRefractionReading_Result : DBCommon, IDBCommon
	{
		public static List<GetPreviousVisitTiming_VisionRefractionReading_Result> GetItemsList(object patientID, object dateFrom,
			object dateTo)
		{
			return
				DBContext_External
					.GetPreviousVisitTiming_VisionRefractionReading((int?) patientID, (DateTime?) dateFrom,
						(DateTime?) dateTo).ToList();
		}
	}
}
