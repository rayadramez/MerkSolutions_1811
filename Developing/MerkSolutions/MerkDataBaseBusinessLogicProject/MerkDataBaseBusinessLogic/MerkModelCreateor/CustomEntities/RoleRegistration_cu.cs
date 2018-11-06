using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;

namespace MerkDataBaseBusinessLogicProject
{
	public partial class RoleRegistration_cu : DBCommon, IDBCommon
	{
		public static List<RoleRegistration_cu> ItemsList = new List<RoleRegistration_cu>();

		#region ColumnNames

		public static String Application_P_ID_ColumnaName
		{
			get { return "Application_P_ID"; }
		}

		public static String Role_P_ID_ColumnaName
		{
			get { return "Role_P_ID"; }
		}

		public static String User_CU_ID_ColumnaName
		{
			get { return "User_CU_ID"; }
		}

		public static String UserGroup_CU_ID_ColumnaName
		{
			get { return "UserGroup_CU_ID"; }
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
			ItemsList = DBContext_External.RoleRegistration_cu.ToList();
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
			get { return (int)DB_TableIdentity.RoleRegistration_cu; }
		}

		List<string> IDBCommon.ChildrenItemsList
		{
			get { return null; }
		}

		public string EntityName
		{
			get { return "RoleRegistration_cu"; }
		}

		public IDBCommon GetSpecificEntity(MerkFinanceEntities context, int id)
		{
			return context.RoleRegistration_cu.FirstOrDefault(item => item.ID.Equals(id));
		}

		#endregion
	}
}
