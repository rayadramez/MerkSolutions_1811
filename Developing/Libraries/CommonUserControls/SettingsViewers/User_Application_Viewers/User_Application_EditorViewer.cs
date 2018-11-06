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

namespace CommonUserControls.SettingsViewers.User_Application_Viewers
{
	public partial class User_Application_EditorViewer :
		//DevExpress.XtraEditors.XtraUserControl
		CommonAbstractEditorViewer<User_Application_cu>,
		IUser_Application_Viewer
	{
		private User_cu SelectedUserFromGrid = null;
		private List<User_cu> List_SelectedUserToBedAdded = null;

		public User_Application_EditorViewer()
		{
			InitializeComponent();
			CommonViewsActions.LoadXMLFromString(layoutControl1, Resources.LocalizedRes.lyt_User_Application_EditorViewer);
			CommonViewsActions.SetupGridControl(grdUsers, Resources.LocalizedRes.grd_User_Application_Internal, true);
			CommonViewsActions.SetupSyle(this);
		}

		public override void FillControls()
		{
			grdUsers.DataSource = User_cu.ItemsList;
			CommonViewsActions.FillGridlookupEdit(lkeApplications, Application_p.ItemsList);
		}

		public override void ClearControls()
		{
			txtUserInternalCode.EditValue = null;
			txtUserName.EditValue = null;
			lst_Users.DataSource = null;
			lkeApplications.EditValue = null;
			SelectedUserFromGrid = null;
			List_SelectedUserToBedAdded = null;
			List_User_Application = null;
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

		private void btnAddList_Click(object sender, EventArgs e)
		{
			if (lkeApplications.EditValue == null)
			{
				XtraMessageBox.Show("يجـب إختيــار البـرنـامج", "تنبيــــــــــه", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return;
			}

			if (SelectedUserFromGrid == null)
			{
				XtraMessageBox.Show("يجب إختيــار مجمـوعــة المستخـدميـــن", "تنبيــــــــــه", MessageBoxButtons.OK,
					MessageBoxIcon.Hand);
				return;
			}

			if (List_SelectedUserToBedAdded == null)
				List_SelectedUserToBedAdded = new List<User_cu>();

			if (List_SelectedUserToBedAdded.Count > 0)
				if (List_SelectedUserToBedAdded.Exists(
						item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(SelectedUserFromGrid.ID))))
				{
					XtraMessageBox.Show("تمــت إضافتـــه مـن قبـــل", "تنبيــــــــــه", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					return;
				}

			List_SelectedUserToBedAdded.Add(SelectedUserFromGrid);
			CommonViewsActions.FillListBoxControl(lst_Users, List_SelectedUserToBedAdded, "FullName", "Person_CU_ID");
			lst_Users.Refresh();

			User_Application_cu userBridge = new User_Application_cu();
			userBridge.User_CU_ID = Convert.ToInt32(SelectedUserFromGrid.ID);
			userBridge.Application_P_ID = Convert.ToInt32(lkeApplications.EditValue);
			if (List_User_Application == null)
				List_User_Application = new List<User_Application_cu>();
			List_User_Application.Add(userBridge);
		}

		private void btnRemoveFromList_Click(object sender, EventArgs e)
		{
			if (lst_Users.SelectedItems.Count == 0 || List_User_Application == null)
				return;

			User_cu selecteduser = (User_cu)lst_Users.SelectedItem;
			if (selecteduser == null)
				return;
			if (List_SelectedUserToBedAdded.Exists(
				item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(selecteduser.ID))))
				List_SelectedUserToBedAdded.Remove(selecteduser);

			CommonViewsActions.FillListBoxControl(lst_Users, List_SelectedUserToBedAdded, "FullName", "Person_CU_ID");
			lst_Users.Refresh();

			User_Application_cu userBridge =
				List_User_Application.Find(
					item => Convert.ToInt32(item.User_CU_ID).Equals(Convert.ToInt32(selecteduser.ID)));
			if (userBridge == null)
				return;
			List_User_Application.Remove(userBridge);
		}

		private void lkeApplications_EditValueChanged(object sender, EventArgs e)
		{
			
		}

		private void gridView1_MouseUp(object sender, MouseEventArgs e)
		{
			SelectedUserFromGrid = CommonViewsActions.GetSelectedRowObject<User_cu>((GridView)sender);
		}

		#region Overrides of CommonAbstractViewer<User_Application_cu>

		public override IMVCController<User_Application_cu> ViewerCollector { get; set; }

		public override object ViewerID
		{
			get { return (int)ViewerName.User_Application_Viewer; }
		}

		public override string HeaderTitle
		{
			get { return "ربـــط المستخـدميـــن بالبـرامـــج"; }
		}

		#endregion

		#region Implementation of IUser_Application_Viewer

		public List<User_Application_cu> List_User_Application { get; set; }

		#endregion
	}
}
