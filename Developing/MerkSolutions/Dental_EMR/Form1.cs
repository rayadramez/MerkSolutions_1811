using CommonControlLibrary;
using CommonUserControls.CommonViewers;
using CommonUserControls.PEMRCommonViewers;
using CommonUserControls.PEMRCommonViewers.PEMR_InternalViewers.Ophthalmology;

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
