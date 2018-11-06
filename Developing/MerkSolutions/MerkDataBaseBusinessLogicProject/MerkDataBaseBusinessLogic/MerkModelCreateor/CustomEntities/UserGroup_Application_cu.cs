using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;

namespace MerkDataBaseBusinessLogicProject
{
	public partial class UserGroup_Application_cu : DBCommon, IDBCommon
	{
		public static List<UserGroup_Application_cu> ItemsList = new List<UserGroup_Application_cu>();

		#region ColumnNames

		public static String UserGroup_CU_ID_ColumnaName
		{
			get { return "UserGroup_CU_ID"; }
		}

		public static String Application_P_ID_ColumnaName
		{
			get { return "Application_P_ID"; }
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
			ItemsList = DBContext_External.UserGroup_Application_cu.Where(item => item.IsOnDuty).ToList();
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
			get { return (int)DB_TableIdentity.UserGroup_Application_cu; }
		}

		List<string> IDBCommon.ChildrenItemsList
		{
			get { return null; }
		}

		public string EntityName
		{
			get { return "UserGroup_Application_cu"; }
		}

		public IDBCommon GetSpecificEntity(MerkFinanceEntities context, int id)
		{
			return context.UserGroup_Application_cu.FirstOrDefault(item => item.ID.Equals(id));
		}

		#endregion

		public UserGroup_cu UserGroup
		{
			get
			{
				UserGroup_cu userGroup = UserGroup_cu.ItemsList.Find(item =>
					Convert.ToInt32(item.ID).Equals(Convert.ToInt32(UserGroup_CU_ID)));
				return userGroup;
			}
		}

		public string UserGroupName
		{
			get
			{
				if (UserGroup == null)
					return string.Empty;

				return UserGroup.Name_P;
			}
		}

		public Application_p Application
		{
			get
			{
				Application_p application = Application_p.ItemsList.Find(item =>
					Convert.ToInt32(item.ID).Equals(Convert.ToInt32(Application_P_ID)));
				return application;
			}
		}

		public string ApplicationName
		{
			get
			{
				if (Application == null)
					return string.Empty;

				return Application.Name_P;
			}
		}
	}
}
