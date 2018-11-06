using System;
using System.Collections.Generic;
using System.Linq;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;

namespace MerkDataBaseBusinessLogicProject
{
	public partial class GetPreviousMedicalVisits_Result : DBCommon, IDBCommon
	{
		public static List<GetPreviousMedicalVisits_Result> GetItemsList(object patientID, object isOnDUty,
			object serviceID, object visitTimingDateFrom, object visitTimingDateTo, object userID)
		{
			if (patientID != null)
				return DBContext_External.GetPreviousMedicalVisits(Convert.ToInt32(patientID), isOnDUty as bool?,
						serviceID as int?, visitTimingDateFrom as DateTime?, visitTimingDateTo as DateTime?,
						userID as int?)
					.ToList();
			return DBContext_External.GetPreviousMedicalVisits(null, isOnDUty as bool?,
					serviceID as int?, visitTimingDateFrom as DateTime?, visitTimingDateTo as DateTime?,
					userID as int?)
				.ToList();
		}

		public Service_cu Service
		{
			get
			{
				Service_cu service =
					Service_cu.ItemsList.Find(item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(ServiceID)));
				return service;
			}
		}

		public string ServiceName
		{
			get
			{
				if (Service == null)
					return string.Empty;

				return Service.Name_P;
			}
		}

		public string SignInDateString
		{
			get
			{
				if (SignInDate == null)
					return String.Empty;

				return Convert.ToDateTime(SignInDate).ConvertDateTimeToString(true, true);
			}
		}

		public string SignOutDateString
		{
			get
			{
				if (SignOutDate == null)
					return String.Empty;

				return Convert.ToDateTime(SignOutDate).ConvertDateTimeToString(true, true);
			}
		}
	}
}
