using ApplicationConfiguration;
using CommonControlLibrary;
using CommonUserControls.CommonViewers;
using CommonUserControls.InvoiceViewers;
using MVCBusinessLogicLibrary.BaseViewers;

namespace Reception
{
	public partial class ReceptionMainForm : DevExpress.XtraEditors.XtraForm
	{
		private PatientListContainer_UC _patientsListUc;
		private ReceptionReportsContainer _receptionReportsContainer;
		private InvoiceManagerQueueContainerWithHeaderIcons_UC _invoiceManagerQueueContainer;

		public ReceptionMainForm()
		{
			InitializeComponent();
			BringToFront();

			CommonViewsActions.SetupSyle(this);
			btnUserDropDown.Text = ApplicationStaticConfiguration.UserName;
			CommonViewsActions.ShowUserControl(ref _patientsListUc, this);
			if (_patientsListUc != null)
				_patientsListUc.Initialize(this);
		}

		private void btnNewPatient_Click(object sender, System.EventArgs e)
		{
			CommonViewsActions.ShowUserControl(ref _patientsListUc, this);
			if(_patientsListUc != null)
				_patientsListUc.Initialize(this);
		}

		private void btnReports_Click(object sender, System.EventArgs e)
		{
			CommonViewsActions.ShowUserControl(ref _receptionReportsContainer, this);
		}

		private void btnInvoiceManager_Click(object sender, System.EventArgs e)
		{
			CommonViewsActions.ShowUserControl(ref _invoiceManagerQueueContainer, this);
		}

		private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			ChangeTheme_UC changeTheme = new ChangeTheme_UC();
			PopupBaseForm.ShowAsPopup(changeTheme, this);
		}
	}
}