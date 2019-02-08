using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CommonControlLibrary;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using MerkDataBaseBusinessLogicProject;
using MVCBusinessLogicLibrary.BaseViewers;
using MVCBusinessLogicLibrary.Controller;
using MVCBusinessLogicLibrary.MVCFactories;
using MVCBusinessLogicLibrary.Viewers;

namespace CommonUserControls.SettingsViewers.User_UserGroupViewers
{
	public partial class User_UserGroup_EditorViewer :
		//DevExpress.XtraEditors.XtraUserControl
		CommonAbstractEditorViewer<User_UserGroup_cu>,
		IUser_UserGroup_Viewer
	{
		private User_cu SelectedUserFromGrid = null;
		private List<User_cu> List_SelectedusersToBedAdded = null;

		public User_UserGroup_EditorViewer()
		{
			InitializeComponent();

			CommonViewsActions.LoadXMLFromString(layoutControl1, Resources.LocalizedRes.lyt_User_UserGroup_EditorViewer);
			CommonViewsActions.SetupGridControl(grdUsers, Resources.LocalizedRes.grd_User_UserGroup_InternalUsers, true);
			CommonViewsActions.SetupSyle(this);
		}

		public override void FillControls()
		{
			grdUsers.DataSource = User_cu.ItemsList;
			CommonViewsActions.FillGridlookupEdit(lkeUserGroup, UserGroup_cu.ItemsList);
			//CommonViewsActions.FillListBoxControl(lst_User_UserGroup, User_cu.ItemsList, "FullName", "Person_CU_ID");
		}

		public override void ClearControls()
		{
			txtUserInternalCode.EditValue = null;
			txtUserName.EditValue = null;
			lst_User_UserGroup.DataSource = null;
			lkeUserGroup.EditValue = null;
			txtuserLogin.EditValue = null;
			List_SelectedusersToBedAdded = null;
			SelectedUserFromGrid = null;
			List_User_UserGroup = null;
		}

		private void btnAddList_Click(object sender, System.EventArgs e)
		{
			if (lkeUserGroup.EditValue == null)
			{
				XtraMessageBox.Show("يجـب إختيــار مجمــوعــــة المستخــدميـــــن", "تنبيــــــــــه", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return;
			}

			if (SelectedUserFromGrid == null)
			{
				XtraMessageBox.Show("يجب إختيــار المستخـــــدم", "تنبيــــــــــه", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return;
			}

			if (List_SelectedusersToBedAdded == null)
				List_SelectedusersToBedAdded = new List<User_cu>();

			if (List_SelectedusersToBedAdded.Count > 0)
				if (List_SelectedusersToBedAdded.Exists(
						item => Convert.ToInt32(item.Person_CU_ID).Equals(Convert.ToInt32(SelectedUserFromGrid.Person_CU_ID))))
				{
					XtraMessageBox.Show("تمــت إضافتـــه مـن قبـــل", "تنبيــــــــــه", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					return;
				}

			List_SelectedusersToBedAdded.Add(SelectedUserFromGrid);
			CommonViewsActions.FillListBoxControl(lst_User_UserGroup, List_SelectedusersToBedAdded, "FullName", "Person_CU_ID");
			lst_User_UserGroup.Refresh();

			User_UserGroup_cu userBridge = new User_UserGroup_cu();
			userBridge.User_CU_ID = SelectedUserFromGrid.Person_CU_ID;
			userBridge.UserGroup_CU_ID = Convert.ToInt32(lkeUserGroup.EditValue);
			if (List_User_UserGroup == null)
				List_User_UserGroup = new List<User_UserGroup_cu>();
			List_User_UserGroup.Add(userBridge);
		}

		private void btnRemoveFromList_Click(object sender, EventArgs e)
		{
			if (lst_User_UserGroup.SelectedItems.Count == 0 || List_User_UserGroup == null)
			{
				XtraMessageBox.Show("لا يـوجــد مستخــدميـــن", "تنبيــــــــــه", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return;
			}

			User_cu selecteduser = (User_cu)lst_User_UserGroup.SelectedItem;
			if (selecteduser == null)
				return;
			if (List_SelectedusersToBedAdded.Exists(
				item => Convert.ToInt32(item.Person_CU_ID).Equals(Convert.ToInt32(selecteduser.Person_CU_ID))))
				List_SelectedusersToBedAdded.Remove(selecteduser);

			CommonViewsActions.FillListBoxControl(lst_User_UserGroup, List_SelectedusersToBedAdded, "FullName", "Person_CU_ID");
			lst_User_UserGroup.Refresh();

			User_UserGroup_cu userBridge =
				List_User_UserGroup.Find(item => Convert.ToInt32(item.User_CU_ID).Equals(Convert.ToInt32(selecteduser.Person_CU_ID)));
			if (userBridge == null)
				return;
			List_User_UserGroup.Remove(userBridge);
		}

		private void gridView1_GotFocus(object sender, EventArgs e)
		{
			SelectedUserFromGrid = GetSelectedRow<User_cu>((GridView)sender);
		}

		private void gridView1_MouseUp(object sender, MouseEventArgs e)
		{
			SelectedUserFromGrid = GetSelectedRow<User_cu>((GridView)sender);
		}

		private void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
		{
			SelectedUserFromGrid = GetSelectedRow<User_cu>((GridView)sender);
		}

		private TEntity GetSelectedRow<TEntity>(GridView gridView)
		{
			return (TEntity)gridView.GetRow(gridView.FocusedRowHandle);
		}

		private void txtUserInternalCode_EditValueChanged(object sender, EventArgs e)
		{
			if (txtUserInternalCode.EditValue == null || string.IsNullOrEmpty(txtUserInternalCode.Text) ||
				string.IsNullOrWhiteSpace(txtUserInternalCode.Text))
			{
				grdUsers.DataSource = User_cu.ItemsList;
				return;
			}

			grdUsers.DataSource =
				User_cu.ItemsList.FindAll(
					item =>
						item.InternalCode != null &&
						Convert.ToString(item.InternalCode).Contains(Convert.ToString(txtUserInternalCode.Text)));
		}

		private void txtUserName_EditValueChanged(object sender, EventArgs e)
		{
			if (txtUserName.EditValue == null || string.IsNullOrEmpty(txtUserName.Text) ||
				string.IsNullOrWhiteSpace(txtUserName.Text))
			{
				grdUsers.DataSource = User_cu.ItemsList;
				return;
			}

			grdUsers.DataSource =
				User_cu.ItemsList.FindAll(
					item => item.FullName != null && Convert.ToString(item.FullName).Contains(Convert.ToString(txtUserName.Text)));
		}

		private void txtuserLogin_EditValueChanged(object sender, EventArgs e)
		{
			if (txtuserLogin.EditValue == null || string.IsNullOrEmpty(txtuserLogin.Text) ||
					string.IsNullOrWhiteSpace(txtuserLogin.Text))
			{
				grdUsers.DataSource = User_cu.ItemsList;
				return;
			}

			grdUsers.DataSource =
				User_cu.ItemsList.FindAll(
					item => item.LoginName != null && Convert.ToString(item.LoginName).Contains(Convert.ToString(txtuserLogin.Text)));
		}

		#region Overrides of CommonAbstractViewer<User_UserGroup_cu>

		public override IMVCController<User_UserGroup_cu> ViewerCollector { get; set; }

		public override object ViewerID
		{
			get { return (int)ViewerName.User_UserGroup_Viewer; }
		}

		public override string HeaderTitle
		{
			get { return "ربـــط مجمـوعــــة المستخـدميـــن بالمستخــدميــــن"; }
		}

		#endregion

		#region Implementation of IUser_UserGroup_Viewer

		public List<User_UserGroup_cu> List_User_UserGroup { get; set; }

		#endregion

	}
}
