using System;
using System.Drawing;
using System.Windows.Forms;
using ApplicationConfiguration;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using MerkDataBaseBusinessLogicProject;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;

namespace CommonUserControls.CommonViewers
{
	public partial class Login_UC : DevExpress.XtraEditors.XtraUserControl
	{
		private DialogResult Result { get; set; }

		public Login_UC(DialogResult result)
		{
			InitializeComponent();
			Result = result;
			switch (ApplicationStaticConfiguration.Application)
			{
				case DB_Application.AllReception:
					lblApplicationTItle.Text = "All Receptions";
					break;
				case DB_Application.ClinicReception:
					lblApplicationTItle.Text = "Out Patient Reception";
					break;
				case DB_Application.AdmissionReception:
					lblApplicationTItle.Text = "In Patient Reception";
					break;
				case DB_Application.PEMR:
					lblApplicationTItle.Text = "Electronic Patient Medical Record";
					break;
				case DB_Application.InvoiceManager:
					lblApplicationTItle.Text = "Invoice Manager";
					break;
				case DB_Application.Settings:
					lblApplicationTItle.Text = "Applications Settings Manager";
					break;
				case DB_Application.FinanceInvoiceCreation:
					lblApplicationTItle.Text = "Merk Finance";
					break;
				case DB_Application.OphalmologySurgeryApplication:
					lblApplicationTItle.Text = "Oph. Surgery Room";
					break;
			}

			lblApplicationTItle.ForeColor = Color.FromArgb(41, 45, 56);
		}

		public static DialogResult ShowLoginScreen()
		{
			DialogResult result = DialogResult.None;
			Login_UC login = new Login_UC(result);
			login.Dock = DockStyle.Fill;
			XtraForm form = new XtraForm();
			form.FormBorderStyle = FormBorderStyle.None;
			form.ClientSize = login.ClientSize;
			form.StartPosition = FormStartPosition.CenterScreen;
			form.Controls.Add(login);
			result = form.ShowDialog();
			return result;
		}

		private void btnExit_Click(object sender, EventArgs e)
		{
			if (ParentForm != null)
			{
				ParentForm.DialogResult = DialogResult.Cancel;
				ParentForm.Close();
			}
		}

		private void btnLogin_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(txtUserName.Text) || string.IsNullOrWhiteSpace(txtUserName.Text))
			{
				XtraMessageBox.Show("يجب كتابة إسم الستخدم", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Error,
					MessageBoxDefaultButton.Button1, DefaultBoolean.Default);
				return;
			}

			if (string.IsNullOrEmpty(txtPassword.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
			{
				XtraMessageBox.Show("يجب كتابة كلمة السر", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Error,
					MessageBoxDefaultButton.Button1, DefaultBoolean.Default);
				return;
			}

			if (ApplicationStaticConfiguration.UserCanAccess(txtUserName.Text, txtPassword.Text))
			{
				Result = DialogResult.OK;
				OrganizationMachine_cu organizationmachine =
					ApplicationStaticConfiguration.GetOrganizationMachine((int)ApplicationStaticConfiguration.Organization);
				if (organizationmachine != null)
				{
					ApplicationStaticConfiguration.OrganizationMachine = organizationmachine;
					ApplicationStaticConfiguration.OrganizationMachineName = organizationmachine.Name_P;
					if (ApplicationStaticConfiguration.OrganizationMachine.SkinName != null)
						ApplicationStaticConfiguration.SkinName =
							ApplicationStaticConfiguration.OrganizationMachine.SkinName;
					else
						ApplicationStaticConfiguration.OrganizationMachine.SkinName = "Office 2010 Black";
					if (ApplicationStaticConfiguration.OrganizationMachine.Color != null)
						ApplicationStaticConfiguration.SkinColor =
							ApplicationStaticConfiguration.GetSkinColor(ApplicationStaticConfiguration
								.OrganizationMachine.Color);
					else
						ApplicationStaticConfiguration.SkinColor = null;

					//StationPoint_cu stationPoint = StationPoint_cu.ItemsList.Find(item =>
					//	Convert.ToInt32(item.ID).Equals(Convert.ToInt32(organizationmachine.StationPoint_CU_ID)));
					//if (stationPoint != null)
					//	ApplicationStaticConfiguration.StationPoint = (DB_Station) stationPoint.Station_P_ID;
					//else
					//{
					//	if (ParentForm != null)
					//	{
					//		ParentForm.DialogResult = DialogResult.Cancel;
					//		ParentForm.Close();
					//	}
					//}

					if (ParentForm != null)
					{
						ParentForm.DialogResult = DialogResult.OK;
						ParentForm.Close();
					}
				}
				else
					XtraMessageBox.Show("This machine has no record in Organization Machines List", "Note",
					                    MessageBoxButtons.OK,
					                    MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1, DefaultBoolean.Default);
				return;
			}

			XtraMessageBox.Show("User has no access to this application.", "Note", MessageBoxButtons.OK,
								MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1, DefaultBoolean.Default);

		}

		private void txtUserName_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == (char)Keys.Enter)
			{
				btnLogin_Click(null, null);
				return;
			}

			if (e.KeyChar == (char)Keys.Escape)
			{
				btnExit_Click(null, null);
				return;
			}
		}

		private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == (char)Keys.Enter)
			{
				btnLogin_Click(null, null);
				return;
			}

			if (e.KeyChar == (char)Keys.Escape)
			{
				btnExit_Click(null, null);
				return;
			}
		}

		private void txtUserName_Click(object sender, EventArgs e)
		{
			txtUserName.SelectAll();
		}

		private void txtPassword_Click(object sender, EventArgs e)
		{
			txtPassword.SelectAll();
		}
	}
}
