using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;

namespace MerkDataBaseBusinessLogicProject
{
	public partial class ServicePrice_cu : DBCommon, IDBCommon
	{
		MerkFinanceEntities _context;
		private static List<ServicePrice_cu> _items;
		public static List<ServicePrice_cu> ItemsList = new List<ServicePrice_cu>();

		#region ColumnNames

		public static String Service_CU_ID_ColumnaName
		{
			get { return "Service_CU_ID"; }
		}

		public static String ServiceCategory_CU_ID_ColumnaName
		{
			get { return "ServiceCategory_CU_ID"; }
		}

		public static String Doctor_CU_ID_ColumnaName
		{
			get { return "Doctor_CU_ID"; }
		}

		public static String DoctorCategory_CU_ID_ColumnaName
		{
			get { return "DoctorCategory_CU_ID"; }
		}

		public static String Price_ColumnaName
		{
			get { return "Price"; }
		}

		public static String InsuranceCarrier_InsuranceLevel_CU_ID_ColumnaName
		{
			get { return "InsuranceCarrier_InsuranceLevel_CU_ID"; }
		}

		public static String InsurancePrice_ColumnaName
		{
			get { return "InsurancePrice"; }
		}

		#endregion

		public override bool LoadFromDB
		{
			get { return true; }
		}

		public override DBCommonEntitiesType TableType
		{
			get { return DBCommonEntitiesType.CustomUserEntities; }
		}

		public override bool LoadItemsList()
		{
			ItemsList.Clear();
			ItemsList = DBContext_External.ServicePrice_cu.Where(item => item.IsOnDuty).OrderBy(item => item.ID).ToList();
			return true;
		}

		#region Implementation of IDBCommon

		public override IList ReGenerateList()
		{
			LoadItemsList();

			return ItemsList;
		}

		public int TableIdentityID
		{
			get { return (int)DB_TableIdentity.ServicePrice_cu; }
		}

		List<string> IDBCommon.ChildrenItemsList
		{
			get { return null; }
		}

		public string EntityName
		{
			get { return "ServicePrice_cu"; }
		}

		public IDBCommon GetSpecificEntity(MerkFinanceEntities context, int id)
		{
			return context.ServicePrice_cu.FirstOrDefault(item => item.ID.Equals(id));
		}

		#endregion

		public string InsuranceCarrierName
		{
			get
			{
				if (InsuranceCarrier_InsuranceLevel_CU_ID != null)
				{
					InsuranceCarrier_InsuranceLevel_cu insuranceBridge =
						InsuranceCarrier_InsuranceLevel_cu.ItemsList.Find(
							item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(InsuranceCarrier_InsuranceLevel_CU_ID)));
					if (insuranceBridge != null)
					{
						InsuranceCarrier_cu insuranceCarrier =
							InsuranceCarrier_cu.ItemsList.Find(
								item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(insuranceBridge.InsuranceCarrier_CU_ID)));
						if (insuranceCarrier != null)
							return insuranceCarrier.Name_P;
					}
				}

				return String.Empty;
			}
		}

		public string InsuranceLevelName
		{
			get
			{
				if (InsuranceCarrier_InsuranceLevel_CU_ID != null)
				{
					InsuranceCarrier_InsuranceLevel_cu insuranceBridge =
						InsuranceCarrier_InsuranceLevel_cu.ItemsList.Find(
							item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(InsuranceCarrier_InsuranceLevel_CU_ID)));
					if (insuranceBridge != null)
					{
						InsuranceLevel_cu insuranceLevel =
							InsuranceLevel_cu.ItemsList.Find(
								item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(insuranceBridge.InsuranceLevel_CU_ID)));
						if (insuranceLevel != null)
							return insuranceLevel.Name_P;
					}
				}

				return String.Empty;
			}
		}

		public string ServiceName
		{
			get
			{
				if (Service_CU_ID != null)
				{
					Service_cu service =
						Service_cu.ItemsList.Find(item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(Service_CU_ID)));
					if (service != null)
						return service.Name_P;
				}
				
				return String.Empty;
			}
		}

		public string ServiceCategoryName
		{
			get
			{
				if (ServiceCategory_CU_ID != null)
				{
					ServiceCategory_cu serviceCategory =
						ServiceCategory_cu.ItemsList.Find(item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(ServiceCategory_CU_ID)));
					if (serviceCategory != null)
						return serviceCategory.Name_P;
				}

				return String.Empty;
			}
		}

		public string DoctorName
		{
			get
			{
				if (Doctor_CU_ID != null)
				{
					Doctor_cu doctor =
						Doctor_cu.ItemsList.Find(item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(Doctor_CU_ID)));
					if (doctor != null)
						return doctor.Name_P;
				}

				return String.Empty;
			}
		}

		public string DoctorCategoryName
		{
			get
			{
				if (DoctorCategory_CU_ID != null)
				{
					DoctorCategory_cu doctorCategory =
						DoctorCategory_cu.ItemsList.Find(item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(DoctorCategory_CU_ID)));
					if (doctorCategory != null)
						return doctorCategory.Name_P;
				}

				return String.Empty;
			}
		}
	}
}
