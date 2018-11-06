using System.Collections.Generic;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;
using MVCBusinessLogicLibrary.MVCDataCollectors;
using MVCBusinessLogicLibrary.MVCFactories;
using MVCBusinessLogicLibrary.Viewers;

namespace MVCBusinessLogicLibrary.Controller
{
	public interface IMVCController<TEntity> : IMVCControllerFactory, IViewer, IMVCDataCollector<TEntity>
		where TEntity : DBCommon, IDBCommon, new()
	{
		IMVCController<TEntity> ParentController { get; set; }
		ViewerName ActiveViewerName { get; set; }
		List<IMVCController<TEntity>> List_ChildrenControllers { get; set; }
		void InitializeCollector(IViewer viewer, ViewerName viewerName);
		void SetRelatedViewer();
		IMVCController<TEntity> GetActiveController();
		IMVCController<TEntity> GetActiveController(ViewerName viewerName);
	}
}
