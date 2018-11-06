using CommonUserControls.SettingsViewers.CustomersViewers;
using CommonUserControls.SettingsViewers.FloorViewers;
using CommonUserControls.SettingsViewers.LocationViewers;
using CommonUserControls.SettingsViewers.RoleRegistrationViewers;
using CommonUserControls.SettingsViewers.SupplierViewers;
using CommonUserControls.SettingsViewers.UserGroup;
using CommonUserControls.SettingsViewers.UserGroup_ApplicationViewers;
using CommonUserControls.SettingsViewers.UserViewers;
using CommonUserControls.SettingsViewers.User_Application_Viewers;
using CommonUserControls.SettingsViewers.User_UserGroupViewers;
using MerkDataBaseBusinessLogicProject;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MVCBusinessLogicLibrary.BaseViewers;
using MVCBusinessLogicLibrary.MVCFactories;

namespace ApplicationsSettingsTool
{
	public partial class GeneralSettingsContainer_UC : DevExpress.XtraEditors.XtraUserControl
	{
		private UserEditorViewer_UC _userEditorViewer;
		private UserSearchViewer_UC _userSearchViewer;

		private UserGroupEditorViewer_UC _userGroupEditorViewer;
		private UserGroupSearchViewer_UC _userGroupSearchViewer;

		private User_UserGroup_EditorViewer _user_UserGroupEditor;
		private User_UserGroup_SearchViewer _user_UserGroupSearch;

		private UserGroup_Application_EditorViewer _userGroupApplicationEditor;
		private UserGroup_Application_SearchViewer _userGroupApplicationSearch;

		private RoleRegistration_EditorViewer _roleRegistrationEditorViewer;
		private RoleRegistration_SearchViewer _roleRegistrationSearchViewer;

		private User_Application_EditorViewer _userApplicationEditorViewer;
		private User_Application_SearchViewer _userApplicationSearchViewer;

		private Location_EditorViewer_UC _locationEditorViewer;
		private Location_SearchViewer_UC _locationSearchViewer;

		private Floor_EditorViewer _floorEditorViewer;
		private Floor_SearchViewer _floorSearchViewer;

		private Customer_EditorViewer _customerEditorViewer;
		private Customer_SearchViewer _customerSearchViewer;

		private Supplier_EditorViewer _supplierEditorViewer;
		private Supplier_SearchViewer _supplierSearchViewer;

		public GeneralSettingsContainer_UC()
		{
			InitializeComponent();
		}

		private void GeneralSettingsContainer_UC_Load(object sender, System.EventArgs e)
		{
			splitContainerControl1.Collapsed = false;
		}

		private void btnUsers_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
		{
			BaseController<User_cu>.ShowControl(ref _userEditorViewer, ref _userSearchViewer, splitContainerControl1.Panel1,
				EditorContainerType.Settings,
				ViewerName.User_Viewer, DB_CommonTransactionType.CreateNew, "بيــانـــــات المستخــدميـــــــن",
				AbstractViewerType.SearchViewer,
				true);
		}

		private void btnUserGroup_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
		{
			BaseController<UserGroup_cu>.ShowControl(ref _userGroupEditorViewer, ref _userGroupSearchViewer,
				splitContainerControl1.Panel1,
				EditorContainerType.Settings,
				ViewerName.UserGroup_Viewer, DB_CommonTransactionType.CreateNew, "بيــانـــــات مجمـوعــــــات المستخــدميـــــــن",
				AbstractViewerType.SearchViewer,
				true);
		}

		private void btnRelateUserToUserGroup_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
		{
			BaseController<User_UserGroup_cu>.ShowControl(ref _user_UserGroupEditor, ref _user_UserGroupSearch,
				splitContainerControl1.Panel1,
				EditorContainerType.Settings,
				ViewerName.User_UserGroup_Viewer, DB_CommonTransactionType.CreateNew,
				"ربـــط مجمـوعــــة المستخـدميـــن بالمستخــدميــــن",
				AbstractViewerType.SearchViewer,
				true);
		}

		private void btnUserGroup_Application_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
		{
			BaseController<UserGroup_Application_cu>.ShowControl(ref _userGroupApplicationEditor, ref _userGroupApplicationSearch,
				splitContainerControl1.Panel1,
				EditorContainerType.Settings,
				ViewerName.UserGroup_Application_Viewer, DB_CommonTransactionType.CreateNew,
				"ربـــط مجمـوعــــة المستخـدميـــن بالبـرامـــج",
				AbstractViewerType.SearchViewer,
				true);
		}

		private void btnUser_Application_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
		{
			BaseController<User_Application_cu>.ShowControl(ref _userApplicationEditorViewer, ref _userApplicationSearchViewer,
				splitContainerControl1.Panel1,
				EditorContainerType.Settings,
				ViewerName.User_Application_Viewer, DB_CommonTransactionType.CreateNew,
				"ربـــط المستخـدميـــن بالبـرامـــج",
				AbstractViewerType.SearchViewer,
				true);
		}

		private void btnRoleRegistration_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
		{
			BaseController<RoleRegistration_cu>.ShowControl(ref _roleRegistrationEditorViewer, ref _roleRegistrationSearchViewer,
				splitContainerControl1.Panel1,
				EditorContainerType.Settings,
				ViewerName.RoleRegistration_Viewer, DB_CommonTransactionType.CreateNew,
				"ربـــط مجمـوعـــات المستخـدميـــن بالوظائـــف",
				AbstractViewerType.SearchViewer,
				true);
		}

		private void btnLocations_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
		{
			BaseController<Location_cu>.ShowControl(ref _locationEditorViewer, ref _locationSearchViewer,
				splitContainerControl1.Panel1,
				EditorContainerType.Settings,
				ViewerName.Location_Viewer, DB_CommonTransactionType.CreateNew,
				"مـواقــــع المنظمـــــة",
				AbstractViewerType.SearchViewer,
				true);
		}

		private void btnFloors_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
		{
			BaseController<Floor_cu>.ShowControl(ref _floorEditorViewer, ref _floorSearchViewer,
				splitContainerControl1.Panel1,
				EditorContainerType.Settings,
				ViewerName.FloorViewer, DB_CommonTransactionType.CreateNew,
				"أدوار المـواقــــع",
				AbstractViewerType.SearchViewer,
				true);
		}

		private void btnSuppliers_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
		{
			BaseController<Supplier_cu>.ShowControl(ref _supplierEditorViewer, ref _supplierSearchViewer,
				splitContainerControl1.Panel1,
				EditorContainerType.Settings,
				ViewerName.Supplier_Viewer, DB_CommonTransactionType.CreateNew,
				"المـورديــــن",
				AbstractViewerType.SearchViewer,
				true);
		}

		private void btnCustomers_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
		{
			BaseController<Customer_cu>.ShowControl(ref _customerEditorViewer, ref _customerSearchViewer,
				splitContainerControl1.Panel1,
				EditorContainerType.Settings,
				ViewerName.Customer_Viewer, DB_CommonTransactionType.CreateNew,
				"العمــــلاء",
				AbstractViewerType.SearchViewer,
				true);
		}
	}
}
