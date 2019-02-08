using System.Collections.Generic;
using System.Windows.Forms;
using CommonControlLibrary;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;
using MVCBusinessLogicLibrary.Controller;
using MVCBusinessLogicLibrary.MVCDataCollectors;
using MVCBusinessLogicLibrary.MVCFactories;
using MVCBusinessLogicLibrary.Viewers;

namespace MVCBusinessLogicLibrary.BaseViewers
{
	public class BaseController<TEntity> : IMVCDataCollector<TEntity> where TEntity : DBCommon, IDBCommon, new()
	{
		public static IViewer AbstractViewer { get; set; }
		public static ViewerName ViewerName { get; set; }
		public static TEntity ActiveDBEntity { get; set; }
		public static MVCController<TEntity> MVCEditorController { get; set; }
		public static MVCController<TEntity> MVCSearchController { get; set; }
		public static BaseContainer<TEntity> _baseContainer;
		public static BaseEditorViewerContainer<TEntity> BasicEditorViewerContainer;
		public static BaseSettingsEditorContainer<TEntity> _baseEditorContainer;
		public static BaseSettingsSearchContatiner<TEntity> _baseSearchContainer;
		public static BaseController<TEntity> BaseControllerObject { get; set; }
		private static PopupBaseForm _popupForm;
		public static DB_CommonTransactionType CommontransactionType { get; set; }
		public static Control ParentControl { get; set; }
		public static IViewer EditorViewer { get; set; }
		public static IViewer SearchViewer { get; set; }
		public static string HeaderTitle { get; set; }
		public string _message;

		public static void ShowControl<TEditorViewer, TSearchViewer>(ref TEditorViewer editorViewerToShow,
			ref TSearchViewer searchViewerToShow, Control parentControlToAttach, EditorContainerType editorContainerType, ViewerName viewerName,
			DB_CommonTransactionType commontransactionType,
			string headerTitle, AbstractViewerType viewerType, bool showAsPopup, bool showInFullScreen = false, bool isSearchPanelexpanded = false)
			where TEditorViewer : Control, new()
			where TSearchViewer : Control, new()
		{
			//1--- Initialize the BaseContainer
			//2--- Initialize the BaseSettingsEditorContainer / BaseSearchContainer
			//3--- Attach the BaseSettingsEditorContainer to BaseContainer
			//4--- Initialize the ViewerToShow
			//5--- Attach the ViewerToShow to BaseSettingsEditorContainer to Top Panel
			//6--- If the ViewerToShow has children Viewers, then >>
			//7--- If Yes :: Initialize Each Child in the ViewerToShow Children
			//8---			 Initialize The CommonTabControl
			//9---			 Create a Tab page for each viewerToShow child
			//10--			 Attach each Child to each suitable Tab page
			//11-- Attach the CommonTabControl to the BaseSettingsEditorContainer Bottom Panel
			//12-- Initialize the ViewerToShow MVCController
			//13-- Initialize the MVCController for Each Child in the ViewerToShow Children

			ParentControl = parentControlToAttach;
			ViewerName = viewerName;
			HeaderTitle = headerTitle;
			CommontransactionType = commontransactionType;

			if (showAsPopup)
			{
				_popupForm = new PopupBaseForm();
				_popupForm.InitializePopupBaseForm(FormWindowState.Maximized, false, headerTitle, FormBorderStyle.None);
				CommonViewsActions.ShowUserControl(ref _baseContainer, _popupForm, true);
			}
			else
				CommonViewsActions.ShowUserControl(ref _baseContainer, parentControlToAttach, true);

			if (_baseContainer == null)
				return;

			if (!_baseContainer.IsBaseControllerInitialized)
			{
				BaseControllerObject = new BaseController<TEntity>();
				_baseContainer.InitializeBaseViewerController(BaseControllerObject);
			}

			_baseContainer.InitializeBaseContainer(ViewerName, viewerType, headerTitle, false, true, true, true);

			if (editorViewerToShow == null || editorViewerToShow.IsDisposed)
				editorViewerToShow = new TEditorViewer();
			if (searchViewerToShow == null || editorViewerToShow.IsDisposed)
				searchViewerToShow = new TSearchViewer();

			_baseContainer._editorViewerToShow = editorViewerToShow;

			EditorViewer = (IViewer)editorViewerToShow;
			SearchViewer = (IViewer)searchViewerToShow;

			if (EditorViewer != null)
			{
				EditorViewer.ClearControls();
				EditorViewer.FillControls();
			}

			if (SearchViewer != null)
			{
				SearchViewer.ClearControls();
				SearchViewer.FillControls();
			}

			switch (viewerType)
			{
				case AbstractViewerType.EditorViewer:
					MVCEditorController = GenerateEditorMVCController(EditorViewer, null, editorContainerType, viewerName, viewerType);
					break;
				case AbstractViewerType.SearchViewer:
					MVCSearchController = GenerateEditorMVCController(SearchViewer, null, editorContainerType, viewerName, viewerType);
					break;
			}

			switch (viewerType)
			{
				case AbstractViewerType.EditorViewer:
					if (_baseEditorContainer == null)
						return;
					_baseEditorContainer._editorViewer = (Control)EditorViewer;
					_baseEditorContainer.CommonTransactionType = CommontransactionType;
					if (BaseControllerObject != null)
						BaseControllerObject.PassMVCController(editorContainerType, viewerType);
					_baseEditorContainer.InitializeBaseEditorContainer(true, false, false);
					break;
				case AbstractViewerType.SearchViewer:
					if (_baseSearchContainer == null)
						return;
					_baseSearchContainer._searchViewer = (Control)SearchViewer;
					if (BaseControllerObject != null)
						BaseControllerObject.PassMVCController(editorContainerType, viewerType);
					_baseSearchContainer.InitializeBaseSearchContainer(isSearchPanelexpanded);
					_baseSearchContainer.LoadGrid();
					break;
			}

			if (showAsPopup && _popupForm != null)
				_popupForm.Show();
		}

