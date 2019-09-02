using ApplicationConfiguration;
using CommonControlLibrary;
using CommonUserControls.MerkDesignWork;

namespace MerkFinance
{
	public partial class MainForm : DevExpress.XtraEditors.XtraForm
	{
		private MerkFinanceMainMenu_UC _merkFinanceMainMenu;

		public MainForm()
		{
			InitializeComponent();
			CommonViewsActions.SetupSyle(this);
			if (ApplicationStaticConfiguration.ActiveLoginUser != null)
				btnLoggingUser.Text = ApplicationStaticConfiguration.ActiveLoginUser.FullName.ToString();

			CommonViewsActions.ShowUserControl(ref _merkFinanceMainMenu, pnlMain);
		}

		private void btnHome_Click(object sender, System.EventArgs e)
		{
			CommonViewsActions.ShowUserControl(ref _merkFinanceMainMenu, pnlMain);
			//CommonViewsActions.ShowUserControl(ref _mainHomeScreen, pnlMain);
			//if (_mainHomeScreen != null)
			//	_mainHomeScreen.Initialize(this);
		}

		private void btnMenu_Click(object sender, System.EventArgs e)
		{

		}

		private void btnInvoiceReport_Click(object sender, System.EventArgs e)
		{

		}
	}
}