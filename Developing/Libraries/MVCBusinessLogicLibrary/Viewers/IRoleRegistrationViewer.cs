using System.Collections.Generic;
using MerkDataBaseBusinessLogicProject;

namespace MVCBusinessLogicLibrary.Viewers
{
	public interface IRoleRegistrationViewer : IViewer
	{
		List<RoleRegistration_cu> List_RoleRegistration { get; set; }
	}
}
