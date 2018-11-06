using CommonControlLibrary;
using DevExpress.XtraReports.UI;

namespace CommonUserControls.Reports
{
	public partial class PEMR_HeaderAndFooterTemplate_A4_rpt : DevExpress.XtraReports.UI.XtraReport
	{
		public PEMR_HeaderAndFooterTemplate_A4_rpt()
		{
			InitializeComponent();
		}

		public static PEMR_HeaderAndFooterTemplate_A4_rpt Initialize(XtraReport detailReport, bool showRightsReserved)
		{
			PEMR_HeaderAndFooterTemplate_A4_rpt reprotTemplate = new PEMR_HeaderAndFooterTemplate_A4_rpt();
			reprotTemplate.Initialize(detailReport);
			reprotTemplate.lblRightsAreReserved.Visible = showRightsReserved;
			return reprotTemplate;
		}

		private void Initialize(XtraReport detailReport)
		{
			xrSubreport2.ReportSource = detailReport;
			reportHeader.LoadFile(FileManager.GetReportTemplateFullPath(ReportTemplateType.Header_A4));
		}
	}
}
