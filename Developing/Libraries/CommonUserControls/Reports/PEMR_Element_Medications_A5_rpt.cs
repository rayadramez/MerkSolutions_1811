using System.Collections.Generic;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;

namespace CommonUserControls.Reports
{
	public partial class PEMR_Element_Medications_A5_rpt : DevExpress.XtraReports.UI.XtraReport
	{
		public PEMR_Element_Medications_A5_rpt()
		{
			InitializeComponent();
		}

		public void Initialize(List<PEMR_Translated> translatedList)
		{
			if (translatedList == null || translatedList.Count == 0)
				return;

			DetailReport.Visible = true;
			DetailReport.DataSource = translatedList;
			lblElementValue.DataBindings.Add("Text", translatedList, PEMR_Translated.MedicationNameValueField);
			lblDosage.DataBindings.Add("Text", translatedList, PEMR_Translated.MedicationDosageNameValueField);
			lblReccommendations.DataBindings.Add("Text", translatedList,
				PEMR_Translated.MedicationReccommendationsNameValueField);
		}
	}
}
