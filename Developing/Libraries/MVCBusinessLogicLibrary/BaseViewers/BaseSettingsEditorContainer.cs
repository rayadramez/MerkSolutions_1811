using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using CommonControlLibrary;
using DevExpress.XtraLayout.Utils;
using DevExpress.XtraTab;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;
using MVCBusinessLogicLibrary.Controller;
using MVCBusinessLogicLibrary.MVCFactories;
using MVCBusinessLogicLibrary.Viewers;

namespace MVCBusinessLogicLibrary.BaseViewers
{
	public partial class BaseSettingsEditorContainer<TEntity> : DevExpress.XtraEditors.XtraUserControl, IViewer
		where TEntity : DBCommon, IDBCommon, new()
	{
		public static MVCController<TEntity> ParentMVCController { get; set; }
		public BaseController<TEntity> BaseControllerObject { get; set; }
		public XtraTabControl _tabControl = null;
		public CommonTabControl_UC<TEntity> _CommonTabControl;
		public Control _editorViewer;
		public Control _searchViewer;
		private int pageIndex = 0;

		public BaseSettingsEditorContainer()
		{
			InitializeComponent();

			lytlSubViewerContainer.Visibility = lytSepertator.Visibility = LayoutVisibility.Never;
		}

		public BaseSettingsEditorContainer(BaseController<TEntity> baseController)
		{
			InitializeComponent();
			BaseControllerObject = baseController;

			lytlSubViewerContainer.Visibility = lytSepertator.Visibility = LayoutVisibility.Never;
		}

		public void InitializeBaseEditorContainer(bool showSaveButton, bool showAddToParentButton, bool showSubViewerContainer)
		{
			CommonViewsActions.ShowUserControl(ref _editorViewer, pnlMainViewerContainer);

			if (_editorViewer == null)
				return;

			pnlMainViewerContainer.MinimumSize = new Size(0, _editorViewer.MinimumSize.Height + 3);

			SetControls(showSaveButton, showAddToParentButton, showSubViewerContainer);
		}

		public void ShowRelatedViewers(bool show)
		{
			lytlSubViewerContainer.Visibility =
				lytSepertator.Visibility = show ? LayoutVisibility.Always : LayoutVisibility.Never;

			CommonAbstractEditorViewer<TEntity> editorViewer = (CommonAbstractEditorViewer<TEntity>) _editorViewer;
			if (editorViewer == null)
				return;
			RelatedViewers = editorViewer.GetRelatedViewers();

			if (RelatedViewers != null && RelatedViewers.Count > 0)
			{
				_CommonTabControl = new CommonTabControl_UC<TEntity>(BaseControllerObject);
				BaseControllerObject.EnableRelatedViewersButton(true);

				ParentMVCController.List_ChildrenControllers = new List<IMVCController<TEntity>>();

				foreach (IViewer relatedViewer in RelatedViewers)
				{
					_tabControl = InitiaizeRelatedViewer(relatedViewer, pageIndex);

					#region Initialize the Suitable MVC Controller

					ParentMVCController.List_ChildrenControllers.Add(
						(MVCController<TEntity>)
							MVCControllerFactory.GetControllerFactory<TEntity>((ViewerName) relatedViewer.ViewerID, relatedViewer));

					foreach (var mvcController in ParentMVCController.List_ChildrenControllers)
					{
						mvcController.BeforeCreatingNew();
						mvcController.CreateNew();
						mvcController.AfterCreateNew();
						mvcController.ParentController = ParentMVCController;
						mvcController.ActiveCollector.ParentActiveCollector = ParentMVCController.ActiveCollector;
						mvcController.ActiveCollector.ParentActiveDBItem =
							(IDBCommon) mvcController.ParentController.ActiveCollector.ActiveDBItem;
					}

					#endregion

					pageIndex++;
				}
			}

			if (_tabControl != null)
			{
				pnlSubViewerContainer.Controls.Add(_CommonTabControl);
				_CommonTabControl.Dock = DockStyle.Fill;
				pnlSubViewerContainer.Dock = DockStyle.Fill;
				_tabControl.Dock = DockStyle.Fill;
			}

		}

		public void SetControls(bool showSaveButton, bool showAddToParentButton, bool showSubViewerContainer)
		{
			lytSave.Visibility = showSaveButton ? LayoutVisibility.Always : LayoutVisibility.Never;
			lytLeftAddToParent.Visibility = showAddToParentButton ? LayoutVisibility.Always : LayoutVisibility.Never;
			lytLeftControls.Visibility = lytLeftEmptySpace.Visibility = !showSaveButton && !showAddToParentButton
				? LayoutVisibility.Never
				: LayoutVisibility.Always;
			lytSepertator.Visibility =
				lytlSubViewerContainer.Visibility = showSubViewerContainer ? LayoutVisibility.Always : LayoutVisibility.Never;
		}

		public void InitalizeContainer(Control controlTOAttach, bool showSaveButton, bool showAddToParentButton)
		{
			CommonViewsActions.ShowUserControl(ref controlTOAttach, pnlMainViewerContainer);
			if (controlTOAttach != null)
				pnlMainViewerContainer.MaximumSize = new Size(0, controlTOAttach.MinimumSize.Height + 3);

			lytLeftAddToParent.Visibility = showAddToParentButton ? LayoutVisibility.Always : LayoutVisibility.Never;
			lytSave.Visibility = showSaveButton ? LayoutVisibility.Always : LayoutVisibility.Never;

			lytLeftControls.Visibility = lytLeftEmptySpace.Visibility = !showSaveButton && !showAddToParentButton
				? LayoutVisibility.Never
				: LayoutVisibility.Always;
		}

		public XtraTabControl InitiaizeRelatedViewer(IViewer editorViewer, int pageIndex)
		{
			_tabControl = _CommonTabControl.InitializeCommonTabControl((Control) editorViewer, editorViewer.HeaderTitle,
				pageIndex, true, true,
				(ViewerName) editorViewer.ViewerID);

			return _tabControl;
		}

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

		}

		public void FillControls()
		{

		}

		#endregion

		private void btnSave_Click(object sender, System.EventArgs e)
		{
			if (BaseControllerObject == null)
				return;

			BaseControllerObject.SaveChanges(CommonTransactionType);
		}

		private void btnAddToParent_Click(object sender, System.EventArgs e)
		{
			if (ParentMVCController != null && ParentMVCController.List_ChildrenControllers != null &&
			    ParentMVCController.List_ChildrenControllers.Count > 0)
			{
				MVCController<TEntity> controller =
					(MVCController<TEntity>) ParentMVCController.GetActiveController(ViewerName.InPatientRoomViewer);
				if (controller == null)
					return;
				controller.AddToParent();
			}
		}

		public bool Close()
		{
			if (ParentForm != null)
			{
				ParentForm.Visible = false;
				return true;
			}

			return false;
		}

		private void BaseSettingsEditorContainer_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.F2)
			{
				
			}
		}
	}
}
