using System.Collections.Generic;
using MerkDataBaseBusinessLogicProject;

namespace MVCBusinessLogicLibrary.Viewers
{
	public interface IUserGroup_Application_Viewer : IViewer
	{
		List<UserGroup_Application_cu> List_UserGroup_Application { get; set; }
	}
}
