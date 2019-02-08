using System.Windows.Forms;
using CommonControlLibrary;
using DevExpress.Utils;
using DevExpress.XtraTab;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;
using MVCBusinessLogicLibrary.MVCFactories;

namespace MVCBusinessLogicLibrary.BaseViewers
{
	public partial class CommonTabControl_UC<TEntity> : DevExpress.XtraEditors.XtraUserControl
		where TEntity : DBCommon, IDBCommon, new()
	{
		private static BaseContainer<TEntity> _baseContainer;
		private static BaseEditorViewerContainer<TEntity> _baseEditorContainer;
		private BaseController<TEntity> BaseController { get; set; }

		public CommonTabControl_UC(BaseController<TEntity> baseController)
		{
			InitializeComponent();
			BaseController = baseController;
		}

		public CommonTabControl_UC()
		{

		}

		public XtraTabControl InitializeCommonTabControl(Control viewer,
			string pageHeaderTitle, int tabIndex, bool isPageEnabled, bool isPageVisible, ViewerName viewerName,
			DefaultBoolean showCloseButton = DefaultBoolean.False, BorderStyle borderStyle = BorderStyle.None,
			DockStyle dockStyle = DockStyle.Fill)
		{
			XtraTabPage tabPage = CommonViewsActions.CreateTabPageControl(mainTab, pageHeaderTitle, tabIndex, "Office 2010 Black",
				isPageEnabled,
				isPageVisible,
				showCloseButton, borderStyle, dockStyle);
			tabPage.Dock = DockStyle.Fill;
			_baseEditorContainer = new BaseEditorViewerContainer<TEntity>(BaseController);
			_baseEditorContainer.Dock = DockStyle.Fill;
			_baseEditorContainer.InitalizeContainer(viewer, false, true);
			_baseContainer = BaseController.InitiaizeBaseContainer(_baseEditorContainer, pageHeaderTitle, false, false);

			tabPage.Controls.Add(_baseContainer);
			mainTab.TabPages.Add(tabPage);

			return mainTab;
		}
	}
}
