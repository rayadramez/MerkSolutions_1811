using CommonControlLibrary;

namespace MerkFinance
{
	public partial class MainForm : DevExpress.XtraEditors.XtraForm
	{
		public MainForm()
		{
			InitializeComponent();
			CommonViewsActions.SetupSyle(this);
		}
	}
}