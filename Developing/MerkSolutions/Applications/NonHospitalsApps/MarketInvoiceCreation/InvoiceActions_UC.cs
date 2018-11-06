using System.Windows.Forms;
using CommonUserControls.InvoiceViewers;
using CommonUserControls.InvoiceViewers.MarketInvoice;
using MerkDataBaseBusinessLogicProject;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MVCBusinessLogicLibrary.BaseViewers;
using MVCBusinessLogicLibrary.MVCFactories;

namespace MarketInvoiceCreation
{
	public partial class InvoiceActions_UC : UserControl
	{
		private MarketCreationFinanceInvoice_UC _marketInvoiceCreation;
		private InvoicePayment_UC _invoicePayment;

		public InvoiceActions_UC()
		{
			InitializeComponent();
		}

		private void btnPurchasingInvoice_Click(object sender, System.EventArgs e)
		{
			BaseController<FinanceInvoice>.ShowEditorControl(ref _marketInvoiceCreation, this, DB_InvoiceType.PurchasingInvoice,
				null, EditorContainerType.Regular, ViewerName.FinanceInvoiceCreation_Viewer, DB_CommonTransactionType.CreateNew,
				"فـاتــــــورة المشتـريــــــات", true);
		}

		private void btnSellingInvoice_Click(object sender, System.EventArgs e)
		{
			BaseController<FinanceInvoice>.ShowEditorControl(ref _marketInvoiceCreation, this, DB_InvoiceType.SellingInvoice,
				null, EditorContainerType.Regular, ViewerName.FinanceInvoiceCreation_Viewer, DB_CommonTransactionType.CreateNew,
				"فـاتــــــورة المبيعــــــات", true);
		}

		private void btnReturningPurchasingInvoice_Click(object sender, System.EventArgs e)
		{
			BaseController<FinanceInvoice>.ShowEditorControl(ref _marketInvoiceCreation, this,
				DB_InvoiceType.ReturningPurchasingInvoice, null, EditorContainerType.Regular,
				ViewerName.FinanceInvoiceCreation_Viewer, DB_CommonTransactionType.CreateNew, "فـاتــــــورة مــــردودات المشتـريــــــات", true);
		}

		private void btnReturningSellingInvoice_Click(object sender, System.EventArgs e)
		{
			BaseController<FinanceInvoice>.ShowEditorControl(ref _marketInvoiceCreation, this, DB_InvoiceType.ReturningSellingInvoice,
				null, EditorContainerType.Regular, ViewerName.FinanceInvoiceCreation_Viewer, DB_CommonTransactionType.CreateNew,
				"فـاتــــــورة مــــردودات المبيعــــــات", true);
		}
	}
}
