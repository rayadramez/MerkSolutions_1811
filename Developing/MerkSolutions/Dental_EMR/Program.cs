using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using ApplicationConfiguration;
using CommonUserControls.CommonViewers;
using CommonUserControls.PEMRCommonViewers;
using DevExpress.LookAndFeel;
using DevExpress.Skins;
using DevExpress.UserSkins;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;

namespace Dental_EMR
{
	static class Program
	{
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			BonusSkins.Register();
			SkinManager.EnableFormSkins();

			DialogResult result = DialogResult.None;
			Choose_PEMR_VS_Surgery choose = new Choose_PEMR_VS_Surgery();
			result = choose.ShowDialog();
			switch (result)
			{
				case DialogResult.Cancel:
					if (ApplicationStaticConfiguration.LoadApplicationConfiguration())
					{
						DBBusinessLogicLibrary.LoadDBItemsList();
						result = Login_UC.ShowLoginScreen();
					}
					break;
			}

			switch (result)
			{
				case DialogResult.OK:
					MerkDBBusinessLogicEngine.Private_StationPoint = ApplicationStaticConfiguration.Station;
					UserLookAndFeel.Default.SetSkinStyle(ApplicationStaticConfiguration.SkinName);
					if (ApplicationStaticConfiguration.SkinColor != null)
						UserLookAndFeel.Default.SkinMaskColor = Color.FromArgb(
							((Color) ApplicationStaticConfiguration.SkinColor).R,
							((Color) ApplicationStaticConfiguration.SkinColor).G,
							((Color) ApplicationStaticConfiguration.SkinColor).B);
					Application.Run(new Form1());
					break;
				case DialogResult.Cancel:
					Process.GetCurrentProcess().Kill();
					break;
			}
		}
	}
}
