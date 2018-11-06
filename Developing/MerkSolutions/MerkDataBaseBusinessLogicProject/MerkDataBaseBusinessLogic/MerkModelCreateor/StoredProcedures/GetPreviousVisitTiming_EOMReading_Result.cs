using System;
using System.Collections.Generic;
using System.Linq;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;

namespace MerkDataBaseBusinessLogicProject
{
	public partial class GetPreviousVisitTiming_EOMReading_Result : DBCommon, IDBCommon
	{
		public static List<GetPreviousVisitTiming_EOMReading_Result> GetItemsList(object patientID, object dateFrom,
			object dateTo)
		{
			return
				DBContext_External
					.GetPreviousVisitTiming_EOMReading((int?)patientID, (DateTime?)dateFrom, (DateTime?)dateTo).ToList();
		}
	}
}
