using System;
using System.Collections.Generic;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;

namespace CommonUserControls.Reports
{
	public partial class PEMR_PatientMedicalRecordReportElement_VisionRefraction_rpt : DevExpress.XtraReports.UI.XtraReport
	{
		public PEMR_PatientMedicalRecordReportElement_VisionRefraction_rpt()
		{
			InitializeComponent();
		}

		public void Initialize(List<PEMR_Translated> translatedList)
		{
			if (translatedList == null || translatedList.Count == 0)
				return;

			DetailReport_Main.Visible = true;
			DetailReport_Main.DataSource = translatedList;
			List<PEMR_Translated> list_OD = new List<PEMR_Translated>();
			List<PEMR_Translated> list_OU = new List<PEMR_Translated>();
			List<PEMR_Translated> list_OS = new List<PEMR_Translated>();
			foreach (PEMR_Translated translated in translatedList)
			{
				if(Convert.ToInt32(translated.EyeType).Equals(Convert.ToInt32(DB_EyeType_p.OD)))
					list_OD.Add(translated);
				if (Convert.ToInt32(translated.EyeType).Equals(Convert.ToInt32(DB_EyeType_p.OU)))
					list_OU.Add(translated);
				if (Convert.ToInt32(translated.EyeType).Equals(Convert.ToInt32(DB_EyeType_p.OS)))
					list_OS.Add(translated);
			}

			lbl_OU_ElementValue.DataBindings.Add("Text", list_OU, PEMR_Translated.TranslatedItemValueField);
			lbl_OD_ElementValue.DataBindings.Add("Text", list_OD, PEMR_Translated.TranslatedItemValueField);
			lbl_OS_ElementValue.DataBindings.Add("Text", list_OS, PEMR_Translated.TranslatedItemValueField);
			lbl_Element.DataBindings.Add("Text", list_OS, PEMR_Translated.ElementNameField);
		}
	}
}
