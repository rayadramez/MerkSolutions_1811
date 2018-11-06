using System.Collections.Generic;
using CommonControlLibrary;
using MerkDataBaseBusinessLogicProject;
using MVCBusinessLogicLibrary.BaseViewers;
using MVCBusinessLogicLibrary.MVCFactories;
using MVCBusinessLogicLibrary.Viewers;

namespace CommonUserControls.SettingsViewers.User_UserGroupViewers
{
	public partial class User_UserGroup_SearchViewer : 
		//DevExpress.XtraEditors.XtraUserControl
		CommonAbstractSearchViewer<User_UserGroup_cu>,
		IUser_UserGroup_Viewer
	{
		public User_UserGroup_SearchViewer()
		{
			InitializeComponent();

			CommonViewsActions.SetupSyle(this);
		}

		#region Overrides of CommonAbstractViewer<User_UserGroup_cu>

		public override object ViewerID
		{
			get { return (int)ViewerName.User_UserGroup_Viewer; }
		}

		public override string HeaderTitle
		{
			get { return "ربـــط مجمـوعــــة المستخـدميـــن بالمستخــدميــــن"; }
		}

		public override string GridXML
		{
			get { return Resources.LocalizedRes.grd_User_UserGroup_SearchViewer; }
		}

		#endregion

		#region Implementation of IUser_UserGroup_Viewer

		public object UserGroup { get; set; }
		public object UserInternalCode { get; set; }
		public object UserName { get; set; }
		public List<User_UserGroup_cu> List_User_UserGroup { get; set; }

		#endregion
	}
}
