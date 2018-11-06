using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;

namespace MerkDataBaseBusinessLogicProject
{
	public partial class Service_cu : DBCommon, IDBCommon
	{
		MerkFinanceEntities _context;
		private static List<Service_cu> _items;
		public static List<Service_cu> ItemsList = new List<Service_cu>();

		#region ColumnNames

		public static String ServiceCategory_CU_ID_ColumnaName
		{
			get { return "ServiceCategory_CU_ID"; }
		}

		public static String ServiceType_P_ID_ColumnaName
		{
			get { return "ServiceType_P_ID"; }
		}

		public static String ParentService_CU_ID_ColumnaName
		{
			get { return "ParentService_CU_ID"; }
		}

		public static String InternalCode_ColumnaName
		{
			get { return "InternalCode"; }
		}

		public static String EnforceCategorization_ColumnaName
		{
			get { return "EnforceCategorization"; }
		}

		public static String IsDailyCharged_ColumnaName
		{
			get { return "IsDailyCharged"; }
		}

		public static String DefaultPrice_ColumnaName
		{
			get { return "DefaultPrice"; }
		}

		public static String AllowAddmission_ColumnaName
		{
			get { return "AllowAddmission"; }
		}

		public static String Description_ColumnaName
		{
			get { return "Description"; }
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
			ItemsList = DBContext_External.Service_cu.Where(item => item.IsOnDuty).OrderBy(item => item.ID).ToList();
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
			get { return (int)DB_TableIdentity.Service_cu; }
		}

		List<string> IDBCommon.ChildrenItemsList
		{
			get { return null; }
		}

		public string EntityName
		{
			get { return "Service_cu"; }
		}

		public IDBCommon GetSpecificEntity(MerkFinanceEntities context, int id)
		{
			return context.Service_cu.FirstOrDefault(item => item.ID.Equals(id));
		}

		#endregion

		public ServiceCategory_cu ServiceCategory
		{
			get
			{
				if (ServiceCategory_CU_ID == null)
					return null;
				ServiceCategory_cu serviceCategory =
					ServiceCategory_cu.ItemsList.Find(item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(ServiceCategory_CU_ID)));
				return serviceCategory;
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
	}
}
