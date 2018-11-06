using System.Collections.Generic;
using MerkDataBaseBusinessLogicProject;
using MVCBusinessLogicLibrary.BaseViewers;
using MVCBusinessLogicLibrary.MVCFactories;
using MVCBusinessLogicLibrary.Viewers;

namespace CommonUserControls.SettingsViewers.User_Application_Viewers
{
	public partial class User_Application_SearchViewer : 
		//DevExpress.XtraEditors.XtraUserControl
		CommonAbstractSearchViewer<User_Application_cu>,
		IUser_Application_Viewer
	{
		public User_Application_SearchViewer()
		{
			InitializeComponent();
		}

		#region Overrides of CommonAbstractViewer<User_Application_cu>

		public override object ViewerID
		{
			get { return (int)ViewerName.User_Application_Viewer; }
		}

		public override string HeaderTitle
		{
			get { return "ربـــط المستخـدميـــن بالبـرامـــج"; }
		}

		public override string GridXML
		{
			get { return Resources.LocalizedRes.grd_UserGroupSearchViewer; }
		}

		#endregion

		#region Implementation of IUser_Application_Viewer

		public List<User_Application_cu> List_User_Application { get; set; }

		#endregion
	}
}
