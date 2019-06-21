using System.Collections.Generic;
using DevExpress.XtraReports.UI;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;

namespace CommonUserControls.Reports
{
	public partial class PEMR_ElementContainer_A5_rpt : DevExpress.XtraReports.UI.XtraReport
	{
		private DB_PEMR_ElementType PEMR_Element { get; set; }

		public PEMR_ElementContainer_A5_rpt()
		{
			InitializeComponent();
			lblElementHeaderTitle.EvaluateBinding += ElementHeaderTitle_EvaluateBinding;
		}

		public void Initialize(List<PEMR_Translated> translatedList, DB_PEMR_ElementType pemrElement)
		{
			if (translatedList == null || translatedList.Count == 0)
				return;

			DetailReport.DataSource = translatedList;
			lblElementHeaderTitle.DataBindings.Add("Tag", translatedList, "Handle");
			lblElementHeaderTitle.DataBindings.Add("Text", translatedList, "ElementName");
			PEMR_Element = pemrElement;
			lblDoctorName.Text = PEMRBusinessLogic.ActivePEMRObject.Active_InvoiceDetail.DoctorName;
			lblDate.Text = PEMRBusinessLogic.ActivePEMRObject.Active_InvoiceDetail.Date.ConvertDateTimeToString(false, true, false);
			lblPatientID.Text = lblDoctorName.Text = PEMRBusinessLogic.ActivePEMRObject.Active_Patient.ID.ToString();
			lblPatientName.Text = lblDoctorName.Text = PEMRBusinessLogic.ActivePEMRObject.Active_Patient.PatientFullName;
		}

		private void ElementHeaderTitle_EvaluateBinding(object sender, BindingEventArgs e)
		{
			XRLabel mainServiceTitle = sender as XRLabel;
			if (mainServiceTitle == null || !(e.Value is PEMR_Translated))
				return;

			switch (PEMR_Element)
			{
				case DB_PEMR_ElementType.VisitTiming_Medications:
					PEMR_Element_Medications_A5_rpt elementReport = new PEMR_Element_Medications_A5_rpt();
					PEMR_Translated translated = e.Value as PEMR_Translated;
					if (translated.List_PEMR_Element_Translated != null &&
					    translated.List_PEMR_Element_Translated.Count > 0)
					{
						elementReport.Initialize(translated.List_PEMR_Element_Translated);
						elementSubReport.ReportSource = elementReport;
					}

					break;
			}
		}
	}
}
