using System.Collections.Generic;
using DevExpress.XtraReports.UI;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;

namespace CommonUserControls.Reports
{
	public partial class PEMR_PatientMedicalRecordReportContainer_rpt : DevExpress.XtraReports.UI.XtraReport
	{
		public PEMRObject ActivePEMRObject { get; set; }

		public PEMR_PatientMedicalRecordReportContainer_rpt()
		{
			InitializeComponent();
		}

		public void Initialize(PEMRObject pemrObject)
		{
			ActivePEMRObject = pemrObject;
			if (ActivePEMRObject == null)
				return;
			lblElementHeaderTitle.EvaluateBinding += ElementHeaderTitle_EvaluateBinding;
			lblPatientID.Text = ActivePEMRObject.Active_Patient.ID.ToString();
			lblPatientName.Text = ActivePEMRObject.Active_Patient.PatientFullName;
			lblDoctorName.Text = ActivePEMRObject.Active_InvoiceDetail.DoctorName;
			lblServiceName.Text = ActivePEMRObject.Active_InvoiceDetail.ServiceName;
			lblServiceCategoryName.Text = ActivePEMRObject.Active_InvoiceDetail.ServiceCategoryName;
			lblDate.Text = ActivePEMRObject.Active_InvoiceDetail.Date.ToShortDateString() + " - "
						   + ActivePEMRObject.Active_InvoiceDetail.Date.ToShortTimeString();

			List<PEMR_Translated> list = PEMRBusinessLogic.Translate_PEMR_Report(ActivePEMRObject);
			DetailReport.DataSource = list;
			lblElementHeaderTitle.DataBindings.Add("Tag", list, "Handle");
			lblElementHeaderTitle.DataBindings.Add("Text", list, "ElementName");
		}

		private void ElementHeaderTitle_EvaluateBinding(object sender, BindingEventArgs e)
		{
			PEMR_Translated mainTranslated = e.Value as PEMR_Translated;
			if (mainTranslated == null)
				return;
			mainTranslated.ElementName = "Patient Electronic Medical Record";
			PEMR_PatientMedicalRecordReportSub_rpt subReport = new PEMR_PatientMedicalRecordReportSub_rpt();
			subReport.Initialize((e.Value as PEMR_Translated).List_PEMR_Element_Translated);
			elementSubReport.ReportSource = subReport;
		}
	}
}
