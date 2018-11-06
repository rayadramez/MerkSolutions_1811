using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using ApplicationConfiguration;
using CommonUserControls.CommonViewers;
using DevExpress.LookAndFeel;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;

namespace InvoicingManager
{
	static class Program
	{
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			DevExpress.Skins.SkinManager.EnableFormSkins();
			DevExpress.UserSkins.BonusSkins.Register();

			UserLookAndFeel.Default.SetSkinStyle("Office 2010 Black");
			UserLookAndFeel.Default.SkinMaskColor = Color.FromArgb(100, 80, 70);

			DialogResult result = DialogResult.None;

			ApplicationStaticConfiguration.Application = DB_Application.InvoiceManager;
			if (ApplicationStaticConfiguration.LoadApplicationConfiguration())
			{
				DBBusinessLogicLibrary.LoadDBItemsList();
				result = Login_UC.ShowLoginScreen();
			}

			switch (result)
			{
				case DialogResult.OK:
					Application.Run(new MainForm());
					break;
				case DialogResult.Cancel:
					Process.GetCurrentProcess().Kill();
					break;
			}
		}
	}
}