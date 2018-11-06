using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;

namespace WindowsViewerLibraries.WIndowsMVCBusinessLogic.Views.Interfaces
{
	public interface IEditViewerAction
	{
		void BeforeEdit(IDBCommon entity);
		void Edit(IDBCommon entity);
		void AfterEdit(IDBCommon entity);
	}
}
