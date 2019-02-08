using System.Windows.Forms;
using ApplicationConfiguration;
using CommonControlLibrary;
using DevExpress.XtraLayout.Utils;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;
using MVCBusinessLogicLibrary.MVCFactories;

namespace MVCBusinessLogicLibrary.BaseViewers
{
	public partial class BaseContainer<TEntity> :
		DevExpress.XtraEditors.XtraUserControl where TEntity : DBCommon, IDBCommon, new()
	{
		public MVCViewerType CommonViewerType { get; set; }
		public BaseController<TEntity> BaseControllerObject { get; set; }
		public ViewerName ViewerName { get; set; }
		public BaseContainer<TEntity> _baseContainer;
		public Control _editorViewerToShow;

		public string HeaderTitle
		{
			get { return lblTitle.Text; }
			set { lblTitle.Text = value; }
		}

		public bool ShowTitle
		{
			get { return lblTitle.Visible; }
			set { lblTitle.Visible = value; }
		}

		public bool ShowCloseButtong
		{
			get { return btnClose.Visible; }
			set { btnClose.Visible = value; }
		}

		public bool ShowFullScreenButton
		{
			get { return btnFullScreen.Visible; }
			set { btnFullScreen.Visible = value; }
		}

		public bool ShowTopControls
		{
			set
			{
				lytTopSeperatorLabel.Visibility = value ? LayoutVisibility.Always : LayoutVisibility.Never;
				lytBottomSperatorLabel.Visibility = value ? LayoutVisibility.Always : LayoutVisibility.Never;
				lytPnlControls.Visibility = value ? LayoutVisibility.Always : LayoutVisibility.Never;
				lytTitle.Visibility = value ? LayoutVisibility.Always : LayoutVisibility.Never;
				lytTopSeperatorLabel.Visibility = value ? LayoutVisibility.Always : LayoutVisibility.Never;
			}
		}

		public string GridXML { get; set; }

		public bool EnableRelatedVeiwersButton
		{
			set { btnShowHideRelatedViewers.Enabled = value; }
		}

		public bool IsBaseControllerInitialized
		{
			get { return BaseControllerObject != null; }
		}

		public BaseContainer()
		{
			InitializeComponent();

			CommonViewsActions.LoadXMLFromString(layoutControl1, Resources.LocalizedRes.lyt_BaseContainer);
			CommonViewsActions.SetupSyle(this);

			if (!string.IsNullOrEmpty(ApplicationStaticConfiguration.SkinName) &&
				!string.IsNullOrWhiteSpace(ApplicationStaticConfiguration.SkinName))
				if (ApplicationStaticConfiguration.SkinName.Equals("McSkin"))
					btnClose.Image = Properties.Resources.ExitIcon_8;
				else
					btnClose.Image = Properties.Resources.Exit_1_16;

			BringToFront();
		}

		public void InitializeBaseViewerController(BaseController<TEntity> baseController)
		{
			BaseControllerObject = baseController;

			if (!string.IsNullOrEmpty(ApplicationStaticConfiguration.SkinName) &&
				!string.IsNullOrWhiteSpace(ApplicationStaticConfiguration.SkinName))
				if (ApplicationStaticConfiguration.SkinName.Equals("McSkin"))
					btnClose.Image = Properties.Resources.ExitIcon_8;
				else
					btnClose.Image = Properties.Resources.Exit_1_16;

			BringToFront();
		}

		public void InitializeBaseContainer(ViewerName viewerName, AbstractViewerType viewerType, string headerTitle,
			bool enableFullScreenButton, bool showNewButton, bool showPrintButton, bool showClearButton)
		{
			HeaderTitle = headerTitle;
			ShowFullScreenButton = enableFullScreenButton;
			lytNewButton.Visibility = showNewButton ? LayoutVisibility.Always : LayoutVisibility.Never;
			lytClearButton.Visibility = showClearButton ? LayoutVisibility.Always : LayoutVisibility.Never;
			lytPrintButton.Visibility = showPrintButton ? LayoutVisibility.Always : LayoutVisibility.Never;
			ViewerName = viewerName;

			switch (viewerType)
			{
				case AbstractViewerType.EditorViewer:
					CommonViewsActions.ShowUserControl(ref BaseController<TEntity>.BasicEditorViewerContainer, pnlMain, true);
					BaseControllerObject.PassBaseController(BaseController<TEntity>.BasicEditorViewerContainer);
					break;
				case AbstractViewerType.SearchViewer:
					CommonViewsActions.ShowUserControl(ref BaseController<TEntity>._baseSearchContainer, pnlMain, true);
					BaseControllerObject.PassBaseController(BaseController<TEntity>._baseSearchContainer);
					break;
			}

			if (!string.IsNullOrEmpty(ApplicationStaticConfiguration.SkinName) &&
				!string.IsNullOrWhiteSpace(ApplicationStaticConfiguration.SkinName))
				if (ApplicationStaticConfiguration.SkinName.Equals("McSkin"))
					btnClose.Image = Properties.Resources.ExitIcon_8;
				else
					btnClose.Image = Properties.Resources.Exit_1_16;
			BringToFront();
		}

		public void InitiaizeBaseContainer(Control controlToAttach, string headerTitle, bool showTopControls)
		{
			HeaderTitle = headerTitle;
			ShowTopControls = showTopControls;

			if (!pnlMain.Controls.Contains(controlToAttach))
			{
				controlToAttach.Dock = DockStyle.Fill;
				pnlMain.Controls.Add(controlToAttach);
			}
			BringToFront();
		}

		private void btnShowHideRelatedViewers_CheckedChanged(object sender, System.EventArgs e)
		{
			BaseControllerObject.ShowRelatedViewers(btnShowHideRelatedViewers.Checked);
		}

		private void btnClose_Click(object sender, System.EventArgs e)
		{
			if (ParentForm != null)
			{
				if(ParentForm.ParentForm != null)
					ParentForm.ParentForm.BringToFront();
				ParentForm.Close();
			}
		}

		private void btnFullScreen_Click(object sender, System.EventArgs e)
		{
			PopupBaseForm _popupForm = new PopupBaseForm();
			_popupForm.InitializePopupBaseForm(FormWindowState.Maximized, false, HeaderTitle);
			CommonViewsActions.ShowUserControl(ref _baseContainer, _popupForm);
		}

		private void btnClear_Click(object sender, System.EventArgs e)
		{

		}

		private void btnAddNew_Click(object sender, System.EventArgs e)
		{
			BaseController<TEntity>.ShowEditorControl(ref _editorViewerToShow, pnlMain, null, null, EditorContainerType.Regular,
				ViewerName, DB_CommonTransactionType.CreateNew, HeaderTitle, true);
		}
	}
}
