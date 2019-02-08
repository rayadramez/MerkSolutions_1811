using ApplicationConfiguration;
using CommonControlLibrary;
using CommonUserControls.MerkFinanceCommonViewers;

namespace MerkFinance
{
	public partial class MainForm : DevExpress.XtraEditors.XtraForm
	{
		private MainHomeScreen_UC _mainHomeScreen;

		public MainForm()
		{
			InitializeComponent();
			CommonViewsActions.SetupSyle(this);
			if (ApplicationStaticConfiguration.ActiveLoginUser != null)
				btnLoggingUser.Text = ApplicationStaticConfiguration.ActiveLoginUser.FullName.ToString();
		}

		private void btnHome_Click(object sender, System.EventArgs e)
		{
			CommonViewsActions.ShowUserControl(ref _mainHomeScreen, pnlMain);
			if (_mainHomeScreen != null)
				_mainHomeScreen.Initialize(this);
		}
	}
}