		public static void ShowEditorControl<TEditorViewer>(ref TEditorViewer editorViewerToShow,
			Control parentControlToAttach, object objectToPassToViewer, IDBCommon dbObjectToLoad, EditorContainerType editorContainerType,
			ViewerName viewerName,
			DB_CommonTransactionType commontransactionType,
			string headerTitle, bool showAsPopup, bool showTopMost = false,
			bool showInFullScreen = true)
			where TEditorViewer : Control, new()
		{
			ParentControl = parentControlToAttach;
			ViewerName = viewerName;
			HeaderTitle = headerTitle;
			CommontransactionType = commontransactionType;

			switch (editorContainerType)
			{
				case EditorContainerType.Settings:
					if (showAsPopup)
					{
						_popupForm = new PopupBaseForm();
						_popupForm.InitializePopupBaseForm(FormWindowState.Maximized, false, headerTitle);
						CommonViewsActions.ShowUserControl(ref _baseContainer, _popupForm, true);
					}
					else
						CommonViewsActions.ShowUserControl(ref _baseContainer, parentControlToAttach, true);

					if (_baseContainer == null)
						return;

					if (!_baseContainer.IsBaseControllerInitialized)
					{
						BaseControllerObject = new BaseController<TEntity>();
						_baseContainer.InitializeBaseViewerController(BaseControllerObject);
					}
					_baseContainer.InitializeBaseContainer(ViewerName, AbstractViewerType.EditorViewer, headerTitle, false, true, true,
						true);
					break;
				case EditorContainerType.Regular:
					if (showAsPopup)
					{
						_popupForm = new PopupBaseForm();
						if (showInFullScreen)
							_popupForm.InitializePopupBaseForm(FormWindowState.Maximized, false, headerTitle, FormBorderStyle.None);
						else
							_popupForm.InitializePopupBaseForm(FormWindowState.Normal, false, headerTitle, FormBorderStyle.None);
						CommonViewsActions.ShowUserControl(ref BasicEditorViewerContainer, _popupForm, true);
					}

					if (BasicEditorViewerContainer == null)
						return;

					BasicEditorViewerContainer.SetHeader(headerTitle, commontransactionType);
					if (!BasicEditorViewerContainer.IsBaseControllerInitialized)
						BaseControllerObject = new BaseController<TEntity>();
					BasicEditorViewerContainer.InitializeBaseEditorContainer(ViewerName, HeaderTitle);
					BaseControllerObject.PassBaseController(BasicEditorViewerContainer);
					break;
			}

			if (editorViewerToShow == null || editorViewerToShow.IsDisposed)
				editorViewerToShow = new TEditorViewer();

			EditorViewer = (IViewer)editorViewerToShow;

			if (EditorViewer != null)
			{
				if (objectToPassToViewer != null && EditorViewer is IViewerDataRelated)
					((IViewerDataRelated)EditorViewer).ViewerDataRelated = objectToPassToViewer;

				EditorViewer.ClearControls();
				EditorViewer.FillControls();
			}

			MVCEditorController = GenerateEditorMVCController(EditorViewer, dbObjectToLoad, editorContainerType, viewerName,
				AbstractViewerType.EditorViewer);

			switch (editorContainerType)
			{
				case EditorContainerType.Settings:
					if (_baseEditorContainer == null)
						return;
					_baseEditorContainer._editorViewer = (Control)EditorViewer;
					_baseEditorContainer.CommonTransactionType = CommontransactionType;
					break;
				case EditorContainerType.Regular:
					BaseEditorViewerContainer<TEntity>._editorViewer = (Control)EditorViewer;
					BasicEditorViewerContainer.CommonTransactionType = CommontransactionType;
					break;
			}

			if (BaseControllerObject != null)
				BaseControllerObject.PassMVCController(editorContainerType, AbstractViewerType.EditorViewer);

			switch (editorContainerType)
			{
				case EditorContainerType.Settings:
					_baseEditorContainer.InitializeBaseEditorContainer(true, false, false);
					break;
				case EditorContainerType.Regular:
					BasicEditorViewerContainer.InitializeBaseEditorContainer();
					break;
			}

			if (showAsPopup && _popupForm != null)
			{
				_popupForm.Initialize((Control)EditorViewer, BasicEditorViewerContainer);
				_popupForm.TopMost = showTopMost;
				_popupForm.BringToFront();
				_popupForm.ShowDialog();
			}
		}

