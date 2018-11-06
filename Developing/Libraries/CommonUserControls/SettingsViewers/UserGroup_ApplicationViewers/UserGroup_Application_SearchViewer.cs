using System.Collections.Generic;
using MerkDataBaseBusinessLogicProject;
using MVCBusinessLogicLibrary.BaseViewers;
using MVCBusinessLogicLibrary.MVCFactories;
using MVCBusinessLogicLibrary.Viewers;

namespace CommonUserControls.SettingsViewers.UserGroup_ApplicationViewers
{
	public partial class UserGroup_Application_SearchViewer : 
		//DevExpress.XtraEditors.XtraUserControl
		CommonAbstractSearchViewer<UserGroup_Application_cu>,
		IUserGroup_Application_Viewer
	{
		public UserGroup_Application_SearchViewer()
		{
			InitializeComponent();
		}

		#region Overrides of CommonAbstractViewer<User_UserGroup_cu>

		public override object ViewerID
		{
			get { return (int)ViewerName.RoleRegistration_Viewer; }
		}

		public override string HeaderTitle
		{
			get { return " ربـــط مجمـوعـــات المستخـدميـــن بالوظائـــف"; }
		}

		public override string GridXML
		{
			get { return Resources.LocalizedRes.grd_UserGroup_Application_SearchViewer; }
		}

		#endregion

		#region Implementation of IUserGroup_Application_Viewer

		public List<UserGroup_Application_cu> List_UserGroup_Application { get; set; }

		#endregion
	}
}
