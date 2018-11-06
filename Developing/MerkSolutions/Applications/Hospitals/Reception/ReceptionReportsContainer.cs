using System;
using System.Drawing;
using System.Windows.Forms;
using ApplicationConfiguration;
using CommonUserControls.ReportsContainer;
using MerkDataBaseBusinessLogicProject;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MVCBusinessLogicLibrary.BaseViewers;
using MVCBusinessLogicLibrary.MVCFactories;

namespace Reception
{
	public partial class ReceptionReportsContainer : UserControl
	{
		private InvoicePaymentBrief_Report _invoicePaymentBriefReport;
		private TotalServiceAndDoctorRevenues_Report _totalServiceAndDoctorRevenuesReport;
		private PatientDepositBalance_Report _patientDepositBalanceReport;

		public ReceptionReportsContainer()
		{
			InitializeComponent();

			if (!string.IsNullOrEmpty(ApplicationStaticConfiguration.SkinName) &&
				!string.IsNullOrWhiteSpace(ApplicationStaticConfiguration.SkinName))
				if (ApplicationStaticConfiguration.SkinName.Equals("McSkin"))
				{
					btnRevenueReport.ForeColor = Color.Navy;
					btnServiceAndDoctorReport.ForeColor = Color.Navy;
				}
				else
				{
					btnRevenueReport.ForeColor = Color.White;
					btnServiceAndDoctorReport.ForeColor = Color.White;
				}
		}

		private void btnRevenueReport_Click(object sender, EventArgs e)
		{
			BaseController<GetInvoicesPaymentBriefReport_Result>.ShowSearchControl(ref _invoicePaymentBriefReport, this,
				ViewerName.InvoicePaymentBrief_Report_Viewer, DB_CommonTransactionType.SearchReport,
				".... تقــريــــــر الإيـــــرادات التفصيليــــــة .....", true, true);
		}

		private void btnServiceAndDoctorReport_Click(object sender, EventArgs e)
		{
			BaseController<GetTotalServiceAndDoctorRevenues_Result>.ShowSearchControl(ref _totalServiceAndDoctorRevenuesReport, this,
				ViewerName.TotalServiceAndDoctorRevenues_Report_Viewer, DB_CommonTransactionType.SearchReport,
				".... تقـريـــــــر إيــــــرادات الخــدمــــــات والأطبـــــاء .....", true, true);
		}

		private void btnPatientDepositBalanceReport_Click(object sender, EventArgs e)
		{
			BaseController<GetPatientDepositBalance_Result>.ShowSearchControl(ref _patientDepositBalanceReport, this,
				ViewerName.PatientDepositBalance_Report_Viewer, DB_CommonTransactionType.SearchReport,
				".... تقـريـــــــر إيــــــرادات تحـــت الحســـاب ....", true, true);
		}
	}
}