		public static void ShowSearchControl<TSearchViewer>(ref TSearchViewer searchViewerToShow,
			Control parentControlToAttach, ViewerName viewerName, DB_CommonTransactionType commontransactionType,
			string headerTitle, bool showAsPopup, bool isSearchPanelexpanded = false)
			where TSearchViewer : Control, new()
		{
			ParentControl = parentControlToAttach;
			ViewerName = viewerName;
			HeaderTitle = headerTitle;
			CommontransactionType = commontransactionType;

			if (showAsPopup)
			{
				_popupForm = new PopupBaseForm();
				_popupForm.InitializePopupBaseForm(FormWindowState.Maximized, false, headerTitle, FormBorderStyle.None);
				CommonViewsActions.ShowUserControl(ref _baseContainer, _popupForm, true);
			}
			else
				CommonViewsActions.ShowUserControl(ref _baseContainer, parentControlToAttach, true);

			if (_baseContainer == null)
				return;

			if (!_baseContainer.IsBaseControllerInitialized)
			{
				BaseControllerObject = new BaseController<TEntity>();
				_baseContainer.InitializeBaseViewerController(BaseControllerObject);
			}

			_baseContainer.InitializeBaseContainer(ViewerName, AbstractViewerType.SearchViewer, headerTitle, true, false, true,
				true);

			if (searchViewerToShow == null || searchViewerToShow.IsDisposed)
				searchViewerToShow = new TSearchViewer();

			SearchViewer = (IViewer) searchViewerToShow;

			if (SearchViewer != null)
			{
				SearchViewer.ClearControls();
				SearchViewer.FillControls();
			}

			MVCSearchController = GenerateSearchMVCController(SearchViewer, viewerName, AbstractViewerType.SearchViewer);

			if (_baseSearchContainer == null)
				return;
			_baseSearchContainer._searchViewer = (Control) SearchViewer;
			if (BaseControllerObject != null)
				BaseControllerObject.PassMVCController(EditorContainerType.Settings, AbstractViewerType.SearchViewer);
			_baseSearchContainer.InitializeBaseSearchContainer(isSearchPanelexpanded);
			_baseSearchContainer.LoadGrid();

			if (showAsPopup && _popupForm != null)
				_popupForm.Show();
		}

		private static MVCController<TEntity> GenerateSearchMVCController<TViewer>(TViewer viewerToShow, ViewerName viewerName,
			AbstractViewerType viewerType) where TViewer : IViewer
		{
			MVCSearchController =
				(MVCController<TEntity>)MVCControllerFactory.GetControllerFactory<TEntity>(viewerName, viewerToShow);

			return MVCSearchController;
		}

