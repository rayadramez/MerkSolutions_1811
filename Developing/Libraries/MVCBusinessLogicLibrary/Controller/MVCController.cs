using System.Collections.Generic;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;
using MVCBusinessLogicLibrary.MVCDataCollectors;
using MVCBusinessLogicLibrary.MVCFactories;
using MVCBusinessLogicLibrary.Viewers;

namespace MVCBusinessLogicLibrary.Controller
{
	public class MVCController<TEntity> : IMVCController<TEntity> where TEntity : DBCommon, IDBCommon, new()
	{
		public MVCController<TEntity> ActiveController { get; set; }

		public MVCController(IViewer viewer, ViewerName viewerName)
		{
			InitializeCollector(viewer, viewerName);
		}

		#region Implementation of IMVCController

		public AbstractDataCollector<TEntity> ParentActiveCollector { get; set; }
		public IDBCommon ActiveDBItem { get; set; }
		public IDBCommon ParentActiveDBItem { get; set; }
		public IViewer ActiveViewer { get; set; }

		public AbstractDataCollector<TEntity> ActiveCollector { get; set; }
		public IMVCController<TEntity> ParentController { get; set; }
		public ViewerName ActiveViewerName { get; set; }
		public List<IMVCController<TEntity>> List_ChildrenControllers { get; set; }
		public void InitializeCollector(IViewer viewer, ViewerName viewerName)
		{
			if (ActiveCollector == null)
				ActiveCollector = (AbstractDataCollector<TEntity>)DataCollectorFactory.GetDataCollectorFactory<TEntity>(viewerName);

			if (ActiveCollector != null)
				ActiveCollector.ActiveViewer = viewer;

			ActiveViewerName = viewerName;

			Collect(ActiveCollector);
		}

		public void SetRelatedViewer()
		{

		}

		public bool Collect(AbstractDataCollector<TEntity> collector)
		{
			if (collector == null)
				return false;

			return collector.Collect(collector);
		}

		public IEnumerable<TEntity> GetItemsList()
		{
			if (ActiveCollector == null)
				return null;

			return ActiveCollector.GetItemsList();
		}

		public string MessageToView { get; set; }

		public IMVCController<TEntity> GetActiveController()
		{
			return this;
		}

		public IMVCController<TEntity> GetActiveController(ViewerName viewerName)
		{
			if (List_ChildrenControllers != null)
				return List_ChildrenControllers.Find(item => item.ActiveViewerName.Equals(viewerName));
			return null;
		}

		#endregion

		#region Implementation of IViewer

		public object ID { get; set; }
		public object ViewerID { get; private set; }
		public object UserID { get; set; }
		public object EditingDate { get; set; }
		public object IsOnDUty { get; set; }
		public DB_CommonTransactionType CommonTransactionType { get; set; }
		public string HeaderTitle { get; private set; }
		public string GridXML { get; private set; }
		public List<IViewer> RelatedViewers { get; set; }
		public void ClearControls()
		{
			if (ActiveCollector != null)
				//ActiveCollector.ClearControls();
				ActiveCollector.ActiveViewer.ClearControls();
		}

		public void FillControls()
		{
			throw new System.NotImplementedException();
		}

		#endregion

		#region Implementation of IMVCControllerFactory

		public ViewerName GetViewerName()
		{
			throw new System.NotImplementedException();
		}

		#endregion

		#region Implementation of INewViewerAction

		public bool BeforeCreatingNew()
		{
			if (ActiveCollector != null)
				return ActiveCollector.BeforeCreatingNew();
			return false;
		}

		public bool CreateNew()
		{
			if (ActiveCollector != null)
				return ActiveCollector.CreateNew();
			return false;
		}

		public bool AfterCreateNew()
		{
			if (ActiveCollector != null)
			{
				if (ActiveCollector.AfterCreateNew())
				{
					ActiveCollector.ActiveViewer.ClearControls();
					ActiveCollector.ActiveViewer.FillControls();
				}
			}

			return false;
		}

		#endregion

		#region Implementation of ISaveViewerAction

		public bool CheckIfActiveDBItemExists()
		{
			if (ActiveCollector != null)
				return ActiveCollector.CheckIfActiveDBItemExists();

			return false;
		}

		public bool ValidateBeforeSave(ref string message)
		{
			if (ActiveCollector != null)
			{
				if (ActiveCollector.ValidateBeforeSave(ref message))
					return true;
				MessageToView = ActiveCollector.MessageToView;
			}

			return false;
		}

		public bool BeforeSave()
		{
			if (ActiveCollector != null)
				return ActiveCollector.BeforeSave();

			return false;
		}

		public bool SaveChanges(DB_CommonTransactionType commonTransactionType)
		{
			if (ActiveCollector != null)
			{
				ActiveCollector.CommonTransactionType = commonTransactionType;
				if (ActiveCollector.Collect(ActiveCollector) && ActiveCollector.SaveChanges(commonTransactionType))
					return true;
			}

			return false;
		}

		public bool AddToParent()
		{
			if (ActiveCollector != null)
				ActiveCollector.AddToParent();

			return false;
		}

		public bool AfterSave()
		{
			if (ActiveCollector != null)
				if (ActiveCollector.AfterSave())
				{
					ActiveCollector.ActiveDBItem = null;
					return true;
				}

			return false;
		}

		#endregion

		#region Implementation of IEditViewerAction

		public void BeforeEdit(IDBCommon entity)
		{
			if (ActiveCollector == null)
				return;

			ActiveCollector.BeforeEdit(entity);
		}

		public void Edit(IDBCommon entity)
		{
			if (ActiveCollector == null)
				return;

			ActiveCollector.ActiveDBItem = entity;
			ActiveCollector.Edit(entity);
		}

		public void AfterEdit(IDBCommon entity)
		{
			if (ActiveCollector == null)
				return;

			ActiveCollector.BeforeEdit(entity);
		}

		#endregion

		#region Implementation of IDeleteViewerAction

		public bool ValidateBeforeDelete()
		{
			throw new System.NotImplementedException();
		}

		public bool BeforeDelete(IDBCommon entity)
		{
			throw new System.NotImplementedException();
		}

		public bool Delete(IDBCommon entity)
		{
			if (ActiveCollector == null)
				return false;
			return ActiveCollector.Delete(entity);
		}

		public bool DeleteFromParent()
		{
			throw new System.NotImplementedException();
		}

		public bool AfterDelete(IDBCommon entity)
		{
			throw new System.NotImplementedException();
		}

		#endregion

		#region Implementation of ICloseViewerAction

		public bool BeforeClosing()
		{
			if (ActiveCollector != null)
				return ActiveCollector.BeforeClosing();
			return false;
		}

		public bool Close()
		{
			if (ActiveCollector != null)
				return ActiveCollector.Close();
			return false;
		}

		public bool AfterClosing()
		{
			if (ActiveCollector != null)
				return ActiveCollector.AfterClosing();
			return false;
		}

		#endregion

		#region Implementation of IPrintViewerAction

		public bool BeforePrint()
		{
			throw new System.NotImplementedException();
		}

		public bool Print()
		{
			throw new System.NotImplementedException();
		}

		public bool AfterPrint()
		{
			throw new System.NotImplementedException();
		}

		#endregion

		#region Implementation of ISearchAction

		public object[] CollectSearchCriteria()
		{
			if (ActiveCollector == null)
				return null;
			return ActiveCollector.CollectSearchCriteria();
		}

		#endregion
	}
}
