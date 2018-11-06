using System;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;

namespace WindowsViewerLibraries.WIndowsMVCBusinessLogic.Views.Interfaces
{
	public interface ISaveViewerAction
	{
		bool CheckIfActiveDBItemExists();
		bool ValidateBeforeSave(ref String message);
		bool BeforeSave();
		bool SaveChanges(DB_CommonTransactionType commonTransactionType);
		bool AddToParent();
		bool AfterSave();
	}
}
