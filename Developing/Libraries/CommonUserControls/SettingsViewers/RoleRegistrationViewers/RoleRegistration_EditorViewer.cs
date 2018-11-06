using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CommonControlLibrary;
using DevExpress.XtraEditors;
using DevExpress.XtraLayout.Utils;
using MerkDataBaseBusinessLogicProject;
using MVCBusinessLogicLibrary.BaseViewers;
using MVCBusinessLogicLibrary.Controller;
using MVCBusinessLogicLibrary.MVCFactories;
using MVCBusinessLogicLibrary.Viewers;

namespace CommonUserControls.SettingsViewers.RoleRegistrationViewers
{
	public partial class RoleRegistration_EditorViewer :
		//DevExpress.XtraEditors.XtraUserControl
		CommonAbstractEditorViewer<RoleRegistration_cu>,
		IRoleRegistrationViewer
	{
		private Role_p SelectedRoleFromGrid = null;
		private List<Role_p> List_SelectedRoleToBedAdded = null;

		public RoleRegistration_EditorViewer()
		{
			InitializeComponent();
			CommonViewsActions.LoadXMLFromString(layoutControl1, Resources.LocalizedRes.lyt_RoleRegistration_EditorViewer);
			CommonViewsActions.SetupGridControl(grdRoles, Resources.LocalizedRes.grd_RoleRegistration_Internal, true);
			CommonViewsActions.SetupSyle(this);
		}

		public override void FillControls()
		{
			grdRoles.DataSource = Role_p.ItemsList;
			CommonViewsActions.FillGridlookupEdit(lkeUserGroup, UserGroup_cu.ItemsList);
			CommonViewsActions.FillGridlookupEdit(lkeUser, User_cu.ItemsList, "FullName", "Person_CU_ID");
			CommonViewsActions.FillGridlookupEdit(lkeApplications, Application_p.ItemsList);
			
			chkUserGroup.Checked = true;
			lytUserGroup.Visibility = chkUserGroup.Checked ? LayoutVisibility.Always : LayoutVisibility.Never;
			lytUser.Visibility = chkUser.Checked ? LayoutVisibility.Always : LayoutVisibility.Never;
		}

		public override void ClearControls()
		{

		}

		private void lkeApplications_EditValueChanged(object sender, System.EventArgs e)
		{
			if (lkeApplications.EditValue == null)
			{
				grdRoles.DataSource = Role_p.ItemsList;
				return;
			}

			List<Role_p> rolesList =
				Role_p.ItemsList.FindAll(
					item => Convert.ToInt32(item.Application_P_ID).Equals(Convert.ToInt32(lkeApplications.EditValue)));
			grdRoles.DataSource = rolesList;
		}

		private void lkeUserGroup_EditValueChanged(object sender, EventArgs e)
		{
			lkeUser.EditValue = null;
		}

		private void lkeUser_EditValueChanged(object sender, EventArgs e)
		{
			lkeUserGroup.EditValue = null;
		}

		private void chkUserGroup_CheckedChanged(object sender, EventArgs e)
		{
			lytUserGroup.Visibility = chkUserGroup.Checked ? LayoutVisibility.Always : LayoutVisibility.Never;
			lytUser.Visibility = chkUser.Checked ? LayoutVisibility.Always : LayoutVisibility.Never;
		}

		private void chkUser_CheckedChanged(object sender, EventArgs e)
		{
			lytUserGroup.Visibility = chkUserGroup.Checked ? LayoutVisibility.Always : LayoutVisibility.Never;
			lytUser.Visibility = chkUser.Checked ? LayoutVisibility.Always : LayoutVisibility.Never;
		}

		private void txtRoleName_EditValueChanged(object sender, EventArgs e)
		{
			List<Role_p> rolesList = null;

			if (lkeApplications.EditValue != null)
			{
				rolesList =
					Role_p.ItemsList.FindAll(
						item => Convert.ToInt32(item.Application_P_ID).Equals(Convert.ToInt32(lkeApplications.EditValue)));
			}
			else
				rolesList = Role_p.ItemsList;

			if (txtRoleName.EditValue == null || string.IsNullOrEmpty(txtRoleName.Text) ||
				string.IsNullOrWhiteSpace(txtRoleName.Text))
			{
				grdRoles.DataSource = rolesList;
				return;
			}

			if (rolesList != null)
				grdRoles.DataSource =
					rolesList.FindAll(
						item => item.Name_P != null && Convert.ToString(item.Name_P).Contains(Convert.ToString(txtRoleName.Text)));
			else
				grdRoles.DataSource =
					Role_p.ItemsList.FindAll(
						item => item.Name_P != null && Convert.ToString(item.Name_P).Contains(Convert.ToString(txtRoleName.Text)));
		}

		private void btnAddList_Click(object sender, EventArgs e)
		{
			if (lkeUserGroup.EditValue == null && lkeUser.EditValue == null)
			{
				XtraMessageBox.Show("يجب إختيــار مجمـوعــة المستخـدميـــن أو مستخـــدم", "تنبيــــــــــه", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return;
			}

			if (SelectedRoleFromGrid == null)
			{
				XtraMessageBox.Show("يجب إختيــار الوظيفــــــة", "تنبيــــــــــه", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return;
			}

			if (List_SelectedRoleToBedAdded == null)
				List_SelectedRoleToBedAdded = new List<Role_p>();

			if (List_SelectedRoleToBedAdded.Count > 0)
				if (List_SelectedRoleToBedAdded.Exists(
						item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(SelectedRoleFromGrid.ID))))
				{
					XtraMessageBox.Show("تمــت إضافتـــه مـن قبـــل", "تنبيــــــــــه", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					return;
				}

			List_SelectedRoleToBedAdded.Add(SelectedRoleFromGrid);
			CommonViewsActions.FillListBoxControl(lst_Roles, List_SelectedRoleToBedAdded);
			lst_Roles.Refresh();

			RoleRegistration_cu roleRegistration = new RoleRegistration_cu();
			roleRegistration.Role_P_ID = SelectedRoleFromGrid.ID;
			if (lkeUserGroup.EditValue != null)
				roleRegistration.UserGroup_CU_ID = Convert.ToInt32(lkeUserGroup.EditValue);
			if (lkeUser.EditValue != null)
				roleRegistration.User_CU_ID = Convert.ToInt32(lkeUser.EditValue);

			if (List_RoleRegistration == null)
				List_RoleRegistration = new List<RoleRegistration_cu>();
			List_RoleRegistration.Add(roleRegistration);
		}

		private void btnRemoveFromList_Click(object sender, EventArgs e)
		{

		}

		private void gridView1_GotFocus(object sender, EventArgs e)
		{
			SelectedRoleFromGrid = CommonViewsActions.GetSelectedRowObject<Role_p>(gridView1);
		}

		#region Overrides of CommonAbstractViewer<RoleRegistration_cu>

		public override IMVCController<RoleRegistration_cu> ViewerCollector { get; set; }

		public override object ViewerID
		{
			get { return (int)ViewerName.RoleRegistration_Viewer; }
		}

		public override string HeaderTitle
		{
			get { return "ربـــط مجمـوعـــات المستخـدميـــن بالوظائـــف"; }
		}

		#endregion

		#region Implementation of IRoleRegistrationViewer

		public List<RoleRegistration_cu> List_RoleRegistration { get; set; }

		#endregion
	}
}
