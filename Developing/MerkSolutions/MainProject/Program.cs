using System;
using System.Windows.Forms;
using CommonControlLibrary;
using DevExpress.LookAndFeel;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;

namespace MainProject
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
			UserLookAndFeel.Default.SetSkinStyle("Metropolis");
			CommonViewsActions.SetCulture(CommonViewsActions.ArabicCulture);

			DBBusinessLogicLibrary.LoadDBItemsList();

			Application.Run(new MainForm());
		}
	}
}