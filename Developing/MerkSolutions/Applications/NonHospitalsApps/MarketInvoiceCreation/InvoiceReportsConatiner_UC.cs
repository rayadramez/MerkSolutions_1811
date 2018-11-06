using System;
using System.Windows.Forms;
using CommonUserControls.ReportsContainer;
using MerkDataBaseBusinessLogicProject;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MVCBusinessLogicLibrary.BaseViewers;
using MVCBusinessLogicLibrary.MVCFactories;

namespace MarketInvoiceCreation
{
	public partial class InvoiceReportsConatiner_UC : UserControl
	{
		private CustomerFinanceInvoices_Report _customerFinanceInvoicesReport;

		public InvoiceReportsConatiner_UC()
		{
			InitializeComponent();
		}

		private void btnCustomerInvoicesReport_Click(object sender, EventArgs e)
		{
			BaseController<GetCustomerInvoices_Result>.ShowSearchControl(ref _customerFinanceInvoicesReport, this,
				ViewerName.CustomerFinanceInvoicesReport_Viewer, DB_CommonTransactionType.SearchReport,
				"تقـريـــر فـواتيـــر العمـــلاء", true, true);
		}
	}
}
