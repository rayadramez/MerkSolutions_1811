using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using ApplicationConfiguration;
using CommonUserControls.CommonViewers;
using DevExpress.LookAndFeel;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;

namespace QueueManager
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

			DevExpress.Skins.SkinManager.EnableFormSkins();
			DevExpress.UserSkins.BonusSkins.Register();

			UserLookAndFeel.Default.SetSkinStyle("Office 2010 Black");
			UserLookAndFeel.Default.SkinMaskColor = Color.FromArgb(50, 59, 74);

			DialogResult result = DialogResult.None;

			ApplicationStaticConfiguration.Application = DB_Application.ClinicReception;
			if (ApplicationStaticConfiguration.LoadApplicationConfiguration())
			{
				DBBusinessLogicLibrary.LoadDBItemsList();
				result = Login_UC.ShowLoginScreen();
			}

			switch (result)
			{
				case DialogResult.OK:
					Application.Run(new QueueManagerMainForm());
					break;
				case DialogResult.Cancel:
					Process.GetCurrentProcess().Kill();
					break;
			}
		}
	}
}
