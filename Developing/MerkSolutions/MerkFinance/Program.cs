﻿using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using ApplicationConfiguration;
using CommonUserControls.CommonViewers;
using DevExpress.LookAndFeel;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;

namespace MerkFinance
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

			DialogResult result = DialogResult.None;
			ApplicationStaticConfiguration.Application = DB_Application.MerkFinance;
			if (ApplicationStaticConfiguration.LoadApplicationConfiguration())
			{
				DBBusinessLogicLibrary.LoadDBItemsList();
				result = Login_UC.ShowLoginScreen();
			}

			switch (result)
			{
				case DialogResult.OK:
					UserLookAndFeel.Default.SetSkinStyle(ApplicationStaticConfiguration.SkinName);
					if (ApplicationStaticConfiguration.SkinColor != null)
						UserLookAndFeel.Default.SkinMaskColor = Color.FromArgb(
							((Color)ApplicationStaticConfiguration.SkinColor).R,
							((Color)ApplicationStaticConfiguration.SkinColor).G,
							((Color)ApplicationStaticConfiguration.SkinColor).B);
					Application.Run(new MainForm());
					break;
				case DialogResult.Cancel:
					Process.GetCurrentProcess().Kill();
					break;
			}
		}
	}
}
