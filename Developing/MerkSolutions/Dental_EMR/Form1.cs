using CommonControlLibrary;
using CommonUserControls.PEMRCommonViewers;

namespace Dental_EMR
{
	public partial class Form1 : DevExpress.XtraEditors.XtraForm
	{
		private PEMRContainer pemr;

		public Form1()
		{
			InitializeComponent();

			CommonViewsActions.ShowUserControl(ref pemr, this);
			//_paintViewer.Initialize(null);
		}
	}
}
