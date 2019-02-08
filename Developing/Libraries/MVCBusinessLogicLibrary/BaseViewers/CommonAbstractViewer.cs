using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;
using MVCBusinessLogicLibrary.Controller;
using MVCBusinessLogicLibrary.Viewers;

namespace MVCBusinessLogicLibrary.BaseViewers
{
	public interface IAbstractViewer
	{
		List<IViewer> RelatedViewers { get; set; }
		string GridXML { get; }

		void SetupGridControl(IList sourceList, string sourceFieldName, string destinationValueMember,
			string sourceDisplayMember = "Name_P");
	}

	public abstract class CommonAbstractViewer<TEntity> : UserControl, IAbstractViewer, IViewer where TEntity :DBCommon, IDBCommon, new()
	{
		public virtual IMVCController<TEntity> ViewerCollector { get; set; }

		#region Implementation of IViewer

		public object ID { get; set; }
		public abstract object ViewerID { get; }
		public object UserID { get; set; }
		public object EditingDate { get; set; }
		public object IsOnDUty { get; set; }
		public DB_CommonTransactionType CommonTransactionType { get; set; }
		public abstract string HeaderTitle { get; }

		#endregion

		#region Implementation of IAbstractViewer

		public abstract List<IViewer> RelatedViewers { get; set; }

		public virtual void ClearControls()
		{
			
		}

		public virtual void FillControls()
		{
			
		}

		public abstract string GridXML { get; }

		public abstract void SetupGridControl(IList sourceList, string sourceFieldName, string destinationValueMember,
			string sourceDisplayMember);
		
		#endregion

		public abstract void SetRelatedViewer(IMVCController<TEntity> relatedController);
		public abstract List<IViewer> GetRelatedViewers();
	}

	public abstract class CommonAbstractSearchViewer<TEntity> : CommonAbstractViewer<TEntity>
		where TEntity : DBCommon, IDBCommon, new()
	{
		public CommonAbstractSearchViewer()
		{

		}

		public override List<IViewer> RelatedViewers { get; set; }

		public virtual BaseSettingsSearchContatiner<TEntity> BaseSearchContainerObject { get; set; }

		public virtual object[] CollectSearchCriteria()
		{
			return null;
		}

		public override void SetupGridControl(IList sourceList, string sourceFieldName, string destinationValueMember,
			string sourceDisplayMember = "Name_P")
		{
			
		}

		public abstract override string GridXML { get; }

		public override void ClearControls()
		{

		}

		public override void FillControls()
		{

		}

		public override IMVCController<TEntity> ViewerCollector { get; set; }

		public override void SetRelatedViewer(IMVCController<TEntity> relatedController)
		{

		}

		public override List<IViewer> GetRelatedViewers()
		{
			return RelatedViewers;
		}
	}

	public abstract class CommonAbstractEditorViewer<TEntity> : CommonAbstractViewer<TEntity>
		where TEntity : DBCommon, IDBCommon, new()
	{
		public CommonAbstractEditorViewer()
		{

		}

		public override List<IViewer> RelatedViewers { get; set; }

		public override void SetupGridControl(IList sourceList, string sourceFieldName, string destinationValueMember,
			string sourceDisplayMember = "Name_P")
		{
		}

		public virtual void CancelEditing()
		{
			
		}

		public override string GridXML
		{
			get { throw new System.NotImplementedException(); }
		}

		public override void ClearControls()
		{

		}

		public override void FillControls()
		{

		}

		public override void SetRelatedViewer(IMVCController<TEntity> relatedController)
		{
			if (RelatedViewers != null && RelatedViewers.Count > 0)
				foreach (IViewer relatedViewer in RelatedViewers)
				{
					relatedViewer.ClearControls();
					relatedViewer.FillControls();
				}
		}

		public override List<IViewer> GetRelatedViewers()
		{
			SetRelatedViewer(ViewerCollector);
			return RelatedViewers;
		}
	}
}
