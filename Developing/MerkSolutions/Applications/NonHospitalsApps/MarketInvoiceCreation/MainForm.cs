using System;
using CommonControlLibrary;
using MerkDataBaseBusinessLogicProject;

namespace MarketInvoiceCreation
{
	public partial class MainForm : DevExpress.XtraEditors.XtraForm
	{
		private InvoiceActions_UC _invoiceActions;
		private InvoiceReportsConatiner_UC _invoiceReports;
		private CashBoxTransactionContainer _cashBoxTransactionContainer;
		private AccountingTransactionContainer_UC _accountingTransactionContainer;

		public MainForm()
		{
			InitializeComponent();

			FinancialInterval_cu financialInterval =
				FinancialInterval_cu.ItemsList.Find(item => Convert.ToDateTime(item.StartDate).Year.Equals(DateTime.Now.Year));
			if (financialInterval == null)
				return;
			btnFinancialInterval.Text = financialInterval.Name_P;

			CommonViewsActions.SetupSyle(this);
		}

		private void btnInvoiceActions_Click(object sender, EventArgs e)
		{
			CommonViewsActions.ShowUserControl(ref _invoiceActions, pnlMain);
		}

		private void btnInvoiceReport_Click(object sender, EventArgs e)
		{
			CommonViewsActions.ShowUserControl(ref _invoiceReports, pnlMain);
		}

		private void btnCashBoxTransactionConatiner_Click(object sender, EventArgs e)
		{
			CommonViewsActions.ShowUserControl(ref _cashBoxTransactionContainer, pnlMain);
		}

		private void btnAccountingTransactions_Click(object sender, EventArgs e)
		{
			CommonViewsActions.ShowUserControl(ref _accountingTransactionContainer, pnlMain);
		}
	}
}