using CommonUserControls.PEMRCommonViewers.PEMR_Interfaces;
using CommonUserControls.Reports;
using DevExpress.XtraReports.UI;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MVCBusinessLogicLibrary.BaseViewers;

namespace CommonUserControls.PEMRCommonViewers.PEMR_InternalViewers
{
	public partial class PEMR_MedicalRecordContainer : DevExpress.XtraEditors.XtraUserControl, IPEMR_Viewer
	{
		private PEMR_PatientMedicalRecordReportContainer_rpt ActiveReport { get; set; }
		private PEMR_HeaderAndFooterTemplate_A4_rpt activeHeaderAndFooterTemplateA4Rpt { get; set; }

		public PEMR_MedicalRecordContainer()
		{
			InitializeComponent();
		}

		public void ClearControls(bool clearAll)
		{
			ActiveReport = null;
			documentViewer1.DocumentSource = null;
		}

		public void FillControls()
		{

		}

		public void Initialize(PEMR_HeaderAndFooterTemplate_A4_rpt reportParentTemplate,
			PEMR_PatientMedicalRecordReportContainer_rpt report)
		{
			ActiveReport = report;
			activeHeaderAndFooterTemplateA4Rpt = reportParentTemplate;
			ActiveReport.CreateDocument();
			reportParentTemplate.CreateDocument();
			ActiveReport.Initialize(PEMRBusinessLogic.ActivePEMRObject);
			documentViewer1.DocumentSource = reportParentTemplate;
		}

		private void btnPrintOrder_Click(object sender, System.EventArgs e)
		{
			PEMR_PrintOrder_UC printOrder = new PEMR_PrintOrder_UC();
			PopupBaseForm.ShowAsPopup(printOrder, this);
			ActiveReport.CreateDocument();
			ActiveReport.Initialize(PEMRBusinessLogic.ActivePEMRObject);
			documentViewer1.DocumentSource = ActiveReport;
		}

		private void btnPrint_Click(object sender, System.EventArgs e)
		{
			using (ReportPrintTool reprotTool = new ReportPrintTool(activeHeaderAndFooterTemplateA4Rpt))
				reprotTool.PrintDialog();
		}
	}
}
