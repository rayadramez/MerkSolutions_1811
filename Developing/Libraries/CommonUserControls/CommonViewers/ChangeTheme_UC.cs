using System;
using System.Drawing;
using System.Windows.Forms;
using ApplicationConfiguration;
using CommonControlLibrary;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;

namespace CommonUserControls.CommonViewers
{
	public partial class ChangeTheme_UC : UserControl
	{
		public ChangeTheme_UC()
		{
			InitializeComponent();

			CommonViewsActions.LoadXMLFromString(layoutControl1, Resources.LocalizedRes.lyt_ChangeTheme);
			CommonViewsActions.SetupSyle(this);

			if(ApplicationStaticConfiguration.SkinName != null)
				if (ApplicationStaticConfiguration.SkinName == "McSkin")
				{
					chkMcSkin.Checked = true;
					BackColor = Color.White;
				}
				else if (ApplicationStaticConfiguration.SkinName == "Office 2010 Black")
				{
					chkOffice2010Black.Checked = true;
					BackColor = Color.LightSlateGray;
				}
				else if (ApplicationStaticConfiguration.SkinName == "Office 2010 Blue")
				{
					chkOffice2010Blue.Checked = true;
					BackColor = Color.LightSlateGray;
				}
				else if (ApplicationStaticConfiguration.SkinName == "Office 2010 Silver")
				{
					chkOffice2010Silver.Checked = true;
					BackColor = Color.LightSlateGray;
				}
			if (ApplicationStaticConfiguration.SkinColor != null)
				colorPickEdit1.EditValue = ApplicationStaticConfiguration.SkinColor;
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			if(ParentForm != null)
				ParentForm.Close();
		}

		private void btnSaveAndClose_Click(object sender, EventArgs e)
		{
			if (chkMcSkin.Checked)
				ApplicationStaticConfiguration.OrganizationMachine.SkinName = "McSkin";
			if (chkOffice2010Black.Checked)
				ApplicationStaticConfiguration.OrganizationMachine.SkinName = "Office 2010 Black";
			if (chkOffice2010Blue.Checked)
				ApplicationStaticConfiguration.OrganizationMachine.SkinName = "Office 2010 Blue";
			if (chkOffice2010Silver.Checked)
				ApplicationStaticConfiguration.OrganizationMachine.SkinName = "Office 2010 Silver";

			if (colorPickEdit1.EditValue == null || (colorPickEdit1.Color.R.Equals(0) &&
			                                         colorPickEdit1.Color.B.Equals(0) &&
			                                         colorPickEdit1.Color.G.Equals(0)))
				ApplicationStaticConfiguration.OrganizationMachine.Color = null;
			else
				ApplicationStaticConfiguration.OrganizationMachine.Color =
					colorPickEdit1.Color.R + "," + colorPickEdit1.Color.G + "," + colorPickEdit1.Color.B;

			ApplicationStaticConfiguration.OrganizationMachine.DBCommonTransactionType =
				DB_CommonTransactionType.UpdateExisting;

			if (ApplicationStaticConfiguration.OrganizationMachine.SaveChanges())
			{
				XtraMessageBox.Show(
					"Saved Successfully ..." + "\r\n\r\n" + "Changes will be applied after restart the application",
					"Notice", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1,
					DefaultBoolean.Default);
			}

			btnClose_Click(null, null);

			ApplicationStaticConfiguration.SkinName = ApplicationStaticConfiguration.OrganizationMachine.SkinName;
			ApplicationStaticConfiguration.SkinColor = ApplicationStaticConfiguration.OrganizationMachine.Color;
		}

		private void colorPickEdit1_EditValueChanged(object sender, EventArgs e)
		{

		}
	}
}
