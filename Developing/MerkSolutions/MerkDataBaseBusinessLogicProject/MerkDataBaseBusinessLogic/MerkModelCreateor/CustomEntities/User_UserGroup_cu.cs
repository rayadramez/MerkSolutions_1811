using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;

namespace MerkDataBaseBusinessLogicProject
{
	public partial class User_UserGroup_cu : DBCommon, IDBCommon
	{
		public static List<User_UserGroup_cu> ItemsList = new List<User_UserGroup_cu>();

		#region ColumnNames

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
			ItemsList = DBContext_External.User_UserGroup_cu.Where(item => item.IsOnDuty).ToList();
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
			get { return (int)DB_TableIdentity.User_UserGroup_cu; }
		}

		List<string> IDBCommon.ChildrenItemsList
		{
			get { return null; }
		}

		public string EntityName
		{
			get { return "User_UserGroup_cu"; }
		}

		public IDBCommon GetSpecificEntity(MerkFinanceEntities context, int id)
		{
			return context.User_UserGroup_cu.FirstOrDefault(item => item.ID.Equals(id));
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

		public User_cu User
		{
			get
			{
				User_cu user = User_cu.ItemsList.Find(item =>
					Convert.ToInt32(item.ID).Equals(Convert.ToInt32(User_CU_ID)));
				return user;
			}
		}

		public string UserName
		{
			get
			{
				if (User == null)
					return string.Empty;

				return User.FullName.ToString();
			}
		}
	}
}
