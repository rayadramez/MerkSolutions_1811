using System;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.LookAndFeel;

namespace Dental_PEMR
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
			UserLookAndFeel.Default.SkinMaskColor = Color.FromArgb(100, 100, 110);

			Application.Run(new Form1());
		}
	}
}