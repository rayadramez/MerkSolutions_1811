using System.Collections.Generic;
using MerkDataBaseBusinessLogicProject;

namespace MVCBusinessLogicLibrary.Viewers
{
	public interface IUser_UserGroup_Viewer : IViewer
	{
		List<User_UserGroup_cu> List_User_UserGroup { get; set; }
	}
}
