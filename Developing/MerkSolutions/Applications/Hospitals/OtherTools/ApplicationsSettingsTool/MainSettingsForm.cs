using System;
using System.Drawing;
using ApplicationConfiguration;
using CommonControlLibrary;
using DevExpress.XtraLayout.Utils;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;

namespace ApplicationsSettingsTool
{
	public partial class MainSettingsForm : DevExpress.XtraEditors.XtraForm
	{
		private GeneralSettingsContainer_UC _generalSettingsContainer;
		private MedicalSettingsContainer_UC _medicalSettingsContainer;
		private InventorySettingsContainer _inventorySettingsContainer;
		private AccountingSettingsContainer _accountingSettingsContainer;

		public MainSettingsForm()
		{
			InitializeComponent();

			switch (ApplicationStaticConfiguration.Organization)
			{
				case DB_Organization.AvvaAbraam:
					lytMedicalSettings.Visibility = LayoutVisibility.Never;
					break;
			}

			btnUserDropDown.Text = ApplicationStaticConfiguration.UserName;

			if (!string.IsNullOrEmpty(ApplicationStaticConfiguration.SkinName) &&
			    !string.IsNullOrWhiteSpace(ApplicationStaticConfiguration.SkinName))
				if (ApplicationStaticConfiguration.SkinName.Equals("McSkin"))
				{
					btnMedicalSettings.ForeColor = Color.Navy;
					btnInventorySettings.ForeColor = Color.Navy;
					btnAccountingSettings.ForeColor = Color.Navy;
					btnGeneralSettings.ForeColor = Color.Navy;
					btnUserDropDown.ForeColor = Color.Navy;
				}
				else
				{
					btnMedicalSettings.ForeColor = Color.White;
					btnInventorySettings.ForeColor = Color.White;
					btnAccountingSettings.ForeColor = Color.White;
					btnGeneralSettings.ForeColor = Color.White;
					btnUserDropDown.ForeColor = Color.White;
				}
		}

		private void btnGeneralSettings_Click(object sender, EventArgs e)
		{
			CommonViewsActions.ShowUserControl(ref _generalSettingsContainer, pnlMain);
		}

		private void btnMedicalSettings_Click(object sender, EventArgs e)
		{
			CommonViewsActions.ShowUserControl(ref _medicalSettingsContainer, pnlMain);
		}

		private void btnInventorySettings_Click(object sender, EventArgs e)
		{
			CommonViewsActions.ShowUserControl(ref _inventorySettingsContainer, pnlMain);
		}

		private void btnAccountingSettings_Click(object sender, EventArgs e)
		{
			CommonViewsActions.ShowUserControl(ref _accountingSettingsContainer, pnlMain);
		}

		private void btnChangeuser_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{

		}
	}
}