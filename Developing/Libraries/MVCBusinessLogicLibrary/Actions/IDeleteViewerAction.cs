using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;

namespace WindowsViewerLibraries.WIndowsMVCBusinessLogic.Views.Interfaces
{
	public interface IDeleteViewerAction
	{
		bool ValidateBeforeDelete();
		bool BeforeDelete(IDBCommon entity);
		bool Delete(IDBCommon entity);
		bool DeleteFromParent();
		bool AfterDelete(IDBCommon entity);
	}
}
