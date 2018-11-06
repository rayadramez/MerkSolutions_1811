using CommonControlLibrary;
using MerkDataBaseBusinessLogicProject;
using MVCBusinessLogicLibrary.BaseViewers;
using MVCBusinessLogicLibrary.Controller;
using MVCBusinessLogicLibrary.MVCFactories;
using MVCBusinessLogicLibrary.Viewers;

namespace CommonUserControls.SettingsViewers.UserGroup
{
	public partial class UserGroupEditorViewer_UC :
		//DevExpress.XtraEditors.XtraUserControl
		CommonAbstractEditorViewer<UserGroup_cu>,
		IUserGroupViewer
	{
		public UserGroupEditorViewer_UC()
		{
			InitializeComponent();

			CommonViewsActions.LoadXMLFromString(layoutControl1, Resources.LocalizedRes.lyt_UserGroupEditor_UC);
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

		public override IMVCController<UserGroup_cu> ViewerCollector { get; set; }

		public override object ViewerID
		{
			get { return (int)ViewerName.UserGroup_Viewer; }
		}

		public override string HeaderTitle
		{
			get { return "بيــانـــــات مجمـوعــــــات المستخــدميـــــــن"; }
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
