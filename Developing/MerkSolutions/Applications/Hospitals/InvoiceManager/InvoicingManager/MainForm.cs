using System;
using CommonControlLibrary;
using CommonUserControls.InvoiceViewers;
using DevExpress.XtraEditors;


namespace InvoicingManager
{
	public partial class MainForm : XtraForm
	{
		private InvoiceManagerQueueContainerWithHeaderIcons_UC _invoiceManagerQueueContainer;
		public MainForm()
		{
			InitializeComponent();
			CommonViewsActions.SetupSyle(this);

			CommonViewsActions.ShowUserControl(ref _invoiceManagerQueueContainer, this);
		}

		private void MainForm_Load(object sender, EventArgs e)
		{

		}

	}
}