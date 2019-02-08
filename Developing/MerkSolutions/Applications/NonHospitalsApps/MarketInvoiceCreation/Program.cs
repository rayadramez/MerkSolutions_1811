using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using ApplicationConfiguration;
using CommonUserControls.CommonViewers;
using DevExpress.UserSkins;
using DevExpress.Skins;
using DevExpress.LookAndFeel;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;

namespace MarketInvoiceCreation
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			BonusSkins.Register();
			SkinManager.EnableFormSkins();
			UserLookAndFeel.Default.SetSkinStyle("Office 2010 Black");
			UserLookAndFeel.Default.SkinMaskColor = Color.FromArgb(50, 59, 74);

			DialogResult result = DialogResult.None;
			ApplicationStaticConfiguration.Application = DB_Application.AllReception;
			ApplicationStaticConfiguration.Organization = DB_Organization.AvvaAbraam;

			if (ApplicationStaticConfiguration.LoadApplicationConfiguration())
			{
				DBBusinessLogicLibrary.LoadDBItemsList();
				result = Login_UC.ShowLoginScreen();
			}

			switch (result)
			{
				case DialogResult.OK:
					DBBusinessLogicLibrary.LoadDBItemsList();
					Application.Run(new MainForm());
					break;
				case DialogResult.Cancel:
					Process.GetCurrentProcess().Kill();
					break;
			}
		}
	}
}
