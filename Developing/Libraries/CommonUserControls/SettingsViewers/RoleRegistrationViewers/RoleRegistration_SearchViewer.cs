using System.Collections.Generic;
using MerkDataBaseBusinessLogicProject;
using MVCBusinessLogicLibrary.BaseViewers;
using MVCBusinessLogicLibrary.MVCFactories;
using MVCBusinessLogicLibrary.Viewers;

namespace CommonUserControls.SettingsViewers.RoleRegistrationViewers
{
	public partial class RoleRegistration_SearchViewer : 
		//DevExpress.XtraEditors.XtraUserControl
		CommonAbstractSearchViewer<RoleRegistration_cu>,
		IRoleRegistrationViewer
	{
		public RoleRegistration_SearchViewer()
		{
			InitializeComponent();
		}

		#region Overrides of CommonAbstractViewer<RoleRegistration_cu>

		public override object ViewerID
		{
			get { return (int)ViewerName.UserGroup_Application_Viewer; }
		}

		public override string HeaderTitle
		{
			get { return "ربـــط مجمـوعــــة المستخـدميـــن بالبـرامـــج"; }
		}

		public override string GridXML
		{
			get { return Resources.LocalizedRes.grd_UserGroupSearchViewer; }
		}

		#endregion

		#region Implementation of IRoleRegistrationViewer

		public List<RoleRegistration_cu> List_RoleRegistration { get; set; }

		#endregion
	}
}