		private static MVCController<TEntity> GenerateEditorMVCController<TViewer>(TViewer viewerToShow, IDBCommon dbObjectToLoad,
			EditorContainerType editorContainerType, ViewerName viewerName,
			AbstractViewerType viewerType) where TViewer : IViewer
		{
			MVCEditorController =
				(MVCController<TEntity>)MVCControllerFactory.GetControllerFactory<TEntity>(viewerName, viewerToShow);

			switch (editorContainerType)
			{
				case EditorContainerType.Settings:
					if (MVCEditorController != null && BaseControllerObject != null && viewerType != AbstractViewerType.SearchViewer &&
						(_baseEditorContainer != null || _baseSearchContainer != null))
					{
						MVCEditorController.BeforeCreatingNew();
						MVCEditorController.CreateNew();
						MVCEditorController.AfterCreateNew();
					}

					break;
				case EditorContainerType.Regular:
					if (MVCEditorController != null)
					{
						if (dbObjectToLoad != null)
						{
							MVCEditorController.BeforeEdit(dbObjectToLoad);
							MVCEditorController.Edit(dbObjectToLoad);
							MVCEditorController.AfterEdit(dbObjectToLoad);
						}
						else
						{
							MVCEditorController.BeforeCreatingNew();
							MVCEditorController.CreateNew();
							if (MVCEditorController.ActiveCollector.ActiveDBItem is TEntity)
								ActiveDBEntity = (TEntity)MVCEditorController.ActiveCollector.ActiveDBItem;
							MVCEditorController.AfterCreateNew();
						}
					}

					break;
			}

			return MVCEditorController;
		}

		public static void EnableRelatedViewersButton(BaseContainer<TEntity> baseContainer, bool isEnabled)
		{
			if (baseContainer != null)
				baseContainer.EnableRelatedVeiwersButton = isEnabled;
		}

		public void EnableRelatedViewersButton(bool isEnabled)
		{
			if (_baseContainer != null)
				_baseContainer.EnableRelatedVeiwersButton = isEnabled;
		}

		public BaseContainer<TEntity> InitiaizeBaseContainer(Control controlToAttach, string headerTitle,
			bool showTopControls, bool enableShowRelatedButton, DockStyle dockStyle = DockStyle.Fill)
		{
			BaseContainer<TEntity> baseContainer = new BaseContainer<TEntity>();
			baseContainer.Dock = dockStyle;
			baseContainer.EnableRelatedVeiwersButton = enableShowRelatedButton;
			baseContainer.InitiaizeBaseContainer(controlToAttach, headerTitle, showTopControls);
			return baseContainer;
		}

		public void PassBaseController(BaseSettingsEditorContainer<TEntity> baseEditorContainer)
		{
			if (baseEditorContainer != null)
				baseEditorContainer.BaseControllerObject = this;
		}

		public void PassBaseController(BaseEditorViewerContainer<TEntity> baseEditorContainer)
		{
			if (baseEditorContainer != null)
				baseEditorContainer.BaseControllerObject = this;
		}

		public void PassBaseController(BaseSettingsSearchContatiner<TEntity> baseSearchContainer)
		{
			if (baseSearchContainer != null)
				baseSearchContainer.BaseControllerObject = this;
		}

		public void PassMVCController(EditorContainerType editorContainerType, AbstractViewerType viewerType)
		{
			switch (viewerType)
			{
				case AbstractViewerType.EditorViewer:
					BaseEditorViewerContainer<TEntity>.MVCController = MVCEditorController;
					break;
				case AbstractViewerType.SearchViewer:
					BaseSettingsSearchContatiner<TEntity>.BaseMvcController = MVCSearchController;
					break;
			}
		}

		public void ShowRelatedViewers(bool show)
		{
			if (_baseEditorContainer != null)
				_baseEditorContainer.ShowRelatedViewers(show);
			//TODO :: a messagbox to show that there is no relatedvewiers
		}

		#region Implementation of INewViewerAction

		public bool BeforeCreatingNew()
		{
			throw new System.NotImplementedException();
		}

		public bool CreateNew()
		{
			throw new System.NotImplementedException();
		}

