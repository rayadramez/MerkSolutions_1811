using System;
using System.Windows.Forms;
using CommonUserControls.SettingsViewers.ChartOfAccountViewers;
using MerkDataBaseBusinessLogicProject;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MVCBusinessLogicLibrary.BaseViewers;
using MVCBusinessLogicLibrary.MVCFactories;

namespace CommonUserControls.MerkFinanceCommonViewers
{
	public partial class MainHomeScreen_UC : UserControl
	{
		private Control _mainForm;
		private ChartOfAccount_EditorViewer _chartOfAccountEditorViewer;
		private ChartOfAccount_SearchViewer _chartOfAccountSearchViewer;

		public MainHomeScreen_UC()
		{
			InitializeComponent();
		}

		public void Initialize(Control mainForm)
		{
			if (mainForm == null)
				return;

			_mainForm = mainForm;
		}

		private void btnChartOfAccount_ItemClick(object sender, DevExpress.XtraEditors.TileItemEventArgs e)
		{
			if (_mainForm == null)
				return;

			BaseController<ChartOfAccount_cu>.ShowControl(ref _chartOfAccountEditorViewer, ref _chartOfAccountSearchViewer,
				_mainForm, EditorContainerType.Settings, ViewerName.ChartOfAccountViewer,
				DB_CommonTransactionType.CreateNew, "شجـــرة الحسـابـــــات", AbstractViewerType.SearchViewer, true);
		}
	}
}
