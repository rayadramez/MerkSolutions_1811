using System.Collections.Generic;
using MerkDataBaseBusinessLogicProject;

namespace MVCBusinessLogicLibrary.Viewers
{
	public interface IUser_Application_Viewer : IViewer
	{
		List<User_Application_cu> List_User_Application { get; set; }
	}
}
