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

namespace CommonUserControls.SettingsViewers.UserGroup_ApplicationViewers
{
	public partial class UserGroup_Application_EditorViewer :
		//DevExpress.XtraEditors.XtraUserControl
		CommonAbstractEditorViewer<UserGroup_Application_cu>,
		IUserGroup_Application_Viewer
	{
		private UserGroup_cu SelectedUserGroupFromGrid = null;
		private List<UserGroup_cu> List_SelectedUserGroupsToBedAdded = null;

		public UserGroup_Application_EditorViewer()
		{
			InitializeComponent();

			CommonViewsActions.LoadXMLFromString(layoutControl1, Resources.LocalizedRes.lyt_UserGroup_Application_EditorViewer);
			CommonViewsActions.SetupGridControl(grdUserGroups, Resources.LocalizedRes.grd_UserGroup_Application_Internal, true);
			CommonViewsActions.SetupSyle(this);
		}

		public override void FillControls()
		{
			grdUserGroups.DataSource = UserGroup_cu.ItemsList;
			CommonViewsActions.FillGridlookupEdit(lkeApplications, Application_p.ItemsList);
		}

		public override void ClearControls()
		{
			txtUserGroupInternalCode.EditValue = null;
			txtUserGroupName.EditValue = null;
			lst_UserGroup.DataSource = null;
			lkeApplications.EditValue = null;
			SelectedUserGroupFromGrid = null;
			List_SelectedUserGroupsToBedAdded = null;
			List_UserGroup_Application = null;
		}

		private void txtUserGroupInternalCode_EditValueChanged(object sender, EventArgs e)
		{
			if (txtUserGroupInternalCode.EditValue == null || string.IsNullOrEmpty(txtUserGroupInternalCode.Text) ||
				string.IsNullOrWhiteSpace(txtUserGroupInternalCode.Text))
			{
				grdUserGroups.DataSource = UserGroup_cu.ItemsList;
				return;
			}

			grdUserGroups.DataSource =
				UserGroup_cu.ItemsList.FindAll(
					item =>
						item.InternalCode != null &&
						Convert.ToString(item.InternalCode).Contains(Convert.ToString(txtUserGroupInternalCode.Text)));
		}

		private void txtUserGroupName_EditValueChanged(object sender, EventArgs e)
		{
			if (txtUserGroupName.EditValue == null || string.IsNullOrEmpty(txtUserGroupName.Text) ||
				string.IsNullOrWhiteSpace(txtUserGroupName.Text))
			{
				grdUserGroups.DataSource = UserGroup_cu.ItemsList;
				return;
			}

			grdUserGroups.DataSource =
				UserGroup_cu.ItemsList.FindAll(
					item => item.Name_P != null && Convert.ToString(item.Name_P).Contains(Convert.ToString(txtUserGroupName.Text)));
		}

		private void btnAddList_Click(object sender, EventArgs e)
		{
			if (lkeApplications.EditValue == null)
			{
				XtraMessageBox.Show("يجـب إختيــار البـرنـامج", "تنبيــــــــــه", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return;
			}

			if (SelectedUserGroupFromGrid == null)
			{
				XtraMessageBox.Show("يجب إختيــار مجمـوعــة المستخـدميـــن", "تنبيــــــــــه", MessageBoxButtons.OK,
					MessageBoxIcon.Hand);
				return;
			}

			if (List_SelectedUserGroupsToBedAdded == null)
				List_SelectedUserGroupsToBedAdded = new List<UserGroup_cu>();

			if (List_SelectedUserGroupsToBedAdded.Count > 0)
				if (List_SelectedUserGroupsToBedAdded.Exists(
						item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(SelectedUserGroupFromGrid.ID))))
				{
					XtraMessageBox.Show("تمــت إضافتـــه مـن قبـــل", "تنبيــــــــــه", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					return;
				}

			List_SelectedUserGroupsToBedAdded.Add(SelectedUserGroupFromGrid);
			CommonViewsActions.FillListBoxControl(lst_UserGroup, List_SelectedUserGroupsToBedAdded);
			lst_UserGroup.Refresh();

			UserGroup_Application_cu userGroupBridge = new UserGroup_Application_cu();
			userGroupBridge.UserGroup_CU_ID = SelectedUserGroupFromGrid.ID;
			userGroupBridge.Application_P_ID = Convert.ToInt32(lkeApplications.EditValue);
			if (List_UserGroup_Application == null)
				List_UserGroup_Application = new List<UserGroup_Application_cu>();
			List_UserGroup_Application.Add(userGroupBridge);
		}

		private void btnRemoveFromList_Click(object sender, EventArgs e)
		{
			if (lst_UserGroup.SelectedItems.Count == 0 || List_UserGroup_Application == null)
				return;

			UserGroup_cu selecteduserGroup = (UserGroup_cu)lst_UserGroup.SelectedItem;
			if (selecteduserGroup == null)
				return;
			if (List_SelectedUserGroupsToBedAdded.Exists(
				item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(selecteduserGroup.ID))))
				List_SelectedUserGroupsToBedAdded.Remove(selecteduserGroup);

			CommonViewsActions.FillListBoxControl(lst_UserGroup, List_SelectedUserGroupsToBedAdded);
			lst_UserGroup.Refresh();

			UserGroup_Application_cu userGroupBridge =
				List_UserGroup_Application.Find(
					item => Convert.ToInt32(item.UserGroup_CU_ID).Equals(Convert.ToInt32(selecteduserGroup.ID)));
			if (userGroupBridge == null)
				return;
			List_UserGroup_Application.Remove(userGroupBridge);
		}

		private void gridView1_MouseUp(object sender, MouseEventArgs e)
		{
			SelectedUserGroupFromGrid = CommonViewsActions.GetSelectedRowObject<UserGroup_cu>((GridView)sender);
		}

		private void gridView1_GotFocus(object sender, EventArgs e)
		{
			SelectedUserGroupFromGrid = CommonViewsActions.GetSelectedRowObject<UserGroup_cu>((GridView)sender);
		}

		private void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
		{
			SelectedUserGroupFromGrid = CommonViewsActions.GetSelectedRowObject<UserGroup_cu>((GridView)sender);
		}

		#region Overrides of CommonAbstractViewer<User_UserGroup_cu>

		public override IMVCController<UserGroup_Application_cu> ViewerCollector { get; set; }

		public override object ViewerID
		{
			get { return (int)ViewerName.UserGroup_Application_Viewer; }
		}

		public override string HeaderTitle
		{
			get { return "ربـــط مجمـوعــــة المستخـدميـــن بالبـرامـــج"; }
		}

		#endregion

		#region Implementation of IUserGroup_Application_Viewer

		public List<UserGroup_Application_cu> List_UserGroup_Application { get; set; }

		#endregion
	}
}
