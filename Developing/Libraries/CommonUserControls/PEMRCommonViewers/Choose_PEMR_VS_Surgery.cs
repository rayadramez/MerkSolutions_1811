using System.Windows.Forms;
using CommonControlLibrary;

namespace CommonUserControls.PEMRCommonViewers
{
	public partial class Choose_PEMR_VS_Surgery : Form
	{
		private PEMR_ChooseApplication pemr;
		public Choose_PEMR_VS_Surgery()
		{
			InitializeComponent();
			CommonViewsActions.ShowUserControl(ref pemr, this);
		}
	}
}