		public bool AfterCreateNew()
		{
			throw new System.NotImplementedException();
		}

		#endregion

		#region Implementation of ISaveViewerAction

		public bool CheckIfActiveDBItemExists()
		{
			if (MVCEditorController != null)
				return MVCEditorController.CheckIfActiveDBItemExists();
			return false;
		}

		public bool ValidateBeforeSave(ref string message)
		{
			if (MVCEditorController != null)
				return MVCEditorController.ValidateBeforeSave(ref message);
			return false;
		}

		public bool BeforeSave()
		{
			if (MVCEditorController != null)
				return MVCEditorController.BeforeSave();
			return false;
		}

		public bool SaveChanges(DB_CommonTransactionType commonTransactionType)
		{
			if (MVCEditorController == null)
				return false;

			if (ValidateBeforeSave(ref _message) && !CheckIfActiveDBItemExists() && BeforeSave() &&
			    MVCEditorController.SaveChanges(commonTransactionType))
			{
				if (MVCEditorController.ActiveCollector.ActiveDBItem is TEntity)
					ActiveDBEntity = (TEntity) MVCEditorController.ActiveCollector.ActiveDBItem;
				if (AfterSave())
					if (_baseSearchContainer != null)
						_baseSearchContainer.LoadGrid();
				return Close();
			}

			MessageToView = MVCEditorController.MessageToView;

			return false;
		}

		public bool AddToParent()
		{
			throw new System.NotImplementedException();
		}

		public bool AfterSave()
		{
			if (MVCEditorController != null)
				return MVCEditorController.AfterSave();
			return false;
		}

		public void ShowNotificationAfterSaving(ref string message)
		{
			throw new System.NotImplementedException();
		}

		#endregion

		#region Implementation of IEditViewerAction

		public void BeforeEdit(IDBCommon entity)
		{
			throw new System.NotImplementedException();
		}

		public void Edit(IDBCommon entity)
		{
			Control editorViewerToShow = (Control)EditorViewer;
			Control searchViewerToShow = (Control)SearchViewer;

			ShowControl(ref editorViewerToShow, ref searchViewerToShow, ParentControl, EditorContainerType.Settings, ViewerName,
				DB_CommonTransactionType.UpdateExisting, "غرف الإقامة", AbstractViewerType.EditorViewer, true);

			MVCEditorController.BeforeEdit(entity);
			MVCEditorController.Edit(entity);
			MVCEditorController.AfterEdit(entity);
		}

		public void AfterEdit(IDBCommon entity)
		{
			throw new System.NotImplementedException();
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
			return MVCEditorController.BeforeDelete(entity) && MVCEditorController.Delete(entity) &&
				   MVCEditorController.AfterDelete(entity);
		}

		public bool DeleteFromParent()
		{
			return true;
		}

		public bool AfterDelete(IDBCommon entity)
		{
			throw new System.NotImplementedException();
		}

		public void ShowNotificationAfterDeleting()
		{
			throw new System.NotImplementedException();
		}

		#endregion

		#region Implementation of ICloseViewerAction

		public bool BeforeClosing()
		{
			if (MVCEditorController != null)
				return MVCEditorController.BeforeClosing();
			return false;
		}

		public bool Close()
		{
			return BeforeClosing() && MVCEditorController.Close() && AfterClosing();
		}

		public bool AfterClosing()
		{
			if (MVCEditorController != null)
				return MVCEditorController.AfterClosing();
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

		#region Implementation of IMVCDataCollector<TEntity>

		public AbstractDataCollector<TEntity> ActiveCollector { get; set; }
		public AbstractDataCollector<TEntity> ParentActiveCollector { get; set; }
		public IDBCommon ActiveDBItem { get; set; }
		public IDBCommon ParentActiveDBItem { get; set; }
		public IViewer ActiveViewer { get; set; }
		public bool Collect(AbstractDataCollector<TEntity> collector)
		{
			return true;
		}

		public IEnumerable<TEntity> GetItemsList()
		{
			if (MVCEditorController != null)
				return MVCEditorController.GetItemsList();

			return null;
		}

		public string MessageToView { get; set; }

		#endregion

		#region Implementation of ISearchAction

		public object[] CollectSearchCriteria()
		{
			throw new System.NotImplementedException();
		}

		#endregion
	}
}
