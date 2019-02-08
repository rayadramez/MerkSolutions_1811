using CommonUserControls.SettingsViewers.BankAccountViewers;
using CommonUserControls.SettingsViewers.BankViewers;
using CommonUserControls.SettingsViewers.CashBoxTranasctionType_GeneralChartOfAccountType_Viewers;
using CommonUserControls.SettingsViewers.CashBoxViewers;
using CommonUserControls.SettingsViewers.ChartOfAccountViewers;
using CommonUserControls.SettingsViewers.GeneralChartOfAccountTypeViewers;
using CommonUserControls.SettingsViewers.PersonType_ChartOfAccount_Viewers;
using CommonUserControls.SettingsViewers.Person_ChartOfAccount_Viewers;
using MerkDataBaseBusinessLogicProject;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MVCBusinessLogicLibrary.BaseViewers;
using MVCBusinessLogicLibrary.MVCFactories;

namespace ApplicationsSettingsTool
{
	public partial class AccountingSettingsContainer : DevExpress.XtraEditors.XtraUserControl
	{
		private ChartOfAccount_EditorViewer _chartOfAccountEditorViewer;
		private ChartOfAccount_SearchViewer _chartOfAccountSearchViewer;

		private CashBox_EditorViewer _cashBoxEditorViewer;
		private CashBox_SearchViewer _cashBoxSearchViewer;

		private GeneralChartOfAccountType_EditorViewer _generalChartOfAccountTypeEditorViewer;
		private GeneralChartOfAccountType_SearchViewer _generalChartOfAccountTypeSearchViewer;

		private CashBoxTranasctionType_GeneralChartOfAccountType_EditorViewer
			_cashBoxTranasctionTypeGeneralChartOfAccountTypeEditorViewer;
		private CashBoxTranasctionType_GeneralChartOfAccountType_SearchViewer
			_cashBoxTranasctionTypeGeneralChartOfAccountTypeSearchViewer;

		private Bank_EditorViewer _bankEditorViewer;
		private Bank_SearchViewer _bankSearchViewer;

		private BankAccount_EditorViewer _bankAccountEditorViewer;
		private BankAccount_SearchViewer _bankAccountSearchViewer;

		private PersonType_ChartOfAccount_EditorViewer _personTypeChartOfAccountEditorViewer;
		private PersonType_ChartOfAccount_SearchViewer _personTypeChartOfAccountSearchViewer;

		private Person_ChartOfAccount_EditorViewer _personChartOfAccountEditorViewer;
		private Person_ChartOfAccount_SearchViewer _personChartOfAccountSearchViewer;

		public AccountingSettingsContainer()
		{
			InitializeComponent();
		}

		private void btnChartOfAccount_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
		{
			BaseController<ChartOfAccount_cu>.ShowControl(ref _chartOfAccountEditorViewer, ref _chartOfAccountSearchViewer,
				splitContainerControl2.Panel1, EditorContainerType.Settings, ViewerName.ChartOfAccountViewer,
				DB_CommonTransactionType.CreateNew, "شجـــرة الحسـابـــــات", AbstractViewerType.SearchViewer, true);
		}

		private void btnCashBox_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
		{
			BaseController<CashBox_cu>.ShowControl(ref _cashBoxEditorViewer,
			                                       ref _cashBoxSearchViewer,
			                                       splitContainerControl2.Panel1,
			                                       EditorContainerType.Settings,
			                                       ViewerName.CashBoxViewer,
			                                       DB_CommonTransactionType.CreateNew,
			                                       "الخـزائـــــن",
			                                       AbstractViewerType.SearchViewer,
			                                       true);
		}

		private void btnGeneralChartOfAccountType_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
		{
			BaseController<GeneralChartOfAccountType_cu>.ShowControl(ref _generalChartOfAccountTypeEditorViewer,
			                                                         ref _generalChartOfAccountTypeSearchViewer,
			                                                         splitContainerControl2.Panel1,
			                                                         EditorContainerType.Settings,
			                                                         ViewerName.GeneralChartOfAccountType_Viewer,
			                                                         DB_CommonTransactionType.CreateNew,
			                                                         "أنـــواع المعـامــــلات المـاليــــة",
			                                                         AbstractViewerType.SearchViewer,
			                                                         true);
		}

		private void btnBank_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
		{
			BaseController<Bank_cu>.ShowControl(ref _bankEditorViewer,
			                                    ref _bankSearchViewer,
			                                    splitContainerControl2.Panel1,
			                                    EditorContainerType.Settings,
			                                    ViewerName.Bank_Viewer,
			                                    DB_CommonTransactionType.CreateNew,
			                                    "البنـــــوك",
			                                    AbstractViewerType.SearchViewer,
			                                    true);
		}

		private void btnBankAccount_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
		{
			BaseController<BankAccount_cu>.ShowControl(ref _bankAccountEditorViewer,
												ref _bankAccountSearchViewer,
			                                    splitContainerControl2.Panel1,
			                                    EditorContainerType.Settings,
			                                    ViewerName.BankAccount_Viewer,
			                                    DB_CommonTransactionType.CreateNew,
			                                    "الحسـابـــــات البنكيـــــة",
			                                    AbstractViewerType.SearchViewer,
			                                    true);
		}

		private void btnPersonTypeChartOfAccount_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
		{
			BaseController<PersonType_ChartOfAccount_cu>.ShowControl(ref _personTypeChartOfAccountEditorViewer,
			                                                         ref _personTypeChartOfAccountSearchViewer,
			                                                         splitContainerControl2.Panel1,
			                                                         EditorContainerType.Settings,
			                                                         ViewerName.PersonType_ChartOfAccount_Viewer,
			                                                         DB_CommonTransactionType.CreateNew,
			                                                         "ربــط الحسـابـــات بـانـــواع الأشخــــاص",
			                                                         AbstractViewerType.SearchViewer,
			                                                         true);
		}

		private void btnPersonChartOfAccount_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
		{
			BaseController<Person_ChartOfAccount_cu>.ShowControl(ref _personChartOfAccountEditorViewer,
			                                                     ref _personChartOfAccountSearchViewer,
			                                                     splitContainerControl2.Panel1,
			                                                     EditorContainerType.Settings,
			                                                     ViewerName.Person_ChartOfAccount_Viewer,
			                                                     DB_CommonTransactionType.CreateNew,
			                                                     "ربـط الحسـابـــات المـاليـــة بالأشخـــاص",
			                                                     AbstractViewerType.SearchViewer,
			                                                     true);
		}
	}
}
