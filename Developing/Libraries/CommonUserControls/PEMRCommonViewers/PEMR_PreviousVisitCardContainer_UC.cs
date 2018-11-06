using System.Collections.Generic;
using System.Windows.Forms;
using CommonControlLibrary;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using MerkDataBaseBusinessLogicProject;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;

namespace CommonUserControls.PEMRCommonViewers
{
	public partial class PEMR_PreviousVisitCardContainer_UC : UserControl
	{
		public static PEMRContainer ParentContainer { get; set; }

		public PEMR_PreviousVisitCardContainer_UC()
		{
			InitializeComponent();
			CommonViewsActions.SetupSyle(this);

			CommonViewsActions.FillGridlookupEdit(lkeService, Service_cu.ItemsList);
			CommonViewsActions.FillGridlookupEdit(lkeDoctors, Doctor_cu.ItemsList, "Name_S", "Person_CU_ID");
		}

		private void btnSearch_Click(object sender, System.EventArgs e)
		{
			tabQueue.Controls.Clear();

			if ((txtPatientID.EditValue == null || string.IsNullOrEmpty(txtPatientID.Text) ||
			     string.IsNullOrWhiteSpace(txtPatientID.Text)) &&
			    (lkeDoctors.EditValue == null || string.IsNullOrEmpty(lkeDoctors.Text) ||
			     string.IsNullOrWhiteSpace(lkeDoctors.Text)) &&
			    (txtPatientName.EditValue == null || string.IsNullOrEmpty(txtPatientName.Text) ||
			     string.IsNullOrWhiteSpace(txtPatientName.Text)) &&
			    (lkeService.EditValue == null || string.IsNullOrEmpty(lkeService.Text) ||
			     string.IsNullOrWhiteSpace(lkeService.Text)) &&
			    (dtFrom.EditValue == null || string.IsNullOrEmpty(dtFrom.Text) ||
			     string.IsNullOrWhiteSpace(dtFrom.Text)) &&
			    (dtTo.EditValue == null || string.IsNullOrEmpty(dtTo.Text) || string.IsNullOrWhiteSpace(dtTo.Text)))
			{
				XtraMessageBox.Show("You should write a search criteria before searching.", "Error",
					MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button1, DefaultBoolean.Default);
				return;
			}

			List<GetPreviousMedicalVisits_Result> previousVisitsList =
				MerkDBBusinessLogicEngine.GetPreviousMedicalVisitsList(txtPatientID.EditValue, true,
					lkeService.EditValue, dtFrom.EditValue, dtTo.EditValue, lkeDoctors.EditValue);
			if (previousVisitsList.Count == 0)
			{
				XtraMessageBox.Show("No Previous visits found.", "Not Found",
					MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, DefaultBoolean.Default);
				return;
			}

			foreach (GetPreviousMedicalVisits_Result medicalVisitsResult in previousVisitsList)
			{
				PEMR_PreviousVisitCard_UC card = new PEMR_PreviousVisitCard_UC();
				card.Dock = DockStyle.Top;
				PEMRObject pemrObject = PEMRBusinessLogic.GetVisitFullTree(medicalVisitsResult);
				if (pemrObject == null)
					return;
				card.Initialize(ParentContainer, pemrObject, medicalVisitsResult);
				tabQueue.Controls.Add(card);
			}
		}
	}
}
