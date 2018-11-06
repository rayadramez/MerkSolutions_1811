using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;

namespace MerkDataBaseBusinessLogicProject
{
	public partial class ServiceCategory_cu : DBCommon, IDBCommon
	{
		MerkFinanceEntities _context;
		private static List<ServiceCategory_cu> _items;
		public static List<ServiceCategory_cu> ItemsList = new List<ServiceCategory_cu>();

		#region ColumnNames

		public static String ParentServiceCategory_CU_ID_ColumnaName
		{
			get { return "ParentServiceCategory_CU_ID"; }
		}

		public static String ServiceType_P_ID_ColumnaName
		{
			get { return "ServiceType_P_ID"; }
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
			ItemsList = DBContext_External.ServiceCategory_cu.Where(item => item.IsOnDuty).OrderBy(item => item.ID).ToList();
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
			get { return (int)DB_TableIdentity.ServiceCategory_cu; }
		}

		List<string> IDBCommon.ChildrenItemsList
		{
			get { return null; }
		}

		public string EntityName
		{
			get { return "ServiceCategory_cu"; }
		}

		public IDBCommon GetSpecificEntity(MerkFinanceEntities context, int id)
		{
			return context.ServiceCategory_cu.FirstOrDefault(item => item.ID.Equals(id));
		}

		#endregion
	}
}
