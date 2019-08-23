using System;
using System.Windows.Forms;
using ApplicationConfiguration;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;

namespace CommonUserControls.PEMRCommonViewers
{
	public partial class PEMR_ChooseApplication : UserControl
	{
		private PEMRContainer pemr;

		public PEMR_ChooseApplication()
		{
			InitializeComponent();
		}

		private void btnPEMR_Click(object sender, EventArgs e)
		{
			ApplicationStaticConfiguration.Application = DB_Application.PEMR;
			if (ParentForm != null)
				ParentForm.Close();
			//CommonViewsActions.ShowUserControl(ref pemr, this);
		}

		private void btnSurgery_Click(object sender, EventArgs e)
		{
			ApplicationStaticConfiguration.Application = DB_Application.OphalmologySurgeryApplication;
			if (ParentForm != null)
				ParentForm.Close();
			//CommonViewsActions.ShowUserControl(ref pemr, this);
		}
	}
}
