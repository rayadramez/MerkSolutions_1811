using CommonControlLibrary;
using MerkDataBaseBusinessLogicProject;
using MVCBusinessLogicLibrary.BaseViewers;
using MVCBusinessLogicLibrary.MVCFactories;
using MVCBusinessLogicLibrary.Viewers;

namespace CommonUserControls.SettingsViewers.UserGroup
{
	public partial class UserGroupSearchViewer_UC :
		//DevExpress.XtraEditors.XtraUserControl
		CommonAbstractSearchViewer<UserGroup_cu>,
		IUserGroupViewer
	{
		public UserGroupSearchViewer_UC()
		{
			InitializeComponent();

			CommonViewsActions.LoadXMLFromString(layoutControl1, Resources.LocalizedRes.lyt_UserGroupSearchViewer_UC);
			CommonViewsActions.SetupSyle(this);

			txtFirstNameP.Focus();
		}

		public override void ClearControls()
		{
			txtFirstNameP.EditValue = null;
			txtFirstNameS.EditValue = null;
			txtInternalCode.EditValue = null;
			txtDescription.EditValue = null;
		}

		#region Overrides of CommonAbstractViewer<UserGroup_cu>

		public override object ViewerID
		{
			get { return (int) ViewerName.UserGroup_Viewer; }
		}

		public override string HeaderTitle
		{
			get { return "بيــانـــــات مجمـوعــــــات المستخــدميـــــــن"; }
		}

		public override string GridXML
		{
			get { return Resources.LocalizedRes.grd_UserGroupSearchViewer; }
		}

		#endregion

		#region Implementation of IUserGroupViewer

		public object NameP
		{
			get { return txtFirstNameP.EditValue; }
			set { txtFirstNameP.EditValue = value; }
		}

		public object NameS
		{
			get { return txtFirstNameS.EditValue; }
			set { txtFirstNameS.EditValue = value; }
		}

		public object InternalCode
		{
			get { return txtInternalCode.EditValue; }
			set { txtInternalCode.EditValue = value; }
		}

		public object Description
		{
			get { return txtDescription.EditValue; }
			set { txtDescription.EditValue = value; }
		}

		#endregion
	}
}
