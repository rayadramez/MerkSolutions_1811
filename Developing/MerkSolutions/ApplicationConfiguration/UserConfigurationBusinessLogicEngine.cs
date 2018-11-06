using System;
using System.Collections.Generic;
using MerkDataBaseBusinessLogicProject;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;

namespace ApplicationConfiguration
{
	public class UserConfigurationBusinessLogicEngine
	{
		public static bool CanUserAccessThisApplication(User_cu user, DB_Application application)
		{
			if (user == null)
				return false;

			User_Application_cu userApplcaition =
				User_Application_cu.ItemsList.Find(
					item =>
						Convert.ToInt32(item.User_CU_ID).Equals(Convert.ToInt32(user.ID)) &&
						item.Application_P_ID.Equals((int) application));
			if (userApplcaition != null)
				return true;

			List<User_UserGroup_cu> userAttachedGroups =
				User_UserGroup_cu.ItemsList.FindAll(item => Convert.ToInt32(item.User_CU_ID).Equals(Convert.ToInt32(user.ID)));
			if (userAttachedGroups.Count == 0)
				return false;

			foreach (User_UserGroup_cu userAttachedGroup in userAttachedGroups)
			{
				UserGroup_cu userGroup =
					UserGroup_cu.ItemsList.Find(
						item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(userAttachedGroup.UserGroup_CU_ID)));
				if (userGroup == null)
					continue;

				UserGroup_Application_cu userGroupApplication =
					UserGroup_Application_cu.ItemsList.Find(
						item =>
							Convert.ToInt32(item.UserGroup_CU_ID).Equals(Convert.ToInt32(userGroup.ID)) &&
							Convert.ToInt32(item.Application_P_ID).Equals((int) application));
				if (userGroupApplication != null)
					return true;
			}

			return false;
		}

		public static List<RoleRegistration_cu> GetAllApplicationRoles(DB_Application application)
		{
			return null;
			//return RoleRegistration_cu.ItemsList.FindAll(item => Convert.ToInt32(item.Application_P_ID).Equals((int) application));
		}
	}
}
