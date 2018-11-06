using System.Windows.Forms;
using ApplicationConfiguration;
using CommonControlLibrary;
using DevExpress.XtraLayout.Utils;
using MerkDataBaseBusinessLogicProject;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;

namespace CommonUserControls.InvoiceViewers
{
	public partial class InvoiceManagerQueueContainerWithHeaderIcons_UC : 
		DevExpress.XtraEditors.XtraUserControl
	{
		private InvoiceManagerQeueCardContainer_UC _QueueCardContainer;
		private InvoiceContainer_UC _InvoiceContainer;
		public Invoice ActiveInvoice { get; set; }
		public Control ParentControl { get; set; }

		public void Initialize(Control parentControl)
		{
			ParentControl = parentControl;
		}

		public InvoiceManagerQueueContainerWithHeaderIcons_UC()
		{
			InitializeComponent();

			InvoiceManagerQeueCardContainer_UC.ParentControlWithHeaderIcon = this;
			CommonViewsActions.ShowUserControl(ref _QueueCardContainer, splitContainerControl1.Panel2);
			CommonViewsActions.SetupSyle(this);

			if(ApplicationStaticConfiguration.Application == DB_Application.ClinicReception)
				ShowHeaderControls(false);
		}

		public void ShowHeaderControls(bool showHeaderControls)
		{
			lytHeaderControls.Visibility = showHeaderControls ? LayoutVisibility.Always : LayoutVisibility.Never;
		}

		public void CollapseLeftPanel(bool doCollapse)
		{
			splitContainerControl1.Collapsed = doCollapse;
		}

		public void ShowInvoiceContainer(bool showInvoiceContainer)
		{
			if (showInvoiceContainer)
			{
				CommonViewsActions.ShowUserControl(ref _InvoiceContainer, splitContainerControl1.Panel1);
				if (_InvoiceContainer != null)
					_InvoiceContainer.Initialize(this, ActiveInvoice);
			}
			else
				CommonViewsActions.ShowUserControl(ref _QueueCardContainer, splitContainerControl1.Panel2, true);
		}

		public void PassInvoice(Invoice activeInvoice)
		{
			ActiveInvoice = activeInvoice;
		}
	}
}
