using System;
using System.Collections.Generic;
using System.Linq;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;

namespace MerkDataBaseBusinessLogicProject
{
	public partial class GetTotalServiceAndDoctorRevenues_Result : DBCommon, IDBCommon
	{
		public static List<GetTotalServiceAndDoctorRevenues_Result> GetItemsList(object invoiceTypeID, object serviceID,
			object serviceTypeID, object serviceCategoryID, object isOnDuty, object fromDate, object toDate, object doctorID)
		{
			return
				DBContext_External.GetTotalServiceAndDoctorRevenues((int?) invoiceTypeID, (int?) serviceID, (int?) serviceTypeID,
					(int?) serviceCategoryID, (int?) doctorID, (bool?)isOnDuty, (DateTime?) fromDate, (DateTime?) toDate).ToList();
		}
	}
}
