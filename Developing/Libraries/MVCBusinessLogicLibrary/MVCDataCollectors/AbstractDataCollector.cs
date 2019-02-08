using System.Collections.Generic;
using System.Linq;
using ApplicationConfiguration;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;
using MVCBusinessLogicLibrary.Controller;
using MVCBusinessLogicLibrary.Viewers;

namespace MVCBusinessLogicLibrary.MVCDataCollectors
{
	public abstract class AbstractDataCollector<TEntity> : IMVCDataCollector<TEntity>, IViewer where TEntity : DBCommon, IDBCommon, new()
	{
		#region Implementation of IMVCDataCollector

		public IMVCController<TEntity> ActiveController { get; set; }
		public abstract AbstractDataCollector<TEntity> ActiveCollector { get; set; }
		public abstract AbstractDataCollector<TEntity> ParentActiveCollector { get; set; }
		public IDBCommon ActiveDBItem { get; set; }
		public IDBCommon ParentActiveDBItem { get; set; }
		public IViewer ActiveViewer { get; set; }
		public abstract bool Collect(AbstractDataCollector<TEntity> collector);
		public virtual IEnumerable<TEntity> GetItemsList()
		{
			return DBCommon.GetItemsList<TEntity>().ToList();
		}

		public virtual string MessageToView { get; set; }

		#endregion

		#region Implementation of IViewer

		public abstract object ID { get; set; }
		public abstract object ViewerID { get; }

		public virtual object UserID
		{
			get
			{
				if (ApplicationStaticConfiguration.ActiveLoginUser != null)
					return ApplicationStaticConfiguration.ActiveLoginUser.Person_CU_ID;
				return null;
			}
		}

		public abstract object EditingDate { get; }
		public abstract object IsOnDUty { get; set; }
		public abstract DB_CommonTransactionType CommonTransactionType { get; set; }
		public abstract string HeaderTitle { get; }
		public abstract string GridXML { get; }
		public abstract List<IViewer> RelatedViewers { get; set; }
		public virtual void ClearControls() {}
		public virtual void FillControls() {}

		#endregion

		#region Implementation of INewViewerAction

		public virtual bool BeforeCreatingNew()
		{
			return true;
		}

		public virtual bool CreateNew()
		{
			return true;
		}

		public virtual bool AfterCreateNew()
		{
			return true;
		}

		#endregion

		#region Implementation of ISaveViewerAction

		public virtual bool CheckIfActiveDBItemExists()
		{
			return false;
		}

		public virtual bool ValidateBeforeSave(ref string message)
		{
			return true;
		}

		public virtual bool BeforeSave()
		{
			return true;
		}

		public virtual bool SaveChanges(DB_CommonTransactionType commonTransactionType)
		{
			return false;
		}

		public virtual bool AddToParent()
		{
			ClearControls();
			ActiveDBItem = null;
			CreateNew();

			return true;
		}

		public virtual bool DeleteFromParent()
		{
			ClearControls();
			ActiveDBItem = null;
			return true;
		}

		public virtual bool AfterSave()
		{
			return true;
		}

		#endregion

		#region Implementation of IEditViewerAction

		public virtual void BeforeEdit(IDBCommon entity)
		{
			
		}

		public virtual void Edit(IDBCommon entity)
		{
			ActiveCollector.ActiveDBItem = entity;
			ActiveCollector.ActiveViewer.CommonTransactionType = DB_CommonTransactionType.UpdateExisting;
			ActiveCollector.ActiveViewer.ClearControls();
			ActiveCollector.ActiveViewer.FillControls();
		}

		public virtual void AfterEdit(IDBCommon entity)
		{
			
		}

		#endregion

		#region Implementation of IDeleteViewerAction

		public virtual bool ValidateBeforeDelete()
		{
			return true;
		}

		public virtual bool BeforeDelete(IDBCommon entity)
		{
			return true;
		}

		public virtual bool Delete(IDBCommon entity)
		{
			return true;
		}

		public virtual bool AfterDelete(IDBCommon entity)
		{
			return true;
		}

		#endregion

		#region Implementation of ICloseViewerAction

		public virtual bool BeforeClosing()
		{
			return true;
		}

		public virtual bool Close()
		{
			return true;
		}

		public virtual bool AfterClosing()
		{
			return true;
		}

		#endregion

		#region Implementation of IPrintViewerAction

		public virtual bool BeforePrint()
		{
			return true;
		}

		public virtual bool Print()
		{
			return true;
		}

		public virtual bool AfterPrint()
		{
			return true;
		}

		#endregion

		#region Implementation of ISearchAction

		public virtual object[] CollectSearchCriteria()
		{
			return null;
		}

		#endregion
	}
}
