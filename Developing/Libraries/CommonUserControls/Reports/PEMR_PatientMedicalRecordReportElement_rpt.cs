using System.Collections.Generic;
using System.Linq;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;

namespace CommonUserControls.Reports
{
	public partial class PEMR_PatientMedicalRecordReportElement_rpt : DevExpress.XtraReports.UI.XtraReport
	{
		public PEMR_PatientMedicalRecordReportElement_rpt()
		{
			InitializeComponent();
		}

		public void Initialize(List<PEMR_Translated> translatedList)
		{
			if (translatedList == null || translatedList.Count == 0)
				return;

			DetailReport_Investigation.Visible = true;
			DetailReport_Investigation.DataSource = translatedList;
			lblElement_Investigation.DataBindings.Add("Text", translatedList, PEMR_Translated.ElementNameField);
			lblElementValue__Investigation.DataBindings.Add("Text", translatedList, PEMR_Translated.TranslatedItemValueField);
		}
	}
}
