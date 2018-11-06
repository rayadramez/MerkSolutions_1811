using System;
using System.Collections.Generic;
using System.Linq;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;

namespace MerkDataBaseBusinessLogicProject
{
	public partial class GetPatientDepositBalance_Result : DBCommon, IDBCommon
	{
		public static List<GetPatientDepositBalance_Result> GetItemsList(object patientID, object isOnDuty,
			object serviceCategoryID, object serviceTypeID, object serviceId,  object userID)
		{
			return
				DBContext_External.GetPatientDepositBalance((int?)patientID, (bool?)isOnDuty, (int?)serviceCategoryID,
					(int?)serviceTypeID, (int?)serviceId, (int?)userID).ToList();
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

		public ServiceCategory_cu ServiceCategory
		{
			get
			{
				if (Service == null)
					return null;

				ServiceCategory_cu service =
					ServiceCategory_cu.ItemsList.Find(item =>
						Convert.ToInt32(item.ID).Equals(Convert.ToInt32(Service.ServiceCategory_CU_ID)));
				return service;
			}
		}

		public string ServiceCategoryName
		{
			get
			{
				if (ServiceCategory == null)
					return string.Empty;

				return ServiceCategory.Name_P;
			}
		}

		public ServiceType_p ServiceType
		{
			get
			{
				if (Service == null)
					return null;

				ServiceType_p service =
					ServiceType_p.ItemsList.Find(item =>
						Convert.ToInt32(item.ID).Equals(Convert.ToInt32(Service.ServiceType_P_ID)));
				return service;
			}
		}

		public string ServiceTypeName
		{
			get
			{
				if (ServiceType == null)
					return string.Empty;

				return ServiceType.Name_P;
			}
		}

		public string DateString
		{
			get { return Convert.ToDateTime(Date).ConvertDateTimeToString(true, false); }
		}
	}
}
