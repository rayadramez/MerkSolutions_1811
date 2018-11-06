using CommonControlLibrary;
using CommonUserControls.PEMRCommonViewers;
using DevExpress.XtraEditors;


namespace Dental_PEMR
{
	public partial class Form1 : XtraForm
	{
		private PEMRQueueContainer pemr;

		public Form1()
		{
			InitializeComponent();
			CommonViewsActions.ShowUserControl(ref pemr, this);
		}

	}
}