using System.Collections.Generic;
using WindowsViewerLibraries.WIndowsMVCBusinessLogic.Views.Interfaces;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;
using MVCBusinessLogicLibrary.Actions;
using MVCBusinessLogicLibrary.Viewers;

namespace MVCBusinessLogicLibrary.MVCDataCollectors
{
	public interface IMVCDataCollector<TEntity> : INewViewerAction, ISaveViewerAction,
		IEditViewerAction, IDeleteViewerAction, ICloseViewerAction, IPrintViewerAction, ISearchAction
		where TEntity : DBCommon, IDBCommon, new()
	{
		AbstractDataCollector<TEntity> ActiveCollector { get; set; }
		AbstractDataCollector<TEntity> ParentActiveCollector { get; set; }
		IDBCommon ActiveDBItem { get; set; }
		IDBCommon ParentActiveDBItem { get; set; }
		IViewer ActiveViewer { get; set; }
		bool Collect(AbstractDataCollector<TEntity> collector);
		IEnumerable<TEntity> GetItemsList();
		string MessageToView { get; set; }
	}
}
