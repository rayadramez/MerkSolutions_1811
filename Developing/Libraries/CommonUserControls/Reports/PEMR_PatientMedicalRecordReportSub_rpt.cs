using System.Collections.Generic;
using DevExpress.XtraReports.UI;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;

namespace CommonUserControls.Reports
{
	public partial class PEMR_PatientMedicalRecordReportSub_rpt : DevExpress.XtraReports.UI.XtraReport
	{
		public PEMR_PatientMedicalRecordReportSub_rpt()
		{
			InitializeComponent();
			lblElementHeaderTitle.EvaluateBinding += ElementHeaderTitle_EvaluateBinding;
		}

		public void Initialize(List<PEMR_Translated> translatedList)
		{
			if (translatedList == null || translatedList.Count == 0)
				return;

			DetailReport.DataSource = translatedList;
			lblElementHeaderTitle.DataBindings.Add("Tag", translatedList, "Handle");
			lblElementHeaderTitle.DataBindings.Add("Text", translatedList, "ElementName");
		}

		private void ElementHeaderTitle_EvaluateBinding(object sender, BindingEventArgs e)
		{
			XRLabel mainServiceTitle = sender as XRLabel;
			if (mainServiceTitle == null || !(e.Value is PEMR_Translated))
				return;

			PEMR_PatientMedicalRecordReportElement_rpt elementReport = new PEMR_PatientMedicalRecordReportElement_rpt();
			PEMR_PatientMedicalRecordReportElement_VisionRefraction_rpt elementReport_VisionRefraction =
				new PEMR_PatientMedicalRecordReportElement_VisionRefraction_rpt();
			PEMR_Translated translated = e.Value as PEMR_Translated;
			elementReport.Initialize(translated.List_PEMR_Element_Translated);
			elementSubReport.ReportSource = elementReport;
			//if (translated.List_PEMR_Element_Translated != null && translated.List_PEMR_Element_Translated.Count > 0)
			//{
			//	if (!translated.IsEyeRelatedType)
			//	{
			//		elementReport.Initialize(translated.List_PEMR_Element_Translated);
			//		elementSubReport.ReportSource = elementReport;
			//	}
			//	else
			//	{
			//		elementReport_VisionRefraction.Initialize(translated.List_PEMR_Element_Translated);
			//		elementSubReport.ReportSource = elementReport_VisionRefraction;
			//	}
			//}
		}
	}
}
