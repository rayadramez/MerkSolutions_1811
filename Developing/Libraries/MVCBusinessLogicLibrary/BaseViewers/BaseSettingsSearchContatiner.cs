using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ApplicationConfiguration;
using CommonControlLibrary;
using DevExpress.XtraEditors.Repository;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;
using MVCBusinessLogicLibrary.Controller;
using MVCBusinessLogicLibrary.Viewers;

namespace MVCBusinessLogicLibrary.BaseViewers
{
	public partial class BaseSettingsSearchContatiner<TEntity> : DevExpress.XtraEditors.XtraUserControl, IViewer
		where TEntity : DBCommon, IDBCommon, new()
	{
		public static MVCController<TEntity> BaseMvcController { get; set; }
		public BaseController<TEntity> BaseControllerObject { get; set; }
		public CommonTabControl_UC<TEntity> _CommonTabControl;
		public Control _searchViewer;
		public Control _editorViewer;
		private int pageIndex = 0;

		public BaseSettingsSearchContatiner()
		{
			InitializeComponent();
			CommonViewsActions.LoadXMLFromString(layoutControl1, Resources.LocalizedRes.lyt_BaseSettingsSearchContainer);
			CommonViewsActions.SetupSyle(this);
		}

		public BaseSettingsSearchContatiner(BaseController<TEntity> baseController)
		{
			InitializeComponent();
			CommonViewsActions.LoadXMLFromString(layoutControl1, Resources.LocalizedRes.lyt_BaseSettingsSearchContainer);
			CommonViewsActions.SetupSyle(this);
			BaseControllerObject = baseController;
		}

		public void InitializeBaseSearchContainer(bool isPanelExpanded = true)
		{
			CommonViewsActions.ShowUserControl(ref _searchViewer, pnlMainViewerContainer);
			if (_searchViewer == null)
				return;

			layoutControlGroup2.Expanded = isPanelExpanded;
			pnlMainViewerContainer.MinimumSize = new Size(0, _searchViewer.MinimumSize.Height + 3);

			CommonViewsActions.SetupGridControl(grdControlItems, ((CommonAbstractSearchViewer<TEntity>)_searchViewer).GridXML,
				false);

			if (!string.IsNullOrEmpty(ApplicationStaticConfiguration.SkinName) &&
				!string.IsNullOrWhiteSpace(ApplicationStaticConfiguration.SkinName))
				if (ApplicationStaticConfiguration.SkinName.Equals("McSkin"))
				{
					BackColor = Color.White;
					if (_searchViewer != null)
						_searchViewer.BackColor = Color.WhiteSmoke;
					if (_editorViewer != null)
						_editorViewer.BackColor = Color.WhiteSmoke;
				}

		}

		public void SetupGridControl(IList sourceList, string sourceFieldName, string destinationValueMember,
			string sourceDisplayMember = "Name_P")
		{
			CommonViewsActions.CreateGridColumnRepositoryItem<RepositoryItemGridLookUpEdit>(grdControlItems, sourceList,
				sourceFieldName, destinationValueMember, sourceDisplayMember);
		}

		public void LoadGrid()
		{
			//if (BaseControllerObject != null)
			//	grdControlItems.DataSource = BaseControllerObject.GetItemsList();
			if (BaseControllerObject != null)
				grdControlItems.DataSource = BaseMvcController.CollectSearchCriteria();
		}

		private void btnSearch_Click(object sender, System.EventArgs e)
		{
			if (BaseMvcController == null)
				return;

			object[] list = BaseMvcController.CollectSearchCriteria();
			grdControlItems.DataSource = list;
			BaseMvcController.ClearControls();
		}

		private void btnEdit_Click(object sender, System.EventArgs e)
		{
			if (BaseMvcController != null && BaseMvcController.ActiveDBItem != null)
				BaseControllerObject.Edit(BaseMvcController.ActiveDBItem);
		}

		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			BaseMvcController.Delete(BaseMvcController.ActiveDBItem);
			LoadGrid();
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
			throw new System.NotImplementedException();
		}

		public void FillControls()
		{
			throw new System.NotImplementedException();
		}

		#endregion

		private void gridView1_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
		{
			Type type = e.GetType();
		}

		private void gridView1_DataSourceChanged(object sender, EventArgs e)
		{
			Type type = e.GetType();
		}
	}
}
